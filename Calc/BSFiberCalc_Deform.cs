using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using TriangleNet.Geometry;

namespace BSFiberConcrete
{
    /// <summary>
    /// Расчет по прочности нормальных сечений на основе нелинейной деформационной модели
    /// </summary>
    public class BSFiberCalc_Deform : IBSFiberCalculation
    {
        public List<string> Msg { get; private set; }

        // координаты центра тяжести сечения 
        public Point CG { get; set; }
        public List<double> triAreas { get; set; }
        public List<Point> triCGs { get; set; }

        // заданные нагрузки
        // изгибающие моменты от внешней нагрузки относительно выбранных и располагаемых в пределах
        // поперечного сечения элемента координатных осей
        public double Mx { get; set; }
        public double My { get; set; }
        // продольная сила от внешней нагрузки
        public double N { get; set; }

        // балка
        public BSBeam Beam
        {
            get { return m_Beam; }
            set { m_Beam = value;
                m_Rods = value.Rods; }
        }

        /// <summary>
        /// Расстановка стержней арматуры
        /// </summary>
        public List<BSRod> Rods {
            get { return m_Rods; }
            set { m_Rods = value; } 
        }

        // свойства бетона
        public BSMatFiber MatFiber { get { return m_Fiber; } set { m_Fiber = value; } }
        // свойства арматуры
        public BSMatRod MatRebar { get { return m_Rod; } set { m_Rod = value; } }

        private BSBeam m_Beam { get; set; }

        private BSMatFiber m_Fiber;
        private BSMatRod m_Rod;

        // расчетная схема сечения
        private int m_Y_N = 1; // разбиение по высоте
        private int m_X_M = 1; // разбиение по ширине

        private List<BSElement> m_BElem;
        private List<BSRod> m_Rods;

        [DisplayName("Радиус кривизны продольной оси в плоскости действия моментов, Rx, см")]
        public double rx { get; private set; }

        [DisplayName("Радиус кривизны продольной оси в плоскости действия моментов, Ry, см")]
        public double ry { get; private set; }

        [DisplayName("Относительная деформация волокна в Ц.Т. сечения, e0") ]
        public double eps_0 { get; private set; }

        [DisplayName("Кривизна 1/Rx, 1/см" )]
        public double Kx { get; private set; }

        [DisplayName("Кривизна 1/Ry, 1/см")]
        public double Ky { get; private set; }

        [DisplayName("Максимальная относительная деформация")]
        public double e_fb_calc { get; private set; }

        // жесткостные характеристики
        private const int I1 = 0, I2 = 1, I3 = 2;
        private Matrix<double> D = DenseMatrix.OfArray(new double[,] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } });
        private double[,] Db = new double[I3 + 1, I3 + 1];
        private double[,] Ds = new double[I3 + 1, I3 + 1];

        // площадь ц.т. участка фибробетона
        private Vector<double> Ab;
        // площадь ц.т. участка арматуры
        private Vector<double> As;
        // координаты ц.т. участка фибробетона
        private Vector<double> Zfbx;
        private Vector<double> Zfby;
        // координаты ц.т. участка арматуры
        private Vector<double> Zsx;
        private Vector<double> Zsy;

        // напряжение на уровне ц.т. фибробетона
        private Vector<double> sigma_fb;
        // напряжение на уровне ц.т. арматуры
        private Vector<double> sigma_s;

        // деформации бетона
        private Vector<double> epsilon_fb;
        // деформации арматуры
        private Vector<double> epsilon_s;
        // коэффициенты упругости стержней арматуры
        private Vector<double> Nju_s;

        // коэффициенты упругости сталефибробетона
        private Vector<double> Nju_fb;

        private Dictionary<string, double> Res = new Dictionary<string, double>();

        public Dictionary<string, double> Efforts
        {
            get { return new Dictionary<string, double> 
            { 
                { "Mx, кгс*см", Mx }, 
                { "My, кгс*см", My }, 
                { "N, кгс", N } }; 
            }
        }

        public Dictionary<string, double> PhysParams
        {
            get
            {
                return new Dictionary<string, double> { 
                    { "Rfb,n, кгс/см2", m_Fiber.Rfbn }, 
                    { "Eb, кгс/см2", m_Fiber.Eb }, 
                    { "e_fb_ult", m_Fiber.Eps_fb_ult },
                    { "Rs, кгс/см2", m_Rod.Rs }, 
                    { "Es, кгс/см2", m_Rod.Es }, 
                    { "e_s_ult", m_Rod.Eps_s_ult }
                };
            }
        }

        public Dictionary<string, double> Reinforcement { get; internal set; }
        public int DeformDiagram { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_Mx">кНм</param>
        /// <param name="_My">кНм</param>
        /// <param name="_N">кН</param>
        public BSFiberCalc_Deform (double _Mx = 0, double _My = 0, double _N = 0)
        {            
            Mx = _Mx;
            My = _My;
            N  = _N;

            rx = 0;
            ry = 0;
            eps_0 = 1;

            m_Fiber = new BSMatFiber();
            m_Rod = new BSMatRod();

            Msg = new List<string>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_I">количество элементов</param>
        /// <param name="_J">количество стержней</param>
        public void InitVectors(int _I, int _J)
        {
            // площади
            Ab = Vector<double>.Build.Dense(_I);
            As = Vector<double>.Build.Dense(_J);

            // координаты ЦТ элементов сечения
            Zfbx = Vector<double>.Build.Dense(_I);            
            Zfby = Vector<double>.Build.Dense(_I);
            // координаты ЦТ арматуры
            Zsx = Vector<double>.Build.Dense(_J);
            Zsy = Vector<double>.Build.Dense(_J);

            // напряжения
            sigma_fb = Vector<double>.Build.Dense(_I);
            sigma_s = Vector<double>.Build.Dense(_J);

            // коэффициенты упругости
            Nju_fb = Vector<double>.Build.Dense(_I);
            Nju_s = Vector<double>.Build.Dense(_J);

            // отн деформации
            epsilon_fb = Vector<double>.Build.Dense(_I);
            epsilon_s = Vector<double>.Build.Dense(_J);           
        }


        public void GetSize(double[] _t = null)
        {                                   
            // цикл по элементам
            foreach (var elem in m_BElem)
            {
                int i = elem.Num;

                Zfbx[i] = elem.Z_X;
                Zfby[i] = elem.Z_Y;
                Ab[i] = elem.Area;
                Nju_fb[i] = elem.Nu;
            }

            // цикл по стержням
            foreach (var rod in m_Rods)
            {
                int j = rod.Id;

                Zsx[j] = rod.CG_X;
                Zsy[j] = rod.CG_Y;
                As[j] = rod.As;
                Nju_s[j] = rod.Nu;
            }
        }

        /// <summary>
        ///  Параметры разбиения поперечного сечения
        /// </summary>
        /// <param name="_t">N, M</param>
        public void GetParams(double[] _t = null)
        {
            m_Y_N = (int)_t[0];
            m_X_M = (int)_t[1];
        }

        private void InitElementParams()
        {
            //BSBeam_Rect beam = (BSBeam_Rect)m_Beam;

            int i = 0;
            foreach (BSElement elem in m_BElem)
            {
                sigma_fb[i] = m_Beam.Sigma_Z(N, Mx, My, elem.Z_X, elem.Z_Y);
                                             
                epsilon_fb[i] = 1; 

                Nju_fb[i] = m_BElem[i].Nu; // sigma_fb[i] / (m_Fiber.Efb * epsilon_fb[i]);
                i++;
            }

            for (int j = 0; j < m_Rods.Count; j++)
            {                
                sigma_s[j] = 1;

                epsilon_s[j] = 1; 

                Nju_s[j] = m_Rods[j].Nu; //  sigma_s[j] / (m_Rod.Es * epsilon_s[j]);
            }
        }

        /// <summary>
        /// Рассчитать жесткости D
        /// </summary>
        /// <param name="_i"></param>
        private void Calc_D(int _i = -1, int _j = -1)
        {                        
            int i = _i;
            int j = _j;

            double AZ2Eb, AZEb, AEb;
            double Zx = 0;
            double Zy = 0;

            if (i > -1) 
            {
                Zx = Zfbx[i];
                Zy = Zfby[i];
            }
            else if (j > -1)
            {
                Zx = Zsx[j];
                Zy = Zsy[j];
            }
                                                                    
            // D(1,1) :
            if (i > -1)
            {
                AZ2Eb = Ab[i] * Math.Pow(Zx, 2) * m_Fiber.Efb;
                Db[I1, I1] += AZ2Eb * Nju_fb[i];
            }
            if (j > -1)
            {
                Ds[I1, I1] += As[j] * Math.Pow(Zsx[j], 2) * m_Rod.Es * Nju_s[j];
            }
            // D(2,2) :
            if (i > -1)
            {
                AZ2Eb = Ab[i] * Math.Pow(Zy, 2) * m_Fiber.Efb;
                Db[I2, I2] += AZ2Eb * Nju_fb[i];
            }
            if (j > -1)
            {
                Ds[I2, I2] += As[j] * Math.Pow(Zsy[j], 2) * m_Rod.Es * Nju_s[j];
            }
            // D(1,2) :
            if (i > -1)
            {
                Db[I1, I2] += Ab[i] * Zx * Zy * m_Fiber.Efb * Nju_fb[i];
            }
            if (j > -1)
            {
                Ds[I1, I2] += As[j] * Zsx[j] * Zsy[j] * m_Rod.Es * Nju_s[j];
            }
            // D(1,3) :
            if (i > -1)
            {
                AZEb = Ab[i] * Zx * m_Fiber.Efb;
                Db[I1, I3] += AZEb * Nju_fb[i];
            }
            if (j > -1)
            {
                Ds[I1, I3] += As[j] * Zsx[j] * m_Rod.Es * Nju_s[j];
            }
            // D(2,3) :
            if (i > -1)
            {
                AZEb = Ab[i] * Zy * m_Fiber.Efb;
                Db[I2, I3] += AZEb * Nju_fb[i];
            }
            if (j > -1)
            {
                Ds[I2, I3] += As[j] * Zsy[j] * m_Rod.Es * Nju_s[j];
            }
            // D(3,3)
            if (i > -1)
            {
                AEb = Ab[i] * m_Fiber.Efb;
                Db[I3, I3] += AEb * Nju_fb[i];
            }
            if (j > -1)
            {             
                Ds[I3, I3] += As[j] * m_Rod.Es * Nju_s[j];
            }

            if (i > -1)
            {
                Db[I2, I1] = Db[I1, I2];
                Db[I3, I1] = Db[I1, I3];
                Db[I3, I2] = Db[I2, I3];
            }

            if (j > -1)
            {
                Ds[I2, I1] = Ds[I1, I2];
                Ds[I3, I1] = Ds[I1, I3];
                Ds[I3, I2] = Ds[I2, I3];
            }
        }

        //Рассчитать усилия
        //
        //Mx = D[I1,I1] * 1/rx + D[I1, I2] * 1/ry + D[I1, I3] * eps_0;
        //My = D[I1, I2] * 1 / ry + D[I2, I2] * 1 / ry + D[I2, I3] * eps_0; 
        //N = D[I1, I3] * 1 / rx + D[I2, I3] * 1/ry + D[I3, I3] * eps_0;
        //
        private void Calc_MxMyN()
        {
            const int I1=0, I2=1, I3=2;

            double[] v_eff = { Mx, My, N};

            //if (Mx == 0)
            //{
            //    v_eff = new double[] {My, N};
            //    D = D.RemoveRow(0);
            //    D = D.RemoveColumn(0);
            //}

            Vector<double> V_Eff = Vector<double>.Build.Dense(v_eff);                          
            Vector<double> X  = D.Solve(V_Eff);

            double kx, ky;

            if (My == 0 && Mx == 0 && N == 0) // todo удалить
            {
                (ky, eps_0) = (X[I1], X[I2]);
                
                ry = (ky != 0) ? 1 / ky : 0;
                Ky = ky;
            }
            else
            {
                // решение:
                (kx, ky, eps_0) = (X[I1], X[I2], X[I3]);

                // TODO ??
                //kx = Math.Abs(kx);
                //ky = Math.Abs(ky); 

                rx = (kx != 0) ? 1 / kx : float.MaxValue;
                ry = (ky != 0) ? 1 / ky :float.MaxValue; 

                Kx = kx;
                Ky = ky;
            }                        
        }

        private void ArraysClear()
        {
            epsilon_fb.Clear();
            epsilon_s.Clear();

            sigma_fb.Clear();
            sigma_s.Clear();

            Nju_fb.Clear();
            Nju_s.Clear();
        }

        private bool CalcResult()
        {
            bool doNextIter = true;

            double kx = (rx != 0) ? 1 / rx : float.MaxValue;
            double ky = (ry != 0) ? 1 / ry : float.MaxValue;

            Kx = kx;
            Ky = ky;

            ArraysClear();

            // бетон
            for (int i = 0; i < Zfby.Count; i++) 
            {
                double _e = eps_0 + ky * Zfby[i] + kx * Zfbx[i];
                epsilon_fb[i] = _e;
                
                // двухлинейная диаграмма для бетона
                double sgm = MatFiber.Eps_StD(_e, out int _res);
                sigma_fb[i] = Math.Sign(_e) * sgm;

                double nju_b = sigma_fb[i] / (MatFiber.Eb * _e);

                Nju_fb[i] = nju_b;
            }
           
            //арматура
            for (int j = 0; j < Zsy.Count; j++)
            {
                double _e = eps_0 + ky * Zsy[j] + kx * Zsx[j];
                epsilon_s[j] = _e;

                // двухлинейная диаграмма для арматуры
                double sgm = MatRebar.Eps_StD( Math.Abs(_e), out int _res);
                sigma_s[j] = Math.Sign(_e) * sgm;

                double nju_s = sigma_s[j] / (MatRebar.Es * _e);

                Nju_s[j] = nju_s;
            }

            double Mx_b_calc = 0;
            double My_b_calc = 0;
            double N_b_calc = 0;

            for (int i = 0; i < Zfby.Count; i ++)
            {
                double Fi = sigma_fb[i] * Ab[i];
                Mx_b_calc += Fi * Zfbx[i];
                My_b_calc += Fi * Zfby[i];
                N_b_calc += Fi;
            }

            double Mx_s_calc = 0, 
                   My_s_calc = 0, 
                   N_s_calc = 0 ;
            for (int j = 0; j < Zsy.Count; j++)
            {
                double Fj = sigma_s[j] * As[j];
                Mx_s_calc +=  Fj * Zsx[j];
                My_s_calc += Fj * Zsy[j];
                N_s_calc += Fj;
            }

            double Mx_calc = Math.Abs(Mx_b_calc + Mx_s_calc);
            double My_calc =  Math.Abs(My_b_calc + My_s_calc);
            double N_calc = Math.Abs(N_b_calc + N_s_calc);

            if (Math.Abs(Mx - Mx_calc) <= BSHelper.Epsilon &&
                Math.Abs(My - My_calc) <= BSHelper.Epsilon && 
                Math.Abs(N - N_calc) <= BSHelper.Epsilon)
            {
                doNextIter = false;
            }
            
            return doNextIter;
        }

        public static void TestMatrix()
        {
            //var m = Matrix<double>.Build.Random(500, 500);

            var M = Matrix<double>.Build;
            var m = M.DenseOfArray(new[,] {{ 1.0,  2.0, 1.0},
                               {-2.0, -3.0, 1.0},
                               { 3.0,  5.0, 0.0}});

            
            var v = Vector<double>.Build.Random(500);
            var y = m.Solve(v);

        }

        private void AddToResult(string _attr, double _value)
        {
            Res.Add(BSFiberCalculation.DsplN(typeof(BSFiberCalc_Deform), _attr), _value);
        }


        /// <summary>
        /// Расчетная схема сечения. оси кооридинат - 0 ц.т. X -вправо Y - вверх
        /// </summary>
        /// <param name="_usemesh"></param>
        /// <returns></returns>
        private List<BSElement> CalculationScheme(bool _usemesh = true)
        {            
            List<BSElement> bs = new List<BSElement>();
            
            // центр тяжести сечения           
            (double X0, double Y0) = (CG.X, CG.Y);
                        
            foreach (var t in triCGs)
            {                               
                // начало координат переносим в ц.т. сечения
                double cgX = t.X ;                
                double cgY = t.Y ;

                BSElement bsElement = new BSElement(t.ID, cgX, cgY) 
                { 
                    Area = triAreas[t.ID],
                    A = 1, 
                    B = 1 
                };

                bsElement.E = m_Beam.Mat.Eb;

                bs.Add(bsElement);                                                          
            }

            return bs;
        }

        /// <summary>
        ///  Рассчитать
        /// </summary>
        /// <returns>Успешно</returns>        
        public bool Calculate()
        {
            int cIters = 10000;

            if (triAreas.Count > 0)
            {
                m_BElem = CalculationScheme(true);
            }
            else
            {
                m_BElem = CalculationScheme(m_Y_N, m_X_M);
            }

            int qty_J = m_Beam.RodsQty; // количество стержней           
            int qty_I = m_BElem.Count; // количество элементов сечения

             double _A_s = m_Beam.AreaS(); 

            InitVectors(qty_I, qty_J);
           
            GetSize();

            InitElementParams();

            for (int iter = 0; iter < cIters; iter++)
            {
                D = DenseMatrix.OfArray(new double[,] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } });
                Db = new double[I3 + 1, I3 + 1];
                Ds = new double[I3 + 1, I3 + 1];
                
                for (int i = 0; i < qty_I; i++)
                {
                    Calc_D(i);
                }

                if (_A_s > 0)
                {
                    for (int j = 0; j < qty_J; j++)
                    {
                        Calc_D(-1, j);
                    }
                }

                var M = Matrix<double>.Build;
                D = M.DenseOfArray(Db);

                var _Ds = M.DenseOfArray(Ds);
                D = D.Add(_Ds);
              
                Calc_MxMyN();

                bool doNextIter = CalcResult();
                
                if (!doNextIter) 
                    break;
            }

            e_fb_calc = epsilon_fb.AbsoluteMaximum();

            AddToResult("eps_0", eps_0);
            AddToResult("rx", rx);
            AddToResult("Kx", Kx);
            AddToResult("ry", ry);
            AddToResult("Ky", Ky);
            AddToResult("e_fb_calc", e_fb_calc);
            
            bool res_fb = e_fb_calc <=  m_Fiber.Eps_fb_ult;            
            if (res_fb)
                Msg.Add("Проверка сечения по фибробетону пройдена");            
            else
                Msg.Add("Не пройдена проверка сечения по фибробетону");

            double e_s_calc = epsilon_s.AbsoluteMaximum();

            bool res_s = e_s_calc <= m_Rod.Eps_s_ult;
            if (res_s)
                Msg.Add("Проверка сечения по арматуре пройдена");
            else
                Msg.Add("Не пройдена проверка сечения по арматуре");

            return true;
        }

        /// <summary>
        /// Разбиваем сечение на конечные элементы
        /// </summary>
        /// <param name="_N">количество делений</param>
        private List<BSElement> CalculationScheme(int _N = 10, int _M = 1)
        {            
            BSBeam_Rect beam = (BSBeam_Rect) m_Beam;

            (double X0, double Y0) = beam.CG();                        
            double dH = beam.h / _N;
            double dB =  beam.b / _M;            

            double elX = 0, 
                   elY = 0;

            List <BSElement> bs = new List<BSElement>();
            int num = 0;
            for (int idx = 0; idx < _M; idx++) 
            {                
                for (int idy = 0; idy < _N; idy++)
                {
                    // Ось X - вправо, ось Y - вниз
                    double cgX = elX + dB / 2.0 - X0;
                    double cgY = elY + dH / 2.0 - Y0;

                    BSElement bsElement = new BSElement(num, cgX, cgY) {A = dB, B = dH };
                    bsElement.E = beam.Mat.Eb; 
                    bs.Add(bsElement);

                    elY += dH;
                    num ++;
                }

                elX += dB;
            }

            return  bs;
        }

        public Dictionary<string, double> GeomParams()
        {             
            return null;
        }

              
        // Вернуть результат
        public Dictionary<string, double> Results()
        {
            return Res;
        }        
    }
}

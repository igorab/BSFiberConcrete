using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.ApplicationServices;


namespace BSFiberConcrete
{
    /// <summary>
    /// Расчет по прочности нормальных сечений на основе нелинейной деформационной модели
    /// </summary>
    internal class BSFiberCalc_Deform : IBSFiberCalculation
    {
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
            get {return m_Beam; } 
            set { m_Beam = value; 
                  m_Rods = value.Rods; } 
        }
        // свойства бетона
        public BSMatFiber MatFiber { get { return m_Fiber; } set { m_Fiber = value; } }
        // свойства арматуры
        public BSMatRod MatRebar { get { return m_Rod; } set { m_Rod = value; } }
        
        private  BSBeam m_Beam { get; set; }

        private  BSMatFiber m_Fiber = new BSMatFiber();
        private  BSMatRod m_Rod = new BSMatRod();

        // расчетная схема сечения
        private int m_Y_N = 1; // разбиение по высоте
        private int m_X_M = 1; // разбиение по ширине
        private List<BSElement> m_BElem;
        private List<BSRod> m_Rods;

        // радиусы кривизны продольной оси в плоскостях действия моментов
        private double rx, ry;
        // относительная деформация волокна, расположенная на пересечении выбранных осей
        private double eps_0;

        // жесткостные характеристики
        private const int I1 = 0, I2 = 1, I3 = 2;
        private Matrix<double> D = DenseMatrix.OfArray( new double[,] {{0,0,0}, {0,0,0}, {0,0,0}} );
        private double[,] Db = new double[I3+1, I3+1];
        private double[,] Ds = new double[I3+1, I3+1];

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

            rx = 1;
            ry = 1;
            eps_0 = 1;            
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
                Ab[i] = elem.Ab;
                Nju_fb[i] = elem.Nu;
            }

            // цикл по стержням
            foreach (var rod in m_Rods)
            {
                int j = rod.Num;

                Zsx[j] = rod.Z_X;
                Zsy[j] = rod.Z_Y;
                As[j] = rod.As;
                Nju_s[j] = rod.Nu;
            }
        }

        /// <summary>
        ///  Параметры разбиения поперечного сечения
        /// </summary>
        /// <param name="_t"></param>
        public void GetParams(double[] _t = null)
        {
            m_Y_N = (int)_t[0];
            m_X_M = (int)_t[1];
        }

        private void InitElementParams()
        {
            BSBeam_Rect beam = (BSBeam_Rect)m_Beam;

            int i = 0;
            foreach (var elem in m_BElem)
            {
                sigma_fb[i] = beam.Sigma_Z(N, Mx, My, elem.Z_X, elem.Z_Y);

                double sgm = 0, eps = 0; 
                sgm = m_Fiber.Eps_StateDiagram(eps);
                                
                epsilon_fb[i] = 1; 

                Nju_fb[i] = m_BElem[i].Nu; // sigma_fb[i] / (m_Fiber.Efb * epsilon_fb[i]);
                i++;
            }

            for (int j = 0; j < m_Rods.Count; j++)
            {
                //MatRebar.Eps_StateDiagram(0);

                sigma_s[j] = 1;

                epsilon_s[j] = 1; 

                Nju_s[j] = m_Rods[j].Nu; //  sigma_s[j] / (m_Rod.Es * epsilon_s[j]);
            }
        }

        /// <summary>
        /// Рассчитать жесткости D
        /// </summary>
        /// <param name="_i"></param>
        private void Calc_D(int _i, int _j = -1)
        {            
            int i = _i;
            int j = _j;

            double AZ2Eb, AZEb, AEb;
            double Zx = 0; 
            if (i > -1) Zx = Zfbx[i];
            double Zy = 0; 
            if (i > -1) Zy = Zfby[i];

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

            Db[I2, I1] = Db[I1, I2];
            Db[I3, I1] = Db[I1, I3];
            Db[I3, I2] = Db[I2, I3];

            Ds[I2, I1] = Ds[I1, I2];
            Ds[I3, I1] = Ds[I1, I3];
            Ds[I3, I2] = Ds[I2, I3];
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

            if (Mx == 0)
            {
                v_eff = new double[] {My, N};
                D = D.RemoveRow(0);
                D = D.RemoveColumn(0);
            }

            Vector<double> V_Eff = Vector<double>.Build.Dense(v_eff);                          
            Vector<double> X  = D.Solve(V_Eff);

            double kx, ky;

            if (Mx == 0)
            {
                (ky, eps_0) = (X[I1], X[I2]);
                
                ry = 1 / ky;
            }
            else
            {
                (kx, ky, eps_0) = (X[I1], X[I2], X[I3]);
                rx = 1 / kx;
                ry = 1 / ky;
            }                        
        }

        private bool CalcResult()
        {
            bool doNextIter = true;

            double kx = 1/rx, ky = 1/ry;

            for (int i = 0; i < Zfby.Count; i++) 
            {
                double _e = eps_0 + ky * Zfby[i];
                epsilon_fb[i] = _e;

                double sgm = MatFiber.Eps_StD( - _e);
                sigma_fb[i] = sgm;

                double nju_b = sgm / (MatFiber.Eb_red * _e);

                Nju_fb[i] = Math.Abs(nju_b);
            }

            for (int j = 0; j < Zsy.Count; j++)
            {
                double _e = eps_0 + ky * Zsy[j];
                epsilon_s[j] = _e;

                double sgm = MatRebar.Eps_StD(_e);
                sigma_s[j] = sgm;

                double nju_s = sgm / (MatRebar.Es * _e);

                Nju_s[j] = Math.Abs(nju_s);
            }

            double Mx_calc = 0;
            double My_calc = 0;
            double N_calc = 0;

            for (int i = 0; i < Zfby.Count; i ++)
            {
                double Fi = sigma_fb[i] * Ab[i];
                Mx_calc += Fi * Zfbx[i];
                My_calc += Fi * Zfby[i];
                N_calc += Fi;
            }

            for (int j = 0; j < Zsy.Count; j++)
            {
                double Fj = sigma_s[j] * As[j];
                Mx_calc +=  Fj * Zsx[j];
                My_calc += Fj * Zsy[j];
                N_calc += Fj;
            }

            
            if (Math.Abs(Mx - Mx_calc) > BSHelper.Epsilon ||
                Math.Abs(My - My_calc) > BSHelper.Epsilon || 
                Math.Abs(N - N_calc) > BSHelper.Epsilon)
            {
                doNextIter = false;
            }

            Dictionary<string, double> res = new Dictionary<string, double>() { { "e0", eps_0 }, {"kx", kx }, {"ky", ky } };

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

        //
        // Рассчитать
        //
        public void Calculate()
        {
            int cIters = 10;

            m_BElem = CalculationScheme(m_Y_N, m_X_M);

            int qty_J = m_Beam.RodsQty; // количество стержней           
            int qty_I = m_BElem.Count; // количество элементов сечения

            double _A_s = m_Beam.AreaS(); 

            InitVectors(qty_I, qty_J);
           
            GetSize();

            InitElementParams();

            for (int iter = 0; iter < cIters; iter++)
            {
                
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
                
                /*
                D = DenseMatrix.OfArray(new double[,] 
                { 
                    { Db[I1,I1], Db[I1,I2], Db[I1,I3] }, 
                    { Db[I2,I1], Db[I2,I2], Db[I2,I3] }, 
                    { Db[I3,I1], Db[I3,I2], Db[I3,I3] } 
                });
                */

                Calc_MxMyN();

                bool doNextIter = CalcResult();
                
                if (!doNextIter) 
                    break;
            }

            bool res_fb = epsilon_fb.AbsoluteMaximum() <=  m_Fiber.Eps_fb_ult;
            Res.Add("res_fb", Convert.ToDouble(res_fb));

            bool res_s = epsilon_s.AbsoluteMaximum() <= m_Rod.Eps_s_ult;
            Res.Add("res_s", Convert.ToDouble(res_s));
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
                    bsElement.E = beam.BSMat.Eb; 
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
            throw new NotImplementedException();
        }

              
        // Вернуть результат
        public Dictionary<string, double> Results()
        {
            return Res;
        }        
    }
}

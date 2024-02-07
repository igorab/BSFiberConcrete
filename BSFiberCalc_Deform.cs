using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using Microsoft.VisualBasic;


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
        public BSBeam Beam { get {return m_Beam; } set { m_Beam = value; } }
        // свойства бетона
        public BSMatFiber MatFiber { get { return m_Fiber; } set { m_Fiber = value; } }
        // свойства арматуры
        public BSMatRod MatRebar { get { return m_Rod; } set { m_Rod = value; } }


        private const int I = 3;
        private  BSBeam m_Beam { get; set; }
        private  BSMatFiber m_Fiber = new BSMatFiber();
        private  BSMatRod m_Rod = new BSMatRod();

        // расчетная схема сечения
        private List<BSElement> m_BS = new List<BSElement>();
        private List<BSRod> m_Rods = new List<BSRod>();

        // радиусы кривизны продольной оси в плоскостях действия моментов
        private double rx, ry;
        // относительная деформация волокна, расположенная на пересечении выбранных осей
        private double eps_0;

        // жесткостные характеристики
        private Matrix<double> D = DenseMatrix.OfArray( new double[,] {{0,0,0}, {0,0,0}, {0,0,0}} );
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
        private Vector<double> nu_s;

        // коэффициенты упругости сталефибробетона
        private Vector<double> nu_fb;

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

            // координаты ЦТ
            Zfbx = Vector<double>.Build.Dense(_I);            
            Zfby = Vector<double>.Build.Dense(_I);

            Zsx = Vector<double>.Build.Dense(_J);
            Zsy = Vector<double>.Build.Dense(_J);

            // напряжения
            sigma_fb = Vector<double>.Build.Dense(_I);
            sigma_s = Vector<double>.Build.Dense(_J);

            // коэффициенты упругости
            nu_fb = Vector<double>.Build.Dense(_I);
            nu_s = Vector<double>.Build.Dense(_J);

            // отн деформации
            epsilon_fb = Vector<double>.Build.Dense(_I);
            epsilon_s = Vector<double>.Build.Dense(_J);

            foreach (var bs in m_BS)
            {
                Ab[bs.Num] = bs.Ab;
                nu_fb[bs.Num] = 1;
            }
        }


        public void GetSize(double[] _t = null)
        {                                   
            // цикл по элементам
            foreach (var elem in m_BS)
            {
                int i = elem.Num;

                Zfbx[i] = elem.Z_X;
                Zfby[i] = elem.Z_Y;
                Ab[i] = elem.Ab;
            }

            // цикл по стержням
            foreach (var rod in m_Rods)
            {
                int j = rod.Num;

                Zsx[j] = rod.Z_X;
                Zfby[j] = rod.Z_Y;
                As[j] = rod.As;
            }
        }

        public void GetParams(double[] _t = null)
        {
            for (int i = 0; i < m_BS.Count; i++)
            {
                nu_fb[i] = sigma_fb[i] / (m_Fiber.Efb * epsilon_fb[i]);
            }

            for (int j = 0; j < m_Rods.Count; j++)
            {
                nu_s[j] = sigma_s[j] / (m_Rod.Es * epsilon_s[j]);
            }
        }

        /// <summary>
        /// Рассчитать жесткости D
        /// </summary>
        /// <param name="_i"></param>
        private void Calc_D(int _i, int _j = -1)
        {
            int I1 = 0, I2 = 1, I3 = 2;
            int i = _i;
            int j = 0;

            D[I1, I1] += Ab[i] * Math.Pow(Zfbx[i], 2) * m_Fiber.Efb * nu_fb[i];
            if (j > 0)
                D[I1, I1] += As[j] * Math.Pow(Zsx[j], 2) * m_Rod.Es * nu_s[j];

            D[I2, I2] += Ab[i] * Math.Pow(Zfby[i], 2) * m_Fiber.Efb * nu_fb[i];
            if (j > 0)
                D[I2, I2] += As[j] * Math.Pow(Zsx[j], 2) * m_Rod.Es * nu_s[j];

            D[I1, I2] += Ab[i] * Zfbx[i] * Zfby[i] * m_Fiber.Efb * nu_fb[i];
            if (j > 0)
                D[I1, I2] += As[j] * Zsx[j] * Zsy[j] * m_Rod.Es * nu_s[j];

            D[I1, I3] += Ab[i] * Zfbx[i] * m_Fiber.Efb * nu_fb[i];
            if (j > 0)
                D[I1, I3] += As[j] * Zsx[j] * Zsx[j] * m_Rod.Es * nu_s[j];

            D[I2, I3] += Ab[i] * Zfby[i] * m_Fiber.Efb * nu_fb[i];
            if (j > 0)
                D[I2, I3] += As[j] * Zsy[j] * m_Rod.Es * nu_s[j];

            D[I3, I3] += Ab[i] * m_Fiber.Efb * nu_fb[i];
            if (j > 0)
                D[I3, I3] += As[j] * m_Rod.Es * nu_s[j];
        }


        //Рассчитать усилия
        private void Calc_MxMyN()
        {
            int I1=0, I2=1, I3=2;
           
            double M_x = D[I1,I1] * 1/rx + D[I1, I2] * 1/ry + D[I1, I3] * eps_0;
            double M_y = D[I1, I2] * 1 / ry + D[I2, I2] * 1 / ry + D[I2, I3] * eps_0; 
            double N_ = D[I1, I3] * 1 / rx + D[I2, I3] * 1/ry + D[I3, I3] * eps_0; 
        }

        private void CalcResult()
        {
            double kx = 1/rx, ky = 1/ry;
            Dictionary<string, double> res = new Dictionary<string, double>() { { "e0", eps_0 }, {"kx", kx }, {"ky", ky } };
        }

        // Рассчитать
        public void Calculate()
        {
            CalculationScheme();

            int qty_J = m_Beam.RodsQty; // количество стержней           
            int qty_I = m_BS.Count; // количество элементов сечения

            InitVectors(qty_I, qty_J);
           
            GetSize();

            GetParams();
            
            for (int i = 0; i < qty_I; i++)
            {
                Calc_D(i);
            }

            Calc_MxMyN();            
            
            CalcResult();

            bool res_fb = epsilon_fb.AbsoluteMaximum() <=  m_Fiber.Eps_fb_ult;
            Res.Add("res_fb", Convert.ToDouble(res_fb));

            bool res_s = epsilon_s.AbsoluteMaximum() <= m_Rod.Eps_s_ult;
            Res.Add("res_s", Convert.ToDouble(res_s));
        }

        /// <summary>
        /// Разбиваем сечение на конечные элементы
        /// </summary>
        /// <param name="_N">количество делений</param>
        private void CalculationScheme(int _N = 10)
        {
            BSBeam_Rect beam = (BSBeam_Rect) m_Beam;
            double dB =  beam.b / _N;
            double dH = beam.h / _N;

            double lX = 0, lY = 0;

            List <BSElement> bs = new List<BSElement>();
            int num = 0;
            for (int idx = 0; idx < _N; idx++) 
            {
                lX += dB;
                for (int idy = 0; idy < _N; idy++)
                {
                    lY += dH;
                    BSElement bsElement = new BSElement(num, lX, lY);
                    bs.Add(bsElement);
                    num ++;
                }                    
            }

            m_BS = bs;
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

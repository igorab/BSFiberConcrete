using System;
using System.Collections.Generic;
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

        public BSBeam Beam { get {return m_Beam; } set { m_Beam = value; } }
        public BSMatFiber MatFiber { get { return m_Fiber; } set { m_Fiber = value; } }
        public BSMatRod MatRebar { get { return m_Rod; } set { m_Rod = value; } }


        private const int I = 3;
        private  BSBeam m_Beam { get; set; }
        private  BSMatFiber m_Fiber = new BSMatFiber();
        private  BSMatRod m_Rod = new BSMatRod();
        
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

            InitVectors();
        }

        private void InitVectors()
        {
            Ab = Vector<double>.Build.Dense(I);
            As = Vector<double>.Build.Dense(I);

            Zfbx = Vector<double>.Build.Dense(I);
            Zfby = Vector<double>.Build.Dense(I);
            sigma_fb = Vector<double>.Build.Dense(I);
            sigma_s = Vector<double>.Build.Dense(I);
            nu_fb = Vector<double>.Build.Dense(I);
            nu_s = Vector<double>.Build.Dense(I);
            epsilon_fb = Vector<double>.Build.Dense(I);
            epsilon_s = Vector<double>.Build.Dense(I);
        }
        
        private void CalcD()
        {
            int I1 = 0, I2 = 1, I3 = 2;
            int i = 0;
            int j = 0;

            D[I1, I1] += Ab[i] * Math.Pow(Zfbx[i], 2) * m_Fiber.Efb * nu_fb[i];
            D[I1, I1] += As[j] * Math.Pow(Zsx[j], 2) * m_Rod.Es * nu_s[j];

            D[I2, I2] += Ab[i] * Math.Pow(Zfby[i], 2) * m_Fiber.Efb * nu_fb[i];
            D[I2, I2] += As[j] * Math.Pow(Zsx[j], 2) * m_Rod.Es * nu_s[j];

            D[I1, I2] += Ab[i] * Zfbx[i] * Zfby[i] * m_Fiber.Efb * nu_fb[i];
            D[I1, I2] += As[j] * Zsx[j] * Zsy[j] * m_Rod.Es * nu_s[j];

            D[I1, I3] += Ab[i] * Zfbx[i] * m_Fiber.Efb * nu_fb[i];
            D[I1, I3] += As[j] * Zsx[j] * Zsx[j] * m_Rod.Es * nu_s[j];

            D[I2, I3] += Ab[i] * Zfby[i] * m_Fiber.Efb * nu_fb[i];
            D[I2, I3] += As[j] * Zsy[j] * m_Rod.Es * nu_s[i];

            D[I3, I3] += Ab[i] * m_Fiber.Efb * nu_fb[i];
            D[I3, I3] += As[j] * m_Rod.Es * nu_s[i];
        }


        private void CalcMN()
        {
            int I1=0, I2=1, I3=2;
           
            double M_x = D[I1,I1] * 1/rx + D[I1, I2] * 1/ry + D[I1, I3] * eps_0;
            double M_y = D[I1, I2] * 1 / ry + D[I2, I2] * 1 / ry + D[I2, I3] * eps_0; 
            double N_ = D[I1, I3] * 1 / rx + D[I2, I3] * 1/ry + D[I3, I3] * eps_0; 
        }

        // Рассчитать
        public void Calculate()
        {
            int j = m_Beam.RodsQty;

            CalculationScheme();

            CalcD();

            CalcMN();            

            for (int i = 0; i < I; i++) 
            {
                nu_fb[i] = sigma_fb[i] / (m_Fiber.Efb * epsilon_fb[i]);
            }

            bool res_fb = epsilon_fb.AbsoluteMaximum() <=  m_Fiber.Eps_fb_ult;
            Res.Add("res_fb", Convert.ToDouble(res_fb));

            bool res_s = epsilon_s.AbsoluteMaximum() <= m_Rod.Eps_s_ult;
            Res.Add("res_s", Convert.ToDouble(res_s));
        }

        private void CalculationScheme()
        {
            BSBeam_Rect beam = (BSBeam_Rect) m_Beam;
            double dB =  beam.b / 100;
            double dH = beam.h / 100;

            List<double> bs = new List<double>();

        }

        public Dictionary<string, double> GeomParams()
        {
            throw new NotImplementedException();
        }

        public void GetParams(double[] _t)
        {
            throw new NotImplementedException();
        }

        public void GetSize(double[] _t)
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

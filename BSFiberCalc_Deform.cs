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

        private const int I = 3;

        private BSBeam beam = new BSBeam();
        private readonly BSMatFiber matFiber = new BSMatFiber();
        private readonly BSMatRod matRod = new BSMatRod();
        
        // радиусы кривизны продольной оси в плоскостях действия моментов
        private double rx, ry;
        // относительная деформация волокна, расположенная на пересечении выбранных осей
        private double eps_0;

        // жесткостные характеристики
        private Matrix<double> D = DenseMatrix.OfArray( new double[,] {{0,0,0}, {0,0,0}, {0,0,0}} );
        private Vector<double> Zfbx;
        private Vector<double> Zfby;
        private Vector<double> sigma_fb;

        // деформации бетона
        private Vector<double> epsilon_fb;
        // деформации арматуры
        private Vector<double> epsilon_sj;
        // коэффициенты упругости стержней арматуры
        private Vector<double> nu_sj;

        // коэффициенты упругости сталефибробетона
        private Vector<double> nu_fb;

        public BSFiberCalc_Deform (double _Mx = 0, double _My = 0, double _N = 0)
        {
            Mx = _Mx;
            My = _My;
            N  = _N;

            InitVectors();
        }

        private void InitVectors()
        {            
            Zfbx = Vector<double>.Build.Dense(I);
            Zfby = Vector<double>.Build.Dense(I);
            sigma_fb = Vector<double>.Build.Dense(I);
            nu_fb = Vector<double>.Build.Dense(I);
            nu_sj = Vector<double>.Build.Dense(I);
        }
        

        private void CalcMN()
        {
            double Mx = D[1,1] * 1/rx + D[1, 2] * 1/ry + D[1, I] * eps_0;
            double My = D[1, 2] * 1 / ry + D[2, 2] * 1 / ry + D[2, I] * eps_0; 
            double N = D[1, I] * 1 / rx + D[2, I] * 1/ry + D[I, I] * eps_0; 

        }


        public void Calculate()
        {
            int j = beam.Rods;

            CalcMN();            

            for (int i = 0; i < I; i++) 
            {
                nu_fb[i] = sigma_fb[i] / (matFiber.Efb * epsilon_fb[i]);
            }

            bool res_fb = epsilon_fb.AbsoluteMaximum() <=  matFiber.Eps_fb_ult;

            bool res_s = epsilon_sj.AbsoluteMaximum() <= matRod.Eps_s_ult;

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

        public Dictionary<string, double> Results()
        {

            throw new NotImplementedException();
        }
    }
}

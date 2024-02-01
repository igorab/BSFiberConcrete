using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;


namespace BSFiberConcrete
{
    /// <summary>
    /// Расчет по прочности нормальных сечений на основе нелинейной деформационной модели
    /// </summary>
    internal class BSFiberCalc_Deform : IBSFiberCalculation
    {
        private BSBeam beam = new BSBeam();
        private double rx, ry;
        private double eps_0;

        Matrix<double> D = DenseMatrix.OfArray(new double[,] {
            {1,1,1},
            {1,2,3},
            {4,3,2}});

        private void InitD()
        {
            D[1, 1] = 0;
            D[2, 2] = 0;
            D[1, 2] = 0;
            D[1, 3] = 0;
            D[2, 3] = 0;
            D[3, 3] = 0;
        }

        private void CalcMN()
        {
            double Mx = D[1,1] * 1/rx + D[1, 2] * 1/ry + D[1,3] * eps_0;
            double My = D[1, 2] * 1 / ry + D[2, 2] * 1 / ry + D[2, 3] * eps_0; 
            double N = D[1, 3] * 1 / rx + D[2, 3] * 1/ry + D[3, 3] * eps_0; 
        }

        public void Calculate()
        {
            int j = beam.Rods;


            //throw new NotImplementedException();
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

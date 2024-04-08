using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using MathNet.Numerics;

namespace BSFiberConcrete.BSRFib
{
    /// <summary>
    /// Определение остаточной прочности сталефибробетона на растяжение
    /// </summary>
    public class BSRFibLabTensile
    {
        /// <summary>
        /// длина пролета, см
        /// </summary>
        public double L { get; set; }

        /// <summary>
        /// ширина образца, см
        /// </summary>
        public double b { get; set; }

        /// <summary>
        /// расстояние между вершиной надреза и верхней гранью образца, cм;
        /// </summary>
        public double h_sp { get; set; }

        // коэффициенты учета неупругих деформаций в
        // сталефибробетоне растянутой зоны образца
        private double k_F05;
        private double k_F25;

        private double k_Fel;

        // см
        public double a_F05 { get; }
        // см
        public double a_F25 { get; }

        public List<FaF> DsFaF
        {
            set 
            { 
                dsFaF = new List<FaF>(value); 
                dsFaF.OrderBy(_x => _x.aF); 
            }
        }
        
        private  List<FaF> dsFaF;

        public BSRFibLabTensile()
        {
            k_F05 = 0.4;
            k_F25 = 0.34;
            //коэффициент учета неупругих деформаций в сталефибробетоне растянутой зоны образца
            k_Fel = 0.6;

            a_F05 = 0.05;
            a_F25 = 0.25;

            h_sp = 12.5;
        }


        public double F05()
        {
            double r = dsFaF.Min(_x=> _x.F);

            double[] x = new double[] { 0, 1 };
            double[] y = new double[] { 2, 3 };

            var fy = Fit.LineFunc(x, y);
            var z = fy(0.05);


            return z;
        }

        public double F25()
        {
            double r = dsFaF.Max(_x => _x.F);

            double[] x = new double[] { 0, 1 };
            double[] y = new double[] { 2, 3 };

            var fy = Fit.LineFunc(x, y);
            var z = fy(0.25);

            return z;
        }


        public double R_F05(double _F05)
        {
            double r = ((3 * _F05 * L) / (2 * b * h_sp * h_sp)) * k_F05;
            return r;
        }

        public double R_F25(double _F25)
        {
            double r = ((3 * _F25 * L) / (2 * b * h_sp * h_sp)) * k_F25;
            return r;
        }

        public double R_Fel(double _Fel)
        {
            double r = ((3 * _Fel * L) / (2 * b * h_sp * h_sp)) * k_F25;
            return r;
        }


    }
}

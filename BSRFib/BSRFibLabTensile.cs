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
            // коэффициенты учета неупругих деформаций в сталефибробетоне растянутой зоны образца >>
            k_F05 = 0.4;
            k_F25 = 0.34;            
            // <<


            a_F05 = 0.05; // mm
            a_F25 = 2.5; // mm

            h_sp = 125; //mm
        }


        /// <summary>
        ///  получение значения F по линейной интерполяции
        /// </summary>
        /// <param name="_a">искомый аргумент</param>
        /// <returns>Значение функции </returns>
        public double LinearFit(double _a)
        {            
            var r_from = dsFaF.First();
            var r_to = dsFaF.Last();

            if (_a <= r_from.aF)
                return r_from.F;

            if (_a >= r_to.aF)
                return r_to.F;

            var from = dsFaF.Where(_x => _x.aF < _a)?.Last();
            var to = dsFaF.Where(_x => _x.aF > _a)?.First();

            double[] x = new double[] { from.aF, to.aF };
            double[] y = new double[] { from.F, to.F };

            var fy = Fit.LineFunc(x, y);
            var _F = fy(_a);

            return _F;
        }

        public double F05()
        {           
            var z = LinearFit(a_F05);
            return z;
        }

        public double F25()
        {
            var z = LinearFit(a_F25);
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

        public static double Defl_aF_f(double _f, int _dec = 4 )
        {
            return Math.Round( (_f - 0.04) / 0.085, _dec);
        }

    }
}

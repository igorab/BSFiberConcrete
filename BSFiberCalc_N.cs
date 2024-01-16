using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BSFiberConcrete
{
    internal class BSFiberCalc_N : IBSFiberCalculation
    {
        [DisplayName("Высота сечения, см")]
        public double h { get; private set; }

        [DisplayName("Ширина сечения, см")]
        public double b { get; private set; }

        [DisplayName("Площадь сечения, см")]
        public double A { get; private set; }

        [DisplayName("Продольное усилие, кг")]
        public double N { get; private set; }

        [DisplayName("Расчетная длинна элемента сп63 см")]
        public double l0 { get; private set; }

        [DisplayName("случайный эксцентриситет, принимаемый по СП 63")]
        public double e0 { get; private set; }

        [DisplayName("Момент инерции прямоугольного сечения")]
        public double I { get; private set; }

        [DisplayName("Момент от действия полной нагрузки")]
        public double M1 { get; private set; }

        [DisplayName("Момент от действия постянных и длительных нагрузок нагрузок")]
        public double Ml1 { get; private set; }

        [DisplayName("Коэффициент, учитывающий влияние длительности действия нагрузки, определяют по формуле (6.27)")]
        public double fi1 { get; private set; }

        [DisplayName("относительное значение эксцентриситета продольной силы")]
        public double delta_e { get; private set; }

        [DisplayName("коэффициент ф.")]
        public double k_b { get; private set; }


        public void Calculate()
        {
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

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

        double Yft, Yb1, Yb2, Yb3, Yb5;
        double Rfbn, Yb;
        double fi = 0.9;
        double Ef, //для фибры из тонкой низкоуглеродистой проволоки МП п.п  кг/см2 
               Eb; //Начальный модуль упругости бетона-матрицы B30 СП63
        //коэффициент фибрового армирования по объему
        double mu_fv;
        //Модуль упругости сталефибробетона п.п. (5.2.7)
        double Efb;
        // жесткость элемента в предельной по прочности стадии,определяемая по формуле (6.25)
        double D;
        //условная критическая сила, определяемая по формуле (6.24)
        double Ncr;
        //коэффициент, учитывающий влияние продольного изгиба (прогиба) элемента на его несущую способность и определяемый по формуле(6.23)
        double eta;
        //площадь сжатой зоны бетона ф. (6.22)
        double Ab;

        public void Calculate()
        {
            M1 = 1;
            Ml1 = 1;
            fi1 = 1 + Ml1 / M1;
            delta_e = e0 / h;

            if (delta_e <= 0.15) 
                { delta_e = 0.15; }
            else if (delta_e >= 1.5) 
                { delta_e = 1.5; }

            k_b = 0.15 / (fi1 * (0.3d + delta_e));
                       
            Efb = Eb * (1 - mu_fv) + Ef * mu_fv;

            D = k_b * Efb * I;

            Ncr = Math.PI * Math.PI * D / Math.Pow(l0, 2);

            eta = 1 / (1 - N / Ncr);

            Ab = b * h * (1 - 2 * e0 * eta / h);

            double Rfb = Rfbn / Yb * Yb1 * Yb2 * Yb3 * Yb5;

            double Nult = fi * Rfb * A;

        }

        public Dictionary<string, double> GeomParams()
        {
            throw new NotImplementedException();
        }

        public void GetParams(double[] _t)
        {
            A = b * h;
            e0 = 2;
            I = b * h*h*h/12;
            Ef = 1936799;
            Eb = 331294;
            mu_fv = 0.005;
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

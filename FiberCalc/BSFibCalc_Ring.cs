using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BSFiberConcrete
{
    [DisplayName("Расчет прочности изгибаемого элемента кольцевого сечения")]
    public class BSFibCalc_Ring : BSFiberCalculation
    {
        [DisplayName("Радиус внутренней грани, см")]
        public double r1 { get; private set; }

        [DisplayName("Радиус наружней грани, см")]
        public double r2 { get; private set; }

        [DisplayName("Расчетные значения сопротивления  на сжатиие по B30 СП63 кг/см2")]
        public double Rfbn { get; private set; }

        [DisplayName("Значения коэффициента надежности по бетону при сжатии СП63")]
        public double Yb { get; private set; }

        [DisplayName("Расчетное значение сопротивления на сжатиие по B30 СП63 СП360")]
        public double Rfb { get; private set; }

        [DisplayName("Расчетное значение остаточного сопротивления осевому растяжению")]
        public new double Rfbt3 { get; private set; }

        [DisplayName("Предельный момент сечения")]
        public double Mult { get; private set; }


        public override void GetParams(double[] _t)
        {
            base.GetParams(_t);

            (Rfbt3n, Rfbn, Yft, Yb, Yb1, Yb2, Yb3, Yb5) = (_t[0], _t[1], _t[2], _t[3], _t[4], _t[5], _t[6], _t[7]);
        }

        public override Dictionary<string, double> GeomParams()
        {
            Dictionary<string, double> geom = base.GeomParams();
            geom.Add(DN(typeof(BSFibCalc_Ring), "r1"), r1);
            geom.Add(DN(typeof(BSFibCalc_Ring), "r2"), r2);
            return geom;
        }

        public override void GetSize(double[] _t)
        {
            (r1, r2) = (_t[0], _t[1]);
        }

        public override Dictionary<string, double> Results()
        {
            return new Dictionary<string, double>() {
                { DN(typeof(BSFibCalc_Ring), "Rfbt3"), Math.Round(Rfbt3, 4) },
                { DN(typeof(BSFibCalc_Ring), "Mult"), Math.Round(Mult, 4) }
            };
        }

        public override void Calculate()
        {
            Rfb = Rfbn / Yb * Yb1 * Yb2 * Yb3 * Yb5;

            Rfbt3 = Rfbt3n / Yft * Yb1 * Yb5;

            //толщина стенки кольца см
            double tr = r2 - r1;

            if (tr < 0)
                throw new Exception("r2-r1 < 0");

            //радиус срединной поверхности стенки кольцевого элемента, определяемый по ф. (6.19)
            double rm = (r1 + r2) / 2;

            //Общая площадь кольцевого сечения, определяемая по формуле (6.18)
            double Ar = 2 * Math.PI * rm * tr;

            double ar = (0.73d * Rfbt3) / (Rfb + 2 * Rfbt3);

            //Предельный момент сечения
            Mult = Ar * (Rfb * Math.Sin(Math.PI * ar) / Math.PI + 0.234d * Rfbt3) * rm;

            //Предельный момент сечения  (т*м)
            Mult = Math.Round(Mult * 0.00001, 4);
        }
    }
}

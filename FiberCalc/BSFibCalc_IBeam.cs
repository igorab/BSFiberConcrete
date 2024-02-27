using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BSFiberConcrete
{
    [BSFiberCalculationAttribute(Descr = "Расчет балки двутаврового сечения")]
    public class BSFibCalc_IBeam : BSFiberCalculation
    {
        // размеры:
        [DisplayName("Ширина нижней полки двутавра")]
        public double bf { get; protected set; }
        [DisplayName("Высота нижней полки двутавра")]
        public double hf { get; protected set; }
        [DisplayName("Высота стенки двутавра")]
        public double hw { get; protected set; }
        [DisplayName("Ширина стенки двутавра")]
        public double bw { get; protected set; }
        [DisplayName("Ширина верхней полки двутавра")]
        public double b1f { get; protected set; }
        [DisplayName("Высота верхней полки двутавра")]
        public double h1f { get; protected set; }

        // физ. характеристики бетона
        [DisplayName("Расчетные значения сопротивления на сжатиие по СП63 кг/см2")]
        public new double Rfbn { get; protected set; }
        [DisplayName("Значения коэффициента надежности по бетону при сжатии СП63")]
        public double Yb { get; protected set; }

        // Результаты
        [DisplayName("Высота сжатой зоны, см")]
        public double x { get; protected set; }

        [DisplayName("Предельный момент сечения, т*м")]
        public double Mult { get; protected set; }

        public override void GetParams(double[] _t)
        {
            base.GetParams(_t);

            (Rfbt3n, Rfbn, Yft, Yb, Yb1, Yb2, Yb3, Yb5) = (_t[0], _t[1], _t[2], _t[3], _t[4], _t[5], _t[6], _t[7]);
        }

        public override Dictionary<string, double> GeomParams()
        {
            Dictionary<string, double> geom = base.GeomParams();
            geom.Add(DN(typeof(BSFibCalc_IBeam), "bf"), bf);
            geom.Add(DN(typeof(BSFibCalc_IBeam), "hf"), hf);
            geom.Add(DN(typeof(BSFibCalc_IBeam), "hw"), hw);
            geom.Add(DN(typeof(BSFibCalc_IBeam), "bw"), bw);
            geom.Add(DN(typeof(BSFibCalc_IBeam), "b1f"), b1f);
            geom.Add(DN(typeof(BSFibCalc_IBeam), "h1f"), h1f);
            return geom;
        }

        public override void GetSize(double[] _t)
        {
            (bf, hf, hw, bw, b1f, h1f) = (_t[0], _t[1], _t[2], _t[3], _t[4], _t[5]);
        }

        protected void Calc_Pre()
        {
            //Расчетное остаточное остаточного сопротивления осевому растяжению
            Rfbt3 = (Rfbt3n / Yft) * Yb1 * Yb5;

            //Расчетные значения сопротивления  на сжатиие по B30 СП63
            Rfb = Rfbn / Yb * Yb1 * Yb2 * Yb3 * Yb5;
        }

        public override void Calculate()
        {
            Calc_Pre();

            Action calc_a = delegate
            {
                x = Rfbt3 * (b1f * h1f + bw * hw + bf * hf) / (b1f * (Rfbt3 + Rfb));

                Mult = 0.5 * Rfbt3 * (b1f * (h1f - x) * (h1f + x) + bf * hf * (hf - x + 2 * (hw + h1f)) + bw * hw * (hw - x + 2 * h1f));
            };

            Action calc_b = delegate
            {
                x = Rfbt3 * (bw * h1f + bw * hw + bw * h1f) / (bw * (Rfbt3 + Rfb));

                Mult = Rfb * bw * (x - h1f);
            };


            bool cond = Rfbt3 * (bf * hf + bw * hw) < Rfb * b1f * h1f;

            if (cond)
            {
                calc_a();
            }
            else
            {
                calc_b();
            }

            Mult = Math.Round(Mult * 0.00001d, 4);
        }

        public override Dictionary<string, double> Results()
        {
            return new Dictionary<string, double>() {
                { DN(typeof(BSFibCalc_IBeam), "x"), Math.Round(x, 4) },
                { DN(typeof(BSFibCalc_IBeam), "Mult") , Mult }
            };
        }
    }
}

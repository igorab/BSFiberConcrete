using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFiberConcrete
{
    internal class BSFiberCalc_Norm
    {
    }

    [BSFiberCalculationAttribute(Descr = "Расчет балки таврового сечения")]
    public class BSFibCalc_TBeam : BSFiberCalculation
    {
    }

    [BSFiberCalculationAttribute(Descr = "Расчет балки двутаврового сечения")]
    public class BSFibCalc_IBeam : BSFiberCalculation
    {
        // размеры:
        [DisplayName("Ширина нижней полки двутавра")]
        public double bf { get; private set; }
        [DisplayName("Высота нижней полки двутавра")]
        public double hf { get; private set; }
        [DisplayName("Высота стенки двутавра")]
        public double hw { get; private set; }
        [DisplayName("Ширина стенки двутавра")]
        public double bw { get; private set; }
        [DisplayName("Ширина верхней полки двутавра")]
        public double b1f { get; private set; }
        [DisplayName("Высота верхней полки двутавра")]
        public double h1f { get; private set; }

        // физ. характеристики бетона
        [DisplayName("Расчетные значения сопротивления на сжатиие по СП63 кг/см2")]
        public new double Rfbn { get; private set; }
        [DisplayName("Значения коэффициента надежности по бетону при сжатии СП63")]
        public double Yb { get; private set; }

        // Результаты
        [DisplayName("Высота сжатой зоны, см")]
        public double x { get; private set; }

        [DisplayName("Предельный момент сечения, т*м")]
        public double Mult { get; private set; }

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

        public override void Calculate()
        {
            //общая высота
            double h = hf + hw + h1f;

            Rfbt3 = (Rfbt3n / Yft) * Yb1 * Yb5;

            double Rfb = Rfbn / Yb * Yb1 * Yb2 * Yb3 * Yb5;

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

    [DisplayName("Расчет прочности изгибаемого элемента прямоугольного сечения")]
    public class BSFibCalc_Rect : BSFiberCalculation
    {
        //размеры, см
        [DisplayName("Высота сечения, см")]
        public double h { get; private set; }
        [DisplayName("Ширина сечения, см")]
        public double b { get; private set; }
        [DisplayName("Упругопластический момент сопротивления")]
        public double Wpl { get; private set; }
        [DisplayName("Предельный момент сечения для изгибаемых сталефибробетонных элементов")]
        public double Mult { get; private set; }

        public override Dictionary<string, double> GeomParams()
        {
            Dictionary<string, double> geom = base.GeomParams();
            geom.Add(DN(typeof(BSFibCalc_Rect), "b"), b);
            geom.Add(DN(typeof(BSFibCalc_Rect), "h"), h);
            return geom;
        }

        public override Dictionary<string, double> Results()
        {
            return new Dictionary<string, double>() {
                    { DN(typeof(BSFibCalc_Rect), "Wpl"), Wpl},
                    { DN(typeof(BSFibCalc_Rect), "Mult"), Mult}
            };
        }

        public override void GetParams(double[] _t)
        {
            base.GetParams(_t);

            (Rfbt3n, Rfbn, Yft, Yb, Yb1, Yb2, Yb3, Yb5) = (_t[0], _t[1], _t[2], _t[3], _t[4], _t[5], _t[6], _t[7]);
        }

        public override void GetSize(double[] _t)
        {
            (b, h) = (_t[0], _t[1]);
        }

        public override void Calculate()
        {
            //Расчетное остаточное остаточного сопротивления осевому растяжению
            double Rfbt3 = (Rfbt3n / Yft) * Yb1 * Yb5;

            //коэффициент, учитывающий неупругие свойства фибробетона растянутой зоны сечения
            double Y = 1.73d - 0.005d * (B - 15);

            //Упругопластический момент сопротивления  Ф.(6.3)
            Wpl = b * h * h / 6 * Y;

            //Значение пердльнолго момента сечения для изгибаемых сталефибробетонных элементов прямоугольного сечения определяют по формуле (6.3) (кг*см)
            Mult = Rfbt3 * Wpl;

            //Предельный момент сечения  (т*м)
            Mult = Math.Round(Mult * 0.00001, 4);
        }
    }
}

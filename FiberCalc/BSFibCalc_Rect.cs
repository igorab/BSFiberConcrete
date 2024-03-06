using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace BSFiberConcrete
{
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
        [DisplayName("Предельный момент сечения для изгибаемых сталефибробетонных элементов, т*м")]
        public double Mult { get; protected set; }

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

        protected double Rfbt_3() => (Yft != 0) ? (Rfbt3n / Yft) * Yb1 * Yb5 : Rfbt3n * Yb1 * Yb5;
       
        public override void Calculate()
        {
            string info;

            //Расчетное остаточное остаточного сопротивления осевому растяжению
            Rfbt3 = Rfbt_3();

            //коэффициент, учитывающий неупругие свойства фибробетона растянутой зоны сечения
            double Y = 1.73d - 0.005d * (B - 15);

            //Упругопластический момент сопротивления  Ф.(6.3)
            Wpl = b * h * h / 6 * Y;

            //Значение пердльнолго момента сечения для изгибаемых сталефибробетонных элементов прямоугольного сечения определяют по формуле (6.3) (кг*см)
            Mult = Rfbt3 * Wpl;

            //Предельный момент сечения  (т*м)
            Mult = Mult * 0.00001;

            info = "Расчет успешно выполнен!";
            Msg.Add(info);
        }
    }
}

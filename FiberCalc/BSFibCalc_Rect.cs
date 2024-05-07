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
        [DisplayName("Предельный момент сечения для изгибаемых сталефибробетонных элементов, кг*см")]
        public double Mult { get; protected set; }

        public override Dictionary<string, double> Coeffs => new Dictionary<string, double>() { { "Yft", Yft }, { "Yb1", Yb1 }, { "Yb5", Yb5 } };

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

            // need refactoring
            ( Yft, Yb, Yb1, Yb2, Yb3, Yb5) = (_t[2], _t[3], _t[4], _t[5], _t[6], _t[7]);
        }

        public override void GetSize(double[] _t)
        {
            (b, h) = (_t[0], _t[1]);
        }
       
        public override bool Validate()
        {
            bool ret = base.Validate();

            if (Rfbt == 0)
            {
                Msg.Add("Требуется задать класса фибробетона на осевое растяжение");
                ret = false;
            }

            if (MatFiber.B < 15)
            {
                Msg.Add("Требуется увеличение класса фибробетона на осевое сжатие");
                ret = false;
            }

            return ret;
        }

        public override void Calculate()
        {            
            if (!Validate()) 
                return;
             
            //коэффициент, учитывающий неупругие свойства фибробетона растянутой зоны сечения
            // Изменение 1 к СП 360
            double cGamma = Gamma(MatFiber.B);

            //Упругопластический момент сопротивления  Ф.(6.3)
            Wpl = BSBeam_Rect.Wx(b, h) * cGamma;

            //Значение предельного момента сечения для изгибаемых сталефибробетонных элементов определяют по формуле (6.3) (кг*см)
            Mult = Rfbt * Wpl;

            InfoCheckM(Mult);

            //Предельный момент сечения  (т*м)
            //Mult = BSHelper.Kgsm2Tm(Mult);
        }
    }
}

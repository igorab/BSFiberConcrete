using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace BSFiberConcrete
{
    [DisplayName("Расчет прочности изгибаемого элемента прямоугольного сечения")]
    public class BSFibCalc_Rect : BSFiberCalculation
    {
                [DisplayName("Высота сечения, [см]")]
        public double h { get; private set; }
        [DisplayName("Ширина сечения, [см]")]
        public double b { get; private set; }
        [DisplayName("Упругопластический момент сопротивления")]
        public double Wpl { get; private set; }
        [DisplayName("Предельный момент сечения для изгибаемых сталефибробетонных элементов, [кг*см]")]
        public double Mult { get; protected set; }

        [DisplayName("Коэффициент, учитывающий неупругие свойства фибробетона растянутой зоны")]
        public double cGamma { get; protected set; }

        [DisplayName("Коэффициент использования по усилию")]
        public double UtilRate { get; protected set; }

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
                    { DN(typeof(BSFibCalc_Rect), "Mult"), Mult},
                    { DN(typeof(BSFibCalc_Rect), "UtilRate"), UtilRate}
            };
        }

        public override Dictionary<string, double> PhysicalParameters()
        {
            Dictionary<string, double> phys = new Dictionary<string, double>
            {
                { DN(typeof(BSFiberCalculation), "Rfbt"), Rfbt },
                { DN(typeof(BSFiberCalculation), "B"), B },
                { DN(typeof(BSFibCalc_Rect), "cGamma"), cGamma }
            };

            return phys;
        }

        public override void SetParams(double[] _t)
        {
            base.SetParams(_t);

                        ( Yft, Yb, Yb1, Yb2, Yb3, Yb5) = (_t[2], _t[3], _t[4], _t[5], _t[6], _t[7]);
        }

        public override void SetSize(double[] _t)
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
                Msg.Add("Требуется увеличение класса бетона-матрицы (менее B15 не используется)");
                ret = false;
            }

            if (MatFiber.B > 60)
            {
                Msg.Add("Для бетона классом более B60 расчет вести на основе нелинейной деформационной модели");

                ret = false;
            }


            return ret;
        }

                                protected void UtilRateCalc()
        {
                        UtilRate = (Mult != 0) ? m_Efforts["My"] / Mult : 0;
        }

        public override bool Calculate()
        {
            if (!Validate())            
                return false;
                        
                        cGamma = Gamma(MatFiber.B);

                        Wpl = BSBeam_Rect.Wx(b, h) * cGamma;

                        Mult = Rfbt * Wpl;
            
            UtilRateCalc();

            InfoCheckM(Mult);
            
            return true;
        }       
    }
}

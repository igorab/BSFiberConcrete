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
       
        [DisplayName("Предельный момент сечения")]
        public double Mult { get; private set; }

        public override void GetParams(double[] _t)
        {
            base.GetParams(_t);

            // need refactoring
            (Yft, Yb, Yb1, Yb2, Yb3, Yb5) = ( _t[2], _t[3], _t[4], _t[5], _t[6], _t[7]);
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
                { DN(typeof(BSFibCalc_Ring), "Rfbt3"), Rfbt3 },
                { DN(typeof(BSFibCalc_Ring), "Mult"), Mult }
            };
        }

        public override bool Validate()
        {
            bool ret = base.Validate();

            if (Rfb == 0 || Rfbt3 == 0)
            {
                Msg.Add("Требуется задать класс фибробетона на осевое сжатие и остаточное растяжение Rfbt3");
                ret = false;
            }

            return ret;
        }


        public override void Calculate()
        {
            if (!Validate())
                return;
            
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

            InfoCheckM(Mult);

            //Предельный момент сечения  (т*м)
            Mult = BSHelper.Kgsm2Tm( Mult );            
        }
    }
}

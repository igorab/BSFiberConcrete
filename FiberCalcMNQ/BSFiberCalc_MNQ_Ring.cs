using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFiberConcrete
{
    public class BSFiberCalc_MNQ_Ring : BSFiberCalc_MNQ
    {
        public BSBeam_Ring beam { get; set; }

        public BSFiberCalc_MNQ_Ring()
        {
            this.beam = new BSBeam_Ring();
            base.m_Beam = this.beam;
        }

        public override void GetSize(double[] _t)
        {
            (r1, r2, l0) = (beam.r1, beam.r2, beam.Length) = (_t[0], _t[1], _t[2]);
            A = beam.Area();
            
            h = beam.h;
            b = beam.b;

            base.m_Beam = this.beam;
            l0 = beam.Length;
            
            I = beam.Jx();
            
            y_t = beam.y_t;
        }

        public override void GetParams(double[] _t)
        {
            base.GetParams(_t);

            e_N = 25;
        }

        private new void Calculate_N()
        {
            double Ar = beam.Area();

            Rfb = Rfbn / Yb * Yb1 * Yb2 * Yb3 * Yb5;

            double Rfbt3 = Rfbt3n / Yft * Yb1 * Yb5;

            // значение относительной площади сжатой зоны сталефибробетона
            double alfa_r = (N + Rfbt3 * Ar) / ((Rfb + 3.35d * Rfbt3) * Ar);

            if (alfa_r < 0.15)
            {
                alfa_r = (N + 0.73 * Rfbt3 * Ar) / ((Rfb + 2 * Rfbt3) * Ar);
            }

            N_ult = Ar * (Rfb * Math.Sin(Math.PI * alfa_r) / Math.PI + Rfbt3 * (1 - 1.35 * alfa_r) * 1.6 * alfa_r) * beam.r_m / e_N;

            N_ult *= 0.001d;
        }


        public override void Calculate()
        {
            if (Fissure)
            {
                Calculate_N_Out();
            }
            else if (Shear)
            {   // Расчет на действие поперечной силы
                CalculateQ();
                // Расчет на действие моментов
                CalculateM();
            }
            else
            {
                Calculate_N();
            }
        }
    }
}

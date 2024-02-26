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
        }

        public override void GetSize(double[] _t)
        {
            (r1, r2) = (beam.r1, beam.r2) = (_t[0], _t[1]);
            A = beam.Area();
        }

        public override void GetParams(double[] _t)
        {
            base.GetParams(_t);

            e_N = 25;
        }


        public override void Calculate()
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
    }
}

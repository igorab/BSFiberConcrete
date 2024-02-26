using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFiberConcrete
{
    public class BSFiberCalc_MNQ_IT : BSFiberCalc_MNQ
    {
        public BSBeam_IT beam { get; set; }

        public BSFiberCalc_MNQ_IT()
        {
            this.beam = new BSBeam_IT();
        }

        public override void Calculate()
        {
            throw new Exception("Расчет не выполнен (нет в СП)");
        }

        public override void GetSize(double[] _t)
        {
            (beam.bf, beam.hf, beam.hw, beam.bw, beam.b1f, beam.h1f) = (_t[0], _t[1], _t[2], _t[3], _t[4], _t[5]);
        }
    }
}

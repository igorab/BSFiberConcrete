using BSFiberConcrete.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFiberConcrete.LocalStrength
{
    public class BSLocalCompressionCalc : BSLocalStrengthCalc
    {
        public override List<LocalStress> DataSource()
        {
            return BSData.LoadLocalStress();
        }

        public override void RunCalc()
        {
            base.RunCalc();
        }
    }
}

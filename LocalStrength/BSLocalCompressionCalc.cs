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
        public override void InitDataSource()
        {
            m_DS = BSData.LoadLocalStress();
        }

        public override string ReportName()
        {
            return "Местное сжатие";
        }

        public override bool RunCalc()
        {
            bool ok = base.RunCalc();

            return ok;
        }

        public override string SampleDescr()
        {
            return "";
        }

        public override string SampleName()
        {
            return "Расчет сталефибробетонных элементов на местное сжатие без арматуры";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFiberConcrete
{
    public class BSFiberReport_N : BSFiberReport_MNQ
    {
        public override void InitFromFiberCalc(BSFiberCalc_MNQ _fiberCalc)
        {
            base.InitFromFiberCalc(_fiberCalc);

            m_Efforts = new Dictionary<string, double>(){ {"N", m_Efforts["N"]}};
            m_CalcResults = _fiberCalc.Results();
        }
    }
}

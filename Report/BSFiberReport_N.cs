using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BSFiberConcrete
{
    [DisplayName("Расчет элементов на действие продольной силы")]    
    public class BSFiberReport_N : BSFiberReport_MNQ
    {
        public BSFiberReport_N()
        {
            ReportName = typeof(BSFiberReport_N).GetCustomAttribute<DisplayNameAttribute>().DisplayName;
        }

        public override void InitFromFiberCalc(BSFiberCalc_MNQ _fiberCalc)
        {
            base.InitFromFiberCalc(_fiberCalc);

            m_Efforts = new Dictionary<string, double>()
            {
                {"My,[кг*см]", m_Efforts["My"]},
                {"N, [кг]", m_Efforts["N"]}
            };
            m_CalcResults = _fiberCalc.Results();
        }
    }
}

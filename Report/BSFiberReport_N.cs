using BSFiberConcrete.BSRFib;
using BSFiberConcrete.Report;
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
            m_CalcResults1Group = _fiberCalc.Results();
        }
                
        public static void RunMultiReport(ref int _iRep, List<BSFiberReport_N> _calcResults)
        {
            foreach (var fiberReport_N in _calcResults)
            {                
                string pathToHtmlFile = fiberReport_N.CreateReport(++_iRep);
                System.Diagnostics.Process.Start(pathToHtmlFile);
            }
        }

        public BSFiberReportData GetBSFiberReportData()
        {
            BSFiberReportData data = new BSFiberReportData();

            data.BeamSection      = m_BeamSection;
            data.UseReinforcement = UseReinforcement;
            data.m_Coeffs         = m_Coeffs;
            data.m_Efforts        = m_Efforts;
            data.m_GeomParams     = m_GeomParams;            
            data.m_Messages       = m_Messages;
            data.m_PhysParams     = m_PhysParams;
            data.UnitConverter    = _unitConverter;
            // результат расчета по первой группе предельных состояний
            data.m_CalcResults1Group = m_CalcResults1Group;
            // результат расчета по второй группе предельных состояний
            data.m_CalcResults2Group = m_CalcResults2Group;
            
            return data;
        }
    }
}

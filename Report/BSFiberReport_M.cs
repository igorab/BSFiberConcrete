using BSFiberConcrete.BSRFib;
using BSFiberConcrete.UnitsOfMeasurement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BSFiberConcrete
{
    public class BSFiberReport_M
    {
        private BeamSection m_BeamSection;
        private IEnumerable<string> m_Msg;
        private BSFiberReportData m_ReportData;

        public BSFiberReportData BSFibCalc {             
            set { m_ReportData = value;
                  m_BeamSection = value.BeamSection;
                  m_Msg = value.m_Messages; } 
        }
        public bool UseRebar { get; private set; }
        public LameUnitConverter UnitConverter { get; set; }

        public static void RunMultiReport(ref int iRep, List<BSFiberReportData> _calcResults)
        {
            foreach (var _FibCalc in _calcResults)
            {
                BSFiberReport_M fiberReport_M = new BSFiberReport_M
                {
                    UnitConverter = _FibCalc.UnitConverter,
                    BSFibCalc = _FibCalc
                };

                fiberReport_M.RunReport(true, _FibCalc.UseReinforcement,  ++iRep);
            }
        }

        public void RunReport(bool calcOk, bool _useRebar, int _irep = 1)
        {
            try
            {
                if (m_ReportData == null)
                    throw new Exception("Не выполнен расчет");

                if (calcOk)
                {
                    string pathToHtmlFile = CreateReport(_irep, m_BeamSection, _useReinforcement: _useRebar);

                    System.Diagnostics.Process.Start(pathToHtmlFile);
                }
                else
                {
                    string errMsg = "";
                    foreach (string ms in m_Msg) errMsg += ms + ";\t\n";

                    MessageBox.Show(errMsg);
                }
            }
            catch (Exception _e)
            {
                MessageBox.Show("Ошибка в отчете " + _e.Message);
            }
        }

        private void InitReportSections(ref BSFiberReport report)
        {           
            report.Beam              = null;
            report.Coeffs            = m_ReportData.m_Coeffs; 
            report.Efforts           = m_ReportData.m_Efforts; 
            report.GeomParams        = m_ReportData.m_GeomParams; 
            report.PhysParams        = m_ReportData.m_PhysParams;
            report.Reinforcement     = m_ReportData.m_Reinforcement;
            report.CalcResults       = m_ReportData.m_CalcResults;
            report.CalcResults2Group = m_ReportData.m_CalcResults2Group;
            report.ImageStream       = m_ReportData.ImageStream;
            report.Messages          = m_ReportData.m_Messages;
            report.Path2BeamDiagrams = null;
            report._unitConverter    = m_ReportData.UnitConverter;
        }

        /// <summary>
        ///  Сформировать отчет
        /// </summary>
        /// <param name="_reportName">Заголовок</param>
        /// <param name="_useReinforcement">Используется ли арматура</param>
        /// <returns>Путь к файлу отчета</returns>
        private string CreateReport(int _fileId,
                                    BeamSection _BeamSection,
                                    string _reportName = "",
                                    bool _useReinforcement = false)
        {
            try
            {
                string path = "";
                BSFiberReport report = new BSFiberReport();

                if (_reportName != "")
                    report.ReportName = _reportName;

                report.BeamSection = _BeamSection;
                report.UseReinforcement = _useReinforcement;

                InitReportSections(ref report);

                path = report.CreateReport(_fileId);
                return path;
            }
            catch (Exception _e)
            {
                throw _e;
            }
        }
    }
}

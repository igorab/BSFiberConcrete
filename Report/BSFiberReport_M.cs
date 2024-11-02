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
        private BSFiberCalculation m_FibCalc;

        public BSFiberCalculation BSFibCalc {             
            set { m_FibCalc = value;
                  m_BeamSection = value.BeamSectionType();
                  m_Msg = value.Msg; } 
        }
        public bool UseRebar { get; private set; }
        public LameUnitConverter UnitConverter { get; set; }

        public static void RunMultiReport(ref int iRep, List<BSFiberCalculation> _calcResults, LameUnitConverter _UnitConverter)
        {
            foreach (BSFiberCalculation _FibCalc in _calcResults)
            {
                BSFiberReport_M fiberReport_M = new BSFiberReport_M
                {
                    UnitConverter = _UnitConverter,
                    BSFibCalc = _FibCalc
                };

                fiberReport_M.RunReport(true, ++iRep);
            }
        }

        public void RunReport(bool calcOk, int _irep = 1)
        {
            try
            {
                if (m_FibCalc == null)
                    throw new Exception("Не выполнен расчет");

                if (calcOk)
                {
                    string pathToHtmlFile = CreateReport(_irep, m_BeamSection, _useReinforcement: UseRebar);

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
            report.Beam       = null;
            report.Coeffs     = m_FibCalc.Coeffs; 
            report.Efforts    = m_FibCalc.Efforts; 
            report.GeomParams = m_FibCalc.GeomParams(); 
            report.PhysParams = m_FibCalc.PhysicalParameters();
            report.Reinforcement     = null;
            report.CalcResults       = m_FibCalc.Results();
            report.CalcResults2Group = null;
            report.ImageStream       = null;
            report.Messages          = m_FibCalc.Msg;
            report.Path2BeamDiagrams = null;
            report._unitConverter    = UnitConverter;
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

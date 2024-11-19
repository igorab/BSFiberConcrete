using BSFiberConcrete.Report;
using BSFiberConcrete.UnitsOfMeasurement;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
                    BSFibCalc     = _FibCalc
                };

                fiberReport_M.RunReport(true, _FibCalc.UseReinforcement,  ++iRep);
            }
        }

        public void RunReport(bool calcOk, bool _useRebar, int _fileIdx = 1)
        {
            try
            {
                if (m_ReportData == null)
                    throw new Exception("Не выполнен расчет");

                string pathToHtmlFile = "";
                string _reportName = "";

                if (calcOk)
                {
                    //string pathToHtmlFile = CreateReport(_irep, m_BeamSection, _useReinforcement: _useRebar);                    
                    try
                    {
                        BSFiberReport report = new BSFiberReport();

                        if (_reportName != "")
                            report.ReportName = _reportName;

                        report.BeamSection = m_BeamSection;
                        report.UseReinforcement = _useRebar;

                        InitReportSections(ref report);

                        //path = report.CreateReport(_fileId);
                        
                        string filename = "FiberCalculationReport{0}.htm";
                        try
                        {
                            filename = (_fileIdx == 0) ? string.Format(filename, "") : string.Format(filename, _fileIdx);

                            using (FileStream fs = new FileStream(filename, FileMode.Create))
                            {
                                using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                                {
                                    report.Header(w);

                                    report.ReportBody(w);

                                    report.ReportEfforts(w);

                                    report.ReportResult(w);

                                    report.Footer(w);
                                }

                                pathToHtmlFile = fs.Name;
                            }
                        }
                        catch (Exception _e)
                        {
                            MessageBox.Show("Ошибка при формировании отчета: " + _e.Message);
                            pathToHtmlFile = "";
                        }                        
                    }
                    catch (Exception _e)
                    {
                        throw _e;
                    }

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
            report.CalcResults1Group = m_ReportData.m_CalcResults1Group;
            report.CalcResults2Group = m_ReportData.m_CalcResults2Group;
            report.ImageStream       = m_ReportData.ImageStream;
            report.Messages          = m_ReportData.m_Messages;
            report._unitConverter    = m_ReportData.UnitConverter;
        }

        /// <summary>
        ///  Сформировать отчет
        /// </summary>
        /// <param name="_reportName">Заголовок</param>
        /// <param name="_useReinforcement">Используется ли арматура</param>
        /// <returns>Путь к файлу отчета</returns>
        private string CreateReport(int _fileIdx,
                                    BeamSection _BeamSection,
                                    string _reportName = "",
                                    bool _useReinforcement = false)
        {
            try
            {                
                BSFiberReport report = new BSFiberReport();

                if (_reportName != "")
                    report.ReportName = _reportName;

                report.BeamSection = _BeamSection;
                report.UseReinforcement = _useReinforcement;

                InitReportSections(ref report);

                //path = report.CreateReport(_fileId);

                string pathToHtmlFile = "";
                string filename = "FiberCalculationReport{0}.htm";
                try
                {
                    filename = (_fileIdx == 0) ? string.Format(filename, "") : string.Format(filename, _fileIdx);

                    using (FileStream fs = new FileStream(filename, FileMode.Create))
                    {
                        using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                        {
                            report.Header(w);

                            report.ReportBody(w);

                            report.ReportEfforts(w);

                            report.ReportResult(w);

                            report.Footer(w);
                        }

                        pathToHtmlFile = fs.Name;
                    }
                }
                catch (Exception _e)
                {
                    MessageBox.Show("Ошибка при формировании отчета: " + _e.Message);
                    pathToHtmlFile = "";
                }

                return pathToHtmlFile;                
            }
            catch (Exception _e)
            {
                throw _e;
            }
        }      
    }
}

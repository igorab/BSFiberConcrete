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
        public BeamSection BeamSection;
        public IEnumerable<string> Msg;

        public BSFiberCalculation BSFibCalc { get; set; }
        public bool UseRebar { get; private set; }
        public LameUnitConverter UnitConverter { get; set; }

        public void RunReport(bool calcOk, int _irep = 1)
        {
            try
            {
                if (BSFibCalc == null)
                    throw new Exception("Не выполнен расчет");

                if (calcOk)
                {
                    string pathToHtmlFile = CreateReport(_irep, BeamSection, _useReinforcement: UseRebar);

                    System.Diagnostics.Process.Start(pathToHtmlFile);
                }
                else
                {
                    string errMsg = "";
                    foreach (string ms in Msg) errMsg += ms + ";\t\n";

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
            report.Beam = null;
            report.Coeffs = BSFibCalc.Coeffs; 
            report.Efforts = BSFibCalc.Efforts; 
            report.GeomParams = BSFibCalc.GeomParams(); 
            report.PhysParams = BSFibCalc.PhysicalParameters();
            report.Reinforcement = null;
            report.CalcResults = BSFibCalc.Results();
            report.CalcResults2Group = null;
            report.ImageStream = null;
            report.Messages = BSFibCalc.Msg;
            report.Path2BeamDiagrams = null;
            report._unitConverter = UnitConverter;
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

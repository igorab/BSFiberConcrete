using BSFiberConcrete.UnitsOfMeasurement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BSFiberConcrete.Report
{
    public class BSReport
    {
        private BeamSection m_BeamSection;

        private Dictionary<string, double> m_Beam;
        
        private LameUnitConverter _UnitConverter;

        public BSCalcResultNDM CalcRes { get; set; }

        public static void RunFromCode(BeamSection m_BeamSection, BSCalcResultNDM calcRes)
        {
            BSReport bSReport = new BSReport(m_BeamSection);
            bSReport.CalcRes = calcRes;
            bSReport.CreateReportNDM();
        }

        public BSReport(BeamSection _beamSection)
        {
            m_BeamSection = _beamSection;
            m_Beam = new Dictionary<string, double>();            
        }
        
        private void InitReportSections(ref BSFiberReport report)
        {
            if (CalcRes == null) return;

            report.Beam = CalcRes.Beam;
            report.Coeffs = CalcRes.Coeffs;
            report.Efforts = CalcRes.Efforts;
            report.GeomParams = CalcRes.GeomParams;
            report.PhysParams = CalcRes.PhysParams; //m_PhysParams;
            report.Reinforcement = CalcRes.Reinforcement;
            report.CalcResults = CalcRes.GetResults1Group();
            report.CalcResults2Group = CalcRes.GetResults2Group();
            report.ImageStream = CalcRes.ImageStream;
            report.Messages = CalcRes.Message;
            report.Path2BeamDiagrams = CalcRes.Path2BeamDiagrams;
            report._unitConverter = CalcRes.UnitConverter;
        }

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

        [DisplayName("Расчет по прочности нормальных сечений на основе нелинейной деформационной модели")]
        public void CreateReportNDM()
        {
            try
            {                
                string reportName = "";
                try
                {
                    MethodBase method = MethodBase.GetCurrentMethod();
                    DisplayNameAttribute attr = (DisplayNameAttribute)method.GetCustomAttributes(typeof(DisplayNameAttribute), true)[0];
                    reportName = attr.DisplayName;
                }
                catch
                {
                    MessageBox.Show("Не задан атрибут DisplayName метода");
                }

                string pathToHtmlFile = CreateReport(1, m_BeamSection, reportName);
                
                System.Diagnostics.Process.Start(pathToHtmlFile);
            }
            catch (Exception _e)
            {
                MessageBox.Show("Ошибка в отчете " + _e.Message);
            }
        }
    }
}

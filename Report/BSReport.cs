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

        private Dictionary<string, double> m_Coeffs;

        public BSCalcResultNDM CalcRes { get; set; }

        public BSReport(BeamSection _beamSection)
        {
            m_BeamSection = _beamSection;
            m_Beam = new Dictionary<string, double>();
            m_Coeffs = new Dictionary<string, double>();
        }
        
        private void InitReportSections(ref BSFiberReport report)
        {            
            report.Beam = CalcRes.Beam;
            report.Coeffs =  m_Coeffs;
            report.Efforts = CalcRes.Efforts;
            report.GeomParams = CalcRes.GeomParams;
            report.PhysParams = CalcRes.PhysParams; //m_PhysParams;
            report.Reinforcement = CalcRes.Reinforcement;
            report.CalcResults = CalcRes.GetResults1Group();
            report.CalcResults2Group = CalcRes.GetResults2Group();
            //report.ImageStream = m_ImageStream;
            //report.Messages = m_Message;
            //report.Path2BeamDiagrams = m_Path2BeamDiagrams;
            //report._unitConverter = _UnitConverter;
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

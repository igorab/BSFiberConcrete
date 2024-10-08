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
        BeamSection m_BeamSection;
        Dictionary<string, double> m_Beam;
        Dictionary<string, double> m_Coeffs;

        public BSCalcResultNDM calcRes;

        private double InitBeamLength(double _lgth, double _coeflgth)
        {                        
            m_Beam.Clear();
            m_Beam.Add("Длина элемента, см", _lgth);
            m_Beam.Add("Коэффициет расчетной длины", _coeflgth);
            
            return (_coeflgth != 0) ? _lgth * _coeflgth : _lgth;
        }

        private void InitReportSections(ref BSFiberReport report)
        {            
            report.Beam = m_Beam;
            report.Coeffs =  m_Coeffs;
            report.Efforts = calcRes.Efforts;
            report.GeomParams = calcRes.GeomParams;
            report.PhysParams = calcRes.PhysParams; //m_PhysParams;
            report.Reinforcement = calcRes.Reinforcement;
            report.CalcResults = calcRes.GetResults1Group();
            report.CalcResults2Group = calcRes.GetResults2Group();
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
                InitBeamLength(0, 0);

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

                BSFiberReport report = new BSFiberReport();
                report.ReportName = reportName;
                report.BeamSection = m_BeamSection;
                report.UseReinforcement = true;

                InitReportSections(ref report);

                pathToHtmlFile = report.CreateReport(1);


                System.Diagnostics.Process.Start(pathToHtmlFile);
            }
            catch (Exception _e)
            {
                MessageBox.Show("Ошибка в отчете " + _e.Message);
            }
        }


    }
}

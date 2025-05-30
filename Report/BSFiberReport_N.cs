﻿using BSFiberConcrete.BSRFib;
using BSFiberConcrete.Report;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace BSFiberConcrete
{
    [DisplayName("Расчет элементов на действие продольной силы")]    
    public class BSFiberReport_N : BSFiberReport_MNQ
    {
        private BSFiberReportData m_ReportData;
        
        private bool UseRebar { get; set; }

        private List<BSFiberReport_N> ListFiberReportData;

        public BSFiberReport_N()
        {
            ReportName = typeof(BSFiberReport_N).GetCustomAttribute<DisplayNameAttribute>().DisplayName;
        }

        public override void InitFromFiberCalc(BSFiberCalc_MNQ _fiberCalc)
        {
            base.InitFromFiberCalc(_fiberCalc);
           
            m_CalcResults1Group = _fiberCalc.Results();
        }
                

        private void InitReportSections(ref BSFiberReport report)
        {
            report.Beam = null;
            report.Coeffs = m_ReportData.m_Coeffs;
            report.Efforts = m_ReportData.m_Efforts;
            report.GeomParams = m_ReportData.m_GeomParams;
            report.PhysParams = m_ReportData.m_PhysParams;
            report.Reinforcement = m_ReportData.m_Reinforcement;
            report.CalcResults1Group = m_ReportData.m_CalcResults1Group;
            report.CalcResults2Group = m_ReportData.m_CalcResults2Group;
            report.ImageStream = m_ReportData.ImageStream;
            report.Messages = m_ReportData.m_Messages;
            report._unitConverter = m_ReportData.UnitConverter;
            report.UseReinforcement = m_ReportData.UseReinforcement;
        }


        /// <summary>
        /// сформировать отчет по различным загружениям
        /// </summary>
        public void CreateMultiReport()
        {
            try
            {
                if (m_ReportData == null)
                    throw new Exception("Не выполнен расчет");

                string pathToHtmlFile = "";
                string _reportName = "";
                int fileIdx = 0;

                BSFiberReport report = new BSFiberReport();

                if (_reportName != "")
                    report.ReportName = _reportName;
                report.BeamSection = m_BeamSection;

                var data = ListFiberReportData[0];

                InitReportSections(ref report);

                string filename = "FiberCalculationReport{0}.htm";
                try
                {
                    filename = (fileIdx == 0) ? string.Format(filename, "") : string.Format(filename, fileIdx);

                    using (FileStream fs = new FileStream(filename, FileMode.Create))
                    {
                        using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                        {
                            report.Header(w);

                            report.ReportBody(w);

                            foreach (var fiberReport in ListFiberReportData)
                            {
                                report.InitFromBSFiberReportData(fiberReport);
                                report.ReportEfforts(w);
                                report.ReportResult(w);
                            }

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

                System.Diagnostics.Process.Start(pathToHtmlFile);

                //
                //    string errMsg = "";
                //    foreach (string ms in m_Msg) errMsg += ms + ";\t\n";

                //    MessageBox.Show(errMsg);
                //}
            }
            catch (Exception _e)
            {
                MessageBox.Show("Ошибка в отчете " + _e.Message);
            }
        }       
    }
}

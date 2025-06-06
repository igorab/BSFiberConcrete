﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using System.Windows.Forms;
using System.Drawing;
using BSFiberConcrete.Lib;

namespace BSFiberConcrete.LocalStrength
{
    public class BSLocalStrengthReport
    {
        public BSLocalStrengthReport() { }

        public List<LocalStress> DataSource { get; internal set; }
        public string ReportName {private get; set; }
        public string SampleDescr {private get; set; }
        public string SampleName {private get; set; }
        public Dictionary<string, double> CalcResults { get; private set; }
        public Image ImageScheme { get; internal set; }

        public MemoryStream ImageStream { get; internal set; }

        public void RunReport()
        {
            string pathToHtmlFile =  CreateReport(0);

            System.Diagnostics.Process.Start(pathToHtmlFile);
        }

        public string CreateReport(int _fileIdx = 0)
        {
            string pathToHtmlFile = "";
            string filename = "FiberLabReport{0}.htm";
            try
            {
                filename = (_fileIdx == 0) ? string.Format(filename, "") : string.Format(filename, _fileIdx);

                using (FileStream fs = new FileStream(filename, FileMode.Create))
                {
                    using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                    {
                        Header(w);
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


        protected virtual void Header(StreamWriter w)
        {
            w.WriteLine("<html>");
            w.WriteLine("<head>");
            w.WriteLine($"<H1>{ReportName}</H1>");
            w.WriteLine($"<H4>Расчет выполнен по {BSData.ProgConfig.NormDoc}</H4>");
            w.WriteLine($"<H2>{SampleDescr}</H2>");
            w.WriteLine($"<H3>{SampleName}</H3>");

            w.WriteLine(@"<style>
               td{                    
                    height: 60px;
                    border: solid 1px silver;
                    text-align: center;
                }
                td.id 
                {
                    width: 20px;
                }
                td.descr 
                {
                    text-align: left
                }
                </style>");

            w.WriteLine("</head>");
            w.WriteLine("<body>");
                       
            if (ImageScheme != null)
            {
                string img = BSHelper.MakeImageSrcData(ImageScheme, "ImageScheme.png");
                w.WriteLine($"<table><tr><td> <img src={img}/ width=\"500\" height=\"500\"> </td></tr> </table>");
            }

            if (CalcResults != null)
            {
                w.WriteLine("<Table border=1 bordercolor = darkblue>");
                w.WriteLine("<caption>Расчеты: </caption>");

                foreach (var _pair in CalcResults)
                {
                    w.WriteLine("<tr>");
                    w.WriteLine($"<td><b>{_pair.Key}</b></td>");
                    w.WriteLine($"<td> {Math.Round(_pair.Value, 4)}</td>");
                    w.WriteLine("</tr>");
                }

                w.WriteLine("</tr>");
                w.WriteLine("</Table>");
                w.WriteLine("<br>");
            }
                        
            if (DataSource != null)
            {
                w.WriteLine("<Table border=1 bordercolor = darkblue>");
                w.WriteLine("<tr><td class=\"id\"><b>Id</b></td><td>Описание</td><td>Параметр</td><td>Значение</td></tr>");

                foreach (var item in DataSource)
                {
                    w.WriteLine("<tr>");
                    w.WriteLine($"<td class=\"id\"><b>{item.Id}</b></td>");                    
                    w.WriteLine($"<td class = \"descr\">{item.VarDescr}:</td>");
                    w.WriteLine($"<td>{item.VarName}</td>");
                    w.WriteLine($"<td>{Math.Round(item.Value, 4)} </td>");                    
                    w.WriteLine("</tr>");
                }
                w.WriteLine("</Table>");
                w.WriteLine("<br>");
            }

            w.WriteLine("</body>");
            w.WriteLine("</html>");
        }

    }
}

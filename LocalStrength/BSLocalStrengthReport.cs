using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using System.Windows.Forms;

namespace BSFiberConcrete.LocalStrength
{
    public class BSLocalStrengthReport
    {
        public BSLocalStrengthReport() { }

        public List<LocalStress> DataSource { get; internal set; }
        public object ReportName { get; private set; }
        public object SampleDescr { get; private set; }
        public object SampleName { get; private set; }
        public Dictionary<string, double> CalcResults { get; private set; }

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
            w.WriteLine("<H4>Расчет выполнен по СП 360.1325800.2017</H4>");
            w.WriteLine($"<H2>{SampleDescr}</H2>");
            w.WriteLine($"<H3>{SampleName}</H3>");

            w.WriteLine(@"<style>
               td{
                    width: 80px;
                    height: 60px;
                    border: solid 1px silver;
                    text-align: center;
                }
                </style>");

            w.WriteLine("</head>");
            w.WriteLine("<body>");

            if (CalcResults != null)
            {
                w.WriteLine("<Table border=1 bordercolor = darkblue>");
                w.WriteLine("<caption>Расчеты: </caption>");

                foreach (var _pair in CalcResults)
                {
                    w.WriteLine("<tr>");
                    w.WriteLine($"<td><b>{_pair.Key}</b></td>");
                    w.WriteLine($"<td> {_pair.Value}</td>");
                    w.WriteLine("</tr>");
                }

                w.WriteLine("</tr>");
                w.WriteLine("</Table>");
                w.WriteLine("<br>");
            }
                        
            if (DataSource != null)
            {
                w.WriteLine("<Table border=1 bordercolor = darkblue>");
                w.WriteLine("<tr><td width = \"200\"'><b>Id</b></td><td>Описание</td><td>Параметр</td><td>Значение</td></tr>");

                foreach (var item in DataSource)
                {
                    w.WriteLine("<tr>");
                    w.WriteLine($"<td width = \"200\"><b>{item.Id}</b></td>");                    
                    w.WriteLine($"<td>{item.VarDescr}</td>");
                    w.WriteLine($"<td>{item.VarName}</td>");
                    w.WriteLine($"<td>{item.Value} </td>");                    
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

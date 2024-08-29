using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace BSFiberConcrete.BSRFib
{
    public class BSRFibLabReport
    {
        public MemoryStream ChartImage { get; set; }
        
        public Dictionary<string, double> LabResults { get; set; }

        public Dictionary<string, string> LabItems { get; set; }

        public string FileChart { get; }

        public object ReportName { get; set; }

        public object SampleName { get; set; }

        public object SampleDescr { get; set; }


        public List<FaF> ChartData { get; internal set; }

        public List<Deflection_f_aF> D_f_aF { get; internal set; }
        public List<FibLab> FibLab { get; internal set; }


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

        public void RunReport()
        {
            //MakeImageSrcData(FileChart);

            string pathToHtmlFile = CreateReport(0);

            System.Diagnostics.Process.Start(pathToHtmlFile);
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
                    border: solid 1px silver;
                    text-align: left;
                }
                </style>");

            //w.WriteLine(@"<style>
            //   td{
            //        border: solid 1px silver;
            //        text-align: center;
            //    }
            //    </style>");

            w.WriteLine("</head>");
            w.WriteLine("<body>");

            if (LabResults != null)
            {
                w.WriteLine("<Table border=1 bordercolor = darkblue>");
                w.WriteLine("<caption>Расчеты: </caption>");

                foreach (var _pair in LabResults)
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

            if (LabItems != null)
            {
                w.WriteLine("<Table border=1 bordercolor = darkblue>");
                w.WriteLine("<caption>Расчеты: </caption>");

                foreach (var _pair in LabItems)
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

            if (ChartImage != null)
            {                
                string img = MakeImageSrcData(ChartImage);
                w.WriteLine($"<table><tr><td> <img src={img}/> </td></tr> </table>");
            }

            if (ChartData != null)
            {
                w.WriteLine("<Table border=1 bordercolor = darkblue>");
                w.WriteLine("<tr><td><b>F</b></td><td><b>a<sub>F</sub></b></td></tr>");
                foreach (FaF pair in ChartData)
                {
                    w.WriteLine("<tr>");
                    w.WriteLine($"<td><b>{pair.F }</b></td>");
                    w.WriteLine($"<td colspan=2> {pair.aF} </td>");
                    w.WriteLine("</tr>");
                }
                w.WriteLine("</Table>");
                w.WriteLine("<br>");                
            }


            if (D_f_aF != null)
            {
                w.WriteLine("<Table border=1 bordercolor = darkblue>");
                w.WriteLine("<tr><td>Образец</td><td>№</td> <td><b>f</b></td><td><b>a<sub>F</sub></b></td></tr>");
                foreach (var pair in D_f_aF)
                {
                    w.WriteLine("<tr>");
                    w.WriteLine($"<td><b>{pair.Id}</b></td>");
                    w.WriteLine($"<td>{pair.Num}</td>");
                    w.WriteLine($"<td><b>{pair.f}</b></td>");
                    w.WriteLine($"<td colspan=2> {pair.aF} </td>");
                    w.WriteLine("</tr>");
                }
                w.WriteLine("</Table>");
                w.WriteLine("<br>");
            }

            if (FibLab != null)
            {
                w.WriteLine("<Table border=1 bordercolor = darkblue>");
                w.WriteLine("<tr><td width = \"200\"'><b>Id</b></td><td>L</td><td>B</td><td>H sp</td><td>F el</td><td>F 0.5</td><td>F 2.5</td></tr>");

                foreach (FibLab item in FibLab)
                {
                    w.WriteLine("<tr>");
                    w.WriteLine($"<td width = \"200\"><b>{item.Id}</b></td>");
                    w.WriteLine($"<td>{item.L}</td>");
                    w.WriteLine($"<td>{item.B}</td>");
                    w.WriteLine($"<td>{item.H_sp}</td>");
                    w.WriteLine($"<td>{item.Fel} </td>");
                    w.WriteLine($"<td>{item.F05} </td>");
                    w.WriteLine($"<td>{item.F25} </td>");
                    w.WriteLine("</tr>");
                }
                w.WriteLine("</Table>");
                w.WriteLine("<br>");
            }

            w.WriteLine("</body>"); 
            w.WriteLine("</html>");            
        }

        private string MakeImageSrcData(MemoryStream _img)
        {
            if (_img == null)  return "";
            string _filename = "";
            string simg = "";
            
            try
            {
                using (Image img = Image.FromStream(_img))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        img.Save(ms, ImageFormat.Png);

                        byte[] imgBytes = ms.ToArray();
                        string _extension = ".png";

                        simg = String.Format("\"data:image/{0};base64, {1}\" alt = \"{2}\" ", _extension, Convert.ToBase64String(imgBytes), _filename);
                    }
                }
            }
            catch (Exception _e)
            {
                simg = _e.Message;
            }

            return simg;
        }

    }
}

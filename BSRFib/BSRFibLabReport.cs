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

        public string FileChart { get; }

        public object ReportName { get; set; }

        public object SampleName { get; set; }

        public object SampleDescr { get; set; }


        public List<FaF> ChartData { get; internal set; }

        public List<Deflection_f_aF> D_f_aF { get; internal set; }

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

            w.WriteLine( @"<style>
               td{
                    width: 60px;
                    height: 60px;
                    border: solid 1px silver;
                    text-align: center;
                }
                </style>");

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

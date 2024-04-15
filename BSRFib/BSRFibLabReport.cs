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

        private string ImageCalc;

        public Dictionary<string, object> m_GeomParams { get; private set; }

        public string FileChart { get; }
        public object ReportName { get; set; }
        public Enum m_BeamSection { get; private set; }
        public Dictionary<string, object> m_Beam { get; private set; }


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
            w.WriteLine($"<H1>{ReportName}</H1>");
            w.WriteLine("<H4>Расчет выполнен по СП 360.1325800.2017</H4>");

            string beamDescr = typeof(BeamSection).GetCustomAttribute<DescriptionAttribute>(true).Description;
            //string beamSection = BSHelper.EnumDescription(m_BeamSection);
            //w.WriteLine($"<H2>{beamDescr}: {beamSection}</H2>");
            
            if (ChartImage != null)
            {                
                string img = MakeImageSrcData(ChartImage);
                w.WriteLine($"<table><tr><td> <img src={img}/> </td></tr> </table>");
            }

            if (m_Beam != null)
            {
                w.WriteLine("<Table border=1 bordercolor = darkblue>");
                foreach (var _pair in m_Beam)
                {
                    w.WriteLine("<tr>");
                    w.WriteLine($"<td><b>{_pair.Key}</b></td>");
                    w.WriteLine($"<td colspan=2>| {_pair.Value} </td>");
                    w.WriteLine("</tr>");
                }
                w.WriteLine("</Table>");
                w.WriteLine("<br>");

                if (m_GeomParams != null)
                {
                    w.WriteLine("<Table border=1 bordercolor = darkblue>");
                    w.WriteLine("<caption>Геометрия</caption>");

                    foreach (var _pair in m_GeomParams)
                    {
                        w.WriteLine("<tr>");
                        w.WriteLine($"<td><b>{_pair.Key}</b></td>");
                        w.WriteLine($"<td>| {_pair.Value}</td>");
                        w.WriteLine("</tr>");
                    }

                    w.WriteLine("</tr>");
                    w.WriteLine("</Table>");
                    w.WriteLine("<br>");
                }
            }
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

using System;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace BSFiberConcrete
{    
    /// <summary>
    /// Построитель отчета
    /// </summary>
    public class BSFiberReport
    {
        public string ReportName { get; set; }
        public Dictionary<string, double> Beam {set{m_Beam = value;}}
        public Dictionary<string, double> Coeffs {set { m_Coeffs = value; } }
        public Dictionary<string, double> PhysParams {set { m_PhysParams = value; } }
        public Dictionary<string, double> GeomParams {set { m_GeomParams = value; } }
        public Dictionary<string, double> CalcResults {set { m_CalcResults = value; } }
        public BeamSection BeamSection { set { m_BeamSection = value; } }


        protected Dictionary<string, double> m_Beam;
        protected Dictionary<string, double> m_Coeffs;
        protected Dictionary<string, double> m_PhysParams;
        protected Dictionary<string, double> m_GeomParams;
        protected Dictionary<string, double> m_CalcResults;
        protected BeamSection m_BeamSection;

        public BSFiberReport()
        {
            ReportName = "Сопротивление сечения из фибробетона";
        }

        protected virtual void Header(StreamWriter w)
        {
            w.WriteLine("<html>");
            w.WriteLine($"<H1>{ReportName}</H1>");
            w.WriteLine("<H4>Расчет выполнен по СП 360.1325800.2017</H4>");

            string beamDescr = typeof(BeamSection).GetCustomAttribute<DescriptionAttribute > (true).Description;
            string beamSection = BSHelper.EnumDescription(m_BeamSection);
            w.WriteLine($"<H2>{beamDescr}: {beamSection}</H2>");

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

        protected virtual void ReportBody(StreamWriter w)
        {            
            if (m_PhysParams != null)
            {
                w.WriteLine("<Table border=1 bordercolor = darkblue>");
                w.WriteLine("<caption>Физические характеристики</caption>");
                foreach (var _pair in m_PhysParams)
                {
                    w.WriteLine("<tr>");
                    w.WriteLine($"<td>{_pair.Key}</td>");
                    w.WriteLine($"<td>| {_pair.Value} </td>");
                    w.WriteLine("</tr>");
                }
               
                w.WriteLine("</Table>");
                w.WriteLine("<br>");
            }

            if (m_Coeffs != null)
            {
                w.WriteLine("<Table border=1 bordercolor = darkblue>");
                w.WriteLine("<caption>Коэффициенты</caption>");
                foreach (var _pair in m_Coeffs)
                {
                    w.WriteLine("<tr>");
                    w.WriteLine($"<td>{_pair.Key}</td>");
                    w.WriteLine($"<td>| {_pair.Value} </td>");
                    w.WriteLine("</tr>");
                }                                
                w.WriteLine("</Table>");
                w.WriteLine("<br>");
            }
            
            
        }

        protected virtual void ReportResult(StreamWriter w)
        {
            w.WriteLine("<H3>Расчет:</H3>");
            if (m_CalcResults != null)
            {
                w.WriteLine("<Table border=1 bordercolor = darkblue>");
                w.WriteLine("<tr>");

                foreach (var _pair in m_CalcResults)
                {
                    w.WriteLine("<tr>");
                    w.WriteLine($"<td><b>{_pair.Key}</b></td>");
                    w.WriteLine($"<td colspan=2>| {_pair.Value} </td>");
                    w.WriteLine("</tr>");
                }
                
                w.WriteLine("</tr>");
                w.WriteLine("</Table>");
                w.WriteLine("<br>");
            }
            else
            {
                w.WriteLine("<th>Расчет не выполнен</th>");
            }
        }
        private string MakeImageSrcData(string filename)
        {
            if (filename == "") return "";

            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            byte[] filebytes = new byte[fs.Length];
            fs.Read(filebytes, 0, Convert.ToInt32(fs.Length));
            return "data:image/png;base64," + Convert.ToBase64String(filebytes, Base64FormattingOptions.None);
        }

        protected virtual void Footer(StreamWriter w)
        {
            string path = ""; // global::BSFiberConcrete.Properties.Resources.FiberBeton;
            //w.WriteLine($"<img src={MakeImageSrcData(path)}/>");

            w.WriteLine("</html>");            
        }

        public string CreateReport()
        {
            string pathToHtmlFile = "";

            try
            {
                using (FileStream fs = new FileStream("test.htm", FileMode.Create))
                {
                    using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                    {
                        Header(w);

                        ReportBody(w);

                        ReportResult(w);

                        Footer(w);
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
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFiberConcrete
{
    public class BSFiberReport
    {
        public Dictionary<string, double> m_Beam{ get; set; }
        public Dictionary<string, double> m_Coeffs { get; set; }
        public Dictionary<string, double> m_PhysParams { get; set; }
        public Dictionary<string, double> m_GeomParams { get; set; }
        public Dictionary<string, double> m_CalcResults { get; set; }

        public BeamSection m_BeamSection { get; set; }

        public string CreateReport()
        {
            string pathToHtmlFile = "";

            using (FileStream fs = new FileStream("test.htm", FileMode.Create))
            {
                using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                {
                    w.WriteLine("<html>");
                    w.WriteLine("<H1>Сопротивление сечения из фибробетона</H1>");
                    w.WriteLine("<H4>Расчет выполнен по СП 360.1325800.2017</H4>");


                    if (m_Beam != null)
                    {
                        w.WriteLine("<Table border=1 bordercolor = darkblue>");                        
                        foreach (var _prm in m_Beam.Keys)
                            w.WriteLine($"<th>{_prm}</th>");
                        w.WriteLine("<tr>");
                        foreach (var _val in m_Beam.Values)
                            w.WriteLine($"<td>{_val}</td>");
                        w.WriteLine("</tr>");
                        w.WriteLine("</Table>");
                    }


                    if (m_PhysParams != null)
                    {
                        w.WriteLine("<Table border=1 bordercolor = darkblue>");
                        w.WriteLine("<caption>Физические характеристики</caption>");
                        foreach (var _prm in m_PhysParams.Keys)
                            w.WriteLine($"<th>{_prm}</th>");
                        w.WriteLine("<tr>");
                        foreach (var _val in m_PhysParams.Values)
                            w.WriteLine($"<td>{_val}</td>");
                        w.WriteLine("</tr>");
                        w.WriteLine("</Table>");
                    }

                    if (m_Coeffs != null)
                    {
                        w.WriteLine("<Table border=1 bordercolor = darkblue>");
                        w.WriteLine("<caption>Коэффициенты</caption>");
                        foreach (var _prm in m_Coeffs.Keys)
                            w.WriteLine($"<th>{_prm}</th>");
                        w.WriteLine("<tr>");
                        foreach (var _val in m_Coeffs.Values)
                            w.WriteLine($"<td>{_val}</td>");
                        w.WriteLine("</tr>");
                        w.WriteLine("</Table>");
                    }

                    w.WriteLine($"<H2>Балка {m_BeamSection}</H2>");
                    if (m_GeomParams != null)
                    {
                        w.WriteLine("<Table border=1 bordercolor = darkblue>");
                        w.WriteLine("<caption>Геометрия</caption>");
                        foreach (var _prm in m_GeomParams.Keys)
                            w.WriteLine($"<th>{_prm}</th>");
                        w.WriteLine("<tr>");
                        foreach (var _val in m_GeomParams.Values)
                            w.WriteLine($"<td>{_val}</td>");
                        w.WriteLine("</tr>");
                        w.WriteLine("</Table>");
                    }
                    w.WriteLine("<H3>Расчет:</H3>");
                    if (m_CalcResults != null)
                    {
                        w.WriteLine("<Table border=1 bordercolor = darkblue>");
                        w.WriteLine("<tr>");
                        foreach (var _prm in m_CalcResults.Keys)
                        {
                            w.WriteLine($"<th>{_prm}</th>");
                        }
                        w.WriteLine("</tr>");
                        w.WriteLine("<tr>");
                        foreach (var _val in m_CalcResults.Values)
                        {
                            w.WriteLine($"<td>{_val}</td>");
                        }
                        w.WriteLine("</tr>");
                        w.WriteLine("</Table>");
                    }
                    else
                    {
                        w.WriteLine("<th>Расчет не выполнен</th>");
                    }
                    w.WriteLine("</html>");
                }

                pathToHtmlFile = fs.Name;
            }

            return pathToHtmlFile;
        }
    }
}

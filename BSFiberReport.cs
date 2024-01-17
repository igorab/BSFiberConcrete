using System;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BSFiberConcrete
{
    public class BSFiberReport_N : BSFiberReport
    {
        private BSFiberCalc_N fiberCalc;
        private Dictionary<string, string> dattr = new Dictionary<string, string>();

        public void Init(BSFiberCalc_N _fiberCalc)
        {
            fiberCalc = _fiberCalc;

            GetAttr();

            InitFromAttr();
        }

        private void GetAttr()
        {            
            PropertyInfo[] props = typeof(BSFiberCalc_N).GetProperties();
            foreach (PropertyInfo prop in props)
            {
                var attribute = prop.GetCustomAttributes(typeof(DisplayNameAttribute), true);
                DisplayNameAttribute attr = attribute.Cast<DisplayNameAttribute>().Single();                
                string displayName= attr.DisplayName;

                dattr.Add(prop.Name, displayName);
            }            
        }

        private void InitFromAttr()
        {
            m_GeomParams = new Dictionary<string, double>();

            foreach (var attr in dattr)
            {
                m_GeomParams.Add(attr.Key, 1 );
            }
        }


    }


    /// <summary>
    /// Построитель отчета
    /// </summary>
    public class BSFiberReport
    {
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

        private void Header(StreamWriter w)
        {
            w.WriteLine("<H1>Сопротивление сечения из фибробетона</H1>");
            w.WriteLine("<H4>Расчет выполнен по СП 360.1325800.2017</H4>");
        }


        public string CreateReport()
        {
            string pathToHtmlFile = "";

            using (FileStream fs = new FileStream("test.htm", FileMode.Create))
            {
                using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                {
                    w.WriteLine("<html>");

                    Header(w);

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

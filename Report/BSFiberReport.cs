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
using BSFiberConcrete.Properties;
using System.Drawing;
using System.Drawing.Imaging;

namespace BSFiberConcrete
{
    /// <summary>
    /// Построитель отчета
    /// </summary>
    public class BSFiberReport
    {
        public string ReportName { get; set; }
        public Dictionary<string, double> Beam { set { m_Beam = value; } }
        public Dictionary<string, double> Coeffs { set { m_Coeffs = value; } }
        public Dictionary<string, double> Efforts { set { m_Efforts = value; } }
        public Dictionary<string, double> PhysParams { set { m_PhysParams = value; } }
        public Dictionary<string, double> GeomParams { set { m_GeomParams = value; } }
        public Dictionary<string, double> CalcResults { set { m_CalcResults = value; } }
        public List<string> Messages { set { m_Messages = value; }}
        public BeamSection BeamSection { set { m_BeamSection = value; } }

        public bool UseReinforcement { get; set; }

        protected Dictionary<string, double> m_Beam;
        protected Dictionary<string, double> m_Coeffs;
        protected Dictionary<string, double> m_Efforts;
        protected Dictionary<string, double> m_PhysParams;
        protected Dictionary<string, double> m_GeomParams;
        protected Dictionary<string, double> m_CalcResults;
        protected List<string> m_Messages;

        protected BeamSection m_BeamSection;

        public string ImageCalc { get; set; }

        public BSFiberReport()
        {
            ReportName = "Сопротивление сечения из фибробетона";
            UseReinforcement = false;
        }

        /// <summary>
        ///  Верхняя часть отчета
        /// </summary>        
        protected virtual void Header(StreamWriter w)
        {
            w.WriteLine("<html>");
            w.WriteLine($"<H1>{ReportName}</H1>");
            w.WriteLine("<H4>Расчет выполнен по СП 360.1325800.2017</H4>");

            string beamDescr = typeof(BeamSection).GetCustomAttribute<DescriptionAttribute > (true).Description;
            string beamSection = BSHelper.EnumDescription(m_BeamSection);
            w.WriteLine($"<H2>{beamDescr}: {beamSection}</H2>");
            
            string _filename = string.IsNullOrWhiteSpace(ImageCalc) ? BSHelper.ImgResource(m_BeamSection, UseReinforcement) : ImageCalc;
            if (!string.IsNullOrEmpty(_filename))
            {                
                string path = Lib.BSData.ResourcePath(_filename);
                string img = MakeImageSrcData(path);
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

        /// <summary>
        /// Основная часть отчета
        /// </summary>        
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
                    w.WriteLine($"<td>| {Math.Round(_pair.Value, 4)} </td>");
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
                    w.WriteLine($"<td>| {Math.Round(_pair.Value, 4)} </td>");
                    w.WriteLine("</tr>");
                }                                
                w.WriteLine("</Table>");
                w.WriteLine("<br>");
            }

            if (m_Efforts != null)
            {
                w.WriteLine("<Table border=1 bordercolor = darkblue>");
                w.WriteLine("<caption>Усилия</caption>");
                foreach (var _pair in m_Efforts)
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
                    w.WriteLine($"<td colspan=2>| {Math.Round(_pair.Value, 4)} </td>");
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

        protected void MakeImageData(string _filename)
        {
            using (Bitmap image = new Bitmap(450, 100))
            {
                using (Graphics graphic = Graphics.FromImage(image))
                {
                    System.Drawing.Image thumbnail = System.Drawing.Image.FromFile(_filename);

                    // Сохранить изображение в поток
                    //Response.ContentType = "image/png";

                    // Создать PNG-изображение, хранящееся в памяти
                    MemoryStream mem = new MemoryStream();
                    image.Save(mem, System.Drawing.Imaging.ImageFormat.Png);

                    // Нарисовать эскиз
                    //graphic.DrawImage(thumbnail, 0, 0, x, y);

                    // Сохранить изображение
                    image.Save(mem, ImageFormat.Png);


                    // Записать данные MemoryStream в выходной поток
                    //mem.WriteTo(Response.OutputStream);
                }
            }
        }


        private string MakeImageSrcData(string _filename)
        {
            if (_filename == "") return "";

            string _img = "";
            try
            {
                using (Image img = Image.FromFile(_filename))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        img.Save(ms, ImageFormat.Png);

                        byte[] imgBytes = ms.ToArray();
                        string _extension = Path.GetExtension(_filename).Replace(".", "").ToLower();

                        _img = String.Format("\"data:image/{0};base64, {1}\" alt = \"{2}\" ", _extension, Convert.ToBase64String(imgBytes), _filename);
                    }
                }
            }
            catch (Exception _e) 
            {
                _img = _e.Message;
            }
            
            return _img;
        }

        protected virtual void Footer(StreamWriter w)
        {
            if (m_Messages != null)
            {
                w.WriteLine("<Table border=1 bordercolor = darkblue>");
                w.WriteLine("<caption>Итог:</caption>");
                foreach (var _value in m_Messages)
                {
                    w.WriteLine("<tr>");                    
                    w.WriteLine($"<td>| {_value} </td>");
                    w.WriteLine("</tr>");
                }
                w.WriteLine("</Table>");
                w.WriteLine("<br>");
            }

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

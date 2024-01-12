using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace BSFiberConcrete
{
    public partial class BSFiberMain : Form
    {
        private DataTable m_Table;
        private Dictionary<string, double> m_Coeffs;
        private Dictionary<string, double> m_PhysParams;
        private Dictionary<string, double> m_GeomParams;
        private Dictionary<string, double> m_CalcResults;
        BeamSection m_BeamSection;

        public BSFiberMain()
        {
            InitializeComponent();
        }

        private void BSFiberMain_Load(object sender, EventArgs e)
        {
            m_Table = new DataTable();
            m_BeamSection = BeamSection.Ring;

            cmbBetonClass.DataSource = BSFiberCocreteLib.betonList;
            cmbBetonClass.DisplayMember = "Name";
            cmbBetonClass.ValueMember = "Id";

            //cmbBetonClass.SelectedIndexChanged += cmbBetonClass_SelectedIndexChanged;

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            try
            {
                BSFiberCalculation bsCalc = BSFiberCalculation.construct(m_BeamSection);
                BSFiberLoadData bsLoad = new BSFiberLoadData();
                bsLoad.Load();
                double[] prms = bsLoad.Params;

                bsCalc.GetParams(prms);
                
                m_PhysParams = bsCalc.PhysParams;
                m_Coeffs = bsCalc.Coeffs;
                m_GeomParams = bsCalc.GeomParams();
               
                double[] sz = new double[2];
                foreach (DataRow r in m_Table.Rows)
                {
                    sz = new double[r.ItemArray.Length];
                    int idx = 0;
                    foreach (var item in r.ItemArray)
                    {
                        sz[idx] = (double)item;
                        idx++;
                    }
                }
                bsCalc.GetSize(sz);
                bsCalc.Calculate();

                m_CalcResults = bsCalc.Results();

                if (m_CalcResults.TryGetValue("Wpl", out double _wpl))
                {
                    lblRes0.Text = "Wpl";
                    tbResultW.Text = _wpl.ToString();
                }

                if (m_CalcResults.TryGetValue("x", out double _x))
                {
                    lblRes0.Text = "x";
                    tbResultW.Text = _x.ToString();
                }

                if (m_CalcResults.TryGetValue("Mult", out double _mult))
                {
                    lblResult.Text = "Mult";
                    tbResult.Text = _mult.ToString();
                }
            }
            catch
            {
                MessageBox.Show("Ошибка в расчете");
            }
        }

        private string CreateReport()
        {
            string pathToHtmlFile = "";
            
            using (FileStream fs = new FileStream("test.htm", FileMode.Create))
            {
                using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                {
                    w.WriteLine("<html>");
                    w.WriteLine("<H1>Фибробетон</H1>");
                    
                    if (m_PhysParams != null)
                    {
                        w.WriteLine("<Table>");
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
                        w.WriteLine("<Table>");
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
                        w.WriteLine("<Table>");
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
                        w.WriteLine("<Table>");
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


        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                string pathToHtmlFile = CreateReport();

                System.Diagnostics.Process.Start(pathToHtmlFile);
            }
            catch
            {
                MessageBox.Show("Ошибка в отчете");
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void propertyGrid1_Click(object sender, EventArgs e)
        {

        }

        // прямоугольное сечение
        private void btnRectang_Click(object sender, EventArgs e)
        {
            m_BeamSection = BeamSection.Rect;

            m_Table = new DataTable();
            m_Table.Columns.Add("b, cm", typeof(double));
            m_Table.Columns.Add("h, cm", typeof(double));

            dataGridView1.DataSource = m_Table;
            m_Table.Rows.Add(80d, 60d);

            picBeton.Image = global::BSFiberConcrete.Properties.Resources.FiberBeton;
        }

        // тавровое сечение
        private void btnTSection_Click(object sender, EventArgs e)
        {
            m_BeamSection = BeamSection.TBeam;

            m_Table = new DataTable();
            m_Table.Columns.Add("b, cm", typeof(double));
            m_Table.Columns.Add("h, cm", typeof(double));
            m_Table.Columns.Add("b1, cm", typeof(double));
            m_Table.Columns.Add("h1, cm", typeof(double));

            dataGridView1.DataSource = m_Table;
            m_Table.Rows.Add(80d, 20d, 20d, 20d);
        }

        // кольцевое сечение
        private void btnRing_Click(object sender, EventArgs e)
        {
            m_BeamSection = BeamSection.Ring;

            m_Table = new DataTable();
            m_Table.Columns.Add("r1, cm", typeof(double));
            m_Table.Columns.Add("r2, cm", typeof(double));
                        
            dataGridView1.DataSource = m_Table;
            m_Table.Rows.Add(25d, 40d);

            picBeton.Image = global::BSFiberConcrete.Properties.Resources.Ring;
        }

        // двутавровое сечение
        private void btnIBeam_Click(object sender, EventArgs e)
        {
            m_BeamSection = BeamSection.IBeam;

            m_Table = new DataTable();
            m_Table.Columns.Add("bf, cm", typeof(double));
            m_Table.Columns.Add("hf, cm", typeof(double));
            m_Table.Columns.Add("hw, cm", typeof(double));
            m_Table.Columns.Add("bw, cm", typeof(double));
            m_Table.Columns.Add("b1f, cm", typeof(double));
            m_Table.Columns.Add("h1f, cm", typeof(double));

            dataGridView1.DataSource = m_Table;
            m_Table.Rows.Add(80d, 20d, 20d, 20d, 80d, 20d);

            picBeton.Image = global::BSFiberConcrete.Properties.Resources.IBeam;
            picBeton.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void AboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            BSFiberAboutBox aboutWindow = new BSFiberAboutBox();
            aboutWindow.Show();
        }

        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BSFiberSetup setupWindow = new BSFiberSetup();
            setupWindow.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBetonType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbBetonClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // получаем id выделенного объекта
                int id = (int)cmbBetonClass.SelectedValue;

                // получаем весь выделенный объект
                BSFiberBeton beton = (BSFiberBeton)cmbBetonClass.SelectedItem;
                MessageBox.Show(id.ToString() + ". " + beton.Name);
            }
            catch { }
        }
    }
}

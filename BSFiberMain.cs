using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace BSFiberConcrete
{
    public partial class BSFiberMain : Form
    {
        private DataTable m_Table { get; set; }

        private BSFiberCalculation bsCalc;
        private BSFiberLoadData m_BSLoadData;
        public Dictionary<string, double> m_Beam { get; private set; }
        private Dictionary<string, double> m_Coeffs;
        private Dictionary<string, double> m_PhysParams;
        private Dictionary<string, double> m_GeomParams;
        private Dictionary<string, double> m_CalcResults;
        private BeamSection m_BeamSection;

        public BSFiberMain()
        {
            InitializeComponent();
        }

        private void BSFiberMain_Load(object sender, EventArgs e)
        {
            m_Beam = new Dictionary<string, double>();
            m_Table = new DataTable();

            m_BSLoadData = new BSFiberLoadData();
            m_BSLoadData.Load();

            LoadRectangle();

            cmbBetonClass.DataSource = BSFiberCocreteLib.betonList;
            cmbBetonClass.DisplayMember = "Name";
            cmbBetonClass.ValueMember = "Id";

            numRfbt3n.Value = (decimal)BSFiberCocreteLib.PhysElements.Rfbt3n;
            numRfbn.Value = (decimal)BSFiberCocreteLib.PhysElements.Rfbn;

            numYft.Value =  (decimal)BSFiberCocreteLib.PhysElements.Yft;
            numYb1.Value = (decimal)BSFiberCocreteLib.PhysElements.Yb1;
            numYb2.Value = (decimal)BSFiberCocreteLib.PhysElements.Yb2;
            numYb3.Value = (decimal)BSFiberCocreteLib.PhysElements.Yb3;
            numYb5.Value = (decimal)BSFiberCocreteLib.PhysElements.Yb5;


            //cmbBetonClass.SelectedIndexChanged += cmbBetonClass_SelectedIndexChanged;


        }

        private double[] BeamSizes(double _length = 0)
        {
            double[] sz = new double[2];
            foreach (DataRow r in m_Table.Rows)
            {
                sz = new double[r.ItemArray.Length + 1];
                int idx = 0;
                foreach (var item in r.ItemArray)
                {
                    sz[idx] = (double)item;
                    idx++;
                }
                sz[idx] = _length;
            }

            return sz;
        }

        
        private void btnCalc_Click(object sender, EventArgs e)
        {
            try
            {
                bsCalc = BSFiberCalculation.construct(m_BeamSection);
                
                double[] prms = m_BSLoadData.Params;

                bsCalc.GetParams(prms);
                
                m_Beam.Clear();
                m_PhysParams = bsCalc.PhysParams;
                m_Coeffs = bsCalc.Coeffs;                                               
                bsCalc.GetSize(BeamSizes());                

                double.TryParse(tbLength.Text, out double lgth);
                m_Beam.Add("Длина элемента",  lgth);
                double.TryParse(cmbEffectiveLengthFactor.Text, out double coeflgth);
                m_Beam.Add("Коэффициет расчетной длины", coeflgth);

                bsCalc.Calculate();

                m_GeomParams = bsCalc.GeomParams();
                m_CalcResults = bsCalc.Results();

                if (m_CalcResults.TryGetValue("Rfbt3", out double _rfbt3))
                {
                    lblRes0.Text = "Rfbt3";
                    tbResultW.Text = _rfbt3.ToString();
                }

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
            catch (Exception _e)
            {
                MessageBox.Show("Ошибка в расчете: " + _e.Message);
            }
        }

        private string CreateReport()
        {
            string path = "";
            BSFiberReport report = new BSFiberReport();

            report.Beam = m_Beam;
            report.Coeffs = m_Coeffs;
            report.GeomParams = m_GeomParams;
            report.PhysParams = m_PhysParams;
            report.BeamSection = m_BeamSection;
            report.CalcResults = m_CalcResults;

            var publicProperties = typeof(BSFibCalc_IBeam).GetProperties();
            foreach (PropertyInfo property in publicProperties)
            {
                var attr = property.GetCustomAttributes();

                string s = property.Name;
            }

            path = report.CreateReport();
            return path;
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
            
        
        private void LoadRectangle()
        {
            m_BeamSection = BeamSection.Rect;

            m_Table = new DataTable();
            m_Table.Columns.Add("b, cm", typeof(double));
            m_Table.Columns.Add("h, cm", typeof(double));

            dataGridView1.DataSource = m_Table;
            m_Table.Rows.Add(80d, 60d);

            picBeton.Image = global::BSFiberConcrete.Properties.Resources.FiberBeton;
            picBeton.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        // прямоугольное сечение
        private void btnRectang_Click(object sender, EventArgs e)
        {
            LoadRectangle();
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

            picBeton.Image = global::BSFiberConcrete.Properties.Resources.IBeam;
            picBeton.SizeMode = PictureBoxSizeMode.StretchImage;
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

            //setupWindow.Records;
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

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void lblRes0_Click(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void tbLength_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!double.TryParse(tbLength.Text + e.KeyChar.ToString(), out double a) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void btnCalcM_Click(object sender, EventArgs e)
        {
            BSFiberCalc_M fiberCalc = new BSFiberCalc_M();
            fiberCalc.Calculate();
        }

        private void btnCalcQ_Click(object sender, EventArgs e)
        {
            BSFiberCalc_Q  fiberCalc = new BSFiberCalc_Q();
            fiberCalc.Calculate();
        }

        private void btnCalcN_Click(object sender, EventArgs e)
        {
            var value = gridEfforts.Rows[0].Cells[1].Value;
            
            BSFiberCalc_N fiberCalc = new BSFiberCalc_N();
            try
            {
                fiberCalc.GetParams(m_BSLoadData.Params);

                double beamLngth = double.Parse(tbLength.Text);

                double[] sz = BeamSizes(beamLngth);

                fiberCalc.GetSize(sz);

                fiberCalc.Calculate();

                m_CalcResults = fiberCalc.Results();
            }
            catch (Exception _ex)
            {
                MessageBox.Show("Ошибка расчета: " + _ex.Message);
            }
            finally
            {
                BSFiberReport_N report = new BSFiberReport_N();

                report.BeamSection = m_BeamSection;
                report.Init(fiberCalc);
                
                string pathToHtmlFile = report.CreateReport();

                System.Diagnostics.Process.Start(pathToHtmlFile);
            }
        }
    }
}

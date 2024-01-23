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

            m_BSLoadData.ReadParamsFromJson();

            cmbBetonClass.DataSource = BSFiberCocreteLib.betonList;
            cmbBetonClass.DisplayMember = "Name";
            cmbBetonClass.ValueMember = "Id";
            cmbBetonClass.SelectedValue = 30;

            numRfbt3n.Value = (decimal)BSFiberCocreteLib.PhysElements.Rfbt3n;
            numRfbn.Value = (decimal)BSFiberCocreteLib.PhysElements.Rfbn;

            numYft.Value =  (decimal)BSFiberCocreteLib.PhysElements.Yft;
            numYb.Value = (decimal)BSFiberCocreteLib.PhysElements.Yb;
            numYb1.Value = (decimal)BSFiberCocreteLib.PhysElements.Yb1;
            numYb2.Value = (decimal)BSFiberCocreteLib.PhysElements.Yb2;
            numYb3.Value = (decimal)BSFiberCocreteLib.PhysElements.Yb3;
            numYb5.Value = (decimal)BSFiberCocreteLib.PhysElements.Yb5;

            double[] mnq = { 1.0, 50000, 3.0 }; //MNQ
            
            gridEfforts.Rows[0].Cells[0].Value = mnq[0];
            gridEfforts.Rows[0].Cells[1].Value = mnq[1];
            gridEfforts.Rows[0].Cells[2].Value = mnq[2];

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

        private void InitUserParams(double[] prms)
        {
            if (prms.Length >= 8)
            {
                prms[0] = Convert.ToDouble(numRfbt3n.Value);
                prms[1] = Convert.ToDouble(numRfbn.Value);
                prms[2] = Convert.ToDouble(numYft.Value);
                prms[3] = Convert.ToDouble(numYb.Value);
                prms[4] = Convert.ToDouble(numYb1.Value);
                prms[5] = Convert.ToDouble(numYb2.Value);
                prms[6] = Convert.ToDouble(numYb3.Value);
                prms[7] = Convert.ToDouble(numYb5.Value);
                prms[8] = Convert.ToDouble(cmbBetonClass.SelectedValue);
            }
        }
        
        private void btnCalc_Click(object sender, EventArgs e)
        {
            try
            {
                bsCalc = BSFiberCalculation.construct(m_BeamSection);
                
                double[] prms = m_BSLoadData.Params;
                InitUserParams(prms);
                                                                    
                bsCalc.GetParams(prms);                
                m_Beam.Clear();                                                       
                bsCalc.GetSize(BeamSizes());                

                double.TryParse(tbLength.Text, out double lgth);
                m_Beam.Add("Длина элемента",  lgth);
                double.TryParse(cmbEffectiveLengthFactor.Text, out double coeflgth);
                m_Beam.Add("Коэффициет расчетной длины", coeflgth);

                bsCalc.Calculate();

                m_PhysParams = bsCalc.PhysParams;
                m_Coeffs = bsCalc.Coeffs;
                m_GeomParams = bsCalc.GeomParams();
                m_CalcResults = bsCalc.Results();

                int ires = 0;
                foreach (var res in m_CalcResults)
                {
                    if (ires == 0)
                    {
                        lblRes0.Text = res.Key;
                        tbResultW.Text = res.Value.ToString();
                    }
                    else if (ires == 1)
                    {
                        lblResult.Text = res.Key;
                        tbResult.Text = res.Value.ToString();
                    }
                    else 
                        break;

                    ires++;
                }
                
            }
            catch (Exception _e)
            {
                MessageBox.Show("Ошибка в расчете: " + _e.Message);
            }
        }

        private string CreateReport()
        {
            try
            {
                if (bsCalc is null)
                    throw new Exception("Не выполнен расчет");

                string path = "";
                BSFiberReport report = new BSFiberReport();

                report.Beam = m_Beam;
                report.Coeffs = m_Coeffs;
                report.GeomParams = m_GeomParams;
                report.PhysParams = bsCalc.PhysicalParameters();
                report.BeamSection = m_BeamSection;
                report.CalcResults = m_CalcResults;

                path = report.CreateReport();
                return path;
            }
            catch (Exception _e)
            {
                throw _e;
            }
        }


        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                string pathToHtmlFile = CreateReport();

                System.Diagnostics.Process.Start(pathToHtmlFile);
            }
            catch (Exception _e)
            {
                MessageBox.Show("Ошибка в отчете " + _e.Message);
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
                
                numRfbt3n.Value = Convert.ToDecimal( BSFiberCalcHelper.MPA2kgsm2(beton.Rfbt3));
                numRfbn.Value = Convert.ToDecimal(BSFiberCalcHelper.MPA2kgsm2(beton.Rfbn));
                //MessageBox.Show(id.ToString() + ". " + beton.Name);


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
        
        private void btnCalcN_Click(object sender, EventArgs e)
        {
            BSFiberCalc_N fiberCalc = new BSFiberCalc_N();
            Dictionary<char, double> MNQ = new Dictionary<char, double>();
            char[] F = new char[] { 'M', 'N', 'Q' };

            try
            {
                DataGridViewRowCollection rows = gridEfforts.Rows;
                var row = rows[0];

                for (int i=0; i<3; i++ )
                {                    
                    var x = Convert.ToDouble(row.Cells[i].Value);
                    MNQ.Add(F[i], x);
                }

                //if (effortsMNQ == null)
                //    throw new Exception("Не заданы усилия");

                double[] prms = m_BSLoadData.Params;

                InitUserParams(prms);

                fiberCalc.GetParams(prms);

                double beamLngth = double.Parse(tbLength.Text);

                double[] sz = BeamSizes(beamLngth);

                fiberCalc.GetSize(sz);

                fiberCalc.GetEfforts(MNQ);

                fiberCalc.GetFiberParamsFromJson(m_BSLoadData.Fiber);

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

        private void gridEfforts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

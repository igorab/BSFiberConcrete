using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
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
        
        // глобальные настройки
        private void BSFiberMain_Load(object sender, EventArgs e)
        {
            try
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

                numYft.Value = (decimal)BSFiberCocreteLib.PhysElements.Yft;
                numYb.Value = (decimal)BSFiberCocreteLib.PhysElements.Yb;
                numYb1.Value = (decimal)BSFiberCocreteLib.PhysElements.Yb1;
                numYb2.Value = (decimal)BSFiberCocreteLib.PhysElements.Yb2;
                numYb3.Value = (decimal)BSFiberCocreteLib.PhysElements.Yb3;
                numYb5.Value = (decimal)BSFiberCocreteLib.PhysElements.Yb5;

                double[] mnq = { 40.0, 95.0, 3.0, 5000, 1.0, 37 }; //MNQ Ml
                gridEfforts.Rows.Add(mnq);
                for (int i = 0; i < mnq.Length; i++)
                {
                    gridEfforts.Rows[0].Cells[i].Value = mnq[i];
                }

                double[] t_rebar = { 6, 2, 4, 400, 1}; // поперечная арматура
                gridTRebar.Rows.Add(t_rebar);
                for (int i = 0; i < t_rebar.Length; i++)
                {
                    gridTRebar.Rows[0].Cells[i].Value = t_rebar[i];
                }

                double[] long_rebar = { 16, 2, 4, 400, 1 }; // продольная арматура
                gridLRebar.Rows.Add(long_rebar);
                for (int i = 0; i < long_rebar.Length; i++)
                {
                    gridLRebar.Rows[0].Cells[i].Value = long_rebar[i];
                }
            }
            catch (Exception _ex) 
            {
                MessageBox.Show(_ex.Message);
            }
        }

        //  размеры балки
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

        // параметры , заданные пользователем 
        private void InitUserParams(double[] prms)
        {
            int idx = -1;
            if (prms.Length >= 8)
            {
                prms[++idx] = Convert.ToDouble(numRfbt3n.Value);
                prms[++idx] = Convert.ToDouble(numRfbn.Value);
                prms[++idx] = Convert.ToDouble(numYft.Value);
                prms[++idx] = Convert.ToDouble(numYb.Value);
                prms[++idx] = Convert.ToDouble(numYb1.Value);
                prms[++idx] = Convert.ToDouble(numYb2.Value);
                prms[++idx] = Convert.ToDouble(numYb3.Value);
                prms[++idx] = Convert.ToDouble(numYb5.Value);
                prms[++idx] = Convert.ToDouble(cmbBetonClass.SelectedValue);
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
        
        private void cmbBetonClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {                
                int id = (int)cmbBetonClass.SelectedValue;
             
                BSFiberBeton beton = (BSFiberBeton)cmbBetonClass.SelectedItem;
                
                numRfbt3n.Value = Convert.ToDecimal( BSHelper.MPA2kgsm2(beton.Rfbt3));
                numRfbn.Value = Convert.ToDecimal(BSHelper.MPA2kgsm2(beton.Rfbn));
                
            }
            catch { }
        }
                
        private void tbLength_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!double.TryParse(tbLength.Text + e.KeyChar.ToString(), out double a) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void InitEfforts(ref Dictionary<string, double> MNQ)
        {
            string[] F = new string[] { "Mx", "My", "N", "Q", "Ml", "eN"};
            DataGridViewRowCollection rows = gridEfforts.Rows;
            var row = rows[0];

            for (int i = 0; i < F.Length; i++)
            {
                var x = Convert.ToDouble(row.Cells[i].Value);
                MNQ.Add(F[i], x);
            }

            if (MNQ.Count == 0)
                throw new Exception("Не заданы усилия");
        }

        /// <summary>
        /// Усилия
        /// </summary>
        /// <param name="fiberCalc"></param>
        /// <param name="_rebar">Армирование</param>
        /// <param name="_fissurre">Расчет на трещиностойкость</param>
        private void FiberCalc_MNQ(out BSFiberCalc_MNQ fiberCalc, bool _useRebar = false, bool _fissurre = false, bool _shear = false )
        {
            fiberCalc = BSFiberCalc_MNQ.Construct(m_BeamSection);
            fiberCalc.UseRebar = _useRebar;
            fiberCalc.Fissure = _fissurre;
            fiberCalc.Shear = _shear;

            Dictionary<string, double> MNQ = new Dictionary<string, double>();

            InitEfforts(ref MNQ);

            if (_shear /* && _rebar*/)
            {
                InitLRebar(out double[] t_r);

                InitTRebar(out double[] l_r);

                fiberCalc.GetRebarParams(t_r, l_r);
            }

            double[] prms = m_BSLoadData.Params;

            InitUserParams(prms);

            fiberCalc.GetParams(prms);

            double beamLngth = double.Parse(tbLength.Text);

            double[] sz = BeamSizes(beamLngth);

            fiberCalc.GetSize(sz);

            fiberCalc.GetEfforts(MNQ);

            fiberCalc.GetFiberParamsFromJson(m_BSLoadData.Fiber);
            // Армирование
            fiberCalc.Rebar = m_BSLoadData.Rebar; 

            fiberCalc.Calculate();

            m_CalcResults = fiberCalc.Results();
            
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_shear">Расчет по наклонному сечению на действие Q</param>
        private void FiberCalculateMNQ_Report(bool _shear = false)
        {
            BSFiberCalc_MNQ fiberCalc = new BSFiberCalc_MNQ();

            try
            {
                FiberCalc_MNQ(out fiberCalc, checkBoxRebar.Checked, checkBoxFissure.Checked, _shear);
            }
            catch (Exception _ex)
            {
                MessageBox.Show("Ошибка расчета: " + _ex.Message);
            }
            finally
            {
                BSFiberReport_MNQ report = new BSFiberReport_MNQ();

                report.BeamSection = m_BeamSection;
                report.Init(fiberCalc);

                string pathToHtmlFile = report.CreateReport();

                System.Diagnostics.Process.Start(pathToHtmlFile);
            }
        }

        private void btnCalcMNQ_Click(object sender, EventArgs e)
        {
            FiberCalculateMNQ_Report();
        }

        // поперечная арматура
        private void InitTRebar(out double[] t_rebar)
        {            
            DataGridViewRowCollection rows_t = gridTRebar.Rows;
            var row = rows_t[0];

            t_rebar = new double[row.Cells.Count];
            for (int i = 0; i < t_rebar.Length; i++)
            {
                double x = Convert.ToDouble(row.Cells[i].Value);
                t_rebar[i] = x;
            }
        }

        // продольная арматура
        private void InitLRebar(out double[] l_rebar)
        {
            DataGridViewRowCollection rows_l = gridLRebar.Rows;
            var row = rows_l[0];

            l_rebar = new double[row.Cells.Count];
            for (int i = 0; i < l_rebar.Length; i++)
            {
                double x = Convert.ToDouble(row.Cells[i].Value);
                l_rebar[i] = x;
            }
        }


        // Расчет элементов по полосе между наклонными сечениями
        // Расчет на действие момента и поперечной силы
        private void btnCalc_Q_Click(object sender, EventArgs e)
        {                        
            FiberCalculateMNQ_Report(_shear: true);
        }
        
        private void btnFactors_Click(object sender, EventArgs e)
        {
            BSFactors bsFactors = new BSFactors();
            bsFactors.Show();
        }

        private bool UserInput = false;

        private void btnCalc_Deform_Click(object sender, EventArgs e)
        {
            const string cBtCls = "B3.5";
            const double cRb = 7.65; // МПа
            const double cRs = 350; // МПа
            const double cEb = 24000; // Мпа
            const double cEs = 200000d; // Мпа
            const double c_eps_s0 = 0.00175; // Мпа
            const double c_eps_s2 = 0.025; // Мпа

            const double c_eps_b1 = 0.0015;
            const double c_eps_b1_red = 0.0015;
            const double c_eps_b2 = 0.0035;

            const double c_h = 60, // см 
                         c_b = 30; // см

            // Начало координат:
            double X0 = c_b / 2.0;
            double Y0 = c_h / 2.0;

            double c_L = Convert.ToDouble(tbLength.Text); // см
            const string cRCls = "B3.5";
            const double c_D_lng = 16.0;
            const double c_D_tr = 8.0;

            const int c_Y_N = 10; // разбиение по высоте
            const int c_X_M = 1; // разбиение по ширине

            // Mx  , My - усилия, кНм
            double c_Mx = 0; // 45; 
            double c_My = 200;  //95; 
            double c_N = 0;  //0; 

            // задать усилия
            if (UserInput)
            {
                Dictionary<string, double> MNQ = new Dictionary<string, double>();
                InitEfforts(ref MNQ);
                c_Mx = MNQ["Mx"];
                c_My = MNQ["My"];
                c_N = MNQ["N"];
            }

            BSFiberCalc_Deform fiberCalc_Deform = new BSFiberCalc_Deform(_Mx: c_Mx, _My: c_My, _N: c_N);

            // задать тип арматуры
            fiberCalc_Deform.MatRebar = new BSMatRod(cEs) 
            { 
                RCls = cRCls,
                Rs = cRs,
                e_s0 = c_eps_s0,
                e_s2 = c_eps_s2
            };

            // расстановка арматурных стержней
            List<BSRod> rods = new List<BSRod>();
            // раскладка арматуры X Y:, см
            double[,] rdYdX = { { 4, 4 }, { 15, 4 }, { 26, 4 } };
            double[] rD_lng = new double[] { 2.5, 1.8, 2.5 }; // D , см
            double[] _As = new double[] {4.909, 2.545, 4.909 };
            //  пример фибробетон  
            //{ { 40, 80 }, { 300, 80 }, { 40, 120 }, { 300, 120 }, { 40, 640 }, { 300, 640 }, {40, 1115 }, {300, 1115}};
            int rows = rdYdX.GetUpperBound(0) + 1;    // количество строк            
            for (int i = 0; i < rows; i++)
            {
                BSRod rod = new BSRod()
                {
                    Num = i,
                    LTType = RebarLTType.Longitudinal,
                    D = rD_lng[i],
                    Z_X = rdYdX[i, 0] - X0,
                    Z_Y = -1 * (rdYdX[i, 1] - Y0),
                    MatRod = fiberCalc_Deform.MatRebar,
                    Nu = 1.0 // считать
                };

                rods.Add(rod);
            }


            BSMatFiber material = new BSMatFiber(cEb)
            {
                BTCls = cBtCls,
                Nu_fb = 1,
                Rfbn = cRb, //МПа
                Rfbt = 0,  
                Rfbt2 = 0,
                Rfbt3 = 0,
                e_b1_red = c_eps_b1_red,
                e_b1 = c_eps_b1,
                e_b2 = c_eps_b2 
            };

            // задать свойства бетона
            fiberCalc_Deform.MatFiber = material;

            // задать размеры балки,
            // задать материал,
            // присвоить арматуру
            fiberCalc_Deform.Beam = new BSBeam_Rect(c_b, c_h)
            {
                Length = c_L,
                Rods = rods,
                BSMat = material 
            };

            double[] NM = new double[] { c_Y_N, c_X_M };
            fiberCalc_Deform.GetParams(NM);

            // рассчитать
            fiberCalc_Deform.Calculate();

            // получить результат
            var res = fiberCalc_Deform.Results();
        }
        
    }
}

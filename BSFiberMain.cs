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
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Text.Json;
using BSFiberConcrete.Lib;
using CsvHelper.Configuration.Attributes;

namespace BSFiberConcrete
{
    public partial class BSFiberMain : Form
    {
        private DataTable m_Table { get; set; }

        public CalcType CalcType { get; set; }

        //private BetonType
        private Dictionary<string, double> m_Iniv ;
        private BSFiberCalculation bsCalc;
        private BSFiberLoadData m_BSLoadData;
        private List<Elements> FiberConcrete;

        public Dictionary<string, double> m_Beam { get; private set; }
        private Dictionary<string, double> m_Coeffs;
        private Dictionary<string, double> m_Efforts;
        private Dictionary<string, double> m_PhysParams;
        private Dictionary<string, double> m_GeomParams;
        private Dictionary<string, double> m_CalcResults;

        private List<string> m_Message;
        private BeamSection m_BeamSection;


        public BSFiberMain()
        {
            InitializeComponent();
        }
        
        private void CalcTypeShow()
        {
            if (CalcType == CalcType.Static)
            {                
                btnCalc_Q.Visible = true;
                btnCalc_Deform.Visible = false;               
            }
            else if (CalcType == CalcType.Nonlinear)
            {             
                btnCalc_Q.Visible = false;
                btnCalc_Deform.Visible = true;
            }
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

                FiberConcrete = BSData.LoadFiberConcreteTable();                
                cmbFib_i.SelectedIndex = 0;
                comboBetonType.SelectedIndex = 0;

                m_Iniv = m_BSLoadData.ReadInitFromJson();
                List<Efforts> eff = Lib.BSData.LoadEfforts();
                if (eff.Count > 0)
                {
                    m_Iniv["Mx"] = eff[0].Mx;
                    m_Iniv["My"] = eff[0].My;
                    m_Iniv["N"] = eff[0].N;
                    m_Iniv["Q"] = eff[0].Q;
                    m_Iniv["Ml"] = eff[0].Ml;
                    m_Iniv["eN"] = eff[0].eN;
                }

                m_BSLoadData.ReadParamsFromJson();

                LoadRectangle(m_Iniv["b"], m_Iniv["h"]);

                cmbBetonClass.DataSource = BSFiberLib.BetonList;
                cmbBetonClass.DisplayMember = "Name";
                cmbBetonClass.ValueMember = "Name";
                cmbBetonClass.SelectedValue = BSFiberLib.BetonList[5].Name;

                Elements fiberConcrete = BSFiberLib.PhysElements;

                //numRfbt3n.Value = (decimal)fiberConcrete.Rfbt3n;
                //numRfbn.Value = (decimal)fiberConcrete.Rfbn;
                numYft.Value = (decimal)fiberConcrete.Yft;
                numYb.Value = (decimal)fiberConcrete.Yb;
                numYb1.Value = (decimal)fiberConcrete.Yb1;
                numYb2.Value = (decimal)fiberConcrete.Yb2;
                numYb3.Value = (decimal)fiberConcrete.Yb3;
                numYb5.Value = (decimal)fiberConcrete.Yb5;
                                
                double[] mnq = { m_Iniv["Mx"], m_Iniv["My"], m_Iniv["N"], m_Iniv["Q"], m_Iniv["Ml"], m_Iniv["eN"] }; //Mx My N Q Ml
                gridEfforts.Rows.Add(mnq);
                for (int i = 0; i < mnq.Length; i++)
                {
                    gridEfforts.Rows[0].Cells[i].Value = mnq[i];
                }

                double[] t_rebar = { m_Iniv["D_t"], m_Iniv["Qty_t"], m_Iniv["a_t"], m_Iniv["Cls_t"], m_Iniv["Coef_t"] }; // поперечная арматура
                gridTRebar.Rows.Add(t_rebar);
                for (int i = 0; i < t_rebar.Length; i++)
                {
                    gridTRebar.Rows[0].Cells[i].Value = t_rebar[i];
                }
                
                double[] long_rebar = { m_Iniv["D_l"], m_Iniv["Qty_l"], m_Iniv["a_l"], m_Iniv["Cls_l"], m_Iniv["Coef_l"] }; // продольная арматура
                for (int i=0; i<3; i ++) gridLRebar.Rows.Add(long_rebar);
                
                for (int i = 0; i < long_rebar.Length; i++)
                {
                    gridLRebar.Rows[0].Cells[i].Value = long_rebar[i];
                    gridLRebar.Rows[1].Cells[i].Value = long_rebar[i];// (i==1)? 0: long_rebar[i];
                    gridLRebar.Rows[2].Cells[i].Value = long_rebar[i];  // (i==1) ? 0 : long_rebar[i];
                }

                numAs.Value = (decimal)m_Iniv["As"]; 
                numAs1.Value = (decimal)m_Iniv["As1"];
                num_a.Value = (decimal)m_Iniv["_a"]; 
                num_a1.Value = (decimal)m_Iniv["_a_1"];

                CalcTypeShow();
            }
            catch (Exception _ex) 
            {
                MessageBox.Show(_ex.Message);
            }
        }

        //  размеры балки (поперечное сечение + длина)
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
                prms[++idx] = 0; // Convert.ToDouble(cmbBetonClass.SelectedItem.Id);
            }
        }

        private void InitBeamLength()
        {
            m_Beam.Clear();

            double.TryParse(tbLength.Text, out double lgth);
            m_Beam.Add("Длина элемента, см", lgth);
            double.TryParse(cmbEffectiveLengthFactor.Text, out double coeflgth);
            m_Beam.Add("Коэффициет расчетной длины", coeflgth);
        }

        /// <summary>
        /// Передать данные по армированию
        /// </summary>
        /// <param name="_bsCalc"></param>
        private void InitRebar(BSFiberCalculation _bsCalc)
        {
            double[] matRod = new double[] { m_BSLoadData.Rebar.Rs, m_BSLoadData.Rebar.Rsc,
                                                (double)numAs.Value, (double)numAs1.Value,  m_BSLoadData.Rebar.Es,
                                                (double)num_a.Value, (double)num_a1.Value };

            if (_bsCalc is BSFiberCalc_RectRods)
            {
                BSFiberCalc_RectRods _bsCalcRods = (BSFiberCalc_RectRods)_bsCalc;

                InitLRebar(out double[] _l_rebar);
                InitTRebar(out double[] _t_rebar);

                _bsCalcRods.GetLTRebar(_l_rebar, _t_rebar, matRod);
            }
            else if (_bsCalc is BSFiberCalc_IBeamRods)
            {
                BSFiberCalc_IBeamRods _bsCalcRods = (BSFiberCalc_IBeamRods)_bsCalc;

                InitLRebar(out double[] _l_rebar);
                InitTRebar(out double[] _t_rebar);
                               
                _bsCalcRods.GetLTRebar(_l_rebar, _t_rebar, matRod);
            }
        }
        
        /// <summary>
        /// Расчет прочности сечения
        /// </summary>        
        private void btnCalc_Click(object sender, EventArgs e)
        {
            bool useReinforcement = checkBoxRebar.Checked;

            try
            {               
                bsCalc = BSFiberCalculation.construct(m_BeamSection, useReinforcement);
                                
                InitRebar(bsCalc);

                double[] prms = m_BSLoadData.Params;
                InitUserParams(prms);
                                                                    
                bsCalc.GetParams(prms);                                                                                      
                bsCalc.GetSize(BeamSizes());

                InitBeamLength();
                
                bsCalc.Calculate();

                m_PhysParams = bsCalc.PhysParams;
                m_Coeffs = bsCalc.Coeffs;
                m_GeomParams = bsCalc.GeomParams();
                m_CalcResults = bsCalc.Results();
                m_Message =  bsCalc.Msg;

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

                m_PhysParams = bsCalc.PhysicalParameters();

            }
            catch (Exception _e)
            {
                MessageBox.Show("Ошибка в расчете: " + _e.Message);
            }

            try
            {
                if (bsCalc is null)                
                    throw new Exception("Не выполнен расчет");
                
                string pathToHtmlFile = CreateReport(_useReinforcement: useReinforcement);

                System.Diagnostics.Process.Start(pathToHtmlFile);
            }
            catch (Exception _e)
            {
                MessageBox.Show("Ошибка в отчете " + _e.Message);
            }

        }

        /// <summary>
        ///  Сформировать отчет
        /// </summary>
        /// <param name="_reportName">Заголовок</param>
        /// <param name="_useReinforcement">Используется ли арматура</param>
        /// <returns>Путь к файлу отчета</returns>
        private string CreateReport(string _reportName = "", bool _useReinforcement = false)
        {
            try
            {
                //if (bsCalc is null)
                //    throw new Exception("Не выполнен расчет");

                string path = "";
                BSFiberReport report = new BSFiberReport();

                if (_reportName != "")
                    report.ReportName = _reportName;

                report.Beam = m_Beam;
                report.Coeffs = m_Coeffs;
                report.Efforts = m_Efforts;
                report.GeomParams = m_GeomParams;
                report.PhysParams = m_PhysParams;// bsCalc.PhysicalParameters();
                report.BeamSection = m_BeamSection;
                report.CalcResults = m_CalcResults;
                report.Messages = m_Message;
                report.UseReinforcement = _useReinforcement;

                path = report.CreateReport();
                return path;
            }
            catch (Exception _e)
            {
                throw _e;
            }
        }
                    
        // Прямоугольное сечение
        private void LoadRectangle(double _b, double _h)
        {
            m_BeamSection = BeamSection.Rect;

            m_Table = new DataTable();
            m_Table.Columns.Add("b, cm", typeof(double));
            m_Table.Columns.Add("h, cm", typeof(double));

            dataGridView1.DataSource = m_Table;
            m_Table.Rows.Add( _b, _h);

            picBeton.Image = global::BSFiberConcrete.Properties.Resources.FiberBeton;
            picBeton.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        // прямоугольное сечение
        private void btnRectang_Click(object sender, EventArgs e)
        {
            LoadRectangle(m_Iniv["b"], m_Iniv["h"]);
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
            m_Table.Rows.Add(m_Iniv["r1"], m_Iniv["r2"]);

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
        

        private void SelectedFiberBetonValues()
        {
            try
            {
                var b_i = Convert.ToString(cmbFib_i.SelectedItem);                                
                BSFiberBeton beton = (BSFiberBeton)cmbBetonClass.SelectedItem;
                string btName = beton.Name.Replace("i", b_i) ;

                var getQuery = FiberConcrete.Where(f => f.BT == btName);
                if (getQuery?.Count() > 0)
                {
                    var fib = getQuery?.First();

                    numRfbt3n.Value = Convert.ToDecimal(BSHelper.MPA2kgsm2(fib?.Rfbt3n));
                    numRfbn.Value = Convert.ToDecimal(BSHelper.MPA2kgsm2(beton?.Rfbn));

                    numYft.Value = Convert.ToDecimal(fib?.Yft);
                    numYb.Value = Convert.ToDecimal(fib?.Yb);                    
                    numYb1.Value = Convert.ToDecimal(fib?.Yb1);
                    numYb2.Value = Convert.ToDecimal(fib?.Yb2);
                    numYb3.Value = Convert.ToDecimal(fib?.Yb3);
                    numYb5.Value = Convert.ToDecimal(fib?.Yb5);
                }

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

        private void InitEfforts(out Dictionary<string, double> MNQ)
        {
            MNQ = new Dictionary<string, double>();

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
        ///  Введенные пользователем значения по арматуре
        /// </summary>
        /// <param name="_Rebar"></param>
        private void InitRebarValues(ref Rebar _Rebar)
        {
            _Rebar.As = (double) numAs.Value;
            _Rebar.A1s = (double) numAs1.Value;
            _Rebar.a = (double) num_a.Value;
            _Rebar.a1 = (double) num_a1.Value;
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
            
            InitEfforts(out Dictionary<string, double> MNQ);

            fiberCalc.BetonType = BSQuery.BetonTypeFind(comboBetonType.SelectedIndex);

            if (_shear || _useRebar)
            {
                Rebar rebar = m_BSLoadData.Rebar;                                
                InitRebarValues(ref rebar);
                // Армирование
                fiberCalc.Rebar = rebar;

                InitLRebar(out double[] l_r);

                InitTRebar(out double[] t_r);

                fiberCalc.GetRebarParams(l_r, t_r);
            }
            
            double[] prms = m_BSLoadData.Params;

            InitUserParams(prms);

            fiberCalc.GetParams(prms);

            double beamLngth = BSHelper.ToDouble(tbLength.Text);

            double[] sz = BeamSizes(beamLngth);

            fiberCalc.GetSize(sz);

            fiberCalc.GetEfforts(MNQ);

            fiberCalc.GetFiberParamsFromJson(m_BSLoadData.Fiber);
           
            fiberCalc.Calculate();

            fiberCalc.Msg.Add("Расчет успешно выполнен!");

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
        ///  Расчет по наклонному сечению на действие силы N
        ///  (Внецентренное сжатие)
        /// </summary>        
        private void FiberCalculate_N()
        {
            BSFiberCalc_MNQ fiberCalc = new BSFiberCalc_MNQ();

            try
            {
                FiberCalc_MNQ(out fiberCalc, checkBoxRebar.Checked, checkBoxFissure.Checked);
            }
            catch (Exception _ex)
            {
                MessageBox.Show("Ошибка расчета: " + _ex.Message);
            }
            finally
            {
                BSFiberReport_MNQ report = new BSFiberReport_MNQ();
                report.ImageCalc = fiberCalc.ImageCalc();
                report.BeamSection = m_BeamSection;                
                report.Init(fiberCalc);

                string pathToHtmlFile = report.CreateReport();

                System.Diagnostics.Process.Start(pathToHtmlFile);
            }
        }

        /// <summary>
        ///  Расчет по наклонному сечению на действие Q
        /// </summary>        
        private void FiberCalculate_Shear()
        {
            BSFiberCalc_MNQ fiberCalc = new BSFiberCalc_MNQ();

            try
            {
                FiberCalc_MNQ(out fiberCalc, true, false, _shear: true);
            }
            catch (Exception _ex)
            {
                MessageBox.Show("Ошибка расчета: " + _ex.Message);
            }
            finally
            {
                BSFiberReport_MNQ report = new BSFiberReport_MNQ();
                report.BeamSection = m_BeamSection;
                report.ImageCalc = fiberCalc.ImageCalc();
                report.Init(fiberCalc);

                string pathToHtmlFile = report.CreateReport(1);

                System.Diagnostics.Process.Start(pathToHtmlFile);
            }
        }

        // Расчет на внецентренное сжатие
        private void btnCalc_N_Click(object sender, EventArgs e)
        {
            FiberCalculate_N();
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
            l_rebar = new double[10];
            DataGridViewRowCollection rows_l = gridLRebar.Rows;

            for (int irow = 0; irow < 3; irow++)
            {
                var row = rows_l[irow];

                l_rebar = new double[row.Cells.Count];
                for (int i = 0; i < l_rebar.Length; i++)
                {
                    double x = Convert.ToDouble(row.Cells[i].Value);
                    l_rebar[i] = x;
                }
            }
        }


        // Расчет элементов по полосе между наклонными сечениями
        // Расчет на действие момента и поперечной силы
        private void btnCalc_Q_Click(object sender, EventArgs e)
        {
            FiberCalculate_N();

            FiberCalculate_Shear();
        }
        
        private void btnFactors_Click(object sender, EventArgs e)
        {
            BSFactors bsFactors = new BSFactors();
            bsFactors.Show();
        }

        private bool UserInput = true;

        [DisplayName("Расчет по прочности нормальных сечений на основе нелинейной деформационной модели")]
        private void btnCalc_Deform_Click(object sender, EventArgs e)
        {
            var b2 = m_BSLoadData.Beton2;
            var r2 = m_BSLoadData.Rod2;

            string cBtCls = b2.Cls_b;
            double cRb = b2.Rb; // МПа
            double cRs = r2.Rs; // МПа
            double cEb = b2.Eb; // Мпа
            double cEs = r2.Es; // Мпа
            double c_eps_s0 = r2.eps_s0;// 0.00175; // Мпа
            double c_eps_s2 = r2.eps_s2; // 0.025; // Мпа

            double c_eps_b1 = b2.eps_b1;
            double c_eps_b1_red = b2.eps_b1_red;
            double c_eps_b2 = b2.eps_b2;

            double c_h = 60, // см 
                   c_b = 30; // см
           
            double c_L = Convert.ToDouble(tbLength.Text); // см
            string cRCls = r2.Cls_s;
            
            const int c_Y_N = 10; // разбиение по высоте
            const int c_X_M = 1; // разбиение по ширине

            // Mx  , My - усилия, кНм
            double c_Mx = 0; // 45; 
            double c_My = 0;  //95; 
            double c_N = 0;  //0; 
            
            double[] l_r = new double[5]; // параметры продольной арматуры
            double[] t_r = new double[5];  // параметры поперечной арматуры

            // расстановка арматурных стержней
            List<BSRod> rods = new List<BSRod>();

            //использовать  пользовательские данные
            if (UserInput)
            {
                // Усилия                
                InitEfforts(out Dictionary<string, double> MNQ);
                c_Mx = MNQ["Mx"];
                c_My = MNQ["My"];
                c_N = MNQ["N"];

                // Размеры балки
                double[] rect = BeamSizes();
                c_b = rect[0];
                c_h = rect[1];

                // Арматура
                    // продольная
                InitLRebar(out l_r);
                    // поперечная
                InitTRebar(out t_r);
            }

            try
            {
                // Начало координат:
                double X0 = c_b / 2.0;
                double Y0 = c_h / 2.0;

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
                //  пример фибробетон  
                //{ { 40, 80 }, { 300, 80 }, { 40, 120 }, { 300, 120 }, { 40, 640 }, { 300, 640 }, {40, 1115 }, {300, 1115}};
                Action Reinforcement = delegate()
                {
                    // раскладка арматуры X Y:, см
                    double d_r = l_r[0];
                    int d_qty = Convert.ToInt32(l_r[1]);

                    double a_r; // защитный слой арматуры
                    a_r = l_r[2];

                    double[,] rdYdX = new double[d_qty, 2]; //  { { 4, 4 }, { 15, 4 }, { 26, 4 } };
                    double[] rD_lng = new double[d_qty];   //{ 2.5, 1.8, 2.5 }; // D , см
                    double[] _As = new double[d_qty]; // { 4.909, 2.545, 4.909 };

                    // ширина минус защитный слой слева и справа:
                    double bx = c_b - a_r - a_r;
                    // расстояние между стержнями
                    double d_bx = bx / (d_qty - 1);   

                    for (int r_idx = 0; r_idx < d_qty; r_idx++)
                    {
                        rdYdX[r_idx, 0] = a_r + d_bx * r_idx;
                        rdYdX[r_idx, 1] = a_r;

                        rD_lng[r_idx] = d_r;
                        _As[r_idx] = BSHelper.AreaCircle(d_r);
                    }

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
                            Nu = 1.0 // на 1 итерации задаем 1
                        };

                        rods.Add(rod);
                    }
                };

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

                Reinforcement();

                // задать размеры балки,
                // задать материал,
                // присвоить арматуру
                fiberCalc_Deform.Beam = new BSBeam_Rect(c_b, c_h)
                {
                    Length = c_L,
                    Rods = rods,
                    Mat = material
                };

                double[] NM = new double[] { c_Y_N, c_X_M };
                fiberCalc_Deform.GetParams(NM);

                // рассчитать
                fiberCalc_Deform.Calculate();

                m_Efforts = fiberCalc_Deform.Efforts;

                m_PhysParams = fiberCalc_Deform.PhysParams;
                // получить результат
                m_CalcResults = fiberCalc_Deform.Results();
            }
            catch (Exception _ex) 
            {
                MessageBox.Show(_ex.Message);
            }
                        
            try
            {
                InitBeamLength();

                MethodBase method = MethodBase.GetCurrentMethod();
                DisplayNameAttribute attr = (DisplayNameAttribute)method.GetCustomAttributes(typeof(DisplayNameAttribute), true)[0];
                string value = attr.DisplayName; 

                string pathToHtmlFile = CreateReport(value);

                System.Diagnostics.Process.Start(pathToHtmlFile);
            }
            catch (Exception _e)
            {
                MessageBox.Show("Ошибка в отчете " + _e.Message);
            }
        }

        private void gridEfforts_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
                      
        }

        private void btnEffortsRefresh_Click(object sender, EventArgs e)
        {            
            InitEfforts(out Dictionary<string, double> MNQ);

            Efforts ef = new Efforts() {Id = 1, Mx = MNQ["Mx"], My = MNQ["My"], N = MNQ["N"], Q = MNQ["Q"], Ml = MNQ["Ml"], eN = MNQ["eN"] };
            Lib.BSData.SaveEfforts(ef);
        }

                
        private void btnSaveParams_Click(object sender, EventArgs e)
        {
            InitEfforts(out Dictionary<string, double> MNQ);

            Dictionary<string, double> ef = new Dictionary<string, double> { 
                ["Mx"] = MNQ["Mx"], ["My"] = MNQ["My"], ["N"] = MNQ["N"], ["Q"] = MNQ["Q"], ["Ml"] = MNQ["Ml"], ["eN"] = MNQ["eN"] };

            m_BSLoadData.SaveInitToJson(ef);
        }

        private void tabConcrete_Click(object sender, EventArgs e)
        {

        }

        private void cmbBetonClass_SelectedValueChanged(object sender, EventArgs e)
        {
            SelectedFiberBetonValues();
        }

        private void cmbBetonClass_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnCalcResults_Click(object sender, EventArgs e)
        {
            BSCalcResults bSCalcResults = new BSCalcResults();
            bSCalcResults.CalcResults = m_CalcResults;
            bSCalcResults.Show();
        }

        private void labelCalculation_Click(object sender, EventArgs e)
        {

        }
    }
}

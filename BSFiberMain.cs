using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using BSFiberConcrete.Lib;
using System.Diagnostics;
using BSFiberConcrete.Section;
using BSCalcLib;
using System.Drawing;
using TriangleNet.Geometry;
using TriangleNet.Topology;
using MathNet.Numerics.Distributions;
using BSFiberConcrete.DeformationDiagram;
using System.Windows.Forms.DataVisualization.Charting;

namespace BSFiberConcrete
{
    public partial class BSFiberMain : Form
    {
        private DataTable m_Table { get; set; }

        public CalcType CalcType { get; set; }

        //private BetonType
        private Dictionary<string, double> m_Iniv;
        private BSFiberCalculation bsCalc;

        private BSFiberLoadData m_BSLoadData { get; set; }

        private List<Rebar> m_Rebar;
        private BSMatFiber m_MatFiber;
        private List<Elements> FiberConcrete;
        private List<Beton> m_Beton;

        public Dictionary<string, double> m_Beam { get; private set; }
        private Dictionary<string, double> m_Coeffs;
        private Dictionary<string, double> m_Efforts;
        private Dictionary<string, double> m_PhysParams;
        private Dictionary<string, double> m_Reinforcement;
        private Dictionary<string, double> m_GeomParams;
        private Dictionary<string, double> m_CalcResults;

        private List<string> m_Message;

        private BeamSection m_BeamSection { get; set; }
        private BeamSection m_BeamSectionReport { get; set; }

        // Mesh generation
        // площади элементов (треугольников)
        private List<double> triAreas;
        // координаты центра тяжести элементов (треугольников)
        private List<TriangleNet.Geometry.Point> triCGs;
        

        public BSFiberMain()
        {
            InitializeComponent();
        }

        private void CalcTypeShow()
        {
            if (CalcType == CalcType.Static)
            {
                btnStaticEqCalc.Visible = true;
                btnCalc_Deform.Visible = false;
                panelRods.Visible = false;
                gridEfforts.Columns["Mx"].Visible = false;
            }
            else if (CalcType == CalcType.Nonlinear)
            {
                btnStaticEqCalc.Visible = false;
                btnCalc_Deform.Visible = true;
                panelRods.Visible = true;
                gridEfforts.Columns["Mx"].Visible = true;
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
                m_MatFiber = new BSMatFiber();

                flowLayoutPanelRebar.Enabled = true;// (checkBoxRebar.Checked == true);

                FiberConcrete = BSData.LoadFiberConcreteTable();
                cmbFib_i.SelectedIndex = 0;
                comboBetonType.SelectedIndex = 0;
                cmbRebarClass.SelectedIndex = 1;

                m_Iniv = m_BSLoadData.ReadInitFromJson();
                List<Efforts> eff = Lib.BSData.LoadEfforts();
                if (eff.Count > 0)
                {
                    m_Iniv["Mx"] = eff[0].Mx;
                    m_Iniv["My"] = eff[0].My;
                    m_Iniv["N"] = eff[0].N;
                    m_Iniv["Q"] = eff[0].Q;
                }

                num_eN.Value = (decimal)m_Iniv["eN"];
                num_Ml1_M1.Value = (decimal)m_Iniv["Ml"];

                m_BSLoadData.ReadParamsFromJson();
                m_MatFiber.e_b2 = m_BSLoadData.Beton2.eps_b2;
                m_MatFiber.Efb = m_BSLoadData.Fiber.Efb; // TODO на значения с формы

                m_Rebar = BSData.LoadRebar();

                numRandomEccentricity.Value = (decimal)m_BSLoadData.Fiber.e_tot;

                LoadRectangle(m_Iniv["b"], m_Iniv["h"]);
                
                cmbBetonClass.DataSource = BSFiberLib.BetonList;
                cmbBetonClass.DisplayMember = "Name";
                cmbBetonClass.ValueMember = "Name";
                cmbBetonClass.SelectedValue = BSFiberLib.BetonList[5].Name;

                m_Beton = BSData.LoadBetonData();
                cmbBfn.DataSource = m_Beton;
                cmbBfn.DisplayMember = "BT";
                cmbBfn.ValueMember = "BT";
                cmbBfn.SelectedValue = (m_Beton.Count > 7) ? m_Beton[7].BT : ""; // в настройки

                cmbBftn.DataSource = BSData.LoadFiberBft();
                cmbBftn.DisplayMember = "ID";
                cmbBftn.ValueMember = "ID";
                cmbBftn.SelectedValue = "Bft3";

                Elements fiberConcrete = BSFiberLib.PhysElements;

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
                for (int i = 0; i < 3; i++) gridLRebar.Rows.Add(long_rebar);

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

        /// <summary>
        /// Максимальные размеры сечения
        /// </summary>
        /// <param name="_w">максимальная ширина</param>
        /// <param name="_h">максимальная высота</param>
        /// <returns>массив размеров</returns>
        /// <exception cref="Exception"></exception>
        private double[] BeamWidtHeight(out double _w, out double _h, out double _area)
        {
            double[] sz = BeamSizes();

            if (m_BeamSection == BeamSection.Rect)
            {
                _w = sz[0];
                _h = sz[1];
                _area = _w * _h;
            }
            else if (m_BeamSection == BeamSection.Ring)
            {
                _w = Math.Max( sz[0], sz[1]);
                _h = Math.Max(sz[0], sz[1]);
                _area = Math.PI * Math.Pow(Math.Abs(_w - _h), 2)/4.0;
            }            
            else if (m_BeamSection == BeamSection.TBeam || m_BeamSection == BeamSection.IBeam || m_BeamSection == BeamSection.LBeam )            
            {
                _w = Math.Max(sz[0], sz[4]);
                _h = sz[1] + sz[3] + sz[5];
                _area = sz[0] * sz[1] + sz[2] * sz[3] + sz[4] * sz[5];
            }
            else
            {
                throw new Exception("Неопределен тип сечения");
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
                prms[++idx] = Convert.ToDouble(numRfb_n.Value);
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

                InitLRebar(out List<double[]> _l_rebar);
                InitTRebar(out double[] _t_rebar);
                //TODO refactoring
                _bsCalcRods.GetLTRebar(_l_rebar[0], _t_rebar, matRod);
            }
            else if (_bsCalc is BSFiberCalc_IBeamRods)
            {
                BSFiberCalc_IBeamRods _bsCalcRods = (BSFiberCalc_IBeamRods)_bsCalc;

                InitLRebar(out List <double[]> _l_rebar);
                InitTRebar(out double[] _t_rebar);

                //TODO refactoring
                _bsCalcRods.GetLTRebar(_l_rebar[0], _t_rebar, matRod);
            }
        }
        
        // Определение классов фибробетона по данным, введенным пользователем
        private void InitMatFiber()
        {            
            // Сжатие Rfb
            Beton fb = Lib.BSQuery.BetonTableFind(cmbBfn.Text);
            // Растяжение Rfbt
            FiberBft fbt = (FiberBft)cmbBftn.SelectedItem;

            m_BSLoadData.Fiber.Efib = (double) numE_fiber.Value;

            // сжатие:
            m_MatFiber.B = fb.B;
            m_MatFiber.Rfbn = BSHelper.MPA2kgsm2(fb.Rbn);
            // растяжение:            
            m_MatFiber.Rfbtn = BSHelper.MPA2kgsm2(fbt.Rfbtn);
            //остаточное растяжение:            
            m_MatFiber.Rfbt2n = (double)numRfbt2n.Value; // кг/см2
            m_MatFiber.Rfbt3n = (double)numRfbt3n.Value; // кг/см2
            m_MatFiber.Efb = (double)numE_fiber.Value;
        }


        /// <summary>
        /// Расчет прочности сечения на действие момента
        /// </summary>        
        private void FiberCalculate_M(double _M = 0)
        {
            bool useReinforcement = checkBoxRebar.Checked;
            bool calcOk = false;
            try
            {               
                bsCalc = BSFiberCalculation.construct(m_BeamSection, useReinforcement);
                
                bsCalc.MatFiber = m_MatFiber;
                
                InitRebar(bsCalc);

                double[] prms = m_BSLoadData.Params;
                InitUserParams(prms);
                                                                    
                bsCalc.GetParams(prms);                                                                                      
                bsCalc.GetSize(BeamSizes());
                bsCalc.Efforts = new Dictionary<string, double> {{ "My", _M }};

                //InitBeamLength();

                calcOk = bsCalc.Calculate();

                m_PhysParams = bsCalc.PhysParams;
                m_Coeffs = bsCalc.Coeffs;
                m_Efforts = bsCalc.Efforts;
                m_GeomParams = bsCalc.GeomParams();
                m_CalcResults = bsCalc.Results();
                m_Message =  bsCalc.Msg;

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

                if (calcOk)
                {
                    string pathToHtmlFile = CreateReport(1, m_BeamSectionReport, _useReinforcement: useReinforcement);

                    System.Diagnostics.Process.Start(pathToHtmlFile);
                }
                else
                {
                    string errMsg = "";
                    foreach (string ms in m_Message) errMsg += ms + ";\t\n";

                    MessageBox.Show(errMsg);
                }

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
        private string CreateReport(int _fileId ,
                                    BeamSection _BeamSection,
                                    string _reportName = "",                                    
                                    bool _useReinforcement = false)
        {
            try
            {                
                string path = "";
                BSFiberReport report = new BSFiberReport();

                if (_reportName != "")
                    report.ReportName = _reportName;

                report.Beam = m_Beam;
                report.Coeffs = m_Coeffs;
                report.Efforts = m_Efforts;
                report.GeomParams = m_GeomParams;
                report.PhysParams = m_PhysParams;
                report.Reinforcement = m_Reinforcement;
                report.BeamSection = _BeamSection;
                report.CalcResults = m_CalcResults;
                report.Messages = m_Message;
                report.UseReinforcement = _useReinforcement;

                path = report.CreateReport(_fileId);
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
            m_BeamSectionReport = m_BeamSection;

            dataGridSection.DataSource = null;

            m_Table = new DataTable();
            m_Table.Columns.Add("b, cm", typeof(double));
            m_Table.Columns.Add("h, cm", typeof(double));

            dataGridSection.DataSource = m_Table;
            m_Table.Rows.Add( _b, _h);

            foreach (DataGridViewColumn column in dataGridSection.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            picBeton.Image = global::BSFiberConcrete.Properties.Resources.FiberBeton;
            picBeton.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        // прямоугольное сечение
        private void btnRectang_Click(object sender, EventArgs e)
        {
            LoadRectangle(m_Iniv["b"], m_Iniv["h"]);
        }


        private void TSection(char _T_L)
        {
            m_BeamSection = BeamSection.TBeam;
            
            dataGridSection.DataSource = null;

            m_Table = new DataTable();
            m_Table.Columns.Add("bf, cm", typeof(double));
            m_Table.Columns.Add("hf, cm", typeof(double));
            m_Table.Columns.Add("bw, cm", typeof(double));
            m_Table.Columns.Add("hw, cm", typeof(double));
            m_Table.Columns.Add("b1f, cm", typeof(double));
            m_Table.Columns.Add("h1f, cm", typeof(double));

            dataGridSection.DataSource = m_Table;
            
            if (_T_L == 'T')
            {
                m_BeamSectionReport = BeamSection.TBeam;
                m_Table.Rows.Add(0, 0, m_Iniv["bw"], m_Iniv["hw"], m_Iniv["b1f"], m_Iniv["h1f"]);
                dataGridSection.Columns[0].Visible = false;
                dataGridSection.Columns[1].Visible = false;
                picBeton.Image = global::BSFiberConcrete.Properties.Resources.TBeam;
            }
            else if (_T_L == 'L')
            {
                m_BeamSectionReport = BeamSection.LBeam;
                m_Table.Rows.Add(m_Iniv["bf"], m_Iniv["hf"], m_Iniv["bw"], m_Iniv["hw"], 0, 0);
                dataGridSection.Columns[4].Visible = false;
                dataGridSection.Columns[5].Visible = false;
                picBeton.Image = global::BSFiberConcrete.Properties.Resources.LBeam;
            }

            foreach (DataGridViewColumn column in dataGridSection.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            picBeton.SizeMode = PictureBoxSizeMode.StretchImage;
        }



        // тавровое сечение
        // Принимаем как двутавровое, у которого нижняя полка равна по толщине стенке
        private void btnTSection_Click(object sender, EventArgs e)
        {
            TSection('T');
        }

        private void btn_LSection_Click(object sender, EventArgs e)
        {            
            TSection('L');
        }

        // кольцевое сечение
        private void btnRing_Click(object sender, EventArgs e)
        {
            m_BeamSection = BeamSection.Ring;
            m_BeamSectionReport = m_BeamSection;
            dataGridSection.DataSource = null;

            m_Table = new DataTable();
            m_Table.Columns.Add("r1, cm", typeof(double));
            m_Table.Columns.Add("r2, cm", typeof(double));
                        
            dataGridSection.DataSource = m_Table;
            m_Table.Rows.Add(m_Iniv["r1"], m_Iniv["r2"]);

            foreach (DataGridViewColumn column in dataGridSection.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            picBeton.Image = global::BSFiberConcrete.Properties.Resources.Ring;
        }

        // двутавровое сечение
        private void btnIBeam_Click(object sender, EventArgs e)
        {
            m_BeamSection = BeamSection.IBeam;
            m_BeamSectionReport = m_BeamSection;

            dataGridSection.DataSource = null;

            m_Table = new DataTable();
            m_Table.Columns.Add("bf, cm", typeof(double));
            m_Table.Columns.Add("hf, cm", typeof(double));
            m_Table.Columns.Add("bw, cm", typeof(double));
            m_Table.Columns.Add("hw, cm", typeof(double));            
            m_Table.Columns.Add("b1f, cm", typeof(double));
            m_Table.Columns.Add("h1f, cm", typeof(double));

            dataGridSection.DataSource = m_Table;
            m_Table.Rows.Add(m_Iniv["bf"], m_Iniv["hf"], m_Iniv["bw"], m_Iniv["hw"], m_Iniv["b1f"], m_Iniv["h1f"]);

            foreach (DataGridViewColumn column in dataGridSection.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            picBeton.Image = global::BSFiberConcrete.Properties.Resources.IBeam;
            picBeton.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void AboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            BSFiberAboutBox aboutWindow = new BSFiberAboutBox();
            aboutWindow.Show();
        }

        private void setupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BSFiberSetup setupWindow = new BSFiberSetup();
            setupWindow.Show();
        }
        

        /// <summary>
        /// Определить класс по остаточному сопротивлению        
        /// </summary>
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
                    numRfbt2n.Value = Convert.ToDecimal(BSHelper.MPA2kgsm2(fib?.Rfbt2n));

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

        /// <summary>
        ///  Получить данные по усилиям с формы
        /// </summary>
        /// <param name="MNQ"></param>
        /// <exception cref="Exception"></exception>
        private void GetEffortsFromForm(out Dictionary<string, double> MNQ)
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

            MNQ["Ml"] = (double)num_Ml1_M1.Value;
            MNQ["eN"] = (double)num_eN.Value;
            MNQ["e0"] = (double)numRandomEccentricity.Value;

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
        private void FiberCalc_MNQ(out BSFiberCalc_MNQ fiberCalc, bool _useRebar = false, bool _shear = false )
        {            
            fiberCalc = BSFiberCalc_MNQ.Construct(m_BeamSection);
            fiberCalc.UseRebar = _useRebar;            
            fiberCalc.Shear = _shear;

            fiberCalc.InitFiberParams(m_BSLoadData.Fiber);

            fiberCalc.MatFiber = m_MatFiber;
            
            GetEffortsFromForm(out Dictionary<string, double> MNQ);

            fiberCalc.BetonType = BSQuery.BetonTypeFind(comboBetonType.SelectedIndex);
            
            if (_shear || _useRebar)
            {
                Rebar rebar = (Rebar) m_BSLoadData.Rebar.Clone(); // из глобальных параметров
                // настройки из БД
                Rebar dbRebar = m_Rebar.Where(x => x.ID == Convert.ToString(cmbRebarClass.SelectedItem))?.First();
                //  введено пользователем
                InitRebarValues(ref rebar);
                rebar.Es = BSHelper.MPA2kgsm2(dbRebar.Es);

                // Армирование
                fiberCalc.Rebar = rebar;

                InitLRebar(out List<double[]> l_r);

                InitTRebar(out double[] t_r);

                fiberCalc.GetRebarParams(l_r[0], t_r);
            }
            
            double[] prms = m_BSLoadData.Params;
            InitUserParams(prms);
            
            fiberCalc.GetParams(prms); // передаем коэффициенты Yb, Yft, Yb1, Yb2, Yb3, Yb5, B

            double beamLngth = BSHelper.ToDouble(tbLength.Text);

            double[] sz = BeamSizes(beamLngth);

            fiberCalc.GetSize(sz);

            // передаем усилия и связанные с ними велечины
            double e_tot = fiberCalc.GetEfforts(MNQ); 

            bool _N_out = false;
            if (fiberCalc.h /2 < e_tot) _N_out = true;
            fiberCalc.N_Out = _N_out;

            fiberCalc.Calculate();

            fiberCalc.Msg.Add("Расчет успешно выполнен!");

            m_CalcResults = fiberCalc.Results();                        
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
                FiberCalc_MNQ(out fiberCalc, checkBoxRebar.Checked );
            }
            catch (Exception _ex)
            {
                MessageBox.Show("Ошибка расчета: " + _ex.Message);
            }
            finally
            {
                BSFiberReport_N report = new BSFiberReport_N();                
                report.ImageCalc = fiberCalc.ImageCalc();
                report.BeamSection = m_BeamSection;                
                report.InitFromFiberCalc(fiberCalc);

                string pathToHtmlFile = report.CreateReport(2);

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
                FiberCalc_MNQ(out fiberCalc, true, _shear: true);
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
                report.InitFromFiberCalc(fiberCalc);

                string pathToHtmlFile = report.CreateReport(3);

                System.Diagnostics.Process.Start(pathToHtmlFile);
            }
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
        private void InitLRebar(out List<double[]> _Rods_rebar)
        {
            double[]  l_rebar = new double[10];

            _Rods_rebar = new List<double[]>();

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

                _Rods_rebar.Add(l_rebar);
            }
        }


        // Расчет элементов по полосе между наклонными сечениями
        // Расчет на действие момента и поперечной силы
        private void btnStaticEqCalc_Click(object sender, EventArgs e)
        {
            // Данные, введенные пользователем
            InitMatFiber();

            GetEffortsFromForm(out Dictionary<string, double> MNQ);

            (double _M, double _N, double _Q) = (MNQ["My"], MNQ["N"], MNQ["Q"]);

            if (_M < 0 || _N < 0)
            {
                MessageBox.Show("Расчет по методу статического равновесия не реализован для отрицательных значений M и N.\n Воспользуйтесь расчетом по методу НДМ");

                return;
            }

            if (_M != 0 && _N == 0)
            {
                FiberCalculate_M(_M);
            }
            else if (_N != 0)
            {
                FiberCalculate_N();
            }

            if (_Q != 0)
            {
                FiberCalculate_Shear();
            }
        }
        
        private void btnFactors_Click(object sender, EventArgs e)
        {
            BSFactors bsFactors = new BSFactors();
            bsFactors.Show();
        }

                    
        [DisplayName("Расчет по прочности нормальных сечений на основе нелинейной деформационной модели")]
        private void btnCalc_Deform_Click(object sender, EventArgs e)
        {
            // центр тяжести сечения
            TriangleNet.Geometry.Point CG = new TriangleNet.Geometry.Point(0.0, 0.0);

            int deformDiagram = cmbDeformDiagram.SelectedIndex;            
            BSMatFiber beamMaterial;                                               
                        
            // класс фибробетона (бетона) на сжатие
            string cBt_class = cmbBfn.Text;
            // Фибробетон:
            double cRb = (double)numRfb_n.Value; // сопротивление сжатию, кгс/см2
            double cEb = (double)numEfb.Value; // модуль упругости,  кгс/см2

            // диаграмма:
            // арматура
            string cR_class = cmbRebarClass.Text;
            double cRs = (double)numRs.Value; // кгс/см2            
            double cEs = (double)numEs.Value; // кгс/см2
            double c_eps_s0 = (double)numEpsilonS0.Value;// 0.00175; 
            double c_eps_s2 = (double)numEpsilonS2.Value; ; // 0.025; 

            //бетон
            double c_eps_b1 =  (double)numEps_fb0.Value;
            double c_eps_b1_red = (double)numEps_fb0.Value; // уточнить
            double c_eps_b2 = (double)numEps_fb2.Value;
               
            // длина балки, см 
            double c_Length = Convert.ToDouble(tbLength.Text); 
                                                                        
            // расстановка арматурных стержней
            List<BSRod> Rods = new List<BSRod>();

            // Усилия Mx, My - моменты, кгс*см , N - сила, кгс              
            GetEffortsFromForm(out Dictionary<string, double> MNQ);
            double c_Mx = MNQ["Mx"];
            double c_My = MNQ["My"];
            double c_N = MNQ["N"];

            // сечение балки балки, см 
            double[] beam_sizes = BeamSizes(c_Length);
            
            var bsBeam = BSBeam.construct(m_BeamSectionReport);
            bsBeam.GetSizes(beam_sizes);
            bsBeam.Length = c_Length;

            double c_b = bsBeam.Width; 
            double c_h = bsBeam.Height;             
            m_GeomParams = new Dictionary<string, double> { { "b, см", c_b }, { "h, см", c_h }};

            //смещение начала координат            
            double dX0, dY0;           
            
            // координаты ц.т. сечения, если т. 0 - левый нижний угол
            (CG.X, CG.Y) = bsBeam.CG();
            
            try
            {                
                BSFiberCalc_Deform fiberCalc_Deform = new BSFiberCalc_Deform(_Mx: c_Mx, _My: c_My, _N: c_N);
                fiberCalc_Deform.DeformDiagram =  deformDiagram;
                fiberCalc_Deform.Beam = bsBeam;
                // 
                GenerateMesh(ref CG); // покрыть сечение сеткой
                //
                fiberCalc_Deform.CG = CG;
                fiberCalc_Deform.triAreas = triAreas;
                fiberCalc_Deform.triCGs = triCGs;

                // Начало координат:
                dX0 = CG.X;
                dY0 = CG.Y;

                // задать тип арматуры
                fiberCalc_Deform.MatRebar = new BSMatRod(cEs)
                {
                    RCls = cR_class,
                    Rs = cRs,
                    e_s0 = c_eps_s0,
                    e_s2 = c_eps_s2
                };

                m_Reinforcement = new Dictionary<string, double>();

                ///
                /// расстановка арматурных стержней                
                /// 
                Action RodsReinforcement = delegate()
                {
                    // значения из БД
                    var _rods = BSData.LoadBSRod(m_BeamSection);

                    // количество стержней
                    int d_qty = _rods.Count; 

                    // площадь арматуры
                    double area_total = 0;
                    foreach (var lr in _rods)
                    {
                        area_total += BSHelper.AreaCircle(lr.D); 
                    }
                                        
                    int idx = 0; // Индекс стержня
                    foreach (var lr in _rods)
                    {                                                                                                                                          
                        BSRod rod = new BSRod()
                        {
                            Id = idx,
                            LTType = RebarLTType.Longitudinal,
                            D = lr.D,
                            CG_X = -(lr.CG_X),
                            CG_Y = -(lr.CG_Y - dY0),
                            MatRod = fiberCalc_Deform.MatRebar,
                            Nu = 1.0 // на первой итерации задаем 1
                        };

                        idx++;
                        Rods.Add(rod);                        
                    }

                    fiberCalc_Deform.Rods = Rods;


                    m_Reinforcement.Add("Количество стержней, шт", d_qty);
                    m_Reinforcement.Add("Площадь арматуры, см2", area_total);
                };

                beamMaterial = new BSMatFiber(cEb, numYft.Value, numYb.Value, numYb1.Value, numYb2.Value, numYb3.Value, numYb5.Value)
                {
                    BTCls = cBt_class,
                    Nu_fb = 1,
                    Rfbn = cRb,
                    Rfbtn = (double) numRfbt_n.Value,
                    Rfbt2n = (double)numRfbt2n.Value,
                    Rfbt3n = (double)numRfbt3n.Value,
                    e_b1_red = c_eps_b1_red,
                    e_b1 = c_eps_b1,
                    e_b2 = c_eps_b2,
                    Eps_fb_ult = 0.002,
                    Eps_fbt_ult = 0.0001,                    
                };

                // задать свойства бетона
                fiberCalc_Deform.MatFiber = beamMaterial;

                RodsReinforcement();
                                
                // арматура балки                                                
                fiberCalc_Deform.Beam.Rods = Rods;
                // материал
                fiberCalc_Deform.Beam.Mat = beamMaterial;                                
                fiberCalc_Deform.GetParams(new double[] { 10, 1});

                // рассчитать
                fiberCalc_Deform.Calculate();
                //
                m_Efforts = fiberCalc_Deform.Efforts;

                m_PhysParams = fiberCalc_Deform.PhysParams;

                //m_Reinforcement = fiberCalc_Deform.Reinforcement;

                // получить результат
                m_CalcResults = fiberCalc_Deform.Results();

                m_Message = fiberCalc_Deform.Msg;

                if (m_CalcResults?.Count > 0)
                    fiberCalc_Deform.Msg.Add("Расчет успешно выполнен!");
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

                string pathToHtmlFile = CreateReport(1, m_BeamSectionReport,  value);

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

        // сохранить усилия
        private void btnEffortsRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                GetEffortsFromForm(out Dictionary<string, double> MNQ);

                Efforts ef = new Efforts() { Id = 1, Mx = MNQ["Mx"], My = MNQ["My"], N = MNQ["N"], Q = MNQ["Q"], Ml = MNQ["Ml"], eN = MNQ["eN"] };
                Lib.BSData.SaveEfforts(ef);
            }
            catch (Exception _ex)
            {
                MessageBox.Show(_ex.Message);
            }
        }


        // сохранить геометрические размеры
        private void btnSaveParams_Click(object sender, EventArgs e)
        {
            try
            {
                Dictionary<string, double> SZ = new Dictionary<string, double>();
                double[] sz = BeamSizes();
                Dictionary<string, double> ef = null;

                if (m_BeamSection == BeamSection.Rect)
                {
                    ef = new Dictionary<string, double> {
                        ["b"] = sz[0], ["h"] = sz[1]                        
                    };
                }
                else if (m_BeamSection == BeamSection.IBeam || m_BeamSection == BeamSection.TBeam || m_BeamSection == BeamSection.LBeam)
                {
                    ef = new Dictionary<string, double> {
                        ["bf"] = sz[0], ["hf"] = sz[1], ["bw"] = sz[2], ["hw"] = sz[3], ["b1f"] = sz[4], ["h1f"] = sz[5]
                    };
                }
                else if (m_BeamSection == BeamSection.Ring)
                {
                    ef = new Dictionary<string, double> {
                        ["r1"] = sz[0], ["r2"] = sz[1]
                    };
                }

                if (ef != null)
                    m_BSLoadData.SaveInitSectionsToJson(ef);
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);                
            }
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

        private void cmbBftn_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FiberBft bft = (FiberBft)cmbBftn.SelectedItem;
                numRfbt_n.Value = (decimal) BSHelper.MPA2kgsm2(bft.Rfbtn); // Convert
            }
            catch { }
        }

        private void cmbBfn_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Beton bt = Lib.BSQuery.BetonTableFind(cmbBfn.Text);
                numRfb_n.Value = (decimal)BSHelper.MPA2kgsm2(bt.Rbn);
            }
            catch { }
        }

        private void cmbRebarClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var rb = Lib.BSQuery.RebarFind(cmbRebarClass.Text);
                numRs.Value = (decimal)BSHelper.MPA2kgsm2(rb.Rs);
            }
            catch { }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void paramsToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void cmbTRebarClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var trb = Lib.BSQuery.RebarFind(cmbTRebarClass.Text);
                numRsw.Value = (decimal)BSHelper.MPA2kgsm2(trb.Rsw);
            }
            catch { }
        }

        private void numRfbt_n_ValueChanged(object sender, EventArgs e)
        {
            labelRfbtnMPa.Text = string.Format("{0} МПа ", BSHelper.Kgsm2MPa((double)numRfbt_n.Value)); 
        }

        private void numRfb_n_ValueChanged(object sender, EventArgs e)
        {
            labelRfbnMPa.Text = string.Format("{0} МПа ", BSHelper.Kgsm2MPa((double)numRfb_n.Value));
        }

        private void numRfbt2n_ValueChanged(object sender, EventArgs e)
        {
            labelRfbt2nMPa.Text = string.Format("{0} МПа ", BSHelper.Kgsm2MPa((double)numRfbt2n.Value));
        }

        private void numRfbt3n_ValueChanged(object sender, EventArgs e)
        {
            labelRfbt3nMPa.Text = string.Format("{0} МПа ", BSHelper.Kgsm2MPa((double)numRfbt3n.Value));
        }

        private void numRs_ValueChanged(object sender, EventArgs e)
        {
            labelRsMPa.Text = string.Format("{0} МПа ", BSHelper.Kgsm2MPa((double)numRs.Value));
        }

        private void numRsw_ValueChanged(object sender, EventArgs e)
        {
            labelRswMPa.Text = string.Format("{0} МПа ", BSHelper.Kgsm2MPa((double)numRsw.Value));
        }

        private void checkBoxRebar_CheckedChanged(object sender, EventArgs e)
        {
            flowLayoutPanelRebar.Enabled = (checkBoxRebar.Checked == true);
        }

        /// <summary>
        ///  Конструктор сечений
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSection_Click(object sender, EventArgs e)
        {
            BSSectionChart sectionChart = new BSSectionChart();
            sectionChart.m_BeamSection = m_BeamSectionReport;

            var sz = BeamWidtHeight(out double b, out double h, out double _area);

            sectionChart.Wdth = (float)b;
            sectionChart.Hght = (float)h;            
            sectionChart.Sz = sz;
            sectionChart.NumArea = _area;

            sectionChart.Show();
        }

        // <summary>
        /// Покрыть сечение сеткой
        /// </summary>
        private string GenerateMesh(ref TriangleNet.Geometry.Point _CG)
        {            
            string pathToSvgFile = "";
            double[] sz = BeamWidtHeight(out double b, out double h, out double area);

            double meshSize = (double)numMeshN.Value;

            BSMesh.Nx = (int)meshSize;
            BSMesh.Ny = (int)meshSize;

            BSMesh.MinAngle = (double) numTriAngle.Value;
            Tri.MinAngle = (double)numTriAngle.Value;

            if (meshSize > 0)
                Tri.MaxArea = area / meshSize ;    

            BSMesh.FilePath = Path.Combine(Environment.CurrentDirectory, "Templates");
            Tri.FilePath = BSMesh.FilePath;
            BeamSection T = BeamSection.TBeam | BeamSection.IBeam | BeamSection.LBeam;

            if (m_BeamSection == BeamSection.Rect)
            {
                List<double> rect = new List<double> { 0, 0, b, h };
                //_CG = new TriangleNet.Geometry.Point(0.0, h/2.0);                
                pathToSvgFile = BSCalcLib.BSMesh.GenerateRectangle(rect);
                Tri.Mesh = BSMesh.Mesh;
                // сместить начало координат из левого нижнего угла в центр тяжести
                Tri.Oxy = _CG; 
                var xxTr = Tri.CalculationScheme();
            }
            else if (m_BeamSection == BeamSection.Ring)
            {
                _CG = new TriangleNet.Geometry.Point(0, 0);
                
                double R = sz[1];
                double r = sz[0];
                if (r > R)
                    throw BSBeam_Ring.RadiiError();

                BSMesh.Center = _CG;
                pathToSvgFile = BSMesh.GenerateRing(R, r, true);

                Tri.Mesh = BSMesh.Mesh;
                var xxTr =  Tri.CalculationScheme();
            }
            else if (T.HasFlag(m_BeamSection))
            {
                List<PointF> pts;
                BSSection.IBeam(sz, out pts, out PointF _center);
                _CG = new TriangleNet.Geometry.Point(_center.X, _center.Y);
                
                pathToSvgFile = BSCalcLib.Tri.CreateIBeamContour(pts);
                var xxTr = Tri.CalculationScheme();                
            }            
            else
            {
                throw new Exception("Не задано сечение");
            }

            triAreas = Tri.triAreas; // площади треугольников
            triCGs = Tri.triCGs; // ц.т. треугольников

            return pathToSvgFile;
        }

        /// <summary>
        /// Покрыть сечение сеткой
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMesh_Click(object sender, EventArgs e)
        {
            // центр тяжести сечения            
            try
            {
                string pathToSvgFile = "";

                TriangleNet.Geometry.Point cg = new TriangleNet.Geometry.Point();
                pathToSvgFile = GenerateMesh(ref cg);
               
                Process.Start(new ProcessStartInfo { FileName = pathToSvgFile, UseShellExecute = true });

            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
        }

        private void toolBSFiberParams_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Path.Combine(Environment.CurrentDirectory, "Templates\\BSFiberParams.json");
                Process.Start(@"notepad.exe", path);
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
        }

        private void toolsBSInit_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Path.Combine(Environment.CurrentDirectory, "Templates\\BSInit.json");
                Process.Start(@"notepad.exe", path);
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
        }

        private void dataGridSection_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void dataGridSection_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnCalcDeformDiagram_Click(object sender, EventArgs e)
        {
            string typeDiagram = cmbDeformDiagram.Text;
            string typeMaterial = cmbTypeMaterial.Text;

            double eb0 = (double)numEps_fb0.Value;
            double eb2 = (double)numEps_fb2.Value;
            //double eb0 = (double)0.003m;
            //double eb2 = (double)0.0042m;

            double Eb = (double)numEfb.Value;
            double Efb = Eb;                    // !!!

            double Rb_n = (double)numRfb_n.Value;
            double Rfbt_n = (double)numRfbt_n.Value;
            double Rfbt2_n = (double)numRfbt3n.Value;
            double Rfbt3_n = (double)numRfbt2n.Value;

            double efbt2 = (double)numEps_fbt2.Value;
            double efbt3 = (double)numEps_fbt3.Value;

            DataForDeformDiagram.typesDiagram  = new string[] { typeMaterial, typeDiagram };
            DataForDeformDiagram.resists = new double[] { Rb_n, Rfbt_n, Rfbt2_n, Rfbt3_n };
            DataForDeformDiagram.deforms = new double[] { eb0, eb2, efbt2, efbt3 }; 
            DataForDeformDiagram.E = new double[] { Eb, Efb };

            DeformDiagram deformDiagram = new DeformDiagram();
            deformDiagram.Show();
        }

        private void cmbWetAir_SelectedIndexChanged(object sender, EventArgs e)
        {
            string title = cmbWetAir.Text;
            if (title == BSHelper.IgnoreHumidity)
            {
                numEps_fb0.Enabled = true;
                numEps_fb2.Enabled = true;
            }
            else
            {
                numEps_fb0.Enabled = false;
                numEps_fb2.Enabled = false;
                List<EpsilonFromAirHumidity> e_DB = BSData.LoadBetonEpsilonFromAirHumidity();
                foreach (EpsilonFromAirHumidity rowEps in e_DB)
                {
                    if (title == rowEps.AirHumidityStr)
                    {
                        numEps_fb0.Value = (decimal)rowEps.Eps_b0;
                        numEps_fb2.Value = (decimal)rowEps.Eps_b2;
                        break;
                    }
                }
            }
        }

        private void numEs_ValueChanged(object sender, EventArgs e)
        {
            labelEsMPa.Text = string.Format("{0} МПа ", BSHelper.Kgsm2MPa((double)numEs.Value));
        }
    }
}

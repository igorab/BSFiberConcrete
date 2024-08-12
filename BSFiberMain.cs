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
using BSFiberConcrete.DeformationDiagram;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using BSFiberConcrete.Control;
using BSBeamCalculator;
using BSFiberConcrete.CalcGroup2;
using MathNet.Numerics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Runtime.Remoting.Messaging;

namespace BSFiberConcrete
{
    public partial class BSFiberMain : Form
    {
        private DataTable m_Table { get; set; }
        public CalcType CalcType { get; set; }

        private Dictionary<string, double> m_Iniv;
        private BSFiberCalculation bsCalc;
        private BSFiberLoadData m_BSLoadData { get; set; }
        private List<Rebar> m_Rebar;
        private BSMatFiber m_MatFiber;
        private List<Elements> FiberConcrete;
        private List<Beton> m_Beton;
        private List<RebarDiameters> m_RebarDiameters;
        // Список, в котором хранится актуальные данные геометрии сечений
        private List<InitBeamSectionGeometry> m_InitBeamSectionsGeometry;
        public Dictionary<string, double> m_Beam { get; private set; }
        /// <summary>
        /// Коэффициенты надежности 
        /// </summary>
        private Dictionary<string, double> m_Coeffs;
        /// <summary>
        /// Нагрузки на балки
        /// </summary>
        private Dictionary<string, double> m_Efforts;
        /// <summary>
        /// Параметры ФИбробетона
        /// </summary>
        private Dictionary<string, double> m_PhysParams;
        private Dictionary<string, double> m_Reinforcement;
        /// <summary>
        /// Габаритные размеры сечения, и доп хар-ки
        /// </summary>
        private Dictionary<string, double> m_GeomParams;
        private Dictionary<string, double> m_CalcResults;
        private Dictionary<string, double> m_CalcResults2Group;
        /// <summary>
        /// Содержит путь до картинки с эпюрой
        /// </summary>
        private List<string> m_Path2BeamDiagrams;
        /// <summary>
        /// Статусы расчета, отражаемые в отчете
        /// </summary>
        private List<string> m_Message;

        private BeamSection m_BeamSection { get; set; }

        // Mesh generation
        // площади элементов (треугольников)
        private List<double> triAreas;

        // координаты центра тяжести элементов (треугольников)
        private List<TriangleNet.Geometry.Point> triCGs;

        public BSFiberMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Дизайн формы в зависимости от вида расчета
        /// </summary>
        private void CalcTypeShow()
        {
            if (CalcType == CalcType.Static)
            {
                btnStaticEqCalc.Visible = true;
                btnCalc_Deform.Visible = false;
                gridEfforts.Columns["Mx"].Visible = false;
                tabFiber.TabPages.Remove(tabPageNDM);
                tabFiber.TabPages.Remove(tabPBeam);
            }
            else if (CalcType == CalcType.Nonlinear)
            {
                btnStaticEqCalc.Visible = false;
                btnCalc_Deform.Visible = true;
                gridEfforts.Columns["Mx"].Visible = true;
                tabFiber.TabPages.Remove(tabPBeam);
            }
            else if (CalcType == CalcType.BeamCalc)
            {
                tabFiber.TabPages.Remove(tabStrength);

                btnStaticEqCalc.Visible = false;
                btnCalc_Deform.Visible = true;

                //gridEfforts
                //BeamCalculatorControl beamCalculatorControl = new BeamCalculatorControl(test_Efforts);

                tbLength.Enabled = false;
                for (int i = 0; i < gridEfforts.ColumnCount; i++)
                {
                    gridEfforts[i, 0].Value = "0";
                }
                m_Path2BeamDiagrams = new List<string>() { };
                BeamCalculatorControl beamCalculatorControl = new BeamCalculatorControl(tbLength, gridEfforts, m_Path2BeamDiagrams);
                tabPBeam.Controls.Add(beamCalculatorControl);
            }
        }

        // глобальные настройки
        private void BSFiberMain_Load(object sender, EventArgs e)
        {
            try
            {
                m_Path2BeamDiagrams = new List<string>() { };


                m_RebarDiameters = BSData.LoadRebarDiameters();
                
                m_Beam = new Dictionary<string, double>();
                m_Table = new DataTable();
                m_Rebar = BSData.LoadRebar();

                dataGridSection.DataSource = m_Table;

                m_BSLoadData = new BSFiberLoadData();
                m_MatFiber = new BSMatFiber();

                flowLayoutPanelRebar.Enabled = true;

                FiberConcrete = BSData.LoadFiberConcreteTable();
                cmbFib_i.SelectedIndex = 0;
                comboBetonType.SelectedIndex = 0;
                cmbRebarClass.SelectedIndex = 1;
                cmbDeformDiagram.SelectedIndex = (int)DeformDiagramType.D3Linear;

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
                m_MatFiber.Efb = m_BSLoadData.Fiber.Efb; // TODO источником должно быть значение с формы.

                m_InitBeamSectionsGeometry = Lib.BSData.LoadBeamSectionGeometry(m_BeamSection);
                numRandomEccentricity.Value = (decimal)m_BSLoadData.Fiber.e_tot;

                LoadRectangle();

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

                //Mx My N Q Ml
                double[] mnq = { 
                    m_Iniv["Mx"], m_Iniv["My"], m_Iniv["N"], m_Iniv["Q"], m_Iniv["Ml"], m_Iniv["eN"] 
                }; 

                gridEfforts.Rows.Add(mnq);
                for (int i = 0; i < mnq.Length; i++)
                {
                    gridEfforts.Rows[0].Cells[i].Value = mnq[i];
                }

                //
                // настройки из БД
                Rebar dbRebar = m_Rebar.Where(x => x.ID == Convert.ToString(cmbRebarClass.SelectedItem))?.First();
                numEs.Value = (decimal)BSHelper.MPA2kgsm2(dbRebar.Es);                
                numAs.Value = (decimal)dbRebar.As;
                numAs1.Value = (decimal)dbRebar.As1;
                num_a.Value = (decimal)dbRebar.a;
                num_a1.Value = (decimal)dbRebar.a1;

                numEps_fb2.Value = 0.0042M;

                CalcTypeShow();
            }
            catch (Exception _ex)
            {
                MessageBox.Show(_ex.Message);
            }
        }

        /// <summary>
        ///  размеры балки (поперечное сечение + длина)
        /// </summary>
        /// <param name="_length">Длина балки </param>
        /// <returns>массив размеров </returns>
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

            double bf = 0, hf = 0, bw, hw, b1f = 0, h1f = 0;
            if (m_BeamSection == BeamSection.TBeam)
            {
                bw = sz[0]; hw = sz[1]; b1f = sz[2]; h1f = sz[3];
                sz = new double[] { bf, hf, bw, hw, b1f, h1f, _length };
            }
            else if (m_BeamSection == BeamSection.LBeam)
            {
                bf = sz[0]; hf = sz[1]; bw = sz[2]; hw = sz[3];
                sz = new double[] { bf, hf, bw, hw, b1f, h1f, _length };
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
                _w = Math.Max(sz[0], sz[1]);
                _h = Math.Max(sz[0], sz[1]);
                _area = Math.PI * Math.Pow(Math.Abs(sz[1] - sz[0]), 2) / 4.0;
            }
            else if (m_BeamSection == BeamSection.TBeam || m_BeamSection == BeamSection.IBeam || m_BeamSection == BeamSection.LBeam)
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
            double[] matRod = new double[]
            {
                (double) numRs.Value,
                (double) numRsc.Value,
                (double) numAs.Value,
                (double) numAs1.Value,
                (double) numEs.Value,
                (double) num_a.Value,
                (double) num_a1.Value
            };

            if (_bsCalc is BSFiberCalc_RectRods)
            {
                BSFiberCalc_RectRods _bsCalcRods = (BSFiberCalc_RectRods)_bsCalc;

                InitTRebar(out double[] _t_rebar);

                //TODO refactoring
                _bsCalcRods.SetLTRebar(matRod);
            }
            else if (_bsCalc is BSFiberCalc_IBeamRods)
            {
                BSFiberCalc_IBeamRods _bsCalcRods = (BSFiberCalc_IBeamRods)_bsCalc;

                InitTRebar(out double[] _t_rebar);

                //TODO refactoring
                _bsCalcRods.GetLTRebar(matRod);
            }
        }

        // Определение классов фибробетона по данным, введенным пользователем
        private void InitMatFiber()
        {
            // Сжатие Rfb
            Beton fb = Lib.BSQuery.BetonTableFind(cmbBfn.Text);
            // Растяжение Rfbt
            FiberBft fbt = (FiberBft)cmbBftn.SelectedItem;

            m_BSLoadData.Fiber.Efib = (double)numE_fiber.Value;

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
                bsCalc.Efforts = new Dictionary<string, double> { { "My", _M } };

                calcOk = bsCalc.Calculate();

                m_PhysParams = bsCalc.PhysParams;
                m_Coeffs = bsCalc.Coeffs;
                m_Efforts = bsCalc.Efforts;
                m_GeomParams = bsCalc.GeomParams();
                m_CalcResults = bsCalc.Results();
                m_Message = bsCalc.Msg;
                //TODO need refactoring - параметры с описанием
                m_PhysParams = bsCalc.PhysicalParameters();

                // запуск расчет по второй группе предельных состояний
                FiberCalculate_Cracking();
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
                    string pathToHtmlFile = CreateReport(1, m_BeamSection, _useReinforcement: useReinforcement);

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
        /// Расчеты стельфибробетона по предельным состояниям второй группы
        /// 1) Расчет предельного момента образования трещин
        /// 2) Расчет ширины раскрытия трещины
        /// </summary>        
        private void FiberCalculate_Cracking()
        {
            bool calcOk = false;
            try
            {

                // диаграмма:
                // арматура
                string cR_class = cmbRebarClass.Text;
                double cRs = (double)numRs.Value; // кгс/см2            
                double cEs = (double)numEs.Value; // кгс/см2
                double c_eps_s0 = (double)numEpsilonS0.Value;// 0.00175; 
                double c_eps_s2 = (double)numEpsilonS2.Value; ; // 0.025; 

                double c_As = (double)numAs.Value;
                double c_As1 = (double)numAs1.Value;
                double c_a_s = (double)num_a.Value;
                double c_a_s1 = (double)num_a1.Value;

                bool reinforcement = checkBoxRebar.Checked;



                // длина балки, см 
                double c_Length = Convert.ToDouble(tbLength.Text);
                // сечение балки балки, см 
                double[] beam_sizes = BeamSizes(c_Length);

                var bsBeam = BSBeam.construct(m_BeamSection);
                bsBeam.SetSizes(beam_sizes);
                bsBeam.Length = c_Length;

                double c_b = bsBeam.Width;
                double c_h = bsBeam.Height;


                //m_GeomParams = new Dictionary<string, double> { { "b, см", c_b }, { "h, см", c_h } };
                //m_Reinforcement = new Dictionary<string, double>();



                // Усилия Mx, My - моменты, кгс*см , N - сила, кгс              
                GetEffortsFromForm(out Dictionary<string, double> MNQ);

                BSFiberCalc_Cracking calc_Cracking = new BSFiberCalc_Cracking(MNQ);
                //calc_Cracking.Efforts = MNQ;
                calc_Cracking.Beam = bsBeam;



                //calc_Cracking.DeformDiagram = deformDiagramType;
                //calc_Cracking.DeformMaterialType = deformMaterialType;
                //calc_Cracking.Beam = bsBeam;

                calc_Cracking.typeOfBeamSection = m_BeamSection;

                // задать тип арматуры
                calc_Cracking.MatRebar = new BSMatRod(cEs)
                {
                    RCls = cR_class,
                    Rs = cRs,
                    e_s0 = c_eps_s0,
                    e_s2 = c_eps_s2,
                    As = c_As,
                    As1 = c_As1,
                    a_s = c_a_s,
                    a_s1 = c_a_s1,
                    Reinforcement = reinforcement
                };


                InitMatFiber();
                InitBeamLength();
                calc_Cracking.MatFiber = m_MatFiber;
                calc_Cracking.GetParams(new double[] { 10, 1 });

                // рассчитать 
                calcOk = calc_Cracking.Calculate();
                //calcOk = calc_Cracking.CalculateUltM();
                //calcOk = calc_Cracking.CalculateWidthCrack();


                //calc_Cracking.ShowResult();



                //m_PhysParams = calc_Cracking.PhysParams;    // хардкодом прописанные параметры фибробетона
                //m_Coeffs = calc_Cracking.Coeffs;          // коэффициенты надежности 
                //m_Efforts = calc_Cracking.Efforts;          // нагрузки 
                //m_GeomParams = bsBeam.GetDimension();       // Геометрия сечения
                //m_CalcResults = calc_Cracking.Results();
                //m_Message = calc_Cracking.Msg;


                //m_Coeffs = bsCalc.Coeffs;
                //m_Efforts = bsCalc.Efforts;
                //m_GeomParams = bsCalc.GeomParams();
                if (m_Message == null)
                { m_Message = new List<string>(); }
                m_Message.AddRange(calc_Cracking.Msg);

                m_CalcResults2Group = calc_Cracking.Results();
                


            }
            catch (Exception _e)
            {
                MessageBox.Show("Ошибка в расчете: " + _e.Message);
            }

            //try
            //{
            //    //if (bsCalc is null)
            //    //    throw new Exception("Не выполнен расчет");
            //if (calcOk)
            //{
            //    string reportName = "Расчет по обрзованию и раскрытию трещин";
            //    string pathToHtmlFile = CreateReport(3, m_BeamSection, reportName);
            //    System.Diagnostics.Process.Start(pathToHtmlFile);
            //}
            //else
            //{
            //    string errMsg = "";
            //    foreach (string ms in m_Message) errMsg += ms + ";\t\n";

            //    MessageBox.Show(errMsg);
            //}
            //}
            //catch (Exception _e)
            //{ MessageBox.Show("Ошибка в отчете " + _e.Message); }

        }


        /// <summary>
        ///  Сформировать отчет
        /// </summary>
        /// <param name="_reportName">Заголовок</param>
        /// <param name="_useReinforcement">Используется ли арматура</param>
        /// <returns>Путь к файлу отчета</returns>
        private string CreateReport(int _fileId,
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
                report.CalcResults2Group = m_CalcResults2Group;
                report.Messages = m_Message;
                report.UseReinforcement = _useReinforcement;
                report.Path2BeamDiagrams = m_Path2BeamDiagrams; 

                path = report.CreateReport(_fileId);
                return path;
            }
            catch (Exception _e)
            {
                throw _e;
            }
        }

        // Прямоугольное сечение
        private void LoadRectangle()
        {
            m_BeamSection = BeamSection.Rect;
            dataGridSection.DataSource = null;

            m_Table = FiberMainFormHelper.GetTableFromBeamSections(m_InitBeamSectionsGeometry, m_BeamSection);
            dataGridSection.DataSource = m_Table;

            foreach (DataGridViewColumn column in dataGridSection.Columns)
            { column.SortMode = DataGridViewColumnSortMode.NotSortable; }

            picBeton.Image = global::BSFiberConcrete.Properties.Resources.FiberBeton;
            picBeton.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        // прямоугольное сечение
        private void btnRectang_Click(object sender, EventArgs e)
        {
            LoadRectangle();
        }

        /// <summary>
        /// Тавровое сечение
        /// </summary>
        /// <param name="_T_L">Тип полки</param>
        private void TSection(char _T_L)
        {
            // TODO доработать использование переменных m_BeamSection и m_BeamSectionReport
            if (_T_L == 'T')
            {
                m_BeamSection = BeamSection.TBeam;
                dataGridSection.DataSource = null;

                m_Table = FiberMainFormHelper.GetTableFromBeamSections(m_InitBeamSectionsGeometry, m_BeamSection);
                dataGridSection.DataSource = m_Table;
                picBeton.Image = global::BSFiberConcrete.Properties.Resources.TBeam;
            }
            else if (_T_L == 'L')
            {
                m_BeamSection = BeamSection.LBeam;
                dataGridSection.DataSource = null;

                m_Table = FiberMainFormHelper.GetTableFromBeamSections(m_InitBeamSectionsGeometry, m_BeamSection);
                dataGridSection.DataSource = m_Table;
                picBeton.Image = global::BSFiberConcrete.Properties.Resources.LBeam;
            }

            foreach (DataGridViewColumn column in dataGridSection.Columns)
                column.SortMode = DataGridViewColumnSortMode.NotSortable;

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
            dataGridSection.DataSource = null;

            m_Table = FiberMainFormHelper.GetTableFromBeamSections(m_InitBeamSectionsGeometry, m_BeamSection);
            dataGridSection.DataSource = m_Table;

            foreach (DataGridViewColumn column in dataGridSection.Columns)
            { column.SortMode = DataGridViewColumnSortMode.NotSortable; }
            picBeton.Image = global::BSFiberConcrete.Properties.Resources.Ring;
        }

        // двутавровое сечение
        private void btnIBeam_Click(object sender, EventArgs e)
        {
            m_BeamSection = BeamSection.IBeam;
            dataGridSection.DataSource = null;

            m_Table = FiberMainFormHelper.GetTableFromBeamSections(m_InitBeamSectionsGeometry, m_BeamSection);
            dataGridSection.DataSource = m_Table;

            foreach (DataGridViewColumn column in dataGridSection.Columns) {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            picBeton.Image = global::BSFiberConcrete.Properties.Resources.IBeam;
            picBeton.SizeMode = PictureBoxSizeMode.StretchImage;
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
                if (beton == null)
                    return;

                string btName = beton.Name.Replace("i", b_i);

                var getQuery = FiberConcrete.Where(f => f.BT == btName);
                if (getQuery?.Count() > 0)
                {
                    Elements fib = getQuery?.First();

                    numRfbt3n.Value = Convert.ToDecimal(BSHelper.MPA2kgsm2(fib.Rfbt3n));
                    numRfbt2n.Value = Convert.ToDecimal(BSHelper.MPA2kgsm2(fib.Rfbt2n));

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
        /// <param name="_MNQ"></param>
        /// <exception cref="Exception"></exception>
        private void GetEffortsFromForm(out Dictionary<string, double> _MNQ, bool _convert = false)
        {
            _MNQ = new Dictionary<string, double>();

            string[] F = new string[] { "Mx", "My", "N", "Q", "Ml", "eN" };
            DataGridViewRowCollection rows = gridEfforts.Rows;
            var row = rows[0];

            for (int i = 0; i < F.Length; i++)
            {
                var x = Convert.ToDouble(row.Cells[i].Value);

                if (_convert)
                {
                    if (i<4) x = BSHelper.Kgs2kN(x, 4);
                }

                _MNQ.Add(F[i], x);
            }

            if (_MNQ.Count == 0)
                throw new Exception("Не заданы усилия");

            _MNQ["Qy"] = 0;
            _MNQ["Ml"] = (double)num_Ml1_M1.Value;
            _MNQ["eN"] = (double)num_eN.Value;
            _MNQ["e0"] = (double)numRandomEccentricity.Value;
        }

        /// <summary>
        ///  Введенные пользователем значения по арматуре
        /// </summary>
        /// <param name="_Rebar"></param>
        private void InitRebarValues(ref Rebar _Rebar)
        {
            _Rebar.As = (double)numAs.Value;
            _Rebar.As1 = (double)numAs1.Value;
            _Rebar.a = (double)num_a.Value;
            _Rebar.a1 = (double)num_a1.Value;

            _Rebar.Es = (double)numEs.Value;
            _Rebar.Esw = (double)numEsw.Value;

            _Rebar.s_w = _Rebar.s_w;

        }

        /// <summary>
        /// Усилия
        /// </summary>
        /// <param name="fiberCalc"></param>
        /// <param name="_rebar">Армирование</param>
        /// <param name="_fissurre">Расчет на трещиностойкость</param>
        private void FiberCalc_MNQ(out BSFiberCalc_MNQ fiberCalc, bool _useRebar = false, bool _shear = false)
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
                Rebar rebar = (Rebar)m_BSLoadData.Rebar.Clone(); // из глобальных параметров                
                //  введено пользователем
                InitRebarValues(ref rebar);
                
                // Армирование
                fiberCalc.Rebar = rebar;

                InitTRebar(out double[] t_r);

                double[] l_rebar = new double[1]; // TODO rebar 
                fiberCalc.SetRebarParams(l_rebar, t_r);
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
            if (fiberCalc.h / 2 < e_tot) _N_out = true;
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
                m_Message = new List<string>();
                FiberCalc_MNQ(out fiberCalc, checkBoxRebar.Checked);
                // расчет по второй группе предельных состояний
                FiberCalculate_Cracking();
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
                // для расчета по второй грппе пред состояний
                report.CalcResults2Group = m_CalcResults2Group;
                report.Messages = m_Message;
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
                m_Message = new List<string>();

                FiberCalc_MNQ(out fiberCalc, true, _shear: true);
                // Расчет по второй группе предельных состояний
                FiberCalculate_Cracking();
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
                // для расчета по второй грппе пред состояний
                report.CalcResults2Group = m_CalcResults2Group;
                report.Messages = m_Message;

                string pathToHtmlFile = report.CreateReport(3);

                System.Diagnostics.Process.Start(pathToHtmlFile);
            }
        }

        // поперечная арматура
        private void InitTRebar(out double[] t_rebar)
        {
            t_rebar = new double[3];

            t_rebar[0] = (double)numRsw.Value;
            t_rebar[1] = (double)numRsc.Value;
            t_rebar[2] = (double)numEs.Value;
        }

        /// <summary>
        ///  Расчет по методу статического равновесия           
        ///  Расчет элементов по полосе между наклонными сечениями
        ///  Расчет на действие момента и поперечной силы
        /// </summary>        
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

        /// <summary>
        /// Привязка арматуры 
        /// (экранные координаты ц.т. стержней привязываем к с.к. сечения балки)
        /// </summary>
        public static (List<double>, List<double>, List<double>, double, double) ReinforcementBinding(BeamSection _BeamSection, double _leftX, double _leftY)
        {
            // значения из БД
            var _rods = BSData.LoadBSRod(_BeamSection);

            List<double> rodD = new List<double>();
            List<double> bY = new List<double>(); // по ширине
            List<double> hX = new List<double>(); // по высоте

            // количество стержней
            int d_qty = _rods.Count;

            // площадь арматуры
            double area_total = 0;
            foreach (var lr in _rods)
            {
                area_total += BSHelper.AreaCircle(lr.D);
            }

            foreach (var lrod in _rods)
            {
                rodD.Add(lrod.D);
                hX.Add(lrod.CG_Y - _leftY);
                bY.Add(lrod.CG_X - _leftX);
            }

            return (rodD, hX, bY, d_qty, area_total);
        }

        /// <summary>
        ///  данные с формы
        /// </summary>
        /// <param name="_beamSection">Тип сечения </param>
        /// <returns>словарь данных</returns>
        private Dictionary<string, double> DictCalcParams(BeamSection _beamSection)
        {
            // Усилия Mx, My - моменты, кгс*см , N - сила, кгс              
            GetEffortsFromForm(out Dictionary<string, double> MNQ);

            Dictionary<string, double> D = new Dictionary<string, double>()
            {
                // enforces
                ["N"] = -MNQ["N"],
                ["My"] = MNQ["My"],
                ["Mz"] = MNQ["Mx"],
                //

                //section size
                ["b"] = 0,
                ["h"] = 0,

                ["bf"] = 0,
                ["hf"] = 0,
                ["bw"] = 0,
                ["hw"] = 0,
                ["b1f"] = 0,
                ["h1f"] = 0,

                ["r1"] = 0,
                ["R2"] = 0,
                //

                //Mesh
                ["ny"] = (int)numMeshN.Value,
                ["nz"] = (int)numMeshN.Value,
                // beton
                ["Eb0"] = (double)numEfb.Value,
                // - нормативные
                ["Rbcn"] = (double)(numRfb_n.Value),
                ["Rbtn"] = (double)(numRfbt_n.Value),
                // - расчетные
                ["Rbc"] = (double)(numRfb_n.Value / numYb.Value),
                ["Rbt"] = (double)(numRfbt_n.Value / numYft.Value),
                // - деформации
                ["ebc0"] = (double)numEps_fb0.Value, // ? 
                ["ebc2"] = (double)numEps_fb2.Value, // ?
                ["ebt0"] = (double)numEps_fbt0.Value, // ? 
                ["ebt1"] = (double)numEps_fbt1.Value, // ? 
                ["ebt2"] = (double)numEps_fbt2.Value, // ?
                ["ebt_ult"] = (double)numEps_fbt_ult.Value,
                // steel
                ["Es0"] = (double)numEs.Value,
                // нормативные 
                ["Rscn"] = (double)(numRscn.Value),
                ["Rstn"] = (double)(numRsn.Value),
                // расчетные
                ["Rsc"] = (double)numRsc.Value,
                ["Rst"] = (double)numRs.Value,
                // деформации
                ["esc2"] = (double)numEpsilonS2.Value, // уточнить
                ["est2"] = (double)numEpsilonS2.Value, // уточнить
                ["es_ult"] = (double)numEps_s_ult.Value,
            };

            double[] beam_sizes = BeamSizes();

            double b = 0;
            double h = 0;

            if (_beamSection == BeamSection.Rect)
            {
                b = beam_sizes[0];
                h = beam_sizes[1];
            }
            else if (_beamSection == BeamSection.IBeam)
            {
                D["bf"] = beam_sizes[0];
                D["hf"] = beam_sizes[1];
                D["bw"] = beam_sizes[2];
                D["hw"] = beam_sizes[3];
                D["b1f"] = beam_sizes[4];
                D["h1f"] = beam_sizes[5];

                b = D["bf"];
                h = D["hf"] + D["hw"] + D["h1f"];
            }
            else if (_beamSection == BeamSection.Ring)
            {
                D["r1"] = beam_sizes[0];
                D["R2"] = beam_sizes[1];

                b = 2 * D["R2"];
                h = 2 * D["R2"];
            }

            D["b"] = b;
            D["h"] = h;

            return D;
        }

        /// <summary>
        /// "Расчет по прочности нормальных сечений на основе нелинейной деформационной модели"
        /// </summary>        
        private void CalcNDM(BeamSection _beamSection)
        {
            // данные с формы
            var D = DictCalcParams(_beamSection);

            //привязка арматуры (по X - высота, по Y ширина балки)
            double leftX = 0;
            // для прямоугольных и тавровых сечений привязка к центу нижней грани 
            if (_beamSection == BeamSection.Rect || _beamSection == BeamSection.IBeam) leftX = -D["b"] / 2.0;
            (List<double> listD, List<double> listX, List<double> listY, double _qty, double _area) = ReinforcementBinding(_beamSection, leftX, 0);

            D.Add("rods_qty", _qty);
            D.Add("rods_area", _area);

            // выполнить расчет по 1 группе п.с.
            BSCalcNDM bsCalc1 = new BSCalcNDM(1);
            bsCalc1.BeamSection = _beamSection;
            bsCalc1.DictParams(D);
            bsCalc1.GetRods(listD, listX, listY);
            bsCalc1.Run();
            var values = bsCalc1.SigmaBResult;

            BSCalcResultNDM calcRes = new BSCalcResultNDM(bsCalc1.Results);
            calcRes.InitCalcParams(D);
            calcRes.ErrorIdx.Add(bsCalc1.Err); // вывести описание ошибки
            calcRes.Results1Group(ref m_CalcResults);
            calcRes.ResultsMsg1Group(ref m_Message);

            // выполнить расчет по 2 группе п.с.
            BSCalcNDM bsCalc2 = new BSCalcNDM(2);
            bsCalc2.BeamSection = _beamSection;
            bsCalc2.DictParams(D);
            bsCalc2.GetRods(listD, listX, listY);
            bsCalc2.Run();
            calcRes.ErrorIdx.Add(bsCalc2.Err);
            calcRes.GetRes2Group(bsCalc2.Results);
            calcRes.Results2Group(ref m_CalcResults2Group);

            m_GeomParams = calcRes.GeomParams;
            m_Efforts = calcRes.Efforts;
            m_PhysParams = calcRes.PhysParams;
            m_Reinforcement = calcRes.Reinforcement;

            ShowMosaic(values);
        }


        /// <summary>
        /// Расчет по прочности нормальных сечений на основе нелинейной деформационной модели
        /// </summary>
        /// <param name="_LSD">Группа предельных состояний</param>
        ///
        [DisplayName("Расчет по прочности нормальных сечений на основе нелинейной деформационной модели")]
        private void CalcDeformNDM()
        {
            // центр тяжести сечения
            TriangleNet.Geometry.Point CG = new TriangleNet.Geometry.Point(0.0, 0.0);

            DeformDiagramType deformDiagramType = (DeformDiagramType)cmbDeformDiagram.SelectedIndex;
            DeformMaterialType deformMaterialType = (DeformMaterialType)cmbTypeMaterial.SelectedIndex;

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
            double c_eps_b1 = (double)numEps_fb0.Value;
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

            var bsBeam = BSBeam.construct(m_BeamSection);
            bsBeam.SetSizes(beam_sizes);
            bsBeam.Length = c_Length;

            double c_b = bsBeam.Width;
            double c_h = bsBeam.Height;
            m_GeomParams = new Dictionary<string, double> { { "b, см", c_b }, { "h, см", c_h } };

            //смещение начала координат            
            double dX0, dY0;

            // координаты ц.т. сечения, если т. 0 - левый нижний угол
            (CG.X, CG.Y) = bsBeam.CG();

            try
            {
                BSFiberCalc_Deform fiberCalc_Deform = new BSFiberCalc_Deform(_Mx: c_Mx, _My: c_My, _N: c_N);
                fiberCalc_Deform.DeformDiagram = deformDiagramType;
                fiberCalc_Deform.DeformMaterialType = deformMaterialType;
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

                // задать класс арматуры
                fiberCalc_Deform.MatRebar = new BSMatRod(cEs)
                {
                    RCls = cR_class,
                    Rsn = (double)numRsn.Value,
                    Rs = cRs,
                    Rsc = (double)numRsc.Value,
                    e_s0 = c_eps_s0,
                    e_s2 = c_eps_s2
                };

                m_Reinforcement = new Dictionary<string, double>();

                ///
                /// расстановка арматурных стержней                
                /// 
                Action RodsReinforcement = delegate ()
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
                    Rfbtn = (double)numRfbt_n.Value,
                    Rfbt2n = (double)numRfbt2n.Value,
                    Rfbt3n = (double)numRfbt3n.Value,
                    e_b1_red = c_eps_b1_red,
                    e_b1 = c_eps_b1,
                    e_b2 = c_eps_b2,
                    Eps_fb_ult = (double)numEps_fb_ult.Value,
                    Eps_fbt_ult = (double)numEps_fbt_ult.Value,
                };

                // задать свойства бетона
                fiberCalc_Deform.MatFiber = beamMaterial;
                // расстановка стержней арматуры
                RodsReinforcement();
                // арматура балки                                                
                fiberCalc_Deform.Beam.Rods = Rods;
                // материал
                fiberCalc_Deform.Beam.Mat = beamMaterial;
                // параметры расчета:  (кол-во точек разбиения )
                fiberCalc_Deform.GetParams(new double[] { (int)numMeshN.Value, (int)numMeshN.Value });
                //
                // рассчитать                

                //fiberCalc_Deform.CalcNDM();

                fiberCalc_Deform.Calculate();
                //
                m_Efforts = fiberCalc_Deform.Efforts;

                m_PhysParams = fiberCalc_Deform.PhysParams;
                // получить результат
                m_CalcResults = fiberCalc_Deform.Results();

                // Расчет по 2 группе предельных состояний
                m_CalcResults2Group = fiberCalc_Deform.Result2Group;

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

                string value = "";
                try
                {
                    MethodBase method = MethodBase.GetCurrentMethod();
                    DisplayNameAttribute attr = (DisplayNameAttribute)method.GetCustomAttributes(typeof(DisplayNameAttribute), true)[0];
                    value = attr.DisplayName;
                }
                catch
                {
                    MessageBox.Show("Не задан атрибут DisplayName метода");
                }

                string pathToHtmlFile = CreateReport(1, m_BeamSection, value);

                System.Diagnostics.Process.Start(pathToHtmlFile);
            }
            catch (Exception _e)
            {
                MessageBox.Show("Ошибка в отчете " + _e.Message);
            }
        }

        [DisplayName("Расчет по прочности нормальных сечений на основе нелинейной деформационной модели")]
        private void CreateReportNDM()
        {
            try
            {
                InitBeamLength();

                string value = "";
                try
                {
                    MethodBase method = MethodBase.GetCurrentMethod();
                    DisplayNameAttribute attr = (DisplayNameAttribute)method.GetCustomAttributes(typeof(DisplayNameAttribute), true)[0];
                    value = attr.DisplayName;
                }
                catch
                {
                    MessageBox.Show("Не задан атрибут DisplayName метода");
                }

                string pathToHtmlFile = CreateReport(1, m_BeamSection, value);

                System.Diagnostics.Process.Start(pathToHtmlFile);
            }
            catch (Exception _e)
            {
                MessageBox.Show("Ошибка в отчете " + _e.Message);
            }
        }


        /// <summary>
        /// Расчет по НДМ            
        /// </summary>        
        private void btnCalc_Deform_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_BeamSection == BeamSection.Rect)
                {
                    CalcNDM(BeamSection.Rect);
                }
                else if (m_BeamSection == BeamSection.IBeam ||
                         m_BeamSection == BeamSection.TBeam ||
                         m_BeamSection == BeamSection.LBeam)
                {
                    CalcNDM(BeamSection.IBeam);
                }
                else if (m_BeamSection == BeamSection.Ring)
                {
                    //
                    var CG = new TriangleNet.Geometry.Point(0, 0);
                    GenerateMesh(ref CG); // покрыть сечение сеткой
                    //
                    CalcNDM(BeamSection.Ring);                    
                }
                else
                {
                    CalcDeformNDM();
                }
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }

            CreateReportNDM();
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
            //TODO Удалить кнопку 
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

        private void btnCalcResults_Click(object sender, EventArgs e)
        {
            BSCalcResults bSCalcResults = new BSCalcResults();
            bSCalcResults.CalcParams = DictCalcParams(m_BeamSection);
            bSCalcResults.CalcResults = m_CalcResults;
            bSCalcResults.Show();
        }

        private void cmbBftn_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FiberBft bft = (FiberBft)cmbBftn.SelectedItem;
                numRfbt_n.Value = (decimal)BSHelper.MPA2kgsm2(bft.Rfbtn); // Convert
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
            foreach (Rebar rebar in m_Rebar)
            {
                if (rebar.ID == cmbRebarClass.Text)
                {
                    numRs.Value = (decimal)BSHelper.MPA2kgsm2(rebar.Rs);
                    numRsc.Value = (decimal)BSHelper.MPA2kgsm2(rebar.Rsc);
                    numRsn.Value = (decimal)BSHelper.MPA2kgsm2(rebar.Rsn);
                    numRscn.Value = (decimal)BSHelper.MPA2kgsm2(rebar.Rsn);
                    numEs.Value = (decimal)BSHelper.MPA2kgsm2(rebar.Es);

                    labelTypeDDRebar.Text = rebar.TypeDiagramm;
                    numEps_s_ult.Value = (decimal)rebar.Epsilon_s_ult;

                    break;
                }
            }

            # region Выбираем список диаметров и площадь в зависимости от класса арматуры
            double? valueDiameterPreviosRebar = null;
            int newSelectedIndex = 0;
            if (cmbRebarDiameters.Items.Count > 0)
            {
                valueDiameterPreviosRebar = (double)cmbRebarDiameters.Items[cmbRebarDiameters.SelectedIndex];
                cmbRebarDiameters.Items.Clear();
                cmbRebarSquare.Items.Clear();
            }
            List<object> diametersForRebar = new List<object>();
            List<object> squareForRebar = new List<object>();
            foreach (RebarDiameters tmpRebarD in m_RebarDiameters)
            {
                if (tmpRebarD.TypeRebar == cmbRebarClass.Text)
                {
                    diametersForRebar.Add(tmpRebarD.Diameter);
                    squareForRebar.Add(tmpRebarD.Square);
                    if ((valueDiameterPreviosRebar != null) && (tmpRebarD.Diameter == valueDiameterPreviosRebar))
                    {
                        newSelectedIndex = diametersForRebar.Count - 1;
                    }
                }
            }
            cmbRebarSquare.Items.AddRange(squareForRebar.ToArray());
            cmbRebarSquare.SelectedIndex = newSelectedIndex;
            cmbRebarDiameters.Items.AddRange(diametersForRebar.ToArray());
            cmbRebarDiameters.SelectedIndex = newSelectedIndex;
            #endregion          
        }

        private void cmbTRebarClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Rebar trb = m_Rebar.Find(match => match.ID == cmbTRebarClass.Text);      //Lib.BSQuery.RebarFind(cmbTRebarClass.Text);
                if (trb != null)
                {
                    numRsw.Value = (decimal)BSHelper.MPA2kgsm2(trb.Rsw);
                    numEsw.Value = (decimal)BSHelper.MPA2kgsm2(trb.Es);
                    num_s_w.Value = (decimal)trb.s_w;
                }
            }
            catch { }
        }

        private void numRfbt_n_ValueChanged(object sender, EventArgs e)
        {
            labelRfbtnMPa.Text = string.Format("{0} МПа ", BSHelper.Kgsm2MPa((double)numRfbt_n.Value));

            numEps_fbt0.Value = BSMatFiber.NumEps_fbt0(numRfbt_n.Value, numE_fiber.Value);
            numEps_fbt1.Value = numEps_fbt0.Value + 0.0001m;
        }

        private void numRfb_n_ValueChanged(object sender, EventArgs e)
        {
            labelRfbnMPa.Text = string.Format("{0} МПа ", BSHelper.Kgsm2MPa((double)numRfb_n.Value));

            numEps_fb1.Value = BSMatFiber.NumEps_fb1(numRfb_n.Value, numE_fiber.Value);
        }

        private void numRfbt2n_ValueChanged(object sender, EventArgs e)
        {
            labelRfbt2nMPa.Text = string.Format("{0} МПа ", BSHelper.Kgsm2MPa((double)numRfbt2n.Value));

            // расчетные значения отличаются от нормативных коэфициентом numYft, поэтому можно передать нормативные значения
            numEps_fbt3.Value = BSMatFiber.NumEps_fbt3(numRfbt2n.Value, numRfbt3n.Value);
        }

        private void numRfbt3n_ValueChanged(object sender, EventArgs e)
        {
            labelRfbt3nMPa.Text = string.Format("{0} МПа ", BSHelper.Kgsm2MPa((double)numRfbt3n.Value));

            numEps_fbt3.Value = BSMatFiber.NumEps_fbt3(numRfbt2n.Value, numRfbt3n.Value);
        }

        private void numRs_ValueChanged(object sender, EventArgs e)
        {
            labelRsMPa.Text = string.Format("{0} МПа ", BSHelper.Kgsm2MPa((double)numRs.Value));

            numEpsilonS1.Value = BSMatRod.NumEps_s1(numRs.Value, numEs.Value);
            numEpsilonS0.Value = BSMatRod.NumEps_s0(numRs.Value, numEs.Value);
        }

        private void numRsw_ValueChanged(object sender, EventArgs e)
        {
            labelRswMPa.Text = string.Format("{0} МПа ", BSHelper.Kgsm2MPa((double)numRsw.Value));
        }

        private void checkBoxRebar_CheckedChanged(object sender, EventArgs e)
        {
            flowLayoutPanelRebar.Enabled = (checkBoxRebar.Checked == true);

            if (checkBoxRebar.Checked)
                numYb2.Value = 1.0M;
            else
                numYb2.Value = 0.9M;
        }

        /// <summary>
        ///  Конструктор сечений
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSection_Click(object sender, EventArgs e)
        {
            BSSectionChart sectionChart = new BSSectionChart();
            sectionChart.m_BeamSection = m_BeamSection;

            var sz = BeamWidtHeight(out double b, out double h, out double _area);

            sectionChart.Wdth = (float)b;
            sectionChart.Hght = (float)h;
            sectionChart.Sz = sz;
            sectionChart.NumArea = _area;

            sectionChart.Show();
        }

        /// <summary>
        ///  Разбиение сечения на конечные элементы
        /// </summary>
        /// <param name="_values"></param>
        private void ShowMosaic(List<double> _values = null)
        {
            MeshDraw mDraw;

            double[] sz = BeamSizes();

            if (BSHelper.IsRectangled(m_BeamSection))
            {
                mDraw = new MeshDraw((int)numMeshN.Value, (int)numMeshN.Value);                                                
                mDraw.MaxVal = (double)numEps_fbt_ult.Value;
                mDraw.MinVal = -(double)numEps_fb_ult.Value;
                mDraw.Values = _values;
                mDraw.CreateRectanglePlot(sz, m_BeamSection);
                mDraw.ShowMesh();
            }
            else if (m_BeamSection == BeamSection.Ring)
            {
                TriangleNet.Geometry.Point cg = new TriangleNet.Geometry.Point();
                _= GenerateMesh(ref cg);

                mDraw = new MeshDraw(Tri.Mesh);
                mDraw.MaxVal = (double)numEps_fbt_ult.Value;
                mDraw.MinVal = -(double)numEps_fb_ult.Value;
                mDraw.Values = _values;                
                mDraw.PaintSectionMesh();

                mDraw.ShowMesh();
                //md.SaveToPNG();
            }
        }


        // <summary>
        /// Покрыть сечение сеткой
        /// </summary>
        private string GenerateMesh(ref TriangleNet.Geometry.Point _CG)
        {
            string pathToSvgFile;

            double[] sz = BeamWidtHeight(out double b, out double h, out double area);
            double meshSize = (double)numMeshN.Value;

            BSMesh.Nx = (int)meshSize;
            BSMesh.Ny = (int)meshSize;

            BSMesh.MinAngle = (double)numTriAngle.Value;
            Tri.MinAngle = (double)numTriAngle.Value;

            if (meshSize > 0)
            {
                Tri.MaxArea = area / meshSize;
                BSMesh.MaxArea = Tri.MaxArea;
            }

            BSMesh.FilePath = Path.Combine(Environment.CurrentDirectory, "Templates");
            Tri.FilePath = BSMesh.FilePath;
            
            if (m_BeamSection == BeamSection.Rect)
            {
                List<double> rect = new List<double> { 0, 0, b, h };
                //_CG = new TriangleNet.Geometry.Point(0.0, h/2.0);                
                pathToSvgFile = BSCalcLib.BSMesh.GenerateRectangle(rect);
                Tri.Mesh = BSMesh.Mesh;
                // сместить начало координат из левого нижнего угла в центр тяжести
                Tri.Oxy = _CG;

                _ = Tri.CalculationScheme();
            }
            else if (m_BeamSection == BeamSection.Ring)
            {
                _CG = new TriangleNet.Geometry.Point(0, 0);

                double r = sz[0];
                double R = sz[1];
                
                if (r > R)
                    throw BSBeam_Ring.RadiiError();

                BSMesh.Center = _CG;
                pathToSvgFile = BSMesh.GenerateRing(R, r, true);

                Tri.Mesh = BSMesh.Mesh;
                _ = Tri.CalculationScheme();
            }
            else if (BSHelper.IsITL(m_BeamSection))
            {
                List<PointF> pts;
                BSSection.IBeam(sz, out pts, out PointF _center);
                _CG = new TriangleNet.Geometry.Point(_center.X, _center.Y);

                pathToSvgFile = BSCalcLib.Tri.CreateIBeamContour(pts);
                _ = Tri.CalculationScheme();
            }
            else
            {
                throw new Exception("Не задано сечение");
            }

            // площади треугольников
            triAreas = Tri.triAreas;
            // центры тяжести треугольников
            triCGs = Tri.triCGs; 

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

        private void btnCalcDeformDiagram_Click(object sender, EventArgs e)
        {
            string typeDiagram = cmbDeformDiagram.Text;

            string typeMaterial = cmbTypeMaterial.Text;

            // сжатие
            double R_n = 0;
            double e0 = 0;
            double e2 = 0;
            double E = 0;
            // растяжение
            double Rt_n = 0;
            double Rt2_n = 0;
            double Rt3_n = 0;
            double Et = 0;
            double et0 = 0;
            double et2 = 0;
            double et3 = 0;

            if (typeMaterial == BSHelper.Concrete)
            {
                // Характеристики по сжатию
                R_n = (double)numRfb_n.Value;       // Rb_n 
                e0 = (double)numEps_fb0.Value;      // eb0
                e2 = (double)numEps_fb2.Value;      // eb2
                E = (double)numEfb.Value;           // Eb

            }
            else if (typeMaterial == BSHelper.FiberConcrete)
            {
                // Характеристики по сжатию такие же как у бетона
                R_n = (double)numRfb_n.Value;       // Rb_n 
                e0 = (double)numEps_fb0.Value;      // eb0
                e2 = (double)numEps_fb2.Value;      // eb2
                //e0 = (double)0.003m;
                //e2 = (double)0.0042m;
                E = (double)numEfb.Value;           //Eb
                // Характеристики по растяжению
                Rt_n = (double)numRfbt_n.Value;     // Rfbt_n
                Rt2_n = (double)numRfbt3n.Value;    // Rfbt2_n
                Rt3_n = (double)numRfbt2n.Value;    // Rfbt3_n
                Et = E;                    // !!!   // Efbt
                et2 = (double)numEps_fbt2.Value;    // efbt2
                et3 = (double)numEps_fbt3.Value;    // efbt3
            }
            else if (typeMaterial == BSHelper.Rebar)
            {

                // Характеристики по растяжению
                Rt_n = (double)numRs.Value;         // 
                Et = (double)numEs.Value;           //
                et0 = (double)numEpsilonS0.Value;   //
                et2 = (double)numEpsilonS2.Value;   //

                // Характеристики по сжатию
                R_n = (double)numRsc.Value;
                e0 = et0;
                e2 = et2;
                E = Et;
            }
            else
            {
                throw new Exception("Выбрано значение материала, выходящее за предел предопределенных значений.");
            }

            DataForDeformDiagram.typesDiagram = new string[] { typeMaterial, typeDiagram };
            DataForDeformDiagram.resists = new double[] { R_n, Rt_n, Rt2_n, Rt3_n };
            DataForDeformDiagram.deforms = new double[] { e0, e2, et0, et2, et3 };
            DataForDeformDiagram.E = new double[] { E, Et };

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

        /// <summary>
        /// При изменении пользователем dataGridSection.DataSource производится перезапись поля m_InitBeamSectionsGeometry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserEditGridSection(object sender, DataGridViewCellEventArgs e)
        {
            // To Do необходимо выполнять проверку пользовательских данных
            InitBeamSectionGeometry beamSectionGeometry = FiberMainFormHelper.CreateBeamSectionsGeometry((DataTable)dataGridSection.DataSource, m_BeamSection);
            int index = FiberMainFormHelper.IndexOfSectionGeometry(m_InitBeamSectionsGeometry, m_BeamSection);
            m_InitBeamSectionsGeometry[index] = beamSectionGeometry;

        }

        //
        private void CloseFiberMainForm(object sender, FormClosingEventArgs e)
        {
            try
            {
                BSData.UpdateBeamSectionGeometry(m_InitBeamSectionsGeometry);

                GetEffortsFromForm(out Dictionary<string, double> MNQ);
                Lib.BSData.SaveEfforts(new Efforts() { Id = 1, Mx = MNQ["Mx"], My = MNQ["My"], N = MNQ["N"], Q = MNQ["Q"], Ml = MNQ["Ml"], eN = MNQ["eN"] });
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
        }

        private void NumEsInit()
        {
        }
        
        private void numEs_ValueChanged(object sender, EventArgs e)
        {
            labelEsMPa.Text = string.Format("{0} МПа ", BSHelper.Kgsm2MPa((double)numEs.Value));
            numEpsilonS1.Value = BSMatRod.NumEps_s1(numRs.Value, numEs.Value);
            numEpsilonS0.Value = BSMatRod.NumEps_s0(numRs.Value, numEs.Value);


        }

        private void BSFiberMain_Leave(object sender, EventArgs e)
        {

        }

        private void numRsc_ValueChanged(object sender, EventArgs e)
        {
            labelRsсMPa.Text = string.Format("{0} МПа ", BSHelper.Kgsm2MPa((double)numRsc.Value));
            
        }


        /// <summary>
        /// СП360 6.1.25
        /// </summary>        
        private void numEps_fbt3_ValueChanged(object sender, EventArgs e)
        {
            numEps_fbt_ult.Value = numEps_fbt3.Value;
        }

        /// <summary>
        /// СП 8.1.30
        /// </summary>        
        private void numEps_fb2_ValueChanged(object sender, EventArgs e)
        {
            numEps_fb_ult.Value = numEps_fb2.Value;
        }

        /// <summary>
        /// обновление плоащди сечения в зависимости от изменения диаметра
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbRebarDiameters_SelectedIndexChanged(object sender, EventArgs e)
        {
            int previosIndexRebarDiameter = cmbRebarDiameters.SelectedIndex;
            cmbRebarSquare.SelectedIndex = previosIndexRebarDiameter;
        }
        
        
        private void numE_fiber_ValueChanged(object sender, EventArgs e)
        {
            numEps_fbt0.Value = BSMatFiber.NumEps_fbt0(numRfbt_n.Value, numE_fiber.Value);
            numEps_fbt1.Value = numEps_fbt0.Value + 0.0001m;
            numEps_fb1.Value = BSMatFiber.NumEps_fb1(numRfb_n.Value, numE_fiber.Value);

        }

        /// <summary>
        /// Запуск расчета по второй грппе предельных состояний
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCalcCrack_Click(object sender, EventArgs e)
        {
            try
            {
                FiberCalculate_Cracking();
            }
            catch (Exception _ex)
            {
                MessageBox.Show(_ex.Message);
            }
        }


        private void label12_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Коэффициент расчета  по прочности на растяжение(1 группа предельных состояний)", "Информация");
        }

        private void label13_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Значения коэффициента надежности по бетону при сжатии ", "Информация");            
        }

        private void cmbFib_i_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedFiberBetonValues();
        }

        private void cmbBetonClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedFiberBetonValues();
        }

        private void tableLayoutPanelRebar_Paint(object sender, PaintEventArgs e)
        {

        }

        private void labelMNQ_Click(object sender, EventArgs e)
        {
            GetEffortsFromForm(out Dictionary<string, double> _MNQ, true);
            MessageBox.Show($"My = {_MNQ["My"]} Кн*см\n Mx = {_MNQ["Mx"]} Кн*см\n N = {_MNQ["N"]} Кн\n Q = {_MNQ["Q"]} Кн", "Усилия"); 
        }

        private void label35_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Yb1 - коэффициент условий работы к значениям Rfb, Rfbt, Rfbt3 учитывающий влияние длительности действия статической нагрузки", "Информация");
        }

        private void label36_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Yb2 - коэффициент для элементов без стержневой арматуры к значениям Rfb и учитывающий характер разрушения таких конструкций", "Информация");
        }

        private void label37_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Yb3 - коэффициент вводимиый при бетонировании в вертикальном положении при высоте слоя более 1.5 м", "Информация");
        }

        private void label38_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Yb5 - коэффициент условий работы, учитывающий влияние попереременного замораживания и оттаивания, а также отрицательных температур", "Информация");
        }

       
        private void label38_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.Hand;            
        }

       
        private void label37_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.Hand;
        }
                
        private void label36_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.Hand;
        }
    
        private void label35_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.Hand;
        }
    
        private void label13_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.Hand;
        }

        private void label12_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.Hand;
        }

        private void btnMosaic_Click(object sender, EventArgs e)
        {
            try
            {
                ShowMosaic();
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
        }
    }
}

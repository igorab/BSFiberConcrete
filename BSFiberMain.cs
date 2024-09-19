using BSBeamCalculator;
using BSCalcLib;
using BSFiberConcrete.CalcGroup2;
using BSFiberConcrete.Control;
using BSFiberConcrete.DeformationDiagram;
using BSFiberConcrete.Lib;
using BSFiberConcrete.Section;
using BSFiberConcrete.UnitsOfMeasurement;
using BSFiberConcrete.UnitsOfMeasurement.PhysicalQuantities;
using ScottPlot.Statistics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;


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

        // Текущий тип сечения
        private BeamSection m_BeamSection { get; set; }

        // Mesh generation
        // площади элементов (треугольников)
        private List<double> triAreas;

        // координаты центра тяжести элементов (треугольников)
        private List<TriangleNet.Geometry.Point> triCGs;

        //Изображение рассчитываемого сечения
        private BSSectionChart m_SectionChart;
        private MemoryStream m_ImageStream;

        private LameUnitConverter _UnitConverter;


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
                tableLayoutAreaRebar.Visible = false;
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

        /// <summary>
        ///  Заполнить поля значениями по умолчанию
        /// </summary>
        private void InitFormControls()
        {
            FormParams prms = BSData.LoadFormParams();

            tbLength.Text = Convert.ToString(prms.Length);
            cmbEffectiveLengthFactor.Text = Convert.ToString(prms.LengthCoef);

            cmbFib_i.Text = prms.Fib_i;
            comboBetonType.Text = prms.BetonType;
            cmbBetonClass.Text = prms.Bft3n;
            cmbBfn.Text = prms.Bfn;
            cmbBftn.Text = prms.Bftn;
            //numE_beton.Text = prms.Eb;
            //numE_fiber.Text = prms.Efbt;
            cmbRebarClass.Text = prms.Rs;
            cmbTRebarClass.Text = prms.Rsw;

            numAs.Value = (decimal)prms.Area_s;
            numAs1.Value = (decimal)prms.Area1_s;
            num_a.Value = (decimal)prms.a_s;
            num_a1.Value = (decimal)prms.a1_s;
        }


        // глобальные настройки
        public void BSFiberMain_Load(object sender, EventArgs e)
        {
            try
            {
                // Установка высплывающего текста
                System.Windows.Forms.ToolTip toolTip1 = new System.Windows.Forms.ToolTip();
                // Set up the delays for the ToolTip.
                toolTip1.AutoPopDelay = 5000;
                toolTip1.InitialDelay = 1000;
                toolTip1.ReshowDelay = 50;
                // Force the ToolTip text to be displayed whether or not the form is active.
                toolTip1.ShowAlways = true;
                // Set up the ToolTip text for the Button and Checkbox.
                toolTip1.SetToolTip(this.btnRectang, "Прямоугольное сечение");
                toolTip1.SetToolTip(this.btnTSection, "Тавровое сечение \"Верхняя полка\"");
                toolTip1.SetToolTip(this.btnLSection, "Тавровое сечение \"Нижняя полка\"");
                toolTip1.SetToolTip(this.btnIBeam, "Двутавровое сечение");
                toolTip1.SetToolTip(this.btnRing, "Кольцевое сечение");
                toolTip1.SetToolTip(btnCalcResults, "Результаты расчета");
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

                m_BSLoadData.InitEfforts(ref m_Iniv);
               
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

                m_Beton = BSData.LoadHeavyBetonData();
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

                InitFormControls();
                //Mx My N Qx Qy
                double[] mnq = { m_Iniv["Mx"], m_Iniv["My"], m_Iniv["N"], m_Iniv["Qx"], m_Iniv["Qy"] };
                gridEfforts.Rows.Add(mnq);
                for (int i = 0; i < mnq.Length; i++)
                {
                    gridEfforts.Rows[0].Cells[i].Value = mnq[i];
                }

                // пользовательское изменение ед измерения для нагрузок
                List<Enum> modelUnitsMeasurement = new List<Enum>()
                {
                    LengthUnits.m,
                    ForceUnits.kg,
                    MomentOfForceUnits.kgBycm
                };
                _UnitConverter = new LameUnitConverter(modelUnitsMeasurement);
                cmbForceUnit.DataSource = ForceMeasurement.ListOfName;
                cmbMomentOfForceUnit.DataSource = MomentOfForceMeasurement.ListOfName;
                cmbMomentOfForceUnit.SelectedIndex = 1;


                // настройки из БД
                Rebar dbRebar = m_Rebar.Where(x => x.ID == Convert.ToString(cmbRebarClass.SelectedItem))?.First();
                numEs.Value = (decimal)BSHelper.MPA2kgsm2(dbRebar.Es);
                numAs.Value = (decimal)dbRebar.As;
                numAs1.Value = (decimal)dbRebar.As1;
                num_a.Value = (decimal)dbRebar.a;
                num_a1.Value = (decimal)dbRebar.a1;

                //СП63 6.1.20 
                numEps_fb2.Value = 0.0035M;

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

        /// <summary>
        /// Расчетная длина балки 
        /// </summary>
        /// <param name="_init"></param>
        /// <returns></returns>
        private double InitBeamLength(bool _beaminit = false)
        {
            double.TryParse(tbLength.Text, out double lgth);
            double.TryParse(cmbEffectiveLengthFactor.Text, out double coeflgth);

            if (_beaminit)
            {
                m_Beam.Clear();
                m_Beam.Add("Длина элемента, см", lgth);
                m_Beam.Add("Коэффициет расчетной длины", coeflgth);
            }

            return (coeflgth != 0) ? lgth * coeflgth : lgth;
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
            Beton fb = Lib.BSQuery.HeavyBetonTableFind(cmbBfn.Text);
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

                bsCalc.SetParams(prms);
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
                double cRs = (double)numRs.Value; // кг/см2            
                double cEs = (double)numEs.Value; // кг/см2
                double c_eps_s0 = (double)numEpsilonS0.Value;// 0.00175; 
                double c_eps_s2 = (double)numEpsilonS2.Value; ; // 0.025; 

                double c_As = (double)numAs.Value;
                double c_As1 = (double)numAs1.Value;
                double c_a_s = (double)num_a.Value;
                double c_a_s1 = (double)num_a1.Value;

                bool reinforcement = checkBoxRebar.Checked;

                // сечение балки балки, см 
                double[] beam_sizes = BeamSizes();
                var bsBeam = BSBeam.construct(m_BeamSection);
                bsBeam.SetSizes(beam_sizes);

                double c_b = bsBeam.Width;
                double c_h = bsBeam.Height;

                // Усилия Mx, My - моменты, кг*см , N - сила, кг              
                GetEffortsFromForm(out Dictionary<string, double> MNQ);
                BSFiberCalc_Cracking calc_Cracking = new BSFiberCalc_Cracking(MNQ);
                //calc_Cracking.Efforts = MNQ;
                calc_Cracking.Beam = bsBeam;
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
                //InitBeamLength();
                calc_Cracking.MatFiber = m_MatFiber;
                calc_Cracking.SetParams(new double[] { 10, 1 });

                // рассчитать 
                calcOk = calc_Cracking.Calculate();
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
                report.ImageStream = m_ImageStream;
                report.Messages = m_Message;
                report.UseReinforcement = _useReinforcement;
                report.Path2BeamDiagrams = m_Path2BeamDiagrams;
                report._unitConverter = _UnitConverter;


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
        private void GetEffortsFromForm(out Dictionary<string, double> _MNQ)
        {
            _MNQ = new Dictionary<string, double>();

            DataGridViewColumnCollection columns = gridEfforts.Columns;

            for (int i = 0; i < columns.Count; i++)
            {
                string tmpName = columns[i].Name;
                double value = Convert.ToDouble(gridEfforts.Rows[0].Cells[i].Value);

                double newValue = _UnitConverter.ConvertEfforts(tmpName, value);
                _MNQ.Add(tmpName, newValue);
            }


            if (_MNQ.Count == 0)
                throw new Exception("Не заданы усилия");

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

            // передаем коэффициенты Yft, Yb, Yb1, Yb2, Yb3, Yb5, B
            fiberCalc.SetParams(prms);

            double beamLngth = InitBeamLength(true); // BSHelper.ToDouble(tbLength.Text);

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
                report._unitConverter = _UnitConverter;

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
                // для расчета по второй группе пред состояний
                report.CalcResults2Group = m_CalcResults2Group;
                report.Messages = m_Message;
                report._unitConverter = _UnitConverter;

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

            (double _M, double _N, double _Q) = (MNQ["My"], MNQ["N"], MNQ["Qy"]);
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
        ///  данные с формы
        /// </summary>
        /// <param name="_beamSection">Тип сечения </param>
        /// <returns>словарь данных</returns>
        private Dictionary<string, double> DictCalcParams(BeamSection _beamSection)
        {
            // Усилия Mx, My - моменты, кг*см , N - сила, кг              
            GetEffortsFromForm(out Dictionary<string, double> MNQ);

            BSMatFiber mf = new BSMatFiber((double)numEfb.Value, numYft.Value, numYb.Value, numYb1.Value, numYb2.Value, numYb3.Value, numYb5.Value);
            mf.Rfbn = (double)numRfb_n.Value;
            mf.Rfbtn = (double)numRfbt_n.Value;
            mf.Rfbt2n = (double)numRfbt2n.Value;
            mf.Rfbt3n = (double)numRfbt3n.Value;

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
                ["ny"] = (int)numMeshNY.Value,
                ["nz"] = (int)numMeshNX.Value, // в алгоритме плосткость сечения YOZ

                // beton
                ["Eb0"] = (double)numE_beton.Value, // сжатие
                ["Ebt"] = (double)numE_fiber.Value, // растяжение

                // - нормативные
                ["Rbcn"] = (double)(numRfb_n.Value),
                ["Rbtn"] = (double)(numRfbt_n.Value),
                ["Rbt2n"] = (double)(numRfbt2n.Value),
                ["Rbt3n"] = (double)(numRfbt3n.Value),
                // - расчетные 
                ["Rbc"] = mf.Rfb,
                ["Rbt"] = mf.Rfbt,
                ["Rbt2"] = mf.Rfbt2,
                ["Rbt3"] = mf.Rfbt3,
                // - деформации
                // сжатие
                ["ebc0"] = (double)numEps_fb0.Value,
                ["ebc2"] = (double)numEps_fb2.Value,
                ["eb_ult"] = (double)numEps_fb_ult.Value,

                // растяжение
                ["ebt0"] = (double)numEps_fbt0.Value,
                ["ebt1"] = (double)numEps_fbt1.Value,
                ["ebt2"] = (double)numEps_fbt2.Value,
                ["ebt3"] = (double)numEps_fbt3.Value,
                ["ebt_ult"] = (double)numEps_fbt_ult.Value,

                // арматура steel
                ["Es0"] = (double)numEs.Value,
                // нормативные 
                ["Rscn"] = (double)(numRscn.Value),
                ["Rstn"] = (double)(numRsn.Value),
                // расчетные
                ["Rsc"] = (double)numRsc.Value,
                ["Rst"] = (double)numRs.Value,
                // деформации
                ["esc2"] = (double)numEpsilonS2.Value, // сжатие уточнить
                ["est2"] = (double)numEpsilonS2.Value, // растяжение уточнить
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
        private void CalcNDM(BeamSection _beamSection, bool _useRebar = true)
        {
            const int GR1 = BSFiberLib.CG1;
            const int GR2 = BSFiberLib.CG2; 

            // данные с формы
            Dictionary<string, double> D = DictCalcParams(_beamSection);

            NDMSetup setup = BSData.LoadNDMSetup();

            //привязка арматуры (по X - высота, по Y ширина балки)
            double leftX = 0;
            // для прямоугольных и тавровых сечений привязка к центу нижней грани 
            if (BSHelper.IsRectangled(_beamSection)) leftX = -D["b"] / 2.0;
            (List<double> lD, List<double> lX, List<double> lY, double _qty, double _area) = BSCalcNDM.ReinforcementBinding(_beamSection, leftX, 0, _useRebar);

            D.Add("rods_qty", _qty);
            D.Add("rods_area", _area);

            setup.BetonTypeId = (cmbTypeMaterial.SelectedIndex == 1) ? 1 : 0;
            
            ///
            /// выполнить расчет по 1 группе предельных состояний
            ///
            BSCalcNDM bsCalc1 = new BSCalcNDM(GR1, _beamSection, setup);            
            bsCalc1.SetDictParams(D);
            bsCalc1.SetRods(lD, lX, lY);
            bsCalc1.Run();

            BSCalcResultNDM calcRes = new BSCalcResultNDM(bsCalc1.Results);
            calcRes.InitFromCalcNDM(bsCalc1);            
            calcRes.InitCalcParams(D);            
            calcRes.Results1Group(ref m_CalcResults);
            calcRes.ResultsMsg1Group(ref m_Message);

            ///
            /// выполнить расчет по 2 группе предельных состояний
            /// 
            // 1 этап
            // определяем моменты трещинообразования от кратковременных и длительных нагрузок (раздел X)
            double Mx_crc; double My_crc; double N_crc;
            double eps_s_crc; // параметр для определения ширины раскрытия трещины

            // используем заданные усилия и определяем коэфф использования по 2-гр пр сост
            BSCalcNDM bsCalc2 = new BSCalcNDM(GR2, _beamSection, setup);            
            bsCalc2.SetDictParams(D);
            bsCalc2.MzMyNUp(1);
            bsCalc2.SetRods(lD, lX, lY);
            bsCalc2.Run();

            calcRes.ErrorIdx.Add(bsCalc2.Err);
            calcRes.GetRes2Group(bsCalc2.Results);

            double ur_fb2 = bsCalc2.UtilRate_fb_t;

            if (_useRebar)
            {
                BSCalcNDM bsCalc3 = new BSCalcNDM(GR2, _beamSection, setup);
                bsCalc3.SetDictParams(D);
                bsCalc3.MzMyNUp(ur_fb2);
                bsCalc3.SetRods(lD, lX, lY);
                bsCalc3.Run();
                double ur_fb3 = bsCalc3.UtilRate_fb_t;
            
                // Если же хотя бы один из моментов трещинообразования оказывается меньше
                // соответствующего действующего момента, выполняют второй этап расчета.
                BSCalcNDM bsCalc4 = new BSCalcNDM(GR2, _beamSection, setup);
                bsCalc4.SetDictParams(D);
                bsCalc4.MzMyNUp(ur_fb2 * 1.182);
                bsCalc4.SetRods(lD, lX, lY);
                bsCalc4.Run();
                calcRes.ErrorIdx.Add(bsCalc4.Err);
                calcRes.GetRes2Group(bsCalc4.Results);
                // момент трещинообразования
                Mx_crc = bsCalc4.Mz_crc;
                My_crc = bsCalc4.My_crc;
                N_crc = bsCalc4.N_crc;
                List<double> E_S_crc = bsCalc4.EpsilonSResult;
                eps_s_crc = E_S_crc.Max();
                double ur_s = bsCalc4.UtilRate_s_t;
                double ur_fb4 = bsCalc4.UtilRate_fb_t;

                // определение ширины раскрытия трещины
                // расчитываем на заданные моменты и силы
                BSCalcNDM bsCalc5 = new BSCalcNDM(GR2, _beamSection, setup);
                bsCalc5.SetDictParams(D);
                bsCalc5.SetE_S_Crc(E_S_crc);
                bsCalc5.SetRods(lD, lX, lY);
                bsCalc5.Run();
                calcRes.ErrorIdx.Add(bsCalc5.Err);
                calcRes.GetRes2Group(bsCalc5.Results);
            }
            
            calcRes.Results2Group(ref m_CalcResults2Group);

            m_GeomParams = calcRes.GeomParams;
            m_Efforts = calcRes.Efforts;
            m_PhysParams = calcRes.PhysParams;
            m_Reinforcement = calcRes.Reinforcement;

            ShowMosaic(calcRes);
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
            double cRb = (double)numRfb_n.Value; // сопротивление сжатию, кг/см2
            double cEb = (double)numEfb.Value; // модуль упругости,  кг/см2

            // диаграмма:
            // арматура
            string cR_class = cmbRebarClass.Text;
            double cRs = (double)numRs.Value; // кг/см2            
            double cEs = (double)numEs.Value; // кг/см2
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

            // Усилия Mx, My - моменты, кг*см , N - сила, кг              
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
                fiberCalc_Deform.SetParams(new double[] { (int)numMeshNX.Value, (int)numMeshNX.Value });
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
                InitBeamLength(true);

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
                InitBeamLength(true);

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

        private bool ValidateNDMCalc()
        {
            if (m_SectionChart == null || m_SectionChart.BSBeamSection != m_BeamSection)
            {
                MessageBox.Show("Нажмите кнопку Сечение и задайте диаметры и расстановку стержней арматуры.", 
                    "Расчет по НДМ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Расчет по НДМ            
        /// </summary>        
        private void btnCalc_Deform_Click(object sender, EventArgs e)
        {
            if (!ValidateNDMCalc()) return;

            bool useRebar = checkBoxRebar.Checked;

            try
            {              
                if (m_BeamSection == BeamSection.Rect)
                {
                    CalcNDM(BeamSection.Rect, useRebar);
                }
                else if (BSHelper.IsITL(m_BeamSection))
                {
                    CalcNDM(BeamSection.IBeam, useRebar);
                }
                else if (m_BeamSection == BeamSection.Ring)
                {
                    //
                    var CG = new TriangleNet.Geometry.Point(0, 0);
                    GenerateMesh(ref CG); // покрыть сечение сеткой
                    //
                    CalcNDM(BeamSection.Ring, useRebar);
                }
                else if (m_BeamSection == BeamSection.None)
                {
                    CalcDeformNDM();
                }
                else
                {
                    throw new Exception("Тип сечения не поддерживается");
                }
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }

            CreateReportNDM();
        }

        // сохранить усилия
        private void btnEffortsRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                GetEffortsFromForm(out Dictionary<string, double> _MNQ);

                Efforts ef = new Efforts() { Id = 1, Mx = _MNQ["Mx"], My = _MNQ["My"], N = _MNQ["N"], Qx = _MNQ["Qx"], Qy = _MNQ["Qy"] };

                Lib.BSData.SaveEfforts(ef);
            }
            catch (Exception _ex)
            {
                MessageBox.Show(_ex.Message);
            }
        }

        
        // сохранить данные с формы
        private void FormParamsSaveData()
        {
            FormParams formParams = new FormParams()
            {
                ID = 1,
                Length = double.Parse(tbLength.Text),
                LengthCoef = Convert.ToDouble(cmbEffectiveLengthFactor.SelectedItem),
                BetonType = comboBetonType.SelectedItem.ToString(),
                Fib_i = cmbFib_i.SelectedItem.ToString(),
                Bft3n = cmbBetonClass.Text,
                Bfn = cmbBfn.Text,
                Bftn = cmbBftn.Text,
                Eb = numE_beton.Value.ToString(),
                Efbt = numE_fiber.Value.ToString(),
                Rs = Convert.ToString(cmbRebarClass.SelectedItem),
                Rsw = Convert.ToString(cmbTRebarClass.SelectedItem),
                Area_s = (double)numAs.Value,
                Area1_s = (double)numAs1.Value,
                a_s = (double)num_a.Value,
                a1_s = (double)num_a1.Value
            };

            BSData.UpdateFormParams(formParams);
        }


        // сохранить геометрические размеры
        private void btnSaveParams_Click(object sender, EventArgs e)
        {
            try
            {
                FormParamsSaveData();

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
                Beton bt = Lib.BSQuery.HeavyBetonTableFind(cmbBfn.Text);
                numRfb_n.Value = (decimal)BSHelper.MPA2kgsm2(bt.Rbn);
                numE_beton.Value = (decimal)BSHelper.MPA2kgsm2(bt.Eb * 1000);

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
            numEps_fbt3.Value = (decimal) BSMatFiber.NumEps_fbt3((double)numRfbt2n.Value, (double) numRfbt3n.Value);
        }

        private void numRfbt3n_ValueChanged(object sender, EventArgs e)
        {
            labelRfbt3nMPa.Text = string.Format("{0} МПа ", BSHelper.Kgsm2MPa((double)numRfbt3n.Value));

            numEps_fbt3.Value = (decimal) BSMatFiber.NumEps_fbt3((double) numRfbt2n.Value, (double) numRfbt3n.Value);
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
            m_SectionChart = new BSSectionChart();
            m_SectionChart.BSBeamSection = m_BeamSection;
            m_SectionChart.UseRebar = checkBoxRebar.Checked;
            var sz = BeamWidtHeight(out double b, out double h, out double _area);

            m_SectionChart.RebarClass = cmbRebarClass.SelectedItem.ToString();
            m_SectionChart.Wdth = (float)b;
            m_SectionChart.Hght = (float)h;
            m_SectionChart.Sz = sz;
            m_SectionChart.NumArea = _area;           
            m_SectionChart.ShowDialog();// .Show();

            m_ImageStream = m_SectionChart.GetImageStream;
        }

        private void ShowMosaic(BSCalcResultNDM _CalcResNDM)
        {
            int mode = comboMosaic.SelectedIndex;

            if (mode == 1)
            {
                ShowMosaic(mode, _CalcResNDM.Eps_B, _CalcResNDM.Eps_S, (double)numEps_fbt_ult.Value, -(double)numEps_fb_ult.Value, _CalcResNDM.Rs);
            }
            else if (mode == 2)
            {
                ShowMosaic(mode, _CalcResNDM.Sig_B, _CalcResNDM.Sig_S,  _CalcResNDM.Rfbt, BSHelper.kgssm2kNsm(_CalcResNDM.Rfb), BSHelper.kgssm2kNsm(_CalcResNDM.Rs));
            }
            else if (mode == 3)
            {
                ShowMosaic(mode, _CalcResNDM.Eps_B, _CalcResNDM.Eps_S);
            }
            else if (mode == 4)
            {
                ShowMosaic(mode, _CalcResNDM.Sig_B, _CalcResNDM.Sig_S);
            }
        }


        /// <summary>
        ///  Разбиение сечения на конечные элементы
        /// </summary>
        /// <param name="_valuesB">значения для бетона</param>
        /// <param name="_valuesB">значения для арматуры</param>
        private void ShowMosaic(int _Mode = 0,
                                List<double> _valuesB = null, 
                                List<double> _valuesS = null,
                                double _ultMax = 0,
                                double _ultMin = 0,
                                double _ultRs = 0,
                                double _e_st_ult = 0,
                                double _e_s_ult = 0)
        {
            MeshDraw mDraw;

            double[] sz = BeamSizes();

            if (BSHelper.IsRectangled(m_BeamSection))
            {
                mDraw = new MeshDraw((int)numMeshNX.Value, (int)numMeshNX.Value);
                mDraw.MosaicMode = _Mode;
                mDraw.UltMax = _ultMax;
                mDraw.UltMin = _ultMin;
                mDraw.Rs_Ult = _ultRs;
                mDraw.e_st_ult = _e_st_ult;
                mDraw.e_s_ult  = _e_s_ult;
                mDraw.Values_B = _valuesB;
                mDraw.Values_S = _valuesS;
                mDraw.CreateRectanglePlot(sz, m_BeamSection);
                mDraw.ShowMesh();
            }
            else if (m_BeamSection == BeamSection.Ring)
            {
                TriangleNet.Geometry.Point cg = new TriangleNet.Geometry.Point();
                _= GenerateMesh(ref cg);

                mDraw = new MeshDraw(Tri.Mesh);
                mDraw.MosaicMode = _Mode;
                mDraw.UltMax = _ultMax;
                mDraw.UltMin = _ultMin;
                mDraw.Rs_Ult = _ultRs;
                mDraw.e_st_ult = _e_st_ult;
                mDraw.e_s_ult = _e_s_ult;
                mDraw.Values_B = _valuesB;
                mDraw.Values_S = _valuesS;
                mDraw.PaintSectionMesh();
                mDraw.ShowMesh();                
            }
        }

        // <summary>
        /// Покрыть сечение сеткой
        /// </summary>
        private string GenerateMesh(ref TriangleNet.Geometry.Point _CG)
        {
            string pathToSvgFile;

            double[] sz = BeamWidtHeight(out double b, out double h, out double area);
            double meshSize = (double)numMeshNX.Value;

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
                Rt2_n = (double)numRfbt2n.Value;    // Rfbt2_n
                Rt3_n = (double)numRfbt3n.Value;    // Rfbt3_n
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

        /// <summary>
        ///  Закрытие формы. Сохраняются значения формы
        /// </summary>        
        private void CloseFiberMainForm(object sender, FormClosingEventArgs e)
        {
            try
            {
                FormParamsSaveData();

                BSData.UpdateBeamSectionGeometry(m_InitBeamSectionsGeometry);

                GetEffortsFromForm(out Dictionary<string, double> _MNQ);

                Lib.BSData.SaveEfforts(new Efforts() { Id = 1, Mx = _MNQ["Mx"], My = _MNQ["My"], N = _MNQ["N"], Qx = _MNQ["Qx"], Qy = _MNQ["Qy"]});
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
        }
              
        private void numEs_ValueChanged(object sender, EventArgs e)
        {
            labelEsMPa.Text = string.Format("{0} МПа ", BSHelper.Kgsm2MPa((double)numEs.Value));
            numEpsilonS1.Value = BSMatRod.NumEps_s1(numRs.Value, numEs.Value);
            numEpsilonS0.Value = BSMatRod.NumEps_s0(numRs.Value, numEs.Value);
        }
        
        private void numRsc_ValueChanged(object sender, EventArgs e)
        {
            labelRsсMPa.Text = string.Format("{0} МПа ", BSHelper.Kgsm2MPa((double)numRsc.Value));
            
        }

        /// <summary>
        /// СП360 6.1.24
        /// пересекается с пунктом  6.1.25
        /// </summary>        
        private void numEps_fbt3_ValueChanged(object sender, EventArgs e)
        {
            numEps_fbt_ult.Value = numEps_fbt3.Value; // требует уточнения
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
        
        // модуль упругости для фибробетона на растяжение
        private void numE_fiber_ValueChanged(object sender, EventArgs e)
        {
            numEps_fbt0.Value = BSMatFiber.NumEps_fbt0(numRfbt_n.Value, numE_fiber.Value);
            numEps_fbt1.Value = numEps_fbt0.Value + 0.0001m;
            numEps_fb1.Value = BSMatFiber.NumEps_fb1(numRfb_n.Value, numE_fiber.Value);

            numEfb.Value = numE_fiber.Value;
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
        
        private void labelMNQ_Click(object sender, EventArgs e)
        {
            //MessageBox.Show($"My = {_MNQ["My"]} Кн*см\n Mx = {_MNQ["Mx"]} Кн*см\n N = {_MNQ["N"]} Кн\n Q = {_MNQ["Q"]} Кн", "Усилия"); 
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
                ShowMosaic(comboMosaic.SelectedIndex);
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
        }

        // СП63 П 6.1.25 
        private void numEps_fbt1_ValueChanged(object sender, EventArgs e)
        {
            //numEps_fbt_ult.Value = numEps_fbt1.Value;
        }
       
        private void lbE_beton_info_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Модуль упругости на сжатие: \n " +
                $"{BSHelper.Kgsm2MPa((double) numE_beton.Value ) } МПа \n" +
                $"{BSHelper.Kgssm2ToKNsm2((double)numE_beton.Value, 2)} КН/см2 \n" +
                $"СП 63. Таблица 6.11", 
                "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    
        private void lbE_fb_info_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Модуль упругости на растяжение \n" +
                $"{BSHelper.Kgsm2MPa((double)numE_fiber.Value)} МПа \n" +
                $"{BSHelper.Kgssm2ToKNsm2((double)numE_fiber.Value, 2)} КН/см2 \n", 
                "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void lbE_beton_info_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.Hand;
        }

        private void lbE_fb_info_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.Hand;
        }
        
        // создать сечение произвольной формы
        private void btnSectionAdd_Click(object sender, EventArgs e)
        {
            BSSectionChart sectionChart = new BSSectionChart
            {
                BSBeamSection = BeamSection.None,
                Wdth = 0,
                Hght = 0,
                NumArea = 0
            };

            sectionChart.Show();
        }

        private void picEffortsSign_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.Hand; 
        }

        private void picEffortsSign_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Центр координат находтится в приведенном центре тяжести сечения. X0Y - плоскость сечения, Z - вдоль оси сечения \n " +
                            $"Задавать знаки усилий следует: \n" +
                            $"N > 0 - сжатие \n; My > 0 - растягивает нижние волокна \n;" +
                            $"Qx > 0 вращает правую отсеченную часть по часовой стрелке ", "Система координат", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
        // Трещиностойкость
        private void btnNDMCrc_Click(object sender, EventArgs e)
        {
            BSCalcNDMCrc calcNDMCrc = new BSCalcNDMCrc();
            calcNDMCrc.Show();
        }

        private void cmbForceUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cmbForceUnit.SelectedIndex;
            _UnitConverter.ChangeCustomUnitForce(index);

            // добавление ед изм в HeaderText колонки с силами 
            DataGridViewColumnCollection columns = gridEfforts.Columns;
            for (int i = 0; i < columns.Count; i++)
            {
                string columnName = columns[i].Name;
                string headerText = columns[i].HeaderText;
                columns[i].HeaderText = _UnitConverter.ChangeHT4ForForce(headerText);
            }


        }
        private void cmbMomentOfForceUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cmbMomentOfForceUnit.SelectedIndex;
            _UnitConverter.ChangeCustomUnitMomentOfForce(index);

            // добавление ед изм в HeaderText колонки с силами 
            DataGridViewColumnCollection columns = gridEfforts.Columns;
            for (int i = 0; i < columns.Count; i++)
            {
                string columnName = columns[i].Name;
                string headerText = columns[i].HeaderText;
                columns[i].HeaderText = _UnitConverter.ChangeHTForMomentOfForce(headerText);
            }

        }
    }
}

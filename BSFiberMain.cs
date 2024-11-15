using BSBeamCalculator;
using BSCalcLib;
using BSFiberConcrete.CalcGroup2;
using BSFiberConcrete.Control;
using BSFiberConcrete.DeformationDiagram;
using BSFiberConcrete.Lib;
using BSFiberConcrete.Report;
using BSFiberConcrete.Section;
using BSFiberConcrete.Section.MeshSettingsOfBeamSection;
using BSFiberConcrete.UnitsOfMeasurement;
using BSFiberConcrete.UnitsOfMeasurement.PhysicalQuantities;
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
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BSFiberConcrete
{
    /// <summary>
    /// Форма: Фибробетон
    /// </summary>
    public partial class BSFiberMain : Form
    {
        private DataTable m_Table { get; set; }
        public CalcType CalcType { get; set; }

        private Dictionary<string, double> m_Iniv;
        private BSFiberCalculation BSFibCalc { get; set; }
        private BSFiberLoadData m_BSLoadData { get; set; }
        //арматура
        private List<Rebar> m_Rebar { get; set; }
        //материал (фибробетон)
        private BSMatFiber m_MatFiber { get; set; }
        private List<Elements> FiberConcrete { get; set; }
        private List<Beton> m_Beton { get; set; }
        private List<RebarDiameters> m_RebarDiameters { get; set; }

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
        /// Содержит в себе комбинацию загружений для расчета 
        /// </summary>
        private Dictionary<string, double> m_EffortsForCalc;

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
        private BSSectionChart m_SectionChart { get; set; }

        // изображение сечения на форме picBeton
        private MemoryStream m_ImageStream { get; set; }

        private LameUnitConverter _UnitConverter;

        //private ControllerBeamDiagram _beamDiagramController;
        private BeamCalculatorViewModel _beamCalcVM;

        private bool UseRebar => checkBoxRebar.Checked;

        private MeshSectionSettings _beamSectionMeshSettings;


        /// <summary>
        /// конструктор
        /// </summary>
        public BSFiberMain()
        {
            InitializeComponent();

            m_Beam       = new Dictionary<string, double>();
            m_Table      = new DataTable();
            m_BSLoadData = new BSFiberLoadData();
            m_MatFiber   = new BSMatFiber();
            m_Iniv       = new Dictionary<string, double>() { 
                ["Mx"] = 0, ["My"] = 0, ["N"] = 0, ["Qx"] = 0, ["Qy"] = 0, ["eN"] = 0, ["Ml"] = 0 
            };
        }

        [Conditional("DEBUG")]
        private void IsDebugCheck(ref bool isDebug)
        {
            isDebug = true;        
        }

        [Conditional("RELEASE")]
        private void IsReleaseCheck(ref bool isDebug)
        {
            isDebug = true;
        }


        /// <summary>
        /// Дизайн формы в зависимости от вида расчета
        /// </summary>
        private void CalcTypeShow()
        {
            bool isDebug = false;
            IsDebugCheck(ref isDebug);

            if (!isDebug)
            {
                tabFiber.TabPages.Remove(tabPageAdmin);
                btnSaveFormData.Visible = false;
            }
           
            //RefreshSectionChart();

            if (CalcType == CalcType.Static)
            {
                btnStaticEqCalc.Visible = true;
                btnCalc_Deform.Visible = false;
                gridEfforts.Columns["Mx"].Visible = false;
                gridEfforts.Columns["Qy"].Visible = false;
                tabFiber.TabPages.Remove(tabPageNDM);
                tabFiber.TabPages.Remove(tabPBeam);
                btnCustomSection.Enabled = false;
                btnMeshSettings.Visible = false;

                labelCalculation.Text = BSFiberLib.TxtStaticEqCalc;
                tableLayoutPanelLRebar.Visible = true;

                num_s_w_Y.Enabled = false;
                cmbDw_Y.Enabled = false;
                numN_w_Y.Enabled = false;

            }
            else if (CalcType == CalcType.Nonlinear)
            {
                btnStaticEqCalc.Visible = false;
                btnCalc_Deform.Visible = true;
                gridEfforts.Columns["Mx"].Visible = true;
                gridEfforts.Columns["Qy"].Visible = true;
                tabFiber.TabPages.Remove(tabPBeam);                
                btnCustomSection.Enabled = true;
                btnMeshSettings.Visible = true;

                labelCalculation.Text = BSFiberLib.TxtCalc_Deform;
                tableLayoutPanelLRebar.Visible = false;
            }
            else if (CalcType == CalcType.BeamCalc)
            {
                tabFiber.TabPages.Remove(tabStrength);

                btnStaticEqCalc.Visible = false;
                btnCalc_Deform.Visible = true;
                tbLength.Enabled = false;

                gridEfforts.Rows.Clear();
                gridEfforts.Rows.Add(2);
                gridEfforts.Columns["Mx"].Visible = false;
                gridEfforts.Columns["Qy"].Visible = false;
                gridEfforts.Columns["N"].Visible = false;
                //for (int i = 0; i < gridEfforts.ColumnCount; i++)
                //{
                //    gridEfforts[i, 0].Value = "0";
                //}


                m_Path2BeamDiagrams = new List<string>() { };


                InitForBeamDiagram savedValues = BSData.LoadForBeamDiagram();
                List<double> initValues;
                if (savedValues != null)
                { initValues = new List<double>() { savedValues.LengthX, savedValues.Force }; }
                else { initValues = new List<double>() { 0, 0 }; }
                
                _beamCalcVM = new BeamCalculatorViewModel(tbLength, gridEfforts, initValues);
                BeamCalculatorControl beamCalculatorControl = new BeamCalculatorControl(_beamCalcVM);
                tabPBeam.Controls.Add(beamCalculatorControl);
            }
        }

        /// <summary>
        ///  Заполнить поля значениями по умолчанию
        /// </summary>
        private void InitFormControls()
        {
            FormParams prms = BSData.LoadFormParams();

            tbLength.Text                 = Convert.ToString(prms.Length);
            cmbEffectiveLengthFactor.Text = Convert.ToString(prms.LengthCoef);

            cmbFib_i.Text         = prms.Fib_i;
            comboBetonType.Text   = prms.BetonType;
            cmbBetonClass.Text    = prms.Bft3n;
            cmbBfn.Text           = prms.Bfn;
            cmbBftn.Text          = prms.Bftn;
            //numE_beton.Text = prms.Eb;
            //numE_fiber.Text = prms.Efbt;
            cmbRebarClass.Text    = prms.Rs;
            
            numAs.Value = (decimal)prms.Area_s;
            numAs1.Value = (decimal)prms.Area1_s;
            num_a.Value = (decimal)prms.a_s;
            num_a1.Value = (decimal)prms.a1_s;
        }

        /// <summary>
        /// коэффициенты надежности
        /// </summary>
        private void InitStrengthFactors()
        {
            StrengthFactors strengthFactors = BSFiberLib.StrengthFactors();

            numYft.Value = (decimal)strengthFactors.Yft;
            numYb.Value = (decimal)strengthFactors.Yb;
            numYb1.Value = (decimal)strengthFactors.Yb1;
            numYb2.Value = (decimal)strengthFactors.Yb2;
            numYb3.Value = (decimal)strengthFactors.Yb3;
            numYb5.Value = (decimal)strengthFactors.Yb5;
        }

        /// <summary>
        /// подсказки
        /// Установка высплывающего текста
        /// </summary>
        private void InitToolTips()
        {           
            System.Windows.Forms.ToolTip tlTip = new System.Windows.Forms.ToolTip();            
            tlTip.AutoPopDelay = 5000;
            tlTip.InitialDelay = 1000;
            tlTip.ReshowDelay = 50;            
            tlTip.ShowAlways = true;
            
            tlTip.SetToolTip(this.btnRectang, "Прямоугольное сечение");
            tlTip.SetToolTip(this.btnTSection, "Тавровое сечение \"Верхняя полка\"");
            tlTip.SetToolTip(this.btnLSection, "Тавровое сечение \"Нижняя полка\"");
            tlTip.SetToolTip(this.btnIBeam, "Двутавровое сечение");
            tlTip.SetToolTip(this.btnRing, "Кольцевое сечение");
            tlTip.SetToolTip(btnCustomSection, "Произвольное сечение");
            tlTip.SetToolTip(btnCalcResults, "Результаты расчета");
            tlTip.SetToolTip(btnMeshSettings, "Настройка густоты расчетной сетки");
            tlTip.SetToolTip(this.btnStaticEqCalc, BSFiberLib.TxtStaticEqCalc);
            tlTip.SetToolTip(this.btnCalc_Deform, BSFiberLib.TxtCalc_Deform);
        }

        // арматура
        private void InitRebarValues()
        {
            // настройки из БД
            Rebar dbRebar = m_Rebar.Where(x => x.ID == Convert.ToString(cmbRebarClass.SelectedItem))?.First();
            numEs.Value = (decimal)BSHelper.MPA2kgsm2(dbRebar.Es);
            numAs.Value = (decimal)dbRebar.As;
            numAs1.Value = (decimal)dbRebar.As1;
            num_a.Value = (decimal)dbRebar.a;
            num_a1.Value = (decimal)dbRebar.a1;
        }

        /// <summary>
        /// Mx My N Qx Qy
        /// </summary>
        private void InitEffortValues()
        {
            List<Efforts> eff = Lib.BSData.LoadEfforts();
            if (eff.Count == 0) return;

            // словарь с назщваниями столбцов и индексами в таблице gridEfforts
            Dictionary<string, int> columnNameAndIndex = new Dictionary<string, int>();

            for (int i = 0; gridEfforts.Columns.Count > i; i++)
            { 
                columnNameAndIndex.Add(gridEfforts.Columns[i].Name, i); 
            }

            // Добавление строк в таблицу gridEfforts
            gridEfforts.Rows.Clear();
            gridEfforts.Rows.Add(eff.Count);

            for (int iRow = 0; eff.Count > iRow; iRow++)
            {
                for (int iProperty = 0; typeof(Efforts).GetProperties().Count() > iProperty; iProperty++)
                {
                    string propertyEffortName = typeof(Efforts).GetProperties()[iProperty].Name;
                    
                    if (columnNameAndIndex.TryGetValue(propertyEffortName, out int indexCol))
                    {
                        double propertyEffortValue = (double)eff[iRow].GetType().GetProperty(propertyEffortName).GetValue(eff[iRow], null);
                        //dataGridViewRow.Cells[propertyEffortName].Value = propertyEffortValue;
                        //System.Windows.Forms.DataGridViewCellCollection a = dataGridViewRow.Cells;
                        gridEfforts.Rows[iRow].Cells[indexCol].Value = propertyEffortValue;
                    }
                }
            }

            num_eN.Value = (decimal)m_Iniv["eN"];
            num_Ml1_M1.Value = (decimal)m_Iniv["Ml"];
        }

        /// <summary>
        /// Начальное состояние элементов управления
        /// </summary>
        private void InitBetonControls()
        {
            cmbBetonClass.DataSource = BSFiberLib.BetonList;
            cmbBetonClass.DisplayMember = "Name";
            cmbBetonClass.ValueMember = "Name";
            cmbBetonClass.SelectedValue = BSFiberLib.BetonList[5].Name;

            m_Beton = BSData.LoadBetonData(0);
            cmbBfn.DataSource = m_Beton;
            cmbBfn.DisplayMember = "BT";
            cmbBfn.ValueMember = "BT";
            
            cmbBftn.DataSource = BSData.LoadFiberBft();
            cmbBftn.DisplayMember = "ID";
            cmbBftn.ValueMember = "ID";
            cmbBftn.SelectedValue = "Bft3";
        }

        private void DefaultMaterialParameters()
        {
            cmbFib_i.SelectedIndex        = 0;
            comboBetonType.SelectedIndex  = 0;
            cmbRebarClass.SelectedItem    = BSFiberLib.RebarClassDefault;
            cmbTypeMaterial.SelectedIndex = 0;
            cmbTRebarClass_X.SelectedItem = BSFiberLib.RebarClassDefault;
            cmbTRebarClass_Y.SelectedItem = BSFiberLib.RebarClassDefault;
        }

        // глобальные настройки
        public void BSFiberMain_Load(object sender, EventArgs e)
        {
            try
            {
                InitToolTips();
               
                m_RebarDiameters = BSData.LoadRebarDiameters();                
                m_Rebar          = BSData.LoadRebar();
                FiberConcrete    = BSData.LoadFiberConcreteTable();

                dataGridSection.DataSource = m_Table;   
                                
                DefaultMaterialParameters();

                cmbDeformDiagram.SelectedIndex = (int)DeformDiagramType.D3Linear;
                
                m_BSLoadData.InitEffortsFromDB(ref m_Iniv);
                
                m_BSLoadData.ReadParamsFromJson();

                m_MatFiber.e_b2 = m_BSLoadData.Beton2.eps_b2;
                
                m_InitBeamSectionsGeometry = Lib.BSData.LoadBeamSectionGeometry(m_BeamSection);

                numRandomEccentricity.Value = 0;

                LoadRectangle();

                InitBetonControls();
                
                InitStrengthFactors();
                
                InitFormControls();

                InitEffortValues();
                
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

                InitRebarValues();
                
                CalcTypeShow();

                NDMSetupInitFormValues();
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
            else if (BSHelper.IsITL(m_BeamSection))
            {
                //_w = Math.Max(sz[0], sz[4]);
                _w = sz[2];
                _h = sz[1] + sz[3] + sz[5];
                _area = sz[0] * sz[1] + sz[2] * sz[3] + sz[4] * sz[5];
            }
            else if (m_BeamSection == BeamSection.Any)
            {
                _w = 0;
                _h = 0;
                _area = 0;
            }
            else
            {
                throw new Exception("Неопределен тип сечения");
            }

            return sz;
        }

        //  , заданные пользователем 
        private void InitStrengthFactorsFromForm(double[] prms)
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
                prms[++idx] = 0;
            }
        }
        /// <summary>
        /// длина балки, коэф расчетной длины
        /// </summary>        
        private (double, double) BeamLength()
        {
            double.TryParse(tbLength.Text, out double lgth);
            double.TryParse(cmbEffectiveLengthFactor.Text, out double coeflgth);

            return (lgth, coeflgth);
        }

        /// <summary>
        /// Расчетная длина балки 
        /// </summary>
        /// <param name="_init"></param>
        /// <returns></returns>
        private double InitBeamLength(bool _beaminit = false)
        {
            double lgth, coeflgth;
            (lgth, coeflgth) = BeamLength();

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

        /// <summary>
        /// Задать свойства фибробетона по данным, введенным пользователем
        /// </summary>
        private void SetFiberMaterialProperties()
        {
            // Сжатие Rfb
            Beton fb = (Beton) cmbBfn.SelectedItem;

            // Растяжение Rfbt
            //FiberBft fbt = (FiberBft)cmbBftn.SelectedItem;
            m_BSLoadData.Fiber.E_fbt = (double)numE_fbt.Value;

            // сжатие:
            m_MatFiber.B = fb.B;
            m_MatFiber.Rfbn = (double) numRfb_n.Value;

            // модуль упругости
            m_MatFiber.Efb = (double)numE_fbt.Value;

            // растяжение:            
            m_MatFiber.Rfbtn = (double)numRfbt_n.Value; // кг/см2           
            m_MatFiber.Rfbt2n = (double)numRfbt2n.Value; // кг/см2
            m_MatFiber.Rfbt3n = (double)numRfbt3n.Value; // кг/см2

            // модуль упругости
            m_MatFiber.Efbt = (double)numE_fbt.Value;
        }
       
        private void FiberReport_N(BSFiberCalc_MNQ fiberCalc,  int _irep = 1)
        {
            BSFiberReport_N report = new BSFiberReport_N()
            {
                BeamSection = m_BeamSection,
                Messages = m_Message
            };

            report.InitFromFiberCalc(fiberCalc);
            // результат расчета по первой группе предельных состояний
            report.CalcResults1Group = fiberCalc.CalcResults;
            // результат расчета по второй группе предельных состояний
            report.CalcResults2Group = m_CalcResults2Group;

            report._unitConverter = _UnitConverter;
        
            string pathToHtmlFile = report.CreateReport(_irep);

            System.Diagnostics.Process.Start(pathToHtmlFile);
        }


        /// <summary>
        /// Расчет прочности сечения на действие момента
        /// </summary>        
        private BSFiberReportData FiberCalculate_M(double _M, double[] _prms)
        {
            bool useReinforcement = checkBoxRebar.Checked;
            bool calcOk;
            BSFiberReportData reportData = new BSFiberReportData();

            try
            {
                BSFibCalc = BSFiberCalculation.construct(m_BeamSection, useReinforcement);
                BSFibCalc.MatFiber = m_MatFiber;
                InitRebar(BSFibCalc);
                
                BSFibCalc.SetParams(_prms);
                BSFibCalc.SetSize(BeamSizes());
                BSFibCalc.Efforts = new Dictionary<string, double> { { "My", _M } };

                calcOk = BSFibCalc.Calculate();
                if (calcOk)                
                    reportData.InitFromBSFiberCalculation(BSFibCalc, _UnitConverter);                   
                
                // запуск расчет по второй группе предельных состояний
                var  FibCalcGR2 = FiberCalculate_Cracking();
                reportData.m_Messages.AddRange(FibCalcGR2.Msg);
                reportData.m_CalcResults2Group = FibCalcGR2.Results();

                return reportData;
            }
            catch (Exception _e)
            {
                MessageBox.Show("Ошибка в расчете: " + _e.Message);
                return null;
            }           
        }

        /// <summary>
        /// Расчеты стельфибробетона по предельным состояниям второй группы
        /// 1) Расчет предельного момента образования трещин
        /// 2) Расчет ширины раскрытия трещины
        /// </summary>        
        private BSFiberCalc_Cracking FiberCalculate_Cracking()
        {
            bool calcOk;
            try
            {                                
                BSBeam   bsBeam = BSBeam.construct(m_BeamSection);
                
                bsBeam.SetSizes(BeamSizes());
                
                Dictionary<string, double> MNQ = GetEffortsForCalc();

                BSFiberCalc_Cracking  calc_Cracking = new BSFiberCalc_Cracking(MNQ)
                {
                    Beam = bsBeam,
                    typeOfBeamSection = m_BeamSection
                };

                // задать тип арматуры
                calc_Cracking.MatRebar = new BSMatRod((double)numEs.Value)
                {
                    RCls  = cmbRebarClass.Text,
                    Rs    = (double)numRs.Value,
                    e_s0  = 0,
                    e_s2  = 0,
                    As    = (double)numAs.Value,
                    As1   = (double)numAs1.Value,
                    a_s   = (double)num_a.Value,
                    a_s1  = (double)num_a1.Value,
                    Reinforcement = checkBoxRebar.Checked
                };

                SetFiberMaterialProperties();
                
                calc_Cracking.MatFiber = m_MatFiber;
                calc_Cracking.SetParams(new double[] { 10, 1 });                

                calcOk = calc_Cracking.Calculate();
                
                if (m_Message == null) m_Message = new List<string>();
                m_Message.AddRange(calc_Cracking.Msg);

                if (calcOk)
                    m_CalcResults2Group = calc_Cracking.Results();

                return calc_Cracking;
            }
            catch (Exception _e)
            {                
                MessageBox.Show("Ошибка в расчете: " + _e.Message);
            }

            return null;
        }

        private void InitReportSections(ref BSFiberReport report)
        {
            report.Beam = m_Beam;
            report.Coeffs = m_Coeffs;
            report.Efforts = m_Efforts;
            report.GeomParams = m_GeomParams;
            report.PhysParams = m_PhysParams;
            report.Reinforcement = m_Reinforcement;
            report.CalcResults1Group = m_CalcResults;
            report.CalcResults2Group = m_CalcResults2Group;
            report.ImageStream = m_ImageStream;
            report.Messages = m_Message;
            report._unitConverter = _UnitConverter;
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

                report.BeamSection = _BeamSection;
                report.UseReinforcement = _useReinforcement;

                InitReportSections(ref report);

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
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            picBeton.Image = global::BSFiberConcrete.Properties.Resources.FiberBeton;
            picBeton.SizeMode = PictureBoxSizeMode.StretchImage;

            RefreshSectionChart(m_BeamSection);
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

            RefreshSectionChart(m_BeamSection);
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
            { 
                column.SortMode = DataGridViewColumnSortMode.NotSortable; 
            }
            picBeton.Image = global::BSFiberConcrete.Properties.Resources.Ring;

            RefreshSectionChart(m_BeamSection);
        }

        // двутавровое сечение
        private void btnIBeam_Click(object sender, EventArgs e)
        {
            m_BeamSection = BeamSection.IBeam;
            dataGridSection.DataSource = null;

            m_Table = FiberMainFormHelper.GetTableFromBeamSections(m_InitBeamSectionsGeometry, m_BeamSection);
            dataGridSection.DataSource = m_Table;

            foreach (DataGridViewColumn column in dataGridSection.Columns) 
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            picBeton.Image = global::BSFiberConcrete.Properties.Resources.IBeam;
            picBeton.SizeMode = PictureBoxSizeMode.StretchImage;

            RefreshSectionChart(m_BeamSection);
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
        /// <param name="_MNQ">список строк с значениями усилий</param>
        /// <exception cref="Exception"></exception>
        private void GetEffortsFromForm(out List<Dictionary<string, double>> _MNQ)
        {            
            _MNQ = new List<Dictionary<string, double>>();

            DataGridViewColumnCollection columns = gridEfforts.Columns;
            DataGridViewRowCollection row = gridEfforts.Rows;

            for (int j = 0; j < row.Count; j++)
            {
                bool isZeroValues = true;
                Dictionary<string, double> tmpEfforts = new Dictionary<string, double>();
                for (int i = 0; i < columns.Count; i++)
                {
                    string tmpName = columns[i].Name;

                    if (!columns[i].Visible)
                    { 
                        tmpEfforts.Add(tmpName, 0);
                        continue;
                    }
                    
                    double value = Convert.ToDouble(gridEfforts.Rows[j].Cells[i].Value);
                    isZeroValues &= (value == 0);
                    
                    // перевод ед измерения
                    double newValue = _UnitConverter.ConvertEfforts(tmpName, value);
                    tmpEfforts.Add(tmpName, newValue);
                }

                // Если все значения 0, то не записываем в список
                if (!isZeroValues)
                {
                    tmpEfforts["Ml"] = (double)num_Ml1_M1.Value;
                    tmpEfforts["eN"] = (double)num_eN.Value;
                    tmpEfforts["e0"] = (double)numRandomEccentricity.Value;
                    _MNQ.Add(tmpEfforts);
                }                
            }

            //if (_MNQ.Count == 0)
            //{
            //    //_MNQ = new List<Dictionary<string, double>>() {new Dictionary<string, double>{ ["My"] = 0, ["Mx"] = 0, ["N"] = 0, ["Qx"] = 0, ["Qy"] = 0, ["Ml"] = 0, ["eN"] = 0, ["e0"] = 0}};
            //}
        }

        /// <summary>
        /// Получить нагрузки для расчета
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, double> GetEffortsForCalc()
        {
            if (m_EffortsForCalc != null)
            { 
                return m_EffortsForCalc; 
            }

            GetEffortsFromForm(out List<Dictionary<string, double>> _MNQ);

            if (_MNQ.Count > 0)
                return _MNQ[0];

            return new Dictionary<string, double>() { };// ["My"] = 0, ["Mx"] = 0, ["N"] = 0, ["Qx"] = 0, ["Qy"] = 0, ["Ml"] = 0, ["eN"] = 0, ["e0"] = 0 };
        }

        /// <summary>
        ///  Введенные пользователем значения по арматуре
        /// </summary>
        /// <param name="_Rebar">Установлены параметры стержней арматуры </param>
        private Rebar InitRebarFromForm()
        {
            double Asw_X = (double)numN_w_X.Value * BSHelper.AreaCircle(double.Parse(cmbDw_X.SelectedItem.ToString()) / 10.0);
            double Asw_Y = (double)numN_w_X.Value * BSHelper.AreaCircle(double.Parse(cmbDw_Y.SelectedItem.ToString()) / 10.0);

            // Арматура из настроек формы
            Rebar _Rebar = new Rebar()
            {
                // ПРОДОЛЬНАЯ
                // модуль упругости
                Es = (double)numEs.Value,

                // Площадь
                // растянутая
                As  = (double)numAs.Value,
                // сжатая
                As1 = (double)numAs1.Value,
                // расстояние до ц.т.
                // растянутая
                a = (double)num_a.Value,
                // сжатая
                a1 = (double)num_a1.Value,
                                
                // поперечная по X
                Rsw_X   = (double)numRsw_X.Value,
                Esw_X   = (double)numEsw_X.Value,
                Sw_X    = (double)num_s_w_X.Value,
                Asw_X   = Asw_X,
                
                // поперечная по Y
                Rsw_Y = (double)numRsw_Y.Value,
                Esw_Y = (double)numEsw_Y.Value,                
                Sw_Y  = (double)num_s_w_Y.Value,
                Asw_Y = Asw_Y,                
            };

            return _Rebar;
        }

        /// <summary>
        ///  Введенные пользователем значения по арматуре
        /// </summary>
        /// <param name="_Rebar"></param>
        private void InitRebarValues(ref Rebar _Rebar)
        {
            // площади растянутой и сжатой арматуры - задаются для метода статического равновесия
            // для расчета по НДМ - через форму задания сечения
            _Rebar.As = (double)numAs.Value;
            _Rebar.As1 = (double)numAs1.Value;
            // расстояния до ц.т.
            _Rebar.a = (double)num_a.Value;
            _Rebar.a1 = (double)num_a1.Value;
            // модули упругости арматуры
            // продольной
            _Rebar.Es = (double)numEs.Value;
            // поперечной
            _Rebar.Esw_X = (double)numEsw_X.Value;
            //TODO Шаг поперечной арматуры
            _Rebar.Sw_X = (double) num_s_w_X.Value;
            _Rebar.Sw_Y = (double)num_s_w_Y.Value;
            // 
            //_Rebar.Asw_X = 1;
            //_Rebar.Asw_Y = 1;
        }
              
        /// <summary>
        ///  Расчет по наклонному сечению на действие Q
        /// </summary>        
        private Dictionary<string, double> FiberCalculate_QxQy(Dictionary<string, double> _MNQ, double[] _sz)
        {                        
            double[] prms = new double[9];
            InitStrengthFactorsFromForm(prms);
            Fiber fiber = new Fiber(m_BSLoadData.Fiber)
            {
                Ef = (double)numEfiber.Value,
                Eb = (double)numE_beton.Value,
                E_fbt = (double)numE_fbt.Value,
                mu_fv = (double)numMu_fv.Value
            };
            var betonType = BSQuery.BetonTypeFind(comboBetonType.SelectedIndex);

            BSFiberCalc_QxQy fiberCalc = new BSFiberCalc_QxQy();
            fiberCalc.MatFiber      = m_MatFiber;
            fiberCalc.UseRebar      = UseRebar;
            fiberCalc.Rebar         = InitRebarFromForm();
            fiberCalc.BetonType     = betonType;
            fiberCalc.UnitConverter = _UnitConverter;
            fiberCalc.SetFiberFromLoadData(fiber);
            fiberCalc.SetSize(_sz);
            fiberCalc.SetParams(prms);
            fiberCalc.SetEfforts(_MNQ);            
            fiberCalc.SetN_Out();

            bool calcOk = fiberCalc.Calculate();

            if (calcOk)
                fiberCalc.Msg.Add("Расчет успешно выполнен!");
            else
                fiberCalc.Msg.Add("Расчет по наклонному сечению на действие Q не выполнен!");

            Dictionary<string, double> xR = fiberCalc.Results();

            return xR;
        }
                       
        // поперечная арматура
        private void InitTRebar(out double[] t_rebar)
        {
            t_rebar = new double[3];

            t_rebar[0] = (double)numRsw_X.Value;
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
            SetFiberMaterialProperties();

            RecalRandomEccentricity_e0();

            GetEffortsFromForm(out List<Dictionary<string, double>> lstMNQ);

            double[] sz = BeamSizes(InitBeamLength(true));

            double[] prms = m_BSLoadData.Params;
            InitStrengthFactorsFromForm(prms);
           
            if (lstMNQ.Count == 0)
            {
                MessageBox.Show("Необходимо задать нагрузки");
                return;
            }

            Fiber fiber = new Fiber(m_BSLoadData.Fiber) { 
                Ef = (double) numEfiber.Value, Eb = (double)numE_beton.Value, E_fbt = (double)numE_fbt.Value, mu_fv = (double)numMu_fv.Value 
            };

            var betonType = BSQuery.BetonTypeFind(comboBetonType.SelectedIndex);

            // проверка на модули упругости!
            Rebar rebar = (Rebar)m_BSLoadData.Rebar.Clone();
            InitRebarValues(ref rebar);

            List<BSFiberReportData> calcResults_M = new List<BSFiberReportData>();
            List<BSFiberReport_N> calcResults_N = new List<BSFiberReport_N>();

            BSFiberCalc_MNQ FibCalc_MNQ(Dictionary<string, double> _MNQ)
            {
                BSFiberCalc_MNQ fibCalc = BSFiberCalc_MNQ.Construct(m_BeamSection);
                fibCalc.MatFiber      = m_MatFiber;
                fibCalc.UseRebar      = UseRebar;
                fibCalc.Rebar         = UseRebar ? rebar : null;
                fibCalc.BetonType     = betonType;
                fibCalc.UnitConverter = _UnitConverter;
                fibCalc.SetFiberFromLoadData(fiber);
                fibCalc.SetSize(sz);
                fibCalc.SetParams(prms);
                fibCalc.SetEfforts(_MNQ);
                fibCalc.SetN_Out();
                
                return fibCalc;
            }

            int iRep = 0;
            foreach (Dictionary<string, double> _MNQ in lstMNQ)
            {
                (double _M, double _N, double _Qx) = (_MNQ["My"], _MNQ["N"], _MNQ["Qx"]);
                if (_M == 0 && _N == 0 && _Qx == 0) continue;

                if (_M < 0 || _N < 0)
                {
                    MessageBox.Show("Расчет по методу статического равновесия не реализован для отрицательных значений M и N.\n " +
                                    "Воспользуйтесь расчетом по методу НДМ");
                    continue;
                }

                double Mc_ult, UtilRate_Mc;
                double N_ult, UtilRate_N;

                if (_M > 0 && _N == 0 && _Qx == 0)
                {
                    // расчет на чистый изгиб
                    BSFiberReportData  FibCalc_M    = FiberCalculate_M(_M, prms);
                    // расчет по наклонной полосе на действие момента [6.1.7]
                    BSFiberCalc_MNQ    fiberCalc_Mc = FibCalc_MNQ(_MNQ);
                    
                    (Mc_ult, UtilRate_Mc) = fiberCalc_Mc.Calculate_Mc();

                    var resMc =  fiberCalc_Mc.Results_Mc();
                    foreach (var r in resMc)
                        FibCalc_M.m_CalcResults1Group.Add(r.Key, r.Value);

                    // [6.1.7] [6.1.30]                    
                    //FibCalc_M.m_CalcResults1Group.Add("Момент по наклонному сечению", Mc_ult);
                    //FibCalc_M.m_CalcResults1Group.Add("Коэфф исп момента по накл сечению [6.1.30]", UtilRate_Mc);

                    calcResults_M.Add(FibCalc_M);
                }                
                else if (_N > 0 && _Qx == 0)
                {
                    // Расчет по 1 гр пред. сост                    
                    BSFiberCalc_MNQ  fiberCalc_N = FibCalc_MNQ(_MNQ);

                    // расчет на действие сжимающей силы (учитывает заданный изгибающий момент + момент от эксцентриситета N)
                    (N_ult, UtilRate_N) = fiberCalc_N.Calculate_Nz();

                    // [6.1.13] [6.1.30] + проверка на момент по наклонному сечению
                    if (_M != 0) // + M = N*e учесть
                    {
                        (Mc_ult, UtilRate_Mc) = fiberCalc_N.Calculate_Mc();
                    }

                    BSFiberReport_N report = new BSFiberReport_N() { _unitConverter = _UnitConverter };
                    report.InitFromFiberCalc(fiberCalc_N);

                    // расчет по второй группе предельных состояний                    
                    var calcResults2Group    = FiberCalculate_Cracking();
                    report.CalcResults2Group = calcResults2Group.Results();

                    calcResults_N.Add(report);
                }                
                else if (_Qx != 0)
                {
                    // Расчет на действие поперечных сил     
                    // учитывает расчет по накл полосе надействие момента                    
                    BSFiberCalc_MNQ  fiberCalc_Qc = FibCalc_MNQ(_MNQ);

                    // [6.1.27] [6.1.28]
                    (double Qc_ult, double UtilRate_Qc) = fiberCalc_Qc.Calculate_Qcx();
                    if (_N > 0)
                    {
                        // [6.1.13] [6.1.30]
                        (N_ult, UtilRate_N) = fiberCalc_Qc.Calculate_Nz();
                    }

                    if (_M > 0) // + M = N*e учесть
                    {
                        (Mc_ult, UtilRate_Mc) = fiberCalc_Qc.Calculate_Mc();
                    }

                    BSFiberCalc_Cracking calcResults2Group = FiberCalculate_Cracking();
                    fiberCalc_Qc.CalcResults2Group         = calcResults2Group.Results();

                    BSFiberReport_MNQ.FiberReport_Qc(fiberCalc_Qc, ++iRep);                    
                }                                
            }

            BSFiberReport_M.RunMultiReport(ref iRep, calcResults_M);

            BSFiberReport_N.RunMultiReport(ref iRep, calcResults_N);
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
            Dictionary<string, double> MNQ = GetEffortsForCalc();
            
            BSMatFiber mf = new BSMatFiber((double)numE_beton.Value, numYft.Value, numYb.Value, numYb1.Value, numYb2.Value, numYb3.Value, numYb5.Value);
            mf.Rfbn   = (double)numRfb_n.Value;
            mf.Rfbtn  = (double)numRfbt_n.Value;
            mf.Rfbt2n = (double)numRfbt2n.Value;
            mf.Rfbt3n = (double)numRfbt3n.Value;

            double lgth, coeflgth;
            (lgth, coeflgth) = BeamLength();

            Dictionary<string, double> D = new Dictionary<string, double>()
            {
                // enforces
                ["N"]  = MNQ.ContainsKey("N")  ?  -MNQ["N"]: 0,
                ["My"] = MNQ.ContainsKey("My") ?  MNQ["My"]: 0,
                ["Mz"] = MNQ.ContainsKey("Mz") ?  MNQ["Mz"]: 0,
                ["Qx"] = MNQ.ContainsKey("Qx") ?  MNQ["Qx"]: 0,
                ["Qy"] = MNQ.ContainsKey("Qy") ?  MNQ["Qy"]: 0,
                //
                //length
                ["lgth"]     = lgth,
                ["coeflgth"] = coeflgth,
                //
                //section size
                ["b"] = 0,
                ["h"] = 0,

                ["bf"]  = 0,
                ["hf"]  = 0,
                ["bw"]  = 0,
                ["hw"]  = 0,
                ["b1f"] = 0,
                ["h1f"] = 0,

                ["r1"] = 0,
                ["R2"] = 0,
                //
                //Mesh
                ["ny"] = _beamSectionMeshSettings.NY,
                ["nz"] = _beamSectionMeshSettings.NX, // в алгоритме плосткость сечения YOZ

                // beton
                ["Eb0"] = (double)numE_beton.Value, // сжатие
                ["Ebt"] = (double)numE_fbt.Value, // растяжение

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
                ["ebc0"] = 0,
                ["ebc2"] = 0.0035d,
                ["eb_ult"] = (double)numEps_fb_ult.Value,

                // растяжение
                ["ebt0"] = 0,
                ["ebt1"] = 0,
                ["ebt2"] = 0,
                ["ebt3"] = 0,
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
                ["esc2"] = 0,
                ["est2"] = 0,
                ["es_ult"] = (double)numEps_s_ult.Value,
                // коэффициенты надежности
                ["Yft"] = (double)numYft.Value,
                ["Yb"]  = (double)numYb.Value,
                ["Yb1"] = (double)numYb1.Value,
                ["Yb2"] = (double)numYb2.Value,
                ["Yb3"] = (double)numYb3.Value,
                ["Yb5"] = (double)numYb5.Value
            };

            double[] beam_sizes = BeamSizes();

            double b = 0;
            double h = 0;

            if (_beamSection == BeamSection.Rect)
            {
                b = beam_sizes[0];
                h = beam_sizes[1];
            }
            else if (BSHelper.IsITL(_beamSection))
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
        /// Сохранение параметров, введенных пользователем
        /// </summary>
        /// <param name="_D"></param>
        private void DictFormUserParams( Dictionary<string, double> _D)
        {
            _D["BT"]    = comboBetonType.SelectedIndex;
            _D["BTi"]   = cmbFib_i.SelectedIndex;
            _D["Bft3n"] = cmbBetonClass.SelectedIndex;
            _D["Bftn"]  = cmbBftn.SelectedIndex;
            _D["Bfn"]   = cmbBfn.SelectedIndex;
            _D["Humi"]  = cmbWetAir.SelectedIndex;
        }

        /// <summary>
        /// Сохранение Параметров, для формы BeamDiagram
        /// </summary>
        /// <param name="_D"></param>
        private void DictFormBeamDiagram(Dictionary<string, double> _D)
        {
            if (_beamCalcVM == null)
            { return; }

            _D["Force"] = _beamCalcVM.Force;
            _D["LenX"] = _beamCalcVM.LengthX;
        }


        /// <summary>
        ///  Расчет жесткости сечения для списка моментов
        /// </summary>
        /// <param name="_My">Изгибающие моменты кг*с</param>
        /// <returns>Жесткость кг*см2</returns>
        private List<double> CalculateStiffness(List<double> _My)
        {
            Dictionary<string, double> dictParams = DictCalcParams(m_BeamSection);
            NDMSetup ndmSetup = NDMSetupValuesFromForm();
            List<double> bendingStiffness = new List<double>();

            foreach (double my in _My)
            {
                CalcNDM calcNDM = new CalcNDM(m_BeamSection) { setup = ndmSetup, D = dictParams };
                Dictionary<string, double> res = calcNDM.RunMy(my);

                if (res != null)
                    bendingStiffness.Add(Math.Abs(my / res["Kz"]));
            }

            return bendingStiffness;
        }
        
        /// <summary>
        /// расчет прогибов, построение графика прогибов для балки
        /// </summary>
        /// <param name="_beamCalcVM"></param>
        /// <param name="isUtilizationFactor"> флаг, определяющий превышение коэф использования; false - превышает, true - коэф в норме</param>
        /// <returns></returns>
        private double CalculateBeamDeflections(bool isUtilizationFactor)
        {

            //TODO
            // выполнять расчет только в случае, если ранее были сохранены 
            //if (_beamCalcVM == null || _beamCalcVM.path2BeamDiagrams.Count == 0)
            if (_beamCalcVM == null)
            { 
                return double.NaN;
            }
            if (!isUtilizationFactor)
            {
                MessageBox.Show("Конструкция не удовлетворяет требованиям прочности.\n " +
                    "Расчет прогибов не производится.");
                return double.NaN;
            }
            
            // Значения в точках
            List<double> X = new List<double>();
            List<double> valueMomentInX = new List<double>();
            // Значения на середине рассматриваемого участка
            List<double> valuesMomentOnSection = new List<double>();
            //List<double> valuesStiffnesOnSection = new List<double>();
            _beamCalcVM.BeamDiagramModel.BreakBeamIntoSections(X, valueMomentInX, valuesMomentOnSection);

            if (valuesMomentOnSection.Count == 0) { return double.NaN; }

            // Получение жесткости на участках
            List<double> valuesStiffnesOnSection = CalculateStiffness(valuesMomentOnSection);
            //List<double> valuesСurvatureOnSection = CalcNDM_My(valuesMomentOnSection);

            // график прогиба по расчетным значениям
            double deflexionMax = _beamCalcVM.BeamDiagramModel.CalculateDeflectionDiagram(X, valueMomentInX, valuesStiffnesOnSection);

            if (deflexionMax == 0)
                return double.NaN;

            return deflexionMax;
        }

        /// <summary>
        /// Создать картинки для отчета заглавной части отчета
        /// </summary>
        public void CreatePictureForHeaderReport(List<BSCalcResultNDM> calcResults)
        {
            List<string> pathToPictures = new List<string>();
            string pathToPicture;

            // балка
            if (_beamCalcVM != null)
            {
                // эпюра силы
                if (true)
                {
                    pathToPicture = _beamCalcVM.BeamDiagramModel.SaveChart(_beamCalcVM.BeamDiagramModel.diagramQ, "DiagramQ");
                    pathToPictures.Add(pathToPicture);
                }
                // эпюра момента
                if (true)
                {
                    pathToPicture = _beamCalcVM.BeamDiagramModel.SaveChart(_beamCalcVM.BeamDiagramModel.diagramM, "DiagramM");
                    pathToPictures.Add(pathToPicture);
                }
                // эпюра прогиба
                if (true)
                {
                    calcResults[0].Deflexion_max = CalculateBeamDeflections(CheckUtilizationFactor(calcResults));
                    pathToPicture = _beamCalcVM.BeamDiagramModel.SaveChart(_beamCalcVM.BeamDiagramModel.diagramU, "DiagramU");
                    pathToPictures.Add(pathToPicture);
                }
            }

            // Диаграмма деформирования
            if (true)
            {
                // собрать данные
                DataForDeformDiagram data = ValuesForDeformDiagram();
                // определить vm
                CalcDeformDiagram calculateDiagram = new CalcDeformDiagram(data.typesDiagram, data.resists, data.elasticity);
                Chart deformDiagram = calculateDiagram.CreteChart();
                pathToPicture = CalcDeformDiagram.SaveChart(deformDiagram);
                pathToPictures.Add(pathToPicture);
            }
            calcResults[0].PictureForHeaderReport = pathToPictures;
        }

        public void CreatePictureForBodyReport(List<BSCalcResultNDM> calcResultsNDM)
        {
            for(int i = 0; calcResultsNDM.Count > i; i++)
            {
                BSCalcResultNDM calcResNDM = calcResultsNDM[i];

                List<string> pathToPictures = new List<string>();
                string pathToPicture;
                // изополя сечения по деформации
                if (true)
                {
                    string pictureName = $"beamSectionMeshDeform{i}";
                    pathToPicture = Directory.GetCurrentDirectory() + "\\" + pictureName + ".png";
                    MeshDraw mDraw = CreateMosaic(1, calcResNDM.Eps_B, calcResNDM.Eps_S, (double)numEps_fbt_ult.Value, -(double)numEps_fb_ult.Value, calcResNDM.Rs);
                    mDraw.SaveToPNG("Деформации", pathToPicture);

                    pathToPictures.Add(pathToPicture);
                }

                // изополя сечения по напряжению
                if (true)
                {
                    string pictureName = $"beamSectionMeshStress{i}";
                    pathToPicture = Directory.GetCurrentDirectory() + "\\" + pictureName + ".png";
                    MeshDraw mDraw = CreateMosaic(2, calcResNDM.Sig_B, calcResNDM.Sig_S, calcResNDM.Rfbt, BSHelper.kgssm2kNsm(calcResNDM.Rfb), BSHelper.kgssm2kNsm(calcResNDM.Rs));
                    mDraw.SaveToPNG("Напряжения", pathToPicture);

                    pathToPictures.Add(pathToPicture);
                }

                if (pathToPictures.Count > 0)
                {
                    calcResNDM.PictureForBodyReport = pathToPictures;
                }
            }
        }

        /// <summary>
        /// Данные с формы
        /// </summary>
        /// <returns></returns>
        private NDMSetup NDMSetupValuesFromForm()
        {            
            NDMSetup ndmSetup = BSData.LoadNDMSetup();
            ndmSetup.BetonTypeId = (cmbTypeMaterial.SelectedIndex == 1) ? 1 : 0;
            ndmSetup.UseRebar = checkBoxRebar.Checked;
            ndmSetup.RebarType = Convert.ToString(cmbRebarClass.SelectedItem);
            ndmSetup.N = _beamSectionMeshSettings?.NX??0;
            ndmSetup.M = _beamSectionMeshSettings?.NY??0;
            ndmSetup.MinAngle = _beamSectionMeshSettings?.MinAngle??0;
            ndmSetup.MaxArea = _beamSectionMeshSettings?.MaxArea??0;

            BSData.SaveNDMSetup(ndmSetup);

            return ndmSetup;
        }

        private NDMSetup NDMSetupInitFormValues()
        {
            NDMSetup ndmSetup = BSData.LoadNDMSetup();
            
            _beamSectionMeshSettings = new MeshSectionSettings(ndmSetup.N, ndmSetup.M, ndmSetup.MinAngle, ndmSetup.MaxArea);

            return ndmSetup;
        }

        /// <summary>
        /// Сохранить данные для формы BeamDiagram
        /// </summary>
        private void SaveValuesForBeamDiagram()
        {
            if (_beamCalcVM == null)
            { return; }

            InitForBeamDiagram dataToSave = new InitForBeamDiagram()
            {
                Force = _beamCalcVM.Force,
                LengthX = _beamCalcVM.LengthX
            };

            BSData.SaveForBeamDiagram(dataToSave);
        }


        /// <summary>
        /// Расчет на действие поперечных сил
        /// </summary>
        private Dictionary<string, double> CalcQxQy()
        {            
            SetFiberMaterialProperties();

            RecalRandomEccentricity_e0();
            
            Dictionary<string, double> dMNQ = GetEffortsForCalc();

            if (dMNQ["Qx"] == 0 && dMNQ["Qy"] == 0)
            {
                return null;
            }
            else if (dMNQ["Qx"] != 0 && num_s_w_X.Value <= 0)
            {
                MessageBox.Show("Задайте шаг арматуры по X", "Расчет на Qx", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
            else if (dMNQ["Qy"] != 0 && num_s_w_Y.Value <= 0)
            {
                MessageBox.Show("Задайте шаг арматуры по Y", "Расчет на Qy", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }

            double   beamLngth = InitBeamLength(true);
            double[] sz        = BeamSizes(beamLngth);
                                    
            Dictionary<string, double> resQxQy = FiberCalculate_QxQy(dMNQ, sz);

            return resQxQy;            
        }

        /// <summary>
        /// "Расчет по прочности нормальных сечений на основе нелинейной деформационной модели"
        /// </summary>        
        private BSCalcResultNDM CalcNDM(BeamSection _beamSection)
        {
            Dictionary<string, double> resQxQy = CalcQxQy();

            // данные с формы:
            Dictionary<string, double> _D = DictCalcParams(_beamSection);          

            // расчет на MxMyN по НДМ            
            NDMSetup _setup = NDMSetupValuesFromForm();                
           
            // расчет:
            CalcNDM calcNDM = new CalcNDM(_beamSection) {setup = _setup, D = _D };            
            calcNDM.Run();

            BSCalcResultNDM calcRes = calcNDM.CalcRes;
            calcRes.ResQxQy       = resQxQy;
            calcRes.ImageStream   = m_ImageStream;
            calcRes.Coeffs        = m_Coeffs;
            calcRes.UnitConverter = _UnitConverter;

            return calcNDM.CalcRes;           
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
            double cEb = (double)numE_beton.Value; // модуль упругости,  кг/см2

            // диаграмма:
            // арматура
            string cR_class = cmbRebarClass.Text;
            double cRs = (double)numRs.Value; // кг/см2            
            double cEs = (double)numEs.Value; // кг/см2
            double c_eps_s0 =  0.00175; 
            double c_eps_s2 =  0.025; 

            //бетон
            double c_eps_b1 = 0.0030;
            double c_eps_b1_red = 0.0030; // уточнить
            double c_eps_b2 = 0.0035d;

            // длина балки, см 
            double c_Length = Convert.ToDouble(tbLength.Text);

            // расстановка арматурных стержней
            List<BSRod> Rods = new List<BSRod>();

            // Усилия Mx, My - моменты, кг*см , N - сила, кг              
            //GetEffortsFromForm(out List<Dictionary<string, double>> MNQ);
            Dictionary<string, double> MNQ = GetEffortsForCalc();

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
                fiberCalc_Deform.SetParams(new double[] { _beamSectionMeshSettings.NX, _beamSectionMeshSettings .NX});
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

      
        private bool ValidateNDMCalc(Dictionary<string, double> _D)
        {
            if (m_SectionChart == null || m_SectionChart.m_BeamSection != m_BeamSection)
            {
                MessageBox.Show("Нажмите кнопку Сечение и задайте диаметры и расстановку стержней арматуры.", 
                    "Расчет по НДМ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (_D["Mz"] == 0 && _D["My"] == 0 && _D["N"] == 0 &&  _D["Qx"] == 0 &&  _D["Qy"] == 0)
            {
                MessageBox.Show("Требуется задать моменты Mx My или силу N или поперчные силы Qx Qy ", "Расчет по НДМ",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }


        /// <summary>
        /// Валидация данных перед проведением расчета по деф. модели
        /// </summary>
        /// <param name="_lstD"> нагнрузки</param>
        /// <returns></returns>
        private bool ValidateNDMCalc(List<Dictionary<string, double>> _lstD)
        {
            string message = string.Empty;

            if (m_SectionChart == null || m_SectionChart.m_BeamSection != m_BeamSection)
            {            
                message += "Перейдите на закладку Сечение и задайте диаметры и расстановку стержней арматуры.\n";
            }

            if (_lstD.Count == 0)
            {             
                message += "Требуется задать моменты Mx My или силу N или поперечные силы Qx Qy. \n";
            }
            
            bool messageIsWritten_Qx = true;
            bool messageIsWritten_Qy = true;
            foreach (var _mnqD in _lstD)
            {
                Dictionary<string, double> MNQ = _mnqD;

                if (MNQ["Qx"] != 0 && num_s_w_X.Value <= 0)
                {
                    if (messageIsWritten_Qx)
                    {
                        message = message + "Задайте шаг арматуры по X.\n";
                        messageIsWritten_Qx = false;
                    }
                }
                else if (MNQ["Qy"] != 0 && num_s_w_Y.Value <= 0)
                {
                    if (messageIsWritten_Qy)
                    { 
                        message = message + "Задайте шаг арматуры по Y.\n";
                        messageIsWritten_Qy = false;
                    }
                }
            }

            if (!string.IsNullOrEmpty(message))
            {
                MessageBox.Show(message, "Расчет по НДМ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Расчет по НДМ            
        /// </summary>        
        private void btnCalc_Deform_Click(object sender, EventArgs e)
        {                        
            try
            {
                List<BSCalcResultNDM> calcResults = new List<BSCalcResultNDM>();
                 
                GetEffortsFromForm(out List<Dictionary<string, double>> lstMNQ);

                if (!ValidateNDMCalc(lstMNQ))
                    return;

                foreach (Dictionary<string, double> efforts in lstMNQ)
                {
                    m_EffortsForCalc = efforts;

                    TriangleNet.Geometry.Point CG = new TriangleNet.Geometry.Point(0, 0);
                    BSCalcResultNDM calcRes = null;

                    if (m_BeamSection == BeamSection.Rect)
                    {
                        calcRes = CalcNDM(BeamSection.Rect);
                    }
                    else if (BSHelper.IsITL(m_BeamSection))
                    {
                        calcRes = CalcNDM(m_BeamSection);
                    }
                    else if (m_BeamSection == BeamSection.Ring)
                    {
                        GenerateMesh(ref CG);
                        calcRes = CalcNDM(BeamSection.Ring);
                    }
                    else if (m_BeamSection == BeamSection.Any)
                    {
                        GenerateMesh(ref CG);
                        calcRes = CalcNDM(BeamSection.Any);
                    }

                    if (calcRes != null)
                    {
                        calcResults.Add(calcRes);
                    }
                }

                CreatePictureForHeaderReport(calcResults);
                CreatePictureForBodyReport(calcResults);

                // формирование отчета
                BSReport.RunReport(m_BeamSection, calcResults);

            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }                      
        }

        // сохранить усилия
        private void btnEffortsRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                GetEffortsFromForm(out List<Dictionary<string, double>> _lstMNQ);
                List<Efforts> effortsFromForm = new List<Efforts>();
                int i = 0;
                foreach (var _MNQ in _lstMNQ)
                {
                    Efforts ef = new Efforts() { Id = ++i, Mx = _MNQ["Mx"], My = _MNQ["My"], N = _MNQ["N"], Qx = _MNQ["Qx"], Qy = _MNQ["Qy"] };
                    effortsFromForm.Add(ef);
                }
                
                Lib.BSData.SaveEfforts(effortsFromForm);
            }
            catch (Exception _ex)
            {
                MessageBox.Show(_ex.Message);
            }
        }
        
        /// <summary>
        /// сохранить данные с формы
        /// </summary>
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
                Efbt = numE_fbt.Value.ToString(),
                Rs = Convert.ToString(cmbRebarClass.SelectedItem),
                Rsw = Convert.ToString(cmbTRebarClass_X.SelectedItem),
                Area_s = (double)numAs.Value,
                Area1_s = (double)numAs1.Value,
                a_s = (double)num_a.Value,
                a1_s = (double)num_a1.Value
            };

            BSData.UpdateFormParams(formParams);

            StrengthFactors sf = new StrengthFactors()
            {
                Id = 1,
                Yft = (double)numYft.Value,
                Yb = (double)numYb.Value,
                Yb1 = (double)numYb1.Value,
                Yb2 = (double)numYb2.Value,
                Yb3 = (double)numYb3.Value,
                Yb5 = (double)numYb5.Value
            };
            BSData.SaveStrengthFactors(sf);

        }

        // сохранить параметры расчета и геометрические размеры в БД
        private void btnSaveParams_Click(object sender, EventArgs e)
        {
            try
            {
                NDMSetupValuesFromForm();

                RefreshSectionChart(m_BeamSection);

                FormParamsSaveData();

                BSData.UpdateBeamSectionGeometry(m_InitBeamSectionsGeometry);                
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
        }

        // открыть форму со списком данных
        private void btnCalcResults_Click(object sender, EventArgs e)
        {
            BSCalcResults bSCalcResults = new BSCalcResults();
            var dCalcParams = DictCalcParams(m_BeamSection);

            DictFormUserParams(dCalcParams);
            DictFormBeamDiagram(dCalcParams);

            bSCalcResults.CalcParams = dCalcParams;
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

        /// <summary>
        /// Свойства бетона по классу на сжатие
        /// </summary>
        /// <param name="_betonTypeId"></param>
        private void Init_Rfb_Efb(int _betonTypeId, int _airHumidityId = 0)
        {         
            string betonClass = Convert.ToString(cmbBfn.SelectedValue);
            if (string.IsNullOrEmpty(betonClass)) return; 

            Beton bt = BSQuery.HeavyBetonTableFind(betonClass, _betonTypeId);
            
            if (bt.Rbn != 0)
                numRfb_n.Value = (decimal)BSHelper.MPA2kgsm2(bt.Rbn);

            double fi_b_cr = 0;

            if (_airHumidityId >= 0 && _airHumidityId <= 3 && bt.B >= 10)
            {
                int iBClass = (int)Math.Round(bt.B, MidpointRounding.AwayFromZero);

                fi_b_cr = BSFiberLib.CalcFi_b_cr(_airHumidityId, iBClass);
            }

            if (bt.Eb != 0)
            {
                double _eb = BSHelper.MPA2kgsm2(bt.Eb * 1000);
                numE_beton.Value = (decimal)(_eb / (1.0 + fi_b_cr));
            }
        }


        // B, fn класс на сжатие
        private void cmbBfn_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbBfn.SelectedIndex > -1 && comboBetonType.SelectedIndex > -1)
                {
                    Init_Rfb_Efb(comboBetonType.SelectedIndex, cmbWetAir.SelectedIndex+1);   
                }
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

                    labelTypeDDRebar.Text = rebar.DiagramType;
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
                Rebar trb = m_Rebar.Find(match => match.ID == (string) cmbTRebarClass_X.SelectedItem);      
                if (trb != null)
                {
                    numRsw_X.Value       = (decimal)BSHelper.MPA2kgsm2(trb.Rsw_X);
                    numEsw_X.Value       = (decimal)BSHelper.MPA2kgsm2(trb.Es);
                    num_s_w_X.Value      = (decimal)trb.Sw_X;
                    cmbDw_X.SelectedItem = "12";
                    numN_w_X.Value       = 2;
                }
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
            labelRswMPa.Text = string.Format("{0} МПа ", BSHelper.Kgsm2MPa((double)numRsw_X.Value));
        }

        private void checkBoxRebar_CheckedChanged(object sender, EventArgs e)
        {            
            if (checkBoxRebar.Checked)
                numYb2.Value = 1.0M;
            else
                numYb2.Value = 0.9M;
        }

        private void RefreshSectionChart(BeamSection _beamSection)
        {
            if (CalcType == CalcType.Static) return;

            //NDMSetupValuesFromForm();
            m_SectionChart = new BSSectionChart(_beamSection);
            m_SectionChart.Dock = DockStyle.Top;            
            //m_SectionChart.m_BeamSection = _beamSection;

            var sz = BeamWidtHeight(out double b, out double h, out double _area);

            m_SectionChart.RebarClass = cmbRebarClass.SelectedItem.ToString();
            m_SectionChart.Wdth = (float)b;
            m_SectionChart.Hght = (float)h;
            m_SectionChart.Sz = sz;
            m_SectionChart.NumArea = _area;
            m_SectionChart.a_t_Nx = (int)numN_w_X.Value;
            //m_SectionChart.FormReload();
            // m_SectionChart.ShowDialog(); //.Show();

            panelSectionDraw.Controls.Clear();
            panelSectionDraw.Controls.Add(m_SectionChart);

            m_ImageStream = m_SectionChart.GetImageStream;
        }


        /// <summary>
        ///  Конструктор сечений
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSection_Click(object sender, EventArgs e)
        {
            RefreshSectionChart(m_BeamSection);
        }

        /// <summary>
        /// По результатам расчета Создать поверхность MeshDraw с разбитыми на элементы участками
        /// </summary>
        /// <param name="_CalcResNDM"></param>
        private void ShowMosaic(BSCalcResultNDM _CalcResNDM)
        {
            int mode = comboMosaic.SelectedIndex;
            MeshDraw mDraw = null;

            if (mode == 1)
            {
                //ShowMosaic(mode, _CalcResNDM.Eps_B, _CalcResNDM.Eps_S, (double)numEps_fbt_ult.Value, -(double)numEps_fb_ult.Value, _CalcResNDM.Rs);
                mDraw = CreateMosaic(mode, _CalcResNDM.Eps_B, _CalcResNDM.Eps_S, (double)numEps_fbt_ult.Value, -(double)numEps_fb_ult.Value, _CalcResNDM.Rs);
            }
            else if (mode == 2)
            {
                //ShowMosaic(mode, _CalcResNDM.Sig_B, _CalcResNDM.Sig_S,  _CalcResNDM.Rfbt, BSHelper.kgssm2kNsm(_CalcResNDM.Rfb), BSHelper.kgssm2kNsm(_CalcResNDM.Rs));
                mDraw = CreateMosaic(mode, _CalcResNDM.Sig_B, _CalcResNDM.Sig_S, _CalcResNDM.Rfbt, BSHelper.kgssm2kNsm(_CalcResNDM.Rfb), BSHelper.kgssm2kNsm(_CalcResNDM.Rs));
            }
            else if (mode == 3)
            {
                //ShowMosaic(mode, _CalcResNDM.Eps_B, _CalcResNDM.Eps_S);
                mDraw = CreateMosaic(mode, _CalcResNDM.Eps_B, _CalcResNDM.Eps_S);
            }
            else if (mode == 4)
            {
                //ShowMosaic(mode, _CalcResNDM.Sig_B, _CalcResNDM.Sig_S);
                mDraw = CreateMosaic(mode, _CalcResNDM.Sig_B, _CalcResNDM.Sig_S);
            }

            if (mDraw != null)
                mDraw.ShowMesh();
        }








        /// <summary>
        ///  Разбиение сечения на конечные элементы
        /// </summary>
        /// <param name="_valuesB">значения для бетона</param>
        /// <param name="_valuesB">значения для арматуры</param>
        private MeshDraw CreateMosaic(int _Mode = 0,
                                List<double> _valuesB = null,
                                List<double> _valuesS = null,
                                double _ultMax = 0,
                                double _ultMin = 0,
                                double _ultRs = 0,
                                double _e_st_ult = 0,
                                double _e_s_ult = 0)
        {
            MeshDraw mDraw = null;

            double[] sz = BeamSizes();

            if (BSHelper.IsRectangled(m_BeamSection))
            {
                mDraw = new MeshDraw(_beamSectionMeshSettings.NX, _beamSectionMeshSettings.NY);
                mDraw.MosaicMode = _Mode;
                mDraw.UltMax = _ultMax;
                mDraw.UltMin = _ultMin;
                mDraw.Rs_Ult = _ultRs;
                mDraw.e_st_ult = _e_st_ult;
                mDraw.e_s_ult = _e_s_ult;
                mDraw.Values_B = _valuesB;
                mDraw.Values_S = _valuesS;
                mDraw.CreateRectanglePlot(sz, m_BeamSection);
            }
            else if (m_BeamSection == BeamSection.Ring)
            {
                TriangleNet.Geometry.Point cg = new TriangleNet.Geometry.Point();
                _ = GenerateMesh(ref cg);

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
            }
            else if (m_BeamSection == BeamSection.Any) //заданное пользователем сечение
            {
                TriangleNet.Geometry.Point cg = new TriangleNet.Geometry.Point();
                _ = GenerateMesh(ref cg);

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
            }

            return mDraw;
        }

        /// <summary>
        /// Покрыть сечение сеткой
        /// </summary>
        private string GenerateMesh(ref TriangleNet.Geometry.Point _CG)
        {
            string pathToSvgFile;
            double[] sz = BeamWidtHeight(out double b, out double h, out double area);
            
            BSMesh.Nx = _beamSectionMeshSettings.NX;
            BSMesh.Ny = _beamSectionMeshSettings.NY;
            double meshSize = Math.Max(BSMesh.Nx, BSMesh.Ny);

            BSMesh.MinAngle = _beamSectionMeshSettings.MinAngle;
            Tri.MinAngle = _beamSectionMeshSettings.MinAngle;            
            BSMesh.MaxArea = 0;

            if (meshSize > 0)
            {
                BSMesh.MaxArea = area / meshSize;
            }

            BSMesh.FilePath = Path.Combine(Environment.CurrentDirectory, "Templates");
            Tri.FilePath = BSMesh.FilePath;
            
            if (m_BeamSection == BeamSection.Rect)
            {
                List<double> rect = new List<double> { 0, 0, b, h };
                              
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
                BSSection.IBeam(sz, out pts, out PointF _center, out PointF _left);
                _CG = new TriangleNet.Geometry.Point(_center.X, _center.Y);

                pathToSvgFile = BSCalcLib.Tri.CreateSectionContour(pts, BSMesh.MaxArea);
                _ = Tri.CalculationScheme();
            }
            else if (m_BeamSection == BeamSection.Any)
            {
                List<PointF> pts;
                BSSection.IBeam(sz, out pts, out PointF _center, out PointF _left); // TODO доработать до произвольного сечения
                _CG = new TriangleNet.Geometry.Point(_center.X, _center.Y);

                pathToSvgFile = BSCalcLib.Tri.CreateSectionContour(pts, BSMesh.MaxArea);
                _ = Tri.CalculationScheme(false);
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
        

        /// <summary>
        /// Построить диаграмму деформирования
        /// </summary>
        private void btnCalcDeformDiagram_Click(object sender, EventArgs e)
        {
            // собрать данные
            DataForDeformDiagram data = ValuesForDeformDiagram();
            // определить vm
            CalcDeformDiagram calculateDiagram = new CalcDeformDiagram(data.typesDiagram, data.resists, data.elasticity);
            // форма
            DeformDiagram deformDiagram = new DeformDiagram(calculateDiagram);
            deformDiagram.Show();
        }


        private DataForDeformDiagram ValuesForDeformDiagram()
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

            //if (typeMaterial == BSHelper.Concrete)
            //{
            //    // Характеристики по сжатию
            //    R_n = (double)numRfb_n.Value;       // Rb_n 
            //    e0 = (double)numEps_fb0.Value;      // eb0
            //    e2 = (double)numEps_fb2.Value;      // eb2
            //    E = (double)numEfb.Value;           // Eb

            //}
            if (typeMaterial == BSHelper.FiberConcrete)
            {
                // Характеристики по сжатию такие же как у бетона
                R_n = (double)numRfb_n.Value;       // Rb_n 
                E = (double)numE_fbt.Value;           //Eb
                // Характеристики по растяжению
                Rt_n = (double)numRfbt_n.Value;     // Rfbt_n
                Rt2_n = (double)numRfbt2n.Value;    // Rfbt2_n
                Rt3_n = (double)numRfbt3n.Value;    // Rfbt3_n
                Et = E;                    // !!!   // Efbt

                // значения забираются с другой формы
                //e0 = 0;      // eb0
                //e2 = 0;      // eb2                
                //et2 = 0;    // efbt2
                //et3 = 0;    // efbt3
            }
            else if (typeMaterial == BSHelper.Rebar)
            {

                // Характеристики по растяжению
                Rt_n = (double)numRs.Value;         // 
                Et = (double)numEs.Value;           //
                // Характеристики по сжатию
                R_n = (double)numRsc.Value;
                E = Et;

                // значения забираются с другой формы
                //e0 = et0;
                //e2 = et2;
                //et0 = 0; // (double)numEpsilonS0.Value;
                //et2 = 0; //(double)numEpsilonS2.Value;
            }
            else
            {
                throw new Exception("Выбрано значение материала, выходящее за предел предопределенных значений.");
            }

            DataForDeformDiagram data = new DataForDeformDiagram();
            data.typesDiagram = new string[] { typeMaterial, typeDiagram };
            data.resists = new double[] { R_n, Rt_n, Rt2_n, Rt3_n };
            data.elasticity = new double[] { E, Et };
            return data;
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
        ///  Сохранить пользовательские значения формы
        /// </summary>        
        private void SaveUserValuesFromFiberMainForm()
        {
            try
            {
                FormParamsSaveData();

                BSData.UpdateBeamSectionGeometry(m_InitBeamSectionsGeometry);

                GetEffortsFromForm(out List<Dictionary<string, double>> lsMNQ);
                List<Efforts> effortsFromForm = new List<Efforts>();
                int i=0;
                foreach (var _MNQ in lsMNQ )
                {
                    Efforts ef = new Efforts() { Id = ++i, Mx = _MNQ["Mx"], My = _MNQ["My"], N = _MNQ["N"], Qx = _MNQ["Qx"], Qy = _MNQ["Qy"] };
                    effortsFromForm.Add(ef);
                }
                Lib.BSData.SaveEfforts(effortsFromForm);

                NDMSetupValuesFromForm();

                SaveValuesForBeamDiagram();

            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
        }
              
        private void numEs_ValueChanged(object sender, EventArgs e)
        {
            labelEsMPa.Text = string.Format("{0} МПа ", BSHelper.Kgsm2MPa((double)numEs.Value));
        }
        
        private void numRsc_ValueChanged(object sender, EventArgs e)
        {
            labelRsсMPa.Text = string.Format("{0} МПа ", BSHelper.Kgsm2MPa((double)numRsc.Value));
            
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
            //numE_beton.Value = numE_fiber.Value;
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
            System.Windows.Forms.Cursor.Current = Cursors.Hand;
        }

        private void label37_MouseMove(object sender, MouseEventArgs e)
        {
            System.Windows.Forms.Cursor.Current = Cursors.Hand;
        }

        private void label36_MouseMove(object sender, MouseEventArgs e)
        {
            System.Windows.Forms.Cursor.Current = Cursors.Hand;
        }

        private void label35_MouseMove(object sender, MouseEventArgs e)
        {
            System.Windows.Forms.Cursor.Current = Cursors.Hand;
        }

        private void label13_MouseMove(object sender, MouseEventArgs e)
        {
            System.Windows.Forms.Cursor.Current = Cursors.Hand;
        }

        private void label12_MouseMove(object sender, MouseEventArgs e)
        {
            System.Windows.Forms.Cursor.Current = Cursors.Hand;
        }

        private void lbE_beton_info_MouseMove(object sender, MouseEventArgs e)
        {
            System.Windows.Forms.Cursor.Current = Cursors.Hand;
        }

        private void lbE_fb_info_MouseMove(object sender, MouseEventArgs e)
        {
            System.Windows.Forms.Cursor.Current = Cursors.Hand;
        }

        private void picEffortsSign_MouseMove(object sender, MouseEventArgs e)
        {
            System.Windows.Forms.Cursor.Current = Cursors.Hand;
        }


        private void btnMosaic_Click(object sender, EventArgs e)
        {
            try
            {
                MeshDraw mDraw = CreateMosaic(comboMosaic.SelectedIndex);
                mDraw.ShowMesh();
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
        }
              
        private void lbE_beton_info_Click(object sender, EventArgs e)
        {
            try
            {
                BSFiberConcrete.Beton val = cmbBfn.SelectedItem as BSFiberConcrete.Beton;

                double fi_b_cr = BSFiberLib.CalcFi_b_cr(cmbWetAir.SelectedIndex+1, (int)val.B);

                MessageBox.Show($"Модуль упругости на сжатие: \n " +
                    $"{BSHelper.Kgsm2MPa((double)numE_beton.Value)} МПа \n" +
                    $"{BSHelper.Kgssm2ToKNsm2((double)numE_beton.Value, 2)} КН/см2 \n" +
                    $"СП 63. Таблица 6.11 \n" +
                    $"СП 63 п 6.1.15 учет коэффицента ползучести φb,cr = {fi_b_cr} ",
                    "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
            }
        }
    
        private void lbE_fb_info_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Модуль упругости на растяжение \n" +
                $"{BSHelper.Kgsm2MPa((double)numE_fbt.Value)} МПа \n" +
                $"{BSHelper.Kgssm2ToKNsm2((double)numE_fbt.Value, 2)} КН/см2 \n", 
                "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }



        private void picEffortsSign_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Центр координат находится в приведенном центре тяжести сечения. X0Y - плоскость сечения, Z - вдоль оси сечения \n " +
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

        private void comboBetonType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int betonTypeId = comboBetonType.SelectedIndex;

            if (betonTypeId == 0 || betonTypeId == 1 || betonTypeId == 2)
            {
                Init_Rfb_Efb(betonTypeId, cmbWetAir.SelectedIndex+1);
            }            
        }

        /// <summary>
        /// Рассчитать случайный эксцентриситет
        /// </summary>
        private void RecalRandomEccentricity_e0()
        {
            bool ok = double.TryParse(tbLength.Text, out double _lgth);
            if (ok)
            {
                BeamWidtHeight(out double _w, out double _h, out double _area);
                numRandomEccentricity.Value = (decimal)BSFiberCalc_MNQ.Calc_e_a(_lgth, _h);
            }
        }

        private void tbLength_TextChanged(object sender, EventArgs e)
        {
            try
            {
                RecalRandomEccentricity_e0();
            }
            catch
            {
            }            
        }

        private void cmbWetAir_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (cmbBfn.SelectedIndex > -1 && comboBetonType.SelectedIndex > -1 && cmbWetAir.SelectedIndex > -1)
                {
                    Init_Rfb_Efb(comboBetonType.SelectedIndex, cmbWetAir.SelectedIndex+1);
                }
            }
            catch { }
        }

        private void cmbTRebarClass_Y_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var rclass = (string)cmbTRebarClass_Y.SelectedItem;
                Rebar trb = m_Rebar.Find(match => match.ID == rclass); 
                
                if (trb != null)
                {
                    numRsw_Y.Value = (decimal)BSHelper.MPA2kgsm2(trb.Rsw_X);
                    numEsw_Y.Value = (decimal)BSHelper.MPA2kgsm2(trb.Es);
                    num_s_w_Y.Value = (decimal)trb.Sw_Y;
                    cmbDw_Y.SelectedItem = "12";
                    numN_w_Y.Value = 2; 
                }
            }
            catch { }
        }

        /// <summary>
        /// создать и рассчитать сечение произвольной формы
        /// </summary>        
        private void btnCustomSection_Click(object sender, EventArgs e)
        {
            NDMSetupValuesFromForm();
            m_BeamSection = BeamSection.Any;
            RefreshSectionChart(BeamSection.Any);
            m_SectionChart.DictCalcParams = DictCalcParams(BeamSection.Any);
            tabFiber.SelectedTab = tabPageNDM;
        }

        /// <summary>
        /// Проверить не превышение коэффициентов использования
        /// </summary>
        /// <returns></returns>
        private bool CheckUtilizationFactor(List<BSCalcResultNDM> calcResults)
        {
            // Проходимо по всем парам из  m_CalcResults, если есть хоть одно значение превышающее 1 ( или -1), выдает false
            

            Type type = typeof(BSCalcResultNDM);
            PropertyInfo[] properties = type.GetProperties();

            foreach (BSCalcResultNDM calcRes in calcResults)
            {
                foreach (PropertyInfo property in properties)
                {
                    DisplayNameAttribute DN = type.GetProperty(property.Name).GetCustomAttribute<DisplayNameAttribute>();
                    if (DN == null)
                    { continue; }

                    string displayName = DN.DisplayName;

                    if (!displayName.Contains("Коэффициент использования"))
                    { continue; }

                    object propertyValue = property.GetValue(calcRes);
                    double value = 0;
                    if (double.TryParse(propertyValue.ToString(), out value))
                    {
                        if (value >= 1 || value <= -1)
                        { return false; }
                    }
                }
            }
            return true;
        }

        private void btnEffortsAddRow_Click(object sender, EventArgs e)
        {
            gridEfforts.Rows.Add();
        }

        private void btnEffortsDelRow_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow selectedRow in gridEfforts.SelectedRows)
            {
                gridEfforts.Rows.Remove(selectedRow);
            }
        }

        private void gridEfforts_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection selectedRow = gridEfforts.SelectedRows;
            if (selectedRow.Count > 0)
            {
                btnEffortsDelRow.Enabled = true;
            }
            else
            {
                btnEffortsDelRow.Enabled = false;
            }
        }

        /// <summary>
        /// пользовательские данные из файла
        /// </summary>
        /// <param name="_D"></param>
        private void InitControlValuesFromUser(Dictionary<string, object> _D)
        {
            // element
            if (_D.ContainsKey("lgth"))
                tbLength.Text                 = _D["lgth"].ToString();
            if (_D.ContainsKey("coeflgth"))
                cmbEffectiveLengthFactor.Text = _D["coeflgth"].ToString();
            if (_D.ContainsKey("UseRebar"))
                checkBoxRebar.Checked         = false; // доделать
            if (_D.ContainsKey("BT"))
                comboBetonType.SelectedIndex = int.Parse(_D["BT"].ToString());
            if (_D.ContainsKey("BTi"))
                cmbFib_i.SelectedIndex       = Convert.ToInt32(_D["BTi"].ToString());

            // классы
            if (_D.ContainsKey("Bft3n"))
                cmbBetonClass.SelectedIndex = Convert.ToInt32(_D["Bft3n"].ToString());
            if (_D.ContainsKey("Bftn"))
                cmbBftn.SelectedIndex       = Convert.ToInt32(_D["Bftn"].ToString());
            if (_D.ContainsKey("Bfn"))
                cmbBfn.SelectedIndex        = Convert.ToInt32(_D["Bfn"].ToString());
            if (_D.ContainsKey("Humi"))
                cmbWetAir.SelectedIndex     = Convert.ToInt32(_D["Humi"].ToString());

            // factors
            if (_D.ContainsKey("Yft"))
                numYft.Value = decimal.Parse(_D["Yft"].ToString());
            if (_D.ContainsKey("Yb"))
                numYb.Value  = decimal.Parse(_D["Yb"].ToString());
            if (_D.ContainsKey("Yb1"))
                numYb1.Value = decimal.Parse(_D["Yb1"].ToString());
            if (_D.ContainsKey("Yb2"))
                numYb2.Value = decimal.Parse(_D["Yb2"].ToString());
            if (_D.ContainsKey("Yb3"))
                numYb3.Value = decimal.Parse(_D["Yb3"].ToString());
            if (_D.ContainsKey("Yb5"))
                numYb5.Value = decimal.Parse(_D["Yb5"].ToString());

            // BeamDiagram
            if (_beamCalcVM != null)
            {
                if (_D.ContainsKey("Force"))
                    _beamCalcVM.Force = double.Parse(_D["Force"].ToString());
                if (_D.ContainsKey("LenX"))
                    _beamCalcVM.LengthX = double.Parse(_D["LenX"].ToString());
            }
        }

        private void btnOpenCalcFile_Click(object sender, EventArgs e)
        {            
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "json files (*.json)|*.json";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    string filePath = openFileDialog.FileName;

                    BSFiberLoadData loadData = new BSFiberLoadData();
                    var DValues = loadData.ReadInitFromJson(filePath);

                    InitControlValuesFromUser(DValues);                   
                }
            }            
        }
 
        private void BSFiberMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveUserValuesFromFiberMainForm();
        }

        private void E_fb_value(decimal _Eb, decimal _Ef, decimal _mu_fv)
        {
            numE_fbt.Value = (decimal)BSFiberLib.E_fb((double) _Eb, (double) _Ef, (double) _mu_fv);
        }
        
        private void numE_beton_ValueChanged(object sender, EventArgs e)
        {
            E_fb_value(numE_beton.Value, numEfiber.Value, numMu_fv.Value);
        }

        private void numMu_fv_ValueChanged(object sender, EventArgs e)
        {
            E_fb_value(numE_beton.Value, numEfiber.Value, numMu_fv.Value);
        }

        private void numEf_ValueChanged(object sender, EventArgs e)
        {
            E_fb_value(numE_beton.Value, numEfiber.Value, numMu_fv.Value);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MeshSettingsView meshSettings = new MeshSettingsView(_beamSectionMeshSettings);
            meshSettings.ShowDialog();
        }

        private void num_s_w_X_ValueChanged(object sender, EventArgs e)
        {            
        }

        private void numEsw_X_ValueChanged(object sender, EventArgs e)
        {
            labelEswMPa.Text = string.Format("{0} МПа ", BSHelper.Kgsm2MPa((double)numEsw_X.Value));
        }

        private void tabRebar_Click(object sender, EventArgs e)
        {

        }
    }
}

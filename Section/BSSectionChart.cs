using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using BSFiberConcrete.Lib;
using BSCalcLib;
using System.Diagnostics;
using BSFiberConcrete.Report;
using System.Text;

namespace BSFiberConcrete.Section
{
    /// <summary>
    /// Отрисовать сечение, назначить арматуру
    /// </summary>
    public partial class BSSectionChart : UserControl
    {
        public List<PointF> RodPoints
        {
            get { return m_RodPoints; }
            set { m_RodPoints = value; }
        }

        const string UserSection = "UserSection";
        public Dictionary<string, double> DictCalcParams { private get; set; }
        public BeamSection m_BeamSection { get; set; }

        /// <summary>
        /// Класс используемой продольной арматуры 
        /// </summary>
        public string RebarClass { private get; set; }
        /// <summary>
        /// Класс используемой поперечной арматуры по X
        /// </summary>
        public string RebarClassX { private get; set; }
        /// <summary>
        /// Класс используемой поперечной арматуры по Y
        /// </summary>
        public string RebarClassY { private get; set; }

        public MemoryStream GetImageStream => m_ImageStream;
        private MemoryStream m_ImageStream;
        private List<RebarDiameters> m_Diameters;

        // Точки на диаграмме для отрисовки стержней арматуры  
        private List<PointF> m_RodPoints;

        // Точки на диаграмме, для отрисовки сечения        
        private List<PointF> PointsSection;

        // точки на диаграмме для отображения отверстия в сечении
        private List<PointF> InnerPoints;
        public NDMSetup NdmSetup { get; set; }

        private PointF Origin;

        private const int amountOfEdges = 40;

        public PointF Center { get; set; }
        public float Wdth { get { return (float)width; } set { width = value; } }
        public float Hght { get { return (float)height; } set { height = value; } }

        public double[] Sz { get; set; }
        public double NumArea { set { numArea.Value = (decimal)value; } get { return (double)numArea.Value; } }

        // геом характеристики сечения
        private float width;
        private float height;
        public double CF_X; // ц.т. фигуры
        public double CF_Y; // ц.т. фигуры
        public double J_X;
        public double J_Y;
        public double W_X_top;
        public double W_X_low;
        public double W_Y_left;
        public double W_Y_right;
        // характеристики арматуры
        private double As;
        private double As1;

        public Rebar Rebar
        {
            set { m_Rebar = value; }
            get
            {
                m_Rebar = RebarFromControl();
                return m_Rebar;
            }
        }

        private Rebar m_Rebar;
       
        private int as_t = 4; //защитный слой поперечной арматуры 
        private List<PointF> PointsTRebar_X;
        private List<PointF> PointsTRebar_Y;

        private const int SerieSection = 0;
        private const int SerieLRebar = 1;
        private const int SerieInnerSection = 2;
        private const int SerieTRebar_X = 3;
        private const int SerieTRebar_Y = 4;

        public BSSectionChart()
        {
            InitializeComponent();
        }

        public BSSectionChart(BeamSection _beamSection)
        {
            InitializeComponent();

            InitValues(_beamSection);
        }

        public void InitValues(BeamSection _beamSection)
        {
            Center = new PointF(0, 0);
            NdmSetup = BSData.LoadNDMSetup();
            m_BeamSection = _beamSection;
            InnerPoints = new List<PointF>();
            height = 0;
            width = 0;
            Sz = new double[] { 0, 0, 0, 0, 0, 0 };
            PointsTRebar_X = new List<PointF>();
            PointsTRebar_Y = new List<PointF>();
        }

        /// <summary>
        /// Арматура из настроек формы
        /// </summary>
        /// <returns>Rebar</returns>
        private Rebar RebarFromControl()
        {
            double Asw_X = (double)numN_w_X.Value * BSHelper.AreaCircle(double.Parse(Convert.ToString(cmbDw_X.SelectedItem)) / 10.0);
            double Asw_Y = (double)numN_w_X.Value * BSHelper.AreaCircle(double.Parse(Convert.ToString(cmbDw_Y.SelectedItem)) / 10.0);
            
            Rebar rebar = new Rebar()
            {
                // Продольная                
                Es = m_Rebar.Es, // модуль упругости

                    // Площадь                
                As  = m_Rebar.As,  // растянутая                
                As1 = m_Rebar.As1, // сжатая
                    // Расстояние до ц.т.                
                a  = m_Rebar.a,  // растянутая                
                a1 = m_Rebar.a1, // сжатая

                // Поперечная по X
                Rsw_X = m_Rebar.Rsw_X,
                Esw_X = m_Rebar.Esw_X,
                Sw_X  = (double)num_s_w_X.Value,
                N_X   = (int)numN_w_X.Value,
                Asw_X = Asw_X,

                // Поперечная по Y
                Rsw_Y = m_Rebar.Rsw_Y,
                Esw_Y = m_Rebar.Esw_Y,
                Sw_Y  = (double)num_s_w_Y.Value,
                N_Y   = (int)numN_w_Y.Value,
                Asw_Y = Asw_Y,
            };

            return rebar;
        }

        /// <summary>
        /// Стержни арматуры
        /// </summary>
        private void InitRods()
        {
            if (m_RodPoints == null)
                return;

            List<BSRod> bsRods = BSData.LoadBSRod(m_BeamSection);

            if (bsRods.Count == 0)
            {
                foreach (var _rod in m_RodPoints)
                {
                    bsRods.Add(new BSRod()
                    {
                        CG_X = _rod.X,
                        CG_Y = _rod.Y,
                        D = m_Diameters[0].Diameter / 10,
                        Dnom = m_Diameters[0].Diameter.ToString()
                    });
                }
            }

            // проверка, чтобы значения диаметров из базы соответствовали текущему списку диаметров
            List<string> listDiameters = m_Diameters.Select(s => s.Diameter.ToString()).ToList();
            foreach (BSRod tmpBSRod in bsRods)
            {
                if (!listDiameters.Contains(tmpBSRod.Dnom))
                {
                    tmpBSRod.D = m_Diameters[0].Diameter / 10;
                    tmpBSRod.Dnom = m_Diameters[0].Diameter.ToString();
                }
            }
            RodBS.DataSource = bsRods;


            // должны отрабатывать только при первом вызове этого метода

            // поперечная арматура
            if (Dnom.Items.Count == 0) 
                Dnom.Items.AddRange(listDiameters.ToArray());
            // продольная арматура по x
            if (cmbDw_X.Items.Count == 0)
                UpdateRebarClassX();
            // продольная арматура по Y
            if (cmbDw_Y.Items.Count == 0)
                UpdateRebarClassY();
        }


        /// <summary>
        ///  Поперечная арматура по оси X
        /// </summary>
        private void InitTRebar_X(int _N)
        {
            PointsTRebar_X.Clear();

            if (_N == 0)
            {                
                return;
            }
           
            //if (m_BeamSection == BeamSection.Any)
            //{
            //    return;
            //}

            if (m_BeamSection == BeamSection.Ring)
            {
                double in_r = Sz[0];
                double out_r = Sz[1];
                double dr = (out_r - in_r) / (_N + 1);

                for (int n = 1; n <= _N; n++)
                {
                    double radius = in_r + n * dr;
                    PointsTRebar_X.AddRange(MakeCircle(radius, amountOfEdges));
                }
            }
            else
            {
                float y0 = 0 + as_t + Origin.Y,
                yh = height - as_t + Origin.Y;
                float[] x = new float[_N];

                x[0] = width / (_N + 1);

                for (int i = 1; i < _N; i++)
                    x[i] = (i + 1) * x[0];

                for (int i = 0; i < _N; i++)
                    x[i] += Origin.X;

                if (_N == 1)
                {
                    PointsTRebar_X.Add(new PointF(x[0], y0));
                    PointsTRebar_X.Add(new PointF(x[0], yh));
                }
                else
                {
                    for (int _n = 1; _n < _N; _n++)
                    {
                        PointsTRebar_X.Add(new PointF(x[0], y0));
                        PointsTRebar_X.Add(new PointF(x[_n], y0));

                        PointsTRebar_X.Add(new PointF(x[_n], yh));
                        PointsTRebar_X.Add(new PointF(x[0], yh));
                    }

                    PointsTRebar_X.Add(new PointF(x[0], y0));
                }
            }
        }

        // точки окружности
        private List<PointF> MakeCircle(double _radius, int _amountOfEdges)
        {
            List<PointF> pts = new List<PointF>();

            // внешняя граница
            for (int k = 0; k <= _amountOfEdges; k++)
            {
                double x = Center.X + _radius * Math.Cos(k * 2 * Math.PI / _amountOfEdges);
                double y = Center.Y + _radius * Math.Sin(k * 2 * Math.PI / _amountOfEdges);
                pts.Add(new PointF((float)x, (float)y));
            }

            return pts;
        }

        /// <summary>
        /// построить контур сечения по точкам
        /// </summary>
        private void InitPoints()
        {
            if (m_BeamSection == BeamSection.Rect)
            {
                BSSection.RectangleBeam(Sz, out Origin);
                PointsSection = BSSection.SectionPoints;
                m_RodPoints = BSSection.RodPoints;
            }
            else if (BSHelper.IsITL(m_BeamSection))
            {
                BSSection.IBeam(Sz, out PointsSection, out PointF _Center, out Origin);
                m_RodPoints = BSSection.RodPoints;
            }
            else if (m_BeamSection == BeamSection.Ring)
            {
                PointsSection = new List<PointF>();
                double radius = Sz[1];
                double inner_radius = Sz[0];

                // внешнее кольцо
                PointsSection = MakeCircle(radius, amountOfEdges);
                // внутреннее кольцо
                InnerPoints = MakeCircle(inner_radius, amountOfEdges);
                // арматура
                m_RodPoints = new List<PointF>() { new PointF(0, -(height - 4)) };

                Origin = Center;
            }
            else if (m_BeamSection == BeamSection.Any)
            {
                PointsSection = new List<PointF>();
                List<NdmSection> userSection = BSData.LoadNdmSection(UserSection);
                int idx = 0;
                pointBS.Clear();
                foreach (NdmSection _pt in userSection)
                {
                    idx++;
                    PointsSection.Add(new PointF((float)_pt.X, (float)_pt.Y));
                }

                if (PointsSection.Count > 0)
                {
                    width = PointsSection.Max(var => var.X) - PointsSection.Min(var => var.X) ;
                    height = PointsSection.Max(var => var.Y) - PointsSection.Min(var => var.Y);

                    Origin = new PointF(0, 0);
                }
                else
                {
                    width = 0;
                    height = 0;
                }

                m_RodPoints = new List<PointF>();
            }
        }

        /// <summary>
        /// расстановка арматуры по оси Y
        /// </summary>
        /// <param name="_N">количество стержней </param>
        private void InitTRebar_Y(int _N)
        {
            PointsTRebar_Y.Clear();

            if (_N == 0)
            {                
                return;
            }

            float dx = 0;

            if (m_BeamSection == BeamSection.Ring )
            {
                return;
            }
            else if ( m_BeamSection == BeamSection.Any)
            {
                dx = width / 2.0f ;
            }
                        
            if (_N == 1)
            {
                PointsTRebar_Y.Add(new PointF(-(width - as_t ) / 2.0f + dx, height / 2.0f));
                PointsTRebar_Y.Add(new PointF((width - as_t) / 2.0f + dx, height / 2.0f));
            }
            else if (_N == 2)
            {
                PointsTRebar_Y.Add(new PointF(-(width - as_t) / 2.0f + dx, height / 3.0f));
                PointsTRebar_Y.Add(new PointF((width - as_t) / 2.0f + dx, height / 3.0f));
                PointsTRebar_Y.Add(new PointF());
                PointsTRebar_Y.Add(new PointF(-(width - as_t) / 2.0f + dx, 2* height / 3.0f));
                PointsTRebar_Y.Add(new PointF((width - as_t) / 2.0f + dx, 2* height / 3.0f));
            }
            else if (_N == 3)
            {
                PointsTRebar_Y.Add(new PointF(-(width - as_t) / 2.0f + dx, height / 4.0f));
                PointsTRebar_Y.Add(new PointF((width - as_t) / 2.0f + dx, height / 4.0f));
                PointsTRebar_Y.Add(new PointF());
                PointsTRebar_Y.Add(new PointF(-(width - as_t) / 2.0f + dx, 2* height / 4.0f));
                PointsTRebar_Y.Add(new PointF((width - as_t ) / 2.0f + dx, 2* height / 4.0f));
                PointsTRebar_Y.Add(new PointF());
                PointsTRebar_Y.Add(new PointF(-(width - as_t) / 2.0f + dx, 3* height / 4.0f));
                PointsTRebar_Y.Add(new PointF((width - as_t) / 2.0f + dx, 3* height / 4.0f));
            }
            else 
            {
                for (int n = 1; n <= _N; n++)
                {
                    PointsTRebar_Y.Add(new PointF(-(width - as_t + dx) / 2.0f, height * n / (_N+1)));
                    PointsTRebar_Y.Add(new PointF((width - as_t + dx) / 2.0f, height * n / (_N+1)));
                    PointsTRebar_Y.Add(new PointF());
                }
            }           
        }

        private void InitDataSource()
        {
            if (!string.IsNullOrEmpty(RebarClass))
                m_Diameters =  BSData.DiametersOfTypeRebar(RebarClass);

            InitPoints();

            if (NdmSetup.UseRebar)
            {
                InitRods();

                InitTRebar_X((int)numN_w_X.Value);

                InitTRebar_Y((int)numN_w_Y.Value);
            }

            int idx = 0;

            if (PointsSection != null)
            {
                pointBS.Clear();

                foreach (PointF p in PointsSection)
                {
                    idx++;
                    BSPoint bsPt = new BSPoint(p) { Num = idx };
                    pointBS.Add(bsPt);
                }
            }
        }

        /// <summary>
        /// Площадь продольной арматуры
        /// </summary>
        /// <returns>см2</returns>
        private double RodsArea(ref List<PointF> rod_points)
        {            
            double rods_area = 0;
            foreach (BSRod _rod in RodBS)
            {
                rod_points.Add(new PointF((float)_rod.CG_X, (float)_rod.CG_Y));

                rods_area += _rod.As;
            }

            return rods_area;
        }

        /// <summary>
        /// Площадь продольной арматуры
        /// </summary>
        /// <returns>см2</returns>
        private (double, double) RodsArea()
        {
            List<PointF> rod_points = new List<PointF>();
            
            double As = 0;
            double As1 = 0;

            foreach (BSRod _rod in RodBS)
            {
                rod_points.Add(new PointF((float)_rod.CG_X, (float)_rod.CG_Y));

                if (CF_Y <= _rod.CG_Y)
                    As1 += _rod.As;
                else
                    As += _rod.As;
            }
            
            return (As , As1);
        }


        /// <summary>
        /// построить сечение по точкам
        /// </summary>
        /// <param name="_clear"></param>
        private void DrawFromDatasource(bool _clear = false)
        {
            if (chart.Series.Count < 4)
                throw new Exception("Необходимо создать 4 ряда диаграммы");

            m_Rebar = RebarFromControl();

            if (_clear)
            {
                foreach (var serie in  chart.Series)
                    serie.Points.Clear();                
            }

            List<PointF> points = new List<PointF>();
            foreach (BSPoint bsp in pointBS)
            {
                points.Add(new PointF(bsp.X, bsp.Y));
            }

            List<PointF> rod_points = new List<PointF>(); ;
            // арматура            
            double rods_area = RodsArea(ref rod_points);
            
            // сечение внешний контур
            Series serieSection = chart.Series[SerieSection];
            for (int j = 0; j < points.Count; j++)
            {
                var pt = points[j];
                var d_pt = new DataPoint(pt.X, pt.Y);                
                serieSection.Points.Add(d_pt);
            }

            // внутренний контур сечения
            Series serieInnerSection = chart.Series[SerieInnerSection];
            foreach (var p in InnerPoints)
            {                
                serieInnerSection.Points.Add(new DataPoint(p.X, p.Y));
            }

            // арматурные стержни
            Series serieRods = chart.Series[SerieLRebar];          
            for (int j = 0; j < rod_points.Count; j++)
            {
                var rod_pt = rod_points[j];
                serieRods.Points.Add(new DataPoint(rod_pt.X, rod_pt.Y));
            }

            // поперечная арматура по X
            Series serieTRebar = chart.Series[SerieTRebar_X];
            for (int j = 0; j < PointsTRebar_X.Count; j++)
            {
                var pt = PointsTRebar_X[j];
                serieTRebar.Points.Add(new DataPoint(pt.X, pt.Y));
            }

            // поперечная арматура по Y
            Series serieTRebar_Y = chart.Series[SerieTRebar_Y];
            for (int j = 0; j < PointsTRebar_Y.Count; j++)
            {
                PointF pt_f = PointsTRebar_Y[j];

                var dpt = new DataPoint(pt_f.X, pt_f.Y);
                dpt.IsEmpty = pt_f.IsEmpty;

                serieTRebar_Y.Points.Add(dpt);
            }
            
            numAreaRebar.Value = (decimal) rods_area;

            m_ImageStream      = new MemoryStream();

            chart.SaveImage(m_ImageStream, ChartImageFormat.Png);
        }

        public void RedrawSection()
        {
            InitTRebar_X((int)numN_w_X.Value);

            InitTRebar_Y((int)numN_w_Y.Value);

            DrawFromDatasource(true);

            SaveRods2DB();

            if (m_BeamSection == BeamSection.Any)
            {
                SaveSection();
            }
        }


        // перерисовать сечение
        private void btnDraw_Click(object sender, EventArgs e)
        {
            try
            {
                RedrawSection();

                GenerateMesh();
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            PointF pt = new PointF(0, 0);
            
            pointBS.Add(new BSPoint(pt) { Num = pointBS.Count+1}) ;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                pointBS.RemoveAt(pointBS.Count - 1);
            }
            catch (ArgumentOutOfRangeException)
            {
            }
        }

        /// <summary>
        /// сохранить привязку стержней в БД
        /// </summary>
        private void SaveRods2DB()
        {
            if (RodBS == null || RodBS.List == null )
                return;

            List<BSRod> bSRods = (List<BSRod>) RodBS.List; 
            
            BSData.SaveRods(bSRods, m_BeamSection);            
        }

        /// <summary>
        ///  Сохранить координаты в БД
        /// </summary>        
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveRods2DB();

                Save2PolyFile();
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
        }

        private void InitToolTips()
        {
            // Установка высплывающего текста
            System.Windows.Forms.ToolTip tlTip = new System.Windows.Forms.ToolTip();
            
            tlTip.AutoPopDelay = 5000;
            tlTip.InitialDelay = 1000;
            tlTip.ReshowDelay  = 50;
            // Force the ToolTip text to be displayed whether or not the form is active.
            tlTip.ShowAlways = true;
            // Set up the ToolTip text for the Button and Checkbox.
            tlTip.SetToolTip(this.btnDraw, "Обновить (перерисовать) сечение");
            tlTip.SetToolTip(this.btnSaveChart, "Cохранить геометрию сечения");            
            tlTip.SetToolTip(this.btnAddRod, "Добавить стержень продольной арматуры");
            tlTip.SetToolTip(this.btnDelRod, "Удалить стержень (последняя строка)");
            tlTip.SetToolTip(this.btnSave, "Сохранить расстановку арматуры");            
        }

        /// <summary>
        /// Обновить контрол
        /// </summary>
        public void FormReload()
        {
            try
            {
                InitControls();
  
                InitToolTips();

                InitDataSource();

                DrawFromDatasource(true);

                chart.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0.0}";
                chart.ChartAreas[0].AxisY.LabelStyle.Format = "{0:0.0}";

            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
        }

        [Conditional("DEBUG")]
        private void IsDebugCheck(ref bool isDebug)
        {
            isDebug = true;
        }


        private void BSSectionChart_Load(object sender, EventArgs e)
        {
            FormReload();

            bool _isdebug = false;
            IsDebugCheck(ref _isdebug);
            
        }

        private void InitControls()
        {            
            if (m_BeamSection == BeamSection.Any)
            {
                dataGridSection.Enabled = true;                
                btnAdd.Visible = true;
                btnDel.Visible = true;
                //btnCalc.Enabled = true;
                foreach (DataGridViewColumn cl in dataGridSection.Columns)
                    cl.ReadOnly = false;

            }
            else
            {
                btnAdd.Visible = false;
                btnDel.Visible = false;
                //btnCalc.Enabled = false;
                dataGridSection.Enabled = false;

                foreach (DataGridViewColumn cl in dataGridSection.Columns)
                    cl.ReadOnly = true;
            }

            numArea.Enabled = false;
            numAreaRebar.Enabled = false;

            // поперечная арматура
            num_s_w_X.Value       = (decimal) m_Rebar.Sw_X; 
            numN_w_X.Value        = m_Rebar.N_X;

            num_s_w_Y.Value       = (decimal)m_Rebar.Sw_Y;
            numN_w_Y.Value        = m_Rebar.N_Y;
        }

        private void Save2PolyFile()
        {
            /*
             * TODO save to .poly file
             * 
             * # A box with a hole: eight points, no attributes, one boundary marker.
                8 2 0 1
                # Outer box
                  1   0.0 0.0   1
                  2   3.0 0.0   1
                  3   3.0 3.0   1
                  4   0.0 3.0   1
                # Inner square
                  5   1.0 1.0   2
                  6   2.0 1.0   2
                  7   2.0 2.0   2
                  8   1.0 2.0   2
                # Eight segments with boundary markers.
                8 1
                # Outer box
                  1   1 2   1
                  2   2 3   1
                  3   3 4   1
                  4   4 1   1
                # Inner square
                  5   5 6   2
                  6   6 7   2
                  7   7 8   2
                  8   8 5   2
                # One hole in the middle of the inner square.
                1
                  1   1.5 1.5
             */
        }

        /// <summary>
        /// Добавить стержень
        /// </summary>        
        private void btnAddRod_Click(object sender, EventArgs e)
        {
            try
            {
                //RodBS.AddNew();
                RodBS.Add(new BSRod()
                {
                    CG_X = 0,
                    CG_Y = 0,
                    D = m_Diameters[0].Diameter / 10,
                    Dnom = m_Diameters[0].Diameter.ToString()
                });
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
        }


        /// <summary>
        /// Удалить стержень
        /// </summary>        
        private void btnDelRod_Click(object sender, EventArgs e)
        {
            try
            {
                RodBS.RemoveAt(RodBS.Count - 1);                
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
        }

        private void labelRods_MouseMove(object sender, MouseEventArgs e)
        {
            System.Windows.Forms.Cursor.Current = Cursors.Hand;
        }

        private void labelRods_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Задайте привязку продольной арматуры - укажите координаты стержней", "Информация", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// по номинальному диаметру заполнить фактический
        /// </summary>        
        private void bSRodDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    var d_nom = int.Parse(bSRodDataGridView.Rows[e.RowIndex].Cells["Dnom"].Value?.ToString() ?? "0");

                    if (m_Diameters != null)
                    {
                        if (d_nom > 0)
                        {
                            //* 100 перевод из см^2 в мм^2
                            double ar = m_Diameters.Find(_D => _D.Diameter == d_nom).Square * 100;

                            double d_fact = BSHelper.DCircle(ar);

                            bSRodDataGridView.Rows[e.RowIndex].Cells["D"].Value = Math.Round(d_fact, 2)/10;
                        }
                    }
                    else
                    {
                        if (d_nom > 0)
                            bSRodDataGridView.Rows[e.RowIndex].Cells["D"].Value = d_nom / 10.0;
                    }
                }
            }
            catch
            {

            }           
        }

        /// <summary>
        /// сохранить контур сечения
        /// </summary>
        private void SaveSection()
        {
            List<NdmSection> bsSec = new List<NdmSection>();

            BindingList<BSPoint> p = (BindingList<BSPoint>)pointBS.List;

            int idxN = 0;
            foreach (var pt in p)
            {
                NdmSection ndmSection = new NdmSection();
                ndmSection.Num = UserSection;
                ndmSection.N = ++idxN;
                ndmSection.X = pt.X;
                ndmSection.Y = pt.Y;
                bsSec.Add(ndmSection);
            }

            BSData.SaveSection(bsSec, UserSection);
        }
               
        private void btnSaveChart_Click(object sender, EventArgs e)
        {            
            try
            {               
                SaveRods2DB();

                SaveSection();                
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }
                
        private void BeamSectionFromPoints(ref List<PointF> _PointsSection, PointF _center)
        {                                              
            BindingList<BSPoint> bspoints = (BindingList<BSPoint>)pointBS.List;           

            foreach (BSPoint pt in bspoints)
            {             
                _PointsSection.Add(new PointF(pt.X, pt.Y));
            }          
        }

        /// <summary>
        /// Сгенерировать сетеку сечения
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string GenerateMesh()
        {            
            BSMesh.Nx        = NdmSetup.N;
            BSMesh.Ny        = NdmSetup.M;
            BSMesh.MinAngle  = NdmSetup.MinAngle;
            Tri.MinAngle     = NdmSetup.MinAngle;
            BSMesh.MaxArea   = NdmSetup.MaxArea;

            return GenerateMesh(BSMesh.MaxArea);
        }

        public string GenerateMesh(double _MaxArea)
        {            
            List<PointF> pts = new List<PointF>();

            BeamSectionFromPoints(ref pts, Center);

            string pathToSvgFile = Tri.CreateSectionContour(pts, _MaxArea);

            _ = Tri.CalculationScheme(false);

            // центры тяжести треугольников
            int? nTri = Tri.triCGs?.Count();

            if (nTri > 0)
            {
                // площади треугольников
                NumArea = Tri.triAreas?.Sum() ?? 0;

                width = (float)Tri.WidthOfFigure();

                height = (float)Tri.HeightOfFigure();

                (CF_X, CF_Y) = Tri.СenterOfFigure();

                (J_X, J_Y) = Tri.MomentOfInertia();

                (W_X_low, W_X_top, W_Y_left, W_Y_right) = Tri.ModulusOfSection();                
            }

            return pathToSvgFile;
        }


        /// <summary>
        /// сетка
        /// </summary>        
        private void btnMesh_Click(object sender, EventArgs e)
        {
            try
            {
                string pathToSvgFile = GenerateMesh();
                if (File.Exists(pathToSvgFile))
                {
                    string path2file = "BeamSectionMesh.htm";
                    File.CreateText(path2file).Dispose();
                    using (StreamWriter w = new StreamWriter(path2file, true, Encoding.UTF8))
                    {
                        w.WriteLine("<html>");
                        w.WriteLine($"<img src = \"{pathToSvgFile}\"/>");
                        w.WriteLine("</html>");
                    }
                    Process.Start(new ProcessStartInfo { FileName = path2file, UseShellExecute = true });
                }
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
        }


        /// <summary>
        /// Обновить установленный класс поперечной арматуры
        /// производится установка выбранного значения
        /// </summary>
        /// <param name="TRebarClass"> название класса арматуры</param>
        /// <param name="comboboxToUpdate"></param>
        public void UpdateTransverseRebarClass(string TRebarClass, System.Windows.Forms.ComboBox comboboxToUpdate)
        {
            List<RebarDiameters> rebarClassXDiameters = BSData.DiametersOfTypeRebar(TRebarClass);
            List<string> newDiameters = rebarClassXDiameters.Select(s => s.Diameter.ToString()).ToList();
            object selectedValue = null;
            if (comboboxToUpdate.Items.Count != 0)
            {
                if (newDiameters.Contains(comboboxToUpdate.SelectedItem))
                {
                    selectedValue = comboboxToUpdate.SelectedItem;
                }
                comboboxToUpdate.Items.Clear();
            }

            comboboxToUpdate.Items.AddRange(rebarClassXDiameters.Select(s => s.Diameter.ToString()).ToArray());

            if (selectedValue == null)
            { comboboxToUpdate.SelectedIndex = 0; }
            else { comboboxToUpdate.SelectedItem = selectedValue; }
        }


        /// <summary>
        /// Обновить установленный класс поперечной арматуры по X
        /// </summary>
        public void UpdateRebarClassX()
        {
            UpdateTransverseRebarClass(RebarClassX, cmbDw_X);
        }


        /// <summary>
        /// Обновить установленный класс поперечной арматуры по Y
        /// </summary>
        public void UpdateRebarClassY()
        {
            UpdateTransverseRebarClass(RebarClassY, cmbDw_Y);
        }


        // Обновить установленный класс продольной арматуры
        public void UpdateRebarClass()
        {
            m_Diameters = BSData.DiametersOfTypeRebar(RebarClass);
            List<string> newListDiameters = m_Diameters.Select(s => s.Diameter.ToString()).ToList();

            Dnom.Items.Add(m_Diameters[0].Diameter.ToString());
            foreach (BSRod tmpBSRod in RodBS)
            {
                if (!newListDiameters.Contains(tmpBSRod.Dnom))
                {
                    tmpBSRod.D = m_Diameters[0].Diameter / 10;
                    tmpBSRod.Dnom = m_Diameters[0].Diameter.ToString();
                }
            }

            Dnom.Items.Clear();
            Dnom.Items.AddRange(newListDiameters.ToArray());
        }

        private void labelArea_MouseMove(object sender, MouseEventArgs e)
        {
            System.Windows.Forms.Cursor.Current = Cursors.Hand;
        }

        private void labelArea_Click(object sender, EventArgs e)
        {
            GenerateMesh();

            MessageBox.Show(
                $"Высота сечения: h = {Math.Round(height, 4)} ширина: b = {Math.Round(width, 4)}\n" +
                $"Центр тяжести сечения: X = {Math.Round(CF_X, 4)} Y = {Math.Round(CF_Y, 4)}\n" +
                $"Момент инерции : Jx = {Math.Round(J_X, 4)} Jy = {Math.Round(J_Y, 4)}\n" +                
                $"Момент сопротивления: Wx = {Math.Round(W_X_top, 4)} Wx' = {Math.Round(W_X_low, 4)}\n" +
                $"Момент сопротивления: Wy = {Math.Round(W_Y_left, 4)} Wy' = {Math.Round(W_Y_right, 4)}\n",
                "Сечение");
        }

        private void label_area_MouseMove(object sender, MouseEventArgs e)
        {
            System.Windows.Forms.Cursor.Current = Cursors.Hand;
        }

        private void label_area_Click(object sender, EventArgs e)
        {
            GenerateMesh();
            (As, As1) = RodsArea();

            MessageBox.Show(
                $"Площадь верхней зоны: As1 = {Math.Round(As1, 4)} см2\n" +
                $"Площадь нижней зоны:  As = {Math.Round(As, 4)} см2 \n",                 
                "Продольное армирование");

        }
    }
}

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
using ScottPlot.Colormaps;

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
        public Dictionary<string, double> DictCalcParams { private get; set;}
        public BeamSection m_BeamSection { get; set; }
                
        /// <summary>
        /// Класс используемой арматуры
        /// </summary>
        public string RebarClass { private get; set; }

        public MemoryStream  GetImageStream => m_ImageStream;
        
        private MemoryStream m_ImageStream;

        private List<RebarDiameters> m_Diameters;

        // Точки на диаграмме для отрисовки стержней арматуры  
        private List<PointF> m_RodPoints;

        // Точки на диаграмме, для отрисовки сечения        
        private List<PointF> PointsSection;

        // точки на диаграмме для отображения отверстия в сечении
        private List<PointF> InnerPoints;

        private NDMSetup     NdmSetup;
        private PointF       Origin;
        public PointF     Center { get; set; }
        public float Wdth { get { return b; } set { b = value; } }
        public float Hght { get { return h; } set { h = value; } }

        public double[] Sz { get; set; }
        public double NumArea { set { numArea.Value = (decimal)value; } get { return (double)numArea.Value; } }

        private float b;
        private float h;
        
        // количество стержней поперечной арматуры
        public  int a_t_Nx  = 0;
        private int as_t    = 4; //защитный слой поперечной арматуры 
        private List<PointF> PointsTRebar;    

        private const int SerieSection = 0;
        private const int SerieLRebar  = 1;
        private const int SerieInnerSection = 2;        
        private const int SerieTRebar  = 3;

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
            h = 0;
            b = 0;
            Sz = new double[] { 0, 0, 0, 0, 0, 0 };
            a_t_Nx = 2;
            PointsTRebar = new List<PointF>();
        }


        /// <summary>
        /// Стержни арматуры
        /// </summary>
        private void InitRods()
        {
            if (m_RodPoints == null)
                return;

            List<BSRod> bsRods =  BSData.LoadBSRod(m_BeamSection);

            if (bsRods.Count == 0)
            {
                foreach (var _rod in m_RodPoints)
                    bsRods.Add(new BSRod() { CG_X = _rod.X, CG_Y = _rod.Y, D = 14.0 } );
            }

            RodBS.DataSource = bsRods;
        }

        /// <summary>
        ///  Поперечная арматура
        /// </summary>
        private void InitTRebar(int _N)
        {
            if (_N == 0) return;

            float y0 = 0 + as_t + Origin.Y, 
                  yh = h - as_t + Origin.Y;

            float[] x = new float[_N];

            x[0] = b / (_N + 1);

            for (int i = 1; i < _N; i++)            
                x[i] = (i+1) * x[0];

            for (int i = 0; i < _N; i++)
                x[i] += Origin.X;

            if (_N == 1)
            {
                PointsTRebar.Add(new PointF(x[0], y0));
                PointsTRebar.Add(new PointF(x[0], yh));
            }
            else
            {
                for (int _n = 1; _n < _N; _n++)
                {
                    PointsTRebar.Add(new PointF(x[0], y0));
                    PointsTRebar.Add(new PointF(x[_n], y0));

                    PointsTRebar.Add(new PointF(x[_n], yh));
                    PointsTRebar.Add(new PointF(x[0], yh));
                }

                PointsTRebar.Add(new PointF(x[0], y0));
            }           
        }

        private void InitPoints()
        {
            if (m_BeamSection == BeamSection.Rect)                 
            {
                BSSection.RectangleBeam(Sz, out Origin);                
                PointsSection = BSSection.SectionPoints;
                m_RodPoints   = BSSection.RodPoints;               
            }
            else if (BSHelper.IsITL(m_BeamSection))
            {                
                BSSection.IBeam(Sz, out PointsSection, out PointF _Center,  out Origin);
                m_RodPoints = BSSection.RodPoints;
            }            
            else if (m_BeamSection == BeamSection.Ring)
            {
                PointsSection = new List<PointF>();
                int amountOfEdges = 40;

                double radius =  Sz[1];
                double inner_radius = Sz[0];

                for (int k = 0; k <= amountOfEdges; k++)
                {
                    // внешняя граница
                    double x = Center.X + radius * Math.Cos(k * 2 * Math.PI / amountOfEdges);
                    double y = Center.Y + radius * Math.Sin(k * 2 * Math.PI / amountOfEdges);
                    PointsSection.Add(new PointF((float)x, (float)y));
                    // отверстие
                    x = Center.X + inner_radius * Math.Cos(k * 2 * Math.PI / amountOfEdges);
                    y = Center.Y + inner_radius * Math.Sin(k * 2 * Math.PI / amountOfEdges);
                    InnerPoints.Add(new PointF((float)x, (float)y));
                    // арматура
                    m_RodPoints = new List<PointF>() { new PointF(0, -(h - 4)) };
                }

                Origin = Center;
            }
            else if (m_BeamSection == BeamSection.Any)
            {
                List<NdmSection> pointsSection = BSData.LoadNdmSection(UserSection);
                int idx = 0;
                foreach (NdmSection _pt in pointsSection) 
                {
                    idx++;
                    BSPoint bsPt = new BSPoint(_pt) ;
                    pointBS.Add(bsPt);                    
                }

                m_RodPoints = new List<PointF> ();

            }
        }

        private void InitDataSource()
        {
            if (!string.IsNullOrEmpty(RebarClass))
                m_Diameters =  BSData.DiametersOfTypeRebar(RebarClass);

            InitPoints();

            if (NdmSetup.UseRebar)
                InitRods();

            InitTRebar(a_t_Nx);

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
        /// построить сечение
        /// </summary>
        /// <param name="_clear"></param>
        private void DrawFromDatasource(bool _clear = false)
        {
            if (chart.Series.Count < 4)
                throw new Exception("Необходимо создать 4 ряда диаграммы");

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

            // арматура
            List<PointF> rod_points = new List<PointF>();
            double rods_area = 0;
            foreach (BSRod _rod in RodBS)
            {
                rod_points.Add(new PointF((float)_rod.CG_X, (float)_rod.CG_Y) );

                rods_area += _rod.As;
            }

            Series serieSection = chart.Series[SerieSection];
            for (int j = 0; j < points.Count; j++)
            {
                var pt = points[j];
                serieSection.Points.Add(new DataPoint(pt.X, pt.Y));
            }

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

            // поперечная арматура
            Series serieTRebar = chart.Series[SerieTRebar];
            for (int j = 0; j < PointsTRebar.Count; j++)
            {
                var pt = PointsTRebar[j];
                serieTRebar.Points.Add(new DataPoint(pt.X, pt.Y));
            }

            numAreaRebar.Value = (decimal) rods_area;
            m_ImageStream      = new MemoryStream();

            chart.SaveImage(m_ImageStream, ChartImageFormat.Png);
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {            
            DrawFromDatasource(true);            
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
            if (RodBS == null || RodBS.List == null || RodBS.List.Count == 0)
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
            tlTip.SetToolTip(this.btnMesh, "Сетка");
            tlTip.SetToolTip(this.btnCalc, "Рассчитать сечение по НДМ");            
            tlTip.SetToolTip(this.btnAddRod, "Произвольное сечение");
            tlTip.SetToolTip(this.btnDelRod, "Результаты расчета");
            tlTip.SetToolTip(this.btnSave, "Сохранить расстановку арматуры");            
        }

        public void FormReload()
        {
            try
            {
                InitControls();

                InitToolTips();

                InitDataSource();

                DrawFromDatasource(true);

            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
        }


        private void BSSectionChart_Load(object sender, EventArgs e)
        {
            FormReload();
        }

        private void InitControls()
        {            
            if (m_BeamSection == BeamSection.Any)
            {
                dataGrid.Enabled = true;
                btnAdd.Visible = true;
                btnDel.Visible = true;
            }
            else
            {
                dataGrid.Enabled = false;

                foreach (DataGridViewColumn cl in dataGrid.Columns)
                    cl.ReadOnly = true;
            }

            numArea.Enabled = false;
            numAreaRebar.Enabled = false;
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

        private void BSSectionChart_FormClosed(object sender, FormClosedEventArgs e)
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

        /// <summary>
        /// Добавить стержень
        /// </summary>        
        private void btnAddRod_Click(object sender, EventArgs e)
        {
            try
            {
                RodBS.AddNew();
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
            MessageBox.Show("Задайте привязку продольной арматуры - укажите координаты стержней");
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
                            double ar = m_Diameters.Find(_D => _D.Diameter == d_nom).Square;

                            double d_fact = BSHelper.DCircle(ar);

                            bSRodDataGridView.Rows[e.RowIndex].Cells["D"].Value = Math.Round(d_fact, 2);
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

               
        private void btnSaveChart_Click(object sender, EventArgs e)
        {            
            try
            {
                if (RodBS == null || RodBS.List == null || RodBS.List.Count == 0)
                {
                    //List<BSRod> bSRods = (List<BSRod>)RodBS.List;

                    //BSData.SaveRods(bSRods, BSBeamSection);
                }

                // TODO доделать
                List<NdmSection>     bsSec = new List<NdmSection>();
                BindingList<BSPoint> p = (BindingList<BSPoint>)pointBS.List;

                int idxN = 0;
                foreach (var pt in p)
                {
                    NdmSection ndmSection = new NdmSection();
                    ndmSection.Num = UserSection;
                    ndmSection.N = ++idxN;
                    ndmSection.X =  pt.X;
                    ndmSection.Y = pt.Y;
                    bsSec.Add(ndmSection);
                }    

                BSData.SaveSection(bsSec, UserSection);
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            // Close();
        }

        /// <summary>
        /// Расчет сечения по НДМ
        /// </summary>
        /// <param name="_My"></param>
        /// <returns></returns>
        private BSCalcResultNDM CalcNDM_MxMyN()
        {
            Dictionary<string, double> dictParams = DictCalcParams;
                        
            CalcNDM calcNDM = new CalcNDM(m_BeamSection) { setup = NdmSetup, D = dictParams };
            calcNDM.RunGroup1();

            return calcNDM.CalcRes;                                    
        }

        // Запустить расчет
        private void btnCalc_Click(object sender, EventArgs e)
        {
            try
            {
                GenerateMesh();

                BSCalcResultNDM calcRes = CalcNDM_MxMyN();

                BSReport.RunFromCode(m_BeamSection, calcRes);
            }
            catch (Exception _ex)
            {
                MessageBox.Show(_ex.Message);
            }            
        }

        private void BeamSectionFromPoints(ref List<PointF> _PointsSection, PointF _center)
        {                                              
            BindingList<BSPoint> bspoints = (BindingList<BSPoint>)pointBS.List;           
            foreach (BSPoint pt in bspoints)
            {             
                _PointsSection.Add(new PointF(pt.X, pt.Y));
            }
            
            //RodPoints = new List<PointF>();

            //foreach (BSPoint rod in RodBS)
            //{
            //    RodPoints.Add(new PointF(rod.X, rod.Y));
            //}            
        }

        /// <summary>
        /// Сгенерировать сетеку сечения
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private string GenerateMesh()
        {
            //if (m_BeamSection != BeamSection.Any) return "";
            BSMesh.Nx        = NdmSetup.N;
            BSMesh.Ny        = NdmSetup.M;
            BSMesh.MinAngle  = NdmSetup.MinAngle;
            Tri.MinAngle     = NdmSetup.MinAngle;
            BSMesh.MaxArea   = NdmSetup.MaxArea;
            List<PointF> pts = new List<PointF>();
            BeamSectionFromPoints(ref pts, Center);

            string pathToSvgFile = Tri.CreateSectionContour(pts, BSMesh.MaxArea);

            _ = Tri.CalculationScheme(false);

            // центры тяжести треугольников
            int? nTri = Tri.triCGs?.Count();

            if (nTri > 0)
            {        
                // площади треугольников
                NumArea = Tri.triAreas?.Sum() ?? 0;
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
                    Process.Start(new ProcessStartInfo { FileName = pathToSvgFile, UseShellExecute = true });
                }
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
        }
    }
}

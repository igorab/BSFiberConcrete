using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;
using System.Xml.Linq;
using BSFiberConcrete.Lib;
using OpenTK;
using ScottPlot.Colormaps;
using System.Reflection;
using TriangleNet.Geometry;
using static CBAnsDes.Member;
using System.Data.SqlTypes;
using BSCalcLib;
using System.Diagnostics;

namespace BSFiberConcrete.Section
{
    /// <summary>
    /// Отрисовать сечение, назначить арматуру
    /// </summary>
    public partial class BSSectionChart : Form
    {
        public List<PointF> RodPoints
        {
            get { return m_RodPoints; }
            set { m_RodPoints = value; }
        }

        const string UserSection = "UserSection";
        public Dictionary<string, double> DictCalcParams { private get; set;}
        public BeamSection m_BeamSection { get; set; }
        public bool UseRebar { private get; set; }

        /// <summary>
        /// Класс используемой арматуры
        /// </summary>
        public string RebarClass { private get; set; }

        public MemoryStream GetImageStream => m_ImageStream;
        
        private MemoryStream m_ImageStream;

        private List<RebarDiameters> m_Diameters;

        // Точки на диаграмме для отрисовки стержней арматуры  
        private List<PointF> m_RodPoints;

        // Точки на диаграмме, для отрисовки сечения        
        private List<PointF> PointsSection;

        // точки на диаграмме для отображения отверстия в сечении
        private List<PointF> InnerPoints;

        private NDMSetup ndmSetup;

        public PointF Center { get; set; }

        public float Wdth { set { w = value; } }
        public float Hght { set { h = value; } }

        public double[] Sz { get; set; }
        public double NumArea {set { numArea.Value = (decimal) value; }}

        private float w;
        private float h;
        

        public BSSectionChart()
        {
            InitializeComponent();

            UseRebar = true;

            Center = new PointF(0, 0);

            ndmSetup = BSData.LoadNDMSetup();

            InnerPoints = new List<PointF>();

            m_BeamSection = BeamSection.Rect;
            w = 0;
            h = 0;
            Sz = new double[]  { 0, 0, 0, 0, 0, 0 };
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

        private void InitPoints()
        {
            if (m_BeamSection == BeamSection.Rect)                 
            {
                BSSection.RectangleBeam(Sz);
                PointsSection = BSSection.SectionPoints;
                m_RodPoints = BSSection.RodPoints;               
            }
            else if (m_BeamSection == BeamSection.IBeam ||                      
                     m_BeamSection == BeamSection.LBeam)
            {                
                BSSection.IBeam(Sz, out PointsSection, out PointF _center);
                m_RodPoints = BSSection.RodPoints;
            }
            else if (m_BeamSection == BeamSection.TBeam)
            {
                BSSection.IBeam(Sz, out PointsSection, out PointF _center);
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

            if (UseRebar)
                InitRods();

            int idx = 0;

            if (PointsSection != null)
            {
                foreach (PointF p in PointsSection)
                {
                    idx++;
                    BSPoint bsPt = new BSPoint(p) { Num = idx };
                    pointBS.Add(bsPt);
                }
            }
        }


        private void DrawFromDatasource(bool _clear = false)
        {
            if (_clear)
            {
                chart.Series[0].Points.Clear();
                chart.Series[1].Points.Clear();
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

            Series serieSection = chart.Series[0];
            for (int j = 0; j < points.Count; j++)
            {
                var pt = points[j];
                serieSection.Points.Add(new DataPoint(pt.X, pt.Y));
            }

            Series serieInnerSection = chart.Series[2];
            foreach (var p in InnerPoints)
            {                
                serieInnerSection.Points.Add(new DataPoint(p.X, p.Y));
            }

            // арматурные стержни
            Series serieRods = chart.Series[1];          
            for (int j = 0; j < rod_points.Count; j++)
            {
                var rod_pt = rod_points[j];
                serieRods.Points.Add(new DataPoint(rod_pt.X, rod_pt.Y));

                //rod_pt.
            }
            
            numAreaRebar.Value = (decimal) rods_area;

            m_ImageStream = new MemoryStream();
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

        
        private void BSSectionChart_Load(object sender, EventArgs e)
        {
            try
            {
                InitControls();

                InitDataSource();

                DrawFromDatasource();
               
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
        }

        private void InitControls()
        {
            if (m_BeamSection == BeamSection.Any)
            {
                btnAdd.Visible = true;
                btnDel.Visible = true;
            }
            else
            {                
                foreach (DataGridViewColumn cl in dataGrid.Columns)
                    cl.ReadOnly = true;
            }
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
            MessageBox.Show("Задайте привязку арматуры - укажите координаты стержней");
        }

        private void bSRodDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    var d_nom = int.Parse(bSRodDataGridView.Rows[e.RowIndex].Cells["Dnom"].Value?.ToString() ?? "0");

                    if (m_Diameters != null && d_nom > 0)
                    {
                        double ar = m_Diameters.Find(_D => _D.Diameter == d_nom).Square;

                        double d_fact = BSHelper.DCircle(ar);

                        bSRodDataGridView.Rows[e.RowIndex].Cells["D"].Value = Math.Round(d_fact, 2);
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
                List<NdmSection> bsSec = new List<NdmSection>();

                BindingList<BSPoint> p =(BindingList<BSPoint>)pointBS.List;

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
            Close();
        }

        /// <summary>
        /// Расчет сечения по НДМ
        /// </summary>
        /// <param name="_My"></param>
        /// <returns></returns>
        private List<double> CalcNDM_My(double _My)
        {
            Dictionary<string, double> dictParams = DictCalcParams;
            
            List<double> l_Ky = new List<double>();
            
            CalcNDM calcNDM = new CalcNDM(m_BeamSection) { setup = ndmSetup, D = dictParams };
            Dictionary<string, double> res = calcNDM.RunMy(_My);

            if (res != null)
                l_Ky.Add(res["Ky"]);
            
            return l_Ky;
        }

        // Запустить расчет
        private void btnCalc_Click(object sender, EventArgs e)
        {            
            GenerateMesh();
            CalcNDM_My(1000);
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
            string pathToSvgFile;
                        
            BSMesh.Nx = ndmSetup.N;
            BSMesh.Ny = ndmSetup.M;
            BSMesh.MinAngle = ndmSetup.NSize;
            Tri.MinAngle = ndmSetup.NSize;

            double area = 10*10;
            double meshSize = Math.Max(BSMesh.Nx, BSMesh.Ny);            
            Tri.MaxArea = area / meshSize;
            BSMesh.MaxArea = Tri.MaxArea;
            
            BSMesh.FilePath = Path.Combine(Environment.CurrentDirectory, "Templates");
            Tri.FilePath = BSMesh.FilePath;
            
            if (m_BeamSection == BeamSection.Any)
            {
                List<PointF> pts = new List<PointF>();
                BeamSectionFromPoints(ref pts, Center);
                
                pathToSvgFile = BSCalcLib.Tri.CreateSectionContour(pts);

                _ = Tri.CalculationScheme();
            }
            else
            {
                throw new Exception("Не задано сечение");
            }

            // площади треугольников
            var triAreas = Tri.triAreas;
            // центры тяжести треугольников
            var triCGs = Tri.triCGs;

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
                
                Process.Start(new ProcessStartInfo { FileName = pathToSvgFile, UseShellExecute = true });
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
        }
    }
}

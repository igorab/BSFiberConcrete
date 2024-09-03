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

        public BeamSection BSBeamSection { private get; set; }

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

            Center = new PointF(0, 0);

            InnerPoints = new List<PointF>();

            BSBeamSection = BeamSection.Rect;
            w = 0;
            h = 0;
            Sz = new double[]  { 0, 0, 0, 0, 0, 0 };
        }

        private void InitRods()
        {
            if (m_RodPoints == null)
                return;

            List<BSRod> bsRods =  BSData.LoadBSRod(BSBeamSection);

            if (bsRods.Count == 0)
            {
                foreach (var _rod in m_RodPoints)
                    bsRods.Add(new BSRod() { CG_X = _rod.X, CG_Y = _rod.Y, D = 14.0 } );
            }

            RodBS.DataSource = bsRods;
        }

        private void InitPoints()
        {
            if (BSBeamSection == BeamSection.Rect)                 
            {
                BSSection.RectangleBeam(Sz);
                PointsSection = BSSection.SectionPoints;
                m_RodPoints = BSSection.RodPoints;               
            }
            else if (BSBeamSection == BeamSection.IBeam ||                      
                     BSBeamSection == BeamSection.LBeam)
            {                
                BSSection.IBeam(Sz, out PointsSection, out PointF _center);
                m_RodPoints = BSSection.RodPoints;
            }
            else if (BSBeamSection == BeamSection.TBeam)
            {
                BSSection.IBeam(Sz, out PointsSection, out PointF _center);
                m_RodPoints = BSSection.RodPoints;
            }
            else if (BSBeamSection == BeamSection.Ring)
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
            else if (BSBeamSection == BeamSection.None)
            {

            }
        }

        private void InitDataSource()
        {
            if (!string.IsNullOrEmpty(RebarClass))
                m_Diameters =  BSData.DiametersOfTypeRebar(RebarClass);

            InitPoints();
           
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
            pointBS.Add(new BSPoint(pt));
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

        private void SaveRods2DB()
        {
            if (RodBS == null || RodBS.List == null || RodBS.List.Count == 0)
                return;

            List<BSRod> bSRods = (List<BSRod>) RodBS.List; 
            
            BSData.SaveRods(bSRods, BSBeamSection);            
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
            if (BSBeamSection == BeamSection.None)
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
                    var d_nom = int.Parse(bSRodDataGridView.Rows[e.RowIndex].Cells["Dnom"].Value.ToString());

                    if (d_nom > 0)
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

        private void bSRodDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

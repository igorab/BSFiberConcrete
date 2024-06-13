using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;
using System.Xml.Linq;

namespace BSFiberConcrete.Section
{
    public partial class BSSectionChart : Form
    {
        public List<PointF> RodPoints 
        {
            get { return m_RodPoints; }
            set { m_RodPoints = value;}
        }

        public BeamSection m_BeamSection { get; set; }
        private List<PointF> m_RodPoints;

        private List<PointF> PointsSection;

        public PointF Center { get; set; }

        public float Wdth { set { w = value; } }
        public float Hght { set { h = value; } }

        public double[] Sz { get; set; }

        private float w;
        private float h;
        

        public BSSectionChart()
        {
            InitializeComponent();

            Center = new PointF(0, 0);

            m_BeamSection = BeamSection.Rect;
            w = 100;
            h = 100;
            Sz = new double[] { 79, 19, 22, 18, 81, 21 };

        }

        private void InitPoints()
        {
            if (m_BeamSection == BeamSection.Rect)                 
            {
                PointsSection = new List<PointF>()
                {
                    new PointF(0, 0),
                    new PointF(0, h) ,
                    new PointF(w, h),
                    new PointF(w, 0),
                    new PointF(0, 0)
                };

                m_RodPoints = new List<PointF>()
                {
                    //new PointF(0+4, h-4),
                    //new PointF(w/2f, h-4) ,
                    //new PointF(w-4, h-4),

                    new PointF(0+4, 4),
                    new PointF(w/2f, 4) ,
                    new PointF(w-4, 4),
                };
            }
            else if (m_BeamSection == BeamSection.IBeam ||                      
                     m_BeamSection == BeamSection.LBeam)
            {                
                BSSection.IBeam(Sz, out PointsSection, out PointF _center);

                m_RodPoints = new List<PointF>()
                {
                    //new PointF((w-4)/2f, h-4),
                    //new PointF(0, h-4) ,
                    //new PointF(-(w-4)/2f, h-4),

                    new PointF((w-4)/2f, 4),
                    new PointF(0, 4) ,
                    new PointF(-(w-4)/2f, 4),
                };
            }
            else if (m_BeamSection == BeamSection.TBeam)
            {
                BSSection.IBeam(Sz, out PointsSection, out PointF _center);

                m_RodPoints = new List<PointF>()
                {
                    new PointF((w-4)/2f, h-4),
                    new PointF(0, h-4) ,
                    new PointF(-(w-4)/2f, h-4)                    
                };
            }
            else if (m_BeamSection == BeamSection.Ring)
            {
                PointsSection = new List<PointF>();
                int amountOfEdges = 40;

                double radius =  Sz[1];
                double r2 = Sz[0];

                for (int k = 0; k <= amountOfEdges; k++)
                {
                    double x = Center.X + radius * Math.Cos(k * 2 * Math.PI / amountOfEdges);
                    double y = Center.Y + radius * Math.Sin(k * 2 * Math.PI / amountOfEdges);

                    PointsSection.Add(new PointF((float)x, (float)y));

                    m_RodPoints = new List<PointF>() { new PointF(0, -(h - 4)) };
                }
            }
        }

        private void InitDataSource()
        {
            InitPoints();
           
            int idx = 0;    
            foreach(PointF p in PointsSection) 
            {
                idx++;
                BSPoint bsPt = new BSPoint(p) { Num = idx };
                pointBS.Add(bsPt);
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

            Series serieSection = chart.Series[0];
            for (int j = 0; j < points.Count; j++)
            {
                var pt = points[j];
                serieSection.Points.Add(new DataPoint(pt.X, pt.Y));
            }

            Series serieRods = chart.Series[1];
            for (int j = 0; j < m_RodPoints.Count; j++)
            {
                var rod_pt = m_RodPoints[j];
                serieRods.Points.Add(new DataPoint(rod_pt.X, rod_pt.Y));
            }

            //serieSection.ChartType.



        }


        private void btnDraw_Click(object sender, EventArgs e)
        {            
            DrawFromDatasource(true);
        }

        private void chart_Click(object sender, EventArgs e)
        {

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

        private void btnSave_Click(object sender, EventArgs e)
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

        private void BSSectionChart_Load(object sender, EventArgs e)
        {
            InitDataSource();

            DrawFromDatasource();
        }
    }
}

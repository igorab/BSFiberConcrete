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

namespace BSFiberConcrete.Section
{
    public partial class BSSectionChart : Form
    {        
        private List<PointF> PointsRect;
        private List<PointF> PointsIBeam;
        private List<PointF> RodPoints;

        public float Wdth { set { w = value; } }
        public float Hght { set { h = value; } }

        float h = 60;
        float w = 80;

        public BSSectionChart()
        {
            InitializeComponent();
           
            PointsRect = new List<PointF>() 
            { 
                new PointF(0, 0), 
                new PointF(0, h) ,
                new PointF(w, h),
                new PointF(w, 0),
                new PointF(0, 0)
            };

            PointsIBeam = new List<PointF>()
            {
                new PointF(0, 0),
                new PointF(w, 0) ,
                new PointF(w, 20),
                new PointF(60, 20),
                new PointF(60, 40),
                new PointF(80, 40),
                new PointF(80, h),
                new PointF(0, h),
                new PointF(0, 40),
                new PointF(20, 40),
                new PointF(20, 20),
                new PointF(0, 20),
                new PointF(0, 0),
            };


            RodPoints = new List<PointF>()
            {
                new PointF(0+4, h-4),
                new PointF(w/2f, h-4) ,
                new PointF(w-4, h-4),

                new PointF(0+4, 4),
                new PointF(w/2f, 4) ,
                new PointF(w-4, 4),

            };
        }

        private void InitDataSource()
        {
            int idx = 0;    
            foreach(PointF p in PointsRect) 
            {
                idx++;
                BSPoint bsPt = new BSPoint(p) { Num = idx };
                pointBS.Add(bsPt);
            }
        }


        private void btnDraw_Click(object sender, EventArgs e)
        {
            var points = new List<PointF>(PointsIBeam);

            for (int j = 0; j < points.Count; j++)
            {
                var pt = points[j];
                chart.Series[0].Points.Add(new DataPoint(pt.X, pt.Y));
            }

            for (int j = 0; j < RodPoints.Count; j++)
            {
                var pt = RodPoints[j];
                chart.Series[1].Points.Add(new DataPoint(pt.X, pt.Y));
            }

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
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BSFiberConcrete.Section
{
    public partial class BSSectionGrid : Form
    {
        Point pt;
        Bitmap bm;
        Graphics g;
        
        private const int N = 20;

        public BSSectionGrid()
        {
            InitializeComponent();

            pt = new Point() { X = 0, Y = 0 } ;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BSSectionGrid_Load(object sender, EventArgs e)
        {
            bm = new Bitmap(pic.Width, pic.Height);
            g = Graphics.FromImage(bm);
            g.Clear(Color.White);
            pic.Image = bm;

            Pen p = new Pen(Color.Red);

            for (int i = 0; i < N - 1; i++)
            {
                g.DrawLine(p, new Point((pic.Width / N * (i + 1)), 0), new Point((pic.Width / N * (i + 1)), pic.Height));
                g.DrawLine(p, new Point(0, (pic.Height / N * (i + 1))), new Point(pic.Width, (pic.Height / N * (i + 1))));
            }

            pointBS.Add(new BSPoint(pt));
            pt.X += 10;
            pt.Y += 10;
            pointBS.Add(new BSPoint(pt));
        }

        private void DrawLines()
        {
            List<PointF> points = new List<PointF>();

            foreach (BSPoint point in pointBS)
            {
                PointF pt = new PointF(point.X, point.Y);
                points.Add(pt);
            }            

            Pen p = new Pen(Color.Blue);
           
            if (points != null)
                g.DrawCurve(p, points.ToArray());
                        
            //g.
            
        }

        private void subtractButton_Click(object sender, EventArgs e)
        {
            DrawLines();
            this.Refresh();
        }
       
        private void btnAdd_Click(object sender, EventArgs e)
        {
            pt.X = 2 * pt.X;
            pt.Y = 2 * pt.Y;
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
    }
    
}

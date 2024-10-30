using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace BSFiberConcrete.Beam
{
    public partial class BSBeamCalcView : Form
    {
        public BSBeamCalcView()
        {
            InitializeComponent();
        }
        private void BSBeamCalcView_Load(object sender, EventArgs e)
        {
            
                                    
            
        }
        private void panel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = sender as Panel;
            Graphics g = e.Graphics;
            
            Pen pen = new Pen(Color.Black, 3);
            Rectangle r = new Rectangle(10, panel.Height/2, panel.Width-20, 10);
            g.DrawRectangle(pen, r);
            Point[] triangle = new Point[] { new Point(r.Left+10, r.Bottom), new Point(r.Left, r.Bottom+10), new Point(r.Left + 20, r.Bottom + 10) };
            g.DrawPolygon(pen, triangle);
            triangle = new Point[] { new Point(r.Right - 10, r.Bottom), new Point(r.Right, r.Bottom + 10), new Point(r.Right - 20, r.Bottom + 10) };
            g.DrawPolygon(pen, triangle);
            pen = new Pen(Color.Red, 1);
            PointF pt1 = new PointF(r.Left, r.Top -50) ;
            PointF pt2 = new PointF(r.Right, r.Top - 50);
            g.DrawLine(pen, pt1, pt2);
            PointF pt_l = new PointF(r.Left, r.Top);
            g.DrawLine(pen, pt1, pt_l);
            g.DrawLine(pen, pt_l, new PointF(pt_l.X-10, pt_l.Y-10));
            g.DrawLine(pen, pt_l, new PointF(pt_l.X+10, pt_l.Y-10));
            PointF pt_r = new PointF(r.Right, r.Top);
            g.DrawLine(pen, pt2, pt_r);
            g.DrawLine(pen, pt_r, new PointF(pt_r.X-10, pt_r.Y-10));
            g.DrawLine(pen, pt_r, new PointF(pt_r.X+10, pt_r.Y-10));
            g.Dispose();
        }
    }
}

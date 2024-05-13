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
    public partial class BSSectionDraw : Form
    {
        Bitmap bm;
        Graphics g;
        Point px, py;        
        Pen p = new Pen(Color.Black, 4);
        private int N = 20;
        bool drawPoint = false;

        public BSSectionDraw()
        {
            InitializeComponent();
        }

        private void BSSectionDraw_Load(object sender, EventArgs e)
        {
            this.Width = 900;
            this.Height = 700;
            bm = new Bitmap(pic.Width, pic.Height);
            g = Graphics.FromImage(bm);
            g.Clear(Color.White);
            pic.Image = bm;

            Pen p = new Pen(Color.Red);
            
            for (int i = 0; i < N-1; i++)
            {
                g.DrawLine(p, new Point((pic.Width / N * (i + 1)), 0), new Point((pic.Width / N * (i + 1)), pic.Height));
                g.DrawLine(p, new Point(0, (pic.Height / N * (i + 1))), new Point(pic.Width, (pic.Height / N * (i + 1))));
            }
        }

        private void pic_MouseDown(object sender, MouseEventArgs e)
        {
            int X = pic.Width / N;
            int Y = pic.Height / N;

            for (int i = 1; i < N; i++)
            {
                if (e.X <= (X * i))
                {
                    for (int i1 = 1; i1 < N; i1++)
                    {
                        if (e.Y <= (Y * i1))
                        {
                            Graphics a = Graphics.FromImage(pic.Image);
                            Rectangle rect = new Rectangle((X * (i - 1)), (Y * (i1 - 1)), X, Y);
                            a.FillRectangle(Brushes.Wheat, rect);
                            if (drawPoint)
                            {
                                Rectangle rect_p = new Rectangle(rect.X+ rect.Width / 3, rect.Y + rect.Height / 3, rect.Width/5, rect.Width /5);
                                a.DrawEllipse(p, rect_p);
                            }
                            this.Refresh();                            
                            break;
                        }
                    }
                    break;
                }
            }
        }

        private void pic_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void pic_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void btnPoint_Click(object sender, EventArgs e)
        {
            drawPoint = !drawPoint;
        }

        private void btnSectionGrid_Click(object sender, EventArgs e)
        {
            BSSectionGrid sectionGrid   = new BSSectionGrid();
            sectionGrid.Show();
        }

        private void pic_Click(object sender, EventArgs e)
        {

        }
    }
}

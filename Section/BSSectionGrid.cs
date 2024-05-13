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
        Point px, py;
        Pen p = new Pen(Color.Black, 4);
        private int N = 20;

        public BSSectionGrid()
        {
            InitializeComponent();

            pt = new Point() { X = 1, Y = 1 } ;
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
        }

        private void CreatePointsAndSizes()
        {

            // Create the starting point.
            Point startPoint = new Point(1, 1);

            // Use the addition operator to get the end point.
            Point endPoint = startPoint + new Size(140, 150);

            // Draw a line between the points.
            g.DrawLine(SystemPens.Highlight, startPoint, endPoint);

            
            
            g.DrawString("The sizes are equal.",
                new Font(this.Font, FontStyle.Italic),
                Brushes.Indigo, 10.0F, 65.0F);
            
        }

        private void subtractButton_Click(object sender, EventArgs e)
        {
            CreatePointsAndSizes();
        }

        private void BSSectionGrid_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {            
            pointBS.Add(pt);
        }
    }
    
}

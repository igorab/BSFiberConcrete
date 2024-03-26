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
            //Graphics g = panel.CreateGraphics();

            //Pen pen = new Pen(Color.Black, 3);
            //Rectangle r = new Rectangle(0, 0, 1000, 100);
            //g.DrawRectangle(pen, r);

            //g.Dispose();

        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = sender as Panel;
            Graphics g = e.Graphics;

            //g.FillRectangle(new SolidBrush(Color.FromArgb(0, Color.Black)), p.DisplayRectangle);

            Pen pen = new Pen(Color.Black, 3);
            Rectangle r = new Rectangle(0, panel.Height/2, panel.Width, 10);
            g.DrawRectangle(pen, r);

            g.Dispose();
        }
    }
}

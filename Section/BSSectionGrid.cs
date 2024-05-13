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
        public BSSectionGrid()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BSSectionGrid_Load(object sender, EventArgs e)
        {

        }

        private void CreatePointsAndSizes(PaintEventArgs e)
        {

            // Create the starting point.
            Point startPoint = new Point(subtractButton.Size);

            // Use the addition operator to get the end point.
            Point endPoint = startPoint + new Size(140, 150);

            // Draw a line between the points.
            e.Graphics.DrawLine(SystemPens.Highlight, startPoint, endPoint);

            // Convert the starting point to a size and compare it to the
            // subtractButton size.  
            Size buttonSize = (Size)startPoint;
            if (buttonSize == subtractButton.Size)

            // If the sizes are equal, tell the user.
            {
                e.Graphics.DrawString("The sizes are equal.",
                    new Font(this.Font, FontStyle.Italic),
                    Brushes.Indigo, 10.0F, 65.0F);
            }
        }

        private void subtractButton_Click(object sender, EventArgs e)
        {
            
        }

        private void BSSectionGrid_Paint(object sender, PaintEventArgs e)
        {
            CreatePointsAndSizes(e);
        }
    }
    
}

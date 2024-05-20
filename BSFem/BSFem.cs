using BSCalcLib;

namespace BSFem
{
    public partial class BSFem : Form
    {
        public BSFem()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            var Z = FEMSolverODE.RunFromCode();

            if (Z != null)
            {
                textBox1.Text = string.Join("\t\n", Z);
            }
        }

        private void btnMesh_Click(object sender, EventArgs e)
        {
            BSCalcLib.Mesh.Generate();
        }

        /// <summary>
        /// Контур
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPoly_Click(object sender, EventArgs e)
        {
            BSCalcLib.TriPoly.Example();
        }

        /// <summary>
        /// Сегмент
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSegment_Click(object sender, EventArgs e)
        {
            BSCalcLib.TriSegment.Example();
        }
    }
}

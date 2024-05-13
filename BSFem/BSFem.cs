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
            FEMSolverODE.RunFromCode();
        }
    }
}

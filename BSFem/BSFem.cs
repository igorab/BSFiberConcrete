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
                textBox1.Text = string.Join("\t\n", Z) ;   
            }
        }
    }
}

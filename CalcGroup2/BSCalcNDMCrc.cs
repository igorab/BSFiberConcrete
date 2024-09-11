using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BSFiberConcrete.CalcGroup2
{
    public partial class BSCalcNDMCrc : Form
    {
        public BSCalcNDMCrc()
        {
            InitializeComponent();
        }

        private void BSCalcNDMCrc_Load(object sender, EventArgs e)
        {

        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Save2DB();

            MessageBox.Show($"{numFi1.Value * numFi3.Value * numPsiS.Value}");       
        }

        private void Save2DB()
        {
            //throw new NotImplementedException();
        }

        private void linkPsiS_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show($"Коэффициент Ψs  {numPsiS.Value}");
        }
    }
}

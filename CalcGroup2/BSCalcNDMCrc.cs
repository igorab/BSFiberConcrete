using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BSFiberConcrete.Lib;
namespace BSFiberConcrete.CalcGroup2
{
                public partial class BSCalcNDMCrc : Form
    {
        public NdmCrc NdmCrc { get; set; }
                                public string RebarType { get; set; }
        
                                public double N { get; set; }
        public BSCalcNDMCrc()
        {
            InitializeComponent();
        }
        private void BSCalcNDMCrc_Load(object sender, EventArgs e)
        {
            NdmCrc = BSData.LoadNdmCrc();
            NdmCrc.InitFi2(RebarType);
            NdmCrc.InitFi3(N);
            numFi1.Value = (decimal)NdmCrc.fi1;
            numFi2.Value = (decimal)NdmCrc.fi2;
            numFi3.Value = (decimal)NdmCrc.fi3;
            numPsiS.Value = (decimal)NdmCrc.psi_s;
            numMju_fv.Value = (decimal)NdmCrc.mu_fv;
        }
        private void buttonOk_Click(object sender, EventArgs e)
        {
            Save2DB();
            
            this.Close();
        }
        private void Save2DB()
        {
            try
            {
                BSData.SaveNdmCrc(NdmCrc);
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void linkPsiS_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show($"Коэффициент Ψs. Определяется по СП360 п 6.2.17", 
                "Трещиностойкость", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void linkMju_fv_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Коэффициент фибрового армирования по объему", 
                "Трещиностойкость", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void linkFi1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show($"Коэффициент, учитывающий продолжительность действия нагрузки\n" +
                            $"1.0- при непродолжительном действии\n" +
                            $"1.4- при непродолжительном действии"
                            , 
                "Трещиностойкость", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

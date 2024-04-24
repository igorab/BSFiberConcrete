using BSFiberConcrete.Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BSFiberConcrete.LocalStrength
{
    public partial class BSLocalStrength : Form
    {
        public BSLocalStrengthCalc StrengthCalc {  get; set; }
        

        public BSLocalStrength()
        {
            InitializeComponent();
        }
       
        private void btnCalc_Click(object sender, EventArgs e)
        {
            StrengthCalc.UseReinforcement = chboxReinforcement.Checked;            
            StrengthCalc.Scheme = cmbScheme.SelectedIndex + 1;

            StrengthCalc.RunCalc();

            localStressBindingSource.DataSource = new BindingList<LocalStress>(StrengthCalc.GetDS);
            
            
        }

        private void BSLocalStrength_Load(object sender, EventArgs e)
        {
            var ds = new BindingList<LocalStress> (StrengthCalc.GetDS);

            labelHeader.Text = StrengthCalc.SampleDescr();

            localStressBindingSource.DataSource = ds;
        }

        private void btnPrintReport_Click(object sender, EventArgs e)
        {
            BSLocalStrengthReport strengthReport = new BSLocalStrengthReport();
            strengthReport.ReportName = StrengthCalc.ReportName();
            strengthReport.SampleName = StrengthCalc.SampleName();
            strengthReport.SampleDescr = StrengthCalc.SampleDescr();

            if (localStressBindingSource.DataSource is BindingList<LocalStress>)
            {
                var ds = (BindingList<LocalStress>)localStressBindingSource.DataSource;
                strengthReport.DataSource = ds.ToList();
            }

            strengthReport.RunReport();
        }

        private void btnCloseForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

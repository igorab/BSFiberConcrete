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
        public bool IsShowScheme { get; set; }

        private BindingList<LocalStress> Ds;


        public BSLocalStrength()
        {
            InitializeComponent();

            IsShowScheme = true;
        }
       
        private void btnCalc_Click(object sender, EventArgs e)
        {
            StrengthCalc.UseReinforcement = chboxReinforcement.Checked;            
            StrengthCalc.Scheme = cmbScheme.SelectedIndex + 1;

            StrengthCalc.RunCalc();

            localStressBindingSource.DataSource = new BindingList<LocalStress>(StrengthCalc.GetDS);

            chboxReinforcement_CheckedChanged(null, null);

        }

        private void BSLocalStrength_Load(object sender, EventArgs e)
        {
            Ds = new BindingList<LocalStress> (StrengthCalc.GetDS);

            labelHeader.Text = StrengthCalc.SampleDescr();

            localStressBindingSource.DataSource = Ds;

            chboxReinforcement_CheckedChanged(null, null);
            
            labelScheme.Visible = IsShowScheme;
            cmbScheme.Visible = IsShowScheme;            
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

        private void dataGridLocalStrength_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            try
            {
                if (StrengthCalc.Dc.ContainsKey(Convert.ToString(dataGridLocalStrength.Rows[e.RowIndex].Cells[2].Value)))
                {
                    dataGridLocalStrength.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Beige;                                        
                }
            }
            catch
            {

            }
        }

        private void chboxReinforcement_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridLocalStrength.Rows)
                {
                    if (Convert.ToInt32(row.Cells[4].Value) == 1)
                    {
                        try
                        {
                            if (chboxReinforcement.Checked == true)
                                row.Visible = true;
                            else
                                row.Visible = false;
                        }
                        catch { }
                    }
                }
                dataGridLocalStrength.Refresh();
            }
            catch
            { }            
        }
    }
}

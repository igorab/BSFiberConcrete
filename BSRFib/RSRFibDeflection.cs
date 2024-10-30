using BSFiberConcrete.Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
namespace BSFiberConcrete.BSRFib
{
    public partial class RSRFibDeflection : Form
    {
        private BindingList<Deflection_f_aF> Dsf_aF;
        private List<string> beams;
        public RSRFibDeflection()
        {
            InitializeComponent();
            Dsf_aF = new BindingList<Deflection_f_aF>();
        }
        private void RSRFibDeflection_Load(object sender, EventArgs e)
        {
            beams = BSQuery.LoadBeamDeflection();
            idDataGridViewTextBoxColumn.Visible = false;
            foreach (var item in beams)
                cmbBeams.Items.Add(item);
            
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            Deflection_f_aF f_AF = new Deflection_f_aF();
            Deflection_f_aF last = Dsf_aF.LastOrDefault();
            f_AF.Id = textBeamId.Text;
            f_AF.Num = Dsf_aF.Count + 1;
            f_AF.f =  (last == null) ?  0.05 : Math.Round(last.f + 0.01, 2) ;
            f_AF.aF = BSRFibLabTensile.Defl_aF_f (f_AF.f);
            Dsf_aF.Add(f_AF);
                        dataGridDefl.Refresh();
        }
        private void cmbBeams_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBeams.SelectedIndex == 0)
            {
                deflectionfaFBindingSource.DataSource = Dsf_aF;
                btnAdd.Enabled = true;
                textBeamId.Visible = true;
                labelBeamId.Visible = true;
            }
            else
            {
                deflectionfaFBindingSource.DataSource = BSQuery.LoadRDeflection(Convert.ToString(cmbBeams.SelectedItem));
                btnAdd.Enabled = false;
                textBeamId.Visible = false;
                labelBeamId.Visible = false;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbBeams.SelectedIndex == 0)
            {
                BSQuery.SaveFibLabDeflection(Dsf_aF.ToList());
            }
        }
        private void dataGridDefl_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridDefl.Rows.Count > 0)
            {
                if (dataGridDefl.Columns[e.ColumnIndex].Name == "fColumn")
                {
                    var x = dataGridDefl.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    dataGridDefl.Rows[e.RowIndex].Cells[e.ColumnIndex+1].Value = BSRFibLabTensile.Defl_aF_f( (double) x);                    
                }
                               
            }
        }
                      
        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                BSRFibLabReport labReport = new BSRFibLabReport();
                labReport.ReportName = "Лаборатория: " + this.Text;
                labReport.SampleDescr = textBeamId.Text;
                labReport.SampleName = "Образец: " + cmbBeams.Text;
                Dictionary<string, double> LabResults = new Dictionary<string, double>()
                {
                };
                labReport.LabResults = LabResults;
                if (deflectionfaFBindingSource.DataSource is List<Deflection_f_aF>)
                {
                    var fafds = (List<Deflection_f_aF>)deflectionfaFBindingSource.DataSource;
                    labReport.D_f_aF = new List<Deflection_f_aF>(fafds.ToList());
                }
                labReport.RunReport();
                
            }
            catch (Exception _ex) 
            {
                MessageBox.Show(_ex.Message);
            }
        }
        private void deflectionfaFBindingSource_DataSourceChanged(object sender, EventArgs e)
        {
        }
        private void deflectionfaFBindingSource_CurrentChanged(object sender, EventArgs e)
        {
        }
        private void deflectionfaFBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
        }
    }
}

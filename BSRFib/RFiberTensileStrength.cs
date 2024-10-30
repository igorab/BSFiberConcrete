using BSFiberConcrete.Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    public partial class RFiberTensileStrength : Form
    {
        BSRFibLabTensileStats tensileStats;
        private List<FibLab> m_DsFibLab;
        public RFiberTensileStrength()
        {
            InitializeComponent();
            tensileStats = new BSRFibLabTensileStats();
        }
        private void RFiberTensileStrength_Load(object sender, EventArgs e)
        {
            m_DsFibLab = new List<FibLab> (BSData.LoadRFibLab());
            dataGridFFF.DataSource = m_DsFibLab;
            tensileStats.DsFibLab = m_DsFibLab;
        }
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                tensileStats.Calculate();
                numRfbt2n.Value = Convert.ToDecimal(tensileStats.Rfbt2n);
                numRfbt3n.Value = Convert.ToDecimal(tensileStats.Rfbt3n);
                numRFbtn.Value = Convert.ToDecimal(tensileStats.RFbtn);
            }
            catch  (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
        }
        private void btnReport_Click(object sender, EventArgs e)
        {
            BSRFibLabReport labReport = new BSRFibLabReport();
            labReport.ReportName = "Лаборатория";
            labReport.SampleDescr = this.Text;
            Dictionary<string, double>  LabResults = new Dictionary<string, double>()
            {
                {labelRfbt2n.Text, Convert.ToDouble(numRfbt2n.Value)},
                {labelRfbt3n.Text, Convert.ToDouble(numRfbt3n.Value)},
                {labelRFbtn.Text, Convert.ToDouble(numRFbtn.Value)},                
            };
            labReport.LabResults = LabResults;                        
            labReport.FibLab = new List<FibLab>(m_DsFibLab);
            labReport.RunReport();
        }
        private void btnDelCalc_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Удалить расчет?") == DialogResult.OK)
            {
                            }
        }
        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}

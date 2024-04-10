using BSFiberConcrete.Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BSFiberConcrete.BSRFib
{
    public partial class RFiberTensileStrength : Form
    {
        BSRFibLabTensileStats tensileStats;

        public RFiberTensileStrength()
        {
            InitializeComponent();

            tensileStats = new BSRFibLabTensileStats();
        }

        private void RFiberTensileStrength_Load(object sender, EventArgs e)
        {
            var ds = new List<FibLab> (BSData.LoadRFibLab());

            dataGridFFF.DataSource = ds;

            tensileStats.DsFibLab = ds;
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            tensileStats.Calculate();
            
            numRfbt2n.Value = Convert.ToDecimal(tensileStats.Rfbt2n);
            numRfbt3n.Value = (decimal)tensileStats.Rfbt3n;
            numRFbtn.Value = (decimal) tensileStats.RFbtn;


        }
    }
}

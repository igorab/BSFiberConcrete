using BSFiberConcrete.BSRFib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BSFiberConcrete
{
    /// <summary>
    /// Определение сопротивлений сталефибробетона растяжению и сжатию с учетом
    /// влияния фибрового армирования
    /// </summary>
    public partial class BSRFiber : Form
    {
        private double h = 60, b = 80;
      
        public BSRFiber()
        {
            InitializeComponent();
        }
               
        private void BSRFiber_Load(object sender, EventArgs e)
        {
            num_b.Value = (decimal)b;
            num_h.Value = (decimal)h;

            cmbEtaf.SelectedIndex = 1;
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            BSRFibCalc bSRFibCalc = new BSRFibCalc();

            var x = bSRFibCalc.Run();

            numRes.Value = (decimal) (!double.IsNaN(x) ? x : 0);            

            BSRFibReport bSRFibReport = new BSRFibReport();
            bSRFibReport.Run();                
        }        
    }
}

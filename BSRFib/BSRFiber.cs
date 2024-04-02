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
        private double h = 60, b = 80, l_f = 25;

        private Dictionary<int, double> EtaF = new Dictionary<int, double>
        {
            [0] = 0.8,
            [1] = 0.6,
            [2] = 0.9,
        };

        public BSRFiber()
        {
            InitializeComponent();
        }
               
        private void BSRFiber_Load(object sender, EventArgs e)
        {
            num_b.Value = (decimal)b;
            num_h.Value = (decimal)h;
            num_l_f.Value = (decimal)l_f;
            cmbEtaf.SelectedIndex = 1;
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            BSRFibCalc bSRFibCalc = new BSRFibCalc();
            bSRFibCalc.b = (double)num_b.Value;
            bSRFibCalc.h = (double)num_h.Value;
            bSRFibCalc.l_f = (double)num_l_f.Value;
            bSRFibCalc.eta_f = EtaF[cmbEtaf.SelectedIndex]; 

            var x = bSRFibCalc.Run(out Dictionary<string, double> calcResult);

            numRes.Value = (decimal) (!double.IsNaN(x) ? x : 0);            

            BSRFibReport bSRFibReport = new BSRFibReport();
            bSRFibReport.Res = calcResult;
            bSRFibReport.Run();                
        }        
    }
}

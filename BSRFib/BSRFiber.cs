using BSFiberConcrete.BSRFib;
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

namespace BSFiberConcrete
{
    /// <summary>
    /// Определение сопротивлений сталефибробетона растяжению и сжатию с учетом
    /// влияния фибрового армирования
    /// </summary>
    public partial class BSRFiber : Form
    {
        private double h = 60, b = 80, l_f = 25;

        BSRFibCalc bSRFibCalc;

        private Dictionary<int, double> EtaF = new Dictionary<int, double>
        {
            [0] = 0.8,
            [1] = 0.6,
            [2] = 0.9,
        };

        public BSRFiber()
        {
            InitializeComponent();

            bSRFibCalc = new BSRFibCalc();
        }

        private void num_b_ValueChanged(object sender, EventArgs e)
        {
            num_d_f_red.Value = (decimal)bSRFibCalc.d_f_red;
        }

        private void num_h_ValueChanged(object sender, EventArgs e)
        {
            num_d_f_red.Value = (decimal)bSRFibCalc.d_f_red; 
        }

        private void InitFibCalc()
        {
            bSRFibCalc.b = (double)num_b.Value;
            bSRFibCalc.h = (double)num_h.Value;
            bSRFibCalc.l_f = (double)num_l_f.Value;
            bSRFibCalc.eta_f = EtaF[cmbEtaf.SelectedIndex];                
            bSRFibCalc.RFiber = BSQuery.RFiberFind(cmb_RFiber.SelectedIndex+1);
            bSRFibCalc.Rb =  BSHelper.MPA2kgsm2( BSQuery.BetonTableFind(Convert.ToString(cmb_B.SelectedItem)).Rb );

        }

        private void BSRFiber_Load(object sender, EventArgs e)
        {
            bSRFibCalc.b = b;
            bSRFibCalc.h = h;
            bSRFibCalc.l_f = l_f;

            num_b.Value = (decimal)b;
            num_h.Value = (decimal)h;
            num_l_f.Value = (decimal)l_f;
            cmbEtaf.SelectedIndex = 1;
                       
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            InitFibCalc();

            var x = bSRFibCalc.Run(out Dictionary<string, double> calcResult);

            lblRes.Text = "---  ";
            foreach (var kvp in calcResult)
            {
                lblRes.Text += string.Format("{0}= {1}    ", kvp.Key, kvp.Value );
            }

            BSRFibReport bSRFibReport = new BSRFibReport();
            bSRFibReport.Res = calcResult;
            bSRFibReport.Run();                
        }        
    }
}

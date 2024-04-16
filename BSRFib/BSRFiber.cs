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
            bSRFibCalc.eta_f = BSRFibCalc.EtaF[cmbEtaf.SelectedIndex];

            bSRFibCalc.gamma_fb1 = BSRFibCalc.GammaFb[cmbEtaf.SelectedIndex];
            bSRFibCalc.gamma_fb2 = BSRFibCalc.GammaFb[cmbEtaf.SelectedIndex];

            bSRFibCalc.RFiber = BSQuery.RFiberFind(cmb_RFiber.SelectedIndex+1);
            bSRFibCalc.Rb =  BSHelper.MPA2kgsm2( BSQuery.BetonTableFind(Convert.ToString(cmb_B.SelectedItem)).Rb );

            bSRFibCalc.d_f_red = (double)num_d_f_red.Value;
            bSRFibCalc.mu_fv = (double)num_mu_fv.Value;

        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            PrintReport();
        }

        private void PrintReport()
        {
            try
            {
                BSRFibLabReport labReport = new BSRFibLabReport();

                labReport.ReportName = "Подбор";
                labReport.SampleDescr = labelDescr.Text;
                labReport.SampleName = "Rfbt3; Rfb с учетом фибры";

                Dictionary<string, string> LabItems = new Dictionary<string, string>()
                {
                    [label_h.Text] = Convert.ToString(num_h.Value),
                    [label_b.Text] = Convert.ToString(num_b.Value),
                    [labelEtaf.Text] = Convert.ToString(cmbEtaf.Text),
                    [label_l_f.Text] = Convert.ToString(num_l_f.Value),
                    [labelR_f_ser.Text] = Convert.ToString(cmb_RFiber.Text),
                    [labelB.Text] = cmb_B.Text,
                    [label_d_f_red.Text] = Convert.ToString(num_d_f_red.Value),
                    [label_mu_fv.Text] = Convert.ToString(num_mu_fv.Value),
                    ["Результат расчета:"] = Convert.ToString (lblRes.Text)
                };

                labReport.LabItems = LabItems;                
                labReport.RunReport();

            }
            catch (Exception _ex)
            {
                MessageBox.Show(_ex.Message);
            }
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
            cmb_RFiber.SelectedIndex = 1;
            cmb_B.SelectedIndex = 1;
                       
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            InitFibCalc();

            bSRFibCalc.LoadFibKorKn();

            _ = bSRFibCalc.Run(out Dictionary<string, double> calcResult);

            lblRes.Text = "-kg/sm2 :  ";
            foreach (var kvp in calcResult)
            {
                lblRes.Text += string.Format("{0}= {1}    ", kvp.Key, Math.Round(kvp.Value, 4));
            }

            BSRFibReport bSRFibReport = new BSRFibReport();
            bSRFibReport.Res = calcResult;
            bSRFibReport.Run();                
        }        
    }
}

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
    public partial class BSEFib : Form
    {
        public BSEFib()
        {
            InitializeComponent();
        }
        public double Eb => (double)numEb.Value;
        public double Ef => (double)numEf.Value;
        public double Mu_fv => (double)numMu_fv.Value;
        private double E_fb(double _Eb, double _Ef, double _mu_fv)
        {
            double e_fb = _Eb * (1- _mu_fv) + _Ef * _mu_fv;
            return e_fb;
        }
        public decimal E_fb_value(double _Eb, double _Ef, double _mu_fv) => (decimal)E_fb(_Eb, _Ef, _mu_fv);
        
        private void numEb_ValueChanged(object sender, EventArgs e)
        {
            numEfb.Value = E_fb_value(Eb, Ef, Mu_fv);
        }
        private void numEf_ValueChanged(object sender, EventArgs e)
        {
            numEfb.Value = E_fb_value(Eb, Ef, Mu_fv);
        }
        private void numMu_fv_ValueChanged(object sender, EventArgs e)
        {
            numEfb.Value = E_fb_value(Eb, Ef, Mu_fv);
        }
    }
}

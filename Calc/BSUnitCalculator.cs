using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BSFiberConcrete.Calc
{
    public partial class BSUnitCalculator : Form
    {
        public BSUnitCalculator()
        {
            InitializeComponent();
        }

        private void numFrom_ValueChanged(object sender, EventArgs e)
        {
            numTo.Value = (decimal) BSHelper.MPA2kgsm2((double?) numFrom.Value);
            numTo2.Value = (decimal)BSHelper.MPA2kNsm2((double?)numFrom.Value);
        }

        private void numFromF_ValueChanged(object sender, EventArgs e)
        {
            numToF.Value = (decimal)BSHelper.kN2Kgs((double?)numFromF.Value);
        }
    }
}

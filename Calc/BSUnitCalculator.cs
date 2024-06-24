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
        }
    }
}

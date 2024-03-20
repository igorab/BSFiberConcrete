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
    public partial class BSCalcResults : Form
    {
        public Dictionary<string, double> CalcResults { get; set; }

        public BSCalcResults()
        {
            CalcResults = new Dictionary<string, double>();

            InitializeComponent();
        }

        private void BSCalcResults_Load(object sender, EventArgs e)
        {

        }
    }
}

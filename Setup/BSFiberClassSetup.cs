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

namespace BSFiberConcrete.Setup
{
    public partial class BSFiberClassSetup : Form
    {
        public BSFiberClassSetup()
        {
            InitializeComponent();
        }

        private void BSFiberSetup_Load(object sender, EventArgs e)
        {
            fiberClassBS.DataSource = BSData.LoadFiberClass();         
        }
    }
}

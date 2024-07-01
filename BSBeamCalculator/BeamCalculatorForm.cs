using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BSBeamCalculator
{
    public partial class BeamCalculatorForm : Form
    {
        public BeamCalculatorForm()
        {
            InitializeComponent();
            BeamCalculatorControl beamCalculatorControl = new BeamCalculatorControl();
            this.Controls.Add(beamCalculatorControl);
        }
    }
}

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

            BeamCalculatorViewModel _beamCalcVM = new BeamCalculatorViewModel();
            BeamCalculatorControl beamCalculatorControl = new BeamCalculatorControl(_beamCalcVM);
            this.Controls.Add(beamCalculatorControl);
            beamCalculatorControl.Dock = System.Windows.Forms.DockStyle.Fill;
        }
    }
}

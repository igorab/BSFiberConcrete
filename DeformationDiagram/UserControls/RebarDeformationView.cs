using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BSFiberConcrete.DeformationDiagram.UserControls
{
    public partial class RebarDeformationView : UserControl
    {

        private double[] _valuesDeformation;


        public RebarDeformationView(double[] valuesDeformation)
        {
            InitializeComponent();
            
                        _valuesDeformation = valuesDeformation;

            UpdateDisabledNumeric();
        }



                                public void UpdateDisabledNumeric()
        {
            numEpsilonS0.Value = (decimal)_valuesDeformation[0];
            numEpsilonS1.Value = (decimal)_valuesDeformation[1];
        }



        private void numEpsilonS2_ValueChanged(object sender, EventArgs e)
        {
            numEps_s_ult.Value = numEpsilonS2.Value;
            _valuesDeformation[2] = (double)numEpsilonS2.Value;
        }

    }
}

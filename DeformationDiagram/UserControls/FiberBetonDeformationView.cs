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
    public partial class FiberBetonDeformationView : UserControl
    {

        private double[] _valuesDeformation;


        public FiberBetonDeformationView(double[] valuesDeformation)
        {
            InitializeComponent();
            
            _valuesDeformation = valuesDeformation;

            //e0
            //e1
            //e2 
            //et0
            //et1
            //et2 
            //et3 
            numEps_fb0.Value = (decimal)_valuesDeformation[0];
            numEps_fb2.Value = (decimal)_valuesDeformation[2];
            numEps_fbt2.Value = (decimal)_valuesDeformation[5];
            numEps_fbt3.Value = (decimal)_valuesDeformation[6];

            UpdateDisabledNumeric();
        }


        /// <summary>
        /// Обновить состояние объектов NumericUpDown со статусом Enabled false
        /// </summary>
        public void UpdateDisabledNumeric()
        {
            numEps_fb1.Value = (decimal)_valuesDeformation[1];
            numEps_fbt0.Value = (decimal)_valuesDeformation[3];
            numEps_fbt1.Value = (decimal)_valuesDeformation[4];
        }




        private void numEps_fb0_ValueChanged(object sender, EventArgs e)
        {
            _valuesDeformation[0] = (double)numEps_fb0.Value;
        }

        private void numEps_fb2_ValueChanged(object sender, EventArgs e)
        {
            numEps_fb_ult.Value = numEps_fb2.Value;
            _valuesDeformation[2] = (double)numEps_fb2.Value;
        }


        private void numEps_fbt2_ValueChanged(object sender, EventArgs e)
        {
            _valuesDeformation[5] = (double)numEps_fbt2.Value;

        }
        private void numEps_fbt3_ValueChanged(object sender, EventArgs e)
        {
            numEps_fbt_ult.Value = numEps_fbt3.Value;
            _valuesDeformation[6] = (double)numEps_fbt3.Value;

        }

    }
} 

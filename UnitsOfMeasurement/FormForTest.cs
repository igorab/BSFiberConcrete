using BSFiberConcrete.UnitsOfMeasurement.PhysicalQuantities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace BSFiberConcrete.UnitsOfMeasurement
{
    public partial class FormForTest : Form
    {
        LameUnitConverter _UnitConverter;
        public FormForTest()
        {
            InitializeComponent();
            _UnitConverter = new LameUnitConverter();
            cmb_Len1.DataSource = LengthMeasurement.ListOfName;
            cmb_Len2.DataSource = LengthMeasurement.ListOfName;
            cmb_Force1.DataSource = ForceMeasurement.ListOfName;
            cmb_Force2.DataSource = ForceMeasurement.ListOfName;
            cmb_MomentOfForce1.DataSource = MomentOfForceMeasurement.ListOfName;
            cmb_MomentOfForce2.DataSource = MomentOfForceMeasurement.ListOfName;
        }
        private void cmb_Len1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cmb_Len1.SelectedIndex;
            _UnitConverter.ChangeCustomUnitLength(index);
            res_Len.Text = _UnitConverter.СonvertLength((double)num_Len.Value).ToString();
        }
        private void cmb_Len2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cmb_Len2.SelectedIndex;
            _UnitConverter.ChangeModelUnitLength(index);
            res_Len.Text = _UnitConverter.СonvertLength((double)num_Len.Value).ToString();
        }
        private void cmb_Force1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cmb_Force1.SelectedIndex;
            _UnitConverter.ChangeCustomUnitForce(index);
            res_Force.Text = _UnitConverter.ConvertForce((double)num_Force.Value).ToString();
        }
        private void cmb_Force2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cmb_Force2.SelectedIndex;
            _UnitConverter.ChangeModelUnitForce(index);
            res_Force.Text = _UnitConverter.ConvertForce((double)num_Force.Value).ToString();
        }
        private void cmb_MomentOfForce1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cmb_MomentOfForce1.SelectedIndex;
            _UnitConverter.ChangeCustomUnitMomentOfForce(index);
            res_MomentOfForce.Text = _UnitConverter.ConvertMomentOfForce((double)num_MomentOfForce.Value).ToString();
        }
        private void cmb_MomentOfForce2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cmb_MomentOfForce2.SelectedIndex;
            _UnitConverter.ChangeModelUnitMomentOfForce(index);
            res_MomentOfForce.Text = _UnitConverter.ConvertMomentOfForce((double)num_MomentOfForce.Value).ToString();
        }
        private void num_Len_ValueChanged(object sender, EventArgs e)
        {
            res_Len.Text = _UnitConverter.СonvertLength((double)num_Len.Value).ToString();
        }
        private void num_Force_ValueChanged(object sender, EventArgs e)
        {
            res_Force.Text = _UnitConverter.ConvertForce((double)num_Force.Value).ToString();
        }
        private void num_MomentOfForce_ValueChanged(object sender, EventArgs e)
        {
            res_MomentOfForce.Text = _UnitConverter.ConvertMomentOfForce((double)num_MomentOfForce.Value).ToString();
        }
    }
}

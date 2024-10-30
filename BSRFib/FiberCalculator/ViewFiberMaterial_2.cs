using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace BSFiberConcrete.BSRFib.FiberCalculator.model
{
    public partial class ViewFiberMaterial_2 : UserControl
    {
        private FiberConcreteCalculator _model;
        public ViewFiberMaterial_2(FiberConcreteCalculator model)
        {
            InitializeComponent();
            _model = model;
            
                        num_d.Minimum = 0.1m;
            num_S.Minimum = 0.1m;
            num_l.Minimum = 0.1m;
                        lab_Hf.DataBindings.Add(new Binding("Text", _model.Fiber, "Hita_f", true, DataSourceUpdateMode.OnPropertyChanged));
            lab_Gamma_fb1.DataBindings.Add(new Binding("Text", _model.Fiber, "Gamma_fb1", true, DataSourceUpdateMode.OnPropertyChanged));
        }
        private void ViewFiberMaterial_2_Load(object sender, EventArgs e)
        {
            num_Rf_ser.Value = 460m;
            num_Rf .Value = 440m;
            num_Ef .Value = 210000m;
            num_d.Value = 0.3m;
            num_S.Value = 0.15m;
            num_l.Value = 20m;
            checkBox1.Checked = false;
            num_S.Enabled = false;
            cmbFiberMaterialType.DataSource = _model.Fiber.GetFiberType();
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool condition = checkBox1.Checked;
            
            if (condition)
            {
                _model.Fiber.Set_Diameter(0);
                num_S.Enabled = true;
                num_d.Enabled = false;
            }
            else
            {
                _model.Fiber.Set_Square(0);
                _model.Fiber.Set_Diameter((double)num_d.Value);
                num_S.Enabled = false;
                num_d.Enabled = true;
            }
        }
        private void cmbFiberMaterialType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indexSelected = cmbFiberMaterialType.SelectedIndex;
                        _model.Fiber.SetIndexFiberType(indexSelected,true);
        }
        private void num_Rf_ser_ValueChanged(object sender, EventArgs e)
        {
            _model.Fiber.Set_Rf_ser((double)num_Rf_ser.Value);
        }
        private void num_Rf_ValueChanged(object sender, EventArgs e)
        {
            _model.Fiber.Set_Rf((double)num_Rf.Value);
        }
        private void num_Ef_ValueChanged(object sender, EventArgs e)
        {
            _model.Fiber.Set_Ef((double)num_Ef.Value);
        }
        private void num_d_ValueChanged(object sender, EventArgs e)
        {
            _model.Fiber.Set_Diameter((double)num_d.Value);
        }
        private void num_S_ValueChanged(object sender, EventArgs e)
        {
            _model.Fiber.Set_Square((double)num_S.Value);
        }
        private void num_l_ValueChanged(object sender, EventArgs e)
        {
            _model.Fiber.Set_Length((double)num_l.Value);
            _model.FiberCoef.SetLen_f(_model.Fiber.Length);
        }
    }
}

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
    public partial class ViewCalcOfLenRebar : Form
    {

        private CalcOfLenRebar _model;
        public ViewCalcOfLenRebar(CalcOfLenRebar calcOfLen = null)
        {
            if (calcOfLen == null)
            {
                _model = new CalcOfLenRebar();
            }

            InitializeComponent();

            labHita_1.DataBindings.Add(new Binding("Text", _model.Rebar, "Hita_1", true, DataSourceUpdateMode.OnPropertyChanged));
            labHita_2.DataBindings.Add(new Binding("Text", _model.Rebar, "Hita_2", true, DataSourceUpdateMode.OnPropertyChanged));
            lab_Rs.DataBindings.Add(new Binding("Text", _model.Rebar, "Rs", true, DataSourceUpdateMode.OnPropertyChanged));

            lab_Rfbt.DataBindings.Add(new Binding("Text", _model.FiberConcrete, "Rfbt", true, DataSourceUpdateMode.OnPropertyChanged));

            lab_l0_an.DataBindings.Add(new Binding("Text", _model, "l0_an", true, DataSourceUpdateMode.OnPropertyChanged));
            lab_lan.DataBindings.Add(new Binding("Text", _model, "lan", true, DataSourceUpdateMode.OnPropertyChanged));
        }

        private void ViewCalcOfLenRebar_Load(object sender, EventArgs e)
        {

            cmbRebarType.DataSource = _model.Rebar.GetRebarTypes();
            rbtnTension.Checked = true;
            cmbFiberConcreteClass.DataSource = _model.FiberConcrete.Get_Bft();

        }

        private void cmbRebarType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cmbRebarType.SelectedIndex;
            _model.Rebar.SetTypeIndex(index);
            cmbRebarDiameters.DataSource = _model.Rebar.GetDiameters();


        }

        private void cmbRebarDiameters_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cmbRebarDiameters.SelectedIndex;
            _model.Rebar.SetDiameterIndex(index);
        }

        private void rbtnTension_CheckedChanged(object sender, EventArgs e)
        {
            _model.SetCoefAlpha_1(0);
        }

        private void rbtnCompress_CheckedChanged(object sender, EventArgs e)
        {
            _model.SetCoefAlpha_1(1);
        }

        private void cmbFiberConcreteClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cmbFiberConcreteClass.SelectedIndex;
            _model.FiberConcrete.Set_Bft(index);
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            _model.Calculate();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            _model.GenerateReport();
        }
    }
}

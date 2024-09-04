using BSFiberConcrete.BSRFib.FiberCalculator.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BSFiberConcrete.BSRFib.FiberCalculator
{
    public partial class ViewFiberConcreteCalc : Form
    {

        private FiberConcreteCalculator _model;

        /// <summary>
        /// ячейка таблицы tableForFiberMaterial для добавления ViewFiberMaterial
        /// </summary>
        private TableLayoutPanelCellPosition _cellPositionForFiberMaterila;


        public ViewFiberConcreteCalc(FiberConcreteCalculator model = null)
        {
            if (model == null)
            { model = new FiberConcreteCalculator(); }
            _model = model;

            _cellPositionForFiberMaterila = new TableLayoutPanelCellPosition(0,1);

            InitializeComponent();
            
            // устанавливаем привязку полей


            lab_Rb.DataBindings.Add(new Binding("Text", _model.Beton, "Rb", true, DataSourceUpdateMode.OnPropertyChanged));
            lab_Rb_ser.DataBindings.Add(new Binding("Text", _model.Beton, "Rb_ser", true, DataSourceUpdateMode.OnPropertyChanged));
            lab_Rbt.DataBindings.Add(new Binding("Text", _model.Beton, "Rbt", true, DataSourceUpdateMode.OnPropertyChanged));
            lab_Rbt_ser.DataBindings.Add(new Binding("Text", _model.Beton, "Rbt_ser", true, DataSourceUpdateMode.OnPropertyChanged));
            lab_Eb.DataBindings.Add(new Binding("Text", _model.Beton, "Eb", true, DataSourceUpdateMode.OnPropertyChanged));

            lab_Kor.DataBindings.Add(new Binding("Text", _model, "Kor", true, DataSourceUpdateMode.OnPropertyChanged));
            lab_Kn.DataBindings.Add(new Binding("Text", _model, "Kn", true, DataSourceUpdateMode.OnPropertyChanged));
            lab_l_f_an.DataBindings.Add(new Binding("Text", _model, "l_f_an", true, DataSourceUpdateMode.OnPropertyChanged));
            lab_mu_fv_min.DataBindings.Add(new Binding("Text", _model, "mu_fv_min", true, DataSourceUpdateMode.OnPropertyChanged));
            lab_C_max.DataBindings.Add(new Binding("Text", _model, "C_max", true, DataSourceUpdateMode.OnPropertyChanged));
            lab_l_f_min.DataBindings.Add(new Binding("Text", _model, "l_f_min", true, DataSourceUpdateMode.OnPropertyChanged));
            lab_Rfbt3.DataBindings.Add(new Binding("Text", _model, "R_fbt3", true, DataSourceUpdateMode.OnPropertyChanged));
            lab_Rfbt3_n.DataBindings.Add(new Binding("Text", _model, "R_fbt3_n", true, DataSourceUpdateMode.OnPropertyChanged));
            lab_Rfb.DataBindings.Add(new Binding("Text", _model, "R_fb", true, DataSourceUpdateMode.OnPropertyChanged));
            lab_mu_fa.DataBindings.Add(new Binding("Text", _model, "mu_fa", true, DataSourceUpdateMode.OnPropertyChanged));
            lab_mu_1_fa.DataBindings.Add(new Binding("Text", _model, "mu_1_fa", true, DataSourceUpdateMode.OnPropertyChanged));
            lab_Efb.DataBindings.Add(new Binding("Text", _model, "E_fb", true, DataSourceUpdateMode.OnPropertyChanged));
            lab_Gfb.DataBindings.Add(new Binding("Text", _model, "G_fb", true, DataSourceUpdateMode.OnPropertyChanged));
            txtb4Message.DataBindings.Add(new Binding("Text", _model, "message", true, DataSourceUpdateMode.OnPropertyChanged));
        }


        private void ViewFiberConcreteCalc_Load(object sender, EventArgs e)
        {
            // сечение
            num_b.Value = 100m;
            num_h.Value = 200m;

            // коэф фибрового армирования
            numMu_fv.Value = 0.015m;
            _model.SetMu_fv((double)numMu_fv.Value);
            cBox_MuMin.Checked = false;

            // характеристики бетона
            cmbConcreteType.DataSource = _model.Beton.GetConcreteType();

            // установка значения коэф
            rbElement_C_1.Checked = true;

            // выбор формы для управления параметрами фибры
            rbFiberMaterila_1.Checked = true;
        }


        private void cBox_MuMin_CheckedChanged(object sender, EventArgs e)
        {
            if (cBox_MuMin.Checked)
            {
                numMu_fv.Enabled = false;
                _model.SetMu_fv(0);
            }
            else 
            { 
                numMu_fv.Enabled = true;
                _model.SetMu_fv((double)numMu_fv.Value);
            }
        }

        private void num_h_ValueChanged(object sender, EventArgs e)
        {
            _model.SetSectionH((double)num_h.Value);
        }

        private void num_b_ValueChanged(object sender, EventArgs e)
        {
            _model.SetSectionB((double)num_b.Value);
        }

        private void cmbConcrete_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indexConcrete = cmbConcrete.SelectedIndex;
            _model.Beton.SetIndexConcreteClass(indexConcrete);
        }

        private void rbElement_C_1_CheckedChanged(object sender, EventArgs e)
        {
            _model.Fiber.SetCoef_C(0);
        }

        private void rbElement_C_2_CheckedChanged(object sender, EventArgs e)
        {
            _model.Fiber.SetCoef_C(1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _model.Calculate();
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            _model.GenerateReport();
        }

        private void cmbConcreteType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indexConcrete = cmbConcreteType.SelectedIndex;
            // из-за смещения индекса первого элемента таблицы (индексация не с нуля а с единицы)
            _model.Beton.SetIndexConcretKind(indexConcrete);
            cmbConcrete.DataSource = _model.Beton.GetConcreteClass();
        }

        private void rbFiberMaterila_1_CheckedChanged(object sender, EventArgs e)
        {
            if (tableForFiberMaterial.Controls.Count == 2)
            { tableForFiberMaterial.Controls.RemoveAt(1); }

            ViewFiberMaterial_1 viewFM = new ViewFiberMaterial_1(_model);
            tableForFiberMaterial.Controls.Add(viewFM, _cellPositionForFiberMaterila.Column, _cellPositionForFiberMaterila.Row);
            viewFM.Dock = System.Windows.Forms.DockStyle.Fill;
        }

        private void rbFiberMaterila_2_CheckedChanged(object sender, EventArgs e)
        {
            if (tableForFiberMaterial.Controls.Count == 2)
            { tableForFiberMaterial.Controls.RemoveAt(1); }
            ViewFiberMaterial_2 viewFM = new ViewFiberMaterial_2(_model);
            tableForFiberMaterial.Controls.Add(viewFM, _cellPositionForFiberMaterila.Column, _cellPositionForFiberMaterila.Row);
            viewFM.Dock = System.Windows.Forms.DockStyle.Fill;

        }
    }
}

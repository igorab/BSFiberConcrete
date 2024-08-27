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
    public partial class ViewFiberConcrateCalc : Form
    {

        private FiberConcrateCalculator _model;


        public ViewFiberConcrateCalc(FiberConcrateCalculator model = null)
        {
            if (model == null)
            { model = new FiberConcrateCalculator(); }
            _model = model;

            InitializeComponent();



            // устанавливаем привязку полей
            lab_Fiber_d.DataBindings.Add(new Binding("Text", _model.Fiber, "DescriptionGeometry", true, DataSourceUpdateMode.OnPropertyChanged));

            lab_Hf.DataBindings.Add(new Binding("Text", _model.Fiber, "Hita_f", true, DataSourceUpdateMode.OnPropertyChanged));
            lab_Rf_ser.DataBindings.Add(new Binding("Text", _model.Fiber, "Rf_ser", true, DataSourceUpdateMode.OnPropertyChanged));
            lab_Rf.DataBindings.Add(new Binding("Text", _model.Fiber, "Rf", true, DataSourceUpdateMode.OnPropertyChanged));
            lab_Ef.DataBindings.Add(new Binding("Text", _model.Fiber, "Ef", true, DataSourceUpdateMode.OnPropertyChanged));
            lab_Gamma_fb1.DataBindings.Add(new Binding("Text", _model.Fiber, "Gamma_fb1", true, DataSourceUpdateMode.OnPropertyChanged));

            lab_Rb.DataBindings.Add(new Binding("Text", _model.Beton, "Rb", true, DataSourceUpdateMode.OnPropertyChanged));
            lab_Rb_ser.DataBindings.Add(new Binding("Text", _model.Beton, "Rb_ser", true, DataSourceUpdateMode.OnPropertyChanged));
            lab_Rbt.DataBindings.Add(new Binding("Text", _model.Beton, "Rbt", true, DataSourceUpdateMode.OnPropertyChanged));
            lab_Rbt_ser.DataBindings.Add(new Binding("Text", _model.Beton, "Rbt_ser", true, DataSourceUpdateMode.OnPropertyChanged));
            lab_Eb.DataBindings.Add(new Binding("Text", _model.Beton, "Eb", true, DataSourceUpdateMode.OnPropertyChanged));
        }


        private void ViewFiberConcrateCalc_Load(object sender, EventArgs e)
        {

            
            num_b.Value = 150m;
            num_h.Value = 125m;
            numMu_fv.Value = 0.015m;
            _model.SetSectionB((double)num_b.Value);
            _model.SetSectionH((double)num_h.Value);
            _model.SetMu_fv((double)numMu_fv.Value);
            cBox_MuMin.Checked = false;



            // Определяем данные для выпадающего списка типов фибры
            cmbFiberMaterial.DataSource = _model.Fiber.GetFiberTypes();
            int indexFiber = 0;
            cmbFiberMaterial.SelectedIndex = indexFiber;
            cmbFiber_d.SelectedIndex = 0;
            cmbFiber_l.SelectedIndex = 0;
            ChangeFiberType(0);

            cmbConcrete.DataSource = _model.Beton.GetFiberTypes();
            _model.Beton.SetIndexConcreteType(0);

            rbElement_C_1.Checked = true;
            _model.Fiber.SetCoef_C(0);

        }


        /// <summary>
        /// Изменить тип фбиры. Обнавляется выпадающие списки диаметров и длин. Обновляется содержание модели
        /// </summary>
        /// <param name="indexFiber">номер типа фибры из выпадающего списка</param>
        private void ChangeFiberType(int indexFiber)
        {
            _model.Fiber.SetIndexFiberType(indexFiber); // обновили модель
            cmbFiber_d.DataSource = _model.Fiber.GetFiberGeometries();  // обновили view
            ChangeFiberGeometry(0);
        }


        /// <summary>
        /// Изменить диметр фибры. Обновить выпадающий список длин. Обновить содержимое моедли
        /// /// </summary>
        /// <param name="indexGeometry">номер диаметра из выпадающего списка</param>
        private void ChangeFiberGeometry(int indexGeometry)
        {
            _model.Fiber.SetIndexFiberGeometry(indexGeometry);          // обновили модель
            //lab_Fiber_d.Text = _model.Fiber.DescriptionGeometry;
            cmbFiber_l.DataSource = _model.Fiber.GetFiberLengths();     // обновили view
            ChangeFiberLength(0);

        }


        /// <summary>
        /// Изменить длину фибры. Обнавляется содержание модели
        /// /// </summary>
        /// <param name="indexLength">номер длины из выпадающего списка</param>
        private void ChangeFiberLength(int indexLength)
        {
            _model.Fiber.SetIndexFiberLength(indexLength);
        }


        private void cmbFiberMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indexFiber = cmbFiberMaterial.SelectedIndex;
            ChangeFiberType(indexFiber);
        }


        private void cmbFiber_d_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indexGeometry = cmbFiber_d.SelectedIndex;
            ChangeFiberGeometry(indexGeometry);
        }


        private void cmbFiber_l_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indexLength = cmbFiber_l.SelectedIndex;
            ChangeFiberLength(indexLength);
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
            _model.Beton.SetIndexConcreteType(indexConcrete);
        }

        private void rbElement_C_1_CheckedChanged(object sender, EventArgs e)
        {
            _model.Fiber.SetCoef_C(0);
        }

        private void rbElement_C_2_CheckedChanged(object sender, EventArgs e)
        {
            _model.Fiber.SetCoef_C(1);
        }
    }
}

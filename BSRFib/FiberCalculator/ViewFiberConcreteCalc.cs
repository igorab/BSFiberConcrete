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


        public ViewFiberConcreteCalc(FiberConcreteCalculator model = null)
        {
            if (model == null)
            { model = new FiberConcreteCalculator(); }
            _model = model;

            InitializeComponent();
            
            // устанавливаем привязку полей

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

            lab_Kor.DataBindings.Add(new Binding("Text", _model.FiberCoef, "Kor", true, DataSourceUpdateMode.OnPropertyChanged));
            lab_Kn.DataBindings.Add(new Binding("Text", _model.FiberCoef, "Kn", true, DataSourceUpdateMode.OnPropertyChanged));


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

            
            num_b.Value = 100m;
            num_h.Value = 200m;
            
            //_model.SetSectionB((double)num_b.Value);
            //_model.SetSectionH((double)num_h.Value);
            //lab_Kor.Text = "0.5";
            //lab_Kn.Text = "0.5";


            numMu_fv.Value = 0.015m;
            _model.SetMu_fv((double)numMu_fv.Value);
            cBox_MuMin.Checked = false;

            // Определяем данные для выпадающих списков зависящий от типа фибры
            cmbFiberMaterial.DataSource = _model.Fiber.GetFiberTypes();
            // (После обновления dataSource вызывается событие SelectedIndexChanged, SelectedIndex = 0 )
            // Поэтому после обновления dataSource срабатывает такая последовательность вызовов
            // Обновление       cmbFiberMaterial.DataSource  -> вызов cmbFiberMaterial_SelectedIndexChanged ->  вызов ChangeFiberType ->
            // -> Обновление    cmbFiber_Geometry.DataSource -> вызов cmbFiber_Geometry_SelectedIndexChanged -> вызов ChangeFiberGeometry ->
            // -> обновление    cmbFiber_l.DataSource        -> вызов  cmbFiber_l_SelectedIndexChanged       -> вызов ChangeFiberLength   

            //int indexFiber = 0;
            //cmbFiberMaterial.SelectedIndex = indexFiber;
            //cmbFiber_Geometry.SelectedIndex = 0;
            //cmbFiber_l.SelectedIndex = 0;
            //ChangeFiberType(0);

            cmbConcrete.DataSource = _model.Beton.GetFiberTypes();
            //_model.Beton.SetIndexConcreteType(0);

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
            cmbFiber_Geometry.DataSource = _model.Fiber.GetFiberGeometries();  // обновили view
            //ChangeFiberGeometry(0);
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
            //ChangeFiberLength(0);

        }


        /// <summary>
        /// Изменить длину фибры. Обнавляется содержание модели
        /// /// </summary>
        /// <param name="indexLength">номер длины из выпадающего списка</param>
        private void ChangeFiberLength(int indexLength)
        {
            _model.Fiber.SetIndexFiberLength(indexLength);
            _model.FiberCoef.SetLen_f(_model.Fiber.Length);
        }


        private void cmbFiberMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indexFiber = cmbFiberMaterial.SelectedIndex;
            ChangeFiberType(indexFiber);
        }


        private void cmbFiber_Geometry_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indexGeometry = cmbFiber_Geometry.SelectedIndex;
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

        private void button1_Click(object sender, EventArgs e)
        {
            _model.Calculate();
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            _model.GenerateReport();
        }
    }
}

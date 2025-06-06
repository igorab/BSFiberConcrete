﻿using System;
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
    public partial class ViewFiberMaterial_1 : UserControl
    {

        private FiberConcreteCalculator _model;

        public ViewFiberMaterial_1(FiberConcreteCalculator model)
        {

            _model = model;


            InitializeComponent();


            lab_Hf.DataBindings.Add(new Binding("Text", _model.Fiber, "Hita_f", true, DataSourceUpdateMode.OnPropertyChanged));
            lab_Rf_ser.DataBindings.Add(new Binding("Text", _model.Fiber, "Rf_ser", true, DataSourceUpdateMode.OnPropertyChanged));
            lab_Rf.DataBindings.Add(new Binding("Text", _model.Fiber, "Rf", true, DataSourceUpdateMode.OnPropertyChanged));
            lab_Ef.DataBindings.Add(new Binding("Text", _model.Fiber, "Ef", true, DataSourceUpdateMode.OnPropertyChanged));
            lab_Gamma_fb1.DataBindings.Add(new Binding("Text", _model.Fiber, "Gamma_fb1", true, DataSourceUpdateMode.OnPropertyChanged));
        }

        private void ViewFiberMaterial_1_Load(object sender, EventArgs e)
        {
            // Определяем данные для выпадающих списков зависящий от типа фибры
            cmbFiberMaterial.DataSource = _model.Fiber.GetFiberKind();
            // (После обновления dataSource вызывается событие SelectedIndexChanged, SelectedIndex = 0 )
            // Поэтому после обновления dataSource срабатывает такая последовательность вызовов
            // Обновление       cmbFiberMaterial.DataSource  -> вызов cmbFiberMaterial_SelectedIndexChanged ->  вызов ChangeFiberType ->
            // -> Обновление    cmbFiber_Geometry.DataSource -> вызов cmbFiber_Geometry_SelectedIndexChanged -> вызов ChangeFiberGeometry ->
            // -> обновление    cmbFiber_l.DataSource        -> вызов  cmbFiber_l_SelectedIndexChanged       -> вызов ChangeFiberLength 
        }

        private void cmbFiberMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indexFiber = cmbFiberMaterial.SelectedIndex;
            ChangeFiberType(indexFiber);
        }


        /// <summary>
        /// Изменить тип фбиры. Обновляется выпадающие списки диаметров и длин. Обновляется содержание модели
        /// </summary>
        /// <param name="indexFiber">номер типа фибры из выпадающего списка</param>
        private void ChangeFiberType(int indexFiber)
        {
            _model.Fiber.SetIndexFiberKind(indexFiber); // обновили модель
            cmbFiber_Geometry.DataSource = _model.Fiber.GetFiberGeometries();  // обновили view
        }


        /// <summary>
        /// Изменить диметр фибры. Обновить выпадающий список длин. Обновить содержимое моедли
        /// /// </summary>
        /// <param name="indexGeometry">номер диаметра из выпадающего списка</param>
        private void ChangeFiberGeometry(int indexGeometry)
        {
            _model.Fiber.SetIndexFiberGeometry(indexGeometry);          // обновили модель
            cmbFiber_l.DataSource = _model.Fiber.GetFiberLengths();     // обновили view

        }


        /// <summary>
        /// Изменить длину фибры. Обновляется содержание модели
        /// /// </summary>
        /// <param name="indexLength">номер длины из выпадающего списка</param>
        private void ChangeFiberLength(int indexLength)
        {
            _model.Fiber.SetIndexFiberLength(indexLength);
            _model.FiberCoef.SetLen_f(_model.Fiber.Length);
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
    }
}

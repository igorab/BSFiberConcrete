﻿using BSFiberConcrete.DeformationDiagram.UserControls;
using MathNet.Numerics.Integration;
using Microsoft.SqlServer.Server;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;

namespace BSFiberConcrete.DeformationDiagram
{
    public class CalcDeformDiagram
    {
        public string typeMaterial;
        public string typeDiagram;

        #region Характеристики Материала для построения диаграммы деформации на растяжение (фибробетон)
        /// <summary>
        /// норматиыное сопротивление на Растяжение кг/см2
        /// </summary>
        public double Rt_n;
        /// <summary>
        /// сопротивление на растяжение кг/см2
        /// </summary>
        public double Rt1;
        /// <summary>
        /// Нормативное остаточное сопротивление осевому растяжению кг/см2
        /// </summary>
        public double Rt2_n;
        /// <summary>
        /// Нормативное остаточное сопротивление осевому растяжению кг/см2
        /// </summary>
        public double Rt3_n;
        /// <summary>
        /// Начальный модуль упругости кг/см2
        /// </summary>
        public double Et;
        // Относительные деформации
        public double et0;
        public double et1;
        public double et2;
        public double et3;
        #endregion

        #region Характеристик для построения диаграммы деформации на сжатие (для бетона и фибробетона)
        /// <summary>
        /// норматиыное сопротивление на сжатие кг/см2
        /// </summary>
        public double R_n;
        /// <summary>
        /// сопротивление на сжатие кг/см2
        /// </summary>
        public double R1;
        /// <summary>
        /// Начальный модуль упругости материала на сжатие кг/см2 
        /// </summary>
        public double E;
        // Относительные деформации
        public double e0;
        public double e1;
        public double e2;
        # endregion 



        /// <summary>
        /// Поле содержит в себе форму, которая должна отображаться на форме в зависимости от typeMaterial
        /// </summary>
        private UserControl _deformationsView;

        private double[] _valuesRelativeDeformation;

        /// <summary>
        /// Массив, определяющий характерные значения относительных деформаций в зависимости от typeMaterial и typeDiagram 
        /// </summary>
        public double[] deformsArray;



        /// <summary>
        /// Производится заполнение полей класса используя DataForDeformDiagram
        /// </summary>
        /// <exception cref="Exception"></exception>
        public CalcDeformDiagram(string[] typesDiagram, double[] resists, double[] elasticity)
        {
            
            typeMaterial = typesDiagram[0];
            typeDiagram = typesDiagram[1];

            R_n = resists[0];
            Rt_n = resists[1];
            Rt2_n = resists[2];
            Rt3_n = resists[3];

            E = elasticity[0];
            Et = elasticity[1];

            if (typeMaterial == BSHelper.FiberConcrete)
            {
                //FiberBetonDeformationView a = new FiberBetonDeformationView();
                //e0, e1, e2,      et0,  et1, et2,  et3 
                _valuesRelativeDeformation = new double[] { 0.003, 0, 0.0035, 0, 0, 0.004, 0.015 };
                SetValuesRelativeDeformation();
                _deformationsView = new FiberBetonDeformationView(_valuesRelativeDeformation);

                //UserControl//
            }
            else if (typeMaterial == BSHelper.Rebar)
            {
                _valuesRelativeDeformation = new double[] { 0, 0, 0.025};
                SetValuesRelativeDeformation();
                _deformationsView = new RebarDeformationView(_valuesRelativeDeformation);

            }

        }


        /// <summary>
        /// Установить значение относительных деформаций
        /// </summary>
        /// <param name="deformations"></param>
        public void SetValuesRelativeDeformation()
        {

            if (typeMaterial == BSHelper.FiberConcrete)
            {
                //e0, e1, e2,      et0,  et1, et2,  et3 
                e0 = _valuesRelativeDeformation[0];
                //e1 = _valuesRelativeDeformation[1];
                e2 = _valuesRelativeDeformation[2];
                //et0 = _valuesRelativeDeformation[3];
                //et1 = _valuesRelativeDeformation[4];
                et2 = _valuesRelativeDeformation[5];
                et3 = _valuesRelativeDeformation[6];
                FillDiagramsData();

                _valuesRelativeDeformation[1] = e1;
                _valuesRelativeDeformation[3] = et0;
                _valuesRelativeDeformation[4] = et1;
            }
            else if (typeMaterial == BSHelper.Rebar)
            {
                e2 = _valuesRelativeDeformation[2];
                et2 = _valuesRelativeDeformation[2];
                FillDiagramsData();
                 _valuesRelativeDeformation[0] = e0;
                _valuesRelativeDeformation[1] = e1;
            }
        }


        /// <summary>
        /// Заполнить данные для построения диаграммы
        /// </summary>
        private void FillDiagramsData()
        {
            if (typeMaterial == BSHelper.Concrete || typeMaterial == BSHelper.FiberConcrete)
            {
                if (typeDiagram == BSHelper.TwoLineDiagram)
                { R1 = R_n; }
                else if (typeDiagram == BSHelper.ThreeLineDiagram)
                { R1 = R_n * 0.6; }
                e1 = R1 / E;

                et0 = Rt_n / Et;
                et1 = et0 + 0.0001;
                //efbt2 = 0.004m;
                //efbt3 = Math.Abs(0.02m - 0.0125m * (Rfbt3_n / Rfbt2_n));
            }
            else if (typeMaterial == BSHelper.Rebar)
            {
                if (typeDiagram == BSHelper.TwoLineDiagram)
                {
                    R1 = R_n;
                    Rt1 = Rt_n;
                }
                else if (typeDiagram == BSHelper.ThreeLineDiagram)
                {
                    R1 = R_n * 0.9;
                    Rt1 = Rt_n * 0.9;
                }
                e1 = R1 / E;
                et1 = Rt1 / Et;

                e0 = e1 + 0.002;
                et0 = et1 + 0.002;
                //e0 = 2 * (e0 - e1) + e1;
                //e0 = R_n * 1.5 / E;
            }

            if (typeMaterial == BSHelper.Concrete)
            {
                if (typeDiagram == BSHelper.TwoLineDiagram)
                { deformsArray = new double[] { 0, e1, e2 }; }
                else if (typeDiagram == BSHelper.ThreeLineDiagram)
                { deformsArray = new double[] { 0, e1, e0, e2 }; }
            }
            else if (typeMaterial == BSHelper.FiberConcrete)
            {
                if (typeDiagram == BSHelper.TwoLineDiagram)
                { deformsArray = new double[] { -et3, -et2, -et1, -et0, 0, e1, e2 }; }
                else if (typeDiagram == BSHelper.ThreeLineDiagram)
                { deformsArray = new double[] { -et3, -et2, -et1, -et0, 0, e1, e0, e2 }; }
            }
            else if (typeMaterial == BSHelper.Rebar)
            {
                if (typeDiagram == BSHelper.TwoLineDiagram)
                { deformsArray = new double[] { -e2, -e1, 0, et1, et2 }; }
                else if (typeDiagram == BSHelper.ThreeLineDiagram)
                { deformsArray = new double[] { -e2, -e0, -e1, 0, et1, et0, et2 }; }
            }
        }


    /// <summary>
    /// Определяется массив относительных деформаций и напряжений
    /// </summary>
    /// <returns></returns>
    public double[,] Calculate()
        {
            double[,] result = new double[1, 1];
            if (typeMaterial == BSHelper.Concrete)
            {
                if (typeDiagram == BSHelper.TwoLineDiagram)
                { result = new double[2, 3] { {0, e1, e2 }, { 0, R_n, R_n} }; }
                else if (typeDiagram == BSHelper.ThreeLineDiagram)
                { result = new double[2, 4]{ {0, e1, e0, e2 }, { 0, R1, R_n, R_n} }; }
            }
            else if (typeMaterial == BSHelper.FiberConcrete)
            {
                //efbt2 = 0.004m;
                //efbt3 = Math.Abs(0.02m - 0.0125m * (Rfbt3_n / Rfbt2_n));
                if (typeDiagram == BSHelper.TwoLineDiagram)
                {
                    result = new double[2, 7]
                    { {-et3, -et2, -et1, -et0, 0, e1, e2 }, { -Rt3_n, -Rt2_n, -Rt_n, -Rt_n, 0, R_n, R_n} };
                }
                else if (typeDiagram == BSHelper.ThreeLineDiagram)
                {
                    result = new double[2, 8]
                    { {-et3, -et2, -et1, -et0, 0, e1, e0, e2 }, { -Rt3_n, -Rt2_n, -Rt_n, -Rt_n, 0, R1, R_n, R_n} };
                }
            }    
            return result;
        }


        /// <summary>
        /// определяется напряжение для относительной деформации epsilon
        /// </summary>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        public double getResists(double epsilon)
        {
            double res = 0;

            if (typeMaterial == BSHelper.Rebar)
            {
                // участок диаграммы на положительных epsilon (растяжение)
                if (epsilon > 0)
                {
                    if (epsilon > et2)
                        return res;

                    if (typeDiagram == BSHelper.ThreeLineDiagram)
                    {
                        if (0 < epsilon && epsilon <= et1)
                            res = Et * epsilon;
                        else if (et1 < epsilon && epsilon <= et0)
                            res = (Rt1 + (Rt_n * 1.1 - Rt1) * (epsilon - et1) / (et0 - et1));
                        else if (et0 < epsilon && epsilon <= et2)
                            res = Rt_n *1.1;
                    }
                    else if (typeDiagram == BSHelper.TwoLineDiagram)
                    {
                        if (0 < epsilon && epsilon <= et1)
                            res = Et * epsilon;
                        else if (et1 < epsilon && epsilon <= et2)
                            res = Rt_n;
                    }
                }
                else if (epsilon < 0)
                {
                    if (epsilon < -e2)
                        return res;

                    if (typeDiagram == BSHelper.ThreeLineDiagram)
                    {
                        if (epsilon < 0  && -e1 <= epsilon)
                            res = E * epsilon;
                        else if (epsilon < -e1 && -e0 <= epsilon)
                            res = -(R1 + (R_n * 1.1 - R1) * (epsilon - e1) / (-e0 - e1));
                        else if (epsilon < -e0 && -e2 <= epsilon)
                            res = -R_n * 1.1;
                    }
                    else if (typeDiagram == BSHelper.TwoLineDiagram)
                    {
                        if (epsilon < 0 && -e1 <= epsilon)
                            res = E * epsilon;
                        else if ( epsilon < -et1 && -et2 <= epsilon)
                            res = -R_n;
                    }
                }
            }
            else
            {
                if (epsilon > 0)
                {
                    // участок диаграммы на положительных epsilon (СЖАТИЕ)
                    if (epsilon > e2)
                    { return res; }
                    if (typeDiagram == BSHelper.ThreeLineDiagram)
                    {
                        if (0 < epsilon && epsilon <= e1)
                        { res = E * epsilon; }
                        else if (e1 < epsilon && epsilon <= e0)
                        {
                            //res = ((1 - 0.6) * (epsilon - eb1) / (eb0 - eb1) + 0.6) * Rb_n;
                            res = (R1 + (R_n - R1) * (epsilon - e1) / (e0 - e1));
                        }
                        else if (e0 < epsilon && epsilon <= e2)
                        { res = R_n; }
                    }
                    else if (typeDiagram == BSHelper.TwoLineDiagram)
                    {
                        if (0 < epsilon && epsilon <= e1)
                        { res = E * epsilon; }
                        else if (e1 < epsilon && epsilon <= e2)
                        { res = R_n; }
                    }
                }
                else if (epsilon < 0)
                {
                    // участок диаграммы на отрицательных epsilon (РАСТЯЖЕНИЕ)
                    if (epsilon < -et3)
                    { return res; }

                    if (epsilon < 0 && -et0 <= epsilon)
                    { res = Et * epsilon; }
                    if (epsilon < -et0 && -et1 <= epsilon)
                    { res = -Rt_n; }
                    else if (epsilon < -et1 && -et2 <= epsilon)
                    {
                        res = -Rt_n * (1 + (1 - Rt2_n / Rt_n) * (epsilon - et1) / (et2 - et1));
                        //res = Rfbt2_n * (1 - (1 - Rfbt_n / Rfbt2_n) * (epsilon + efbt2) / (efbt1 + efbt2));    
                    }
                    else if (epsilon < -et2 && -et3 <= epsilon)
                    {
                        res = -Rt2_n * (1 + (1 - Rt3_n / Rt2_n) * (epsilon + et2) / (et3 - et2));
                        //res = (Rfbt3_n + (Rfbt3_n - Rfbt2_n) * (epsilon + efbt3) / (efbt2 - efbt3));  
                    }

                }
            }
            return res;
        }


        public void UpDateUserControll(TableLayoutPanel table)
        {
            table.Controls.Add(_deformationsView, 0, 0);
            _deformationsView.Dock = System.Windows.Forms.DockStyle.Fill;
        }

        public System.Windows.Forms.DataVisualization.Charting.Series CreateSeries()
        {


            //System.Windows.Forms.DataVisualization.Charting.DataPointCustomProperties.
            System.Windows.Forms.DataVisualization.Charting.Series series = new System.Windows.Forms.DataVisualization.Charting.Series("Series1");

            //series.Add("Series1")

            series.BorderWidth = 4;
            series.Color = System.Drawing.Color.Red;
            series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            for (int i = 0; i < deformsArray.Length; i++)
            {
                double tmpEpsilon = deformsArray[i];
                double tmpResits = getResists(tmpEpsilon);
                series.Points.AddXY(tmpEpsilon, tmpResits);
                if (tmpResits == 0)
                { continue; }
                string pointLableX = Math.Round(tmpEpsilon, 5).ToString();
                string pointLableY = Math.Round(tmpResits, 2).ToString();
                series.Points[i].Label = $"ε={pointLableX}, σ={pointLableY}";
            }
            series.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F,
                        ((System.Drawing.FontStyle)(System.Drawing.FontStyle.Bold)), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            series.ToolTip = "ε = #VALX, σ = #VALY";

            return series;

        }



        /// <summary>
        /// Создать объект chart
        /// </summary>
        /// <param name="pathToSave"></param>
        /// <returns></returns>
        public Chart CreteChart()
        {
            System.Windows.Forms.DataVisualization.Charting.Chart chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Title title = new System.Windows.Forms.DataVisualization.Charting.Title();

            string numChart = "1";

            string CAName = $"ChartArea{numChart}";
            string cName = $"chart{numChart}";
            string sName = $"Series{numChart}";
            string tName = $"Title{numChart}";


            chartArea.Name = CAName;
            chart.Name = cName;
            chart.Text = cName;
            title.Name = tName;

            string name2Save = "DeformDiagram";

            chart.ChartAreas.Add(chartArea);
            chart.Dock = System.Windows.Forms.DockStyle.None;
            chart.Location = new System.Drawing.Point(3, 3);
            chart.Size = new System.Drawing.Size(700, 400);
            chart.TabIndex = 0;
            chart.Titles.Add(typeMaterial + ". Диаграмма " + typeDiagram + ".");
            chart.Series.Add(sName);
            chart.Series[sName].BorderWidth = 4;
            chart.Series[sName].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            for (int i = 0; i < deformsArray.Length; i++)
            {
                double tmpEpsilon = deformsArray[i];
                double tmpResits = getResists(tmpEpsilon);
                chart.Series[sName].Points.AddXY(tmpEpsilon, tmpResits);
                if (tmpResits == 0)
                { continue; }
                string pointLableX = Math.Round(tmpEpsilon, 5).ToString();
                string pointLableY = Math.Round(tmpResits, 2).ToString();
                chart.Series[sName].Points[i].Label = $"ε={pointLableX}, σ={pointLableY}";
            }


            chart.Series[sName].Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F,
                ((System.Drawing.FontStyle)(System.Drawing.FontStyle.Bold)), System.Drawing.GraphicsUnit.Point, ((byte)(204)));

            chart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            chart.ChartAreas[0].AxisX.Crossing = 0;
            chart.ChartAreas[0].AxisY.Crossing = 0;

            Font axisFont = new System.Drawing.Font("Microsoft Sans Serif", 12F,
    ((System.Drawing.FontStyle)(System.Drawing.FontStyle.Bold)), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            chart.ChartAreas[0].AxisX.Title = "ε";
            chart.ChartAreas[0].AxisX.TitleFont = axisFont;
            chart.ChartAreas[0].AxisY.Title = "σ, кг/см2";
            chart.ChartAreas[0].AxisY.TitleFont = axisFont;
            chart.Series[sName].Color = System.Drawing.Color.Red;
            return chart;
        }



        /// <summary>
        /// Сохранить диаграмму
        /// </summary>
        public static string SaveChart(System.Windows.Forms.DataVisualization.Charting.Chart chart, string pictureName = null)
        {
            if (pictureName == null)
            { pictureName = "DeformDiagram" + ".png"; }
            else
            { pictureName = pictureName + ".png"; }
            chart.SaveImage(pictureName, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Png);
            return Directory.GetCurrentDirectory() + "\\" + pictureName;
        }



    }


    public class DataForDeformDiagram
    {
        public string[] typesDiagram;
        public double[] resists;
        public double[] deforms;
        public double[] elasticity;
    }
}

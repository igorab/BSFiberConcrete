﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml.Linq;
using System.Xml.Schema;

namespace BSBeamCalculator
{
    /// <summary>
    /// Класс нужен для сбора информации с формы
    /// и дальнейшей передачи информации в вычислительный класс CalculateBeamDiagram
    /// </summary>
    public class BeamDiagram
    {
        /// <summary>
        /// Длинна балки
        /// </summary>
        public double l;
        /// <summary>
        /// Тип защемления балки
        /// </summary>
        public string supportType;
        /// <summary>
        /// тип нагрузки на балку
        /// </summary>
        public string loadType;
        /// <summary>
        /// Значение силы на балку
        /// </summary>
        public double f;
        /// <summary>
        /// координата приложение силы
        /// </summary>
        public double x1;


        /// <summary>
        /// DiagramResult - класс для данных необходимых для построения грфика
        /// </summary>
        public DiagramResult result;

        //public CalculateBeamDiagram beamDiagram;
        public Chart diagramQ;
        public Chart diagramM;
        public Chart diagramU;



        /// <summary>
        /// счетчик картинок
        /// </summary>
        private int _numChart = 1;

        public BeamDiagram()
        {
        }

        public List<Chart> RunCalculation()
        {
            List<Chart> forceChart = new List<Chart>();


            SimpleBeamDiagramCase simpleDiagram = new SimpleBeamDiagramCase(supportType, loadType);
            double[][] values_xQ_xM = simpleDiagram.CalculateValuesForDiagram(l, x1, 0, f);
            result = new DiagramResult(values_xQ_xM);

            string[] names1 = { "Сила", "см", "кг", "BeamDiagramQ" };
            diagramQ = CreteChart(result.pointQ[0].ToList(), result.pointQ[1].ToList(), names1, System.Drawing.Color.Blue);
            string[] names2 = { "Момент", "см", "кг*см", "BeamDiagramM" };
            diagramM = CreteChart(result.pointM[0].ToList(), result.pointM[1].ToList(), names2, System.Drawing.Color.Red);
            forceChart.Add(diagramQ);
            forceChart.Add(diagramM);

            return forceChart;

        }


        /// <summary>
        /// Получить значение момента в зависимости от координаты
        /// </summary>
        /// <param name="x"> значение длины</param>
        /// <returns></returns>
        public double GetM(DiagramResult res, double x)
        {

            if (x < 0 && x > l) 
            { return 0; }

            if (x == 0)
            { return res.pointM[1][1]; }

            if (x == l)
            { return res.pointM[1][res.pointM[1].Length-2]; }

            List<double> X = res.pointM[0].ToList();
            List<double> M = res.pointM[1].ToList();

            if (X.Contains(x))
            { return M[X.IndexOf(x)]; }

            int index = 0;
            for (int i = 0; X.Count > i; i++)
            {
                if ((x - X[i]) < 0)
                {
                    index = i;
                    break;
                }
            }

            double resultM;
            double ro = (x - X[index - 1]) / (X[index] - X[index - 1]);
            resultM = M[index - 1] + (M[index] - M[index - 1]) * ro;

            return resultM;

        }


        public double CalculateDeflectionAtPoint(List<double> M, List<double> X, List<double> D, int index)
        {
            // установка единичной нагрузки
            string forceType = "Concentrated";
            double forceValue = 1;
            SimpleBeamDiagramCase tmpSimpleBeamDiagram = new SimpleBeamDiagramCase(supportType, forceType);
            double[][] values_xQ_xM = tmpSimpleBeamDiagram.CalculateValuesForDiagram(l, X[index], 0, forceValue);
            DiagramResult res = new DiagramResult(values_xQ_xM);

            double sectionLength = X[index + 1] - X[index - 1];
            double u = 0;

            for (int i = 1; X.Count > i; i = i + 2)
            { 
                double a = M[i-1];
                double b = M[i];
                double c = M[i+1];
                if (b == 0)
                { 
                    // если значение момента в точке равно 0, исключаем точку из расчета, тк получается некорректное значение жесткости
                    continue;
                }
                double a1 = GetM(res, X[i - 1]);
                double b1 = GetM(res, X[i]);
                double c1 = GetM(res, X[i + 1]);

                int num = (i - 1) / 2;
                double tmpU = sectionLength / (6 * D[num]) * (a * a1 + 4 * b * b1 + c * c1);
                u = u + tmpU;
            }
            return -u;
        }



        /// <summary>
        /// Разделить балку на участки
        /// </summary>
        /// <param name="X"> значения X в точках</param>
        /// <param name="valueMomentInX">Значения M в точках</param>
        /// <param name="valuesMomentOnSection">Значения M на середине рассматриваемого участка</param>
        public void BreakBeamIntoSections(List<double> X, List<double> valueMomentInX, List<double> valuesMomentOnSection)
        {
            // Кол- во рассматриваемых участков
            int n = 20;
            // всего точек 
            int m = n + 1 + n;
            // шаг между точками
            double delta = this.l / (2 * n);

            for (int i = 0; m > i; i++)
            {
                double tmpX = delta * i;
                double tmpM = this.GetM(this.result, tmpX);

                X.Add(tmpX);
                valueMomentInX.Add(tmpM);

                if (i > 0 && i % 2 != 0)
                { valuesMomentOnSection.Add(tmpM); }
            }
        }


        /// <summary>
        /// Построить график прогибов по расчетным значениям
        /// </summary>
        public double  CalculateDeflectionDiagram(List<double> X, List<double> valueMomentInX, List<double> valuesStiffnessOnSection)
        {
            double deflexionMax = 0;
            // график прогибов по расчетным значениям
            List<double> U = new List<double>();
            List<double> XForChart = new List<double>();
            for (int i = 1; X.Count > i; i = i + 2)
            {
                double u = this.CalculateDeflectionAtPoint(valueMomentInX, X, valuesStiffnessOnSection, i);
                U.Add(u * 10); // перевод из см в мм 
                XForChart.Add(X[i]);
                if (deflexionMax > u * 10) // значение прогиба с минусом
                { deflexionMax = u * 10; }
            }
            string[] names = { "Прогиб", "см", "мм", "BeamDiagramU" };
            //this.CreteChart(XForChart, U, names);
            diagramU = this.CreteChart(XForChart, U, names, System.Drawing.Color.Green);
            return deflexionMax;
        }

        /// <summary>
        /// Построить график прогиба по формуле
        /// </summary>
        /// <param name="X"></param>
        /// <param name="valuesStiffnessOnSection"></param>
        /// <param name=""></param>
        public void CalculateDeflectionDiagramByFormula(List<double> X, List<double> valuesStiffnessOnSection)
        {
            // график прогибов по формулам

            SimpleBeamDiagramCase tmpSimpleBeamDiagram = new SimpleBeamDiagramCase(supportType, loadType);

            if (tmpSimpleBeamDiagram.IsCalculateBeamDeflection)
            {
                double d = 0;
                foreach (double Stiffness in valuesStiffnessOnSection)
                {
                    if (double.IsNaN(Stiffness)) { continue; }
                    d = (d + Stiffness) / 2;
                }

                if (d == 0)
                { return; }

                List<double> simpleU = new List<double>();
                List<double> XForChart = new List<double>();
                for (int i = 1; X.Count > i; i = i + 2)
                {
                    XForChart.Add(X[i]);
                    double tmpU = tmpSimpleBeamDiagram.CalculateBeamDeflection(X[i], d) * 10;
                    simpleU.Add(tmpU);
                }
                CreteChart(XForChart, simpleU, new string[] { "Прогиб по формуле", "cм", "мм", "SimpleBeamDiagramU" }, System.Drawing.Color.Green);
            }
        }


        /// <summary>
        /// Создать объект chart
        /// </summary>
        /// <param name="valueX"></param>
        /// <param name="valueY"></param>
        /// <param name="names"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public Chart CreteChart(List<double> valueX, List<double> valueY, string[] names, Color color)
        {
            System.Windows.Forms.DataVisualization.Charting.Chart chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Title title = new System.Windows.Forms.DataVisualization.Charting.Title();

            string numChart = _numChart.ToString();

            string CAName = $"ChartArea{numChart}";
            string cName = $"chart{numChart}";
            string tName = $"Title{numChart}";
            string sName = $"Series{numChart}";

            //names;
            string textName = names[0];
            string titleX = names[1];
            string titleY = names[2];
            string name2Save = names[3];

            chartArea.Name = CAName;
            chart.Name = cName;
            chart.Text = cName;

            title.Name = tName;
            title.Text = textName;

            chart.ChartAreas.Add(chartArea);
            chart.Dock = System.Windows.Forms.DockStyle.Fill;
            chart.Location = new System.Drawing.Point(3, 3);
            chart.Size = new System.Drawing.Size(664, 217);
            chart.TabIndex = 0;
            chart.Titles.Add(title);
            chart.Series.Add(sName);
            chart.Series[sName].BorderWidth = 4;
            chart.ChartAreas[0].AxisX.Minimum = 0;
            chart.ChartAreas[0].AxisX.Maximum = l;
            chart.Series[sName].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            for (int i = 0; i < valueX.Count; i++)
            { chart.Series[sName].Points.AddXY(valueX[i], valueY[i]); }

            Font axisFont = new System.Drawing.Font("Microsoft Sans Serif", 8F,
                ((System.Drawing.FontStyle)(System.Drawing.FontStyle.Bold)), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            chart.ChartAreas[0].AxisX.Title = titleX;
            chart.ChartAreas[0].AxisX.TitleFont = axisFont;
            chart.ChartAreas[0].AxisY.Title = titleY;
            chart.ChartAreas[0].AxisY.TitleFont = axisFont;
            chart.Series[sName].Color = color;

            return chart;
        }


        /// <summary>
        /// Сохранить диаграмму
        /// </summary>
        public string SaveChart(System.Windows.Forms.DataVisualization.Charting.Chart chart, string pictureName = null)
        {
            if (pictureName == null)
            { pictureName = "BeamDiagram" + ".png"; }
            else
            { pictureName = pictureName + ".png"; }
            chart.SaveImage(pictureName, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Png);
            return Directory.GetCurrentDirectory() + "\\" + pictureName;
        }
    }


    public class DiagramResult
    {
        public double[][] pointQ;
        public double[][] pointM;
        public double maxM;
        public double minM;
        public double maxAbsQ;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="values_xQ_xM">4 мерный вложенный массив
        /// 0 - значения x для Q, 1 - значения Q; 2 - значения x для M, 3 - значения M  
        /// </param>
        public DiagramResult(double[][] values_xQ_xM)
        {
            //pointQ = new double[,] { values_xQ_xM[0], values_xQ_xM[1] }
            pointQ = new double[][]{
                values_xQ_xM[0],
                values_xQ_xM[1]
            };
            pointM = new double[][]{
                values_xQ_xM[2],
                values_xQ_xM[3]
            };
            //maxPointQ = FindeMaxAbsValue(new double[][] { values_xQ_xM[0], values_xQ_xM[1] });
            //maxPointM= FindeMaxAbsValue(new double[][] { values_xQ_xM[2], values_xQ_xM[3] });
            List<double> maxAbsPointQ = FindeMaxAbsValue(new double[][] { values_xQ_xM[0], values_xQ_xM[1] });

            maxAbsQ = maxAbsPointQ[1];
            maxM = values_xQ_xM[3].Max();
            minM = values_xQ_xM[3].Min();
        }

        /// <summary>
        /// Функция определяет максимальное (по модулю) значение во втором массиве
        /// и возвращает пару x_value[i][0] x_value[i][1], i - индекс максимального значения
        /// </summary>
        /// <param name="x_value"></param>
        /// <returns></returns>
        public static List<double> FindeMaxAbsValue(double[][] x_value)
        {
            List<double> maxAbsPoint = new List<double>();
            int indMaxAbs_Value;
            double max_value = x_value[1].Max();
            double min_value = x_value[1].Min();
            if (max_value >= Math.Abs(min_value))
            { indMaxAbs_Value = x_value[1].ToList().IndexOf(max_value); }
            else
            { indMaxAbs_Value = x_value[1].ToList().IndexOf(min_value); }
            maxAbsPoint = new List<double>() { x_value[0][indMaxAbs_Value], x_value[1][indMaxAbs_Value] };

            return maxAbsPoint;
        }

    }
}

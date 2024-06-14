using MathNet.Numerics.Providers.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


//using namespace BSFiberConcrete;

namespace BSFiberConcrete.DeformationDiagram
{
    public partial class DeformDiagram : Form
    {
        public DeformDiagram()
        {
            InitializeComponent();

            decimal[,] result = DataForDeformDiagram.Calculate();
            // построение диаграммы
            chartDeformDiagram.Series.Add("Series1");
            chartDeformDiagram.Series["Series1"].BorderWidth = 4;
            chartDeformDiagram.Series["Series1"].Color = System.Drawing.Color.Red;
            chartDeformDiagram.Series["Series1"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chartDeformDiagram.Titles["Title1"].Text = DataForDeformDiagram.typeMaterial + ". Диаграмма " + DataForDeformDiagram.typeDiagram + ".";
            for (int i = 0; i < result.Length/2; i++)
            { 
                chartDeformDiagram.Series["Series1"].Points.AddXY(result[0,i], result[1,i]);
                if (result[0, i] == 0m)
                { continue; }
                string pointLableX = decimal.Round(result[0, i], 5).ToString();
                string pointLableY = decimal.Round(result[1, i], 2).ToString();
                chartDeformDiagram.Series["Series1"].Points[i].Label = $"X={pointLableX}, Y= {pointLableY}";
                //chartDeformDiagram.Series["Series1"].Points[i].Label.
            }
            //


            chartDeformDiagram.Series["Series1"].Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, 
                ((System.Drawing.FontStyle)(System.Drawing.FontStyle.Bold)), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            //chartDeformDiagram.Series["Series1"].Points[0].Lab
            //chartDeformDiagram.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            //chartDeformDiagram.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            chartDeformDiagram.ChartAreas[0].AxisX.Crossing = 0;
            chartDeformDiagram.ChartAreas[0].AxisY.Crossing = 0;
            //chartDeformDiagram.ChartAreas[0].AxisX.Enabled = AxisEnabled.False;

            chartDeformDiagram.Series["Series1"].ToolTip = "X = #VALX, Y = #VALY";

            //double maxValueX = (double)result[0, result.Length/2-1];
            //chartDeformDiagram.ChartAreas[0].AxisX.Minimum = -maxValueX / 10;
            //chartDeformDiagram.ChartAreas[0].AxisX.Maximum = maxValueX + maxValueX / 10;
            //double maxValueY = (double)result[1, result.Length / 2 - 1];
            //chartDeformDiagram.ChartAreas[0].AxisY.Minimum = -maxValueY / 10;
            //chartDeformDiagram.ChartAreas[0].AxisY.Maximum = maxValueY + maxValueY / 10;
            //chartDeformDiagram.Series.Add("Ordinate");
            //chartDeformDiagram.Series["Ordinate"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            //chartDeformDiagram.Series["Ordinate"].Color = System.Drawing.Color.Blue;
            //chartDeformDiagram.Series["Ordinate"].BorderWidth = 2;
            //chartDeformDiagram.Series["Ordinate"].Points.AddXY(0, -maxValueY / 10);
            //chartDeformDiagram.Series["Ordinate"].Points.AddXY(0, maxValueY + maxValueY / 10);
            //chartDeformDiagram.Series.Add("Abscissa");
            //chartDeformDiagram.Series["Abscissa"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            //chartDeformDiagram.Series["Abscissa"].Color = System.Drawing.Color.Blue;
            //chartDeformDiagram.Series["Abscissa"].BorderWidth = 2;
            //chartDeformDiagram.Series["Abscissa"].Points.AddXY(-maxValueX / 10, 0);
            //chartDeformDiagram.Series["Abscissa"].Points.AddXY(maxValueX + maxValueX / 10, 0);

        }
    }
}

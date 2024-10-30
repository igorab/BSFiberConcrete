using BSFiberConcrete.BSRFib.FiberCalculator;
using MathNet.Numerics;
using MathNet.Numerics.Providers.LinearAlgebra;
using ScottPlot.Rendering.RenderActions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Metadata.Edm;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;



namespace BSFiberConcrete.DeformationDiagram
{
    public partial class DeformDiagram : Form
    {

        private CalcDeformDiagram _viewModel; 

        public DeformDiagram(CalcDeformDiagram calculateDiagram)
        {
            _viewModel = calculateDiagram;


            InitializeComponent();

            _viewModel.UpDateUserControll(this.tableLayoutPanel2);



                        


            DrowDiagram();





                                                                                                                                                                                                                                                                                                
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _viewModel.SetValuesRelativeDeformation();
            DrowDiagram();
        }

        private void DrowDiagram()
        {

                                                
            chartDeformDiagram.Series.Clear();

            chartDeformDiagram.Series.Add("Series1");
            chartDeformDiagram.Series["Series1"].BorderWidth = 4;
            chartDeformDiagram.Series["Series1"].Color = System.Drawing.Color.Red;
            chartDeformDiagram.Series["Series1"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chartDeformDiagram.Titles["Title1"].Text = _viewModel.typeMaterial + ". Диаграмма " + _viewModel.typeDiagram + ".";

            for (int i = 0; i < _viewModel.deformsArray.Length; i++)
            {
                double tmpEpsilon = _viewModel.deformsArray[i];
                double tmpResits = _viewModel.getResists(tmpEpsilon);
                chartDeformDiagram.Series["Series1"].Points.AddXY(tmpEpsilon, tmpResits);
                if (tmpResits == 0)
                { continue; }
                string pointLableX = Math.Round(tmpEpsilon, 5).ToString();
                string pointLableY = Math.Round(tmpResits, 2).ToString();
                chartDeformDiagram.Series["Series1"].Points[i].Label = $"ε={pointLableX}, σ={pointLableY}";
            }
            chartDeformDiagram.Series["Series1"].Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F,
                ((System.Drawing.FontStyle)(System.Drawing.FontStyle.Bold)), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            chartDeformDiagram.Series["Series1"].ToolTip = "ε = #VALX, σ = #VALY";

                                    chartDeformDiagram.ChartAreas[0].AxisX.Crossing = 0;
            chartDeformDiagram.ChartAreas[0].AxisY.Crossing = 0;
            
            Font axisFont = new System.Drawing.Font("Microsoft Sans Serif", 12F,
    ((System.Drawing.FontStyle)(System.Drawing.FontStyle.Bold)), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            chartDeformDiagram.ChartAreas[0].AxisX.Title = "ε";
            chartDeformDiagram.ChartAreas[0].AxisX.TitleFont = axisFont;
            chartDeformDiagram.ChartAreas[0].AxisY.Title = "σ, кг/см2";
            chartDeformDiagram.ChartAreas[0].AxisY.TitleFont = axisFont;


        }
    }
}

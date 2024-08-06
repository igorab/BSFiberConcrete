using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BSBeamCalculator
{
    public partial class BeamCalculatorControl : UserControl
    {

        /// <summary>
        /// список с именами картинок с эпюрам
        /// </summary>
        private List<string> _pathToBeamDiagrams;
        /// <summary>
        /// TextBox с главной формы. Передается для оперативного изменения значения на главной форме.
        /// </summary>
        private TextBox _beamLength;
        /// <summary>
        /// DataGridView c главной формы. Передается для удобного и оперативного изменения данных на главной форме
        /// </summary>
        private DataGridView _beamEfforts;

        /// <summary>
        /// Результаты построения диамгрммы
        /// </summary>
        public Dictionary<string, double> effortsModel;


        public BeamCalculatorControl()
        {
            InitializeComponent();

            ControllerBeamDiagram.load = "Concentrated";
            ControllerBeamDiagram.support = "Fixed-Fixed";
        }

        /// <summary>
        ///  Для оперативного обновления данных на главной форме в качестве параметров передаются :
        /// </summary>
        /// <param name="len"> длина балки</param>
        /// <param name="effortsData"> таблица с нагрузками</param>
        /// /// <param name="path2Diagram"> путь к картинке с диагрммой</param>
        public BeamCalculatorControl(TextBox len, DataGridView effortsData, List<string> path2Diagram)
        {
            _beamLength = len;
            _beamEfforts = effortsData;
            _pathToBeamDiagrams = path2Diagram;

            InitializeComponent();
            ControllerBeamDiagram.load = "Concentrated";
            ControllerBeamDiagram.support = "Fixed-Fixed";

            double.TryParse(_beamLength.Text, out double tmpBeamLen );
            if (tmpBeamLen != 0)
            { 
                numericUpDown1.Value = (decimal)tmpBeamLen;
                numericUpDown3.Value = (decimal) tmpBeamLen / 2;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                chart1.Series.Clear();
                chart2.Series.Clear();
                label9.Text = "0";
                label12.Text = "0";
                label18.Text = "0";
                _pathToBeamDiagrams.Clear();

                // собираем данные с формы
                double lengthBeam = (double)numericUpDown1.Value;
                double force = (double)numericUpDown2.Value;
                double startPointForce = (double)numericUpDown3.Value;
                //double supportBeam = 
                //double loadBeam = 

                //ControllerBeamDiagram.support =
                //ControllerBeamDiagram.load =
                ControllerBeamDiagram.l = lengthBeam;
                ControllerBeamDiagram.f = force;
                ControllerBeamDiagram.x1 = startPointForce;
                ControllerBeamDiagram.x2 = 0;

                ControllerBeamDiagram.resultEfforts = effortsModel;

                // запуск расчета
                ControllerBeamDiagram.RunCalculation();

                // Вывод результатов расчета
                DiagramResult result = ControllerBeamDiagram.result;
                chart1.Series.Add("Series1");
                chart1.Series["Series1"].BorderWidth = 4;
                chart1.ChartAreas[0].AxisX.Minimum = 0;
                chart1.ChartAreas[0].AxisX.Maximum = lengthBeam;
                chart1.Series["Series1"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                for (int i = 0; i < result.pointQ[0].Length; i++)
                { chart1.Series["Series1"].Points.AddXY(result.pointQ[0][i], result.pointQ[1][i]); }

                chart2.Series.Add("Series1");
                chart2.Series["Series1"].BorderWidth = 4;
                chart2.Series["Series1"].Color = System.Drawing.Color.Red;
                chart2.ChartAreas[0].AxisX.Minimum = 0;
                chart2.ChartAreas[0].AxisX.Maximum = lengthBeam;
                chart2.Series["Series1"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                for (int i = 0; i < result.pointM[0].Length; i++)
                { chart2.Series["Series1"].Points.AddXY(result.pointM[0][i], result.pointM[1][i]); }

                Font axisFont = new System.Drawing.Font("Microsoft Sans Serif", 8F,
        ((System.Drawing.FontStyle)(System.Drawing.FontStyle.Bold)), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                chart1.ChartAreas[0].AxisX.Title = "см";
                chart1.ChartAreas[0].AxisX.TitleFont = axisFont;
                chart1.ChartAreas[0].AxisY.Title = "кг";
                chart1.ChartAreas[0].AxisY.TitleFont = axisFont;

                chart2.ChartAreas[0].AxisX.Title = "см";
                chart2.ChartAreas[0].AxisX.TitleFont = axisFont;
                chart2.ChartAreas[0].AxisY.Title = "кг*см";
                chart2.ChartAreas[0].AxisY.TitleFont = axisFont;


                int n = 2;
                label9.Text = Math.Round(result.maxM,n).ToString();
                label12.Text = Math.Round(result.minM,n).ToString();
                label18.Text = Math.Round(Math.Abs(result.maxAbsQ),n).ToString();


                if (_pathToBeamDiagrams != null)
                {
                    string pathToPictureQ = "BeamDiagramQ.png";
                    chart1.SaveImage(pathToPictureQ, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Png);
                    _pathToBeamDiagrams.Add(pathToPictureQ);

                    string pathToPictureM = "BeamDiagramM.png";
                    chart2.SaveImage(pathToPictureM, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Png);
                    _pathToBeamDiagrams.Add(pathToPictureM);
                }

                double maxValueM;
                if (Math.Abs(result.maxM) >= Math.Abs(result.minM))
                { maxValueM = result.maxM; }
                else { maxValueM = result.minM; }
                for (int i = 0; i < _beamEfforts.ColumnCount; i++)
                {
                    if (_beamEfforts.Columns[i].Name == "My")
                    { _beamEfforts[i, 0].Value = Math.Round(maxValueM,n).ToString(); }
                    else if (_beamEfforts.Columns[i].Name == "Q")
                    { _beamEfforts[i, 0].Value = Math.Round(result.maxAbsQ, n).ToString(); }
                }
            }
            catch (Exception ex)
            {
                // обработка исключений
                string messageToUser = "";
                string exceptionMessage = ex.Message;
                string userErrorMarker = "Пользовательская ошибка. ";
                if (exceptionMessage.Contains(userErrorMarker))
                { messageToUser = exceptionMessage.Substring(userErrorMarker.Length); }
                else
                { messageToUser = "Что-то пошло не так. Программная ошибка."; }
                MessageBox.Show(messageToUser);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            chart2.Series.Clear();
            label9.Text = "0";
            label12.Text = "0";
            label18.Text = "0";

            //if (effortsModel.ContainsKey("Mmax"))
            //    effortsModel["Mmax"] = 0;
            //if (effortsModel.ContainsKey("Mmin"))
            //    effortsModel["Mmin"] = 0;
            //if (effortsModel.ContainsKey("Q"))
            //    effortsModel["Q"] = 0;


        }

        private void radioButton12_CheckedChanged(object sender, EventArgs e)
        {
            //numericUpDown4.Enabled = true;
            numericUpDown3.Enabled = false;
            ControllerBeamDiagram.load = "Uniformly-Distributed";
        }

        private void radioButton11_CheckedChanged(object sender, EventArgs e)
        {
            //numericUpDown4.Enabled = false;
            //numericUpDown4.Value = 0;
            numericUpDown3.Enabled = true;
            ControllerBeamDiagram.load = "Concentrated";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            ControllerBeamDiagram.support = "Fixed-Fixed";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            ControllerBeamDiagram.support = "Fixed-No";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            ControllerBeamDiagram.support = "Pinned-Movable";
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            ControllerBeamDiagram.support = "Fixed-Movable";
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            ControllerBeamDiagram.support = "No-Fixed";
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            ControllerBeamDiagram.support = "Movable-Fixed";
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            _beamLength.Text = numericUpDown1.Value.ToString();
        }
    }
}

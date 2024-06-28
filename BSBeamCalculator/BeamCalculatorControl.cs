using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BSBeamCalculator
{
    public partial class BeamCalculatorControl : UserControl
    {
        public BeamCalculatorControl()
        {
            InitializeComponent();

            ControllerBeamDiagram.load = "Concentrated";
            ControllerBeamDiagram.support = "Fixed-Fixed";
        }


        public Dictionary<string, double> effortsModel = new Dictionary<string, double>();


        public BeamCalculatorControl(Dictionary<string, double> efforts)
        {
            effortsModel = efforts;
            InitializeComponent();
            ControllerBeamDiagram.load = "Concentrated";
            ControllerBeamDiagram.support = "Fixed-Fixed";
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

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

                int n = 2;
                label9.Text = Math.Round(result.maxM,n).ToString();
                label12.Text = Math.Round(result.minM,n).ToString();
                label18.Text = Math.Round(Math.Abs(result.maxAbsQ),n).ToString();

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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            chart2.Series.Clear();
            label9.Text = "0";
            label12.Text = "0";
            label18.Text = "0";

            if (effortsModel.ContainsKey("Mmax"))
                effortsModel["Mmax"] = 0;
            if (effortsModel.ContainsKey("Mmin"))
                effortsModel["Mmin"] = 0;
            if (effortsModel.ContainsKey("Q"))
                effortsModel["Q"] = 0;


        }

        private void button3_Click(object sender, EventArgs e)
        {

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

        //void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string selectedCountry = listBox1.SelectedItem.ToString();
        //    MessageBox.Show(selectedCountry);
        //}

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
    }
}

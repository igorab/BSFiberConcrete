using BSBeamCalculator.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace BSBeamCalculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
                InitializeComponent();

                //Button saveButton = new Button();
                //TableLayoutPanel tableLayoutPanel1 = new TableLayoutPanel();

                //// добавляем кнопку в следующую свободную ячейку
                //tableLayoutPanel1.Controls.Add(saveButton);
                //// добавляем кнопку в ячейку (2,2)
                //tableLayoutPanel1.Controls.Add(saveButton, 2, 2);

                //tableLayoutPanel1.RowStyles[0].SizeType = SizeType.Percent;
                //tableLayoutPanel1.RowStyles[0].Height = 40;

                //tableLayoutPanel1.ColumnStyles[0].SizeType = SizeType.Absolute;
                //tableLayoutPanel1.ColumnStyles[0].Width = 50;

                Controller.load = "Concentrated";
                Controller.support = "Fixed-Fixed";

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

                //Controller.support =
                //Controller.load =
                Controller.l = lengthBeam;
                Controller.f = force;
                Controller.x1 = startPointForce;
                Controller.x2 = 0;

                // запуск расчета
                Controller.RunCalculation();

                // Вывод результатов расчета
                DiagramResult result = Controller.result;
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

                label9.Text = result.maxM.ToString();
                label12.Text = result.minM.ToString();
                label18.Text = Math.Abs(result.maxAbsQ).ToString();

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
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void radioButton12_CheckedChanged(object sender, EventArgs e)
        {
            //numericUpDown4.Enabled = true;
            numericUpDown3.Enabled = false;
            Controller.load = "Uniformly-Distributed";
        }

        private void radioButton11_CheckedChanged(object sender, EventArgs e)
        {
            //numericUpDown4.Enabled = false;
            //numericUpDown4.Value = 0;
            numericUpDown3.Enabled = true;
            Controller.load = "Concentrated";
        }

        //void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string selectedCountry = listBox1.SelectedItem.ToString();
        //    MessageBox.Show(selectedCountry);
        //}

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Controller.support = "Fixed-Fixed";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Controller.support = "Fixed-No";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            Controller.support = "Pinned-Movable";
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            Controller.support = "Fixed-Movable";
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            Controller.support = "No-Fixed";
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            Controller.support = "Movable-Fixed";
        }
    }
}

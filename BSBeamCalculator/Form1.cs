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
            Controller.support = "Fixed-No";

            AutoCompleteStringCollection source = new AutoCompleteStringCollection()
            {
                "Кузнецов",
                "Иванов",
                "Петров",
                "Кустов"
            };
            textBox1.AutoCompleteCustomSource = source;
            textBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;


            string[] countries = { "Бразилия", "Аргентина", "Чили", "Уругвай", "Колумбия" };
            listBox1.Items.AddRange(countries);

        }


        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            int A = 1;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            chart2.Series.Clear();
            // собираем данные с формы
            double lengthBeam = (double)numericUpDown1.Value;
            double force = (double)numericUpDown2.Value;
            double startPointForce = (double)numericUpDown3.Value;
            double endPointForce = (double)numericUpDown4.Value;
            //double supportBeam = 
            //double loadBeam = 

            //Controller.support =
            //Controller.load =
            Controller.l = lengthBeam;
            Controller.f = force;
            Controller.x1 = startPointForce;
            Controller.x2 = endPointForce;

            // запуск расчета
            Controller.RunCalculation();

            // результат расчета

            DiagramResult result = Controller.result;
            chart1.Series.Add("Series1");
            chart1.Series["Series1"].BorderWidth = 3;
            chart1.Series["Series1"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            for (int i = 0; i < result.valuesx.Count; i++)
            { chart1.Series["Series1"].Points.AddXY(result.valuesx[i], result.valuesQ[i]); }

            chart2.Series.Add("Series1");
            chart2.Series["Series1"].BorderWidth = 3;
            chart2.Series["Series1"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            for (int i = 0; i < result.valuesx.Count; i++)
            { chart2.Series["Series1"].Points.AddXY(result.valuesx[i], result.valuesM[i]); }


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string selectedCountry = listBox1.SelectedItem.ToString();
            listBox1.Items.Remove(selectedCountry);
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void radioButton12_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown4.Enabled = true;
            Controller.load = "Controller";
        }

        private void radioButton11_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown4.Enabled = false;
            numericUpDown4.Value = 0;
            Controller.load = "Uniformly-Distributed";
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
            Controller.support = "Hinged-Movable";
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            Controller.support = "Fixed-Movable";
        }
    }
}

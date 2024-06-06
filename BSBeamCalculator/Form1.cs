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
            MessageBox.Show("Hello World");
            listBox1.Items.Insert(0, "Парагвай");

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
        }

        private void radioButton11_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown4.Enabled = false;
            numericUpDown4.Value = 1;
        }

        //void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string selectedCountry = listBox1.SelectedItem.ToString();
        //    MessageBox.Show(selectedCountry);
        //}



    }
}

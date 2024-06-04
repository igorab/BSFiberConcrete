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
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

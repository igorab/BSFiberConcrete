using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BSFiberConcrete
{
    public partial class BSFiberMain : Form
    {
        public BSFiberMain()
        {
            InitializeComponent();
        }

        private void BSFiberMain_Load(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
           var bs = new BSFiberCalculation();

            double x = bs.Dzeta(1,2);
            double Mult = 0;
            (Mult, x) = bs.Mult_withoutArm(1, 1, 1, 1, 1, 1);

        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            BSFiberLoadData.Load();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void propertyGrid1_Click(object sender, EventArgs e)
        {

        }

        private void btnRectang_Click(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            table.Columns.Add("b, mm", typeof(double));
            table.Columns.Add("h, mm", typeof(double));

            dataGridView1.DataSource = table;
            table.Rows.Add(1.1d, 2.2d);
        }

        private void btnTSection_Click(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            table.Columns.Add("b, mm", typeof(double));
            table.Columns.Add("h, mm", typeof(double));
            table.Columns.Add("b1, mm", typeof(double));
            table.Columns.Add("h1, mm", typeof(double));

            dataGridView1.DataSource = table;
        }

        private void btnRing_Click(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            table.Columns.Add("D, mm", typeof(double));
            table.Columns.Add("d, mm", typeof(double));
            table.Columns.Add("a1, mm", typeof(double));
            
            dataGridView1.DataSource = table;
        }

        private void btnIBeam_Click(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            table.Columns.Add("b, mm", typeof(double));
            table.Columns.Add("h, mm", typeof(double));
            table.Columns.Add("b1, mm", typeof(double));
            table.Columns.Add("h1, mm", typeof(double));
            table.Columns.Add("b2, mm", typeof(double));
            table.Columns.Add("h2, mm", typeof(double));

            dataGridView1.DataSource = table;
        }
    }
}

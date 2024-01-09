using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BSFiberConcrete
{
    public partial class BSFiberMain : Form
    {
        private DataTable table;

        public Dictionary<string, double> CalcResults { get; set; }

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
            BSFiberCalculation bsCalc =  BSFiberCalculation.construct(BeamSection.Ring);
            BSFiberLoadData bsLoad = new BSFiberLoadData();
            bsLoad.Load();
            double[] prms = bsLoad.Params;
            
            bsCalc.GetParams(prms);
            
            double[] sz = new double[2] ;            
            foreach(DataRow r in table.Rows)
            {
                sz = new double[r.ItemArray.Length];
                int idx = 0;
                foreach (var item in r.ItemArray)
                {
                    sz[idx] = (double)item;
                    idx ++;
                }
            }
            bsCalc.GetSize(sz);

            bsCalc.Calculate();

            CalcResults
                = bsCalc.Results();

            tbResultW.Text = CalcResults["Wpl"].ToString(); 
            tbResult.Text = CalcResults["Mult"].ToString();

            //double x = bs.Dzeta(1,2);
            //double Mult = 0;
            //(Mult, x) = bs.Mult_withoutArm(1, 1, 1, 1, 1, 1);

        }

        private string CreateReport()
        {
            string pathToHtmlFile = "";

            using (FileStream fs = new FileStream("test.htm", FileMode.Create))
            {
                using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                {
                    w.WriteLine("<H1>Фибробетон</H1>");
                    w.WriteLine("<H2>Балка</H2>");
                    w.WriteLine("<H3>Расчет:</H3>");
                    w.WriteLine("<Table>");
                    w.WriteLine("<tr><th>Параметр </th><th>Величина </th></tr>");
                    string prm = "Напряжение";
                    double val = 1.0;
                    w.WriteLine($"<tr><td>{prm}</td><td>{val}</td></tr>");
                    w.WriteLine("</Table>");                    
                }

                pathToHtmlFile = fs.Name;
            }

            return pathToHtmlFile;
        }


        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                string pathToHtmlFile = CreateReport();

                System.Diagnostics.Process.Start(pathToHtmlFile);
            }
            catch
            {
                //System.Windows.
            }
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
            table = new DataTable();
            table.Columns.Add("b, mm", typeof(double));
            table.Columns.Add("h, mm", typeof(double));

            dataGridView1.DataSource = table;
            table.Rows.Add(800d, 600d);
        }

        private void btnTSection_Click(object sender, EventArgs e)
        {
            table = new DataTable();
            table.Columns.Add("b, mm", typeof(double));
            table.Columns.Add("h, mm", typeof(double));
            table.Columns.Add("b1, mm", typeof(double));
            table.Columns.Add("h1, mm", typeof(double));

            dataGridView1.DataSource = table;
        }

        private void btnRing_Click(object sender, EventArgs e)
        {
            table = new DataTable();
            table.Columns.Add("D, mm", typeof(double));
            table.Columns.Add("d, mm", typeof(double));
            table.Columns.Add("a1, mm", typeof(double));
            
            dataGridView1.DataSource = table;
        }

        private void btnIBeam_Click(object sender, EventArgs e)
        {
            table = new DataTable();
            table.Columns.Add("b, mm", typeof(double));
            table.Columns.Add("h, mm", typeof(double));
            table.Columns.Add("b1, mm", typeof(double));
            table.Columns.Add("h1, mm", typeof(double));
            table.Columns.Add("b2, mm", typeof(double));
            table.Columns.Add("h2, mm", typeof(double));

            dataGridView1.DataSource = table;
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void AboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            BSFiberAboutBox aboutWindow = new BSFiberAboutBox();
            aboutWindow.Show();
        }

        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BSFiberSetup setupWindow = new BSFiberSetup();
            setupWindow.Show();
        }
    }
}

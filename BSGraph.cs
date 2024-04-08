using BSFiberConcrete.BSRFib;
using BSFiberConcrete.Lib;
using MathNet.Numerics;
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
using Series = System.Windows.Forms.DataVisualization.Charting.Series;

namespace BSFiberConcrete
{
    public partial class BSGraph : Form
    {
        private BindingList<FaF> Qds;

        public BSGraph()
        {
            InitializeComponent();
        }
              
        private void BSGraph_Load(object sender, EventArgs e)
        {
            List<FaF> listFaF = BSData.LoadRChartFaF();

            Qds = new BindingList<FaF>(listFaF);
                        
            gridFaF.DataSource = Qds;
            gridFaF.ReadOnly = false;
            gridFaF.AllowUserToAddRows = false;
            gridFaF.Refresh();
            
        }

        /// <summary>
        /// В ходе испытаний для каждого образца строят графики «F – aF»
        /// </summary>        
        void InitChart()
        {
            Series series = this.ChartFaF.Series["AFSerie"]; 
            series.ChartType = SeriesChartType.Spline;

            //series.AxisLabel = "xxx";
                       
            ChartFaF.DataSource = Qds;
            ChartFaF.DataBind();

            //foreach (var item in q)
            //{
            //    series.Points.AddXY(item.aF, item.F);
            //}                       
        }

        private void CalcF()
        {
            BSRFibLabTensile labTensile = new BSRFibLabTensile();
            labTensile.DsFaF = Qds.ToList();

            numFel.Value = (decimal)Qds.Max(_x => _x.F);
            
            numF05.Value = (decimal)labTensile.F05();
            numF25.Value = (decimal)labTensile.F25();
            
        }


        private void btnDrawChart_Click(object sender, EventArgs e)
        {
            InitChart();

            CalcF();                        
        }

        private void btnDSAdd_Click(object sender, EventArgs e)
        {
            int mx =  Qds.Max(x=>x.Num+1);

            FaF item = new FaF() { Num = mx, F = 5, aF = 5 };
            Qds.Add(item);
            gridFaF.DataSource = Qds;
            //gridFaF.
            gridFaF.Refresh();
        }
    }
}

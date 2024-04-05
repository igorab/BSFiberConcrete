using BSFiberConcrete.Lib;
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

namespace BSFiberConcrete
{
    public partial class BSGraph : Form
    {
        public BSGraph()
        {
            InitializeComponent();
        }

      
        private void BSGraph_Load(object sender, EventArgs e)
        {
            //SplineChartExample();
        }
                
        void InitChart()
        {
            List<FaF> q = BSData.LoadRChartFaF();
            
            Series series = this.Chart.Series.Add("aF");
            series.ChartType = SeriesChartType.Line;

            foreach (var item in q)
            {
                series.Points.AddXY(item.aF, item.F);
            }                       
        }

        private void btnDrawChart_Click(object sender, EventArgs e)
        {
           InitChart();

        }
    }
}

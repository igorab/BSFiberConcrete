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
            SplineChartExample();
        }

        private void SplineChartExample()
        {
            this.Chart.Series.Clear();

            this.Chart.Titles.Add("Total Income");

            Series series = this.Chart.Series.Add("Total Income");
            series.ChartType = SeriesChartType.Spline;
            series.Points.AddXY("September", 100);
            series.Points.AddXY("Obtober", 300);
            series.Points.AddXY("November", 800);
            series.Points.AddXY("December", 200);
            series.Points.AddXY("January", 600);
            series.Points.AddXY("February", 400);
        }
    }
}

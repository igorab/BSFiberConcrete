using ScottPlot.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BSFiberConcrete.Section.DrawBeamSection
{
    public partial class DrawBeamSection : Form
    {

        /// <summary>
        ///  Форма для отрисовки FormsPlot (ScottPanel)
        /// </summary>
        protected FormsPlot _plotForForms = new FormsPlot() { Dock = DockStyle.Fill };

        public double MaxValue { get; set; }
        public double MinValue { get; set; }
        public double e_fb_max { get; set; }
        public double e_fbt_max { get; set; }

        public FormsPlot PlotForForms
        {
            get { return _plotForForms; }
            set
            {
                _plotForForms = value;
                pnlForPlot.Controls.Clear();
                pnlForPlot.Controls.Add(PlotForForms);
            }
        }

        public DrawBeamSection()
        {
            InitializeComponent();

            // Add the FormsPlot to the panel
            pnlForPlot.Controls.Add(_plotForForms);
        }

        private void DrawBeamSection_Load(object sender, EventArgs e)
        {
            numMaxValue.Value = (decimal)MaxValue;
            numMinValue.Value = (decimal)MinValue;

            num_e_fbt_max.Value = (decimal)e_fbt_max;
            num_e_fb_max.Value = (decimal)e_fb_max;

        }
    }
}

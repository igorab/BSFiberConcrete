using MathNet.Numerics;
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

        // предельные значения
        public double MaxValue { get; set; }
        public double MinValue { get; set; }
        // бетон:
        public double e_fb_max { get; set; }
        public double e_fbt_max { get; set; }
        // арматура:
        public double e_s_max { get; set; }
        public double e_st_max { get; set; }


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

        public int Mode { get; internal set; }

        public DrawBeamSection()
        {
            InitializeComponent();

            // Add the FormsPlot to the panel
            pnlForPlot.Controls.Add(_plotForForms);
        }

        private void DrawBeamSection_Load(object sender, EventArgs e)
        {
            comboMode.SelectedIndex = Mode;

            numMaxValue.Value = (decimal)MaxValue;
            numMinValue.Value = (decimal)MinValue;

            num_e_fbt_max.Value = (decimal)e_fbt_max;
            num_e_fb_max.Value = (decimal)e_fb_max;

            num_e_st_max.Value = (decimal)e_st_max;
            num_e_s_max.Value = (decimal)e_s_max;

            //σ, fbt max
            //σ, b max
            if (Mode == 2)
            {
                labelMax.Text = "R fbt3";
                labelMin.Text = "R s";

                label_fbt_max.Text = "σ, fbt max";
                label_b_max.Text = "σ, b max";

                label_st_max.Text = "σ, st max";
                label_s_max.Text = "σ, s max";
            }
        }

        private void comboMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _x = comboMode.SelectedItem;
        }
    }
}

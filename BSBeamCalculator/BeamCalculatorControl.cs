using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Net.Mime.MediaTypeNames;

namespace BSBeamCalculator
{
    public partial class BeamCalculatorControl : UserControl
    {

        /// <summary>
        /// TextBox с главной формы. Передается для оперативного изменения значения на главной форме.
        /// </summary>
        private TextBox _beamLength;
        /// <summary>
        /// DataGridView c главной формы. Передается для удобного и оперативного изменения данных на главной форме
        /// </summary>
        private DataGridView _beamEfforts;


        private BeamCalculatorViewModel _viewModel;



        /// <summary>
        /// Результаты построения диамгрммы
        /// </summary>
        public Dictionary<string, double> effortsModel;



        /// <summary>
        ///  Для оперативного обновления данных на главной форме в качестве параметров передаются :
        /// </summary>
        /// <param name="len"> длина балки</param>
        /// <param name="effortsData"> таблица с нагрузками</param>
        /// /// <param name="path2Diagram"> путь к картинке с диагрммой</param>
        public BeamCalculatorControl(BeamCalculatorViewModel viewModel)
        {
            //effortsData;

            _viewModel = viewModel;

            InitializeComponent();
            PerformBindings();

            rbFixed_Fixed.Checked = true;
            radioButton11.Checked = true;
            Run();
        }



        private void PerformBindings()
        {

            numBeamLen.DataBindings.Add(new Binding("Value", _viewModel, "BeamLength", true, DataSourceUpdateMode.OnPropertyChanged));
            numLenX.DataBindings.Add(new Binding("Maximum", _viewModel, "BeamLength", true, DataSourceUpdateMode.OnPropertyChanged));
            numForce.DataBindings.Add(new Binding("Value", _viewModel, "Force", true, DataSourceUpdateMode.OnPropertyChanged));
            numLenX.DataBindings.Add(new Binding("Value", _viewModel, "LengthX", true, DataSourceUpdateMode.OnPropertyChanged));

            label_Mmax.DataBindings.Add(new Binding("Text", _viewModel, "M_max", true, DataSourceUpdateMode.OnPropertyChanged));
            label_Mmin.DataBindings.Add(new Binding("Text", _viewModel, "M_min", true, DataSourceUpdateMode.OnPropertyChanged));
            label_Qmax.DataBindings.Add(new Binding("Text", _viewModel, "Q_max", true, DataSourceUpdateMode.OnPropertyChanged));

            //lab_Gamma_fb1.DataBindings.Add(new Binding("Text", _beamDiagramController, "Gamma_fb1", true, DataSourceUpdateMode.OnPropertyChanged));
            //lab_Gamma_fb1.DataBindings.Add(new Binding("Text", _beamDiagramController, "Gamma_fb1", true, DataSourceUpdateMode.OnPropertyChanged));
        }



        private void BeamCalculatorControl_Load(object sender, EventArgs e)
        {

            // Create the ToolTip and associate with the Form container.
            ToolTip toolTip1 = new ToolTip();

            // Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 50;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = true;

            // Set up the ToolTip text for the Button and Checkbox.
            toolTip1.SetToolTip(this.rbFixed_Fixed, "Жестко защемленная балка");
            toolTip1.SetToolTip(this.rbFixed_No, "Консольная балка");
            toolTip1.SetToolTip(this.rbPinned_Movable, "Простая балка");
            toolTip1.SetToolTip(this.rbFixed_Movable, "Балка с защемленным и шарнирно опертым концами");
            toolTip1.SetToolTip(this.rbNo_Fixed, "Консольная балка");
            toolTip1.SetToolTip(this.rbMovable_Fixed, "Балка с защемленным и шарнирно опертым концами");
        }


        private void Run()
        {
            try
            {
                tableLayoutPanel7.Controls.Clear();
                List<Chart> chartForceAndMoment = _viewModel.RunCalculation();

                tableLayoutPanel7.Controls.Add(chartForceAndMoment[0], 0, 1);
                tableLayoutPanel7.Controls.Add(chartForceAndMoment[1], 0, 0);
            }
            catch (Exception ex)
            {
                // обработка исключений
                string messageToUser = "";
                string exceptionMessage = ex.Message;
                string userErrorMarker = "Пользовательская ошибка. ";
                if (exceptionMessage.Contains(userErrorMarker))
                { messageToUser = exceptionMessage.Substring(userErrorMarker.Length); }
                else
                { messageToUser = "Что-то пошло не так. Программная ошибка."; }
                MessageBox.Show(messageToUser);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Run();
        }





        private void radioButton12_CheckedChanged(object sender, EventArgs e)
        {
            //numericUpDown4.Enabled = true;
            numLenX.Enabled = false;
            _viewModel.TypeBeamLoad = "Uniformly-Distributed";
            //_typeBeamLoad = "Uniformly-Distributed";
        }

        private void radioButton11_CheckedChanged(object sender, EventArgs e)
        {
            //numericUpDown4.Enabled = false;
            //numericUpDown4.Value = 0;
            numLenX.Enabled = true;
            _viewModel.TypeBeamLoad = "Concentrated";
            //_typeBeamLoad = "Concentrated";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            _viewModel.TypeBeamSupport = "Fixed-Fixed";
            //_typeBeamSupport = "Fixed-Fixed";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            _viewModel.TypeBeamSupport = "Fixed-No";
            //_typeBeamSupport = "Fixed-No";
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            _viewModel.TypeBeamSupport = "Pinned-Movable";
            //_typeBeamSupport = "Pinned-Movable";
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            _viewModel.TypeBeamSupport = "Fixed-Movable";
            //_typeBeamSupport = "Fixed-Movable";
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            _viewModel.TypeBeamSupport = "No-Fixed";
            //_typeBeamSupport = "No-Fixed";
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            _viewModel.TypeBeamSupport = "Movable-Fixed";
            //_typeBeamSupport = "Movable-Fixed";
        }
    }
}

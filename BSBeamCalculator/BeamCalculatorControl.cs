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


        public BeamCalculatorControl()
        {
            InitializeComponent();



        }

        /// <summary>
        ///  Для оперативного обновления данных на главной форме в качестве параметров передаются :
        /// </summary>
        /// <param name="len"> длина балки</param>
        /// <param name="effortsData"> таблица с нагрузками</param>
        /// /// <param name="path2Diagram"> путь к картинке с диагрммой</param>
        public BeamCalculatorControl(TextBox len, DataGridView effortsData, BeamDiagram beamDiagramController)
        {
            _beamLength = len;
            _beamEfforts = effortsData;

            InitializeComponent();


            double.TryParse(_beamLength.Text, out double tmpBeamLen );
            if (tmpBeamLen != 0)
            { 
                numBeamLen.Value = (decimal)tmpBeamLen;
                numLenX.Value = (decimal) tmpBeamLen / 2;
            }
        }

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


            //numericUpDown1.Value = (decimal)_viewModel.BeamLength;


            //double.TryParse(_viewModel._beamLength.Text, out double tmpBeamLen);
            //if (tmpBeamLen != 0)
            //{
            //    numericUpDown1.Value = (decimal)tmpBeamLen;
            //    numericUpDown3.Value = (decimal)tmpBeamLen / 2;
            //}



            //double.TryParse(len.Text, out double tmpBeamLen);
            //if (tmpBeamLen != 0)
            //{
            //    numericUpDown1.Value = (decimal)tmpBeamLen;
            //    numericUpDown3.Value = (decimal)tmpBeamLen / 2;
            //}
        }



        private void PerformBindings()
        {

            numBeamLen.DataBindings.Add(new Binding("Value", _viewModel, "BeamLength", true, DataSourceUpdateMode.OnPropertyChanged));
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



        private void button1_Click(object sender, EventArgs e)
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

            //    try
            //    {


            //        chart1.Series.Clear();
            //        chart2.Series.Clear();
            //        label9.Text = "0";
            //        label12.Text = "0";
            //        label18.Text = "0";
            //        if (_beamDiagramController.path2BeamDiagrams != null)
            //        { _beamDiagramController.path2BeamDiagrams.Clear(); }

            //        // собираем данные с формы
            //        double lengthBeam = (double)numericUpDown1.Value;
            //        double force = (double)numericUpDown2.Value;
            //        double startPointForce = (double)numericUpDown3.Value;

            //        _beamDiagramController.supportType = _typeBeamSupport;
            //        _beamDiagramController.loadType = _typeBeamLoad;
            //        _beamDiagramController.l = lengthBeam;
            //        _beamDiagramController.f = force;
            //        _beamDiagramController.x1 = startPointForce;


            //        // запуск расчета
            //        _beamDiagramController.RunCalculation();

            //        // Вывод результатов расчета
            //        DiagramResult result = _beamDiagramController.result;

            //        //_beamDiagramController.Test();
            //        //string[] names1 = { "Сила", "см", "кг", "BeamDiagramQ" };
            //        //chart1 = _beamDiagramController.CreteChart(result.pointM[0].ToList(), result.pointM[1].ToList(), names1);

            //        //string[] names2 = { "Момент", "см", "кг*см", "BeamDiagramM" };
            //        //chart2 = _beamDiagramController.CreteChart(result.pointM[0].ToList(), result.pointM[1].ToList(), names2);

            //        chart1.Series.Add("Series1");
            //        chart1.Series["Series1"].BorderWidth = 4;
            //        chart1.ChartAreas[0].AxisX.Minimum = 0;
            //        chart1.ChartAreas[0].AxisX.Maximum = _beamDiagramController.l;
            //        chart1.Series["Series1"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            //        for (int i = 0; i < result.pointQ[0].Length; i++)
            //        { chart1.Series["Series1"].Points.AddXY(result.pointQ[0][i], result.pointQ[1][i]); }

            //        chart2.Series.Add("Series1");
            //        chart2.Series["Series1"].BorderWidth = 4;
            //        chart2.Series["Series1"].Color = System.Drawing.Color.Red;
            //        chart2.ChartAreas[0].AxisX.Minimum = 0;
            //        chart2.ChartAreas[0].AxisX.Maximum = _beamDiagramController.l;
            //        chart2.Series["Series1"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            //        for (int i = 0; i < result.pointM[0].Length; i++)
            //        { chart2.Series["Series1"].Points.AddXY(result.pointM[0][i], result.pointM[1][i]); }

            //        Font axisFont = new System.Drawing.Font("Microsoft Sans Serif", 8F,
            //((System.Drawing.FontStyle)(System.Drawing.FontStyle.Bold)), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            //        chart1.ChartAreas[0].AxisX.Title = "см";
            //        chart1.ChartAreas[0].AxisX.TitleFont = axisFont;
            //        chart1.ChartAreas[0].AxisY.Title = "кг";
            //        chart1.ChartAreas[0].AxisY.TitleFont = axisFont;

            //        chart2.ChartAreas[0].AxisX.Title = "см";
            //        chart2.ChartAreas[0].AxisX.TitleFont = axisFont;
            //        chart2.ChartAreas[0].AxisY.Title = "кг*см";
            //        chart2.ChartAreas[0].AxisY.TitleFont = axisFont;


            //        _beamDiagramController.SaveChart(chart1, "BeamDiagramQ");
            //        _beamDiagramController.SaveChart(chart2, "BeamDiagramM");


            //        int n = 2;
            //        label9.Text = Math.Round(result.maxM, n).ToString();
            //        label12.Text = Math.Round(result.minM, n).ToString();
            //        label18.Text = Math.Round(Math.Abs(result.maxAbsQ), n).ToString();

            //        if (_beamEfforts != null)
            //        {
            //            //double maxValueM;
            //            //if (Math.Abs(result.maxM) >= Math.Abs(result.minM))
            //            //{ maxValueM = result.maxM; }
            //            //else { maxValueM = result.minM; }

            //            for (int i = 0; i < _beamEfforts.ColumnCount; i++)
            //            {
            //                if (_beamEfforts.Columns[i].Name == "My")
            //                { 
            //                    _beamEfforts[i, 0].Value = Math.Round(result.maxM, n).ToString();
            //                    _beamEfforts[i, 1].Value = Math.Round(result.minM, n).ToString();
            //                }
            //                else if (_beamEfforts.Columns[i].Name == "Qx")
            //                { 
            //                    if ((result.maxM != 0) || (result.maxM == 0 && result.minM == 0))
            //                        _beamEfforts[i, 0].Value = Math.Round(result.maxAbsQ, n).ToString();

            //                    if (result.minM != 0)
            //                        _beamEfforts[i, 1].Value = Math.Round(result.maxAbsQ, n).ToString();
            //                }
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        // обработка исключений
            //        string messageToUser = "";
            //        string exceptionMessage = ex.Message;
            //        string userErrorMarker = "Пользовательская ошибка. ";
            //        if (exceptionMessage.Contains(userErrorMarker))
            //        { messageToUser = exceptionMessage.Substring(userErrorMarker.Length); }
            //        else
            //        { messageToUser = "Что-то пошло не так. Программная ошибка."; }
            //        MessageBox.Show(messageToUser);
            //    }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //chart1.Series.Clear();
            //chart2.Series.Clear();
            label_Mmax.Text = "0";
            label_Mmin.Text = "0";
            label_Qmax.Text = "0";



            //if (_beamDiagramController.path2BeamDiagrams != null)
            //{
            //    _beamDiagramController.path2BeamDiagrams.Clear();
            //    for (int i = 0; i < _beamEfforts.ColumnCount; i++)
            //    {
            //        if (_beamEfforts.Columns[i].Name == "My")
            //        { _beamEfforts[i, 0].Value = "0"; }
            //        else if (_beamEfforts.Columns[i].Name == "Q")
            //        { _beamEfforts[i, 0].Value = "0"; }
            //    }
            //}



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

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            _viewModel.BeamLength = (double)numBeamLen.Value;
            numBeamLen.Maximum = numBeamLen.Value;


            //if (_beamLength != null)  
            //_beamLength.Text = numBeamLen.Value.ToString();
        }


    }
}

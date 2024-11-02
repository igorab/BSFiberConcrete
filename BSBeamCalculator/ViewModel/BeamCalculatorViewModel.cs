using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace BSBeamCalculator
{
    public class BeamCalculatorViewModel: ViewModelBase
    {
        /// <summary>
        /// TextBox с главной формы. Передается для оперативного изменения значения на главной форме.
        /// </summary>
        private TextBox _beamLength;
        /// <summary>
        /// DataGridView c главной формы. Передается для удобного и оперативного изменения данных на главной форме
        /// </summary>
        private DataGridView _beamEfforts;




        public BeamDiagram BeamDiagramModel;



        /// <summary>
        /// Тип замещение балки
        /// </summary>
        private string _typeBeamSupport;
        /// <summary>
        /// Тип нагрузки на балку
        /// </summary>
        private string _typeBeamLoad;

        private double _Force;
        private double _LengthX;



        private double _M_max;
        private double _M_min;
        private double _Q_max;


        private System.Windows.Forms.DataVisualization.Charting.Chart _ForceChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart _MomentChart;




        #region 

        public string TypeBeamSupport
        {
            get => _typeBeamSupport;
            set => _typeBeamSupport = value;
        }


        public string TypeBeamLoad
        {
            get => _typeBeamLoad;
            set => _typeBeamLoad = value;
        }



        public double Force
        {
            get
            {
                return _Force;
            }
            set
            {
                _Force = value;
                OnPropertyChanged();
            }
        }

        public double LengthX
        {
            get
            {
                return _LengthX;
            }
            set
            {
                _LengthX = value;
                OnPropertyChanged();
            }
        }

        public double BeamLength
        {
            get
            {
                double.TryParse(_beamLength.Text, out double tmpBeamLen);
                return tmpBeamLen;
            }
            set
            {
                _beamLength.Text = value.ToString();
                OnPropertyChanged();
            }
        }



        public double M_max
        {
            get
            {
                return _M_max;
            }
            set
            {
                _M_max = value;
                OnPropertyChanged();
            }
        }


        public double M_min
        {
            get
            {
                return _M_min;
            }
            set
            {
                _M_min = value;
                OnPropertyChanged();
            }
        }

        public double Q_max
        {
            get
            {
                return _Q_max;
            }
            set
            {
                _Q_max = value;
                OnPropertyChanged();
            }
        }

        public System.Windows.Forms.DataVisualization.Charting.Chart ForceChart
        {
            get
            {
                return _ForceChart;
            }
            set
            {
                _ForceChart = value;
                OnPropertyChanged();
            }
        }

        public System.Windows.Forms.DataVisualization.Charting.Chart MomentChart
        {
            get
            {
                return _MomentChart;
            }
            set
            {
                _MomentChart = value;
                OnPropertyChanged();
            }
        }




        #endregion



        public BeamCalculatorViewModel(TextBox beamLength, DataGridView beamEfforts, List<string> m_Path2BeamDiagrams = null)
        {

            //Fixed_Fixed = false;
            _beamLength = beamLength;
            _beamEfforts = beamEfforts;


            Force = 1;
            if (BeamLength != 0)
            { BeamLength = 100; }
            LengthX = BeamLength / 2;


            //double.TryParse(_beamLength.Text, out double tmpBeamLen);

            // if (tmpBeamLen == 0)
            // {
            //     BeamLength = 10;
            // }



            BeamDiagramModel = new BeamDiagram(m_Path2BeamDiagrams);
        }


        public List<Chart> RunCalculation()
        {

            //M_max = 0;
            //M_min = 0;
            //Q_max = 0;


            //ForceChart;

            //chart1.Series.Clear();
            //chart2.Series.Clear();
            //label9.Text = "0";
            //label12.Text = "0";
            //label18.Text = "0";
            //if (_beamDiagramController.path2BeamDiagrams != null)
            //{ _beamDiagramController.path2BeamDiagrams.Clear(); }

            // собираем данные с формы
            //double lengthBeam = (double)numericUpDown1.Value;
            //double force = (double)numericUpDown2.Value;
            //double startPointForce = (double)numericUpDown3.Value;


            if (TypeBeamLoad == "Concentrated" && LengthX > BeamLength)
            { throw new Exception("Пользовательская ошибка. Значение 'Позиция x' не должно превышать 'Длина'."); }

            BeamDiagramModel.supportType = TypeBeamSupport;
            BeamDiagramModel.loadType = TypeBeamLoad;
            BeamDiagramModel.f = Force;
            BeamDiagramModel.l = BeamLength;
            BeamDiagramModel.x1 = LengthX;

            // запуск расчета
            List<Chart> chartForceAndMoment = BeamDiagramModel.RunCalculation();

            //// Вывод результатов расчета
            DiagramResult result = BeamDiagramModel.result;

            //ForceChart = chartForceAndMoment[0];
            //MomentChart = chartForceAndMoment[1];

            return chartForceAndMoment;



            ////_beamDiagramController.Test();
            ////string[] names1 = { "Сила", "см", "кг", "BeamDiagramQ" };
            ////chart1 = _beamDiagramController.CreteChart(result.pointM[0].ToList(), result.pointM[1].ToList(), names1);

            ////string[] names2 = { "Момент", "см", "кг*см", "BeamDiagramM" };
            ////chart2 = _beamDiagramController.CreteChart(result.pointM[0].ToList(), result.pointM[1].ToList(), names2);





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

    }




    //_typeBeamSupport = "Fixed-Fixed";
    //_typeBeamSupport = "Fixed-No";
    //_typeBeamSupport = "Pinned-Movable";
    //_typeBeamSupport = "Fixed-Movable";
    //_typeBeamSupport = "No-Fixed";
    //_typeBeamSupport = "Movable-Fixed";

    // _typeBeamLoad = "Uniformly-Distributed";
    // _typeBeamLoad = "Concentrated";



}

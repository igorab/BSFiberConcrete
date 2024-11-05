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

        /// <summary>
        /// экземпляр класса BeamDiagram, содержит в себе состояния по котрым были построены эпюры.
        /// </summary>
        public BeamDiagram BeamDiagramModel;

        /// <summary>
        /// Тип защемления балки
        /// </summary>
        private string _typeBeamSupport;
        /// <summary>
        /// Тип нагрузки на балку
        /// </summary>
        private string _typeBeamLoad;
        /// <summary>
        /// Нагрузка на балку
        /// </summary>
        private double _Force;
        /// <summary>
        /// точка приложения нагрузки (только для сосредоточенной силы)
        /// </summary>
        private double _LengthX;

        // результаты расчета
        private double _M_max;
        private double _M_min;
        private double _Q_max;

        //private System.Windows.Forms.DataVisualization.Charting.Chart _ForceChart;
        //private System.Windows.Forms.DataVisualization.Charting.Chart _MomentChart;


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

        //public System.Windows.Forms.DataVisualization.Charting.Chart ForceChart
        //{
        //    get
        //    {
        //        return _ForceChart;
        //    }
        //    set
        //    {
        //        _ForceChart = value;
        //        OnPropertyChanged();
        //    }
        //}

        //public System.Windows.Forms.DataVisualization.Charting.Chart MomentChart
        //{
        //    get
        //    {
        //        return _MomentChart;
        //    }
        //    set
        //    {
        //        _MomentChart = value;
        //        OnPropertyChanged();
        //    }
        //}




        #endregion

        public BeamCalculatorViewModel(TextBox beamLength, DataGridView beamEfforts, List<string> m_Path2BeamDiagrams, List<double> savedValues)
        {
            _beamLength = beamLength;
            _beamEfforts = beamEfforts;
            SetProperty(savedValues);
            BeamDiagramModel = new BeamDiagram(m_Path2BeamDiagrams);
        }

        public BeamCalculatorViewModel()
        {
            // используется класс TextBox только потому, что так удобнее для кейса из др конструктора
            _beamLength = new TextBox();
            _beamLength.Text = "100";
            SetProperty(new List<double> {0, 0 });
            BeamDiagramModel = new BeamDiagram();
        }



        private void SetProperty(List<double> savedValues)
        {
            //double tmpLengthX = 0;
            //double tmpForce = 0; 
            double tmpLengthX = savedValues[0];
            double tmpForce = savedValues[1];
            
            double.TryParse(_beamLength.Text, out double tmpBeamLen);

            if (tmpBeamLen <= 0 && tmpLengthX <= 0)
            {
                BeamLength = 100;
                LengthX = BeamLength / 2;
            }
            else if (tmpLengthX <= 0)
            { LengthX = BeamLength / 2; }
            else if (tmpBeamLen <= 0)
            { BeamLength = tmpLengthX * 2; }
            else
            {
                if (tmpBeamLen >= tmpLengthX)
                { LengthX = tmpLengthX; }
                else
                { LengthX = BeamLength / 2; }
            }

            if (tmpForce <= 0)
            { Force = 10; }
            else
            { Force = tmpForce; }
        }



        public List<Chart> RunCalculation()
        {
            if (TypeBeamLoad == "Concentrated" && LengthX > BeamLength)
            { throw new Exception("Пользовательская ошибка. Значение 'Позиция x' не должно превышать 'Длина'."); }

            BeamDiagramModel.supportType = TypeBeamSupport;
            BeamDiagramModel.f = Force;
            BeamDiagramModel.loadType = TypeBeamLoad;
            BeamDiagramModel.l = BeamLength;
            BeamDiagramModel.x1 = LengthX;

            // запуск расчета
            List<Chart> chartForceAndMoment = BeamDiagramModel.RunCalculation();

            //// Вывод результатов расчета
            DiagramResult result = BeamDiagramModel.result;
            int n = 3;
            M_max = Math.Round(result.maxM, n);
            M_min = Math.Round(result.minM, n);
            Q_max = Math.Round(result.maxAbsQ, n);

            // заполнение таблицы Efforts
            if (_beamEfforts != null)
            {
                for (int i = 0; i < _beamEfforts.ColumnCount; i++)
                {
                    if (_beamEfforts.Columns[i].Name == "My")
                    {
                        _beamEfforts[i, 0].Value = Math.Round(result.maxM, n).ToString();
                        _beamEfforts[i, 1].Value = Math.Round(result.minM, n).ToString();
                    }
                    else if (_beamEfforts.Columns[i].Name == "Qx")
                    {
                        if ((result.maxM != 0) || (result.maxM == 0 && result.minM == 0))
                            _beamEfforts[i, 0].Value = Math.Round(result.maxAbsQ, n).ToString();

                        if (result.minM != 0)
                            _beamEfforts[i, 1].Value = Math.Round(result.maxAbsQ, n).ToString();
                    }
                }
            }

            return chartForceAndMoment;

        }
    }
}

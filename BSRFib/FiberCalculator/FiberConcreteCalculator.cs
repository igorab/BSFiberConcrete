﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Xml.Linq;
using BSFiberConcrete.Lib;
using MathNet.Numerics;
using ScottPlot.Hatches;

namespace BSFiberConcrete.BSRFib.FiberCalculator
{
    /// <summary>
    /// Класс для определения характеристик фибробетона по параметрам фибры
    /// </summary>
    public class FiberConcreteCalculator : ViewModelBase
    {
        private FiberMaterial _fiber;

        private ConcreteMaterial _beton;

        private FiberCoef_K _fiberCoef;



        public FiberMaterial Fiber { get => _fiber; }
        public ConcreteMaterial Beton { get => _beton; }
        public FiberCoef_K FiberCoef{ get => _fiberCoef; }


        //private BSMatConcrete _beton;
        //private Beton _beton

        private double _h;
        private double _b;
        private double _mu_fv;


        #region Private Fields Fiber

        #endregion


        #region Private Fields Calculation
        private double _Kor;
        private double _Kn;
        private double _l_f_an;
        private double _mu_fv_min;
        private double _A_min;
        private double _C_max;
        private double _l_f_min;
        private double _R_fbt3;
        private double _R_fbt3_n;
        private double _R_fb;
        private double _mu_fa;
        private double _mu_1_fa;
        private double _E_fb;
        private double _G_fb;
        private string _message;
        private List<string> _msgToReport;

        #endregion

        #region Properties Calculation
        public double Kor
        {
            get => _Kor;
            private set
            {
                _Kor = value;
                OnPropertyChanged();
            }
        }
        public double Kn
        {
            get => _Kn;
            private set
            {
                _Kn = value;
                OnPropertyChanged();
            }
        }
        public double l_f_an
        {
            get => _l_f_an;
            private set
            { 
                _l_f_an = value;
                OnPropertyChanged();
            }
        }
        public double mu_fv_min
        {
            get => _mu_fv_min;
            private set
            {
                _mu_fv_min = value;
                OnPropertyChanged();
            }
        }
        public double A_min
        {
            get => _A_min;
            private set
            {
                _A_min = value;
                OnPropertyChanged();
            }
        }
        public double C_max
        {
            get => _C_max;
            private set
            {
                _C_max = value;
                OnPropertyChanged();
            }
        }
        public double l_f_min
        {
            get => _l_f_min;
            private set
            {
                _l_f_min = value;
                OnPropertyChanged();
            }
        }
        public double R_fbt3_n
        {
            get => _R_fbt3_n;
            private set
            {
                _R_fbt3_n = value;
                OnPropertyChanged();
            }
        }
        public double R_fbt3
        {
            get => _R_fbt3;
            private set
            {
                _R_fbt3 = value;
                OnPropertyChanged();
            }
        }
        public double R_fb
        {
            get => _R_fb;
            private set
            {
                _R_fb = value;
                OnPropertyChanged();
            }
        }
        public double mu_fa
        {
            get => _mu_fa;
            private set
            {
                _mu_fa = value;
                OnPropertyChanged();
            }
        }
        public double mu_1_fa
        {
            get => _mu_1_fa;
            private set
            {
                _mu_1_fa = value;
                OnPropertyChanged();
            }
        }
        public double E_fb
        {
            get => _E_fb;
            private set
            {
                _E_fb = value;
                OnPropertyChanged();
            }
        }
        public double G_fb
        {
            get => _G_fb;
            private set
            {
                _G_fb = value;
                OnPropertyChanged();
            }
        }

        public string message
        {
            get => _message;
            private set
            {
                _message = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Properties Fiber
        #endregion


        public FiberConcreteCalculator()
        {

            // Определить класс FiberMaterial
            _fiber = new FiberMaterial();
            // Определить класс Бетон
            _beton = new ConcreteMaterial();
            // Определяем коэффициенты фибры
            double h = 100;
            double b = 200;
            _fiberCoef = new FiberCoef_K(h,b, _fiber.Length);

            _msgToReport = new List<string>();
            // Определить класс 
            // определить тип нагрузки для 

        }

        public FiberConcreteCalculator(double h, double b)
        {

            // Определить класс FiberMaterial
            _fiber = new FiberMaterial();
            // Определить класс Бетон
            _beton = new ConcreteMaterial();
            // Определяем коэффициенты фибры
            _fiberCoef = new FiberCoef_K(h, b, _fiber.Length);

            _msgToReport = new List<string>();
            // Определить класс 
            // определить тип нагрузки для 

        }


        public void SetSectionH(double h)
        {
            _h = h;
            _fiberCoef.SetH(h);
        }


        public void SetSectionB(double b)
        {
            _b = b;
            _fiberCoef.SetB(b);
        }


        public void SetMu_fv(double mu)
        {
            _mu_fv = mu;
        }


        /// <summary>
        /// Производится расчет характеристик фибры
        /// </summary>
        public void Calculate()
        {

            Kor = FiberCoef.Kor;
            Kn = FiberCoef.Kn;

            double d_f_red;
            if (Fiber.Diameter != 0)
                d_f_red = Fiber.Diameter;
            else
                d_f_red = 1.13 * Math.Sqrt(Fiber.Square);

            double mu_fv;
            // Коэф для расчета R_fbt3
            double Kt;
            // Коэф эффективного косвенного армирования фибрами
            double fi_f;


            // Длина заделки фибры в бетоне
            l_f_an = 0;
            // минимальное значение коэффициента фибрового армирования
            mu_fv_min = 0;
            // минимальная площадь сечения элемента
            A_min = 0;
            // Максимальный размер зерен крупного заполнителя
            C_max = 0;
            // минимальное значение длины фибры
            l_f_min = 0;
            // Сопротивление растяжения стальфибробетона  
            R_fbt3 = 0;
            // Сопротилвение сжатия стальфибробетона
            R_fbt3_n = 0;
            // Сопротивление сжатия стальфибробетона
            R_fb = 0;
            // Коэф фибрового рамирования по площади
            // Для растянутой зоны
            mu_fa = 0;
            // Для сжатой зоны
            mu_1_fa = 0;
            // Модуль упругости
            E_fb = 0;
            G_fb = 0;
            message = null;
            _msgToReport = new List<string>();
            //lab_Kor.DataBindings.Add(new Binding("Text", _model.FiberCoef, "Kor", true, DataSourc

            // мм
            l_f_an = Fiber.Hita_f * d_f_red * Fiber.Rf_ser / Beton.Rb_ser;
            if (l_f_an >= Fiber.Length)
            {
                message = "Расчетное значение \"Длина заделки фибры в бетоне\" не должно превышать значение \"Длина фибры\".";
                _msgToReport.Add(message);
                return;
                
            }

            if ((30 / Fiber.Rf + l_f_an / Fiber.Length) >= 1)
            {
                message = "Невозможно расcчитать характеристики сталефибробетона с указанными параметрами";
                _msgToReport.Add(message);
                return;
                //    if 
                //}
                //{
                //    message = "Значене длины заделки фибры в бетоне ";
                //    return;
            }

            mu_fv_min = 1.5 * Fiber.coef_C * Beton.Rbt / (Fiber.Rf * Math.Pow(FiberCoef.Kor, 2) *
                (1 - 30 / Fiber.Rf - l_f_an / Fiber.Length));
            // сравнить mu_fv_min с верхней границей диапазона 
            if (mu_fv_min > 0.02)
            {
                message = "Расчетная величина \"Минимаьное значение коэф. фибрового армирования\" не должно превышать 0.002.";
                _msgToReport.Add(message);
                return;
            }


            // мм
            C_max = 5.3 * Math.Pow(d_f_red * d_f_red * Fiber.Length / (100 * mu_fv_min), 1.0 / 3);
            string msg = Beton.Evaluate_Cmax(C_max);
            if (msg != null)
            {
                message = msg;
                _msgToReport.Add(message);
            }


            // мм
            l_f_min = 2.5 * C_max;
            // сравнить длину фибры с минимальным значением
            if (l_f_min > Fiber.Length)
            {
                message = "Расчетная величина \"Минимальное значение длины фибры\" не должно превышать значение \"Длина фибры\".";
                _msgToReport.Add(message);
                return;
            }



            // назначение расчетного коэф фибрового армирования
            //  если mu_fv = 0 выполнять расчет по минимальному значению
            if (_mu_fv == 0)
            { 
                mu_fv = mu_fv_min;

                if (mu_fv < 0.005)
                {
                    message = "Минимальное значение Коэф. фибрового армирования, принятое в качестве расчетного, должно быть больше 0.005.";
                    _msgToReport.Add(message);
                    return;
                }
            }
            else
            { 
                mu_fv = _mu_fv;


                if (mu_fv_min > 0.02)
                {
                    message = "Значение Коэф. фибрового армирования не должно превышать 0.02.";
                    _msgToReport.Add(message);
                    return;
                }

                if (mu_fv < mu_fv_min)
                {
                    message = "Значение Коэф. фибрового армирования должно быть больше минимального значения Коэф. фибрового армирования";
                    _msgToReport.Add(message);
                    return;
                }

                // сравнить mu_fv с верхней нижней диапазона 
                if (mu_fv < 0.005)
                {
                    message = "Значение Коэф. фибрового армирования должно быть больше 0.005.";
                    _msgToReport.Add(message);
                    return;
                }


            }

            A_min = 4 * d_f_red / (mu_fv * Kor);

            if (A_min > _h * _b)
            {
                message = "Не выполняется условие по минимальной площади поперечного сечения элемента. Пункт 8.8.";
                _msgToReport.Add(message);
                return;
            }


            Kt = Math.Sqrt(1 - (1.2 - 80 * mu_fv) * (1.2 - 80 * mu_fv));
            // разветвление расчета на длва случая, исходя из условия 
            if (l_f_an < Fiber.Length / 2)
            {
                

                R_fbt3 = Fiber.Gamma_fb1 * (Kt * Math.Pow(FiberCoef.Kor, 2)
                    * mu_fv * Fiber.Rf * (1 - l_f_an / Fiber.Length) + 0.1 * Beton.Rb
                    * (0.8 - Math.Sqrt(2 * mu_fv - 0.005)));
            }
            else // l_f_an >= Fiber.Length / 2
            {
                R_fbt3 = Fiber.Gamma_fb1 * Beton.Rb * (Kt * Math.Pow(FiberCoef.Kor, 2)
                    * mu_fv * Fiber.Length / (8 * Fiber.Hita_f * d_f_red) + 0.08 - 0.5 * mu_fv);
            }

            R_fbt3_n = R_fbt3 * 1.3;



            double L = Math.Pow(FiberCoef.Kn, 2) * mu_fv * Fiber.Rf / Beton.Rb;
            fi_f = (5 + L) / (1 + 4.5 * L);
            R_fb = Beton.Rb + (Math.Pow(FiberCoef.Kn, 2) * fi_f * mu_fv * Fiber.Rf);

            mu_fa = mu_fv * Math.Pow(FiberCoef.Kor, 2);

            mu_1_fa = mu_fv * Math.Pow(FiberCoef.Kn, 2);

            E_fb = Beton.Eb * (1 - mu_fv) + Fiber.Ef * mu_fv;
            G_fb = 0.4 * E_fb;
        }


        public void GenerateReport()
        {
            if ((l_f_an == null) ||(l_f_an == 0))
            {
                message = "Построение отчета не выполнено, т.к. расчет не произведен.";
                _msgToReport.Add(message);
                return;
            }

            try
            {
                BSRFibLabReport labReport = new BSRFibLabReport();

                labReport.ReportName = "Определение характеристик фибробетона";
                labReport.SampleDescr = "";
                labReport.SampleName = "";

                Dictionary<string, string> InputData = new Dictionary<string, string>()
                {
                    ["Высота прямоугольного сечения h [мм]"] = Convert.ToString(Math.Round(_h, 3)),
                    ["Ширина прямоугольного сечения b [мм]"] = Convert.ToString(Math.Round(_b, 3)),
                    [$"Бетон {Beton.NameConcreteType}"] = "",
                    ["Расчетное сопротивление бетона (сжатия) Rb [МПа]"] = Convert.ToString(Math.Round(Beton.Rb, 6)),
                    ["Расчетное сопротивление бетона (сжатия) Rb_ser [МПа]"] = Convert.ToString(Math.Round(Beton.Rb_ser, 6)),
                    ["Расчетное сопротивление бетона (растяжение) Rbt [МПа]"] = Convert.ToString(Math.Round(Beton.Rbt, 6)),
                    ["Расчетное сопротивление бетона (растяжение) Rbt_ser [МПа]"] = Convert.ToString(Math.Round(Beton.Rbt_ser, 6)),
                    [Fiber.FiberName] = "",
                    ["Модуль упругости бетона Eb [МПа]"] = Convert.ToString(Math.Round(Beton.Eb, 6)),
                    ["Модуль упругости бетона Eb [МПа]"] = Convert.ToString(Math.Round(Beton.Eb, 6)),
                    ["Модуль упругости бетона Eb [МПа]"] = Convert.ToString(Math.Round(Beton.Eb, 6)),
                    ["Коэф. анкеровки фибры ηf "] = Convert.ToString(Math.Round(Fiber.Hita_f, 2)),
                    ["Коэф. условий работы γfb1"] = Convert.ToString(Math.Round(Fiber.Gamma_fb1, 2)),
                    ["Нормативное сопротивление растяжению фибры Rf_ser [МПа]"] = Convert.ToString(Math.Round(Fiber.Rf_ser, 6)),
                    ["Расчетное сопротивление фибровой арматуры Rf [МПа]"] = Convert.ToString(Math.Round(Fiber.Rf, 6)),
                    ["Модуль упругости фибры Ef [МПа]"] = Convert.ToString(Math.Round(Fiber.Ef, 6)),
                    ["Длина фибры lf [мм]"] = Convert.ToString(Math.Round(Fiber.Length, 3)),

                };
                if (Fiber.Diameter != 0)
                { InputData.Add("Диаметр фибры df [мм]", Convert.ToString(Math.Round(Fiber.Diameter, 3))); }
                else
                { InputData.Add("Площадь фибры Sf [мм2]", Convert.ToString(Math.Round(Fiber.Square, 3))); }

                if (_mu_fv != 0)
                { InputData.Add("Коэф. фибрового армирования μ_fv ", Convert.ToString(Math.Round(_mu_fv, 4))); }
                else
                { InputData.Add("Коэф. фибрового армирования μ_fv = μfv_min", Convert.ToString(Math.Round(mu_fv_min, 4))); }
                InputData.Add("П 8.10. Безразмерный коэф. C", Convert.ToString(Math.Round(Fiber.coef_C, 4)));


                Dictionary<string, string> LabItems = new Dictionary<string, string>()
                {
                    ["Коэф. ориентации фибры Kor [Таблица В1.]"] = Convert.ToString(Math.Round(FiberCoef.Kor, 3)),
                    ["Коэф. работы фибры Kn [Таблица В2.]"] = Convert.ToString(Math.Round(FiberCoef.Kn, 3)),
                    ["Длина заделки фибры в бетоне l_f,an [мм] [П В1.]"] = Convert.ToString(Math.Round(l_f_an,3)),
                    ["Минимальное значение коэф. фибрового армирования μfv_min [П 8.10.]"] = Convert.ToString(Math.Round(mu_fv_min,4)),
                    ["Максимальный размер зерен крупного заполнителя C_max [мм] [П 8.11.]"] = Convert.ToString(Math.Round(C_max,3)),
                    ["Минимальная площадь поперечного сечения элемента [мм2] П 8.8."] = Convert.ToString(Math.Round(A_min, 3)),
                    ["Минимальное значение длины фибры l_f_min [мм] [П 8.12.]"] = Convert.ToString(Math.Round(l_f_min,3)),
                    ["Расчетное остаточное сопротивление растяжение Rfbt3 [МПа] [П В2, В3.]"] = Convert.ToString(Math.Round(R_fbt3,6)),
                    ["Нормативное остаточное сопротивление растяжение Rfbt3_n [МПа] [П 5.2.6.]"] = Convert.ToString(Math.Round(R_fbt3_n, 6)),
                    ["Расчетное сопротивление сжатия Rfb [МПа] [П В5.]"] = Convert.ToString(Math.Round(R_fb,6)),
                    ["Коэф. фибрового армирования по площади (растянутой зоны) μ_fa [П В7.]"] = Convert.ToString(Math.Round(mu_fa,4)),
                    ["Коэф. фибрового армирования по площади (сжатой зоны) μ'_fa [П В7.]"] = Convert.ToString(Math.Round(mu_1_fa, 4)),
                    ["Модуль упругости Efb [МПа] [П 5.2.8]"] = Convert.ToString(Math.Round(E_fb,6)),
                    ["Модуль сдвига Gfb [МПа]"] = Convert.ToString(Math.Round(G_fb,6))
                };
                labReport.InputData = InputData;
                labReport.LabItems = LabItems;
                labReport.ReportMessage = _msgToReport;
                labReport.RunReport();
            }
            catch (Exception _ex)
            {
                MessageBox.Show(_ex.Message);
            }
        }
    }
}

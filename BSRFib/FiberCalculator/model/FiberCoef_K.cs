using BSFiberConcrete.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BSFiberConcrete.BSRFib.FiberCalculator
{
    /// <summary>
    /// Класс для определение коэф Кor и Kn 
    /// ()
    /// </summary>
    public class FiberCoef_K : ViewModelBase
    {
        # region Private Properties

        private List<Fiber_K> _dataFiber_Kor;
        private List<Fiber_K> _dataFiber_Kn;
        private Dictionary<string, double> keyValueBL = new Dictionary<string, double>()
        {
            { "BL_05", 0.5 },
            { "BL_1", 1 },
            { "BL_2", 2 },
            { "BL_3", 3 },
            { "BL_5", 5 },
            { "BL_10", 10 },
            { "BL_20", 20 },
            { "BL_21", 21 }

        };






        // геометрия сечения
        /// <summary>
        /// Меньший размер сечения 
        /// </summary>
        private double _h;
        /// <summary>
        /// Больший размер сечения
        /// </summary>
        private double _b;
        /// <summary>
        /// длина фибры
        /// </summary>
        private double _lf;

        /// <summary>
        /// kor – коэффициент ориентации, учитывающий ориентацию фибр в объеме элемента в зависимости 
        /// от соотношения размеров сечения элемента и длины фибры. СП 360
        /// </summary>
        private double _Kor;
        /// <summary>
        /// kn – коэффициент, учитывающий работу фибр в сечении перпендикулярном 
        /// направлению внешнего сжимающего усилия. СП 360
        /// </summary>
        private double _Kn;
        #endregion

        #region Fields
        public double Kor
        {
            get { return _Kor; }
            private set
            {
                _Kor = value;
                OnPropertyChanged();
            }
        }
        public double Kn
        {
            get { return _Kn; }
            private set
            {
                _Kn = value;
                OnPropertyChanged();
            }
        }


        #endregion

        public FiberCoef_K(double h, double b, double lf)
        {
            // Определить данные из бд
            _dataFiber_Kn = BSData.LoadFiber_Kn();
            _dataFiber_Kor = BSData.LoadFiber_Kor();


            ValidateData();

            _h = h;
            _b = b;
            _lf = lf;

            // Определить состояние экземпляра
            Calculate_Kor();
            Calculate_Kn();
        }

        /// <summary>
        /// Расcчитать Kn 
        /// </summary>
        private void Calculate_Kn()
        {
            Kn = Math.Round(Calculate_K(_dataFiber_Kn), 3);
        }


        /// <summary>
        /// рассчитать Kor
        /// </summary>
        private void Calculate_Kor()
        {
            Kor = Math.Round(Calculate_K(_dataFiber_Kor), 3);

        }


        /// <summary>
        /// Расcчитать Kn или Kor в зависимсости от hl и bl
        /// </summary>
        /// <param name="dataFiber_K"> Данные из базы</param>
        private double Calculate_K(List<Fiber_K> dataFiber_K)
        {
            double result = 0;

            double hl = _h / _lf;
            double bl = _b / _lf;

            SetSides(ref hl, ref bl);

            int? indexHL = null;
            Fiber_K row = new Fiber_K();

            // Проверить минимальное значение
            if (hl < dataFiber_K[0].HL)
            { indexHL = 0; }
            // Проверить максимальное значение
            else if (hl > dataFiber_K[dataFiber_K.Count - 2].HL)
            { indexHL = dataFiber_K.Count - 1; }
            // Проверить попадание в колонку
            else if (dataFiber_K.FindIndex(x => x.HL == hl) >= 0)
            { indexHL = dataFiber_K.FindIndex(x => x.HL == hl); }

            if (indexHL != null)
            { row = dataFiber_K[(int)indexHL]; }
            // выполнить интерполяцию
            else
            {
                for (int i = 1; i < dataFiber_K.Count - 1; i++)
                {
                    double delta = dataFiber_K[i].HL - hl;
                    if (delta >= 0)
                    {
                        double ro = (hl - dataFiber_K[i - 1].HL) / (dataFiber_K[i].HL - dataFiber_K[i - 1].HL);
                        row.HL = hl;
                        row.BL_05 = dataFiber_K[i - 1].BL_05 + (dataFiber_K[i].BL_05 - dataFiber_K[i - 1].BL_05) * ro;
                        row.BL_1 = dataFiber_K[i - 1].BL_1 + (dataFiber_K[i].BL_1 - dataFiber_K[i - 1].BL_1) * ro;
                        row.BL_2 = dataFiber_K[i - 1].BL_2 + (dataFiber_K[i].BL_2 - dataFiber_K[i - 1].BL_2) * ro;
                        row.BL_3 = dataFiber_K[i - 1].BL_3 + (dataFiber_K[i].BL_3 - dataFiber_K[i - 1].BL_3) * ro;
                        row.BL_5 = dataFiber_K[i - 1].BL_5 + (dataFiber_K[i].BL_5 - dataFiber_K[i - 1].BL_5) * ro;
                        row.BL_10 = dataFiber_K[i - 1].BL_10 + (dataFiber_K[i].BL_10 - dataFiber_K[i - 1].BL_10) * ro;
                        row.BL_20 = dataFiber_K[i - 1].BL_20 + (dataFiber_K[i].BL_20 - dataFiber_K[i - 1].BL_20) * ro;
                        row.BL_21 = dataFiber_K[i - 1].BL_21 + (dataFiber_K[i].BL_21 - dataFiber_K[i - 1].BL_21) * ro;
                        break;
                    }
                }
            }

            var valuesBL = keyValueBL.Values.ToList();
            int? indexBL = null;
            // Проверить минимальное значение
            if (bl < valuesBL[0])
            { indexBL = 0; }
            // Проверить максимальное значение
            else if (bl > valuesBL[valuesBL.Count - 2])
            { indexBL = valuesBL.Count - 1; }
            // Проверить попадание в колонку
            else if (valuesBL.FindIndex(x => x == bl) >= 0)
            { indexBL = valuesBL.FindIndex(x => x == bl); }

            if (indexBL != null)
            {
                string name = keyValueBL.Keys.ToList()[(int)indexBL];
                result = (double)row.GetType().GetProperty(name).GetValue(row);
            }
            else // выполнить интерполяцию
            {
                for (int i = 1; i < valuesBL.Count - 1; i++)
                {
                    double delta = valuesBL[i] - bl;
                    if (delta >= 0)
                    {
                        double ro = (bl - valuesBL[i - 1]) / (valuesBL[i] - valuesBL[i - 1]);

                        string name0 = keyValueBL.Keys.ToList()[i - 1];
                        string name1 = keyValueBL.Keys.ToList()[i];
                        double k0 = (double)row.GetType().GetProperty(name0).GetValue(row);
                        double k1 = (double)row.GetType().GetProperty(name1).GetValue(row);
                        result = k0 + (k1 - k0) * ro;
                        break;
                    }
                }
            }
            return result;
        }


        /// <summary>
        /// Установить ширину сечения
        /// </summary>
        public void SetH(double h)
        {
            _h = h;
            Calculate_Kor();
            Calculate_Kn();
        }


        /// <summary>
        /// Установить ширину сечения
        /// </summary>
        public void SetB(double b)
        {
            _b = b;
            Calculate_Kor();
            Calculate_Kn();
        }


        /// <summary>
        /// Установить длину фибры
        /// </summary>
        public void SetLen_f(double l)
        {
            _lf = l;
            Calculate_Kor();
            Calculate_Kn();
        }


        /// <summary>
        /// установить стороны, bl >= hl 
        /// </summary>
        /// <param name="h"> высота сечения</param>
        /// <param name="b"> ширина сечения</param>
        private void SetSides(ref double hl, ref double bl)
        {
            if (hl > bl)
            {

                double tmpValue = bl;
                bl = hl;
                hl = tmpValue;
            }
        }


        private void ValidateData()
        {
            for (int i = 1; i < keyValueBL.Keys.Count - 1; i++)
            {
                string name = keyValueBL.Keys.ToList()[i];
                //System.Reflection.PropertyInfo prop = _dataFiber_Kn[0].GetType().GetProperty(name);
                System.Reflection.PropertyInfo prop = typeof(Fiber_K).GetProperty(name);
                if (prop == null)
                {
                    MessageBox.Show("Ошибка Загрузки исходных данных");
                    break;
                }
            }
        }
    }
}

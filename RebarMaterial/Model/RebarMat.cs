using BSFiberConcrete.BSRFib.FiberCalculator;
using BSFiberConcrete.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BSFiberConcrete
{
    public class RebarMat : ViewModelBase
    {

        private List<string> _rebarNames = new List<string>() { "A240", "A400", "A500", "B500", "Bp500" };

        private Dictionary<string, double> _rebarHita_1 = new Dictionary<string, double>()
        {
            { "A240", 2.5},
            { "A400", 2.5 },
            { "A500", 2.5 },
            { "B500", 2 },
            { "Bp500",2 }
        };


        /// <summary>
        /// Список классов арматуры
        /// </summary>
        private List<Rebar> _DataRebarType;
        /// <summary>
        /// Полный список всех диаметров армату 
        /// </summary>
        private List<RebarDiameters> _DataRebarDiameters;

        /// <summary>
        /// Список диаметров для указанного класса арматуры
        /// </summary>
        private List<RebarDiameters> _selectedDiameters;


        /// <summary>
        /// Индекс арматуры из табдлицы _DataRebarType
        /// </summary>
        private int _indexTypeRebar;
        /// <summary>
        /// Индекс диаметра из _selectedDiameters
        /// </summary>
        private int _indexRebarDiameter;

        # region private fields
        /// <summary>
        /// Тип арматуры
        /// </summary>
        private string _typeRebar;
        /// <summary>
        /// нормативное значение сопротивления растяжению
        /// </summary>
        private double _Rs_n;
        /// <summary>
        /// Расчетные значения сопротивления арматуры растяжению
        /// </summary>
        private double _Rs;
        /// <summary>
        /// расчетные значения сопротивления арматуры сжатию
        /// </summary>
        private double _Rsc;

        /// <summary>
        /// расчетные значения сопротивления растяжению для предельных состояний второй группы
        /// </summary>
        private double _Rs_ser;
        /// <summary>
        /// расчетные значения сопротивления поперечной арматуры растяжению
        /// для предельных состояний первой группы,
        /// </summary>
        private double _Rsw;

        /// <summary>
        /// Диаметр арматуры
        /// </summary>
        private double _Diameter;
        /// <summary>
        /// Площадь арматуры
        /// </summary>
        private double _Square;
        /// <summary>
        /// периметр сечения арматуры
        /// </summary>
        private double _us;

        /// <summary>
        /// коэффициент, учитывающий влияние вида поверхности арматуры
        /// </summary>
        private double _Hita_1;
        /// <summary>
        /// коэффициент, учитывающий влияние размера диаметра арматуры
        /// </summary>
        private double _Hita_2;

        private List<string> _Diameters;

        #endregion


        #region Properties
        public string TypeRebar
        {
            get { return _typeRebar; }
            private set
            {
                _typeRebar = value;
                OnPropertyChanged();
            }
        }
        public double Rs_n
        {
            get { return _Rs_n; }
            private set
            {
                _Rs_n = value;
                OnPropertyChanged();
            }
        }
        public double Rs
        {
            get { return _Rs; }
            private set
            {
                _Rs = value;
                OnPropertyChanged();
            }
        }

        public double Rsc
        {
            get { return _Rsc; }
            private set
            {
                _Rsc = value;
                OnPropertyChanged();
            }
        }
        public double Rs_ser
        {
            get { return _Rs_ser; }
            private set
            {
                _Rs_ser = value;
                OnPropertyChanged();
            }
        }
        public double Rsw
        {
            get { return _Rsw; }
            private set
            {
                _Rsw = value;
                OnPropertyChanged();
            }
        }
        public double Diameter
        {
            get { return _Diameter; }
            private set
            {
                _Diameter = value;
                OnPropertyChanged();
            }
        }

        public double Square
        {
            get { return _Square; }
            private set
            {
                _Square = value;
                OnPropertyChanged();
            }
        }
        public double us
        {
            get { return _us; }
            private set
            {
                _us = value;
                OnPropertyChanged();
            }
        }
        public double Hita_1
        {
            get { return _Hita_1; }
            private set
            {
                _Hita_1 = value;
                OnPropertyChanged();
            }
        }
        public double Hita_2
        {
            get { return _Hita_2; }
            private set
            {
                _Hita_2 = value;
                OnPropertyChanged();
            }
        }

        public List<string> Diameters
        {
            get { return _Diameters; }
            private set
            {
                _Diameters = value;
                OnPropertyChanged();
            }
        }

        #endregion 



        public RebarMat()
        {

            LoadRebarDB();
            _DataRebarDiameters = BSData.LoadRebarDiameters();
            
            Diameters = new List<string>();

            SetTypeIndex(0);


        }



        private void LoadRebarDB()
        {
            List<Rebar> tmpList = BSData.LoadRebar();
            _DataRebarType = tmpList.Where(p => _rebarNames.Contains(p.ID)).ToList();
        }



        /// <summary>
        /// Установить индекс класса арматуры
        /// </summary>
        /// <param name="indexType"></param>
        public void SetTypeIndex(int indexType)
        {
            if ((indexType < 0) || indexType > _DataRebarType.Count - 1)
            {
                return;
            }
            _indexTypeRebar = indexType;

            TypeRebar = _DataRebarType[_indexTypeRebar].ID;
            Rs_n = _DataRebarType[_indexTypeRebar].Rsn;
            Rs = _DataRebarType[_indexTypeRebar].Rs;
            Rsc = _DataRebarType[_indexTypeRebar].Rs;
            Rs_ser = _Rs_n;
            Rsw = _DataRebarType[_indexTypeRebar].Rsw_X;
            DefineListDiameters();
            SetDiameterIndex(0);
            CalculateHita_1();
        }

        /// <summary>
        /// Установить индекс диаметра из таблицы _selectedDiameters
        /// </summary>
        /// <param name="index"></param>
        public void SetDiameterIndex(int index)
        {
            if ((index < 0) || index > _selectedDiameters.Count - 1)
            {
                return;
            }
            _indexRebarDiameter = index;
            Diameter = _selectedDiameters[index].Diameter;
            Square = _selectedDiameters[index].Square * 100;// перевод из см в мм 
            us = 2 * Math.PI * Diameter / 2;
            CalculateHita_2();
        }



        private void DefineListDiameters()
        {
            // Индекс для Подобра геометрии (диаметра)
            //int gomertyIndex = _DataFiberKind[_indexFiberKind].IndexForGeometry;

            Diameters.Clear();


            _selectedDiameters = _DataRebarDiameters.Where(p => p.TypeRebar == _typeRebar).ToList();
            if (_selectedDiameters == null)
            {
                _selectedDiameters = new List<RebarDiameters> { };
            }


            foreach (RebarDiameters rebarDiameters in _selectedDiameters)
            {
                Diameters.Add(rebarDiameters.Diameter.ToString());
            }
        }


        private void CalculateHita_2()
        {
            if (_Diameter <= 32)
            {
                Hita_2 = 1;
            }
            else { Hita_2 = 0.9; }
        }


        private void CalculateHita_1()
        { 
            _rebarHita_1.TryGetValue(_typeRebar, out double hita_1);
            Hita_1 = hita_1;
        }



        /// <summary>
        /// Получить Список Классов Арматуры
        /// </summary>
        /// <returns></returns>
        public List<string> GetRebarTypes()
        { 
            List<string> result = new List<string>();
            foreach (Rebar rebarRow in _DataRebarType)
            {
                result.Add(rebarRow.ID);
            }
            return result;
        }


        /// <summary>
        /// Получить список диаметров соответсвующих классу арматуры
        /// </summary>
        /// <returns></returns>
        public List<string> GetDiameters()
        {
            List<string> result = new List<string>();
            foreach (RebarDiameters rebarDiameterRow in _selectedDiameters)
            {
                result.Add(rebarDiameterRow.Diameter.ToString());
            }
            return result;
        }

    }

}

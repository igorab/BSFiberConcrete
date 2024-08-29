using BSFiberConcrete.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFiberConcrete.BSRFib.FiberCalculator
{

    /// <summary>
    /// Класс материала фибры
    /// </summary>
    public class FiberMaterial : ViewModelBase
    {
        # region Privat fields 

        /// <summary>
        /// Таблица из БД с типами фибры и характеристиками, зависящими от фибры
        /// </summary>
        private List<RFiber> _DataTypeFiberMaterial;
        private List<FiberGeometry> _DataFiberGeometry;
        private List<FiberLength2> _DataFiberLength;

        /// <summary>
        /// Список геометрий, соответсвующий указанному материалу фубиы
        /// </summary>
        private List<FiberGeometry> _selectedFiberGeometry;
        /// <summary>
        /// Список длин, соответсвующий материалу и диаметру
        /// </summary>
        private List<FiberLength2> _selectedFiberLength;

        /// <summary>
        /// Номер Фибры из таблицы _DataTypeFiberMaterial
        /// </summary>
        private int _indexTypeFiberMaterial;
        /// <summary>
        /// индекс геометрии из _selectedFiberGeometry
        /// </summary>
        private int _indexGeomerty;
        /// <summary>
        /// Ниндекс длины из _selectedFiberLength
        /// </summary>
        private int _indexLength;

        /// <summary>
        /// Безразмерный коэффициент. Нужен для формулы 8.4, сп 360
        /// </summary>
        private double _coef_C;



        private string _FiberName;
        private double _Rf_ser;
        private double _Rf;
        private double _Ef;
        /// <summary>
        /// η_f коэф. анкеровки фибры
        /// </summary>
        private double _Hita_f;
        /// <summary>
        /// коэффициент условий работы, в зависимсоти от материала фибры 
        /// </summary>
        private double _Gamma_fb1;


        private double _Diameter;
        private double _Square;
        private double _Length;
        #endregion



        #region properties

        public double coef_C { get => _coef_C; }
        public string FiberName
        {
            get => _FiberName;
            private set
            {
                _FiberName = value;
                OnPropertyChanged();
            }
        }
        public double Rf_ser
        {
            get => _Rf_ser;
            private set
            {
                _Rf_ser = value;
                OnPropertyChanged();
            }
        }
        public double Rf
        {
            get => _Rf;
            private set
            {
                _Rf = value;
                OnPropertyChanged();
            }
        }
        public double Ef
        {
            get => _Ef;
            private set
            {
                _Ef = value;
                OnPropertyChanged();
            }
        }
        public double Hita_f
        {
            get => _Hita_f;
            private set
            {
                _Hita_f = value;
                OnPropertyChanged();
            }
        }
        public double Gamma_fb1
        {
            get => _Gamma_fb1;
            private set
            {
                _Gamma_fb1 = value;
                OnPropertyChanged();
            }
        }


        public double Diameter
        {
            get => _Diameter;
            private set
            {
                _Diameter = value;
                OnPropertyChanged();
            }
        }
        public double Square
        {
            get => _Square;
            private set
            {
                _Square = value;
                OnPropertyChanged();
            }
        }
        public double Length
        {
            get => _Length;
            private set
            {
                _Length = value;
                OnPropertyChanged();
            }
        }
        #endregion


        public FiberMaterial()
        {
            _DataTypeFiberMaterial = BSQuery.RFiberLoad();
            _DataFiberGeometry = BSQuery.FiberGeometryLoad();
            _DataFiberLength = BSQuery.FiberLengthLoad();

            // Определяется состояние экземпляра
            SetIndexFiberType(0);
            SetIndexFiberGeometry(0);
            SetIndexFiberLength(0);
            SetCoef_C(0);
        }


        /// <summary>
        /// Установить индекс материала фибры
        /// </summary>
        /// <param name="index"> номер строки из _DataTypeFiberMaterial </param>
        public void SetIndexFiberType(int index)
        {

            if ((index < 0) || (index > _DataTypeFiberMaterial.Count - 1))
            {
                return;
            }

            _indexTypeFiberMaterial = index;

            FiberName = _DataTypeFiberMaterial[_indexTypeFiberMaterial].Name;
            Rf_ser = _DataTypeFiberMaterial[_indexTypeFiberMaterial].Rfser;
            Rf = _DataTypeFiberMaterial[_indexTypeFiberMaterial].Rf;
            Ef = _DataTypeFiberMaterial[_indexTypeFiberMaterial].Ef;
            Hita_f = _DataTypeFiberMaterial[_indexTypeFiberMaterial].Hita_f;
            Gamma_fb1 = _DataTypeFiberMaterial[_indexTypeFiberMaterial].Gamma_fb1;
            DefineListFiberGeometry();
        }



        /// <summary>
        /// установить индекс геометрии (диаметра)
        /// </summary>
        /// <param name="index">номер строки из _selectedFiberGeometry </param>
        public void SetIndexFiberGeometry(int index)
        {

            if ((index < 0) || index > _selectedFiberGeometry.Count - 1)
            {
                return;
            }

            _indexGeomerty = index;
            Diameter = _selectedFiberGeometry[_indexGeomerty].Diameter;
            Square = _selectedFiberGeometry[_indexGeomerty].Square;

            DefineListFiberLength();
        }


        /// <summary>
        /// установить индекс длины фибры (диаметра)
        /// </summary>
        /// <param name="index">номер строки из _selectedFiberLength </param>
        public void SetIndexFiberLength(int index)
        {
            if ((index < 0) || index > _selectedFiberLength.Count - 1)
            {
                return;
            }

            _indexLength = index;
            Length = _selectedFiberLength[_indexLength].Length;

        }


        /// <summary>
        /// установить Коэффициент C
        /// </summary>
        /// <param name="index">номер значения, ЛИБО 1 ЛИБО 0</param>
        public void SetCoef_C(int index)
        {
            // 0 - растяжение
            // 1 - изгиб

            if (index == 0)
            { _coef_C = 1; }
            else
            { _coef_C = 0.6; }
        }

        /// <summary>
        /// Определяется Список доступных диамтеров в зависимости от выбраного материала
        /// (Заполняется свойство _selectedFiberGeometry)
        /// </summary>
        private void DefineListFiberGeometry()
        {
            // Индекс для Подобра геометрии (диаметра)
            int gomertyIndex = _DataTypeFiberMaterial[_indexTypeFiberMaterial].IndexForGeometry;
            _selectedFiberGeometry = _DataFiberGeometry.Where(p => p.GeometryIndex == gomertyIndex).ToList();

            if (_selectedFiberGeometry == null)
            {
                _selectedFiberGeometry = new List<FiberGeometry> { };
            }
        }


        /// <summary>
        /// Определяется Список доступных длин (_selectedFiberGeometry) в зависимости от выбраного материала
        /// </summary>
        private void DefineListFiberLength()
        {
            // Индекс для Подобра списка длин 
            int lengthIndex = _selectedFiberGeometry[_indexGeomerty].IndexForLength;
            _selectedFiberLength = _DataFiberLength.Where(p => p.LenghtIndex == lengthIndex).ToList();
            if (_selectedFiberLength == null)
            {
                _selectedFiberLength = new List<FiberLength2> { };
            }
        }


        /// <summary>
        /// Получить список Типов фибры
        /// </summary>
        /// <returns></returns>
        public List<string> GetFiberTypes()
        {
            List<string> fiberType = new List<string>();

            foreach (RFiber fiberMat in _DataTypeFiberMaterial)
            {
                fiberType.Add(fiberMat.Name);
            }

            return fiberType;
        }


        /// <summary>
        /// Получить список значений диаметров (Площадей) для фибры
        /// </summary>
        /// <returns></returns>
        public List<string> GetFiberGeometries()
        {
            List<string> fiberGeometries = new List<string>();
            foreach (FiberGeometry fiberG in _selectedFiberGeometry)
            {
                if (fiberG.Diameter != 0)
                {
                    fiberGeometries.Add("d=" + fiberG.Diameter.ToString() + " мм");
                }
                else if (fiberG.Square != 0)
                {
                    fiberGeometries.Add("S=" + fiberG.Square.ToString() + " мм2");
                }
                // 
            }
            return fiberGeometries;
        }


        /// <summary>
        /// Получить список значений диаметров для 
        /// </summary>
        /// <returns></returns>
        public List<string> GetFiberLengths()
        {
            List<string> fiberLengths = new List<string>();
            foreach (FiberLength2 fiberL in _selectedFiberLength)
            {
                fiberLengths.Add(fiberL.Length.ToString());
            }
            return fiberLengths;
        }


    }
}

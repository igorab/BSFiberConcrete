using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BSFiberConcrete.Lib;
using MathNet.Numerics;

namespace BSFiberConcrete.BSRFib.FiberCalculator
{
    /// <summary>
    /// Класс для определения характеристик фибробетона по параметрам фибры
    /// </summary>
    public class FiberConcrateCalculator
    {
        private FiberMatrial _fiber;

        private ConcreteMaterial _beton;



        public FiberMatrial Fiber { get => _fiber; }
        public ConcreteMaterial Beton { get => _beton; }


        //private BSMatConcrete _beton;
        //private Beton _beton

        private double _h;
        private double _b;
        private double _mu_fv;


        public FiberConcrateCalculator()
        {

            // Определить класс FiberMatrial
            _fiber = new FiberMatrial();
            // Определить класс Бетон
            _beton = new ConcreteMaterial();

            // Определить класс сечения ?
            // определить тип нагрузки для 

        }


        //public void SetSectionGeometry(double h, double b)
        //{ 
        //    _h = h;
        //    _b = b;
        //}



        public void SetSectionH(double h)
        {
            _h = h;
        }

        public void SetSectionB(double b)
        {
            _b = b;
        }

        public void SetMu_fv(double mu)
        {
            _mu_fv = mu;
        }




        # region Обертки над методами из класса FiberMatrial


        //public List<string> GetListFiberType()
        //{
        //    return _fiber.GetFiberTypes();
        //}


        //public List<string> GetListFiberGeometries()
        //{
        //    return _fiber.GetFiberGeometries();
        //}



        //public List<string> GetListFiberLengths()
        //{
        //    return _fiber.GetListFiberLengths();
        //}

        //public void SetIndexFiberType(int index)
        //{
        //    _fiber.SetIndexFiberType(index);
        //}

        //public void SetIndexFiberGeometry(int index)
        //{
        //    _fiber.SetIndexFiberGeometry(index);
        //}

        //public void SetIndexFiberLength(int index)
        //{
        //    _fiber.SetIndexFiberLength(index);
        //}


        #endregion


    }







    public interface IViewModel
    {
        // Should raise property changed events upon internal properties updating.
        void OnPropertyChanged(string prop);
    }


    /// <summary>
    /// Base/Parent of all ViewModel type classes.
    /// </summary>
    public abstract class ViewModelBase : IViewModel, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notifies listeners of a change to the supplied proerty
        /// </summary>
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        #endregion
    }



    /// <summary>
    /// Класс материала фибры
    /// </summary>
    public class FiberMatrial : ViewModelBase
    {
        # region Privat fields 

        /// <summary>
        /// Таблица из БД с типами фибры и характеристиками, зависящими от фибры
        /// </summary>
        private List<RFiber> _DataTypeFiberMaterial;
        private List<FiberGeometry> _DataFiberGeometry;
        private List<FiberLength2> _DataFiberLength;

        /// <summary>
        /// Парамтеры для типа фибры
        /// </summary>
        //private double _DataParametersOfFiber;
        /// <summary>
        /// Kor
        /// </summary>
        //private double _DataKor;

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


        private double _Geometry;
        private string _NameGeometry;
        private string _DescriptionGeometry;
        private double _Length;
        /// <summary>
        /// Безразмерный коэффициент. Нужен для формулы 8.4, сп 360
        /// </summary>
        private double _coef_C;
        #endregion


        
        #region properties
        public string FiberName { 
            get => _FiberName;
            private set { 
                _FiberName = value;
                OnPropertyChanged();
            }
        }
        public double Rf_ser {
            get => _Rf_ser;
            private set
            {
                _Rf_ser = value;
                OnPropertyChanged();
            }
        }
        public double Rf {
            get => _Rf;
            private set
            {
                _Rf = value;
                OnPropertyChanged();
            }
        }
        public double Ef {
            get => _Ef;
            private set
            {
                _Ef = value;
                OnPropertyChanged();
            }
        }
        public double Hita_f {
            get => _Hita_f;
            private set
            {
                _Hita_f = value;
                OnPropertyChanged();
            }
        }
        public double Gamma_fb1 {
            get => _Gamma_fb1;
            private set
            {
                _Gamma_fb1 = value;
                OnPropertyChanged();
            }
        }


        public double Geometry { 
            get =>_Geometry;
            private set
            {
                _Geometry = value;
                OnPropertyChanged();
            }
        }
        public string NameGeometry { 
            get => _NameGeometry;
            private set
            {
                _NameGeometry = value;
                OnPropertyChanged();
            }
        }
        public string DescriptionGeometry { 
            get => _DescriptionGeometry;
            private set
            {
                _DescriptionGeometry = value;
                OnPropertyChanged();
            }
        }
        public double Length { 
            get => _Length;
            private set
            {
                _Length = value;
                OnPropertyChanged();
            }
        }
        #endregion


        public FiberMatrial()
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

            if ((index < 0) || (index > _DataTypeFiberMaterial.Count-1))
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
            Geometry = _selectedFiberGeometry[_indexGeomerty].Geometry;
            NameGeometry = _selectedFiberGeometry[_indexGeomerty].NameGeometry;
            DescriptionGeometry = _selectedFiberGeometry[_indexGeomerty].DescriptionGeometry;

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
            Length= _selectedFiberLength[_indexLength].Length;

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
            _selectedFiberLength = _DataFiberLength.Where(p => p.LenghtIndex== lengthIndex).ToList();
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
        /// Получить список значений диаметров для 
        /// </summary>
        /// <returns></returns>
        public List<string> GetFiberGeometries()
        {
            List<string> fiberGeometries = new List<string>();
            foreach (FiberGeometry fiberG in _selectedFiberGeometry)
            {
                fiberGeometries.Add(fiberG.NameGeometry + "=" + fiberG.Geometry.ToString());
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


    public class ConcreteMaterial : ViewModelBase
    {

        #region Privat fields 
        private List<Beton> _DateConcreteType;
        private int _indexConcrete;

        private string _BT;

        // нормативные значения
        private double _Rbn;        // МПа
        private double _Rbtn;       // МПа
        // Расчетные значения по первой группе предельных состояний
        private double _Rb;         // МПа
        private double _Rbt;        // МПа
        // Расчетные значения по второй группе предельных состояний
        private double _Rb_ser;     // МПа
        private double _Rbt_ser;    // МПа

        private double _Eb;         // МПа
        #endregion


        #region Properties
        public string BT
        {
            get { return _BT; }
            private set
            {
                _BT = value;
                OnPropertyChanged();
            }
        }
        public double Rbn
        {
            get { return _Rbn; }
            private set
            {
                _Rbn = value;
                OnPropertyChanged();
            }
        }
        public double Rbtn
        {
            get { return _Rbtn; }
            private set
            {
                _Rbtn = value;
                OnPropertyChanged();
            }
        }

        public double Rb
        {
            get { return _Rb; }
            private set
            {
                _Rb = value;
                OnPropertyChanged();
            }
        }
        public double Rbt
        {
            get { return _Rbt; }
            private set
            {
                _Rbt = value;
                OnPropertyChanged();
            }
        }
        public double Rb_ser
        {
            get { return _Rb_ser; }
            private set
            {
                _Rb_ser = value;
                OnPropertyChanged();
            }
        }
        public double Rbt_ser
        {
            get { return _Rbt_ser; }
            private set
            {
                _Rbt_ser = value;
                OnPropertyChanged();
            }
        }
        public double Eb
        {
            get { return _Eb; }
            private set
            {
                _Eb = value;
                OnPropertyChanged();
            }
        }
        #endregion



        public ConcreteMaterial()
        {

            _DateConcreteType = BSData.LoadBetonData();
            SetIndexConcreteType(0);
        }



        /// <summary>
        /// Установить индекс бетона из списка _DateConcreteType
        /// </summary>
        /// <param name="index"></param>
        public void SetIndexConcreteType(int index)
        {
            if ((index< 0) || index> _DateConcreteType.Count - 1)
            {
                return;
            }

            _indexConcrete = index;
            BT = _DateConcreteType[_indexConcrete].BT;
            Rbn = _DateConcreteType[_indexConcrete].Rbn;
            Rbtn = _DateConcreteType[_indexConcrete].Rbtn;
            Rb = _DateConcreteType[_indexConcrete].Rb;
            Rbt = _DateConcreteType[_indexConcrete].Rbt;
            Rb_ser = _DateConcreteType[_indexConcrete].Rbn;
            Rbt_ser = _DateConcreteType[_indexConcrete].Rbtn;
            Eb = _DateConcreteType[_indexConcrete].Eb * 1000;
        }


        /// <summary>
        /// Получить список Классов бетона
        /// </summary>
        /// <returns></returns>
        public List<string> GetFiberTypes()
        {
            List<string> ConcreteType = new List<string>();

            foreach (Beton ConcreteMat in _DateConcreteType)
            {
                ConcreteType.Add(ConcreteMat.BT);
            }

            return ConcreteType;
        }

    }

    /// <summary>
    /// Класс для определение коэф Кor и Kn 
    /// ()
    /// </summary>
    public class FiberCoeff_K : ViewModelBase
    {
        # region Private Properties

        //_data

        // геометрия сечения
        private double _h;
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


        #endregion



    }

}

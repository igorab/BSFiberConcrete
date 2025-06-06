﻿using BSFiberConcrete.Lib;
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
        /// Таблица из БД с видами (конкретный гост или ту) фибры и характеристиками, зависящими от фибры
        /// </summary>
        private List<FiberKind> _DataFiberKind;
        /// <summary>
        /// аблица из БД с типами (более широкая классифиакция чем вид ) фибры и характеристиками, 
        /// </summary>
        private List<FiberType> _DataFiberType;
        private List<FiberGeometry> _DataFiberGeometry;
        private List<FiberLength> _DataFiberLength;

        /// <summary>
        /// Список геометрий, соответсвующий указанному материалу фибиы
        /// </summary>
        private List<FiberGeometry> _selectedFiberGeometry;
        /// <summary>
        /// Список длин, соответсвующий материалу и диаметру
        /// </summary>
        private List<FiberLength> _selectedFiberLength;

        /// <summary>
        /// Номер Фибры из таблицы _DataFiberKind
        /// </summary>
        private int _indexFiberKind;
        /// <summary>
        /// Номер Фибры из таблицы _DataFiberType
        /// </summary>
        private int _indexFiberType;
        /// <summary>
        /// индекс геометрии из _selectedFiberGeometry
        /// </summary>
        private int _indexGeomerty;
        /// <summary>
        /// Индекс длины из _selectedFiberLength
        /// </summary>
        private int _indexLength;
        /// <summary>
        /// Определяет возможен ли пользовательский ввод данных
        /// (диаметр и длина фибры)
        /// </summary>
        private bool _customUserData;

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
            _customUserData = false;
            _DataFiberKind = BSQuery.FiberKindLoad();
            _DataFiberType = BSQuery.FiberTypeLoad();
            _DataFiberGeometry = BSQuery.FiberGeometryLoad();
            _DataFiberLength = BSQuery.FiberLengthLoad();


            // Определяется состояние экземпляра
            SetIndexFiberKind(0);
            SetIndexFiberGeometry(0);
            SetIndexFiberLength(0);
            SetCoef_C(0);
        }

        /// <summary>
        /// создается объект с Возможность пользовательского ввода характеристик фибры
        /// </summary>
        /// <param name="customUserData"></param>
        public FiberMaterial(bool customUserData)
        {
            _customUserData = customUserData;
            _DataFiberKind = BSQuery.FiberKindLoad();
            _DataFiberType = BSQuery.FiberTypeLoad();
            _DataFiberGeometry = BSQuery.FiberGeometryLoad();
            _DataFiberLength = BSQuery.FiberLengthLoad();

            // Определяется состояние экземпляра
            SetIndexFiberType(0, customUserData);
            // рандомные заначения, 
            Rf = 440;
            Rf_ser = 460;
            Ef = 210000;
            Diameter = 0.3;
            Length = 20;

            SetCoef_C(0);
        }


        /// <summary>
        /// Установить индекс вид фибры
        /// </summary>
        /// <param name="index"> номер строки из _DataFiberKind </param>
        public void SetIndexFiberKind(int index)
        {

            if ((index < 0) || (index > _DataFiberKind.Count - 1))
            {
                return;
            }

            _indexFiberKind = index;

            FiberName = _DataFiberKind[_indexFiberKind].Name;
            Rf_ser = _DataFiberKind[_indexFiberKind].Rfser;
            Rf = _DataFiberKind[_indexFiberKind].Rf;
            Ef = _DataFiberKind[_indexFiberKind].Ef;
            
            SetIndexFiberType(_DataFiberKind[index].TypeID);

            DefineListFiberGeometry();


        }


        /// <summary>
        /// установить индекс типа фибры
        /// </summary>
        /// <param name="index">номер из таблицы _DataFiberType </param>
        /// <param name="customUserData">необязательный аргумент, передается в случае пользовательского ввода</param>
        public void SetIndexFiberType(int index, bool? customUserData = null)
        {
            if ((index < 0) || (index > _DataFiberType.Count - 1))
            {
                return;
            }

            _indexFiberType = index;
            Hita_f = _DataFiberType[_indexFiberType].Hita_f;
            Gamma_fb1 = _DataFiberType[_indexFiberType].Gamma_fb1;

            if (customUserData == null)
            { _customUserData = false; }
            else
            { 
                _customUserData = (bool)customUserData;
                FiberName = _DataFiberType[_indexFiberType].Name;
            }
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
        /// Установить Rf_ser, только для случая пользовательского ввода данных
        /// </summary>
        /// <param name="r_f_ser"></param>
        public void Set_Rf_ser(double r_f_ser)
        {
            Rf_ser = r_f_ser;
        }
        /// <summary>
        /// Установить Rf, только для случая пользовательского ввода данных
        /// </summary>
        /// <param name="r_f"></param>
        public void Set_Rf(double r_f)
        {
            Rf = r_f;
        }
        /// <summary>
        /// Установить Ef, только для случая пользовательского ввода данных
        /// </summary>
        /// <param name="e_f"></param>
        public void Set_Ef(double e_f)
        {
            Ef = e_f;
        }
        /// <summary>
        /// Установить Diameter, только для случая пользовательского ввода данных
        /// </summary>
        /// <param name="diameter"></param>
        public void Set_Diameter(double diameter)
        {
            Diameter = diameter;
        }
        /// <summary>
        /// Установить Square, только для случая пользовательского ввода данных
        /// </summary>
        /// <param name="s"></param>
        public void Set_Square(double s)
        {
            Square = s;
        }
        /// <summary>
        /// Установить Length, только для случая пользовательского ввода данных
        /// </summary>
        /// <param name="len"></param>
        public void Set_Length(double len)
        {
            Length = len;
        }


        /// <summary>
        /// Определяется Список доступных диамтеров в зависимости от выбраного материала
        /// (Заполняется свойство _selectedFiberGeometry)
        /// </summary>
        private void DefineListFiberGeometry()
        {
            // Индекс для Подобра геометрии (диаметра)
            int gomertyIndex = _DataFiberKind[_indexFiberKind].IndexForGeometry;
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
                _selectedFiberLength = new List<FiberLength> { };
            }
        }


        /// <summary>
        /// Получить список Типов фибры
        /// </summary>
        /// <returns></returns>
        public List<string> GetFiberKind()
        {
            List<string> fiberKind = new List<string>();
            foreach (FiberKind fiberMat in _DataFiberKind)
            { fiberKind.Add(fiberMat.Name); }
            return fiberKind;
        }



        /// <summary>
        /// Получить список видов фибры
        /// </summary>
        /// <returns></returns>
        public List<string> GetFiberType()
        {
            List<string> fiberType = new List<string>();
            foreach (FiberType fiberMat in _DataFiberType)
            { fiberType.Add(fiberMat.Name); }
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
            foreach (FiberLength fiberL in _selectedFiberLength)
            {
                fiberLengths.Add(fiberL.Length.ToString());
            }
            return fiberLengths;
        }


    }
}

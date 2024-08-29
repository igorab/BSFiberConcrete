using System;
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



        #region Private Fields for Bindings
        private double _l_f_an;
        private double _mu_fv_min;
        private double _C_max;
        private double _l_f_min;
        private double _R_fbt3;
        private double _R_fb;
        private double _mu_fa;
        private double _mu_1_fa;
        private double _E_fb;
        private double _G_fb;
        private string _message;

        #endregion

        #region Properties

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

        /// <summary>
        /// Производится расчет характеристик фибры
        /// </summary>
        public void Calculate()
        {
            
            double S;
            if (Fiber.Diameter != 0)
                S = Math.PI * Math.Pow(Fiber.Diameter,2) / 4;
            else
                S = Fiber.Square;
            double d_f_red = 1.13 * Math.Sqrt(S);

            double mu_fv;
            // Коэф для расчета R_fbt3
            double Kt;
            // Коэф эффективного косвенного армирования фибрами
            double fi_f;


            // Длина заделки фибры в бетоне
            l_f_an = 0;
            // минимальное значение коэффициента фибрового армирования
            mu_fv_min = 0;
            // Максимальный размер зерен крупного заполнителя
            C_max = 0;
            // минимальное значение длины фибры
            l_f_min = 0;
            // Сопротивление растяжения стальфибробетона  
            R_fbt3 = 0;
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

            // мм
            l_f_an = Fiber.Hita_f * d_f_red * Fiber.Rf_ser / Beton.Rb_ser;
            if (l_f_an >= Fiber.Length)
            {
                message = "Расчетное значение \"Длина заделки фибры в бетоне\" не должно превышать значение \"Длина фибры\"."; 
                return; 
            }


            mu_fv_min = 1.5 * Fiber.coef_C * Beton.Rbt / (Fiber.Rf * Math.Pow(FiberCoef.Kor, 2) *
                (1 - 30 / Fiber.Rf - l_f_an / Fiber.Length));
            // сравнить mu_fv_min с верхней границей диапазона 
            if (mu_fv_min > 0.02)
            {
                message = "Расчетная величина \"Минимаьное значение коэф. фибрового армирования\" не должно превышать 0.002.";
                return;
            }

            // мм
            C_max = 5.3 * Math.Pow(d_f_red * d_f_red * Fiber.Length / (100 * mu_fv_min), 1.0 / 3);

            // мм
            l_f_min = 2.5 * C_max;
            // сравнить длину фибры с минимальным значением
            if (l_f_min > Fiber.Length)
            {
                message = "Расчетная величина \"Минимальное значение длины фибры\" не должно превышать значение \"Длина фибры\".";
                return;
            }

            // назначение расчетного коэф фибрового армирования
            //  если mu_fv = 0 выполнять расчет по минимальному значению
            if (_mu_fv == 0)
            { 
                mu_fv = mu_fv_min; 
            }
            else
            { 
                mu_fv = _mu_fv;
                if (mu_fv_min > 0.02)
                {
                    message = "Значение Коэф. фибрового армирования не должно превышать 0.02.";
                    return;
                }

            }
            // сравнить mu_fv с верхней нижней диапазона 
            if (mu_fv < 0.005)
            {
                message = "Значение Коэф. фибрового армирования должно быть больше 0.005.";
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
                return;
            }

            try
            {
                BSRFibLabReport labReport = new BSRFibLabReport();

                labReport.ReportName = "Определение характеристик фибробетона";
                labReport.SampleDescr = "";
                labReport.SampleName = "";

                Dictionary<string, string> LabItems = new Dictionary<string, string>()
                {
                    ["Длина заделки фибры в бетоне l_f,an [мм]"] = Convert.ToString(Math.Round(l_f_an,3)),
                    ["Минимальное значение коэф. фибрового армирования μfv_min "] = Convert.ToString(Math.Round(mu_fv_min,4)),
                    ["Максимальный размер зерен крупного заполнителя C_max [мм]"] = Convert.ToString(Math.Round(C_max,3)),
                    ["Минимальное значение длины фибры l_f_min [мм]"] = Convert.ToString(Math.Round(l_f_min,3)),
                    ["Остаточное сопротивление растяжение Rfbt3 [МПа]"] = Convert.ToString(Math.Round(R_fbt3,6)),
                    ["Сопротивление сжатия Rfb [МПа]"] = Convert.ToString(Math.Round(R_fb,6)),
                    ["Коэф. фибрового армирования по площади (растянутой зоны) μ_fa"] = Convert.ToString(Math.Round(mu_fa,4)),
                    ["Коэф. фибрового армирования по площади (сжатой зоны) μ'_fa"] = Convert.ToString(Math.Round(mu_1_fa, 4)),
                    ["Модуль упругости Efb [МПа]"] = Convert.ToString(Math.Round(E_fb,6)),
                    ["Модуль сдвига Gfb [МПа]"] = Convert.ToString(Math.Round(G_fb,6))
                };

                labReport.LabItems = LabItems;
                labReport.RunReport();
            }
            catch (Exception _ex)
            {
                MessageBox.Show(_ex.Message);
            }



        }


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
        public double Length { 
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
            Kn = Math.Round(Calculate_K(_dataFiber_Kn),3);
        }


        /// <summary>
        /// рассчитать Kor
        /// </summary>
        private void Calculate_Kor()
        {
            Kor = Math.Round(Calculate_K(_dataFiber_Kor),3);
            
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
            { indexBL = 0;  }
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

using BSFiberConcrete.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFiberConcrete.BSRFib.FiberCalculator
{
    public class ConcreteMaterial : ViewModelBase
    {

        #region Privat fields 
        /// <summary>
        /// Данные по бетону из бд
        /// </summary>
        private List<Beton> _DateConcreteDB;
        /// <summary>
        /// Данные по Типу бетона
        /// </summary>
        private List<BetonType> _DateConcreteTypeDB;

        /// <summary>
        /// Характеристика бетона для указанного ТИПА 
        /// </summary>
        private List<Beton> _DataConcreteOfKind;
        /// <summary>
        /// Индек класса бетона из таблицы _DataConcreteOfKind
        /// </summary>
        private int _indexConcreteClass;
        /// <summary>
        /// Индекс Типа бетона (тяжелый, мелкозернистый A)
        /// </summary>
        private int _indexConcreteType;

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

        // название типа бетона (тяжелый мелкозернистый и тп)
        private string _nameConcreteType;
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

        public string NameConcreteType
        {
            get { return _nameConcreteType; }
            private set
            {
                _nameConcreteType = value;
                OnPropertyChanged();
            }
        }
        #endregion



        public ConcreteMaterial()
        {
            _DateConcreteDB = BSData.LoadBetonData();
            _DateConcreteTypeDB = BSQuery.LoadBetonType();


            _DateConcreteTypeDB = _DateConcreteTypeDB.GetRange(0, 3);


            SetIndexConcretKind(0);
            SetIndexConcreteClass(0);
        }



        /// <summary>
        /// установить вид бетона
        /// </summary>
        /// <param name="index"> значения 1 - 4 </param>
        public void SetIndexConcretKind(int index)
        {
            if ((index < 0) || index > _DateConcreteTypeDB.Count - 1)
            {
                return;
            }

            _indexConcreteType = index;
            NameConcreteType = _DateConcreteTypeDB[index].Name;
            _DataConcreteOfKind = _DateConcreteDB.Where(p => p.BetonType == index).ToList();

        }


        /// <summary>
        /// Установить индекс бетона из списка _DateConcreteDB
        /// </summary>
        /// <param name="index"></param>
        public void SetIndexConcreteClass(int index)
        {
            if ((index < 0) || index > _DataConcreteOfKind.Count - 1)
            {
                return;
            }

            _indexConcreteClass = index;
            BT = _DataConcreteOfKind[_indexConcreteClass].BT;
            Rbn = _DataConcreteOfKind[_indexConcreteClass].Rbn;
            Rbtn = _DataConcreteOfKind[_indexConcreteClass].Rbtn;
            Rb = _DataConcreteOfKind[_indexConcreteClass].Rb;
            Rbt = _DataConcreteOfKind[_indexConcreteClass].Rbt;
            Rb_ser = _DataConcreteOfKind[_indexConcreteClass].Rbn;
            Rbt_ser = _DataConcreteOfKind[_indexConcreteClass].Rbtn;
            Eb = _DataConcreteOfKind[_indexConcreteClass].Eb * 1000;
        }


        /// <summary>
        /// Получить список Классов бетона
        /// </summary>
        /// <returns></returns>
        public List<string> GetConcreteClass()
        {
            List<string> ConcreteClass = new List<string>();
            foreach (Beton Concrete in _DataConcreteOfKind)
            {
                ConcreteClass.Add(Concrete.BT);
            }

            return ConcreteClass;
        }

        /// <summary>
        /// Получить список Типов бетона
        /// </summary>
        /// <returns></returns>
        public List<string> GetConcreteType()
        {
            List<string> ConcreteType = new List<string>();

            foreach (BetonType typeB in _DateConcreteTypeDB)
            {
                ConcreteType.Add(typeB.Name);
            }
            return ConcreteType;
        }


        /// <summary>
        /// Оценить C_max, и вернуть предупреждение при необходимости
        /// </summary>
        /// <returns></returns>
        public string Evaluate_Cmax(double C_max)
        { 
            string result = null;

            switch (_indexConcreteType)
            {
                case 0 :
                    {
                        if (C_max < 10)
                        {
                            result = "Т.к. C_max<10, рекомендуется выбрать мелкозернистый бетон";
                        }
                        break;
                    }
                case 1 :
                case 2 :
                    {
                        if (C_max >= 10)
                        {
                            result = "Т.к. C_max≥10, рекомендуется выбрать тяжелый бетон";
                        }
                        break;
                    }
            }
            return result;

        }

    }
}

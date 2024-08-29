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
            if ((index < 0) || index > _DateConcreteType.Count - 1)
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
}

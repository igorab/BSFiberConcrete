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
                                        private List<Beton> _DateConcreteDB;
                                private List<BetonType> _DateConcreteTypeDB;
                                private List<Beton> _DataConcreteOfKind;
                                private int _indexConcreteClass;
                                private int _indexConcreteType;
        private string _BT;
                private double _Rbn;                private double _Rbtn;                       private double _Rb;                 private double _Rbt;                        private double _Rb_ser;             private double _Rbt_ser;    
        private double _Eb;         
                private string _nameConcreteType;
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
                public ConcreteMaterial()
        {
            _DateConcreteDB = BSData.LoadBetonData();
            _DateConcreteTypeDB = BSQuery.LoadBetonType();
            _DateConcreteTypeDB = _DateConcreteTypeDB.GetRange(0, 3);
            SetIndexConcretKind(0);
            SetIndexConcreteClass(0);
        }
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
                                        public List<string> GetConcreteClass()
        {
            List<string> ConcreteClass = new List<string>();
            foreach (Beton Concrete in _DataConcreteOfKind)
            {
                ConcreteClass.Add(Concrete.BT);
            }
            return ConcreteClass;
        }
                                        public List<string> GetConcreteType()
        {
            List<string> ConcreteType = new List<string>();
            foreach (BetonType typeB in _DateConcreteTypeDB)
            {
                ConcreteType.Add(typeB.Name);
            }
            return ConcreteType;
        }
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

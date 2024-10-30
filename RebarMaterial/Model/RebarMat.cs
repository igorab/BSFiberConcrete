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
                                private List<Rebar> _DataRebarType;
                                private List<RebarDiameters> _DataRebarDiameters;
                                private List<RebarDiameters> _selectedDiameters;
                                private int _indexTypeRebar;
                                private int _indexRebarDiameter;
                                        private string _typeRebar;
                                private double _Rs_n;
                                private double _Rs;
                                private double _Rsc;
                                private double _Rs_ser;
                                        private double _Rsw;
                                private double _Diameter;
                                private double _Square;
                                private double _us;
                                private double _Hita_1;
                                private double _Hita_2;
        private List<string> _Diameters;
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
                                        public void SetDiameterIndex(int index)
        {
            if ((index < 0) || index > _selectedDiameters.Count - 1)
            {
                return;
            }
            _indexRebarDiameter = index;
            Diameter = _selectedDiameters[index].Diameter;
            Square = _selectedDiameters[index].Square * 100;            us = 2 * Math.PI * Diameter / 2;
            CalculateHita_2();
        }
        private void DefineListDiameters()
        {
                        
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
                                        public List<string> GetRebarTypes()
        { 
            List<string> result = new List<string>();
            foreach (Rebar rebarRow in _DataRebarType)
            {
                result.Add(rebarRow.ID);
            }
            return result;
        }
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

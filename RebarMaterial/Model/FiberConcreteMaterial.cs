using BSFiberConcrete.BSRFib.FiberCalculator;
using BSFiberConcrete.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace BSFiberConcrete
{
    public class FiberConcreteMaterial : ViewModelBase
    {
        private List<FiberConcreteClass> _fiberConcreteClass;

        private int _fiberConcreteClassIndex;

        private double _Gamma_ft = 1.3;

        private string _Bft;
        private double _Rfbt_n;
        private double _Rfbt;

        public string Bft
        {
            get { return _Bft; }
            private set
            {
                _Bft = value;
                OnPropertyChanged();
            }
        }
        public double Rfbt_n
        {
            get { return _Rfbt_n; }
            private set
            {
                _Rfbt_n = value;
                OnPropertyChanged();
            }
        }
        public double Rfbt
        {
            get { return _Rfbt; }
            private set
            {
                _Rfbt = value;
                OnPropertyChanged();
            }
        }


        public FiberConcreteMaterial()
        {
            _fiberConcreteClass = BSData.LoadFiberConcreteClass();
            Set_Bft(0);
        }


                                        public void Set_Bft(int index)
        {
            if ((index < 0) || index > _fiberConcreteClass.Count - 1)
            {
                return;
            }

            _fiberConcreteClassIndex = index;
            Bft = _fiberConcreteClass[_fiberConcreteClassIndex].Bft;
            Rfbt_n = _fiberConcreteClass[_fiberConcreteClassIndex].Rfbt_n;
            Rfbt = Math.Round(Rfbt_n / _Gamma_ft,3);

        }

                                        public List<string> Get_Bft()
        { 
            List<string> result = new List<string>();
            foreach (FiberConcreteClass fc in _fiberConcreteClass)
            {
                result.Add(fc.Bft);
            }
            return result;
        }
    }
}

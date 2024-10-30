using BSFiberConcrete.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFiberConcrete.BSRFib.FiberCalculator
{

                public class FiberMaterial : ViewModelBase
    {
        # region Privat fields 

                                private List<FiberKind> _DataFiberKind;
                                private List<FiberType> _DataFiberType;
        private List<FiberGeometry> _DataFiberGeometry;
        private List<FiberLength> _DataFiberLength;

                                private List<FiberGeometry> _selectedFiberGeometry;
                                private List<FiberLength> _selectedFiberLength;

                                private int _indexFiberKind;
                                private int _indexFiberType;
                                private int _indexGeomerty;
                                private int _indexLength;
                                        private bool _customUserData;

                                private double _coef_C;



        private string _FiberName;
        private double _Rf_ser;
        private double _Rf;
        private double _Ef;
                                private double _Hita_f;
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


                        SetIndexFiberKind(0);
            SetIndexFiberGeometry(0);
            SetIndexFiberLength(0);
            SetCoef_C(0);
        }

                                        public FiberMaterial(bool customUserData)
        {
            _customUserData = customUserData;
            _DataFiberKind = BSQuery.FiberKindLoad();
            _DataFiberType = BSQuery.FiberTypeLoad();
            _DataFiberGeometry = BSQuery.FiberGeometryLoad();
            _DataFiberLength = BSQuery.FiberLengthLoad();

                        SetIndexFiberType(0, customUserData);
                        Rf = 440;
            Rf_ser = 460;
            Ef = 210000;
            Diameter = 0.3;
            Length = 20;

            SetCoef_C(0);
        }


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



                                        public void SetIndexFiberLength(int index)
        {
            if ((index < 0) || index > _selectedFiberLength.Count - 1)
            {
                return;
            }

            _indexLength = index;
            Length = _selectedFiberLength[_indexLength].Length;

        }


                                        public void SetCoef_C(int index)
        {
                        
            if (index == 0)
            { _coef_C = 1; }
            else
            { _coef_C = 0.6; }
        }


                                        public void Set_Rf_ser(double r_f_ser)
        {
            Rf_ser = r_f_ser;
        }
                                        public void Set_Rf(double r_f)
        {
            Rf = r_f;
        }
                                        public void Set_Ef(double e_f)
        {
            Ef = e_f;
        }
                                        public void Set_Diameter(double diameter)
        {
            Diameter = diameter;
        }
                                        public void Set_Square(double s)
        {
            Square = s;
        }
                                        public void Set_Length(double len)
        {
            Length = len;
        }


                                        private void DefineListFiberGeometry()
        {
                        int gomertyIndex = _DataFiberKind[_indexFiberKind].IndexForGeometry;
            _selectedFiberGeometry = _DataFiberGeometry.Where(p => p.GeometryIndex == gomertyIndex).ToList();

            if (_selectedFiberGeometry == null)
            {
                _selectedFiberGeometry = new List<FiberGeometry> { };
            }
        }


                                private void DefineListFiberLength()
        {
                        int lengthIndex = _selectedFiberGeometry[_indexGeomerty].IndexForLength;
            _selectedFiberLength = _DataFiberLength.Where(p => p.LenghtIndex == lengthIndex).ToList();
            if (_selectedFiberLength == null)
            {
                _selectedFiberLength = new List<FiberLength> { };
            }
        }


                                        public List<string> GetFiberKind()
        {
            List<string> fiberKind = new List<string>();
            foreach (FiberKind fiberMat in _DataFiberKind)
            { fiberKind.Add(fiberMat.Name); }
            return fiberKind;
        }



                                        public List<string> GetFiberType()
        {
            List<string> fiberType = new List<string>();
            foreach (FiberType fiberMat in _DataFiberType)
            { fiberType.Add(fiberMat.Name); }
            return fiberType;
        }


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
                            }
            return fiberGeometries;
        }


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

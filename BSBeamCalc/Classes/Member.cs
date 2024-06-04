using System;
using System.Collections.Generic;
using System.Drawing;

namespace CBAnsDes
{

    [Serializable()]
    public class Member
    {
        private float _E;
        private float _I;
        private float _g;
        private float _spanlength;
        private Rectangle _rect;

        [Serializable()]
        public struct P
        {
            private float _pload;
            private float _pdist;
            private Rectangle _rect;
            public float pload
            {
                get
                {
                    return _pload;
                }
                set
                {
                    _pload = value;
                }
            }
            public float pdist
            {
                get
                {
                    return _pdist;
                }
                set
                {
                    _pdist = value;
                }
            }
            public Rectangle rect
            {
                get
                {
                    return _rect;
                }
                set
                {
                    _rect = value;
                }
            }
        }
        [Serializable()]
        public struct U
        {
            private float _uload1;
            private float _uload2;
            private float _udist1;
            private float _udist2;
            private Rectangle _rect;
            public float uload1
            {
                get
                {
                    return _uload1;
                }
                set
                {
                    _uload1 = value;
                }
            }
            public float uload2
            {
                get
                {
                    return _uload2;
                }
                set
                {
                    _uload2 = value;
                }
            }
            public float udist1
            {
                get
                {
                    return _udist1;
                }
                set
                {
                    _udist1 = value;
                }
            }
            public float udist2
            {
                get
                {
                    return _udist2;
                }
                set
                {
                    _udist2 = value;
                }
            }
            public Rectangle rect
            {
                get
                {
                    return _rect;
                }
                set
                {
                    _rect = value;
                }
            }
        }
        [Serializable()]
        public struct M
        {
            private float _mload;
            private float _mdist;
            private Rectangle _rect;
            public float mload
            {
                get
                {
                    return _mload;
                }
                set
                {
                    _mload = value;
                }
            }
            public float mdist
            {
                get
                {
                    return _mdist;
                }
                set
                {
                    _mdist = value;
                }
            }
            public Rectangle rect
            {
                get
                {
                    return _rect;
                }
                set
                {
                    _rect = value;
                }
            }
        }

        private List<P> _pload = new List<P>();
        private List<U> _uload = new List<U>();
        private List<M> _mload = new List<M>();

        public bool[] DOF = new bool[4];
        public float[] FER = new float[4];
        public float[,] stiff = new float[4, 4];
        public float[] RES = new float[4];
        public float[] DISP = new float[4];

        private List<float> _Fshear;
        private List<float> _Bmoment;
        private List<float> _VDeflection;
        private List<float> _slope;

        public float spanlength
        {
            get
            {
                return _spanlength;
            }
            set
            {
                _spanlength = value;
            }
        }

        public float Emodulus
        {
            get
            {
                return _E;
            }
            set
            {
                _E = value;
            }
        }

        public float Inertia
        {
            get
            {
                return _I;
            }
            set
            {
                _I = value;
            }
        }

        public float g
        {
            get
            {
                return _g;
            }
            set
            {
                _g = value;
            }
        }

        public Rectangle rect
        {
            get
            {
                return _rect;
            }
            set
            {
                _rect = value;
            }
        }

        public List<P> Pload
        {
            get
            {
                return _pload;
            }
            set
            {
                _pload = value;
            }
        }

        public List<U> Uload
        {
            get
            {
                return _uload;
            }
            set
            {
                _uload = value;
            }
        }

        public List<M> Mload
        {
            get
            {
                return _mload;
            }
            set
            {
                _mload = value;
            }
        }

        public List<float> Fshear
        {
            get
            {
                return _Fshear;
            }
            set
            {
                _Fshear = value;
            }
        }

        public List<float> Bmoment
        {
            get
            {
                return _Bmoment;
            }
            set
            {
                _Bmoment = value;
            }
        }

        public List<float> VDeflection
        {
            get
            {
                return _VDeflection;
            }
            set
            {
                _VDeflection = value;
            }
        }

        public List<float> Slope
        {
            get
            {
                return _slope;
            }
            set
            {
                _slope = value;
            }
        }
    }
}
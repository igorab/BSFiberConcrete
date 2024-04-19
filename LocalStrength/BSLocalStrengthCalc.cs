using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFiberConcrete.LocalStrength
{
    public class BSLocalStrengthCalc
    {
        protected List<LocalStress> m_DS;
        public List<LocalStress> GetDS => m_DS;


        protected double a1;
        protected double a2;
        protected double c;
        protected double a;

        protected double Yb1;
        protected double Yb2;
        protected double Yb3;
        protected double Yb5;
        protected double Yft;
        protected double Yb;
        protected double Rfb;
        protected double Rfbn;

        protected double Rfbtn;
        protected double Rfbt;

        public virtual string ReportName() => "";
        public virtual string SampleDescr() => "";
        public virtual string SampleName() => "";

        public BSLocalStrengthCalc()
        {
            //(a1, a2, c, Yb1, Yb2, Yb3, Yb5) = (20, 30, 15, 0.9, 0.9, 1, 1);

        }

        public virtual void InitDataSource()
        {
            m_DS = new List<LocalStress>();
        }


        public virtual bool RunCalc()
        {
            return false;
        }

        public virtual double AfbLoc(int scheme)
        {
            double _al = 0;

            switch (scheme)
            {
                case 1:
                    _al = a1 * a2;
                    break;
                case 2:
                    _al = a1 * a2 - 2 * a * a1;
                    break;
                case 3:
                    _al = a1 * a2 - 2 * a * a1 - a * a2;
                    break;
                case 4:
                    _al = a1 * a2 - a * a1 - a * a2;
                    break;                    
                case 5:
                    _al = a1 * a2 - a * a1;
                    break;
                case 6:
                    if (c < a1)
                        if (c < a)
                            _al = a1 * a2 - a1 * (a-c);
                        else
                            _al = a1 * a2;
                    else
                        _al = a1 * a2;
                    break;
            }

            return _al;
        }

        public virtual double AfbMax(int scheme)
        {
            double _am = 0;

            switch (scheme)
            {
                case 1:
                    _am = (2*a2 + a1) * (2 * a1 + a2);
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
            }

            return _am;
        }

        public virtual double Lx(int scheme)
        {
            double _lx = 0;

            switch (scheme)
            {
                case 1:
                    _lx = 2*a2 + a1;
                    break;
                case 2:
                    _lx = 2 * a2 + a1;
                    break;
                case 3:
                    _lx = a2 + a1 - a;
                    break;
                case 4:
                    _lx = a1 + a2 - a;
                    break;
                case 5:
                    _lx = a1 + 2 * a2;
                    break;
                case 6:
                    _lx = 2 * a2 + a1;
                    break;
            }
            return _lx;
        }

        public virtual double Ly(int scheme)
        {
            double _ly = 0;

            switch (scheme)
            {
                case 1:
                    _ly = 2 * a1 + a2;
                    break;
                case 2:
                    _ly = a2 - 2*a;
                    break;
                case 3:
                    _ly = a2 - 2 * a;
                    break;
                case 4:
                    _ly = a2 + a1 - a;
                    break;
                case 5:
                    _ly = a2 - a;
                    break;
                case 6:
                    if (c < a1)
                        if (c < a)
                            _ly = 2  * c + a2 - a;
                        else
                            _ly = 2 * c + a2 - a;
                    else
                        _ly = 2*a1  + a2;
                    break;
            }
            return _ly;
        }


    }
}

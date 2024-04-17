using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFiberConcrete.LocalStrength
{
    public class BSLocalStrengthCalc
    {
        protected double a1;
        protected double a2;
        protected double c;

        protected double Yb1;
        protected double Yb2;
        protected double Yb3;
        protected double Yb5;


        protected double Rfb;
        protected double Rfbn;

        protected double Rfbtn;
        protected double Rfbt;

        public BSLocalStrengthCalc()
        {
            (a1, a2, c, Yb1, Yb2, Yb3, Yb5) = (20, 30, 15, 0.9, 0.9, 1, 1);

        }


        public virtual void RunCalc()
        {
            
        }

    }
}

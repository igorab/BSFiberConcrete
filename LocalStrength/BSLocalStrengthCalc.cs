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

        protected double Yb1;
        protected double Yb2;
        protected double Yb3;
        protected double Yb5;
        protected double Yft;

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

    }
}

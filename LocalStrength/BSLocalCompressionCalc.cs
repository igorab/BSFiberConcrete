using BSFiberConcrete.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFiberConcrete.LocalStrength
{
    public class BSLocalCompressionCalc : BSLocalStrengthCalc
    {
        private double psi;
        private double Afbloc;
        private double Afbmax;
        private double fi_fb;
        private double Rfbloc;
        private double Nult;

        public override void InitDataSource()
        {
            m_DS = BSData.LoadLocalStress();

            Dictionary<string, double> D = new Dictionary<string, double>();
            foreach (var item in m_DS) D[item.VarName] = item.Value;

            (a1, a2, c, Yb1, Yb2, Yb3, Yb5, Rfbn, Yb, Rfbt, psi, Afbloc, Afbmax, Rfb, fi_fb, Rfbloc, Nult) =
                (D["a1"], D["a2"], D["c"], D["Yb1"], D["Yb2"], D["Yb3"], D["Yb5"], D["Rfbn"], D["Yb"], 
                 D["Rfbt"], D["psi"], D["Afbloc"], D["Afbmax"], D["Rfb"], D["fi_fb"], D["Rfbloc"], D["Nult"]);

        }

        public override string ReportName()
        {
            return "Местное сжатие";
        }

        public override bool RunCalc()
        {
            (Afbloc, Afbmax, Rfb, fi_fb, Rfbloc, Nult) = (0, 0, 0, 0, 0, 0);

            bool ok = base.RunCalc();

            try
            {
                // Площадь смятия, см2
                Afbloc = a1 * a2;
                
                // максимальная расчетная площадь
                Afbmax = 6400;

                //Расчетные значения сопротивления  на сжатиие по B30 СП63
                Rfb = Rfbn /Yb * Yb1 * Yb2 * Yb3 * Yb5;

                fi_fb = 0.8 * Math.Sqrt( Afbmax / Afbloc );

                if (fi_fb > 2.5)
                    fi_fb = 2.5;
                else if (fi_fb < 1)
                    fi_fb = 1.0;

                Rfbloc = fi_fb * Rfb;

                Nult = psi * Rfbloc * Afbloc;

                Dictionary<string, double> D = new Dictionary<string, double>()
                {
                    ["Afbloc"] = Afbloc,
                    ["Afbmax"] = Afbmax,
                    ["Rfb"] = Rfb,
                    ["fi_fb"] = fi_fb,
                    ["Rfbloc"] = Rfbloc,
                    ["Nult"] = Nult
                };

                m_DS = BSQuery.UpdateLocalCompression(D);

                ok = true;
            }
            catch
            {
                ok = false;
            }
            return ok;
        }
    

        public override string SampleDescr()
        {
            return "";
        }

        public override string SampleName()
        {
            return "Расчет сталефибробетонных элементов на местное сжатие без арматуры";
        }
    }
}

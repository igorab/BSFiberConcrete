using BSFiberConcrete.Lib;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFiberConcrete.LocalStrength
{
    public class BSLocalCompressionCalc : BSLocalStrengthCalc
    {
        protected double psi;
        protected double Afbloc;
        protected double Afbmax;
        protected double fi_fb;
        protected double Rfbloc;
        protected double Nult;

        // Армирование:
        // расчетное сопротивление растяжению косвенной арматуры кг/см2
        protected double Rs_xy;
        // число стержней по x
        protected double nx;
        // число стержней по y
        protected double ny;
        // Площадь сечения стержня
        protected double Asx;
        // Площадь сечения стержня
        protected double Asy;
        // длина стержня сетки (см)
        protected double lx;
        // длина стержня сетки (см)
        protected double ly;
        // шаг сеток косвенного армирования
        protected double s;

        private double N_ults;

        public override void InitDataSource()
        {
            m_DS = BSData.LoadLocalStress();

            Dictionary<string, double> D = new Dictionary<string, double>();
            foreach (var item in m_DS) D[item.VarName] = item.Value;

            (a1, a2, c, Yb1, Yb2, Yb3, Yb5, Rfbn, Yb, Rfbt, psi, Afbloc, Afbmax, Rfb, fi_fb, Rfbloc, Nult) =
                (D["a1"], D["a2"], D["c"], D["Yb1"], D["Yb2"], D["Yb3"], D["Yb5"], D["Rfbn"], D["Yb"], 
                 D["Rfbt"], D["psi"], D["Afbloc"], D["Afbmax"], D["Rfb"], D["fi_fb"], D["Rfbloc"], D["Nult"]);

            // Аматура
            (Rs_xy, nx, ny, Asx, Asy, lx, ly, s) =
                (D["Rs_xy"], D["nx"], D["ny"], D["Asx"], D["Asy"], D["lx"], D["ly"], D["s"]);
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

                if (UseReinforcement)
                    this.ReinforcementCalc();
                
                m_DS = BSQuery.UpdateLocalCompression(D);

                ok = true;
            }
            catch
            {
                ok = false;
            }
            return ok;
        }
    
        public override bool ReinforcementCalc()
        {
            
            double Afbloc_ef = Afbmax;

            // коэффициент косвенного армирования
            double mu_s_xy = (nx * Asx * lx + ny * Asy * ly) / (Afbloc_ef * s) ;
            // коэффициент косвенного армирования
            double fi_s_xy = Math.Sqrt(Afbloc_ef / Afbloc);

            // приведенное (с учетом косвенной арматуры в зоне местного сжатия) расчетное сопротивление бетона сжатию
            double Rfbs_loc = Rfbloc + 2 * fi_s_xy * Rs_xy * mu_s_xy;

            //Предельная сила смятия (кг)
            N_ults = psi * Rfbs_loc * Afbloc;

            return true;
        }

        public override string SampleDescr()
        {
            return "Расчет сталефибробетонных элементов на местное сжатие";
        }

        public override string SampleName()
        {
            if (UseReinforcement == true)
                return "Расчет сталефибробетонных элементов на местное сжатие c арматурой";
            else
                return "Расчет сталефибробетонных элементов на местное сжатие без арматуры";
        }
    }
}

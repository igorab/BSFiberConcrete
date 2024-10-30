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
        
                        protected double Rs_xy;
                protected double nx;
                protected double ny;
                protected double Asx;
                protected double Asy;
                protected double lx;
                protected double ly;
                protected double s;

                protected double Nult;

        protected double mu_s_xy;
                protected double fi_s_xy;
                protected double Rfbs_loc;


        private void DCalcResult()
        {
            Dc = new Dictionary<string, double>()
            {
                ["Afbloc"] = Afbloc,
                ["Afbmax"] = Afbmax,
                ["Rfb"] = Rfb,
                ["fi_fb"] = fi_fb,
                ["Rfbloc"] = Rfbloc,
                ["Nult"] = Nult,

                ["lx"] = lx,
                ["ly"] = ly,
                ["mu_s_xy"] = mu_s_xy,
                ["fi_s_xy"] = fi_s_xy,
                ["Rfbs_loc"] = Rfbs_loc
            };
        }

        public BSLocalCompressionCalc()
        {
            DCalcResult();
        }

        public override void InitDataSource()
        {
            m_DS = BSData.LoadLocalStress();
            
        }

        private void InitValuesFromDataSource()
        {
            Dictionary<string, double> D = new Dictionary<string, double>();
            foreach (var item in m_DS) D[item.VarName] = item.Value;

            (a1, a2, c, Yb1, Yb2, Yb3, Yb5, Rfbn, Yb, Rfbt, psi, Afbloc, Afbmax, Rfb, fi_fb, Rfbloc, Nult) =
                (D["a1"], D["a2"], D["c"], D["Yb1"], D["Yb2"], D["Yb3"], D["Yb5"], D["Rfbn"], D["Yb"],
                 D["Rfbt"], D["psi"], D["Afbloc"], D["Afbmax"], D["Rfb"], D["fi_fb"], D["Rfbloc"], D["Nult"]);

                        (a, Rs_xy, nx, ny, Asx, Asy, lx, ly, s) =
                (D["a"], D["Rs_xy"], D["nx"], D["ny"], D["Asx"], D["Asy"], D["lx"], D["ly"], D["s"]);

            if (!UseReinforcement)
            {
                a = 0;
            }

        }

        public override string ReportName()
        {
            return "Местное сжатие";
        }

        public override void UpdateInputData(Dictionary<string, double> _Ds)
        {
            BSQuery.UpdateLocalCompression(_Ds);
        }

        public override bool RunCalc()
        {
            (Afbloc, Afbmax, Rfb, fi_fb, Rfbloc, Nult) = (0, 0, 0, 0, 0, 0);

            bool ok = base.RunCalc();
            if (!ok)
            {
                throw new Exception("Ошибка обновления данных в таблице");
            }

            try
            {
                InitValuesFromDataSource();

                                Afbloc = AfbLoc(Scheme);
                
                                Afbmax = AfbMax(Scheme);

                                Rfb = Rfbn /Yb * Yb1 * Yb2 * Yb3 * Yb5;

                fi_fb = 0.8 * Math.Sqrt( Afbmax / Afbloc );

                if (fi_fb > 2.5)
                    fi_fb = 2.5;
                else if (fi_fb < 1)
                    fi_fb = 1.0;

                Rfbloc = fi_fb * Rfb;

                Nult = psi * Rfbloc * Afbloc;
                                
                if (UseReinforcement)
                {
                    lx = this.Lx(Scheme);
                    ly = this.Ly(Scheme);
                    this.ReinforcementCalc();
                }

                DCalcResult();

                m_DS = BSQuery.UpdateLocalCompression(Dc);

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

                        mu_s_xy = (nx * Asx * lx + ny * Asy * ly) / (Afbloc_ef * s) ;
                        fi_s_xy = Math.Sqrt(Afbloc_ef / Afbloc);
                        Rfbs_loc = Rfbloc + 2 * fi_s_xy * Rs_xy * mu_s_xy;

                        Nult = psi * Rfbs_loc * Afbloc;

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

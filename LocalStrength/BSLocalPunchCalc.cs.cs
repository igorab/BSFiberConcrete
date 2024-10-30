using BSFiberConcrete.Lib;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BSFiberConcrete.LocalStrength
{
                public class BSLocalPunchCalc : BSLocalStrengthCalc
    {
                protected double h0;
                protected double h0x;
                protected double h0y;
        protected double u;
        protected double Afb;
        protected double Ffb_ult;
                        protected double F;
                protected double Mx;                 protected double My; 
                        protected double Wfbx;
                protected double Wfby;
                protected double Mfb_x_ult;
                protected double Mfb_y_ult;
                protected double FMxMy_uc;
                        protected double Rsw;
                        protected double Asw;
                protected double sw;
                protected double q_sw;
                protected double Fsw_ult;
                protected double Fult;
                protected double a;
                protected double b;
                protected double util_coeff;
        public BSLocalPunchCalc()
        {
            Scheme = 1;
            UseReinforcement = false;
            DCalcResult();
        }
        public override void InitDataSource()
        {
            m_DS = BSData.LoadLocalPunch();            
        }
        private void InitValuesFromDataSource()
        {
            Dictionary<string, double> D = new Dictionary<string, double>();
            foreach (var item in m_DS) D[item.VarName] = item.Value;
                        (F, Mx, My) = (D["F"], D["Mx"], D["My"]);
                        (a1, a2, h0x, h0y, Rfbtn, Yft, Yb1, Yb5, Rfbt, h0, u, Afb, Ffb_ult) =
                (D["a1"], D["a2"], D["h0x"], D["h0y"], D["Rfbtn"], D["Yft"], D["Yb1"], D["Yb5"], D["Rfbt"], D["h0"], D["u"], D["Afb"], D["Ffbult"]);
                        (Rsw, Asw, sw, q_sw, Fsw_ult, Fult, a, b, util_coeff) = (D["Rsw"], D["Asw"], D["sw"], D["q_sw"], D["Fsw_ult"], D["Fult"], D["a"], D["b"], D["util_coeff"]);
        }
        public override string ReportName()
        {
            return "Продавливание";
        }
        public override string SampleName()
        {
            if (UseReinforcement)
                return "Расчет сталефибробетонных элементов на продавливание при действии сосредоточенной силы c арматурой";
            else
                return "Расчет сталефибробетонных элементов на продавливание при действии сосредоточенной силы без арматуры";
        }
        public override void UpdateInputData(Dictionary<string, double> _Ds)
        {
            m_DS = BSQuery.UpdateLocalPunch(_Ds);
        }
        private void DCalcResult()
        {
            Dc = new Dictionary<string, double>()
            {
                ["h0"] = h0,
                ["u"] = u,
                ["Afb"] = Afb,
                ["Ffbult"] = Ffb_ult,
              
                ["a"] = a,
                ["b"] = b,
                ["Afb"] = Afb,
                ["Ffb_ult"] = Ffb_ult,
                ["Fult"] = Fult,
                ["util_coeff"] = util_coeff,
                ["Wfbx"] = Wfbx,
                ["Wfby"] = Wfby,
                ["Mfb_x_ult"] = Mfb_x_ult,
                ["Mfb_y_ult"] = Mfb_y_ult,
                ["FMxMy_uc"] = FMxMy_uc
            };
        }
        private void RunBetonCalc()
        {
                        h0 = 0.5 * (h0x + h0y);
                        double _a = a1 + h0;
                        double _b = a2 + h0;
                        u = 2 * _a + 2 * _b;
                        Afb = u * h0;
                        Ffb_ult = Rfbt * Afb;
            if (F != 0 || Mx != 0 || My != 0)
            {
                RunCalcFM();
            }
        }
        public override bool RunCalc()
        {
            (h0, u, Afb, Ffb_ult) = (0, 0, 0, 0);
            bool ok = base.RunCalc();
            if (!ok)
            {
                throw new Exception("Ошибка обновления данных в таблице");
            }
            try
            {
                InitValuesFromDataSource();
                                
                if (UseReinforcement)
                {
                                                            ReinforcementCalc();
                }
                else
                {
                                                            RunBetonCalc();
                }
                DCalcResult();
                m_DS = BSQuery.UpdateLocalPunch(Dc);
                ok = true;
            }
            catch 
            {
                ok = false;
            }
            return ok;
        }
                
                public bool RunCalcFM()
        {
            double Lx = a2 + h0;
            double Ly = a1 + h0;
            double Ifbx = 2 * ( Lx * Math.Pow(1,3) /12 + Math.Pow(Ly/2, 2) * Lx * 1 + 1 * Math.Pow(Ly, 3)/12 );
            double Ifby = 2 * (Ly * Math.Pow(1, 3) / 12 + Math.Pow(Lx / 2, 2) * Ly * 1 + 1 * Math.Pow(Lx, 3) / 12);
            double x_max = Lx / 2;
            double y_max = Ly / 2;
                        Wfbx = Ifbx / y_max;
                        Wfby = Ifby / x_max;
                        Mfb_x_ult = Rfbt * Wfbx * h0;
                        Mfb_y_ult = Rfbt * Wfby * h0;
            FMxMy_uc = F / Ffb_ult + Mx / Mfb_x_ult + My / Mfb_y_ult;
            return true;
        }
                public override bool ReinforcementCalc()
        {
                        double ca2 = a1 + 4 * h0;
                        double cb2 = a2 + 4 * h0;
                        u = 2 * ca2 + 2 * cb2;
                        Afb = u * h0;
                        Ffb_ult = Rfbt * Afb;
            Fult = Ffb_ult + Fsw_ult;
                        util_coeff = F / Ffb_ult;
                                                            
            return true;
        }
                public bool RunReinforcementCalcFM()
        {
            double Lx = a2 + h0;
            double Ly = a1 + h0;
            double Ifbx = 2 * (Lx * Math.Pow(1, 3) / 12 + Math.Pow(Ly / 2, 2) * Lx * 1 + 1 * Math.Pow(Ly, 3) / 12);
            double Ifby = 2 * (Ly * Math.Pow(1, 3) / 12 + Math.Pow(Lx / 2, 2) * Ly * 1 + 1 * Math.Pow(Lx, 3) / 12);
            double x_max = Lx / 2;
            double y_max = Ly / 2;
                        Wfbx = Ifbx / y_max;
                        Wfby = Ifby / x_max;
                        Mfb_x_ult = Rfbt * Wfbx * h0;
                        Mfb_y_ult = Rfbt * Wfby * h0;
            FMxMy_uc = F / Ffb_ult + Mx / Mfb_x_ult + My / Mfb_y_ult;
            return true;
        }
        public override string SampleDescr()
        {
            return "Расчет элементов на продавливание";
        }
    }
}

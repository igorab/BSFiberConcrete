using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BSFiberConcrete.FiberCalcMNQ
{
    public class BSFiberCalc_MyMx : BSFiberCalc_MNQ
    {
        protected void Calculate_My(double _b, double _h)
        {
                        double a = Rebar.a;
                        double h0 = _h - a;
                        double _Rfbt3 = R_fbt3();
                        double As = Rebar.As;
                        double Rsw = Rebar.Rsw_X;
                        double Asw = Rebar.Asw_X;
                        double sw = Rebar.Sw_X;
                        double q_sw = (sw != 0) ? Rsw * Asw / sw : 0;
                        if (q_sw < 0.25 * _Rfbt3 * b)
                q_sw = 0;
            double c_min = h0;
            double c_max = 4 * h0;
            double c0_max = 2 * h0;
            List<double> С_x = new List<double>();
            InitC(ref С_x, c_min, c_max, 1);
            double Q_sw,
                   M_sw;             double M_fbt = 0;             double Q_fbt3 = (c_min != 0) ? 1.5d * _Rfbt3 * _b * h0 * h0 / c_min : 0;
                        double N_s = Rebar.Rs * Rebar.As;
                        double z_S = 0.9 * h0;
                        double Ms = N_s * z_S;                         List<double> lst_Q_sw = new List<double>();
                        List<double> lst_M_sw = new List<double>();
            List<double> lst_Q_fbt3 = new List<double>();
            List<double> lst_M_fbt = new List<double>();
            List<double> lst_M_ult = new List<double>();
            foreach (double ci in С_x)
            {
                if (ci > c0_max)
                {
                    Q_sw = q_sw * c0_max;
                    M_sw = 0.5 * Q_sw * c0_max;
                }
                else
                {
                    Q_sw = q_sw * ci;                     M_sw = 0.5 * Q_sw * ci;                 }
                lst_Q_sw.Add(Q_sw);
                lst_M_sw.Add(M_sw);
                Q_fbt3 = (ci != 0) ? 1.5d * _Rfbt3 * _b * h0 * h0 / ci : 0;
                if (Q_fbt3 >= 2.5d * _Rfbt3 * _b * h0)
                {
                    Q_fbt3 = 2.5d * _Rfbt3 * _b * h0;
                }
                else if (Q_fbt3 <= 0.5d * _Rfbt3 * _b * h0)
                {
                    Q_fbt3 = 0.5d * _Rfbt3 * _b * h0;
                }
                M_fbt = 0.5 * Q_fbt3 * ci;
                lst_Q_fbt3.Add(Q_fbt3);
                lst_M_fbt.Add(M_fbt);
                                M_ult = Ms + M_sw + M_fbt; 
                lst_M_ult.Add(M_ult);
            }
            M_ult = (lst_M_ult.Count > 0) ? lst_M_ult.Min() : 0;
                        UtilRate_My = (M_ult != 0) ? m_Efforts["My"] / M_ult : 0;
        }
                                protected void Calculate_Mx()
        {
            if (m_Efforts["Mx"] == 0) return;
                        UtilRate_Mx = (M_ult != 0) ? m_Efforts["Mx"] / M_ult : 0;
        }
        protected void CalculateMyMx()
        {
        }
    }
}

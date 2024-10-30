using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BSFiberConcrete
{
    public class BSFiberCalc_MNQ_Ring : BSFiberCalc_MNQ
    {
        private string m_ImgCalc;
        public BSBeam_Ring beam { get; set; }
        public BSFiberCalc_MNQ_Ring()
        {
            this.beam = new BSBeam_Ring();
            base.m_Beam = this.beam;
        }
        public override string ImageCalc()
        {
            if (!string.IsNullOrEmpty(m_ImgCalc))
                return m_ImgCalc;
            return base.ImageCalc();
        }
        public override void SetSize(double[] _t)
        {
            beam.SetSizes(_t);
            (r1, r2, LngthCalc0) = (_t[0], _t[1], _t[2]);
            A = beam.Area();
            
            h = beam.h;
            b = beam.b;
            base.m_Beam = this.beam;
            LngthCalc0 = beam.Length;
            
            I = beam.Jx();
            
            y_t = beam.y_t;
        }
        public override void SetParams(double[] _t)
        {
            base.SetParams(_t);            
        }
                                private new void Calculate_N()
        {
            double Ar = beam.Area();
            Rfb = Rfbn / Yb * Yb1 * Yb2 * Yb3 * Yb5;
            double Rfbt3 = Rfbt3n / Yft * Yb1 * Yb5;
                        double alfa_r = (N + Rfbt3 * Ar) / ((Rfb + 3.35d * Rfbt3) * Ar);
            if (alfa_r < 0.15)
            {
                alfa_r = (N + 0.73 * Rfbt3 * Ar) / ((Rfb + 2 * Rfbt3) * Ar);
            }
            N_ult = Ar * (Rfb * Math.Sin(Math.PI * alfa_r) / Math.PI + Rfbt3 * (1 - 1.35 * alfa_r) * 1.6 * alfa_r) * beam.r_m / e_N;
                        UtilRate_N = (N_ult != 0) ? m_Efforts["N"] / N_ult : 0;
        }
                                private void Calculate_N_Rods_Comb()
        {
            var _prms = (r1, r2, beam.r_s, N, e_N, LngthCalc0);
                        Rfbt3 = R_fbt3();
                        Rfb = R_fb();
                        double Is = beam.I_s();
                        D = D_stiff(Is);
                        Ncr = N_cr(D);
                                    eta = Eta(N, Ncr);
            double x_denom = (Rebar.Rsc + 1.7* Rebar.Rs) * Rebar.As + (Rfb + Rfbt3) * beam.A_r;
                        double dzeta_cir = (x_denom > 0) ? (N + Rebar.Rs * Rebar.As * + Rfbt3 * beam.A_r) / x_denom : 0;
            delta_e = Delta_e(e0 / m_Beam.h);
            fi1 = Fi1();
            k_b = K_b(fi1, delta_e);
            char calc_mode = 'c';
            if (dzeta_cir > 0.15 && dzeta_cir < 0.6)            
                calc_mode = 'a';            
            else if (dzeta_cir <= 0.15)
                calc_mode = 'b';
            else if (dzeta_cir >= 0.6)
                calc_mode = 'c';
            switch (calc_mode)
            {
                case 'a':
                                        N_ult = (Rfb * beam.A_r * beam.r_m + Rebar.Rsc * Rebar.As * beam.r_s) * Math.Sin(Math.PI * dzeta_cir) / Math.PI;
                    N_ult += (Rebar.Rsc * Rebar.As + Rfbt3 * beam.A_r) * beam.r_s * (1 - 1.7 * dzeta_cir) * (0.2 - 1.3 * dzeta_cir);
                    N_ult = N_ult / (e_N * eta);
                    break;
                case 'b':
                    double dzeta_cir1 = (N + 0.75 * Rebar.Rs * Rebar.As) / (Rebar.Rsc * Rebar.As + Rfb * beam.A_r);
                                        N_ult = (Rfb * beam.A_r * beam.r_m + Rebar.Rsc * Rebar.As * beam.r_s) * Math.Sin(Math.PI * dzeta_cir1) / Math.PI;
                    N_ult += 0.295 * (Rebar.Rs * Rebar.As + Rfbt3 * beam.A_r) * beam.r_s;
                    N_ult /= e_N * eta;
                    break;
                case 'c':
                    double dzeta_cir2 = N / (Rebar.Rsc * Rebar.As + Rfb * beam.A_r);
                                        N_ult = (Rfb * beam.A_r * beam.r_m + Rebar.Rsc * Rebar.As * beam.r_s) * Math.Sin(Math.PI * dzeta_cir2) / Math.PI;
                    N_ult /= e_N * eta;
                    break;
            }
        }
                                protected new void Calculate_N_Rods()
        {
            m_ImgCalc = "Ring_N_Rods.PNG";
            
            Calculate_N_Rods_Comb();
        }
                                                        public override bool Calculate()
        {
            if (N_Out)
            {
                Calculate_N_Out();
            }
            else if (Shear)
            {
                m_ImgCalc = "Incline_Q.PNG";
                                Calculate_Qx(b, h);
                                Calculate_My(b, h);
            }
            else if (UseRebar)
            {
                Calculate_N_Rods();
            }
            else
            {
                N_In = true;
                Calculate_N();
            }
            return true;
        }
    }
}

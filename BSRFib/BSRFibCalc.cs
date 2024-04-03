using BSFiberConcrete.Lib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFiberConcrete.BSRFib
{
    public class BSRFibCalc
    {
        public double h { get; set; }
        public double b { get; set; }

        private double Rf;
        private double Rfbt3;
        private double Rf_ser;
        private double Rb_ser;

        // коэффициент условий работы, принимаемый равным 1,0 для фибры из слябов;
        // 1,1 – для фибры из листа и фибры из проволоки
        public double gamma_fb1 { get; set; }
        public double gamma_fb2 { get; set; }

        public double Rb { get; set; }

        //коэффициент, учитывающий анкеровку фибры
        public double eta_f { get; set; }
        public double l_f { get; set; }

        // коэффициент фибрового армирования по объему
        public double mu_fv { get; set; }

        private double k_or;
        //коэффициент, учитывающий работу фибр в сечении, перпендикулярном
        // направлению внешнего сжимающего усилия, и принимаемый по таблице В.2;
        private double k_n;

        private List<RFibKor> Kor;
        private List<RFibKor> Kn;


        public static Dictionary<int, double> EtaF => new Dictionary<int, double>
        {
            [0] = 0.8,
            [1] = 0.6,
            [2] = 0.9,
        };

        public static Dictionary<int, double> GammaFb => new Dictionary<int, double>
        {
            [0] = 1.0,
            [1] = 1.1,
            [2] = 1.1,
        };


        public BSRFibCalc()
        {
            k_or = 0.5;
            k_n = 0.5;
            Rfbt3 = 30.59;
            Rf = BSHelper.MPA2kgsm2(430);
            Rf_ser = BSHelper.MPA2kgsm2(400);
            Rb_ser = BSHelper.MPA2kgsm2(22);
            Rb = BSHelper.MPA2kgsm2(17);
            mu_fv = 0.005;

            gamma_fb1 = 1.1;
            gamma_fb2 = 1.1;
        }

        // площадь номинального поперечного сечения фибры, определяемая по ее номинальным размерам        
        public double S_f => h * b;

        public double d_f_red
        {
            get => 1.13 * Math.Sqrt(S_f);
            
            set { m_D_f_red = value; }
        }

        private double m_D_f_red;

        public double L_f_an => eta_f * m_D_f_red * Rf_ser / Rb_ser;

        public double K_T => Math.Sqrt(1 - (1.2d - 80 * mu_fv));

        public double L => k_n * k_n * mu_fv * Rf / Rb;

        // коэффициент эффективности косвенного армирования фибрами, вычисляемый по формуле
        public double fi_f => (5.0 + L) / (1 + 4.5 * L);

        public RFiber RFiber 
        { 
            set 
            { 
                Rf_ser = BSHelper.MPA2kgsm2(value.Rfser);  
                Rf = BSHelper.MPA2kgsm2(value.Rf);                  
            } 
        }

        public void LoadFibKorKn()
        {
            Kor = BSData.LoadRFibKor();
            Kn = BSData.LoadRFibKn();
        }

        private double KorKn(List<RFibKor> _KorKn, double _x, double _y)
        {
            double k_coef = 0;

            try
            {
                var kor = _KorKn[0];

                List<double> lskor_y = new List<double>() { };

                int id = 1;
                if (_x >= kor.A && _x < kor.B)
                    id = 2;
                else if (_x >= kor.B && _x < kor.C)
                    id = 3;
                else if (_x >= kor.C && _x < kor.D)
                    id = 4;
                else if (_x >= kor.D && _x < kor.E)
                    id = 5;
                else if (_x >= kor.E && _x < kor.F)
                    id = 6;
                else if (_x >= kor.F && _x < kor.G)
                    id = 7;
                else if (_x >= kor.G && _x < kor.G)
                    id = 8;
                else if (_x >= kor.H && _x < kor.G)
                    id = 9;
                else if (_x >= kor.I)
                    id = 10;

                foreach (var line in Kor)
                    lskor_y.Add(line.A);

                int idy = 1;
                if (_y >= lskor_y[0] && _y < lskor_y[1])
                    idy = 2;
                else if (_y >= lskor_y[1] && _y < lskor_y[2])
                    idy = 3;
                else if (_y >= lskor_y[2] && _y < lskor_y[3])
                    idy = 4;
                else if (_y >= lskor_y[3] && _y < lskor_y[4])
                    idy = 5;
                else if (_y >= lskor_y[4] && _y < lskor_y[5])
                    idy = 6;
                else if (_y >= lskor_y[5] && _y < lskor_y[6])
                    idy = 7;
                else if (_y >= lskor_y[7] && _y < lskor_y[8])
                    idy = 8;
                else if (_y >= lskor_y[8] && _y < lskor_y[9])
                    idy = 9;
                else if (_y >= lskor_y[10])
                    idy = 10;

                kor = _KorKn[idy];

                List<double> lskor_x = new List<double>() { kor.A, kor.B, kor.C, kor.D, kor.E, kor.F, kor.G, kor.H, kor.I };

                k_coef = lskor_x[id];
            }
            catch
            {
                k_coef = 0;
            }
            return k_coef;
        }

        public double Run(out Dictionary<string, double> Res)
        {            
            // B8
            double Rfb = Rb + (k_n * k_n * fi_f * mu_fv * Rf);
            double x = h / l_f;
            double y = b / l_f;

            double _k_or = KorKn(Kor, x, y);
            if (_k_or != 0) k_or = _k_or;
            double _k_n = KorKn(Kn, x, y);
            if (_k_n != 0) k_n = _k_n;

            if (L_f_an < l_f / 2)
            {
                //
                //сопротивление растяжению сталефибробетона исчерпывается из-за
                //обрыва некоторого числа фибр и выдергивания остальных, что определяется условием
                //
                Rfbt3 = gamma_fb1 * (K_T * k_or * k_or * mu_fv * Rf * (1 - L_f_an / l_f) + 0.1 * Rb * (0.8 - Math.Sqrt(2 * mu_fv - 0.005)));
            }
            else
            {
                //
                //сопротивление растяжению сталефибробетона исчерпывается из-за
                //выдергивания из бетона условно всех фибр, что определяется условием
                //
                Rfbt3 =  gamma_fb2 * Rb * (K_T * k_or * k_or * mu_fv * l_f / (8 * eta_f * m_D_f_red) + 0.08 - 0.5 * mu_fv);
            }

            double mu_fa = mu_fv * k_or * k_or;

            double mu1_fa = mu_fv * k_n * k_n;

            Res = new Dictionary<string, double>()
            {
                ["Rfb"] = Rfb,
                ["Rfbt3"] = Rfbt3,
                ["mu_fa"] = mu_fa,
                ["mu1_fa"] = mu1_fa
            };

            return Rfbt3;
        }
    }
}

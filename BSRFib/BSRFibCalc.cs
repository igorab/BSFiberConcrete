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
        public double  eta_f { get; set; }
        public double l_f { get; set; }
                
        // коэффициент фибрового армирования по объему
        public double mu_fv { get; set; }
        
        private double k_or;
        //коэффициент, учитывающий работу фибр в сечении, перпендикулярном
        // направлению внешнего сжимающего усилия, и принимаемый по таблице В.2;
        private double k_n;


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
        
        public double d_f_red => 1.13 * Math.Sqrt(S_f);

        public double L_f_an => eta_f *  d_f_red * Rf_ser / Rb_ser;

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

        public double Run(out Dictionary<string, double> Res)
        {           
            // B8
            double Rfb = Rb + (k_n * k_n * fi_f * mu_fv * Rf);

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
                Rfbt3 =  gamma_fb2 * Rb * (K_T * k_or * k_or * mu_fv * l_f / (8 * eta_f * d_f_red) + 0.08 - 0.5 * mu_fv);
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

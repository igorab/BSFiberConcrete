using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BSFiberConcrete
{
    public partial class BSRFiber : Form
    {
        private double h, b;

        //коэффициент, учитывающий анкеровку фибры
        private double eta_f;

        private double lf, Rf_ser, Rb_ser, Rb, df_red;

        // коэффициент условий работы, принимаемый равным 1,0 для фибры из слябов;
        // 1,1 – для фибры из листа и фибры из проволоки
        private double gamma_fb1;
        // коэффициент фибрового армирования по объему
        private double mu_fv;
        

        private double Rf, vfb2;

        public BSRFiber()
        {
            InitializeComponent();
        }
        
        public double Run()
        {

            double lf_an, KT, kor, kn, Rfbt3, L, fi_f, Rfb;

            lf_an = eta_f * df_red * Rf_ser / Rb_ser;

            KT = Math.Sqrt(1 - (1.2d - 80 * mu_fv));

            kor = 0.5;
            kn = 0.5;

            L = kn * kn * mu_fv * Rf / Rb;

            fi_f = (5 + L) / (1 + 4.5 * L);

            Rfb = Rb + (kn * kn * fi_f * mu_fv * Rf);

            if (lf_an < lf / 2)
            {
                Rfbt3 = gamma_fb1 * (KT * kor * kor * mu_fv * Rf * (1 - lf_an / lf) + 0.1 * Rb * (0.8 - Math.Sqrt(2 * mu_fv - 0.005)));
            }
            else
            {
                Rfbt3 = vfb2 * Rb * (KT * kor * kor * mu_fv * lf / (8 * eta_f * df_red) + 0.08 - 0.5 * mu_fv);
            }

            return Rfbt3;
        }


        private void btnCalc_Click(object sender, EventArgs e)
        {
            var x = Run();

            numRes.Value = (decimal) x;
            
        }

        private void BSRFiber_Load(object sender, EventArgs e)
        {

        }
    }
}

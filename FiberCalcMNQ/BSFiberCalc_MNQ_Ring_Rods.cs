using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFiberConcrete.FiberCalcMNQ
{
    /// <summary>
    ///  Расчет прочности кольцевых сеченний колонн с кобинированным армированием
    /// </summary>
    public class BSFiberCalc_MNQ_Ring_Rods : BSFiberCalc_MNQ_Ring
    {
        double rs = 36.0;
        double rm = 36.0;

        public override void Calculate()
        {
            BSMatRod matRod = beam.MatRod;

            base.Calculate();

            double As_tot = matRod.As + matRod.As1;

            double Ar = beam.Area();

            // относительная площадь сжатой зоны бетона по ф. (6.41)
            double dzeta_cir = (N + matRod.Rs * As_tot + Rfbt3 * Ar) / ((matRod.Rsc + 1.7 * matRod.Rs) * As_tot + (Rfb + Rfbt3) * Ar);

            // Коэфициент по п8.1.5 Сп63
            double k_s = 0.7;

            //коэффициент, учитывающий влияние длительности действия нагрузки п8.1.5 Сп63
            double fi_1 = 2.0;

            // относительное значение эксцентриситета продольной силы Сп63 п8.1.5
            //относительное значение эксцентриситета продольной силы
            delta_e = Delta_e(m_Fiber.e0 / beam.r2);

            // Коэфициент ф.(6.26)
            k_b = K_b(fi1, delta_e);

            // Начальный модуль упругости бетона-матрицы B30 СП63
            Eb = beam.Mat.Eb;

            Efb = beam.Mat.Efb;

            double alfa = beam.Mat.alfa(matRod.Es);

            //Площадь приведенного сечения  см2 Пособие к СП 52-102-2004 ф. (2.11)
            double Ared = Ar + alfa * As_tot;

            double Is_red = alfa * As_tot * Math.Pow(2 * rs, 2) / 8;

            double Ifb = 0;

            double Ired = Ifb + Is_red;

            // жесткость железобетонного элемента в предельной по прочности стадии п8.1.5 сп63
            double D = 1;

            // условная критическая сила, определяемая по формуле (8.15)СП 63
            double Ncr = Math.PI * Math.PI * D / Math.Pow(l0, 2);

            // Коэфициент продольного изгиба определяют по пункту 8.1.15 ф (8.14)СП 63
            double eta = 1 - (1 - N / Ncr);

            double M = 0;

            if (dzeta_cir > 0.15 && dzeta_cir < 0.6)
            {
                // Предельная продольная сжимающая сила сечения элемента
                M = (Rfb * Ar * rm + beam.MatRod.Rsc * As_tot * rs) * Math.Sin(Math.PI * dzeta_cir) / Math.PI;
                M += (matRod.Rs * As_tot + beam.Mat.Rfbt3 * Ar) * rs * (1 - 1.7 * dzeta_cir) * (0.2 - 1.3 * dzeta_cir);
                N_ult = M / e_N * eta;
            }
            else if (dzeta_cir <= 0.15)
            {
                double dzeta_cir1 = (N + 0.75 * matRod.Rs * As_tot) / (matRod.Rsc * As_tot + Rfb * Ar);

                M = (Rfb * Ar * rm + beam.MatRod.Rsc * As_tot * rs) * Math.Sin(Math.PI * dzeta_cir1) / Math.PI + 0.295 * (matRod.Rs * As_tot + beam.Mat.Rfbt3 * Ar) * rs;
                N_ult = M / e_N * eta;
            }
            else if (dzeta_cir >= 0.6)
            {
                double dzeta_cir2 = N / (matRod.Rsc * As_tot + Rfb * Ar);
                M = (Rfb * Ar * rm + beam.MatRod.Rsc * As_tot * rs) * Math.Sin(Math.PI * dzeta_cir2) / Math.PI;
                N_ult = M / e_N * eta;
            }

            N_ult = BSHelper.Kg2T(N_ult);
        }
    }







}

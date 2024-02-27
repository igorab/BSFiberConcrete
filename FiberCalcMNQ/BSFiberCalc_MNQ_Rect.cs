using System;
using System.Collections.Generic;
using System.Linq;

namespace BSFiberConcrete
{
    /// <summary>
    /// Прямоугольная балка
    /// </summary>
    public class BSFiberCalc_MNQ_Rect : BSFiberCalc_MNQ
    {
        public BSBeam_Rect beam { get; set; }

        private double y_t;

        private Dictionary<string, double> m_Result;

        public BSFiberCalc_MNQ_Rect()
        {
            this.beam = new BSBeam_Rect();
            m_Result = new Dictionary<string, double>();
        }

        public override void GetSize(double[] _t)
        {
            (b, h, l0) = (beam.b, beam.h, beam.Length) = (_t[0], _t[1], _t[2]);

            A = beam.Area();

            I = beam.I_s();

            y_t = beam.y_t();
        }

        /// <summary>
        /// Расчет внецентренно сжатых элементов (6.1.13)
        /// </summary>
        private void Calculate_N()
        {
            //Коэффициент, учитывающий влияние длительности действия нагрузки, определяют по формуле (6.27)
            fi1 = (M1 != 0) ? 1 + Ml1 / M1 : 1.0;

            //относительное значение эксцентриситета продольной силы
            delta_e = Delta_e(m_Fiber.e0 / beam.h);

            // Коэфициент ф.(6.26)
            k_b = K_b(fi1, delta_e);

            // Модуль упругости сталефибробетона п.п. (5.2.7)
            Efb = m_Fiber.Eb * (1 - m_Fiber.mu_fv) + m_Fiber.Ef * m_Fiber.mu_fv;

            //жесткость элемента в предельной по прочности стадии,определяемая по формуле (6.25)
            D = k_b * Efb * I;

            // условная критическая сила, определяемая по формуле (6.24)
            Ncr = Math.PI * Math.PI * D / Math.Pow(l0, 2);

            eta = 1 / (1 - N / Ncr);

            Ab = beam.b * beam.h * (1 - 2 * m_Fiber.e0 * eta / beam.h);

            Rfb = R_fb();

            N_ult = fi * Rfb * A;

            double flex = l0 / h;

            if (e0 <= h / 30 && flex <= 20)
            {
                N_ult = fi * Rfb * A;
            }
            else
            {
                N_ult = Rfb * Ab;
            }

            N_ult = BSHelper.Kg2T(N_ult);
        }

        // Расчет внецентренно сжатых сталефибробетонных элементов без рабочей арматуры при
        //расположении продольной сжимающей силы за пределами поперечного сечения элемента и внецентренно сжатых сталефибробетонных элементов без рабочей арматуры при расположении продольной
        // сжимающей силы в пределах поперечного сечения элемента, в которых по условиям эксплуатации не
        // допускается образование трещин
        private void Calculate_N_Out()
        {
            //Коэффициент, учитывающий влияние длительности действия нагрузки (6.27)
            fi1 = (M1 != 0) ? 1 + Ml1 / M1 : 1.0;

            //относительное значение эксцентриситета продольной силы
            delta_e = Delta_e(m_Fiber.e0 / beam.h);

            // Коэфициент ф.(6.26)
            k_b = K_b(fi1, delta_e);

            // Модуль упругости сталефибробетона п.п. (5.2.7)
            Efb = m_Fiber.Eb * (1 - m_Fiber.mu_fv) + m_Fiber.Ef * m_Fiber.mu_fv;

            //Модуль упругости арматуры
            double Es = 0;
            // Момент инерции продольной арматуры соответственно относительно оси, проходящей через центр тяжести поперечного сечения элемента
            double ls = 0;
            //жесткость элемента в предельной по прочности стадии, определяемая по формуле (6.31)
            D = k_b * Efb * I + 0.7 * Es * ls;

            // условная критическая сила, определяемая по формуле (6.24)
            Ncr = Math.PI * Math.PI * D / Math.Pow(beam.Length, 2);

            //коэффициент, учитывающий влияние продольного изгиба элемента на его несущую способность (6.23) 6.1.13
            eta = 1 / (1 - N / Ncr);

            Ab = beam.b * beam.h * (1 - 2 * m_Fiber.e0 * eta / beam.h);

            //Расчетные значения сопротивления осевому растяжению
            double Rfbt = Rfbtn / Yft * Yb1 * Yb5;

            // Предельная сила сечения
            N_ult = fi * Rfbt * A;

            N_ult = BSHelper.Kg2T(N_ult);
        }

        /// <summary>
        /// Расчет элементов по полосе между наклонными сечениями
        /// </summary>
        private void CalculateQ()
        {
            // Растояние до цента тяжести арматуры растянутой арматуры, см
            double a = l_rebar.Length > 2 ? l_rebar[2] : 4;

            // рабочая высота сечения по растянутой арматуре
            double h0 = h - a;

            // Расчетные значения сопротивления  на сжатиие по B30 СП63
            Rfb = R_fb();

            // Предельная перерезывающая сила по полосе между наклонными сечениями
            double _Q_ult = 0.3 * Rfb * b * h0; // (6.74)
            //_Q_ult = BSHelper.Kg2T(_Q_ult);

            // Расчет элементов по наклонным сечениям на действие поперечных сил

            // Минимальная длина проекции(см)
            double c_min = h0;
            // Максимальная длина проекции(см)
            double c_max = 4 * h0;
            double dC = 1;
            // ?? Минимальная длина проекции для формулы
            double c0_max = 2 * h0;

            List<double> lst_C = new List<double>();
            InitC(ref lst_C, c_min, c_max, dC);

            // Расчетное сопротивление сталефибробетона осевому растяжению
            double Rfbt = Rfbtn / Yft * Yb1 * Yb5;

            // поперечная сила, воспр сталефибробетоном
            double Qfb_i;

            List<double> lstQ_fb = new List<double>();

            foreach (double _c in lst_C)
            {
                if (_c == 0) continue;

                Qfb_i = 1.5d * Rfbt * b * h0 * h0 / _c; // 6.76

                // условие на 0.5..2.5
                if (Qfb_i >= 2.5 * Rfbt * b * h0)
                    Qfb_i = 2.5 * Rfbt * b * h0;
                else if (Qfb_i <= 0.5 * Rfbt * b * h0)
                    Qfb_i = 0.5 * Rfbt * b * h0;

                lstQ_fb.Add(Qfb_i);
            }
            // Qfb - максимальная поперечная сила, воспринимаемая сталефибробетоном в наклонном сечении
            double Qfb = lstQ_fb.Max();

            // Максимальный шаг поперечной арматуры см
            double s_w_max = Rfbt * b * h0 * h0 / Q;

            string res;
            if (Rebar.s_w <= s_w_max)
            {
                res = "Условие выполнено, шаг удовлетворяет требованию 6.1.28";
                Msg.Add(res);
            }
            else
            {
                res = "Условие не выполнено, требуется уменьшить шаг поперечной арматуры";
                Msg.Add(res);
            }

            // усилие в поперечной арматуре на единицу длины элемента
            double q_sw = Rebar.Rsw * Rebar.Asw / Rebar.s_w; // 6.78 

            // условие учета поперечной арматуры
            if (q_sw < 0.25 * Rfbt * b)
                q_sw = 0;

            // поперечная сила, воспринимаемая поперечной арматурой в наклонном сечении
            double Qsw = 0;
            List<double> lst_Qsw = new List<double>();
            foreach (double _c in lst_C)
            {
                if (_c > c0_max)
                    Qsw = 0.75 * q_sw * c0_max;
                else
                    Qsw = 0.75 * q_sw * _c;  // 6.77

                lst_Qsw.Add(Qsw);
            }

            List<double> lst_Q_ult = new List<double>();
            for (int i = 0; i < lst_Qsw.Count; i++)
            {
                lst_Q_ult.Add(lstQ_fb[i] + lst_Qsw[i]);
            }

            Q_ult = Qfb + Qsw; // 6.75
        }

        private void InitC(ref List<double> _lst, double _from, double _to, double _dx)
        {
            double val = _from;
            while (val <= _to)
            {
                _lst.Add(val);
                val += _dx;
            }
        }

        /// <summary>
        ///  Расчет элементов по наклонным сечениям на действие моментов
        /// </summary>
        private void CalculateM()
        {
            // Растояние до цента тяжести арматуры растянутой арматуры, см
            double a = l_rebar.Length > 2 ? l_rebar[2] : 4;

            // рабочая высота сечения по растянутой арматуре
            double h0 = h - a;

            // Нормативное остаточное сопротивления осевому растяжению кг/см2
            double Rfbt3 = Rfbt3n / Yft * Yb1 * Yb5;

            // Площадь растянутой арматуры см2
            double As = Rebar.As;

            // Расчетное сопротивление поперечной арматуры  
            double Rsw = Rebar.Rsw;

            // Площадь арматуры
            double Asw = Rebar.Asw;

            // шаг попреречной арматуры
            double sw = Rebar.s_w;

            // усилие в поперечной арматуре на единицу длины элемента
            double q_sw = Rsw * Asw / sw;

            // условие учета поперечной арматуры
            if (q_sw < 0.25 * Rfbt3 * b)
                q_sw = 0;

            double c_min = h0;
            double c_max = 4 * h0;
            double c0_max = 2 * h0;
            List<double> С_x = new List<double>();

            InitC(ref С_x, c_min, c_max, 1);

            double Q_sw,
                   M_sw; // момент, воспр поперечной арматурой
            double M_fbt = 0; // момент, воспр сталефибробетоном

            double Q_fbt3 = 1.5d * Rfbt3 * b * h0 * h0 / c_min;

            // усилие в продольной растянутой арматуре
            double N_s = Rebar.Rs * Rebar.As;

            // плечо внутренней пары сил
            double z_S = 0.9 * h0;

            // момент, воспринимаемый продольной арматурой, пересекающей наклонное сечение, относительно противоположного конца наклонного сечения
            double Ms = N_s * z_S; // 6.80

            //  Усилие в поперечной арматуре:
            List<double> lst_Q_sw = new List<double>();
            // момент, воспринимаемый поперечной арматурой, пересекающей наклонное сечение, относительно противоположного конца наклонного сечения
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
                    Q_sw = q_sw * ci; // усилие в поперечной арматуре
                    M_sw = 0.5 * Q_sw * ci; // (6.81)
                }

                lst_Q_sw.Add(Q_sw);
                lst_M_sw.Add(M_sw);

                Q_fbt3 = 1.5d * Rfbt3 * b * h0 * h0 / ci;

                if (Q_fbt3 >= 2.5d * Rfbt3 * b * h0)
                    Q_fbt3 = 2.5d * Rfbt3 * b * h0;
                else if (Q_fbt3 <= 0.5d * Rfbt3 * b * h0)
                    Q_fbt3 = 0.5d * Rfbt3 * b * h0;

                M_fbt = 0.5 * Q_fbt3 * ci;

                lst_Q_fbt3.Add(Q_fbt3);
                lst_M_fbt.Add(M_fbt);

                // расчет по наклонным сечениям; условие : M <= M_ult (Ms - продольная, Msw - поперечная, М_fbt - фибробетон)
                M_ult = Ms + M_sw + M_fbt; // 6.79

                lst_M_ult.Add(M_ult);
            }

            M_ult = lst_M_ult.Min();

            // предельный момент сечения (т*м)
            M_ult = BSHelper.Kg2T(M_ult);
        }

        public override void Calculate()
        {
            if (UseRebar)
            {
                Calculate_N_Out();
            }
            else if (Shear)
            {   // Расчет на действие поперечной силы
                CalculateQ();
                // Расчет на действие моментов
                CalculateM();
            }
            else
            {
                Calculate_N();
            }
        }

        public override Dictionary<string, double> Results()
        {
            return new Dictionary<string, double>() { { "M_ult", M_ult }, { "Q_ult", Q_ult }, { "N_ult", N_ult } };
        }
    }
}

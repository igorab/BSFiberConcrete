using MathNet.Numerics.Integration;
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
        //public BSBeam_Rect beam { get; set; }        

        private Dictionary<string, double> m_Result;

        public BSFiberCalc_MNQ_Rect()
        {
            m_Beam = new BSBeam_Rect();
            m_Result = new Dictionary<string, double>();
        }

        public override string ImageCalc() => (Fissure) ? "Rect_N_out.PNG" : "Rect_N.PNG";
        
        public override void GetSize(double[] _t)
        {
            (b, h, l0) = (m_Beam.b, m_Beam.h, m_Beam.Length) = (_t[0], _t[1], _t[2]);

            A = m_Beam.Area();

            I = m_Beam.I_s();
            
            y_t = m_Beam.y_t;
        }

        /// <summary>
        /// Расчет внецентренно сжатых элементов (6.1.13)
        /// </summary>
        private new void Calculate_N()
        {
            base.Calculate_N();
            /*
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
            */
        }

        /// <summary>
        /// Расчет внецентренно сжатых сталефибробетонных
        /// элементов прямоугольного сечения с рабочей арматурой
        /// </summary>
        protected void Calculate_N_Rods()
        {                        
            Efb = m_Fiber.Efb;
            string info;

            // Расчетное остаточное остаточного сопротивления осевому растяжению
            Rfbt3 = R_fbt3();

            // Расчетные значения сопротивления  на сжатиие по B30 СП63
            Rfb = R_fb();

            // Расчетная высота сечения см
            double h0 = h - Rebar.a; 

            // Высота сжатой зоны
            double x = (N  + Rebar.Rs * Rebar.As - Rebar.Rsc * Rebar.A1s +  Rfbt3 * b * h )/ ((Rfb + Rfbt3) * b); 

            // относительной высоты сжатой зоны сталефибробетона
            double dzeta = x / h0;

            // характеристика сжатой зоны сталефибробетона, принимаемая для
            // сталефибробетона из тяжелого бетона классов до В60 включительно равной 0,8

            //Значения относительных деформаций арматуры для арматуры с физическим пределом текучести СП 63 п.п. 6.2.11
            double eps =  Rebar.Epsilon_s;

            double dz_R = Rebar.Dzeta_R(BetonType.Omega, BetonType.Eps_fb2);

            double x_denom = (Rfb + Rfbt3) * b + 2 * Rebar.Rs * Rebar.As / (h0 * (1 - dz_R));

            delta_e = Delta_e(e0 / m_Beam.h);

            fi1 = Fi1(); 

            k_b = K_b(fi1, delta_e);

            if (dzeta > dz_R)
            {
                x = (x_denom > 0) ? (N + Rebar.Rs * Rebar.As * ((1 + dz_R) / (1 - dz_R)) - Rebar.Rsc * Rebar.A1s + Rfbt3 * b * h) / x_denom : 0;
            }

            double alfa = Rebar.Es / Efb;

            double A_red = A + alfa * Rebar.As + alfa * Rebar.A1s;

            // Статический момент сечения фибробетона относительно растянутой грани
            double S = A * h / 2;
            // расстояние от центра тяжести приведенного сечения до растянутой в стадии эксплуатации грани Пособие к СП 52-102-2004 ф.2.12 (см)
            double y = (A_red>0)? ( S + alfa * Rebar.As * Rebar.a + alfa * Rebar.A1s * (h - Rebar.a1)) / A_red : 0;
            // расстояние от центра тяжести приведенного сечения до сжатой
            double ys = y - Rebar.a;
            // расстояние от центра тяжести приведенного сечения до растянутой арматуры
            double y1s = h - Rebar.a1 - y;

            double Is = Rebar.As*ys*ys + Rebar.A1s *y1s*y1s;

            // жесткость элемента в предельной по прочности стадии, определяемая по формуле (6.31)
            D = k_b * Efb * I + 0.7 * Rebar.Es * Is;

            // условная критическая сила, определяемая по формуле (6.24)
            Ncr = (Math.PI * Math.PI) * D / Math.Pow(l0, 2);

            // коэффициент, учитывающий влияние продольного изгиба (прогиба) элемента
            // на его несущую способность и определяемый по формуле(6.23)6.1.13
            eta =  1 / (1 - N / Ncr);

            // расстояние отточки приложения продольной силы N до центра тяжести сечения растянутой арматуры ф.6.33 см
            double e = e0 * eta + (h0 - Rebar.a) / 2;

            M_ult = Rfb * b * x * (h0 - 0.5 * x) - Rfbt3 * b * (h - x) * ((h - x) / 2 - Rebar.a) + Rebar.Rsc * Rebar.A1s * (h0 - Rebar.a1);

            N_ult = M_ult / e;

            if (N*e <= M_ult)
                info = "Прочность обеспечена";
            else
                info = "Прочность не обеспечена";

            Msg.Add(info);
                        
            M_ult = BSHelper.Kg2T(M_ult); 
            N_ult = BSHelper.Kg2T(N_ult);
        }

        /// <summary>
        /// Расчет внецентренно сжатых сталефибробетонных элементов без рабочей арматуры при
        /// расположении продольной сжимающей силы за пределами поперечного сечения элемента и внецентренно сжатых сталефибробетонных элементов без рабочей арматуры при расположении продольной
        /// сжимающей силы в пределах поперечного сечения элемента, в которых по условиям эксплуатации не
        /// допускается образование трещин
        /// </summary>
        private new void Calculate_N_Out()
        {
            base.Calculate_N_Out();            
        }

        /// <summary>
        /// Расчет элементов по полосе между наклонными сечениями
        /// </summary>
        private new void CalculateQ()
        {
            base.CalculateQ();
            /*
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
            */
        }
       
        /// <summary>
        ///  Расчет элементов по наклонным сечениям на действие моментов
        /// </summary>
        private new void CalculateM()
        {
            base.CalculateM();
            /*
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
            */
        }

        /// <summary>
        ///  Вычислить
        /// </summary>
        public override void Calculate()
        {
            if (Fissure)
            {
                Calculate_N_Out();
            }
            else if (Shear)
            {   // Расчет на действие поперечной силы
                CalculateQ();
                // Расчет на действие моментов
                CalculateM();
            }
            else if (UseRebar)
            {
                Calculate_N_Rods();
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

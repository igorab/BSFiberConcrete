using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BSFiberConcrete
{
    // Расчет по наклонным сечениям
    internal class BSFiberCalc_InclinedSections
    {
    }

    /// <summary>
    /// Прямоугольная балка
    /// </summary>
    public class BSFiberCalc_MNQ_Rect : BSFiberCalc_MNQ
    {
        public BSBeam_Rect beam { get; set; }

        public double Q_ult { get; set; }

        public double M_ult { get; set; }

        private double y_t;

        public BSFiberCalc_MNQ_Rect()
        {
            this.beam = new BSBeam_Rect();
        }

        public override void GetSize(double[] _t)
        {
            (b, h, l0) = (beam.b, beam.h, beam.Length) = (_t[0], _t[1], _t[2]);

            A = beam.Area();

            I = beam.I_s();

            y_t = beam.y_t();
        }

        private void Calculate0()
        {
            //Коэффициент, учитывающий влияние длительности действия нагрузки, определяют по формуле (6.27)
            fi1 = (M1 != 0) ? 1 + Ml1 / M1 : 1.0;

            //относительное значение эксцентриситета продольной силы
            delta_e = m_Fiber.e0 / beam.h;

            if (delta_e <= 0.15)
            { delta_e = 0.15; }
            else if (delta_e >= 1.5)
            { delta_e = 1.5; }

            // Коэфициент ф.(6.26)
            k_b = 0.15 / (fi1 * (0.3d + delta_e));

            // Модуль упругости сталефибробетона п.п. (5.2.7)
            Efb = m_Fiber.Eb * (1 - m_Fiber.mu_fv) + m_Fiber.Ef * m_Fiber.mu_fv;

            //жесткость элемента в предельной по прочности стадии,определяемая по формуле (6.25)
            D = k_b * Efb * I;

            // условная критическая сила, определяемая по формуле (6.24)
            Ncr = Math.PI * Math.PI * D / Math.Pow(l0, 2);

            eta = 1 / (1 - N / Ncr);

            Ab = beam.b * beam.h * (1 - 2 * m_Fiber.e0 * eta / beam.h);

            Rfb = Rfbn / Yb * Yb1 * Yb2 * Yb3 * Yb5;

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
        private void Calculate1()
        {
            //Коэффициент, учитывающий влияние длительности действия нагрузки (6.27)
            fi1 = (M1 != 0) ? 1 + Ml1 / M1 : 1.0;

            //относительное значение эксцентриситета продольной силы
            delta_e = m_Fiber.e0 / beam.h;

            // проверка условия 0.15 .. 1.5
            if (delta_e <= 0.15)
            { delta_e = 0.15; }
            else if (delta_e >= 1.5)
            { delta_e = 1.5; }

            // Коэфициент ф.(6.26)
            k_b = 0.15 / (fi1 * (0.3d + delta_e));

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
            double a = 4;

            // рабочая высота сечения по растянутой арматуре
            double h0 = h - a;

            // Расчетные значения сопротивления  на сжатиие по B30 СП63
            Rfb = Rfbn / Yb * Yb1 * Yb2 * Yb3 * Yb5;

            // Предельная перерезывающая сила по полосе между наклонными сечениями
            double _Q_ult = 0.3 * Rfb * b * h0;
            _Q_ult = BSHelper.Kg2T(Q_ult);

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
            double Rfbt = Rfbtn/ Yft * Yb1 * Yb5;
            double q_fbi = 0;

            List<double> lstQ_fb = new List<double>(); 

            foreach (double _c in lst_C)
            {
                q_fbi = 1.5 * Rfbt * b * h0*h0 / _c;

                if (q_fbi >= 2.5 * Rfbt * b * h0)
                    q_fbi = 2.5 * Rfbt * b * h0;
                else if (q_fbi <= 0.5 * Rfbt * b * h0)
                    q_fbi = 0.5 * Rfbt * b * h0;

                lstQ_fb.Add(q_fbi);
            }

            double Qfb = Math.Max(q_fbi, 0);

            // Максимальный шаг поперечной арматуры см
            double s_w_max = Rfbt * b * h0 * h0 / Q ;

            string res = "";
            if (Rebar.s_w <= s_w_max)
                res = "Условие выполнено, шаг удовлетворяет требованию 6.1.28";
            else
                res = "Условие не выполнено, требуется уменьшить шаг поперечной арматуры";

            // усилие в поперечной арматуре на единицу длины элемента
            double q_sw = Rebar.Rsw * Rebar.Asw / Rebar.s_w;

            // условие учета поперечной арматуры
            if (q_sw < 0.25 * Rfbt * b)
                q_sw = 0;

            double Qsw = 0;
            List<double> lst_Qsw = new List<double>();
            foreach (double _c in lst_C)
            {
                if (_c > c0_max)
                    Qsw = 0.75 * q_sw * c0_max;
                else
                    Qsw = 0.75 * q_sw * _c;

                lst_Qsw.Add(Qsw);
            }

            List<double> lst_Q_ult = new List<double>();
            for (int i = 0; i < lst_Qsw.Count; i ++)
            {
                lst_Q_ult.Add(lstQ_fb[i] + lst_Qsw[i]);
            }

            Q_ult = Qfb + Qsw;
        }

        private void InitC(ref List<double> _lst, double _from, double _to, double _dx )
        {
            double val = _from;
            while (val <= _to)
            {
                _lst.Add(val);                
                val += _dx;
            }
        }


        private void CalculateM()
        {
            // Растояние до цента тяжести арматуры растянутой арматуры, см
            double a = 4;

            // рабочая высота сечения по растянутой арматуре
            double h0 = h - a;

            // Нормативное остаточное сопротивления осевому растяжению кг/см2
            double Rfbt3 = Rfbt3n / Yft * Yb1 * Yb5;

            // Площадь растянутой арматуры см2
            double As = Rebar.As;

            // Расчетное сопротивление поперечной арматры  
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

            double Q_sw, M_sw;
            double M_fbt = 0;

            double Q_fbt3 = 0;

            //  Усилие в поперечной арматуре:
            List<double> lst_Q_sw = new List<double>();

            // момент, воспринимаемый поперечной арматурой, пересекающей наклонное сечение, относительно противоположного конца наклонного сечения
            List<double> lst_M_sw = new List<double>();

            foreach (double сi in С_x)
            {
                if (сi > c0_max)
                {
                    Q_sw = q_sw * сi;
                    M_sw = 0.5 * Q_sw * c0_max;
                }
                else
                {
                    Q_sw = q_sw * сi;
                    M_sw = 0.5 * Q_sw * сi;
                }

                M_fbt = 0.5 * Q_fbt3 * сi;

                lst_Q_sw.Add(Q_sw);
                lst_M_sw.Add(M_sw);
            }
            
            M_sw = lst_M_sw.Min();

            // усилие в продольной растянутой арматуре
            double N_s = Rebar.Rs * Rebar.As;

            double z_S = 0.9 * h0;

            // момент, воспринимаемый продольной арматурой, пересекающей наклонное сечение, относительно противоположного конца наклонного сечения
            double Ms = N_s * z_S;

            // расчет по наклонным сечениям; условие : M <= M_ult (Ms - продольная, Msw - поперечная, М_fbt - фибробетон)
            M_ult = Ms + M_sw + M_fbt;

            // предельный момент сечения (т*м)
            M_ult = BSHelper.Kg2T(M_ult);            
        }

        public override void Calculate()
        {
            if (UseRebar)
            {
                Calculate1();
            }
            else if (Shear)
            {
                CalculateQ();

                CalculateM();
            }
            else
            {
                Calculate0();
            }
        }

        public  Dictionary<string, double> Results()
        {
            return new Dictionary<string, double>() { { "Rfb", Rfb }, {"M_ult", M_ult },  { "N_ult", N_ult }, {"Q_ult", Q_ult}  };
        }


    }



}

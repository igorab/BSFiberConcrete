using ScottPlot.Hatches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFiberConcrete
{
    /// <summary>
    /// Расчеты на действие поперечной силы
    /// </summary>
    public class BSFiberCalc_QxQy : BSFiberCalc_MNQ
    {
        public override void SetSize(double[] _t)
        {
            (b, h) = (_t[0], _t[1]);
        }

        public override bool Calculate()
        {
            Calculate_Qx(b, h);

            Calculate_Qy(h ,b);

            return CalculateQxQy(b, h);            
        }
       
        protected override void Calculate_Qx(double _b, double _h)
        {
            // Растояние до цента тяжести арматуры растянутой арматуры, см
            double a = Rebar.a;

            // рабочая высота сечения по растянутой арматуре
            double h0 = _h - a;

            // Расчетные значения сопротивления  на сжатиие по B30 СП63
            Rfb = R_fb();

            // Предельная перерезывающая сила по полосе между наклонными сечениями
            double _Q_ult = 0.3 * Rfb * _b * h0; // (6.74)

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

                Qfb_i = 1.5d * Rfbt * _b * h0 * h0 / _c; // 6.76

                // условие на 0.5..2.5
                var Qult25 = 2.5 * Rfbt * _b * h0;
                var Qult05 = 0.5 * Rfbt * _b * h0;

                if (Qfb_i >= Qult25)
                {
                    Qfb_i = Qult25;
                }
                else if (Qfb_i <= Qult05)
                {
                    Qfb_i = Qult05;
                }

                lstQ_fb.Add(Qfb_i);
            }

            // Qfb - максимальная поперечная сила, воспринимаемая сталефибробетоном в наклонном сечении
            double Qfb = (lstQ_fb.Count > 0) ? lstQ_fb.Max() : 0;

            // Максимальный шаг поперечной арматуры см
            double s_w_max = (Qx > 0) ? Rfbt * _b * h0 * h0 / Qx : 0;

            string res;
            if (Rebar.Sw_X <= s_w_max)
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
            double q_sw = (Rebar.Sw_X != 0) ? Rebar.Rsw_X * Rebar.Asw_X / Rebar.Sw_X : 0; // 6.78 

            // условие учета поперечной арматуры
            if (q_sw < 0.25 * Rfbt * _b)
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

            //Коэффициент использования
            UtilRate_Qx = (Q_ult != 0) ? m_Efforts["Qx"] / Q_ult : 0;

            if (_Q_ult <= Q_ult)
            {
                res = "Перерезываюзщая сила превышает предельно допустимую в данном сечении";
                Msg.Add(res);
            }
            else
            {
                res = "Проверка по наклонному сечению на действие поперечной силы Qx пройдена";
                Msg.Add(res);
            }
        }

        /// <summary>
        /// Расчет элементов по полосе между наклонными сечениями
        /// </summary>
        protected override void Calculate_Qy(double _b, double _h)
        {
            if (m_Efforts["Qy"] == 0) return;

            // Растояние до цента тяжести арматуры растянутой арматуры, см
            double a = Rebar.a;

            // рабочая высота сечения по растянутой арматуре
            double h0 = _h - a;

            // Расчетные значения сопротивления  на сжатиие по B30 СП63
            Rfb = R_fb();

            // Предельная перерезывающая сила по полосе между наклонными сечениями
            double _Q_ult = 0.3 * Rfb * b * h0; // (6.74)

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
            double Qfb = (lstQ_fb.Count > 0) ? lstQ_fb.Max() : 0;

            // Максимальный шаг поперечной арматуры см
            double s_w_max = (Qy > 0) ? Rfbt * b * h0 * h0 / Qy : 0;

            string res;
            if (Rebar.Sw_Y <= s_w_max)
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
            double q_sw = (Rebar.Sw_Y != 0) ? Rebar.Rsw_Y * Rebar.Asw_Y / Rebar.Sw_Y : 0; // 6.78 

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

            //Коэффициент использования
            UtilRate_Qy = (Q_ult != 0) ? m_Efforts["Qy"] / Q_ult : 0;

            if (_Q_ult <= Q_ult)
            {
                res = "Поперечная сила превышает предельно допустимую в данном сечении";
                Msg.Add(res);
            }
            else
            {
                res = "Проверка по наклонному сечению на действие поперечной силы Qy пройдена";
                Msg.Add(res);
            }
        }

        public BSFiberCalc_QxQy()
        {
        }

        /// <summary>
        /// 6.1.27 Расчет изгибаемых элементов по бетонной полосе между наклонными сечениями
        /// </summary>
        /// <returns>Расчет выполнен успешно</returns>
        public bool CalculateQxQy(double _b, double _h)
        {
            bool ok = false;
            // поперечная сила в рассматриваемом нормальном сечении элемента
            double Q = Math.Sqrt(Qx*Qx + Qy*Qy);

            double _q = 0.3 * Rfb * _b * _h;

            if (Q <= _q) 
            { 
                ok = true;
            }

            return ok;
        }        
    }
}

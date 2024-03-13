using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace BSFiberConcrete
{    
    [DisplayName("Расчет элементов на действие сил и моментов")]
    public class BSFiberCalc_MNQ : IBSFiberCalculation
    {
        public List<string> Msg;

        #region attributes

        [DisplayName("Высота сечения, см"), Description("Geom")]
        public double h { get; protected set; }

        [DisplayName("Ширина сечения, см"), Description("Geom")]
        public double b { get; protected set; }

        [DisplayName("Радиус внешний, см"), Description("Geom")]
        public double r2 { get; protected set; }

        [DisplayName("Радиус внутренний, см"), Description("Geom")]
        public double r1 { get; protected set; }

        [DisplayName("Расчетная длинна элемента сп63 см"), Description("Geom")]
        public double l0 { get; protected set; }

        [DisplayName("Площадь сечения, см2"), Description("Geom")]
        public double A { get; protected set; }

        [DisplayName("Момент инерции сечения"), Description("Geom")]
        public double I { get; protected set; }

        [DisplayName("Продольное усилие, кг"), Description("Phys")]
        public double N { get; protected set; }

        [DisplayName("Cлучайный эксцентриситет, принимаемый по СП 63"), Description("Phys")]
        public double e0 { get; private set; }

        [DisplayName("Эксцентриситет приложения силы N"), Description("Phys")]
        public double e_N { get; protected set; }

        [DisplayName("Относительное значение эксцентриситета продольной силы"), Description("Phys")]
        public double delta_e { get; protected set; }

        [DisplayName("Момент от действия полной нагрузки"), Description("Phys")]
        public double M1 { get; protected set; }

        [DisplayName("Момент от действия постянных и длительных нагрузок нагрузок"), Description("Phys")]
        public double Ml1 { get; protected set; }

        [DisplayName("Коэффициент ф."), Description("Coef")]
        public double k_b { get; protected set; }

        [DisplayName("Коэффициент, учитывающий влияние длительности действия нагрузки  (6.27)"), Description("Coef")]
        public double fi1 { get; protected set; }

        [DisplayName("Коэффициент надежности Yft"), Description("Coef")]
        public double Yft { get; protected set; }
        [DisplayName("Коэффициент условия работы Yb"), Description("Coef")]
        public double Yb { get; protected set; }
        [DisplayName("Коэффициент условия работы Yb1"), Description("Coef")]
        public double Yb1 { get; protected set; }
        [DisplayName("Коэффициент условия работы Yb2"), Description("Coef")]
        public double Yb2 { get; protected set; }
        [DisplayName("Коэффициент условия работы Yb3"), Description("Coef")]
        public double Yb3 { get; protected set; }
        [DisplayName("Коэффициент условия работы Yb5"), Description("Coef")]
        public double Yb5 { get; protected set; }

        [DisplayName("Предельный момент"), Description("Res")]
        public double M_ult { get; protected set; }

        [DisplayName("Предельная поперечная сила"), Description("Res")]
        public double Q_ult { get; protected set; }

        [DisplayName("Предельная продольная сила"), Description("Res")]
        public double N_ult { get; protected set; }

        #endregion

        public Rebar Rebar {get; set;}
        public bool UseRebar { get; set; }
        public bool Fissure{ get; set; }
        public bool Shear { get; set; }

        protected Fiber m_Fiber;

        public BSMatRod MatRod { get; set; }
        public BSMatFiber MatFiber { get; set; }

        //Нормативное остаточное сопротивления осевому растяжению кг/см2 табл.2
        protected double Rfbt3n;

        //Расчетное остаточное остаточного сопротивления осевому растяжению 
        protected double Rfbt3;

        protected double B;
        protected double y_t;
        protected double Rfbn;
        protected double fi = 0.9;
        protected double Ef; //для фибры из тонкой низкоуглеродистой проволоки МП п.п  кг/см2 
        protected double Eb; //Начальный модуль упругости бетона-матрицы B30 СП63
        //коэффициент фибрового армирования по объему
        protected double mu_fv;
        //Модуль упругости сталефибробетона п.п. (5.2.7)
        protected double Efb;
        // жесткость элемента в предельной по прочности стадии,определяемая по формуле (6.25)
        protected double D;
        //условная критическая сила, определяемая по формуле (6.24)
        protected double Ncr;
        //коэффициент, учитывающий влияние продольного изгиба (прогиба) элемента на его несущую способность и определяемый по формуле(6.23)
        protected double eta;
        //площадь сжатой зоны бетона ф. (6.22)
        protected double Ab;
        //поперечная сила
        protected double Q;

        protected double Rfb;
        protected double Rfbtn;
      
        // параметры продольной арматуры
        protected double[] l_rebar;
        // параметры поперечной арматуры
        protected double[] t_rebar;

        protected Dictionary<string, double> m_Efforts;

        protected BSBeam m_Beam;

        public BSFiberCalc_MNQ()
        {
            m_Beam = new BSBeam();
            m_Efforts = new Dictionary<string, double>();
            Msg = new List<string>();
        }

        /// <summary>
        ///  Расчетная схема , рисунок
        /// </summary>
        /// <returns>Наименование файла</returns>
        public virtual string ImageCalc() => "";
        
        public double Delta_e(double _d_e)
        {
            double d_e;

            if (_d_e <= 0.15)
            {
                d_e = 0.15;
            }
            else if (_d_e >= 1.5)
            {
                d_e = 1.5;
            }
            else
                d_e = _d_e;

            return d_e;
        }

        protected double R_fb() => Rfbn / Yb * Yb1 * Yb2 * Yb3 * Yb5;

        protected double R_fbt() => Rfbtn / Yft * Yb1 * Yb5;

        protected double R_fbt3() => Rfbt3n / Yft * Yb1 * Yb5;

        public double Dzeta_R(double omega) => omega / (1 + MatRod.epsilon_s() / MatFiber.e_b2);

        public double K_b(double _fi1, double _delta_e) => 0.15 / (_fi1 * (0.3d + _delta_e));

        public static BSFiberCalc_MNQ Construct(BeamSection _BeamSection)
        {
            BSFiberCalc_MNQ fiberCalc;

            if (BeamSection.Rect == _BeamSection)
            {
                fiberCalc = new BSFiberCalc_MNQ_Rect();
            }
            else if (BeamSection.Ring == _BeamSection)
            {
                fiberCalc = new BSFiberCalc_MNQ_Ring();
            }
            else if (BeamSection.IBeam == _BeamSection)
            {
                fiberCalc = new BSFiberCalc_MNQ_IT();
            }
            else
            {
                throw new Exception("Сечение балки не определено");
            }

            return fiberCalc;
        }

        public void GetFiberParamsFromJson(Fiber _fiber)
        {
            m_Fiber = _fiber;
            //e0 = _fiber.e0;
            Ef = _fiber.Ef;
            Eb = _fiber.Eb;
            mu_fv = _fiber.mu_fv;
        } 
        
        // параметры арматуры
        public void GetRebarParams(double[] _l_rebar, double[] _t_rebar)
        {
            l_rebar = _l_rebar;
            t_rebar = _t_rebar;
            
            // Шаг поперечной арматуры
            Rebar.s_w = _t_rebar[1];
        }
        
        protected void Calculate_N()
        {
            //Коэффициент, учитывающий влияние длительности действия нагрузки, определяют по формуле (6.27)
            fi1 = (M1 != 0) ? 1 + Ml1 / M1 : 1.0;

            //относительное значение эксцентриситета продольной силы
            delta_e = Delta_e(m_Fiber.e0 / m_Beam.h);

            // Коэфициент ф.(6.26)
            k_b = K_b(fi1, delta_e);

            // Модуль упругости сталефибробетона п.п. (5.2.7)
            Efb = m_Fiber.Eb * (1 - m_Fiber.mu_fv) + m_Fiber.Ef * m_Fiber.mu_fv;

            //жесткость элемента в предельной по прочности стадии,определяемая по формуле (6.25)
            D = k_b * Efb * I;

            // условная критическая сила, определяемая по формуле (6.24)
            Ncr = Math.PI * Math.PI * D / Math.Pow(l0, 2);

            eta = (Ncr!=0) ? 1 / (1 - N / Ncr) : 0;

            Ab = m_Beam.b * m_Beam.h * (1 - 2 * m_Fiber.e0 * eta / m_Beam.h);

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

        /// <summary>
        ///  Расчет внецентренно сжатых сталефибробетонных элементов без рабочей арматуры при
        /// расположении продольной сжимающей силы за пределами поперечного сечения элемента и внецентренно сжатых сталефибробетонных элементов без рабочей арматуры при расположении продольной
        /// сжимающей силы в пределах поперечного сечения элемента, в которых по условиям эксплуатации не
        /// допускается образование трещин
        /// </summary>
        protected void Calculate_N_Out()
        {
            //Коэффициент, учитывающий влияние длительности действия нагрузки (6.27)
            fi1 = (M1 != 0) ? 1 + Ml1 / M1 : 1.0;

            //относительное значение эксцентриситета продольной силы
            delta_e = Delta_e(e0 / m_Beam.h);

            // Коэфициент ф.(6.26)
            k_b = K_b(fi1, delta_e);

            // Модуль упругости сталефибробетона п.п. (5.2.7)
            Efb = m_Fiber.Eb * (1 - m_Fiber.mu_fv) + m_Fiber.Ef * m_Fiber.mu_fv;

            //Модуль упругости арматуры
            double? Es = Rebar?.Es;
            // Момент инерции продольной арматуры соответственно относительно оси, проходящей через центр тяжести поперечного сечения элемента
            double? ls = Rebar?.ls;
            //жесткость элемента в предельной по прочности стадии, определяемая по формуле (6.31)
            D = k_b * Efb * I + 0.7 * (Es??0) * (ls??0);

            // условная критическая сила, определяемая по формуле (6.24)
            Ncr = Math.PI * Math.PI * D / Math.Pow(m_Beam.Length, 2);

            //коэффициент, учитывающий влияние продольного изгиба элемента на его несущую способность (6.23) 6.1.13
            eta = 1 / (1 - N / Ncr);

            Ab = m_Beam.b * m_Beam.h * (1 - 2 * m_Fiber.e0 * eta / m_Beam.h);

            //Расчетные значения сопротивления осевому растяжению
            double Rfbt = R_fbt();
            
            double denom = A / I * e0 * eta * m_Beam.y_t - 1; 

            // Предельная сила сечения
            N_ult = 1/denom * Rfbt * A;
            
            string info;

            if (N <= N_ult)
                info = "Прочность обеспечена";
            else
                info = "Прочность не обеспечена";

            Msg.Add(info);

            N_ult = BSHelper.Kg2T(N_ult);
        }

        protected void InitC(ref List<double> _lst, double _from, double _to, double _dx)
        {
            double val = _from;
            while (val <= _to)
            {
                _lst.Add(val);
                val += _dx;
            }
        }

        protected void CalculateQ()
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
            double Qfb = (lstQ_fb.Count > 0) ? lstQ_fb.Max() : 0;

            // Максимальный шаг поперечной арматуры см
            double s_w_max = (Q > 0) ? Rfbt * b * h0 * h0 / Q : 0;

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

            if (_Q_ult <= Q_ult)
            {
                res = "Перерезываюзщая сила превышает предельно допустимую в данном сечении";
                Msg.Add(res);
            }

        }

        protected void CalculateM()
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

            double Q_fbt3 = (c_min!=0) ? 1.5d * Rfbt3 * b * h0 * h0 / c_min : 0;

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

                Q_fbt3 = (ci != 0) ? 1.5d * Rfbt3 * b * h0 * h0 / ci : 0;

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

            M_ult = (lst_M_ult.Count > 0) ? lst_M_ult.Min() : 0;

            // предельный момент сечения (т*м)
            M_ult = BSHelper.Kg2T(M_ult);
        }

        public virtual void Calculate() {}

        public Dictionary<string, double> GeomParams()
        {
            throw new NotImplementedException();
        }

        public virtual void GetParams(double[] _t)
        {
            (Rfbt3n, Rfbn, Yb, Yft, Yb1, Yb2, Yb3, Yb5, B) = (_t[0], _t[1], _t[2], _t[3], _t[4], _t[5], _t[6], _t[7], _t[8]);
            Rfbtn = Rfbt3n;
        }

        public virtual void GetSize(double[] _t) {}

        public void GetEfforts(Dictionary<string, double> _efforts)
        {
            m_Efforts = _efforts;

            double Mx = m_Efforts["Mx"];
            //Момент от действия полной нагрузки
            M1 = m_Efforts["My"];
            //Продольное усилие кг
            N = m_Efforts["N"];
            // Поперечная сила
            Q = m_Efforts["Q"];
           
            //Момент от действия постянных и длительных нагрузок нагрузок
            Ml1 = m_Efforts["Ml"];
            // Эксцентриситет приложения N
            e_N = m_Efforts["eN"];
            e0 = e_N;
        }

        public virtual Dictionary<string, double> Results()
        {
            return new Dictionary<string, double>() { { "Rfb", Rfb }, { "N_ult", N_ult } };
        }
    }
}

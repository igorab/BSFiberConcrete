using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace BSFiberConcrete
{    
    [DisplayName("Расчет элементов на действие сил и моментов")]
    public class BSFiberCalc_MNQ : IBSFiberCalculation
    {
        public List<string> Msg;

        private string m_ImgCalc;

        #region attributes

        [DisplayName("Высота сечения, [см]"), Description("Geom")]
        public double h { get; protected set; }

        [DisplayName("Ширина сечения, [см]"), Description("Geom")]
        public double b { get; protected set; }

        [DisplayName("Радиус внешний, [см]"), Description("Geom")]
        public double r2 { get; protected set; }

        [DisplayName("Радиус внутренний, [см]"), Description("Geom")]
        public double r1 { get; protected set; }

        [DisplayName("Расчетная длина элемента, [см]"), Description("Geom")]
        public double LngthCalc0 { get; protected set; }

        [DisplayName("Площадь сечения, [см2]"), Description("Geom")]
        public double A { get; protected set; }

        [DisplayName("Момент инерции сечения, [см4]"), Description("Geom")]
        public double I { get; protected set; }
        
        [DisplayName("Модуль упругости сталефибробетона, [кг/см2]"), Description("Phys")]
        public double Efb { get; protected set; }
        
        [DisplayName("Нормативное сопротивление осевому сжатию, [кг/см2]"), Description("Phys")]
        public double Rfbn { get => MatFiber.Rfbn; }
        
        [DisplayName("Нормативное сопротивление осевому растяжению, [кг/см2]"), Description("Phys")]
        public double Rfbtn { get => MatFiber.Rfbtn; }
        
        [DisplayName("Нормативное остаточное сопротивления осевому растяжению, [кг/см2]"), Description("Phys")]
        public double Rfbt3n { get => MatFiber.Rfbt3n; }

        [DisplayName("Продольное усилие, [кг]"), Description("Phys")]
        public double N { get; protected set; }

        [DisplayName("Cлучайный эксцентриситет, принимаемый по СП 63, e0"), Description("Phys")]
        public double e0 { get; private set; }

        [DisplayName("Эксцентриситет приложения силы N, eN"), Description("Phys")]
        public double e_N { get; protected set; }

        [DisplayName("Относительное значение эксцентриситета продольной силы"), Description("Phys")]
        public double delta_e { get; protected set; }

        [DisplayName("Изгибающий момент"), Description("Phys")]
        public double My { get; protected set; }

        [DisplayName("Доля постоянной нагрузки в общей нагрузке на элемент"), Description("Phys")]
        public double Ml1toM1 { get; protected set; }

        [DisplayName("Коэффициент ф.")]
        public double k_b { get; protected set; }

        [DisplayName("Коэффициент, учитывающий влияние длительности действия нагрузки  (6.27)")]
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

        [DisplayName("Предельный момент, [кг*см]"), Description("Res")]
        public double M_ult { get; protected set; }

        [DisplayName("Предельная поперечная сила, [кг]"), Description("Res")]
        public double Q_ult { get; protected set; }

        [DisplayName("Предельная продольная сила, [кг]"), Description("Res")]
        public double N_ult { get; protected set; }

        [DisplayName("Коэффициент использования по усилию")]
        public double UtilRate_N { get; protected set; }

        public string DN(Type _T, string _property) => _T.GetProperty(_property).GetCustomAttribute<DisplayNameAttribute>().DisplayName;
        #endregion
        public BetonType BetonType { get; set; }
        /// <summary>
        /// Свойства арматуры: продольная/поперечная
        /// </summary>
        public Rebar Rebar {get; set;}
        public bool UseRebar { get; set; }

        /// <summary>
        /// Сила вне сечения
        /// </summary>
        public bool N_Out{ get; set; }
        /// <summary>
        /// Сила внутри сечения
        /// </summary>
        public bool N_In { get; set; }
        public bool Shear { get; set; }

        protected Fiber m_Fiber;

        public BSMatRod MatRod { get; set; }
        public BSMatFiber MatFiber { get; set; }
                
        protected double Rfb;

        //Расчетное остаточное остаточного сопротивления осевому растяжению 
        protected double Rfbt3;

        protected double B;
        protected double y_t;        
        protected double fi;
        protected double Ef; //для фибры из тонкой низкоуглеродистой проволоки МП п.п  кг/см2 
        protected double Eb; //Начальный модуль упругости бетона-матрицы B30 СП63
        //коэффициент фибрового армирования по объему
        protected double mu_fv;
       
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
        protected double Qy;
        // параметры продольной арматуры
        protected double[] l_rebar;
        // параметры поперечной арматуры
        protected double[] t_rebar;

        public Dictionary<string, double> m_Efforts;

        protected BSBeam m_Beam;

        public BSFiberCalc_MNQ()
        {
            m_Beam = new BSBeam();
            m_Efforts = new Dictionary<string, double>();
            Msg = new List<string>();
            // TODO refactoring
            fi = 0.9;
        }

        /// <summary>
        ///  Расчетная схема , рисунок
        /// </summary>
        /// <returns>Наименование файла</returns>
        public virtual string ImageCalc() => !string.IsNullOrEmpty(m_ImgCalc) ? m_ImgCalc : "";
                        
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

        public double Fi1() => (Ml1toM1 <= 1) ? 1 + Ml1toM1 : 2.0;

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
            else if (BeamSection.IBeam == _BeamSection ||
                     BeamSection.TBeam == _BeamSection ||
                     BeamSection.LBeam == _BeamSection)                     
            {
                fiberCalc = new BSFiberCalc_MNQ_IT();
            }
            else
            {
                throw new Exception("Сечение балки не определено");
            }

            return fiberCalc;
        }

        /// <summary>
        ///  получить значения из настроек по умолчанию (Из json - файла)
        /// </summary>
        /// <param name="_fiber"></param>
        public void InitFiberParams(Fiber _fiber)
        {
            m_Fiber = (Fiber)_fiber.Clone();

            Ef = m_Fiber.Ef;
            Eb = m_Fiber.Eb;
            mu_fv = m_Fiber.mu_fv;

            Efb = m_Fiber.Efib != 0 ? m_Fiber.Efib :  m_Fiber.Efb;
        } 
        
        // параметры арматуры
        public void SetRebarParams(double[] _l_rebar, double[] _t_rebar)
        {
            l_rebar = _l_rebar;
            t_rebar = _t_rebar;
            
            // Шаг поперечной арматуры
            //Rebar.s_w = _t_rebar[1];
        }

        /// <summary>
        /// Информация о результате проверки сечения на действие продольной силы
        /// </summary>                
        public void InfoCheckN(double _N_ult)
        {
            string info;

            if (m_Efforts["N"] <= _N_ult)
                info = "Сечение прошло проверку на действие продольной силы.";
            else
                info = "Сечение не прошло проверку на действие силы N.";
            Msg.Add(info);            
        }

        protected void Calculate_N()
        {
            //Коэффициент, учитывающий влияние длительности действия нагрузки, определяют по формуле (6.27)
            fi1 = Fi1();

            //относительное значение эксцентриситета продольной силы
            delta_e = Delta_e(m_Fiber.e_tot / m_Beam.h);

            // Коэфициент ф.(6.26)
            k_b = K_b(fi1, delta_e);

            // Модуль упругости сталефибробетона п.п. (5.2.7)
            Efb = MatFiber.Efb; //  m _Fiber.Eb * (1 - m_Fiber.mu_fv) + m_Fiber.Ef * m_Fiber.mu_fv;

            //жесткость элемента в предельной по прочности стадии,определяемая по формуле (6.25)
            D = k_b * Efb * I;

            // условная критическая сила, определяемая по формуле (6.24)
            Ncr = Math.PI * Math.PI * D / Math.Pow(LngthCalc0, 2);

            eta = (Ncr!=0) ? 1 / (1 - N / Ncr) : 0;

            Ab = m_Beam.b * m_Beam.h * (1 - 2 * m_Fiber.e_tot * eta / m_Beam.h);

            Rfb = R_fb();

            N_ult = fi * Rfb * A;

            double flex = LngthCalc0 / h;

            if (m_Fiber.e_tot <= h / 30d && flex <= 20)
            {
                N_ult = fi * Rfb * A;
            }
            else
            {
                N_ult = Rfb * Ab;
            }

            //Коэффициент использования
            UtilRate_N = (N_ult != 0) ? m_Efforts["N"] / N_ult : 0;
            
            InfoCheckN(N_ult);            
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
            fi1 = (Ml1toM1 <=1) ? 1 + Ml1toM1 : 2.0;

            //относительное значение эксцентриситета продольной силы
            delta_e = Delta_e(m_Fiber.e_tot / m_Beam.h);

            // Коэфициент ф.(6.26)
            k_b = K_b(fi1, delta_e);

            // Модуль упругости сталефибробетона п.п. (5.2.7)
            Efb = MatFiber.Efb;   // m_Fiber.Eb * (1 - m_Fiber.mu_fv) + m_Fiber.Ef * m_Fiber.mu_fv;

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

            Ab = m_Beam.b * m_Beam.h * (1 - 2 * m_Fiber.e_tot * eta / m_Beam.h);

            //Расчетные значения сопротивления осевому растяжению
            double Rfbt = R_fbt();
            
            double denom = A / I * m_Fiber.e_tot * eta * m_Beam.y_t - 1; 

            // Предельная сила сечения
            N_ult = (denom != 0) ? 1/denom * Rfbt * A : 0;

            //Коэффициент использования
            UtilRate_N = (N_ult != 0) ? m_Efforts["N"] / N_ult : 0;

            string info;

            if (N <= N_ult)
                info = "Прочность на действие продольной силы обеспечена";
            else
                info = "Прочность не обеспечена. Продольная сила превышает допустимое значение.";

            Msg.Add(info);
            
        }

        // жесткость элемента в предельной по прочности стадии, определяемая по формуле (6.31)
        protected double D_stiff(double _Is) => k_b* Efb * I + Rebar.k_s * Rebar.Es * _Is;

        protected double DStiff(double _I, double _Is) => k_b * Efb * _I + Rebar.k_s * Rebar.Es * _Is;

        // условная критическая сила, определяемая по формуле (6.24)
        protected  double N_cr(double _D) => (Math.PI* Math.PI) * _D / Math.Pow(LngthCalc0, 2);

        // коэффициент, учитывающий влияние продольного изгиба (прогиба) элемента
        // на его несущую способность и определяемый по формуле(6.23)6.1.13
        protected double Eta(double _N, double _Ncr) => 1 / (1 - _N / _Ncr);


        /// <summary>
        /// Расчет внецентренно сжатых сталефибробетонных
        /// элементов прямоугольного сечения с рабочей арматурой
        /// </summary>
        protected void Calculate_N_Rods()
        {                        
            string info;

            // Расчетное остаточное остаточного сопротивления осевому растяжению
            Rfbt3 = R_fbt3();

            // Расчетные значения сопротивления  на сжатиие по B30 СП63
            Rfb = R_fb();

            // Расчетная высота сечения см
            double h0 = h - Rebar.a;

            // Высота сжатой зоны
            double x = (N + Rebar.Rs * Rebar.As - Rebar.Rsc * Rebar.As1 + Rfbt3 * b * h) / ((Rfb + Rfbt3) * b);

            // относительной высоты сжатой зоны сталефибробетона
            double dzeta = x / h0;

            // характеристика сжатой зоны сталефибробетона, принимаемая для
            // сталефибробетона из тяжелого бетона классов до В60 включительно равной 0,8

            //Значения относительных деформаций арматуры для арматуры с физическим пределом текучести СП 63 п.п. 6.2.11
            double eps = Rebar.Epsilon_s;

            double dz_R = Rebar.Dzeta_R(BetonType.Omega, BetonType.Eps_fb2);

            double x_denom = (Rfb + Rfbt3) * b + 2 * Rebar.Rs * Rebar.As / (h0 * (1 - dz_R));

            delta_e = Delta_e(m_Fiber.e_tot / m_Beam.h);

            fi1 = Fi1();

            k_b = K_b(fi1, delta_e);

            if (dzeta > dz_R)
            {
                x = (x_denom > 0) ? (N + Rebar.Rs * Rebar.As * ((1 + dz_R) / (1 - dz_R)) - Rebar.Rsc * Rebar.As1 + Rfbt3 * b * h) / x_denom : 0;
            }

            double alfa = Rebar.Es / Efb;

            double A_red = A + alfa * Rebar.As + alfa * Rebar.As1;

            // Статический момент сечения фибробетона относительно растянутой грани
            double S = A * h / 2;
            // расстояние от центра тяжести приведенного сечения до растянутой в стадии эксплуатации грани Пособие к СП 52-102-2004 ф.2.12 (см)
            double y = (A_red > 0) ? (S + alfa * Rebar.As * Rebar.a + alfa * Rebar.As1 * (h - Rebar.a1)) / A_red : 0;
            // расстояние от центра тяжести приведенного сечения до сжатой
            double ys = y - Rebar.a;
            // расстояние от центра тяжести приведенного сечения до растянутой арматуры
            double y1s = h - Rebar.a1 - y;
            // момент инерции
            double Is = Rebar.As * ys * ys + Rebar.As1 * y1s * y1s;
            // жесткость элемента в предельной по прочности стадии, определяемая по формуле (6.31)
            D = DStiff(I, Is);
            // условная критическая сила, определяемая по формуле (6.24)
            Ncr = N_cr(D);
            // коэффициент, учитывающий влияние продольного изгиба (прогиба) элемента
            // на его несущую способность и определяемый по формуле(6.23)6.1.13
            eta = Eta(N, Ncr);
            // расстояние отточки приложения продольной силы N до центра тяжести сечения растянутой арматуры ф.6.33 см
            double e = e0 * eta + (h0 - Rebar.a) / 2 + e_N;

            M_ult = Rfb * b * x * (h0 - 0.5 * x) - Rfbt3 * b * (h - x) * ((h - x) / 2 - Rebar.a) + Rebar.Rsc * Rebar.As1 * (h0 - Rebar.a1);

            N_ult = M_ult / e;

            if (N * e <= M_ult)
                info = "Прочность обеспечена";
            else
                info = "Прочность не обеспечена";

            Msg.Add(info);
            //
            //M_ult = BSHelper.Kg2T(M_ult);
            //N_ult = BSHelper.Kg2T(N_ult);
            //
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

        /// <summary>
        /// Расчет элементов по полосе между наклонными сечениями
        /// </summary>
        protected void CalculateQ()
        {
            m_ImgCalc = "Incline_Q.PNG";

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
            double _Rfbt3 = R_fbt3();

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
            if (q_sw < 0.25 * _Rfbt3 * b)
                q_sw = 0;

            double c_min = h0;
            double c_max = 4 * h0;
            double c0_max = 2 * h0;
            List<double> С_x = new List<double>();

            InitC(ref С_x, c_min, c_max, 1);

            double Q_sw,
                   M_sw; // момент, воспр поперечной арматурой
            double M_fbt = 0; // момент, воспр сталефибробетоном

            double Q_fbt3 = (c_min!=0) ? 1.5d * _Rfbt3 * b * h0 * h0 / c_min : 0;

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

                Q_fbt3 = (ci != 0) ? 1.5d * _Rfbt3 * b * h0 * h0 / ci : 0;

                if (Q_fbt3 >= 2.5d * _Rfbt3 * b * h0)
                    Q_fbt3 = 2.5d * _Rfbt3 * b * h0;
                else if (Q_fbt3 <= 0.5d * _Rfbt3 * b * h0)
                    Q_fbt3 = 0.5d * _Rfbt3 * b * h0;

                M_fbt = 0.5 * Q_fbt3 * ci;

                lst_Q_fbt3.Add(Q_fbt3);
                lst_M_fbt.Add(M_fbt);

                // расчет по наклонным сечениям; условие : M <= M_ult (Ms - продольная, Msw - поперечная, М_fbt - фибробетон)
                M_ult = Ms + M_sw + M_fbt; // 6.79

                lst_M_ult.Add(M_ult);
            }

            M_ult = (lst_M_ult.Count > 0) ? lst_M_ult.Min() : 0;            
        }

        public virtual bool Calculate() 
        {
            return true;
        }

        public Dictionary<string, double> GeomParams()
        {
            throw new NotImplementedException();
        }

        public virtual void SetParams(double[] _t)
        {
            // TODO Refactoring
            (_, _, Yft, Yb, Yb1, Yb2, Yb3, Yb5, B) = (_t[0], _t[1], _t[2], _t[3], _t[4], _t[5], _t[6], _t[7], _t[8]);            
        }

        public virtual void GetSize(double[] _t) {}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_efforts"></param>
        /// <returns>полный эксцентриситет </returns>
        public double GetEfforts(Dictionary<string, double> _efforts)
        {
            double e_tot; // полный эксцентриситет приложения силы

            m_Efforts = new Dictionary<string, double>(_efforts);
            
            //Момент от действия полной нагрузки
            My = m_Efforts["My"];

            //Продольное усилие кг
            N = m_Efforts["N"];

            // Поперечная сила
            Q = m_Efforts["Q"];
            Qy = m_Efforts["Qy"];

            //Момент от действия постянных и длительных нагрузок нагрузок
            Ml1toM1 = m_Efforts["Ml"];

            // случайный эксцентриситет
            e0 = m_Efforts["e0"];

            // Эксцентриситет приложения силы N
            e_N = m_Efforts["eN"];           
            
            // эксцентриситет от момента
            double e_MN = (N != 0) ? My / N : 0;
            e_N += e_MN;

            e_tot = e0 + e_N + e_MN;
            m_Fiber.e_tot = e_tot;

            return e_tot;
        }

        public virtual Dictionary<string, double> Results()
        {
            return new Dictionary<string, double>() 
            { 
                { "Rfb", Rfb }, 
                { "N_ult", N_ult } 
            };
        }
    }
}

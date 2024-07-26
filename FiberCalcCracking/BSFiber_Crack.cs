using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BSFiberConcrete
{
    /// <summary>
    /// Класс для расчета стальфибробетоннных элементов по образованияю трещин
    /// </summary>
    internal class BSFiberCalc_Cracking : IBSFiberCalculation
    {
        public List<string> Msg { get; private set; }

        // заданные нагрузки
        public double Mx { get; set; }
        public double My { get; set; }
        // продольная сила от внешней нагрузки
        public double N { get; set; }

        // балка
        public BSBeam Beam
        {
            get { return m_Beam; }
            set
            {
                m_Beam = value;
                m_Rods = value.Rods;
            }
        }

        /// <summary>
        /// Расстановка стержней арматуры
        /// </summary>
        public List<BSRod> Rods
        {
            get { return m_Rods; }
            set { m_Rods = value; }
        }

        private List<BSElement> m_BElem;
        private List<BSRod> m_Rods;


        // свойства бетона
        public BSMatFiber MatFiber { get { return m_Fiber; } set { m_Fiber = value; } }
        // свойства арматуры
        public BSMatRod MatRebar { get { return m_Rod; } set { m_Rod = value; } }

        private BSBeam m_Beam { get; set; }

        private BSMatFiber m_Fiber;
        private BSMatRod m_Rod;

        public BeamSection typeOfBeamSection;



        [DisplayName("Нормативное сопротивления осевому растяжению, Rfbt,n, кг/см2")]
        public double Rfbtn { get => MatFiber.Rfbtn; }

        [DisplayName("Сопротивление сталефибробетона осевому растяжению, Rfbt, кг/см2")]
        public double Rfbt { get => R_fbt(); }

        [DisplayName("Нормативное остаточное сопротивления осевому растяжению Rfbt3,n , кг/см2")]
        public double Rfbt3n { get => MatFiber.Rfbt3n; }

        [DisplayName("Остаточное сопротивление сталефибробетона осевому растяжению, Rfbt3, кг/см2")]
        public double Rfbt3 { get => R_fbt3(); }

        [DisplayName("Числовая характеристика класса фибробетона по прочности на осевое сжатие, B")]
        public double B { get => MatFiber.B; }

        [DisplayName("Нормативное значение сопротивления сталефибробетона на осевое сжатие Rfb,n , кг/см2")]
        public double Rfbn { get => MatFiber.Rfbn; }

        [DisplayName("Расчетные значения сопротивления  на сжатиие по B30 СП63, кг/см2")]
        public double Rfb { get => R_fb(); }

        // ??
        public double Gamma(double _B) => 1.73 - 0.005 * (_B - 15);

        //protected double omega = BSMatFiber.omega;

        [BSFiberCalculation(Name = "Коэффициент надежности для расчета по предельным состояниям первой группы при назначении класса сталефибробетона по прочности на растяжение")]
        protected double Yft;
        [BSFiberCalculation(Name = "Коэффициенты условия работы")]
        protected double Yb;
        protected double Yb1;
        protected double Yb2;
        protected double Yb3;
        protected double Yb5;

        [BSFiberCalculation(Name = "Площадь сжатой зоны бетона")]
        private double Ab;
        [BSFiberCalculation(Name = "случайный эксцентриситет, принимаемый по СП 63.13330")]
        private double e0;

        public virtual Dictionary<string, double> Coeffs
        {
            get
            {
                return new Dictionary<string, double>() { { "Yft", Yft }, { "Yb", Yb }, { "Yb1", Yb1 }, { "Yb2", Yb2 }, { "Yb3", Yb3 }, { "Yb5", Yb5 } };
            }
        }


        public virtual Dictionary<string, double> PhysParams
        {
            get
            {
                return new Dictionary<string, double> { { "Rfbt3n", Rfbt3n }, { "B", B }, { "Rfbn", Rfbn } };
            }
        }

        public Dictionary<string, double> Efforts
        {
            get { return m_Efforts; }
            set { m_Efforts = new Dictionary<string, double>(value); }
        }

        protected Dictionary<string, double> m_Efforts;

        // Расчетные значения сопротивления на сжатиие по B30 СП63
        public double R_fb() => (Yb != 0) ? Rfbn / Yb * Yb1 * Yb2 * Yb3 * Yb5 : 0;

        //Расчетное остаточное сопротивление осевому растяжению R_fbt
        public double R_fbt() => (Yft != 0) ? Rfbtn / Yft * Yb1 * Yb5 : 0;

        //Расчетное остаточное сопротивление осевому растяжению R_fbt3
        public double R_fbt3() => (Yft != 0) ? Rfbt3n / Yft * Yb1 * Yb5 : 0;

        public string DN(Type _T, string _property) => _T.GetProperty(_property).GetCustomAttribute<DisplayNameAttribute>().DisplayName;

        public static string DsplN(Type _T, string _property) => new BSFiberCalculation().DN(_T, _property);

        // ??
        public BSFiberCalc_Cracking(double _Mx = 0, double _My = 0, double _N = 0)
        {
            Mx = _Mx;
            My = _My;
            N = _N;

            m_Fiber = new BSMatFiber();
            m_Rod = new BSMatRod();

            Msg = new List<string>();
        }

        /// <summary>
        /// Возвращает результаты расчета геометрических характеристик балки
        /// </summary>
        /// <returns>Описание геометрии балки</returns>
        public virtual Dictionary<string, double> GeomParams()
        {
            return new Dictionary<string, double>() { };
        }

        /// <summary>
        /// Физические свойства материала
        /// </summary>        
        public virtual Dictionary<string, double> PhysicalParameters()
        {
            Dictionary<string, double> phys = new Dictionary<string, double>
            {
                { DN(typeof(BSFiberCalculation), "Rfbt3n"), Rfbt3n },
                { DN(typeof(BSFiberCalculation), "B"), B },
                { DN(typeof(BSFiberCalculation), "Rfbn"), Rfbn }
            };
            return phys;
        }

        /// <summary>
        /// Принимает характерные размеры сечения
        /// </summary>
        /// <param name="_t">Массив - размеры сечения</param>
        public virtual void GetSize(double[] _t)
        {
        }

        public virtual void GetParams(double[] _t)
        {
        }

        public virtual bool Validate()
        {
            return true;
        }

        public virtual bool Calculate()
        {
            return true;
        }



        /// <summary>
        /// Определение момента образования трещин
        /// </summary>
        /// <returns></returns>
        public bool CalculateUltM()
        {
            if (!Validate())
                return false;


            // Продольная сила расположенная в центре тяжести приведенного элемента
            // Сжатие       - "+"
            // Растяжение   - "-"
            // N

            #region Характеристики материала
            // Площадь растянутой арматуры
            double A_s;
            // Площадь сжатой арматуры
            double A_1s;
            // Расстояние до центра тяжести растянутой арматуры
            double a;
            // Расстояние до центра тяжести сжатой арматуры
            double a_1;
            // модуль упрогости арматуры
            double Es;

            // нормативное остаточное сопротивление осевому растяжению для bft3i
            double R_fbt_ser;
            // модуль упругости фибробетона
            double Efb;
            // Класс бетона
            //double B = 30;
            //double Y = 1.73 - 0.005 * (B - 15);
            double Y = 1.67;
            #endregion

            #region Геометрические характеристики сечения 
            // Площадь сечения
            double A;
            // момент инерции сечения 
            double I;
            // статический момент сечения фибробетона
            double S;
            #endregion

            ///
            A_s = MatRebar.As;
            A_1s = MatRebar.As1;
            a = MatRebar.a_s;
            a_1 = MatRebar.a_s1;
            Es = MatRebar.Es;
            ///
            Efb = MatFiber.Efb;
            R_fbt_ser = MatFiber.Rfbt_ser;

            ///
            A = Beam.Area();
            I = Beam.Jy();
            S = Beam.Sy();


            #region Расчет
            // Коэф Привендения арматуры к стальфибробетону
            double alpha = Es / Efb;                                                                                //  (6.113)
            // Площадь приведенного поперечного сечения элемента 
            double A_red = A + A_s * alpha + A_1s * alpha;                                                          //  (6.112)

            double Y_t;
            double I_red;
            if (typeOfBeamSection == BeamSection.Ring)
            {
                BSBeam_Ring tmpBeam = (BSBeam_Ring)Beam;
                Y_t = tmpBeam.r2;
                double r_m = tmpBeam.r_m;

                double SS = (A_s + A_1s) / Math.PI * 2 * r_m;
                double Is = Math.PI / 64 * (Math.Pow(2 * (r_m + SS / 2), 4) - Math.Pow(2 * (r_m - SS / 2), 4));
                I_red = I + alpha * Is;
            }
            else
            {
                double height = Beam.Height;

                // Статический момент площади приведенного поперечного сечеиня элемента
                // относительо наиболе ерастянутого волокна стальфибробетона
                double S_t_red = S + alpha * A_s * alpha + alpha * A_1s * (height - a_1);
                // Расстояние от центра тяжести приведенного сечения до расстянутой в стадии эксплуатауции грани
                Y_t = S_t_red / A_red;                                                                           //  (6.114)

                // расстояние от центра тяжести приведенного сечения до расстянутой арматуры
                double Y_s = Y_t - a;
                // расстояние от центра тяжести приведенного сечения до сжатой арматуры
                double Y_1s = height - Y_t - a_1;

                // Момент инерции растянутой арматуры
                double I_s = A_s * Y_s * Y_s;
                // Момент инерции сжатой арматуры
                double I_1s = A_1s * Y_1s * Y_1s;

                // в сп формула без упоминания alpha
                I_red = I + alpha * I_s + alpha * I_1s;                                                          // (6.131) не (6.111)
            }

            double W_red = I_red / Y_t;                                                                               // (6.109)

            double e_x = W_red / A_red;                                                                              // (6.110)

            double W_pl = Y * W_red;                                                                                // (6.108)

            double M_crc = R_fbt_ser * W_pl + N * e_x;                                                              // (6.107)
            #endregion

            return true;
        }
















        /// <summary>
        /// Расчет ширины раскрытия трещин 
        /// </summary>
        /// <returns></returns>
        public bool CalculateWidthCrack()
        {
            if (!Validate())
                return false;









            // Продольная сила расположенная в центре тяжести приведенного элемента
            // Сжатие       - "+"
            // Растяжение   - "-"
            // N
            #region Характеристики материала
            // Площадь растянутой арматуры
            double A_s;
            // Площадь сжатой арматуры
            double A_1s;
            // Расстояние до центра тяжести растянутой арматуры
            double a;
            // Расстояние до центра тяжести сжатой арматуры
            double a_1;
            // модуль упрогости арматуры
            double Es;

            // нормативное остаточное сопротивление осевому растяжению для bft3i
            double R_fbt_ser;
            // модуль упругости фибробетона
            double Efb;
            // Класс бетона
            //double B = 30;
            //double Y = 1.73 - 0.005 * (B - 15);
            double Y = 1.67;
            #endregion

            #region Геометрические характеристики сечения 
            // Площадь сечения
            double A;
            // момент инерции сечения 
            double I;
            // статический момент сечения фибробетона
            double S;
            #endregion

            ///
            A_s = MatRebar.As;
            A_1s = MatRebar.As1;
            a = MatRebar.a_s;
            a_1 = MatRebar.a_s1;
            Es = MatRebar.Es;
            ///
            Efb = MatFiber.Efb;
            R_fbt_ser = MatFiber.Rfbt;

            
            ///
            A = Beam.Area();
            I = Beam.Jy();
            S = Beam.Sy();


            #region Расчет
            // Коэф Привендения арматуры к стальфибробетону
            double alpha = Es / Efb;                                                                                //  (6.113)
            // Площадь приведенного поперечного сечения элемента 
            double A_red = A + A_s * alpha + A_1s * alpha;                                                          //  (6.112)

            double Y_t;
            double I_red;
            if (typeOfBeamSection == BeamSection.Ring)
            {
                BSBeam_Ring tmpBeam = (BSBeam_Ring)Beam;
                Y_t = tmpBeam.r2;
                double r_m = tmpBeam.r_m;

                double SS = (A_s + A_1s) / Math.PI * 2 * r_m;
                double Is = Math.PI / 64 * (Math.Pow(2 * (r_m + SS / 2), 4) - Math.Pow(2 * (r_m - SS / 2), 4));
                I_red = I + alpha * Is;
            }
            else
            {
                double height = Beam.Height;

                // Статический момент площади приведенного поперечного сечеиня элемента
                // относительо наиболе ерастянутого волокна стальфибробетона
                double S_t_red = S + alpha * A_s * alpha + alpha * A_1s * (height - a_1);
                // Расстояние от центра тяжести приведенного сечения до расстянутой в стадии эксплуатауции грани
                Y_t = S_t_red / A_red;                                                                           //  (6.114)

                // расстояние от центра тяжести приведенного сечения до расстянутой арматуры
                double Y_s = Y_t - a;
                // расстояние от центра тяжести приведенного сечения до сжатой арматуры
                double Y_1s = height - Y_t - a_1;

                // Момент инерции растянутой арматуры
                //double I_s = A_s * Y_s * Y_s;
                // Момент инерции сжатой арматуры
                //double I_1s = A_1s * Y_1s * Y_1s;

                // в сп формула без упоминания alpha
                //I_red = I + alpha * I_s + alpha * I_1s;                                                          // (6.131) не (6.111)
            }

            //double W_red = I_red / Y_t;                                                                               // (6.109)

            //double e_x = W_red / A_red;                                                                              // (6.110)

            //double W_pl = Y * W_red;                                                                                // (6.108)

            //double M_crc = R_fbt_ser * W_pl + N * e_x;                                                              // (6.107)





            // Коэффициент, учитывающий продолжительность действия нагрузки
            double fi_1 = 1.4;
            // Коэф, учитывающий характер нагружения
            double fi_3 = 1;
            // Коэф, учитывающий неравномерное распределение относительных
            // деформаций растянутой арматуры между трещинами
            double psi_s = 1;

            // коэф, учитывающий профиль продольной раматуры, для гладкой арматуры: 
            double fi_2 = 0.8;
            double fi_13 = 0.8;



            // предельно-допустимая ширина раскрытия трещин
            // Принимается в зависимости класса арматуры
            double a_crc_ult = 0.3;








            double epsilon_fb1_red = 0.00015;
            double epslion_fbt2 = 0.004;

            double d_s = 12;
            // диаметр арматуры достать бы
            // MatRebar.

            // длина фибры
            double l_f = 50;
            // диаметр фибры
            double d_f = 0.8;

            // коэф фиброго армирования по объему
            double Mu_fv = 0.0174;


            //R_fb_n = MatFiber.Rfbn;
            double R_fb_n = 300;


            // Приведенный модуль деформации сжатого стальфибробетона, учитьывающий неупругие деформации сжатого стальффиброрбетона
            double E_fb_red = R_fb_n / epsilon_fb1_red;
            //  Коэф. приведения арматуры
            double alpha_s1 = Es / E_fb_red;
            double alpha_s2 = alpha_s1;
            // Приведенный модуль деформации сжатого стальфибробетона, учитывающий неупругие деформации сжатого стальфибробетона
            double E_fbt_red = R_fbt_ser / epslion_fbt2;
            // Коэф. стальфибробетона растянутой зоны к стальфибробетону сжатой зоны
            double alpha_fbt = E_fbt_red / E_fb_red;



            double b;
            double h_0;

            if (typeOfBeamSection == BeamSection.Rect)
            {
                BSBeam_Rect tmpBeam = (BSBeam_Rect)Beam;
                b = tmpBeam.b;
                h_0 = tmpBeam.h;



            }
            else
            {
                b = 0;
                h_0 = 0;
            }
            double Mu_s = A_s / (b * h_0);
            double Mu_1s = A_1s / (b * h_0);


            // для каждого типа сечени своя формаула
            // Высота сжатой зоны
            double Xm = (h_0 / (1 - alpha_fbt)) * (Math.Sqrt(Math.Pow(Mu_s * alpha_s2 + Mu_1s * alpha_s1 + alpha_fbt, 2) +
                (1 - alpha_fbt) * (2 * Mu_s * alpha_s2 + 2 * Mu_1s * alpha_s1 * (a_1 / h_0) + alpha_fbt))) -
                (Mu_s * alpha_s2 + Mu_1s * alpha_s1 + alpha_fbt);
            // формула 6.140

            double y_c = Xm; 

            
            // В ДВУХ ФОРМУЛАХ НИЖЕ ДОЛЖНО БЫТЬ h  вместо h_0 !!!!
            // момент инерции сжатой зоны
            double I_fb = b * Math.Pow(y_c, 3) / 12 + b * y_c * Math.Pow(h_0 / 2 + y_c / 2,2);
            // момент инерции растянутой зоны
            double I_fbt = b * Math.Pow(h_0 - y_c, 3) / 12 + b * (h_0 - y_c)  * Math.Pow(h_0 / 2 + ( h_0 - y_c) / 2, 2);

            double I_1s = A_1s * Math.Pow(y_c - a_1, 2);
            double I_s = A_s * Math.Pow(h_0 - y_c - a, 2); //  В ФОРМУЛЕ НУЖНО ИСПОЛЬЗОВАТЬ h, а не h_0 !!!!
            // момент инерции
            I_red = I_fb + I_fbt * alpha_fbt + I_s * alpha_s2 + I_1s * alpha_s1;

            // Напряжение в растянутой арматуре изгибаемых элементов
            double sigma_s = Mx * (h_0 - y_c) / I_red * alpha_s1;



            double k_f;

            if (l_f / d_f < 50)
                k_f = 50;
            else if (l_f / d_f >= 50 || l_f / d_f <= 100)
                k_f = 50 * d_f / l_f;
            else
            {
                //(l_f / d_f > 100)
                k_f = 0.5;
            }

            // базовое расстояние между смежными нормальными трещинами
            double l_s = k_f * (50 + 0.5 * fi_2 * fi_13 * d_s / Mu_fv);

            // Ширина раскрытия трещин
            double a_crc_1 = fi_1 * fi_3 * psi_s * sigma_s / Es * l_s;


            #endregion

            return true;
        }





        public virtual Dictionary<string, double> Results()
        {
            return new Dictionary<string, double>() { };
        }

        /// <summary>
        /// Информация о результате проверки сечения на действие изгибающего момента
        /// </summary>                
        public void InfoCheckM(double _M_ult)
        {
            string info;

            if (m_Efforts["My"] <= _M_ult)
                info = "Сечение прошло проверку на действие изгибающего момента.";
            else
                info = "Сечение не прошло проверку. Рассчитанный предельный момент сечения превышает предельно допустимый.";
            Msg.Add(info);

            info = "Расчет успешно выполнен!";
            Msg.Add(info);
        }

        /// <summary>
        /// Расчет прочности сечения
        /// </summary>
        /// <param name="_profile">Профиль сечения</param>
        /// <param name="_reinforcement">Используется ли арматура</param>
        /// <returns>Экземпляр класса расчета</returns>
        public static BSFiberCalculation construct(BeamSection _profile, bool _reinforcement = false)
        {
            switch (_profile)
            {
                case BeamSection.TBeam:
                case BeamSection.IBeam:
                    if (_reinforcement)
                        return new BSFiberCalc_IBeamRods();
                    else
                        return new BSFibCalc_IBeam();
                case BeamSection.Ring:
                    return new BSFibCalc_Ring();
                case BeamSection.Rect:
                    if (_reinforcement)
                        return new BSFiberCalc_RectRods();
                    else
                        return new BSFibCalc_Rect();
            }

            return new BSFiberCalculation();
        }
        //public bool Calculate()
        //{
        //    throw new NotImplementedException();
        //}

        //public Dictionary<string, double> GeomParams()
        //{
        //    throw new NotImplementedException();
        //}

        //public void GetParams(double[] _t = null)
        //{
        //    throw new NotImplementedException();
        //}

        //public void GetSize(double[] _t = null)
        //{
        //    throw new NotImplementedException();
        //}

        //public Dictionary<string, double> Results()
        //{
        //    throw new NotImplementedException();
        //}
    }






    //public class Efforts
    //{
    //    public double My;
    //    public double Mx;
    //    public double N;
    //    public double Q;
    //    /// <summary>
    //    /// значение полученное с формы
    //    /// BeamAndEfforts.lengthBeam имеет больший приоритет
    //    /// </summary>
    //    public double lengthBeam;

    //    public BeamAndEfforts beamAndEfforts;


    //}


    //public class BeamAndEfforts
    //{
    //    /// <summary>
    //    /// тип нагрузки на балку
    //    /// </summary>
    //    public LoadBeamType typeOfLoad;
    //    /// <summary>
    //    /// тип закрепления бакли
    //    /// </summary>
    //    public SupportBeamType typeOfSupport;
    //    /// <summary>
    //    /// Данные для построения эпюры
    //    /// </summary>
    //    public DiagramResult result;

    //    /// <summary>
    //    /// длина балки
    //    /// </summary>
    //    public double lengthBeam;
    //    /// <summary>
    //    /// Величина приложенный силы
    //    /// </summary>
    //    public double forceValue;
    //    /// <summary>
    //    /// удаленность от левого края балки до первой точки приложения силы
    //    /// </summary>
    //    public double x1;
    //    /// <summary>
    //    /// удаленность от левого края второй точки приложения силы на балку
    //    /// </summary>
    //    public double x2;

    //}


    [Description("Тип нагрузки")]
    public enum LoadBeamType
    {
        [Description("Нагрузка не определена")]
        None = 0,
        [Description("Сосредоточенная сила")]
        Concentrated = 1,
        [Description("Распределенная нагрузка")]
        Distributed = 2,
    }


    [Description("Тип закрепления балки")]
    public enum SupportBeamType
    {
        [Description("Не определено")]
        None = 0,
        [Description("Жестко защемленная - Свободная")]
        Fixed_No = 1,
        [Description("Свободная - Жестко защемленная")]
        No_Fixed = 2,
        [Description("Жестко защемленная - Жестко защемленная")]
        Fixed_Fixed = 3,
        [Description("Жестко защемленная - Шарнирно подвижная")]
        Fixed_Movable = 4,
        [Description("Шарнирно подвижная - Жестко защемленная")]
        Movable_Fixed = 5,
        [Description("Шарнирно неподвижная - Шарнирно подвижная")]
        Pinned_Movable = 6,
    }
}

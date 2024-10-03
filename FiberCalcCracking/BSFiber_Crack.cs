using ScottPlot.Statistics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace BSFiberConcrete
{
    /// <summary>
    /// Класс для расчета стальфибробетоннных элементов по образованияю трещин
    /// </summary>
    internal class BSFiberCalc_Cracking : IBSFiberCalculation
    {
        public List<string> Msg { get; private set; }

        private BSBeam m_Beam { get; set; }

        private BSMatFiber m_Fiber;
        private BSMatRod m_Rod;

        public BeamSection typeOfBeamSection;


        // балка
        public BSBeam Beam
        {
            get { return m_Beam; }
            set { m_Beam = value; }
        }
        // свойства бетона
        public BSMatFiber MatFiber { get { return m_Fiber; } set { m_Fiber = value; } }
        // свойства арматуры
        public BSMatRod MatRebar { get { return m_Rod; } set { m_Rod = value; } }


        //public virtual Dictionary<string, double> Coeffs
        //{
        //    get
        //    {
        //        return new Dictionary<string, double>() { { "Yft", MatFiber.Yft }, { "Yb", MatFiber.Yb }, { "Yb1", MatFiber.Yb1 }, { "Yb2", MatFiber.Yb2 }, { "Yb3", MatFiber.Yb3 }, { "Yb5", MatFiber.Yb5 } };
        //    }
        //}

        //public virtual Dictionary<string, double> PhysParams
        //{
        //    get
        //    {
        //        return new Dictionary<string, double> { { "Rfbt3n", MatFiber.Rfbt3n }, { "B", MatFiber.B }, { "Rfbn", MatFiber.Rfbn } };
        //    }
        //}

        public Dictionary<string, double> Efforts;


        // заданные нагрузки
        //[DisplayName("Mx [кг*см]")]
        public double Mx;
        public double My;
        // продольная сила от внешней нагрузки
        public double N;

        /// случайный эксцентриситет
        double e0;
        /// эксцентриситет от продольной силы
        double eN;

        private double _M_crc;
        private double _a_crc;

        // Результаты расчета
        public DataTable resultTable;

        public Dictionary<string, double> resultDictionary;


        public string DN(Type _T, string _property) => _T.GetProperty(_property).GetCustomAttribute<DisplayNameAttribute>().DisplayName;

        public static string DsplN(Type _T, string _property) => new BSFiberCalculation().DN(_T, _property);

        public BSFiberCalc_Cracking(Dictionary<string, double> MNQ)
        {
            this.Efforts = MNQ;

            MNQ.TryGetValue("Mx",out Mx);
            MNQ.TryGetValue("My", out My);
            MNQ.TryGetValue("N", out N);
            MNQ.TryGetValue("e0", out e0);
            MNQ.TryGetValue("eN", out eN);

            m_Fiber = new BSMatFiber();
            m_Rod = new BSMatRod();

            Msg = new List<string>();
            resultDictionary = new Dictionary<string, double>();

            //CreateResTable();
            //AddRowInResTable("", "Mx", Mx, "кг*см2");
            //AddRowInResTable("", "N", N, "кг");
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
                { DN(typeof(BSFiberCalculation), "Rfbt3n"), MatFiber.Rfbt3n },
                { DN(typeof(BSFiberCalculation), "B"), MatFiber.B },
                { DN(typeof(BSFiberCalculation), "Rfbn"), MatFiber.Rfbn }
            };
            return phys;
        }



        # region наследие интерфейса IBSFiberCalculation
        /// <summary>
        /// Принимает характерные размеры сечения
        /// </summary>
        /// <param name="_t">Массив - размеры сечения</param>
        public virtual void GetSize(double[] _t)
        {
        }

        public virtual void SetParams(double[] _t)
        {
        }

        public virtual bool Validate()
        {
            return true;
        }

        public virtual bool Calculate()
        {
            if (MatRebar.Reinforcement == false)
            {                
                Msg.Add($"Расчет по предельным состояниям второй группы выполняется только для армированных элементов. \n" +
                    $" Для получения результатов необходимо указать флажок 'Армирование'."); 
                return false;
            }

            if (MatRebar.As <= 0 && MatRebar.As1 <= 0)
            {
                Msg.Add("Для получения результатов по предельным состояниям второй группы необходимо задать площадь арматуры.");
                return false;
            }

            CalculateUltM();

            if (typeOfBeamSection != BeamSection.Rect)
            {
                Msg.Add("Расчет ширины раскрытия трещины выполняется только для прямоугольного сечения");
                return false;
            }

            CalculateWidthCrack();

            return true;
        }

        public virtual Dictionary<string, double> Results()
        {
            return resultDictionary;
        }


        # endregion 



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


            double B = m_Fiber.B;



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
            double Y = 1.73 - 0.005 * (B - 15);
            //double Y = 1.67;
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

                double SS = (A_s + A_1s) / (Math.PI * 2 * r_m);
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

            double e_x_sum = e_x + e0 + eN;


            double W_pl = Y * W_red;                                                                                // (6.108)

            double M_crc = R_fbt_ser * W_pl + N * e_x;                                                              // (6.107)
            #endregion



            _M_crc = M_crc;

            //AddRowInResTable("Момент образования трещин с учетом неупругих деформаций растянутого стальфибробетона", "Mcrc", M_crc, "кг*см2");

            resultDictionary.Add("Момент образования трещин с учетом неупругих деформаций растянутого сталефибробетона, Mcrc [кг*см2]", M_crc);
            resultDictionary.Add("Коэффициент использования по второй группе предельных состояний", My/M_crc);
            resultDictionary.Add("(Значение для отладки) Площадь приведенного поперечного сечения элемента, A_red [см3]", A_red);
            resultDictionary.Add("(Значение для отладки) Расстояние от центра тяжести приведенного сечения до расстянутой в стадии эксплуатауции грани, Y_t [см]", Y_t);
            resultDictionary.Add("(Значение для отладки) Момент инерции приведенного поперечного сечения, I_red [см4]", I_red);

            resultDictionary.Add("(Значение для отладки) Момент сопротивления,  W_red [см3]", W_red);
            resultDictionary.Add("(Значение для отладки) Расчетное расстояние e_x, [см]", e_x);
            resultDictionary.Add("(Значение для отладки) Cуммарное расстояние Σe_x, [см]", e_x_sum);
            resultDictionary.Add("(Значение для отладки) Упругопластический момент сопротивления сечения для крайнего растянутого волокна, W_pl [см4]", W_pl);

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
            R_fbt_ser = MatFiber.Rfb_ser;


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
            double a_crc_ult = 0.03;



            double epsilon_fb1_red = 0.0015;
            double epslion_fbt2 = 0.004;

            double d_s = 12 / 100;
            // диаметр арматуры достать бы
            // MatRebar.

            // длина фибры
            double l_f = 50;
            // диаметр фибры
            double d_f = 0.8;

            // коэф фиброго армирования по объему
            double Mu_fv = 0.0174;


            double R_fb_n = MatFiber.Rfbn;


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
            double h;
            double h_0;

            if (typeOfBeamSection == BeamSection.Rect)
            {
                BSBeam_Rect tmpBeam = (BSBeam_Rect)Beam;
                b = tmpBeam.b;
                h = tmpBeam.h;
                h_0 = h - a_1;
            }
            else
            {
                b = 0;
                h = 0;
                h_0 = 0;
            }
            double Mu_s = A_s / (b * h_0);
            double Mu_1s = A_1s / (b * h_0);


            // для каждого типа сечени своя формаула
            // Высота сжатой зоны
            double Xm = (h_0 / (1 - alpha_fbt)) * ((Math.Sqrt(Math.Pow(Mu_s * alpha_s2 + Mu_1s * alpha_s1 + alpha_fbt, 2) +
                (1 - alpha_fbt) * (2 * Mu_s * alpha_s2 + 2 * Mu_1s * alpha_s1 * (a_1 / h_0) + alpha_fbt))) -
                (Mu_s * alpha_s2 + Mu_1s * alpha_s1 + alpha_fbt));
            // формула 6.140

            double y_c = Xm;


            // момент инерции сжатой зоны
            double I_fb = b * Math.Pow(y_c, 3) / 12 + b * y_c * Math.Pow(h / 2 - y_c / 2, 2);
            // момент инерции растянутой зоны
            double I_fbt = b * Math.Pow(h - y_c, 3) / 12 + b * (h - y_c) * Math.Pow(h / 2 - (h - y_c) / 2, 2);

            double I_1s = A_1s * Math.Pow(y_c - a_1, 2);
            double I_s = A_s * Math.Pow(h - y_c - a, 2);
            // момент инерции
            I_red = I_fb + I_fbt * alpha_fbt + I_s * alpha_s2 + I_1s * alpha_s1;

            // Напряжение в растянутой арматуре изгибаемых элементов
            double sigma_s = (My * (h_0 - y_c) / I_red + N/ A_red) * alpha_s1;


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
            double a_crc = fi_1 * fi_3 * psi_s * sigma_s / Es * l_s;

            _a_crc = a_crc;
            #endregion


            // f (класс арматуры и продолжительности нагрузки)acrc_ult

            //AddRowInResTable("Предельно допустимая ширина раскрытия трещин", "acrc_ult", a_crc_ult, "мм");
            //AddRowInResTable("Ширина раскрытия трещин от действия внешней нагрузки", "acrc", a_crc, "мм");


            resultDictionary.Add("Ширина раскрытия трещин от действия внешней нагрузки, a_crc [см]", a_crc);
            resultDictionary.Add("Предельно допустимая ширина раскрытия трещин, acrc_ult [см]", a_crc_ult);

            //resultDictionary.Add("(Значение для отладки) Площадь приведенного поперечного сечения элемента (2), A_red [см3]", A_red);
            //resultDictionary.Add("(Значение для отладки) Расстояние от центра тяжести приведенного сечения до расстянутой в стадии эксплуатауции грани (2), Y_t [см]", Y_t);
            //resultDictionary.Add("(Значение для отладки) Момент инерции приведенного поперечного сечения (2), I_red [см4]", I_red);
            resultDictionary.Add("(Значение для отладки) Высота сжатой зоны, x_m [см]", Xm);
            resultDictionary.Add("(Значение для отладки) Момент инерции сжатой зоны, I_fb [см4]", I_fb);
            resultDictionary.Add("(Значение для отладки) Момент инерции растянутой зоны, I_fbt [см4]", I_fbt);
            resultDictionary.Add("(Значение для отладки) Напряжение в растянутой арматуре, σ_s [кг/см2]", sigma_s);
            resultDictionary.Add("(Значение для отладки) базовое расстояние между смежными нормальными трещинами, l_s [см]", l_s);
            return true;
        }


        /// <summary>
        /// Создается форма, выводится результат из ResultTable
        /// </summary>
        public void ShowResult()
        {
            CalcCrackingForm ccForm= new CalcCrackingForm(resultTable);
            ccForm.Show();
        }


        /// <summary>
        /// Создается новая таблица параметра ResultTable
        /// </summary>
        protected void CreateResTable()
        {
            resultTable = new DataTable();
            resultTable.Columns.Add("Описание");
            resultTable.Columns.Add("Параметр");
            resultTable.Columns.Add("Значение");
            resultTable.Columns.Add("Ед. Измерения");
        }


        /// <summary>
        /// Добавление строки в таблицу ResultTable
        /// </summary>
        /// <param name="description"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="units"></param>
        protected void AddRowInResTable(string description, string name, double value, string units)
        {
            double valueRound =Math.Round(value,3);
            DataRow row = resultTable.NewRow();
            row["Описание"] = description;
            row["Параметр"] = name;
            row["Значение"] = valueRound.ToString();
            row["Ед. Измерения"] = units;
            resultTable.Rows.Add(row);
        }

    }



    //[Description("Тип нагрузки")]
    //public enum LoadBeamType
    //{
    //    [Description("Нагрузка не определена")]
    //    None = 0,
    //    [Description("Сосредоточенная сила")]
    //    Concentrated = 1,
    //    [Description("Распределенная нагрузка")]
    //    Distributed = 2,
    //}


    //[Description("Тип закрепления балки")]
    //public enum SupportBeamType
    //{
    //    [Description("Не определено")]
    //    None = 0,
    //    [Description("Жестко защемленная - Свободная")]
    //    Fixed_No = 1,
    //    [Description("Свободная - Жестко защемленная")]
    //    No_Fixed = 2,
    //    [Description("Жестко защемленная - Жестко защемленная")]
    //    Fixed_Fixed = 3,
    //    [Description("Жестко защемленная - Шарнирно подвижная")]
    //    Fixed_Movable = 4,
    //    [Description("Шарнирно подвижная - Жестко защемленная")]
    //    Movable_Fixed = 5,
    //    [Description("Шарнирно неподвижная - Шарнирно подвижная")]
    //    Pinned_Movable = 6,
    //}
}

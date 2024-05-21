using System;
using System.ComponentModel;


namespace BSFiberConcrete
{
    public interface IMaterial
    {
        string Name { get; }        
    }

    public interface INonlinear
    {        
        double Eps_StD(double _e);
        double Eps_StateDiagram(double e_s);
    }

    /// <summary>
    ///  Свойства обычного бетона
    /// </summary>
    public class BSMatConcrete : IMaterial, INonlinear
    {
        public string Name => "Бетон";
        public string BT { get; set; }

        [DisplayName("Сопротивление на сжатие")]
        public double Rb { get; set; }

        [DisplayName("Модуль сжатия бетона")]
        public double Eb { get; set; }

        public double e_b0;
        public double e_b1;
        public double e_b2;

        // Диаграмма состояния
        public  double Eps_StateDiagram(double e_b)
        {
            double sigma_b = Rb;
            double sigma_b1 = 0.6 * Rb;

            e_b1 = sigma_b1 / Eb;

            if (0 <= e_b  && e_b <= e_b1 )
            {
                sigma_b = Eb * e_b;
            }
            else if (e_b1 < e_b && e_b < e_b0)
            {
                sigma_b = ((1 - sigma_b1/Rb)*(e_b - e_b1)/ (e_b0 - e_b1) + sigma_b1/Rb )* Rb;
            }
            else if (e_b0 <= e_b && e_b <= e_b2)
            {
                sigma_b = Rb;                    
            }

            return sigma_b;
        }

        public double Eps_StD(double _e)
        {
            double sgm = 0;

            if (0 <= _e && _e < e_b1)
            {
                sgm = Eb * _e;
            }
            else if (e_b1 <= _e && _e < e_b2) 
            {
                sgm = Rb;
            }

            return sgm;
        }

        public BSMatConcrete()
        {
            BT = "B30";
        }
    }

    /// <summary>
    ///  Материал стержня арматуры
    /// </summary>
    public class BSMatRod : IMaterial, INonlinear
    {
        public string Name => "Сталь";

        // Класс
        public string RCls { get; set; }

        /// <summary>
        /// модуль упругости, МПа
        /// </summary>
        public double Es { get; set; }

        // Расчетное сопротивление растяжению кг/см2
        public double Rs { get; set; }

        // Расчетное сопротивление сжатию кг/см2
        public double Rsc { get; set; }

        // Площадь растянутой арматуры
        public double As { get; set; }

        // Площадь сжатой арматуры
        public double As1 { get; set; }

        /// <summary>
        /// коэффициент упругости
        /// </summary>
        public double Nju_s { get; set; }

        public double Eps_s_ult { get; set; }

        /// <summary>
        /// Значения относительных деформаций арматуры для арматуры с физическим пределом текучести СП 63 п.п. 6.2.11
        /// </summary>        
        public double epsilon_s() => Es != 0 ? Rs / Es : 0; 

        public double e_s0 { get; set; }
        public double e_s2 { get; set; }

        public BSMatRod()
        {

        }
        
        public BSMatRod(double _Es)
        {
            Es = _Es;
        }

        // диаграмма состояния
        public double Eps_StateDiagram(double e_s)
        {
            double sigma_s = Rs;
            double sigma_s1 = 0.6 * Rs;
            e_s0 = 1;
            double e_s1 = sigma_s1 / Es;
            e_s2 = 1;

            if (0 <= e_s && e_s <= e_s1)
            {
                sigma_s = Es * e_s;
            }
            else if (e_s1 < e_s && e_s < e_s0)
            {
                sigma_s = ((1 - sigma_s1 / Rs) * (e_s - e_s1) / (e_s0 - e_s1) + sigma_s1 / Rs) * Rs;
            }
            else if (e_s0 <= e_s && e_s <= e_s2)
            {
                sigma_s = Rs;
            }

            return sigma_s;
        }

        // Диаграмма состояния двухлинейная
        public double Eps_StD(double _e)
        {
            double sgm = 0;

            if (0 < _e && _e < e_s0)
            {
                sgm = Es * _e;
            }
            else if (e_s0 <= _e && _e <= e_s2)
            {
                sgm = Rs;
            }

            return sgm;
        }
    }

    /// <summary>
    /// Свойства фибробетона
    /// </summary>
    public class BSMatFiber : IMaterial, INonlinear
    {
        public string Name => "Фибробетон";

        // Начальный модуль упругости бетона-матрицы B30 СП63
        public double Eb { get => Efb; }

        //для фибры из тонкой низкоуглеродистой проволоки МП п.п  кг/см2
        public double Ef { get; set; }

        /// <summary>
        /// Начальный модуль упругости
        /// </summary>
        public double Efb { get; set; }

        /// <summary>
        /// Коэффициент упругости
        /// </summary>
        public double Nu_fb { get; set; }

        public double Eps_fb_ult { get; set; }

        // Класс бетона
        public string BTCls { get; set; }

        [DisplayName("Числовая характеристика класса фибробетона по прочности на осевое сжатие")]
        public double B { get; set; }

        [DisplayName("Нормативное сопротивление сталефибробетона осевому сжатию Rfbn")]
        public double Rfbn { get; set; }

        [DisplayName("Расчетное сопротивление сталефибробетона осевому сжатию Rfbn")]
        public double Rfb { get; set; }

        [DisplayName("Нормативное сопротивление сталефибробетона осевому растяжению Rfbt")]
        public double Rfbtn { get; set; }

        [DisplayName("Расчетное сопротивление сталефибробетона осевому растяжению Rfbt")]
        public double Rfbt { get; set; }

        [DisplayName("Остаточное нормативное сопротивление на растяжение Rfbt2,n")]
        public double Rfbt2n { get; set; }

        [DisplayName("Остаточное расчетное сопротивление на растяжение Rfbt2")]
        public double Rfbt2 { get; set; }

        [DisplayName("Остаточное нормативное сопротивление осевому растяжению Rfbt3,n")]
        public double Rfbt3n { get; set; }

        [DisplayName("Остаточное расчетное сопротивление осевому растяжению Rfbt3")]
        public double Rfbt3 { get; set; }
        
        public double R_fb { get => Rfbn; }
        public double e_b1_red { get; set; }
        public double e_b1 { get; set; }

        // относительные деформации сжатого сталефибробетона при напряжениях R/b,
        // принимаемые по указаниям СП 63.13330 как для обычного бетона
        public double e_b2 { get; set; }

        public double Eb_red { get => (e_b1 !=0) ? R_fb / e_b1 : 0; }

        // коэффициент приведения арматуры к фибробетону Пособие к СП 52-102-2004 п.п.2.33
        public double alfa(double _Es) => _Es / Efb;
        

        //характеристика сжатой зоны сталефибробетона, принимаемая для
        // сталефибробетона из тяжелого бетона классов до В60 включительно равной 0,8
        public static double omega = 0.8;

        // Диаграмма состояния
        public double Eps_StateDiagram(double _eps)
        {
            double sigma_fbt = 0;

            // Относительные деформации
            double e_fbt0 = Rfbt / Efb;
            double e_fbt = e_fbt0;
            double e_fbt1 = e_fbt0 + 0.0001;

            double e_fbt2 = 0.004;

            double e_fbt3 = 0.02 - 0.0125 * (Rfbt3 / Rfbt2 - 0.5);

            if (e_fbt1 < e_fbt && e_fbt <= e_fbt2)
            {
                if (Rfbt != 0)
                    sigma_fbt = Rfbt * (1 - (1 - Rfbt2 / Rfbt) * (e_fbt - e_fbt1) / (e_fbt2 - e_fbt1));
            }
            else if (e_fbt1 < e_fbt && e_fbt <= e_fbt3)
            {
                if (Rfbt2 != 0)
                    sigma_fbt = Rfbt * (1 - (1 - Rfbt3 / Rfbt2) * (e_fbt - e_fbt2) / (e_fbt3 - e_fbt2));
            }
            
            return sigma_fbt;
        }

        // Диаграмма деформирования  
        public double Eps_StD(double _e)
        {
            double sgm = 0;

            if (0 <= _e && _e < e_b1_red)
            {
                sgm = Eb_red * _e;
            }
            else if (e_b1_red <= _e && _e < e_b2)
            {
                sgm = R_fb;
            }

            return sgm;
        }

        public BSMatFiber() 
        {
        }

        public BSMatFiber(double _Eb)
        {
            Efb = _Eb;
        }
    }

}

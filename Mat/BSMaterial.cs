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

    

}

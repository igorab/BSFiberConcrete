using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace BSFiberConcrete
{
    public interface IMaterial
    {
        string Name { get; }        
    }

    /// <summary>
    ///  Свойства обычного бетона
    /// </summary>
    public class BSMatConcrete : IMaterial
    {
        public string Name => "Бетон";
        public string BT { get; set; }

        [DisplayName("Сопротивление на сжатие")]
        public double Rb { get; set; }

        [DisplayName("Модуль сжатия бетона")]
        public double Eb { get; set; }

        private double e_b0;
        private double e_b1;
        private double e_b2;

        // Диаграмма состояния
        public  double StateDiagram(double e_b)
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

        public BSMatConcrete()
        {
            BT = "B30";
        }
    }

    /// <summary>
    ///  Материал стержня арматуры
    /// </summary>
    public class BSMatRod : IMaterial
    {
        public string Name => "Сталь";

        // Класс
        public string RCls { get; set; }

        /// <summary>
        /// модуль упругости, МПа
        /// </summary>
        public double Es { get; set; }

        // Расчетное сопротивление
        public double Rs { get; set; }

        /// <summary>
        /// коэффициент упругости
        /// </summary>
        public double Nju_s { get; set; }

        public double Eps_s_ult { get; set; } 

        public BSMatRod()
        {

        }
        
        public BSMatRod(double _Es)
        {
            Es = _Es;
        }

    }

    /// <summary>
    /// Свойства фибробетона
    /// </summary>
    public class BSMatFiber : IMaterial
    {
        public string Name => "Фибробетон";

        public double Eb { get => Efb; }

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

        [DisplayName("Сопротивление на сжатие")]
        public double Rfbn { get; set; }

        [DisplayName("Сопротивление на растяжение")]
        public double Rfbt { get; set; }

        // Rfbt2 Rfbt3 - остаточные сопротивления сталефибробетона растяжению (Таблица 2 СП 360)

        [DisplayName("Сопротивление на растяжение")]
        public double Rfbt2 { get; set; }

        [DisplayName("Нормативное остаточное сопротивление осевому растяжению")]
        public double Rfbt3 { get; set; }

        // Расчетное сопротивление
        public double Rb { get => Rfbn; }

        // Диаграмма состояния
        public double StateDiagram()
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
                sigma_fbt = Rfbt * (1 - (1 - Rfbt2 / Rfbt) * (e_fbt - e_fbt1) / (e_fbt2 - e_fbt1));
            }
            else if (e_fbt1 < e_fbt && e_fbt <= e_fbt3)
            {
                sigma_fbt = Rfbt * (1 - (1 - Rfbt3 / Rfbt2) * (e_fbt - e_fbt2) / (e_fbt3 - e_fbt2));
            }

            return sigma_fbt;
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

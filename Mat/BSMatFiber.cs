using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFiberConcrete
{
    /// <summary>
    /// Свойства фибробетона
    /// Расчет по нелинейной деформационной модели
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

        /// <summary>
        ///  предельное значение относительной деформации фибробетона при сжатии
        /// </summary>
        public double Eps_fb_ult { get; set; }

        /// <summary>
        ///  предельное значение относительной деформации фибробетона при растяжении
        /// </summary>
        public double Eps_fbt_ult { get; set; }

        private double Yft, Yb, Yb1, Yb2, Yb3, Yb5;

        public double Eps_fb0 { get; set; }
        public double Eps_fb2 { get; set; }
        public double Eps_fbt2 { get; set; }
        public double Eps_fbt3 { get; set; }


        public BSMatFiber(decimal _Yft, decimal _Yb, decimal _Yb1, decimal _Yb2, decimal _Yb3, decimal _Yb5)
        {
            Yft = (double) _Yft; 
            Yb = (double) _Yb; 
            Yb1 = (double) _Yb1; 
            Yb2 = (double) _Yb2; 
            Yb3 = (double) _Yb3; 
            Yb5 = (double) _Yb5;
        }

        // Класс бетона
        public string BTCls { get; set; }

        [DisplayName("Числовая характеристика класса фибробетона по прочности на осевое сжатие")]
        public double B { get; set; }

        [DisplayName("Нормативное сопротивление сталефибробетона осевому сжатию Rfbn")]
        public double Rfbn { get; set; }

        [DisplayName("Расчетное сопротивление сталефибробетона осевому сжатию Rfbn")]
        public double Rfb { get { return R_fb_calc(); } set { Rfb = value; } }

        [DisplayName("Нормативное сопротивление сталефибробетона осевому растяжению Rfbt")]
        public double Rfbtn { get; set; }

        [DisplayName("Расчетное сопротивление сталефибробетона осевому растяжению Rfbt")]
        public double Rfbt { get { return R_fbt_calc();  } set { Rfbt = value; } }

        [DisplayName("Остаточное нормативное сопротивление на растяжение Rfbt2,n")]
        public double Rfbt2n { get; set; }

        [DisplayName("Остаточное расчетное сопротивление на растяжение Rfbt2")]
        public double Rfbt2 { get { return R_fbt2_calc(); } set { Rfbt2 = value; } }

        [DisplayName("Остаточное нормативное сопротивление осевому растяжению Rfbt3,n")]
        public double Rfbt3n { get; set; }

        [DisplayName("Остаточное расчетное сопротивление осевому растяжению Rfbt3")]
        public double Rfbt3 { get { return R_fbt3_calc(); } set { Rfbt3 = value; } }

        public double R_fb { get => Rfbn; }
        public double e_b1_red { get; set; }
        public double e_b1 { get; set; }

        // Расчетные значения сопротивления на сжатиие по B30 СП63
        public double R_fb_calc() => (Yb != 0) ? Rfbn / Yb * Yb1 * Yb2 * Yb3 * Yb5 : 0;

        //Расчетное остаточное сопротивление осевому растяжению R_fbt
        public double R_fbt_calc() => (Yft != 0) ? Rfbtn / Yft * Yb1 * Yb5 : 0;

        //Расчетное остаточное сопротивление осевому растяжению R_fbt2
        public double R_fbt2_calc() => (Yft != 0) ? Rfbt2n / Yft * Yb1 * Yb5 : 0;

        //Расчетное остаточное сопротивление осевому растяжению R_fbt3
        public double R_fbt3_calc() => (Yft != 0) ? Rfbt3n / Yft * Yb1 * Yb5 : 0;

        // относительные деформации сжатого сталефибробетона при напряжениях R/b,
        // принимаемые по указаниям СП 63.13330 как для обычного бетона
        public double e_b2 { get; set; }

        public double Eb_red { get => (e_b1 != 0) ? R_fb / e_b1 : 0; }

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

            double e_fbt3 = (Rfbt2 != 0) ? 0.02 - 0.0125 * (Rfbt3 / Rfbt2 - 0.5) : 0;

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
            else if (_e < 0) // по теории такого быть не должно
            {
                sgm = Eb_red * _e;
            }
            else if (_e >= e_b2) // и такого быть не должно
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

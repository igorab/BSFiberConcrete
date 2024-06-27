using System.Diagnostics;

namespace BSFiberConcrete
{
    /// <summary>
    ///  Материал стержня арматуры
    /// </summary>
    public class BSMatRod : IMaterial, INonlinear
    {
        public string Name => "Сталь";
        public double E_young => Es;

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


        /// <summary>
        /// Диграмма состояния трехлинейная
        /// </summary>
        /// <param name="e_s">отн деформация </param>
        /// <param name="_res"></param>
        /// <returns>Напряжение</returns>
        public double Eps_StateDiagram3L(double e_s, out int _res)
        {
            _res = 0;
            double sigma_s = 0;

            double sigma_s1 = 0.6 * Rs;            
            double e_s1 = sigma_s1 / Es;
            e_s2 = 1;

            if (0 <= e_s && e_s <= e_s1)
            {
                sigma_s = Rs * e_s;
            }
            else if (e_s1 <= e_s && e_s <= e_s2)
            {
                sigma_s = ((1 - sigma_s1 / Rs) * (e_s - e_s1) / (e_s0 - e_s1) + sigma_s1 / Rs) * Rs;
            }
            else if (e_s0 <= e_s && e_s <= e_s2)
            {
                sigma_s = Rs;
            }
            else if (e_s > e_s2)
            {
                Debug.Assert(true, "Превышена деформация арматуры");
                sigma_s = 0;
            }

            return sigma_s;
        }

        //
        /// <summary>
        /// Диаграмма состояния двухлинейная 
        /// на сжатие и растяжение
        /// </summary>
        /// <param name="_e"></param>
        /// <returns></returns>        
        public double Eps_StDiagram2L(double _e, out int _res)
        {
            double sgm = 0;
            _res = 0;

            if (0 < _e && _e < e_s0)
            {
                sgm = Es * _e;
            }
            else if (e_s0 <= _e && _e <= e_s2)
            {
                sgm = Rs;
            }
            else if (_e > e_s2) //теоретически это разрыв
            {
                Debug.Assert(true, "Превышен предел прочности (временное сопротивление) ");
                _res = -1;
                sgm = 0;//  Rs;
            }

            return sgm;
        }
    }
}

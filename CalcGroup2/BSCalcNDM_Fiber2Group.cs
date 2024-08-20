using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFiberConcrete.CalcGroup2
{
    /// <summary>
    /// Расчет по 2 группе предельных состояний
    /// </summary>
    public partial class BSCalcNDM
    {
        /// <summary>
        /// Диаграмма деформирования арматуры (двухлинейная) 
        /// </summary>
        /// <param name="_e">деформация</param>
        /// <returns>Напряжение</returns>
        private double Diagr_S(double _e)
        {
            double s = 0;
           
            if (_e > est2 || _e < -esc2)
                s = 0;
            else if (est0 <= _e && _e <= est2)
                s = Rst;
            else if (-esc2 <= _e && _e <= -esc0)
                s = -Rsc;
            else if (0 <= _e && _e <= est0)
                s = Math.Min(_e * Es0, Rst);
            else if (-esc0 <= _e && _e <= 0)
                s = Math.Max(_e * Es0, -Rsc);

            return s;
        }

        /// <summary>
        /// Диаграмма деформирования обычного бетона (трехлинейная) 
        /// </summary>
        /// <param name="_e">деформация</param>
        /// <returns>напряжение</returns>
        private double Diagr_Beton(double _e)
        {
            double s = 0;
            double sc1 = 0.6 * Rbc;
            double st1 = 0.6 * Rfbt;
            double ebc1 = sc1 / Eb0;
            double ebt1 = st1 / Eb0;
            
            if ((_e > efbt2) || (_e < -ebc2))
                s = 0;
            else if (-ebc2 <= _e && _e <= -ebc0)
                s = -Rbc;
            else if (efbt0 <= _e && _e <= efbt2)
                s = Rfbt;
            else if (-ebc0 <= _e && _e <= -ebc1)
                s = -Rbc * ((1 - sc1 / Rbc) * (Math.Abs(_e) - ebc1) / (ebc0 - ebc1) + sc1 / Rbc);
            else if (ebt1 <= _e && _e <= efbt0)
                s = Rfbt * ((1 - st1 / Rfbt) * (Math.Abs(_e) - ebt1) / (efbt0 - ebt1) + st1 / Rfbt);
            else if (-ebc1 <= _e && _e <= ebt1)
                s = _e * Eb0;

            return s;
        }

        /// <summary>
        /// Диаграмма деформирования фибробетона (трехлинейная) 
        /// СП360 5.2.9
        /// </summary>
        /// <param name="_e">деформация</param>
        /// <returns>напряжение</returns>
        private double Diagr_FB(double _e)
        {
            double s = 0;

            double sc1 = 0.6 * Rbc;            
            double ebc1 = sc1 / Eb0;

            efbt0 = Rfbt / Eb0;
            efbt1 = efbt0 + 0.0001;
            //TODO refactoring
            efbt2 = 0.004;
            efbt3 = 0.02 - 0.0125 * (Rfbt3 / Rfbt2 - 0.5);

            // сжатие: ПО СП 63 6.1.20 (как для обычного бетона)           
            if (_e < -ebc2)
                s = 0;
            else if (-ebc2 <= _e && _e <= -ebc0)
                s = -Rbc;
            else if (-ebc0 <= _e && _e <= -ebc1)
                s = -Rbc * ((1 - sc1 / Rbc) * (Math.Abs(_e) - ebc1) / (ebc0 - ebc1) + sc1 / Rbc);
            else if (-ebc1 <= _e && _e < 0)
                s = _e * Eb0;
            // растяжение
            else if (0 <= _e && _e <= efbt0)
            {
                s = _e * Eb0;
            }
            else if (efbt0 < _e && _e <= efbt1)
            {
                s = Rfbt;
            }
            else if (efbt1 < _e && _e <= efbt2)
            {
                s = Rfbt * (1 - (1 - Rfbt2 / Rfbt) * (_e - efbt1) / (efbt2 - efbt1));
            }
            else if (efbt2 < _e && _e <= efbt3)
            {
                s = Rfbt2 * (1 - (1- Rfbt3 / Rfbt2) * (_e - efbt2) / (efbt3 - efbt2));
            }
            else if (_e > efbt3)
            {
                s = 0;
            }

            return s;
        }

        // секущий модуль
        private double E_sec(double _s, double _e, double _E0)
        {
            if (_e == 0)
                return _E0;
            else
                return _s / _e;
        }

    }
}

using System;
using System.Collections.Generic;
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
        /// Диаграмма деформирования бетона (трехлинейная) 
        /// </summary>
        /// <param name="_e">деформация</param>
        /// <returns>напряжение</returns>
        private double Diagr_B(double _e)
        {
            double s = 0;
            double sc1 = 0.6 * Rbc;
            double st1 = 0.6 * Rbt;
            double ebc1 = sc1 / Eb0;
            double ebt1 = st1 / Eb0;

            if ((_e > ebt2) || (_e < -ebc2))
                s = 0;
            else if (-ebc2 <= _e && _e <= -ebc0)
                s = -Rbc;
            else if (ebt0 <= _e && _e <= ebt2)
                s = Rbt;
            else if (-ebc0 <= _e && _e <= -ebc1)
                s = -Rbc * ((1 - sc1 / Rbc) * (Math.Abs(_e) - ebc1) / (ebc0 - ebc1) + sc1 / Rbc);
            else if (ebt1 <= _e && _e <= ebt0)
                s = Rbt * ((1 - st1 / Rbt) * (Math.Abs(_e) - ebt1) / (ebt0 - ebt1) + st1 / Rbt);
            else if (-ebc1 <= _e && _e <= ebt1)
                s = _e * Eb0;

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

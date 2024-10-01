using ScottPlot.Hatches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFiberConcrete
{
    /// <summary>
    /// Расчеты на действие поперечной силы
    /// </summary>
    public class BSFiberCalc_QxQy : BSFiberCalc_MNQ
    {
        /// <summary>
        /// 6.1.27 Расчет изгибаемых элементов по бетонной полосе между наклонными сечениями
        /// </summary>
        /// <returns>Расчет выполнен успешно</returns>
        public bool StripBetweenInclinedSections(double _b, double _h)
        {
            bool ok = false;
            // поперечная сила в рассматриваемом нормальном сечении элемента
            double Q = Math.Sqrt(Qx*Qx + Qy*Qy);

            double _q = 0.3 * Rfb * _b * _h;

            if (Q <= _q) 
            { 
                ok = true;
            }

            return ok;
        }


        /// <summary>
        /// Расчет элементов по наклонным сечениям на действие поперечных сил
        /// </summary>
        /// <returns></returns>
        public bool InclinedSectionsQ()
        {
            // П 6.1.28 поперечная сила в наклонном сечении с длиной проекции С на продольную ось
            // элемента, определяемая от всех внешних сил, расположенных по одну сторону от рассматриваемого наклонного сечения;
            double _Q = 0;
            double Qfb = 0, Qsw = 0;

            return _Q <= Qfb + Qsw;

        }



    }
}

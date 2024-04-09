using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics;
using MathNet.Numerics.Statistics;

namespace BSFiberConcrete.BSRFib
{
    public class BSRFibLabTensileStats
    {
        List<double> Fel { get; set; }
        List<double> F05 { get; set; }
        List<double> F25 { get; set; }


        // Значения прочности с учетом неупругих свойтв стале фибро бетона ф.(Б.1) Н/мм2
        List<double> RF05 { get; set;}

        // Значения прочности с учетом неупругих свойтв стале фибро бетона ф.(Б.2) H/мм2
        List<double> RF25 { get; set; }


        public void Calculate()
        {
            // средние значения остаточной прочности сталефибробетона на растяжение
            var rF05m = Statistics.Mean(RF05);

            // средние значения остаточной прочности сталефибробетона на растяжение
            var rF25m = Statistics.Mean(RF25);

            // Cреднеквадратичные отклонения
            var S_F05m = Statistics.StandardDeviation(RF05);

            var S_F25m = Statistics.StandardDeviation(RF25); ;
            

            // Коэффициенты вариации
            double nu_F05m = S_F05m / rF05m;

            double nu_F25m = S_F25m / rF25m;

            // Нормативные значения остаточной прочности сталефибробетона на растяжение

            double R_fbt2n = rF05m * (1 - 1.64 * nu_F05m);

            double R_fbt3n = rF25m * (1 - 1.64 * nu_F25m);


        }

    }
}

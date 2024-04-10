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
        BSRFibLabTensile tensile;

        public List<FibLab> DsFibLab { get; set; }

        public double Rfbt2n { get; set; }
        public  double Rfbt3n { get; set; }
        public double RFel  { get; set; }


        // Значения прочности с учетом неупругих свойтв стале фибро бетона ф.(Б.1) Н/мм2
        private List<double> RF05s;

        // Значения прочности с учетом неупругих свойтв стале фибро бетона ф.(Б.2) H/мм2
        private List<double> RF25s;

        private List<double> RFels;


        public BSRFibLabTensileStats()
        {
            tensile = new BSRFibLabTensile() { L = 50, b = 15 } ;

            RF05s = new List<double>();
            // Значения прочности с учетом неупругих свойтв стале фибро бетона ф.(Б.2) H/мм2
            RF25s = new List<double>();
            RFels = new List<double>();

        }


        public void Calculate()
        {
            foreach (FibLab lab in DsFibLab)
            {
                RF05s.Add(tensile.R_F05(lab.F05));
                RF25s.Add(tensile.R_F25(lab.F25));
                RFels.Add(tensile.R_Fel(lab.Fel));
            }


            // средние значения остаточной прочности сталефибробетона на растяжение
            var rF05m = Statistics.Mean(RF05s);

            // средние значения остаточной прочности сталефибробетона на растяжение
            var rF25m = Statistics.Mean(RF25s);

            var rFelm = Statistics.Mean(RFels);

            // Cреднеквадратичные отклонения
            var S_F05m = Statistics.StandardDeviation(RF05s);

            var S_F25m = Statistics.StandardDeviation(RF25s);

            var S_Fel = Statistics.StandardDeviation(RFels);

            // Коэффициенты вариации
            double nu_F05m = S_F05m / rF05m;

            double nu_F25m = S_F25m / rF25m;

            double nu_Fel = S_Fel / rFelm;

            // Нормативные значения остаточной прочности сталефибробетона на растяжение

            Rfbt2n = rF05m * (1 - 1.64 * nu_F05m);

            Rfbt3n = rF25m * (1 - 1.64 * nu_F25m);

            RFel = rFelm * (1 - 1.64 * nu_Fel);

        }

    }
}

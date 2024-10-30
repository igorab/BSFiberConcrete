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
        public double RFbtn  { get; set; }


                private List<double> RF05s;

                private List<double> RF25s;

        private List<double> RFels;


        public BSRFibLabTensileStats()
        {
            tensile = new BSRFibLabTensile() ;

            RF05s = new List<double>();
                        RF25s = new List<double>();
            RFels = new List<double>();

        }

                                public void Calculate()
        {
            Rfbt2n = 0;
            Rfbt3n = 0;
            RFbtn = 0;

            foreach (FibLab lab in DsFibLab)
            {
                tensile.L = lab.L;
                tensile.b = lab.B;

                RF05s.Add(tensile.R_F05(lab.F05));
                RF25s.Add(tensile.R_F25(lab.F25));
                RFels.Add(tensile.R_Fel(lab.Fel));
            }

                        var rF05m = Statistics.Mean(RF05s);                        
                        var S_F05m = Statistics.StandardDeviation(RF05s);                        

            if (!double.IsNaN(S_F05m) && rF05m != 0)
            {
                                double nu_F05m = Math.Sqrt(S_F05m) / rF05m;
                Rfbt2n = rF05m * (1 - 1.64 * nu_F05m);
            }

                        var rF25m = Statistics.Mean(RF25s);
            var S_F25m = Statistics.StandardDeviation(RF25s);

            if (!double.IsNaN(S_F25m) && rF25m != 0)
            {
                double nu_F25m = Math.Sqrt(S_F25m) / rF25m;
                Rfbt3n = rF25m * (1 - 1.64 * nu_F25m);
            }

            var rFelm = Statistics.Mean(RFels);
            var S_Fel = Statistics.StandardDeviation(RFels);

            if (!double.IsNaN(S_Fel) && rFelm != 0)
            {
                double nu_Fel = Math.Sqrt(S_Fel) / rFelm;
                RFbtn = rFelm * (1 - 1.64 * nu_Fel);
            }
        }

    }
}

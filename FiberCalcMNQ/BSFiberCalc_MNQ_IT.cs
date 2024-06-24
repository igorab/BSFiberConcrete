using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFiberConcrete
{
    // Расчет балки двутаврового сечения на внецентренное сжатие
    public class BSFiberCalc_MNQ_IT : BSFiberCalc_MNQ
    {
        public BSBeam_IT beam { get; set; }

        public BSFiberCalc_MNQ_IT()
        {
            this.beam = new BSBeam_IT();

            base.m_Beam = this.beam;
        }

        public override bool Calculate()
        {
            if (N_Out)
            {
                Calculate_N_Out();                
            }
            else if (Shear)
            {   // Расчет на действие поперечной силы
                CalculateQ();
                // Расчет на действие моментов
                CalculateM();
            }
            else if (UseRebar)
            {
                Calculate_N_Rods();
            }
            else
            {
                Calculate_N();
            }

            //throw new Exception("Расчет не выполнен (нет в СП)");
            return true;
        }

        public override void GetSize(double[] _t)
        {
            beam.GetSizes(_t);
            base.m_Beam = this.beam;
            l0 = beam.Length;
            b = beam.b;
            h = beam.h;
            I = beam.Jx();
            A = beam.Area();
            y_t = beam.y_t;
        }
    }
}

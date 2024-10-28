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
            {
                m_ImgCalc = "Incline_Q.PNG";

                // Расчет на действие поперечной силы вдоль оси X
                Calculate_Qx(b, h);

                // Расчет на действие поперечной силы вдоль оси Y
                Calculate_Qy(h, b);

                // Расчет на действие моментов относительно оси Y
                Calculate_My(b, h);

                // Расчет на действие моментов относительно оси X
                Calculate_Mx();

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
       
        public override void SetSize(double[] _t)
        {
            beam.SetSizes(_t);
            base.m_Beam = this.beam;
            LngthCalc0 = beam.Length;
            b = beam.b;
            h = beam.h;
            I = beam.Jx();
            A = beam.Area();
            y_t = beam.y_t;
        }
    }
}

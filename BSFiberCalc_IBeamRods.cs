using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFiberConcrete
{
    /// <summary>
    /// Двутавровое сечение с арматурой
    /// </summary>
    public class BSFiberCalc_IBeamRods : BSFibCalc_IBeam
    {
        // продольная арматура
        private double[] m_LRebar;
        // поперечная арматура
        private double[] m_TRebar;

        // Материал стержня
        private BSMatRod MatRod;
        // Стержни (итого)
        private BSRod Rod;

        public double[] LRebar { get => m_LRebar; set => m_LRebar = value; }
        public double[] TRebar { get => m_TRebar; set => m_TRebar = value; }

        public double Dzeta(double _x, double _h0) => (_h0 != 0) ? _x / _h0 : 0;

        public double Dzeta_R() => omega / (1 + MatRod.epsilon_s() / MatFiber.e_b2);

        private double Rfbr3 = 0;

        public void GetLTRebar(double[] _LRebar, double[] _TRebar)
        {
            LRebar = _LRebar;
            TRebar = _TRebar;

            MatRod = new BSMatRod();
            MatRod.Rs = 3567; // кг/см2
            MatRod.Rsc = 3567; // кг/см2
            MatRod.As = 4.52; // см2
            MatRod.As1 = 4.52; // см2
            MatRod.Es = 2038735;

            Rod = new BSRod();
            Rod.a = 4;
            Rod.a1 = 4;
        }


        public override void Calculate()
        {
            //общая высота
            double h = hf + hw + h1f;

            Calc_Pre();


            Mult_WithArm(_h0 : h- Rod.a, _h:h, _a: Rod.a, _a1: Rod.a1);
            
            Calc_x();
            

            //граничная относительная высота сжатой зоны
            double dzeta_R = Dzeta_R();

        }

        /// <summary>
        /// высота сжатой зоны 
        /// </summary>        
        protected double Calc_x()
        {
            double res_x = (MatRod.Rs * MatRod.As + Rfbt3 * (b1f * h1f + bw * hw + bf * hf) - MatRod.Rsc * MatRod.As1) / (b1f * (Rfbt3 + Rfb));

            return res_x;
        }


        // для изгибаемых сталефибробетонных элементов таврового и двутаврового сечений с
        // полкой в сжатой зоне определяют
        public (double, double) Mult_WithArm( double _h0, double _h, double _a, double _a1)
        {
            double res_Mult;
            double condition = -1;
            double x; // высота сжатой зоны
           

            // расчет по случаю А
            if (condition < 0)
            {
                x = Rfbr3 * (b1f * h1f + bw * hw + bf * hf);

                res_Mult = Rfb * b1f * x * (_h0 - 0.5 * x) - 
                           Rfbt3 * (bf * hf * (0.5 * hf - _a)  + bw * hw * (0.5 * hw + hf -_a) +  b1f * (h1f - x) * (_h0 - 0.5 * (h1f + x))) +
                           MatRod.Rsc * MatRod.As1 * (_h0 - _a1);

                
            }
            else // Расчет по случаю Б
            {

                x = (MatRod.Rs * MatRod.As + Rfbt3 * (b1f * h1f + bw * hw + bf * hf) - Rfb * h1f * (b1f - bw) - MatRod.Rsc * MatRod.As1) / (bw * (Rfbt3 + Rfb)) ;

                
                res_Mult = Rfb * (b1f * h1f * (_h0 - 0.5 * h1f) + bw * (x-h1f) * (_h0 - 0.5*x - 0.5 * h1f) )
                          - Rfbt3 * (bf * hf * (0.5 * hf -_a) + bw * (_h - hf - x) * (_h0 - 0.5 * (_h + x - hf))) 
                          + MatRod.Rsc * MatRod.As1 * (_h0 - _a1);

                
            }

            return (res_Mult, x);
        }
        
    }
}

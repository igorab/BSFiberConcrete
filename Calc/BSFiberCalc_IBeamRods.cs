using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
namespace BSFiberConcrete
{
                public class BSFiberCalc_IBeamRods : BSFibCalc_IBeam
    {
                private double[] m_LRebar;
                private double[] m_TRebar;
                private BSMatRod MatRod;
                private BSRod Rod;
        public double[] LRebar { get => m_LRebar; set => m_LRebar = value; }
        public double[] TRebar { get => m_TRebar; set => m_TRebar = value; }
        public double Dzeta(double _x, double _h0) => (_h0 != 0) ? _x / _h0 : 0;
        public double Dzeta_R() => Omega / (1 + MatRod.epsilon_s() / MatFiber.e_b2);
        public double h { get => hf + hw + h1f; }
        public void GetLTRebar(double[] _MatRod)
        {            
            int idx = -1;
            MatRod = new BSMatRod();
            MatRod.Rs = _MatRod[++idx];             MatRod.Rsc = _MatRod[++idx];             MatRod.As = _MatRod[++idx];             MatRod.As1 = _MatRod[++idx];             MatRod.Es = _MatRod[++idx];
            Rod = new BSRod();
            Rod.a = _MatRod[++idx];
            Rod.a1 = _MatRod[++idx];
        }
        
                                protected double Calc_x()
        {
            double res_x = (MatRod.Rs * MatRod.As + Rfbt3 * (b1f * h1f + bw * hw + bf * hf) - MatRod.Rsc * MatRod.As1) / (b1f * (Rfbt3 + Rfb));
            return res_x;
        }
                        public (double, double) Calc_Mult( double _h0, double _h, double _a, double _a1)
        {
            double res_Mult;
            double condition = -1;
            double _x; 
            _x = Calc_x();
            bool checkOK;
            string info;
            double dzeta = Dzeta(_x, _h0);
                        double dzeta_R = Dzeta_R();
            checkOK = dzeta <= dzeta_R;
            if (checkOK)
            {                
                info =  string.Format( "Условие ξ ({0}) <= ξR ({1}) выполнено ", Math.Round(dzeta,2), Math.Round(dzeta_R,2));
                Msg.Add(info);
            }
            else
            {                
                info = string.Format("Условие ξ ({0}) <= ξR ({1}) не выполнено ", Math.Round(dzeta,2), Math.Round(dzeta_R,2)); ;
                info += "Требуется увеличить высоту элемента.";
                Msg.Add(info);
            }
            
            condition = (MatRod.Rs * MatRod.As + Rfbt3 * (bf * hf + bw * hw)) - (MatRod.Rsc * MatRod.As1 + Rfb * b1f * h1f);
                        if (condition <= 0)
            {                
                res_Mult = Rfb * b1f * _x * (_h0 - 0.5 * _x) - 
                           Rfbt3 * (bf * hf * (0.5 * hf - _a)  + bw * hw * (0.5 * hw + hf -_a) +  b1f * (h1f - _x) * (_h0 - 0.5 * (h1f + _x))) +
                           MatRod.Rsc * MatRod.As1 * (_h0 - _a1);
                
            }
            else             {
                _x = (MatRod.Rs * MatRod.As + Rfbt3 * (b1f * h1f + bw * hw + bf * hf) - Rfb * h1f * (b1f - bw) - MatRod.Rsc * MatRod.As1) / (bw * (Rfbt3 + Rfb)) ;
                
                res_Mult = Rfb * (b1f * h1f * (_h0 - 0.5 * h1f) + bw * (_x-h1f) * (_h0 - 0.5*_x - 0.5 * h1f) )
                          - Rfbt3 * (bf * hf * (0.5 * hf -_a) + bw * (_h - hf - _x) * (_h0 - 0.5 * (_h + _x - hf))) 
                          + MatRod.Rsc * MatRod.As1 * (_h0 - _a1);                
            }
            
            return (res_Mult, _x);
        }
                                public override bool Calculate()
        {
                        
            Calc_Pre();
            (Mult, x) = Calc_Mult(_h0: h - Rod.a, _h: h, _a: Rod.a, _a1: Rod.a1);
            InfoCheckM(Mult);
            Mult = BSHelper.Kgsm2Tm(Mult);
            return true;
        }
    }
}

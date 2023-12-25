using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFiberConcrete
{
    internal class BSFiberCalculation
    {
        //6 Сталефибробетонные конструкции без предварительного напряжения арматуры 

        // растяжение
        double Rfbr3;
        // сжатие
        double Rfb;
        double Rfbt;
        double Rfbt3;
        // растяжение в арматуре
        double Rs;
        // сжатие в арматуре
        double Rsc;
        // Относительная деформация
        double Efbt;

        double As1;

        private const double B = 1.0;
        private double m_Wpl;
        private double gamma = 1.73 - 0.005 * (B - 15);

        public BSFiberCalculation()
        {
            Rfbr3 = 0;
            Rfb = 0;
            Rs = 0;            
            Rsc = 0;
            Efbt = 0;
        }

        public double Dzeta(double _x, double _h0) =>  _x / _h0;


        public double Wpl(double _b, double _h) => _b * Math.Pow(_h, 2) * gamma / 6;  

        public double Mult() => Rfbt * m_Wpl;

        //6.5
        public double Mult_arm(double _b, double _h0,  double _x, double _h, double _a, double _a1)
        {
            double res = Rfb * _b * _x * (_h0 - 0.5 * _x) - Rfbt3 * _b * (_h - _x) * ((_h - _x)/2 - _a) + Rsc * As1* (_h0 - _a1) ;

            return res;
        }

    }
}

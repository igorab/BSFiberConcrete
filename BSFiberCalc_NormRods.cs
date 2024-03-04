using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BSFiberConcrete
{   
    /// <summary>
    ///  Расчет изгибаемого прямоугольного элемента с рабочей арматурой
    /// </summary>
    public class BSFiberCalc_RectRods : BSFibCalc_Rect
    {
        // продольная арматура
        private double[] m_LRebar;
        // поперечная арматура
        private double[] m_TRebar;

        // Материал стержня
        private BSMatRod MatRod;
        // Стержни (итого)
        private BSRod  Rod;

        public double[] LRebar { get => m_LRebar; set => m_LRebar = value; }
        public double[] TRebar { get => m_TRebar; set => m_TRebar = value; }

        public double Dzeta(double _x, double _h0) => (_h0 != 0) ? _x / _h0 : 0;

        public void GetLTRebar(double[]  _LRebar, double[] _TRebar)
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

        //6.5
        // предельный изгибающий момент, который может быть воспринят сечением элемента
        protected double Mult_arm(double _b, double _h0, double _x, double _h, double _a, double _a1)
        {
            double res = Rfbn * _b * _x * (_h0 - 0.5 * _x) - Rfbt3 * _b * (_h - _x) * ((_h - _x) / 2 - _a) + MatRod.Rsc * MatRod.As1 * (_h0 - _a1);
            return res;
        }

        public double Dzeta_R() => omega / (1 + MatRod.epsilon_s() / MatFiber.e_b2);

        public override void Calculate()
        {
            //Расчетное остаточное остаточного сопротивления осевому растяжению
            Rfbt3 = Rfbt_3();

            // Расчетная высота сечения
            double h0 = h - Rod.a;

            double _x = (MatRod.Rs * MatRod.As - MatRod.Rsc * MatRod.As1 + Rfbt3 * b * h ) / ((Rfbn + Rfbt3) * b);

            double dzeta = Dzeta(_x, h0);
                       
            //граничная относительная высота сжатой зоны
            double dzeta_R = Dzeta_R();

            bool checkOK;
            string info;

            if (dzeta <= dzeta_R)
            {
                checkOK = true;
                info = "Условие ξ <= ξR выполнено ";
                Msg.Add(info);
            }
            else
            {
                checkOK = false;
                info = "Условие ξ <= ξR не выполнено! ";
                info += "Требуется увеличить высоту элемента.";
                Msg.Add(info);
            }

            Mult = 10E-5d * Mult_arm(b, h0, _x, h, Rod.a, Rod.a1);
            
            if (!checkOK)
                throw new Exception(info);

            info = "Расчет успешно выполнен!";
            Msg.Add(info);
        }        
    }
    
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BSFiberConcrete
{
    /// <summary>
    ///  Расчет исгибаемых элементов с рабочей арматурой
    /// </summary>
    internal class BSFiberCalc_NormRods
    {
    }

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

        public double Dzeta(double _x, double _h0) => (_h0 != 0) ? _x / _h0 : 0;

        public void GetLTRebar(double[]  _LRebar, double[] _TRebar)
        {
            m_LRebar = _LRebar;
            m_TRebar = _TRebar;

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

        public override void Calculate()
        {
            //Расчетное остаточное остаточного сопротивления осевому растяжению
            Rfbt3 = Rfbt_3();

            // Расчетная высота сечения
            double h0 = h - Rod.a;

            double _x = (MatRod.Rs * MatRod.As - MatRod.Rsc * MatRod.As1 + Rfbt3 * b * h ) / ((Rfbn + Rfbt3) * b);

            double dzeta = Dzeta(_x, h0);

            //относительные деформации сжатого сталефибробетона при напряжениях R/b,
            // принимаемые по указаниям СП 63.13330 как для обычного бетона
            double ε_fb2 = 0.0035;

            //граничная относительная высота сжатой зоны
            double dzeta_R = omega / (1 + MatRod.epsilon_s() / ε_fb2);

            bool checkOK;
            string info;

            if (dzeta <= dzeta_R)
            {
                checkOK = true;
                info = "Условие ξ <= ξR выполнено ";
            }
            else
            {
                checkOK = false;
                info = "Условие ξ <= ξR не выполнено! ";
                info += "Требуется увеличить высоту элемента.";
            }
            
            if (!checkOK)
                throw new Exception(info);
        }        
    }


    public class  BSFiberCalc_IBeamRods: BSFibCalc_IBeam
    {

        BSMatRod MatRod = new BSMatRod();
        BSRod Rod = new BSRod();


        double Rfbr3 = 0; // ???

        // высота сжатой зоны
        public double calc_x(double _bf1, double _hf1, double _bw, double _hw, double _bf, double _hf)
        {
            double res_x = Rfbt3 * (_bf1 * _hf1 + _bw * _hw + _bf * _hf) / (_bf1 * (Rfbr3 + Rfbn));

            return res_x;
        }


        // для изгибаемых сталефибробетонных элементов таврового и двутаврового сечений с
        // полкой в сжатой зоне определяют
        public (double, double) Mult_withoutArm(double _b, double _h0, double _x, double _h, double _a, double _a1)
        {
            double res_Mult = 0;
            double condition = -1;
            double x = 0; // высота сжатой зоны

            double bf1 = 0;
            double hf1 = 0;
            double bw = 0;
            double hw = 0;
            double bf = 0;
            double hf = 0;


            //если граница проходит в полке
            if (condition < 0)
            {
                res_Mult = Rfbn * _b * _x * (_h0 - 0.5 * _x) - Rfbt3 * _b * (_h - _x) * ((_h - _x) / 2 - _a) + MatRod.Rsc * MatRod.As1 * (_h0 - _a1);

                x = Rfbr3 * (bf1 * hf1 + bw * hw + bf * hf);
            }
            else
            {
                // если граница проходит в ребре
                res_Mult = Rfbn * _b * _x * (_h0 - 0.5 * _x) - Rfbt3 * _b * (_h - _x) * ((_h - _x) / 2 - _a) + MatRod.Rsc * MatRod.As1 * (_h0 - _a1);

                x = Rfbr3 * (bf1 * hf1 + bw * hw + bf * hf);
            }

            return (res_Mult, x);
        }
    }
}

using CsvHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace BSFiberConcrete
{
    public interface IBSFiberCalculation
    {
        void Calculate();
        Dictionary<string, double> Results();
    }

    // Расчет Таврового сечения
    public class BSFibCalc_TBeam : BSFiberCalculation
    {
    }

    // Расчет двутаврового сечения
    public class BSFibCalc_IBeam : BSFiberCalculation
    {
        private double Rfbt3;
        private double Rfb;
        private double Mult;
        private double x;

        public override void Calculate()
        {
            double bf, hf, hw, bw, b1f, h1f;

            (bf, hf, hw, bw, b1f, h1f) = (80, 20, 20, 20, 80, 20);
            //общая высота
            double h = hf + hw + h1f;

            double Yft, Yb1, Yb2, Yb3, Yb5, Rfbt3n;

            (Yft, Yb1, Yb2, Yb3, Yb5, Rfbt3n) = (1.3, 0.9, 0.9, 1, 1, 30.58);

            Rfbt3 = Rfbt3n / Yft * Yb1 * Yb5;

            double Rfbn, Yb;
            (Rfbn, Yb) = (224, 1.3);

            Rfb = Rfbn / Yb * Yb1*Yb2*Yb3*Yb5;

            Action calc_a = delegate
            {
                x = Rfbt3* (b1f * h1f + bw*hw + bf*hf) / (b1f * (Rfbt3 + Rfb)) ;

                Mult = 0.5* Rfbt3*(b1f*(h1f - x)*(h1f+ x) + bf*hf*(hf-x+2*(hw+h1f)) + bw*hw*(hw - x * 2*h1f));
            };

            Action calc_b = delegate
            {
                x = Rfbt3* (bw * h1f + bw * hw + bw*h1f) / (bw * (Rfbt3 + Rfb));

                Mult = Rfb*bw*(x-h1f) ;
            };


            bool cond = Rfbt3 * (bf * hf + bw * hw) < Rfb * b1f * h1f;

            if (cond)
            {
                calc_a();
            }
            else
            {
                calc_b();
            }
        }
        
    }

    // Расчет кольцевого сечения
    public class BSFibCalc_Ring : BSFiberCalculation
    {
        private double Rfb;
        private double Rfbt3;
        private double Mult;

        public override Dictionary<string, double> Results()
        {
            return new Dictionary<string, double>() { { "Rfb", Rfb }, { "Rfbt3", Rfbt3 }, { "Mult", Mult } };
        }

        public override void Calculate()
        {
            double r1, r2, Rfbn, Yft, Yb, Yb1, Yb2, Yb3, Yb5;

            (r1, r2, Rfbn, Yft, Yb, Yb1, Yb2, Yb3, Yb5) = (25, 40, 224, 1.3, 1.3, 0.9, 0.9, 1, 1);

            Rfb = Rfbn/Yb * Yb1* Yb2* Yb3* Yb5;

            double Rfbt3n = 30.58;

            Rfbt3 = Rfbt3n / Yft * Yb1 * Yb5;

            double tr = r2 - r1;

            double rm = (r1 + r2) / 2;     
            
            double Ar = 2 * Math.PI * rm * tr;

            double ar = (0.73 * Rfbt3) * (Rfb + 2 * Rfbt3);

            Mult = Ar * (Rfb * Math.Sin(Math.PI * ar) / Math.PI + 0.234 * Rfbt3) * rm;

            Mult = Mult * 0.00001;
        }
    }


    // Расчет прямоугольного сечения
    public class BSFibCalc_Rect : BSFiberCalculation
    {
        //6 Сталефибробетонные конструкции без предварительного напряжения арматуры 
        //сопротивленияосевому растяжению 
        private double Rfbtn;
        //коэффициент надежности для расчета по предельным состояниям первой группы при назначении класса сталефибробетона по прочности на растяжение.
        private double Yft;
        // Коэфициенты условия работы
        private double Yb1;
        private double Yb5;
        // числовая характеристика класса фибробетона по прочности на осевое сжатие
        private double B;

        // Упругопластический момент сопротивления
        private double Wpl;
        //Значение предельного момента сечения для изгибаемых сталефибробетонных элементов прямоугольного сечения
        private  double Mult;

        public override Dictionary<string, double> Results()
        {
            return new Dictionary<string, double>() { {"Wpl", Wpl}, { "Mult", Mult} }; 
        }

        //размеры
        double b = 0;
        double h = 0;

        public override  void GetParams(double[] _t)
        {         
            (Rfbtn, Yft, Yb1, Yb5, B) = (_t[0], _t[1], _t[2], _t[3], _t[4]);             
        }

        public override void GetSize(double[] _t)
        {            
            (b, h) = (_t[0] / 10 , _t[1]/10);
        }

        public override void Calculate()
        {
            Rfbt = Rfbtn / Yft * Yb1 * Yb5;

            double Y = 1.73d - 0.005d * (B - 15);

            Wpl = b * h * h / 6 * Y;

            Mult = Rfbt * Wpl;
        }
    }


    public class BSFiberCalculation
    {
        public double Rfbt;


        // растяжение
        private double Rfbr3;
        // сжатие
        private double Rfb;        
        private double Rfbt3;
        // растяжение в арматуре
        private double Rs;
        // сжатие в арматуре
        private double Rsc;
        // Относительная деформация
        private double Efbt;

        private double As1;

        private const double B = 1.0;
        private double m_Wpl;
        private double gamma = 1.73 - 0.005 * (B - 15);
        
        // Площадь сжатой зоны бетона        
        private double Ab;
        // случайный эксцентриситет, принимаемый по СП 63.13330
        private double e0;

        public BSFiberCalculation()
        {
            Rfbr3 = 0;
            Rfb = 0;
            Rs = 0;            
            Rsc = 0;
            Efbt = 0;
        }

        public virtual void GetSize(double[] _t)
        {

        }


        public virtual void GetParams(double[] _t)
        {

        }

        public virtual void Calculate() { }

        public virtual Dictionary<string, double> Results()
        {
            return new Dictionary<string, double>() {};
        }

        public double Dzeta(double _x, double _h0) =>  _x / _h0;

        // упругопластический момент сопротивления сечения элемента для крайнего растянутого волокна
        private double Wpl(double _b, double _h) => _b * Math.Pow(_h, 2) * gamma / 6;  

        private double Mult() => Rfbt * m_Wpl;

        //6.5
        // предельный изгибающий момент, который может быть воспринят сечением элемента
        public double Mult_arm(double _b, double _h0,  double _x, double _h, double _a, double _a1)
        {
            double res = Rfb * _b * _x * (_h0 - 0.5 * _x) - Rfbt3 * _b * (_h - _x) * ((_h - _x)/2 - _a) + Rsc * As1* (_h0 - _a1) ;
            return res;
        }

        // высота сжатой зоны
        public double calc_x(double _bf1, double _hf1, double _bw, double _hw, double _bf, double _hf)
        {
            double res_x = Rfbt3 * (_bf1 * _hf1 + _bw * _hw + _bf * _hf) / (_bf1 * (Rfbr3 + Rfb));

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
                res_Mult = Rfb * _b * _x * (_h0 - 0.5 * _x) - Rfbt3 * _b * (_h - _x) * ((_h - _x) / 2 - _a) + Rsc * As1 * (_h0 - _a1);
                
                x = Rfbr3 * (bf1*hf1 + bw * hw + bf*hf );
            }
            else
            {
                // если граница проходит в ребре
                res_Mult = Rfb * _b * _x * (_h0 - 0.5 * _x) - Rfbt3 * _b * (_h - _x) * ((_h - _x) / 2 - _a) + Rsc * As1 * (_h0 - _a1);

                x = Rfbr3 * (bf1 * hf1 + bw * hw + bf * hf);
            }

            return (res_Mult, x);
        }

        /// <summary>
        /// 6.1.13 Расчет внецентренно сжатых сталефибробетонных элементов
        /// </summary>
        /// <param name="_N"> действующая продольная сила</param>
        /// <returns>Результат проверки </returns>
        public bool checkN(double _N)
        {
            var res = _N <= Rfb* Ab;
            return res;
        }

        // 6.22
        public double calcEccenticallyCompressed(double _b, double _h)
        {
            // случайный эксцентриситет, принимаемый по СП 63.13330
            double eta = 0;
            double D = 0;

            double Ncr = Math.PI * Math.PI * D;


            Ab = _b * _h * (1 - 2 * e0 * eta / _h);


            return 0;
        }

        public static BSFiberCalculation construct(int _profile)
        {
            switch (_profile)
            {
                case 1:
                    return new BSFibCalc_Rect();
            }

            return new BSFiberCalculation();
        }

    }
}

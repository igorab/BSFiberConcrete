using CsvHelper;
using System;
using System.ComponentModel;
using System.Collections.Generic;


namespace BSFiberConcrete
{
    /// <summary>
    /// Сечение балки
    /// </summary>    
    public enum BeamSection
    {
        [Description("Тавр")]
        TBeam = 1,
        [Description("Двутавр")]
        IBeam = 2,
        [Description("Кольцо")]
        Ring = 3,
        [Description("Прямоугольник")]
        Rect = 4
    } 

    public interface IBSFiberCalculation
    {
        void GetParams(double[] _t);

        Dictionary<string, double> GeomParams();

        void Calculate();
        Dictionary<string, double> Results();
    }

    [BSFiberCalculationAttribute(Descr = "Расчет балки таврового сечения")]
    public class BSFibCalc_TBeam : BSFiberCalculation
    {
    }

    [BSFiberCalculationAttribute(Descr = "Расчет балки двутаврового сечения")]
    public class BSFibCalc_IBeam : BSFiberCalculation
    {
        private double Rfbt3;
        private double Rfb;
        private double Mult;
        private double x;

        private double Rfbn, Yb;

        private double bf, hf, hw, bw, b1f, h1f;

        public override void GetParams(double[] _t)
        {
            base.GetParams(_t);

            (bf, hf, hw, bw, b1f, h1f) = (80, 20, 20, 20, 80, 20);
            (Yft, Yb1, Yb2, Yb3, Yb5, Rfbt3n) = (1.3, 0.9, 0.9, 1, 1, 30.58);
            (Rfbn, Yb) = (224, 1.3);
        }

        public override Dictionary<string, double> GeomParams()
        {
            Dictionary<string, double> geom = base.GeomParams();
            geom.Add("bf", bf);
            geom.Add("hf", hf);            
            geom.Add("hw", hw);
            geom.Add("bw", bw);
            geom.Add("b1f", b1f);
            geom.Add("h1f", h1f);

            return geom;
        }


        public override void Calculate()
        {
                        
            //общая высота
            double h = hf + hw + h1f;
                        
            Rfbt3 = (Rfbt3n / Yft) * Yb1 * Yb5;
                        
            Rfb = Rfbn / Yb * Yb1*Yb2*Yb3*Yb5;

            Action calc_a = delegate
            {
                x = Rfbt3* (b1f * h1f + bw*hw + bf*hf) / (b1f * (Rfbt3 + Rfb)) ;

                Mult = 0.5* Rfbt3*(b1f*(h1f - x)*(h1f + x) + bf*hf*(hf-x+2*(hw+h1f)) + bw * hw * (hw - x * 2 * h1f));
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

            Mult = Mult * 0.00001d;
        }
        
    }

    [BSFiberCalculationAttribute(Descr = "Расчет балки кольцевого сечения")]
    public class BSFibCalc_Ring : BSFiberCalculation
    {
        private double r1, r2, Rfbn, Yb;

        private double Rfb;
        private double Rfbt3;
        private double Mult;
        

        public override void GetParams(double[] _t)
        {
            base.GetParams(_t);

            (r1, r2, Rfbn, Yft, Yb, Yb1, Yb2, Yb3, Yb5) = (25, 40, 224, 1.3, 1.3, 0.9, 0.9, 1, 1);            
        }

        public override Dictionary<string, double> GeomParams()
        {
            Dictionary<string, double> geom = base.GeomParams();
            geom.Add("r1", r1);
            geom.Add("r2", r2);
            return geom;
        }

        public override Dictionary<string, double> Results()
        {
            return new Dictionary<string, double>() { { "Rfb", Rfb }, { "Rfbt3", Rfbt3 }, { "Mult", Mult } };
        }

        public override void Calculate()
        {                                    
            Rfb = Rfbn/Yb * Yb1* Yb2* Yb3* Yb5;
            
            Rfbt3 = Rfbt3n / Yft * Yb1 * Yb5;

            //толщина стенки кольца см
            double tr = r2 - r1;

            //радиус срединной поверхности стенки кольцевого элемента, определяемый по ф. (6.19)
            double rm = (r1 + r2) / 2;

            //Общая площадь кольцевого сечения, определяемая по формуле (6.18)
            double Ar = 2 * Math.PI * rm * tr;

            double ar = (0.73d * Rfbt3) / (Rfb + 2 * Rfbt3);

            //Предельный момент сечения
            Mult = Ar * (Rfb * Math.Sin(Math.PI * ar) / Math.PI + 0.234d * Rfbt3) * rm;

            //Предельный момент сечения  (т*м)
            Mult = Mult * 0.00001;
        }        
    }


    [BSFiberCalculationAttribute(Descr = "Расчет балки прямоугольного сечения")]
    public class BSFibCalc_Rect : BSFiberCalculation
    {                        
        // Упругопластический момент сопротивления
        private double Wpl;
        //Значение предельного момента сечения для изгибаемых сталефибробетонных элементов прямоугольного сечения
        private  double Mult;

        //размеры, см
        private double b;
        private double h;

        public override Dictionary<string, double> GeomParams()
        {
            Dictionary<string, double> geom = base.GeomParams();
            geom.Add("b", b);
            geom.Add("h", h);
            return geom;
        }

        public override Dictionary<string, double> Results()
        {
            return new Dictionary<string, double>() { {"Wpl", Wpl}, { "Mult", Mult} }; 
        }
        
        public override  void GetParams(double[] _t)
        {
            base.GetParams(_t);

            b = 60;
            h = 80;
            (Rfbt3n, Yft, Yb1, Yb5, B) = (_t[0], _t[1], _t[2], _t[3], _t[4]);             
        }

        public override void GetSize(double[] _t)
        {            
            (b, h) = (_t[0] , _t[1]);
        }

        public override void Calculate()
        {
            //Расчетное остаточное остаточного сопротивления осевому растяжению
            double Rfbt3 = (Rfbt3n / Yft) * Yb1 * Yb5;

            //коэффициент, учитывающий неупругие свойства фибробетона растянутой зоны сечения
            double Y = 1.73d - 0.005d * (B - 15);

            //Упругопластический момент сопротивления  Ф.(6.3)
            Wpl = b * h * h / 6 * Y;

            //Значение пердльнолго момента сечения для изгибаемых сталефибробетонных элементов прямоугольного сечения определяют по формуле (6.3) (кг*см)
            Mult = Rfbt3 * Wpl;

            //Предельный момент сечения  (т*м)
            Mult = Mult * 0.00001;
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, Inherited = true)]
    class BSFiberCalculationAttribute : Attribute
    {
        public string Name { get; set; }
        public string Descr { get; set; }
    }

    [BSFiberCalculationAttribute(Name = "Сталефибробетонные конструкции без предварительного напряжения арматуры")]
    public class BSFiberCalculation : IBSFiberCalculation
    {
        [BSFiberCalculation(Name = "Нормативное остаточное сопротивления осевому растяжению кг/см2")] 
        protected double Rfbt3n;
        [BSFiberCalculation(Name = "числовая характеристика класса фибробетона по прочности на осевое сжатие")]
        protected double B;
        // растяжение
        private double Rfbr3;
        // сжатие
        private double Rfbn;        
        private double Rfbt3;

        //TODO - арматуру - в отдельный класс 
        // растяжение в арматуре
        private double Rs;
        // сжатие в арматуре
        private double Rsc;

        // Относительная деформация
        private double Efbt;

        private double As1;
        
        private double m_Wpl;
        private double gamma;

        [BSFiberCalculation(Name = "Коэффициент надежности для расчета по предельным состояниям первой группы при назначении класса сталефибробетона по прочности на растяжение")]
        protected double Yft;
        [BSFiberCalculation(Name = "Коэффициенты условия работы")]
        protected double Yb1;
        protected double Yb2;
        protected double Yb3;
        protected double Yb5;


        // Площадь сжатой зоны бетона        
        private double Ab;
        // случайный эксцентриситет, принимаемый по СП 63.13330
        private double e0;

        public Dictionary<string, double> Coeffs { get; internal set; }
        public Dictionary<string, double> PhysParams { get; internal set; }

        public BSFiberCalculation()
        {
            Rfbt3n = 30.58;
            B = 30;
            Rfbn = 224;
            Rfbr3 = 0;           
            Rs = 0;            
            Rsc = 0;
            Efbt = 0;
            gamma = 1.73 - 0.005 * (B - 15);

            // коэффициенты
            Yft = 1.3d;
            Yb1 = 0.9;
            Yb2 = 0.9;
            Yb3 = 1;
            Yb5 = 1;            
        }

        public virtual Dictionary<string, double> GeomParams()
        {
            return new Dictionary<string, double>() { };
        }


        public virtual void GetSize(double[] _t)
        {

        }

        public virtual void GetParams(double[] _t)
        {
            PhysParams = new Dictionary<string, double>() { { "Rfbt3n", Rfbt3n }, { "B", B }, { "Rfbn", Rfbn } };
            Coeffs = new Dictionary<string, double>() { { "Yft", Yft }, { "Yb1", Yb1 }, { "Yb2", Yb2 }, { "Yb3", Yb3 }, { "Yb5", Yb5 } };
        }

        public virtual void Calculate() { }

        public virtual Dictionary<string, double> Results()
        {
            return new Dictionary<string, double>() {};
        }

        public double Dzeta(double _x, double _h0) =>  _x / _h0;

        // упругопластический момент сопротивления сечения элемента для крайнего растянутого волокна
        private double Wpl(double _b, double _h) => _b * Math.Pow(_h, 2) * gamma / 6;  

        private double Mult() => Rfbt3n * m_Wpl;

        //6.5
        // предельный изгибающий момент, который может быть воспринят сечением элемента
        public double Mult_arm(double _b, double _h0,  double _x, double _h, double _a, double _a1)
        {
            double res = Rfbn * _b * _x * (_h0 - 0.5 * _x) - Rfbt3 * _b * (_h - _x) * ((_h - _x)/2 - _a) + Rsc * As1* (_h0 - _a1) ;
            return res;
        }

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
                res_Mult = Rfbn * _b * _x * (_h0 - 0.5 * _x) - Rfbt3 * _b * (_h - _x) * ((_h - _x) / 2 - _a) + Rsc * As1 * (_h0 - _a1);
                
                x = Rfbr3 * (bf1*hf1 + bw * hw + bf*hf );
            }
            else
            {
                // если граница проходит в ребре
                res_Mult = Rfbn * _b * _x * (_h0 - 0.5 * _x) - Rfbt3 * _b * (_h - _x) * ((_h - _x) / 2 - _a) + Rsc * As1 * (_h0 - _a1);

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
            var res = _N <= Rfbn* Ab;
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

        public static BSFiberCalculation construct(BeamSection _profile)
        {
            switch (_profile)
            {
                case BeamSection.TBeam:
                    return new BSFibCalc_TBeam();
                case BeamSection.IBeam: 
                    return new BSFibCalc_IBeam();
                case BeamSection.Ring:
                    return new BSFibCalc_Ring();
                case BeamSection.Rect:
                    return new BSFibCalc_Rect();
            }

            return new BSFiberCalculation();
        }

    }
}

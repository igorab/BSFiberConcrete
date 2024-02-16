using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Reflection;

namespace BSFiberConcrete
{    
    [Description("Сечение балки")]
    public enum BeamSection
    {
        [Description("Тавровое сечение")]
        TBeam = 1,
        [Description("Двутавровое сечение")]
        IBeam = 2,
        [Description("Кольцевое сечение")]
        Ring = 3,
        [Description("Прямоугольное сечение")]
        Rect = 4
    } 

    public interface IBSFiberCalculation
    {
        void GetParams(double[] _t = null);
        
        void GetSize(double[] _t = null);

        Dictionary<string, double> GeomParams();

        void Calculate();
        Dictionary<string, double> Results();
    }
    
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, Inherited = true)]
    class BSFiberCalculationAttribute : Attribute
    {
        public string Name { get; set; }
        public string Descr { get; set; }
    }

    [DisplayName("Сталефибробетонные конструкции без предварительного напряжения арматуры")]
    public class BSFiberCalculation : IBSFiberCalculation
    {
        [DisplayName("Нормативное остаточное сопротивления осевому растяжению кг/см2")]
        public double Rfbt3n { get; set; }

        [DisplayName("числовая характеристика класса фибробетона по прочности на осевое сжатие")]
        public double B { get; set; }

        [DisplayName("Расчетные значения сопротивления  на сжатиие по СП63 кг/см2")]
        public double Rfbn { get; set; }
                
        protected double Rfbt3;       
        private double m_Wpl;
        private double gamma;
        
        protected double omega = BSMatFiber.omega;

        [BSFiberCalculation(Name = "Коэффициент надежности для расчета по предельным состояниям первой группы при назначении класса сталефибробетона по прочности на растяжение")]
        protected double Yft;
        [BSFiberCalculation(Name = "Коэффициенты условия работы")]
        protected double Yb;
        protected double Yb1;
        protected double Yb2;
        protected double Yb3;
        protected double Yb5;

        [BSFiberCalculation(Name = "Площадь сжатой зоны бетона")]
        private double Ab;
        [BSFiberCalculation(Name = "случайный эксцентриситет, принимаемый по СП 63.13330")]
        private double e0;

        public Dictionary<string, double> Coeffs {
            get {
                return  new Dictionary<string, double>() { { "Yft", Yft }, { "Yb", Yb }, { "Yb1", Yb1 }, { "Yb2", Yb2 }, { "Yb3", Yb3 }, { "Yb5", Yb5 } };
            }             
        }
        public Dictionary<string, double> PhysParams {
            get {
                return new Dictionary<string, double> { { "Rfbt3n", Rfbt3n }, { "B", B }, { "Rfbn", Rfbn } };
                }            
        }

        public string DN(Type _T, string _property) => _T.GetProperty(_property).GetCustomAttribute<DisplayNameAttribute>().DisplayName;
        
        public BSFiberCalculation()
        {
            Rfbt3n = 30.58;
            B = 30;
            Rfbn = 224;                      
            gamma = 1.73 - 0.005 * (B - 15);

            // коэффициенты
            Yft = 1.3d;
            Yb1 = 0.9;
            Yb2 = 0.9;
            Yb3 = 1;
            Yb5 = 1;            
        }

        /// <summary>
        /// Возвращает результаты расчета геометрических характеристик балки
        /// </summary>
        /// <returns>Описание геометрии балки</returns>
        public virtual Dictionary<string, double> GeomParams()
        {
            return new Dictionary<string, double>() { };
        }

        public virtual Dictionary<string, double> PhysicalParameters()
        {
            Dictionary<string, double> phys = new Dictionary<string, double>();

            phys.Add(DN(typeof(BSFiberCalculation), "Rfbt3n"), Rfbt3n);
            phys.Add(DN(typeof(BSFiberCalculation), "B"), B);
            phys.Add(DN(typeof(BSFiberCalculation), "Rfbn"), Rfbn);            

            return phys;
        }

        /// <summary>
        /// Принимает характерные размеры сечения
        /// </summary>
        /// <param name="_t">Массив - размеры сечения</param>
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

        // упругопластический момент сопротивления сечения элемента для крайнего растянутого волокна
        private double Wpl(double _b, double _h) => _b * Math.Pow(_h, 2) * gamma / 6;  

        private double Mult() => Rfbt3n * m_Wpl;
        
               
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

        public static BSFiberCalculation construct(BeamSection _profile, bool _reinforcement = false)
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
                    if (_reinforcement)
                        return new BSFiberCalc_RectRods();
                    else
                        return new BSFibCalc_Rect();
            }

            return new BSFiberCalculation();
        }

    }
}

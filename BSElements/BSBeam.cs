using MathNet.Numerics.Integration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace BSFiberConcrete
{
    public interface IBeamGeometry
    {
        double Area();

        // section moment of inertia
        double W_s();
    }

    // конечный элемент
    public class BSElement
    {
        // Номер
        public int Num;

        // Координаты Ц.Т.
        public double Z_X { get; }
        public double Z_Y { get; }
        // Площадь Ц.Т.
        public double Ab {  get => Area(); } 

        // напряжение на уровне Ц.Т.
        public double Sigma { get; set; }

        // модуль упругости
        public double E { get; set; }

        // относительная деформация
        public double Epsilon { get; set; }

        public double Nu { get => Sigma / (E * Epsilon) ;  }

        private double Area() => Z_X * Z_Y;

        public BSElement (int _N, double _X, double _Y)
        {
            Num = _N;
            Z_X = _X;
            Z_Y = _Y;            
        }
    }

    
    public class BSBeam : IBeamGeometry
    {        
        // количество стержней арматуры
        public int RodsQty {get; set;}
        public List<BSRod> Rods { get; set; }

        // Координаты Ц.Т.
        public double Zfb_X { get; set; }
        public double Z_fb_Y { get; set; }

        public double Length { get; set; }

        public virtual double Area()
        {
            return 0;
        }

        public virtual double W_s()
        {            
            return 0;
        }

        public virtual double I_s()
        {
            return 0;
        }

        public BSBeam()
        {            
        }
    }
    /// <summary>
    /// Прямоугольник
    /// </summary>
    [Description("size")]
    public class BSBeam_Rect : BSBeam
    {
        //размеры, см
        [DisplayName("Высота сечения, см")]
        public double h { get; set; }
        [DisplayName("Ширина сечения, см")]
        public double b { get; set; }

        [DisplayName("Площадь сечения элемента, см2")]
        public override double Area() => b * h;

        [DisplayName("Момент инерции прямоугольного сечения")]
        public override double I_s() => b * Math.Pow(h, 3) / 12;

        [DisplayName("Расстояние от центра тяжести сечения сталефибробетонного элемента до наиболее растянутого волокна, см")]
        public double y_t() => h / 2.0;

        // Центр тяжести сечения
        public (double, double) CG() => (b/2, h/2);
        
        public BSBeam_Rect(double _b = 0, double _h = 0)
        {
            b = _b;
            h = _h;
            Zfb_X = _b/2;
            Z_fb_Y = _h/2;
        }

    }

    /// <summary>
    /// Кольцо
    /// </summary>
    [Description("size")]
    public class BSBeam_Ring : BSBeam
    {
        [DisplayName("Радиус внутренней грани, см")]
        public double r1 { get; set; }

        [DisplayName("Радиус наружней грани, см")]
        public double r2 { get; set; }

        [DisplayName("Радиус срединной поверхности стенки кольцевого элемента")]
        public double r_m { get { return (r1 + r2) / 2d; } /*private set { r_m = value; }*/ }

        [DisplayName("толщина стенки кольца, см")]
        public double t_r { get { return r2 - r1; } }

        [DisplayName("Общая площадь кольцевого сечения")]
        public override double Area()
        {
            double t = t_r;
            if (t <= 0) return 0;
            
            double area = 2* Math.PI * r_m * t ;
            return area;
        }
    }

    /// <summary>
    /// Тавровое-Двутавровое сечение
    /// </summary>
    [Description("size")]
    public class BSBeam_IT : BSBeam
    {
        // размеры:
        [DisplayName("Ширина нижней полки двутавра")]
        public double bf { get; set; }
        [DisplayName("Высота нижней полки двутавра")]
        public double hf { get; set; }
        [DisplayName("Высота стенки двутавра")]
        public double hw { get; set; }
        [DisplayName("Ширина стенки двутавра")]
        public double bw { get; set; }
        [DisplayName("Ширина верхней полки двутавра")]
        public double b1f { get; set; }
        [DisplayName("Высота верхней полки двутавра")]
        public double h1f { get; set; }
    }

}

using System;
using System.ComponentModel;

namespace BSFiberConcrete
{
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
        public double t_r { get => r2 - r1; }

        [DisplayName("Общая площадь кольцевого сечения")]
        public override double Area()
        {
            double t = t_r;
            if (t <= 0) return 0;

            double area = 2 * Math.PI * r_m * t;
            return area;
        }

        // диаметр наружней грани
        public double d_n { get => 2 * r2; }

        // диаметр внутренней грани
        public double d_v { get => 2 * r1; }

    }
}

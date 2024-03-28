using System;
using System.ComponentModel;

namespace BSFiberConcrete
{
    /// <summary>
    /// Прямоугольник
    /// </summary>
    [Description("size")]
    public class BSBeam_Rect : BSBeam
    {
        //размеры, см
        [DisplayName("Высота сечения, см")]
        public override double h { get; set; }
        [DisplayName("Ширина сечения, см")]
        public override double b { get; set; }

        [DisplayName("Площадь сечения элемента, см2")]
        public override double Area() => b * h;

        [DisplayName("Момент инерции прямоугольного сечения")]
        public override double I_s() => b * Math.Pow(h, 3) / 12;

        [DisplayName("Расстояние от центра тяжести сечения сталефибробетонного элемента до наиболее растянутого волокна, см")]
        public double y_t() => h / 2.0;

        // Центр тяжести сечения
        public (double, double) CG() => (b / 2, h / 2);

        public BSBeam_Rect(double _b = 0, double _h = 0)
        {
            b = _b;
            h = _h;
            Zfb_X = _b / 2;
            Z_fb_Y = _h / 2;
        }

        // Моменты инерции сечения
        public override double Jy() => b * (h * h * h) / 12.0;

        public override double Jx() => (b * b * b) * h / 12.0;

        //   Моменты сопротивления сечения
        public static double Wx(double _b, double _h) => _b * _h * _h / 6.0;
        public static double Wy(double _b, double _h) => _b * _b * _h / 6.0;

        // Нормальные напряжения в сечении
        //КН, КНм, КНм  _X см, _Y см
        public double Sigma_Z(double _N, double _Mx, double _My, double _X, double _Y)
        {
            double sgm_z = _N / Area() + _Mx / Jx() * _X - _My / Jy() * _Y;
            return sgm_z;
        }
    }
}

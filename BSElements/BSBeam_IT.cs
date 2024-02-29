using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BSFiberConcrete
{
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

        /// <summary>
        ///  В обозначениях справочника проектировщика стр 357
        /// </summary>
        public double B => bf;
        public double c_h => hf;
        public override double b => bw;
        public double c_b => b1f;
        public override double h => hf;
        public double a => hw;
        public double H => c_h + c_b + h;
        public double B1 => B - a;
        public double b1 => b - a;

        public override double Area()
        {
            double area = b * c_b + a * h + B * c_h;
            return area;
        }

        public double y_h 
        {
            get => (a*H*H + B1 * c_h*c_h + b1*c_b*(2*H - c_b)) / (2 * (a*H + B1 * c_h + b1 * c_b));
        }

        public double y_b => H - y_h;

        public double h_b => y_b - c_b;

        public double h_n => y_h - c_h;

        public override double Jx()
        {
            double j_x =  1/3d * ( B * Math.Pow(y_h, 3) - B1 * Math.Pow(h_n, 3) + b * Math.Pow(y_b, 3) - b1 * Math.Pow(h_b, 3) );
            return j_x;
        }

        public override double Jy()
        {
            return base.Jy();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}

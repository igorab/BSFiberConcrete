using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFiberConcrete
{
    public interface IBSFiberCalculation
    {
        void GetParams(double[] _t = null);

        void GetSize(double[] _t = null);

        Dictionary<string, double> GeomParams();

        void Calculate();
        Dictionary<string, double> Results();
    }

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
   
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, Inherited = true)]
    class BSFiberCalculationAttribute : Attribute
    {
        public string Name { get; set; }
        public string Descr { get; set; }
    }
}

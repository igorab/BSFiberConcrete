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

        bool Calculate();
        Dictionary<string, double> Results();
    }

    [Description("Тип расчета")]
    public enum CalcType
    {
        [Description("Проверка сечения")]
        Section = 0,
        [Description("Статическое равновесие")]
        Static = 1,
        [Description("Нелинейная деформационная модель")]
        Nonlinear = 2
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
        Rect = 4,
        [Description("Тавр нижняя полка")]
        LBeam = 5
    }
   
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, Inherited = true)]
    class BSFiberCalculationAttribute : Attribute
    {
        public string Name { get; set; }
        public string Descr { get; set; }
    }
}

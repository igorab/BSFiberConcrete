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
        void SetParams(double[] _t = null);

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
        Nonlinear = 2,
        [Description("Расчет балки")]
        BeamCalc = 3
    }

              
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, Inherited = true)]
    class BSFiberCalculationAttribute : Attribute
    {
        public string Name { get; set; }
        public string Descr { get; set; }
    }
}

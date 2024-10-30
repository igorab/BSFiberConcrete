using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
namespace BSFiberConcrete.BSRFib
{
    class BSRFibReport
    {
        public static Dictionary<string, string> Fld = new Dictionary<string, string>()
        {
            ["h"] = "Высота сечения",
            ["b"] = "Ширина сечения",
            ["etaf"] = "для фибры, резанной из ...",
            ["lf"] = "по ТУ J 276-001 -70832021 СП52-104-2006* риложение Г группа 2",
            ["Sf"] = "площадь номинального поперечного сечения фибры, определяемая по ее номинальным размерам",
            ["Rfser"] = "нормативное сопротивление растяжению фибрСП52-104-2006*",
            ["Rbser"] = "рассчетное сопротивление сжатия бетона для предельных второй  СП63",
            ["dfred"] = "приведенный диаметр применяемой фибры (мм) (В.4)СП52-104-2006",
            ["lfan"] = "",
            ["Yfb1"] = "для фибры из листа и фибры из ..",
            ["mufv"] = "коэффициент фибрового армирования по объему рекомендуется принимать в пределах 0,005 < /if, < 0,018 для конструкций согласно (7.9)",
            ["lfan"] = "длина заделки фибры в бетоне, обеспечивающая ее разрыв при выдергивании",            
            ["mu_fa"] = "Коэффициент фибрового армирования по площади для растянутой зоны",
            ["mu1_fa"] = "Коэффициент фибрового армирования по площади для сжатой зоны",
        };
        public Dictionary<string, double> Res { set {m_Res = value; } }
        private Dictionary<string, double> m_Res;
        public void Run()
        {
            var x = m_Res["mu_fa"];
            var y = m_Res["mu1_fa"];
        }
    }
}

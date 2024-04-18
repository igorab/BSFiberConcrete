using BSFiberConcrete.Lib;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFiberConcrete.LocalStrength
{
    /// <summary>
    /// Расчет на продавливание
    /// </summary>
    public class BSLocalPunchCalc : BSLocalStrengthCalc
    {
        // Ширина приложения нагрузки (см)
        protected double b1;
        protected double h0;
        // Рабочая высота плиты по x(см)
        protected double h0x;
        //Рабочая высота плиты по y(см)
        protected double h0y;
        protected double u;
        protected double Afb;
        protected double Ffbult;
        
        public override void InitDataSource()
        {
            m_DS = BSData.LoadLocalPunch();

            Dictionary<string, double> D = new Dictionary<string, double>();
            foreach (var item in m_DS) D[item.VarName] = item.Value;    

            (a1, b1, h0x, h0y, Rfbtn, Yft, Yb1, Yb5, Rfbt, h0, u, Afb, Ffbult) = 
                (D["a1"], D["a2"], D["h0x"], D["h0y"], D["Rfbtn"], D["Yft"], D["Yb1"], D["Yb5"], D["Rfbt"], D["h0"], D["u"], D["Afb"], D["Ffbult"]);            
        }

        public override string ReportName()
        {
            return "Продавливание";
        }

        public override string SampleName()
        {
            return "Расчет сталефибробетонных элементов на продавливание при действии сосредоточенной силы без арматуры";
        }

        public override bool RunCalc()
        {
            (h0, u, Afb, Ffbult) = (0, 0, 0, 0);

            bool ok = base.RunCalc();

            try
            {
                // Приведенная рабочая высота сечения
                h0 = 0.5 * (h0x + h0y);

                // Периметр контура расчетного поперечного сечения
                u = 2 * a1 + 2 * b1;

                // Площадь расчетного поперечного сечения (см2) по ф.(6.92)
                Afb = u * h0;

                // Предельное усилие, воспринимаемое сталефибробетоном. (кг)
                Ffbult = Rfbt * Afb;

                Dictionary<string, double> D = new Dictionary<string, double>() 
                { 
                    ["h0"] = h0, 
                    ["u"] = u, 
                    ["Afb"] = Afb, 
                    ["Ffbult"] = Ffbult 
                };

                m_DS = BSQuery.UpdateLocalPunch(D);

                ok = true;
            }
            catch 
            {
                ok = false;
            }
            return ok;
        }
    }
}

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

        public override void RunCalc()
        {
            base.RunCalc();

            // Приведенная рабочая высота сечения
            h0 = 0.5 * (h0x + h0y);

            // Периметр контура расчетного поперечного сечения
            u = 2 * a1 + 2 * b1;

            // Площадь расчетного поперечного сечения (см2) по ф.(6.92)


            // Предельное усилие, воспринимаемое сталефибробетоном. (кг)

        }
    }
}

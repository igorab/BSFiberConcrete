using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFiberConcrete
{

    [Description("Тип предела текучести")]
    public enum TypeYieldStress
    {
        [Description("Не определено")]
        None = 0,
        [Description("Физичесикй")]
        Physical = 1,
        [Description("Условный")]
        Offset = 2,
    }
}

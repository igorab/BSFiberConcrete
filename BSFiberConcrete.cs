using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BSFiberConcrete
{
    /// <summary>
    /// Расчет балок из фибробетона
    /// </summary>
    internal static class BSFiberConcrete
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new BSFiberMain());
            // Application.Run(new BSCalcMenu.BSCalcMenu());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CBAnsDes
{
    /// <summary>
    /// Анализ балки
    /// </summary>
    public static class BSBeamCalculator
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            CBAnsDes.My.MyApplication.Main([]);

            //Application.Run(new MDIMain());
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;
using BSBeamCalculator.model;

namespace BSBeamCalculator
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());



        }

    }

    /// <summary>
    /// Класс нужен для сбора информации с формы
    /// и дальнейшей передачи информации в вычислительный класс
    /// </summary>
    public static class Controller
    {
        /// <summary>
        /// Длинна балки
        /// </summary>
        public static double l;
        /// <summary>
        /// Тип защемления балки
        /// </summary>
        public static string support;
        /// <summary>
        /// тип нагрузки на балку
        /// </summary>
        public static string load;
        /// <summary>
        /// Значение силы на балку
        /// </summary>
        public static double f;
        /// <summary>
        /// координата приложение силы
        /// </summary>
        public static double x1;
        /// <summary>
        /// конечная координата приложение силы
        /// (для распределенной нагрузки)
        /// </summary>
        public static double x2;

        public static DiagramResult result;
        public static void RunCalculation()
        {
            BeamDiagram BD = new BeamDiagram(support, load, l, f, x1, x2);
            result = BD.CalculateBeamDiagram();
        }

    }
}

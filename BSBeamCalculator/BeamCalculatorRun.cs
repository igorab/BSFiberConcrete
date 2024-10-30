using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;
namespace BSBeamCalculator
{
    internal static class BeamCalculatorRun
    {
                                [STAThread]
        static void Main()
        {
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new BeamCalculatorForm());
            }
        }
    }
}

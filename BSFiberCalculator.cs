﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BSFiberConcrete
{
                internal static class BSFiberCalculator
    {
                                [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            Application.Run(new BSCalcMenu.BSCalcMenu());
        }
    }
}

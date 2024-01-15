using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFiberConcrete
{
    class BSFiberBeton
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
    }

    public class Elements
    {
        public double Rfbt3n { get; set; }
        public double Rfbn { get; set; }
        public double Yft { get; set; }
        public double Yb1 { get; set; }
        public double Yb2 { get; set; }
        public double Yb3 { get; set; }
        public double Yb5 { get; set; }
        public int B { get; set; }
    }

    internal class BSFiberCocreteLib
    {
        public static List<BSFiberBeton> betonList = new List<BSFiberBeton>
        {
            new BSFiberBeton{Id = 15, Name = "B15", Type = 0 },
            new BSFiberBeton{Id = 20, Name = "B20", Type = 1 },
            new BSFiberBeton{Id = 30, Name = "B30", Type = 2 }
        };

        public static Elements PhysElements = new Elements { Rfbt3n = 30.58, Rfbn = 224.0, Yft = 1.3, Yb1 = 0.9, Yb2 = 0.9, Yb3 = 0.9, Yb5 = 1,  B = 30 };

    }
}

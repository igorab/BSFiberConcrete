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
            new BSFiberBeton{Id = 2, Name = "B2.5", Type = 0 },
            new BSFiberBeton{Id = 3, Name = "B3.5", Type = 0 },
            new BSFiberBeton{Id = 5, Name = "B5", Type = 0 },            
            new BSFiberBeton{Id = 7, Name = "B7.5", Type = 0 },
            new BSFiberBeton{Id = 10, Name = "B10", Type = 0 },
            new BSFiberBeton{Id = 12, Name = "B12.5",Type = 0},
            new BSFiberBeton{Id = 15, Name = "B15", Type = 0 },
            new BSFiberBeton{Id = 25, Name = "B25", Type = 1 },
            new BSFiberBeton{Id = 30, Name = "B30", Type = 2 },
            new BSFiberBeton{Id = 35, Name = "B35", Type = 2 },
            new BSFiberBeton{Id = 40, Name = "B40", Type = 2 },
            new BSFiberBeton{Id = 45, Name = "B45", Type = 2 },
            new BSFiberBeton{Id = 50, Name = "B50", Type = 2 },
            new BSFiberBeton{Id = 55, Name = "B55", Type = 2 },
            new BSFiberBeton{Id = 60, Name = "B60", Type = 2 }
        };

        public static Elements PhysElements = new Elements { Rfbt3n = 30.58, Rfbn = 224.0, Yft = 1.3, Yb1 = 0.9, Yb2 = 0.9, Yb3 = 0.9, Yb5 = 1,  B = 30 };

    }
}

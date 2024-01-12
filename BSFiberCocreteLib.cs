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


    internal class BSFiberCocreteLib
    {
        public static List<BSFiberBeton> betonList = new List<BSFiberBeton>
        {
            new BSFiberBeton{Id = 15, Name = "B15", Type = 0 },
            new BSFiberBeton{Id = 20, Name = "B20", Type = 1 },
            new BSFiberBeton{Id = 30, Name = "B30", Type = 2 }
        };
            

    }
}

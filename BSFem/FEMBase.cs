using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFem
{
    //Table
    public struct Element
    {
        public int Num { get; set; }
        public int I { get; set; }
        public int J { get; set; }
        public int M { get; set; }
    }


    public struct Node
    {
        public int Num { get; set; }

        public float X { get; set; }

        public float Y { get; set; }
    }


    public struct ElementParams
    {
        public int Num { get; set; }

        public float b_i { get; set; }
        public float b_j { get; set; }
        public float b_m { get; set; }

        public float c_i { get; set; }
        public float c_j { get; set; }
        public float c_m { get; set; }
    }



    internal class FEMBase
    {
    }
}

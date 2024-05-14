using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFiberConcrete.Section
{
    public class BSPoint
    {
        public int Num { get; set; }
        public float X { get; set; }
        public float Y { get; set; }

        public BSPoint()
        {

        }

        public BSPoint(Point _point)
        {
            X = _point.X;
            Y = _point.Y;   
        }

        public BSPoint(PointF _pointF)
        {
            X = _pointF.X;
            Y = _pointF.Y;
        }


    }

    public class BSSection
    {
    }
}

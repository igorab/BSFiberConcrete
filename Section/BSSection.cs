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
        public static void IBeam(double[] _Sz, out List<PointF> PointsSection, out PointF _center)
        {
            float[] Sz = Array.ConvertAll(_Sz, element => (float)element);

            float bf = Sz[0], hf = Sz[1], bw = Sz[2], hw = Sz[3], b1f = Sz[4], h1f = Sz[5];

            _center = new PointF(0, (hf + hw + h1f) /2f);

            PointsSection = new List<PointF>()
            {
                new PointF(bf/2f, 0),
                new PointF(bf/2f, hf) ,
                new PointF(bw/2f, hf),
                new PointF(bw/2f, hf + hw),
                new PointF(b1f/2f, hf + hw),
                new PointF(b1f/2f, hf + hw + h1f),
                new PointF(-b1f/2f, hf + hw + h1f),
                new PointF(-b1f/2f, hf + hw),
                new PointF(-bw/2f, hf + hw),
                new PointF(-bw/2f, hf),
                new PointF(-bf/2f, hf),
                new PointF(-bf/2f, 0)
            };
        }
    }
}

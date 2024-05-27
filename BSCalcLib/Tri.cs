using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TriangleNet;


//using System.G
using TriangleNet.Geometry;
using TriangleNet.IO;
using TriangleNet.Meshing;
using TriangleNet.Rendering.Text;



namespace BSCalcLib
{
    public abstract class Tri
    {
        public static string FilePath { get; set; }

        public static Mesh Mesh { get; set; }

        public static List<object> CalculationScheme(int _N = 10, int _M = 1)
        {
            List<object> result = new List<object> { new object() };

            foreach (var edge in Mesh.Edges)
            {

            }

            foreach (var hole in Mesh.Holes)
            {

            }

            foreach (var vertex in Mesh.Vertices)
            {

            }

            foreach (var tri in Mesh.Triangles)
            {
                var b = tri.Bounds();
                var a = tri.Area;


            }



            return result;
        }


        public static string CreateContour(List<System.Drawing.PointF> _points)
        {            
            Polygon p = new Polygon();

            Vertex[] vrtx = new Vertex[_points.Count];

            int vIdx = 0;
            foreach (var point in _points)   
            {
                var v = new Vertex(point.X, point.Y, 1);
                vrtx[vIdx] = v;
                vIdx++;
            };

            // Add the outer box contour with boundary marker 1.
            p.Add(new Contour(vrtx, 1));

            ConstraintOptions options = new ConstraintOptions() { Convex = false, ConformingDelaunay = true };

            QualityOptions quality = new QualityOptions()
            {
                MinimumAngle = 25.0,
                VariableArea = true
            };

            Mesh = p.Triangulate(options, quality) as Mesh;

            CalculationScheme();

            string svgPath = Path.Combine(FilePath, "IBeam.svg");

            SvgImage.Save(Mesh, svgPath, 800);

            string polyPath = Path.Combine(FilePath, "IBeam.poly");
            FileProcessor.Write(Mesh, polyPath);
            //string s = p.ToString();

            return svgPath;
        }
    }

    //Polygon

    // Creating a polygon

    // Using contours
    public class TriPoly : Tri
    {
        public static void Example()
        {
            var p = new Polygon();

            // Add the outer box contour with boundary marker 1.
            p.Add(new Contour(new Vertex[4]
            {
                new Vertex(0.0, 0.0, 1),
                new Vertex(3.0, 0.0, 1),
                new Vertex(3.0, 3.0, 1),
                new Vertex(0.0, 3.0, 1)
            }, 1));

            // Add the inner box contour with boundary marker 2.
            p.Add(new Contour(new Vertex[4]
            {
                new Vertex(1.0, 1.0, 2),
                new Vertex(2.0, 1.0, 2),
                new Vertex(2.0, 2.0, 2),
                new Vertex(1.0, 2.0, 2)
                }, 2)
            , new Point(1.5, 1.5)); // Make it a hole.
        }
    }

    // Using segments
    public class TriSegment : Tri
    {
        public static void Example()
        {
            var p = new Polygon();

            var v = new Vertex[4]
            {
                new Vertex(0.0, 0.0, 1),
                new Vertex(3.0, 0.0, 1),
                new Vertex(3.0, 3.0, 1),
                new Vertex(0.0, 3.0, 1)
            };

            // Add segments of the outer box.
            p.Add(new Segment(v[0], v[1], 1), 0);
            p.Add(new Segment(v[1], v[2], 1), 0);
            p.Add(new Segment(v[2], v[3], 1), 0);
            p.Add(new Segment(v[3], v[0], 1), 0);

            v = new Vertex[4]
            {
                new Vertex(1.0, 1.0, 2),
                new Vertex(2.0, 1.0, 2),
                new Vertex(2.0, 2.0, 2),
                new Vertex(1.0, 2.0, 2)
            };

            // Add segments of the inner box.
            p.Add(new Segment(v[0], v[1], 2), 0);
            p.Add(new Segment(v[1], v[2], 2), 0);
            p.Add(new Segment(v[2], v[3], 2), 0);
            p.Add(new Segment(v[3], v[0], 2), 0);

            // Add the hole.
            p.Holes.Add(new Point(1.5, 1.5));
        }
    }
}

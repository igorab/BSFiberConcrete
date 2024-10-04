using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
using TriangleNet.Meshing.Algorithm;
using TriangleNet.Rendering.Text;
using TriangleNet.Smoothing;
using TriangleNet.Tools;
using TriangleNet.Topology;


namespace BSCalcLib
{
    public abstract class Tri
    {
        public static string FilePath { get; set; }
        public static double MinAngle { get; set; }

        public static double MaxArea { get; set; }

        public static List<double> triAreas;
        public static List<Point> triCGs;

        /// <summary>
        /// смещение начала координат
        /// </summary>
        public static Point Oxy { get; set; } 

        public static Mesh Mesh { get; set; }

        static Tri()
        {
            triAreas = new List<double>();
            triCGs = new List<Point>();
            MinAngle = 25.0;
            MaxArea = 10;
            Oxy = new Point() {ID = 0, X = 0, Y = 0 };
        }

        /// <summary>
        /// Расчетная схема сечения
        /// </summary>
        /// <returns>ц.т. треугольников и их площади</returns>
        public static List<object> CalculationScheme()
        {            
            List<object> result = new List<object> { new object() };
            if (Mesh is null) return result;

            HashSet<Rectangle> rects = new HashSet<Rectangle>();
            triAreas = new List<double>();
            triCGs = new List<Point>();

            int triIdx = 0;
            foreach (Triangle tri in Mesh.Triangles)
            {
                Rectangle rec = tri.Bounds();               
                rects.Add(rec);

                double a = tri.Area;
                int vId0 = tri.GetVertexID(0);
                int vId1 = tri.GetVertexID(1);
                int vId2 = tri.GetVertexID(2);

                Vertex v0 = tri.GetVertex(0);
                Vertex v1 = tri.GetVertex(1);
                Vertex v2 = tri.GetVertex(2);

                // ц.т. треугольника - смещение начала координат
                double cg_X = - ((v0.X + v1.X + v2.X) / 3.0 - Oxy.X);
                double cg_Y = - ((v0.Y + v1.Y + v2.Y) / 3.0 - Oxy.Y);

                // Центр тяжести треугольника
                Point triCG = new Point() 
                {
                    ID = triIdx,  
                    X = cg_X, 
                    Y = cg_Y 
                };

                triCGs.Add(triCG);
                triAreas.Add((rec.Width * rec.Height)/2.0);

                triIdx++;
            }

            result.Add(triAreas);
            result.Add(triCGs);

            /*
            TriangleQuadTree qtree = new TriangleQuadTree(Mesh);
            var xTri = qtree.Query(0, 0);
           
            foreach (Edge edge in Mesh.Edges)
            {
                var edgeNum = edge.Label;
                var pointFrom =  edge.P0;
                var pointTo =  edge.P1;
            }

            foreach (Point hole in Mesh.Holes)
            {
                var h_x = hole.X;
                var h_y = hole.X;
            }

            foreach (Vertex vertex in Mesh.Vertices)
            {
                var v_x = vertex.X;
                var v_y = vertex.Y;
                var v_Num = vertex.Label;
                var v_Id = vertex.ID;
            }
            */

            return result;
        }

        /// <summary>
        /// Сформировать контур сечения
        /// </summary>
        /// <param name="_points">координаты точек</param>
        /// <returns>Путь к файлу</returns>
        public static string CreateSectionContour(List<System.Drawing.PointF> _points)
        {
            if (_points.Count == 0) return "";  

            Polygon p = new Polygon();

            Vertex[] vrtx = new Vertex[_points.Count];

            int vIdx = 0;
            foreach (var point in _points)   
            {                
                vrtx[vIdx] = new Vertex(point.X, point.Y, 1);
                vIdx++;
            };

            // Add the outer box contour with boundary marker 1.
            p.Add(new Contour(vrtx, 1));

            ConstraintOptions options = new ConstraintOptions() { ConformingDelaunay = true };
            
            QualityOptions quality = new QualityOptions()
            {
                MinimumAngle = MinAngle
                //,VariableArea = false
            };
            quality.UseLegacyRefinement = true;
            quality.MaximumAngle = 180;
            //quality.MaximumArea = MaxArea;
            
            Mesh = p.Triangulate(options, quality) as Mesh;

            var statistic = new Statistic();
            statistic.Update(Mesh, 1);

            // Refine by setting a custom maximum area constraint.
            
            Mesh.Refine(quality);
            
            //var smoother = new SimpleSmoother();
            //smoother.Smooth(Mesh, 5);
            string svgPath = Path.Combine(FilePath, "IBeam.svg");

            SvgImage.Save(Mesh, svgPath, 800);

            string polyPath = Path.Combine(FilePath, "IBeam.poly");
            FileProcessor.Write(Mesh, polyPath);

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

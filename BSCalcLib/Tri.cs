using System;
using System.Collections.Generic;
using System.IO;
using TriangleNet;
using TriangleNet.Geometry;
using TriangleNet.IO;
using TriangleNet.Meshing;
using TriangleNet.Rendering.Text;
using TriangleNet.Tools;
using TriangleNet.Topology;
namespace BSCalcLib
{
    public abstract class Tri
    {
        public static string FilePath { get; set; }
        public static double MinAngle { get; set; }
        
        public static List<double> triAreas;
        public static List<Point> triCGs;
                                public static Point Oxy { get; set; } 
        public static Mesh Mesh { get; set; }
        static Tri()
        {
            triAreas = new List<double>();
            triCGs = new List<Point>();
            MinAngle = 25.0;            
            Oxy = new Point() {ID = 0, X = 0, Y = 0 };
            FilePath = Path.Combine(Environment.CurrentDirectory, "Templates");
        }
                                        public static List<object> CalculationScheme(bool bOxy = true)
        {            
            List<object> result = new List<object> { new object() };
            if (Mesh is null) return result;
            HashSet<Rectangle> bounds = new HashSet<Rectangle>();
            triAreas = new List<double>();
            triCGs = new List<Point>();
            int triIdx = 0;
            foreach (Triangle tri in Mesh.Triangles)
            {
                Rectangle rect = tri.Bounds();               
                bounds.Add(rect);
                double a = tri.Area;
                int vId0 = tri.GetVertexID(0);
                int vId1 = tri.GetVertexID(1);
                int vId2 = tri.GetVertexID(2);
                Vertex v0 = tri.GetVertex(0);
                Vertex v1 = tri.GetVertex(1);
                Vertex v2 = tri.GetVertex(2);
                                double cg_X;
                double cg_Y;
                if (bOxy)
                {
                    cg_X = Oxy.X - (v0.X + v1.X + v2.X) / 3.0;
                    cg_Y = Oxy.Y - ((v0.Y + v1.Y + v2.Y) / 3.0);
                }
                else
                {
                    cg_X =  (v0.X + v1.X + v2.X) / 3.0;
                    cg_Y =  (v0.Y + v1.Y + v2.Y) / 3.0;
                }
                                Point triCG = new Point() 
                {
                    ID = triIdx,  
                    X = cg_X, 
                    Y = cg_Y 
                };
                triCGs.Add(triCG);
                triAreas.Add((rect.Width * rect.Height)/2.0);
                triIdx++;
            }
            result.Add(triAreas);
            result.Add(triCGs);
           
            return result;
        }
        public static Polygon MakePolygon(List<System.Drawing.PointF> _points)
        {           
            Polygon poly = new Polygon();
            Vertex[] vrtx = new Vertex[_points.Count];
            int vIdx = 0;
            foreach (var point in _points)
            {
                vrtx[vIdx] = new Vertex(point.X, point.Y, 1);
                vIdx++;
            };
            
            poly.Add(new Contour(vrtx, 1));
            return poly;                       
        }
                                                public static string CreateSectionContour(List<System.Drawing.PointF> _points, double _MaxArea)
        {
            if (_points.Count == 0) return "";
            var poly = MakePolygon(_points);
            
            ConstraintOptions options = new ConstraintOptions() { ConformingDelaunay = true };
            
            QualityOptions quality = new QualityOptions()
            {
                MinimumAngle = MinAngle
                            };
            quality.UseLegacyRefinement = true;
            quality.MaximumAngle = 180;
            if (_MaxArea > 0)
                quality.MaximumArea = _MaxArea;
            
            Mesh = poly.Triangulate(options, quality) as Mesh;
            var statistic = new Statistic();
            statistic.Update(Mesh, 1);
                        
            Mesh.Refine(quality);
            
                                    string svgPath = Path.Combine(FilePath, "IBeam.svg");
            SvgImage.Save(Mesh, svgPath, 800);
            string polyPath = Path.Combine(FilePath, "IBeam.poly");
            FileProcessor.Write(Mesh, polyPath);
            return svgPath;
        }
    }
    
    
        public class TriPoly : Tri
    {
        public static void Example()
        {
            var p = new Polygon();
                        p.Add(new Contour(new Vertex[4]
            {
                new Vertex(0.0, 0.0, 1),
                new Vertex(3.0, 0.0, 1),
                new Vertex(3.0, 3.0, 1),
                new Vertex(0.0, 3.0, 1)
            }, 1));
                        p.Add(new Contour(new Vertex[4]
            {
                new Vertex(1.0, 1.0, 2),
                new Vertex(2.0, 1.0, 2),
                new Vertex(2.0, 2.0, 2),
                new Vertex(1.0, 2.0, 2)
                }, 2)
            , new Point(1.5, 1.5));         }
    }
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
                        p.Add(new Segment(v[0], v[1], 2), 0);
            p.Add(new Segment(v[1], v[2], 2), 0);
            p.Add(new Segment(v[2], v[3], 2), 0);
            p.Add(new Segment(v[3], v[0], 2), 0);
                        p.Holes.Add(new Point(1.5, 1.5));
        }
    }
}

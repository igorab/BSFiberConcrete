using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Xml.Linq;
using MathNet.Numerics.Optimization.LineSearch;
using netDxf;
using netDxf.Collections;
using netDxf.Entities;
using static netDxf.Entities.HatchBoundaryPath;
using System.Diagnostics;
using System.Management;
using System.Runtime.InteropServices;
using System.Windows;

namespace BSFiberConcrete.Section.DXFReader
{
    public class DxfAreaAnalyzer
    {

        private string _pathToDXF;

        public double Area { get; private set; }

        public List<SimpleCoord> Coordinates { get; private set; }

        public EntityObject _selectedObject;




        public DxfAreaAnalyzer(string pathToFile)
        {
            _pathToDXF = pathToFile;
            Coordinates = new List<SimpleCoord>();
        }



        public void ParseDXF()
        {
            //var ver = DxfDocument.CheckDxfFileVersion(_pathToDXF);

            DxfDocument doc = DxfDocument.Load(_pathToDXF);
            DrawingEntities res = doc.Entities;

            //var tesT = res.All.Where((item,index) => item is netDxf.Entities.Line );

            foreach (var entity in res.All)
            {
                var AreaAndCoords  = CalculateArea(entity);
                if ((AreaAndCoords.Item1 > Area) && (AreaAndCoords.Item2.Count > 1))
                {
                    Area = AreaAndCoords.Item1;
                    _selectedObject = entity;
                    Coordinates = AreaAndCoords.Item2;
                }
            }
        }




        public static (double, List<SimpleCoord>) CalculateArea(EntityObject entity)
        {
            double area = 0;
            List<SimpleCoord> coord = new List<SimpleCoord>();

            switch (entity)
            {
                //case netDxf.Entities.MLine lineTest when lineTest.IsClosed:
                //    return CalculateLwPolylineArea(lineTest);

                //case netDxf.Entities.Line lineTest when lineTest.EndPoint != null:
                //    var testEndPoint = lineTest.EndPoint;
                //    var testFirstPoint = lineTest.StartPoint;
                //    break;

                case netDxf.Entities.Polyline2D lineTest:

                    if (lineTest.Vertexes.Count < 3)
                    { break; }


                    if (lineTest.IsClosed)
                    {
                        List<Polyline2DVertex> tmpVertex = lineTest.Vertexes;
                        // замыкание контура
                        tmpVertex.Add(tmpVertex[0]);
                        area = CalculateLwPolylineArea(tmpVertex);
                        coord = SimpleCoord.ConvertFrom2DVertex(tmpVertex);
                        break;
                    }

                    if (lineTest.Vertexes[0].Position != lineTest.Vertexes[lineTest.Vertexes.Count - 1].Position)
                    { break; }

                    area = CalculateLwPolylineArea(lineTest.Vertexes);
                    coord = SimpleCoord.ConvertFrom2DVertex(lineTest.Vertexes);
                    break;


                case netDxf.Entities.Circle circle:
                    area = Math.PI * Math.Pow(circle.Radius, 2);
                    coord = SimpleCoord.CreateCoordFromCircle(circle.Center.X, circle.Center.Y, circle.Radius); 
                    break;

                //case netDxf.Entities.Ellipse ellipse when ellipse.IsFullEllipse:
                //    double major = ellipse.MajorAxis;
                //    double minor = major * ellipse.MinorAxis;
                //    area =  Math.PI * major * minor;
                //    break;

                //case Hatch hatch when hatch.BoundaryPaths.Count > 0:
                //    area = CalculateHatchArea(hatch);
                //    break;
            }


            SimpleCoord[] a = new SimpleCoord[3];
            return (area, coord);
        }

        public static double CalculateLwPolylineArea(List<Polyline2DVertex> vert)
        {
            double area = 0;
            int count = vert.Count;

            for (int i = 0; i < count; i++)
            {
                var current = vert[i];
                var next = vert[(i + 1) % count];

                area += current.Position.X * next.Position.Y -
                        next.Position.X * current.Position.Y;
            }

            return Math.Abs(area / 2.0);
        }

        public static double CalculateHatchArea(Hatch hatch)
        {
            double area = 0;
            foreach (var boundary in hatch.BoundaryPaths)
            {
                if (boundary.Edges.Count == 0) continue;

                //var vertices = path.Edges.ToPolyline(0.001).Vertexes;
                //int count = vertices.Count;
                double totalArea = 0;

                //netDxf.Entities.MLine
                //if (boundary is netDxf.Entities.Polyline polyline)
                //{
                //    totalArea += CalculatePolygonArea(polyline.Vertexes);
                //}


                //for (int i = 0; path.Edges.Count > i; i++)
                //{
                //    var current = vertices[i];
                //    var next = vertices[(i + 1) % count];

                //    area += current.Position.X * next.Position.Y -
                //            next.Position.X * current.Position.Y;
                //}
            }
            return Math.Abs(area / 2.0);
        }
    }


    public class SimpleCoord
    { 
        public double X { get; set; }
        public double Y { get; set; }


        public static List<SimpleCoord> ConvertFrom2DVertex(List<Polyline2DVertex> vertices)
        {
            List<SimpleCoord> result = new List<SimpleCoord> ();

            foreach (Polyline2DVertex vertex in vertices)
            {
                SimpleCoord tmpCoord = new SimpleCoord()
                {
                    X = vertex.Position.X,
                    Y = vertex.Position.Y
                };
                result.Add(tmpCoord);
            }
            return result;
        }

        public static List<SimpleCoord> CreateCoordFromCircle(double centerX, double centerY, double radius)
        {
            List<SimpleCoord> result = new List<SimpleCoord>();

            int countOfPoint = 41;

            //double delta = radius * 2 / countOfPoint;
            //for (int i = 0; countOfPoint > i; i++)
            //{
            //    double tmpX = centerX - radius + delta * i;
            //    double tmpY = Math.Sqrt(Math.Abs(radius * radius - (tmpX - centerX) * (tmpX - centerX))) + centerY;
            //    result.Add(new SimpleCoord() { X = tmpX, Y = tmpY });
            //}
            //for (int i = 0; countOfPoint > i; i++)
            //{
            //    double tmpX = centerX + radius - delta * i;
            //    double tmpY = -Math.Sqrt(Math.Abs(radius * radius - (tmpX - centerX) * (tmpX - centerX))) + centerY;
            //    result.Add(new SimpleCoord() { X = tmpX, Y = tmpY });
            //}



            for (int i = 0; countOfPoint > i; i++)
            {
                double tmpX = radius * Math.Cos(2 * Math.PI * i/ countOfPoint);
                double tmpY = radius * Math.Sin(2 * Math.PI * i / countOfPoint);
                result.Add(new SimpleCoord() { X = tmpX, Y = tmpY });

            }

            result.Add(result[0]);

            return result;
        }
    }
}

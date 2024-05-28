using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using TriangleNet;
using TriangleNet.Geometry;
using TriangleNet.IO;
using TriangleNet.Meshing;
using TriangleNet.Meshing.Algorithm;
using TriangleNet.Rendering.Text;
using TriangleNet.Topology;


namespace BSCalcLib
{
    public class BSMesh
    {
        public static int Nx { get; set; }
        public static int Ny { get; set; }

        public static Mesh Mesh { get; set; }

        public static string FilePath {  get; set; }     

        static BSMesh()
        {
            Nx = 2;
            Ny = 2;
        }

        public static void Example()
        {
            // Load polygon from file.
            var polygon = FileProcessor.Read("superior.poly");

            var options = new ConstraintOptions() { ConformingDelaunay = true };
            var quality = new QualityOptions() { MinimumAngle = 25 };

            // Triangulate the polygon
            var mesh = polygon.Triangulate(options, quality);
        }

        //Generating a structured mesh
        public static void Generate()
        {
            // Create unit square.
            Rectangle bounds = new Rectangle(-1.0, -1.0, 2.0, 2.0);

            // Generate mesh.
            IMesh mesh = GenericMesher.StructuredMesh(bounds, 5, 5);            

            SvgImage.Save(mesh, "rectangle1.svg", 5000);
            int cnt =  mesh.Triangles.Count ;
        }

        public static string Generate(List<double> _points)
        {
            double x, y, w, h;
            (x, y, w, h) = (_points[0], _points[1], _points[2], _points[3]);

            try
            {
                // Create unit square.
                Rectangle bounds = new Rectangle(x, y, w, h);

                // Generate mesh.
                Mesh = GenericMesher.StructuredMesh(bounds, Nx, Ny) as Mesh;

                string svgPath = Path.Combine(FilePath, "rectangle1.svg");

                SvgImage.Save(Mesh, svgPath, 800);

                int cnt = Mesh.Triangles.Count;

                foreach (Triangle tr in Mesh.Triangles)
                {
                    var a = tr.Area;
                }


                return svgPath;
            }
            catch
            {
                return "";
            }
        }


    }
}

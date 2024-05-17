using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

using TriangleNet.Geometry;
using TriangleNet.IO;
using TriangleNet.Meshing;
using TriangleNet.Meshing.Algorithm;
using TriangleNet.Rendering.Text;


namespace BSCalcLib
{
    public class Mesh
    {
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
            IMesh mesh = GenericMesher.StructuredMesh(bounds, 20, 20);

            SvgImage.Save(mesh, "rectangle1.svg", 500);
            int cnt =  mesh.Triangles.Count ;
        }


    }
}

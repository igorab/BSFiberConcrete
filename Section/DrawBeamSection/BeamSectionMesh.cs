using ScottPlot.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TriangleNet.Topology;
using TriangleNet;

namespace BSFiberConcrete
{
    public partial class BeamSectionMesh : Form
    {
        /// <summary>
        ///  Форма для отрисовки FormsPlot (ScottPanel)
        /// </summary>
        protected FormsPlot _plotForForms = new FormsPlot() { Dock = DockStyle.Fill };
        
        public FormsPlot PlotForForms
        {
            get { return _plotForForms; }
            set 
            {
                _plotForForms = value;
                pnlForPlot.Controls.Clear();
                pnlForPlot.Controls.Add(PlotForForms);
            }
        }

        public BeamSectionMesh()
        {
            InitializeComponent();

            // Add the FormsPlot to the panel
            pnlForPlot.Controls.Add(_plotForForms);
        }



        //private void TestScottPLot()
        //{

        //    FormsPlot FormsPlot1 = new FormsPlot() { Dock = DockStyle.Fill };

        //    ScottPlot.Coordinates[] points =
        //    {
        //        new ScottPlot.Coordinates(0,   0.25),
        //        new ScottPlot.Coordinates(0.3, 0.75),
        //        new ScottPlot.Coordinates(1,   1),
        //        new ScottPlot.Coordinates(0.7, 0.5),
        //        new ScottPlot.Coordinates(1,   0)
        //    };

        //    FormsPlot1.Plot.Add.Polygon(points);

        //    ScottPlot.Plot plot1 = FormsPlot1.Plot;


        //    pnlForPlot.Controls.Add(FormsPlot1);
        //    //FormsPlot1.Plot.SavePng("demo.png", 400, 300);

        //}


        //public void TestCreateMeshSection(Mesh mesh)
        //{

        //    var plt2 = formsPlot1.Plot;
        //    //var plt = new ScottPlot.Plot(600, 400);
        //    Random r = new Random();
        //    // полигоны сетки сечения
        //    int numSeries = 3;
        //    foreach (Triangle tr in mesh.Triangles)
        //    {

        //        double[] valueX = new double[3];
        //        double[] valueY = new double[3];

        //        for (int i = 0; i < 3; i++)
        //        {
        //            valueX[i] = tr.GetVertex(i).X;
        //            valueY[i] = tr.GetVertex(i).Y;
        //        }

        //        if (r.Next(0, 10) > 5)
        //        {
        //            plt2.AddPolygon(valueX, valueY, Color.White, lineWidth: 1);
        //        }
        //        else
        //        {
        //            plt2.AddPolygon(valueX, valueY, Color.Blue, lineWidth: 1, lineColor: Color.White);
        //        }
        //        Color aaColor = plt2.GetNextColor(0.8);

        //    }
        //    plt2.SaveFig("polygon_filledLinePlot.png");

        //}


        //public void TestCreateMeshSection(Mesh mesh)
        //{
        //    // It's work!

        //    FormsPlot FormsPlot1 = new FormsPlot() { Dock = DockStyle.Fill };

        //    // Add the FormsPlot to the panel
        //    pnlForPlot.Controls.Add(FormsPlot1);

        //    // Plot data using the control
        //    double[] data = ScottPlot.Generate.Sin();
        //    FormsPlot1.Plot.Add.Signal(data);
        //    pnlForPlot.Refresh();
        //}


        public void TestCreateMeshSection(Mesh mesh)
        {

            FormsPlot FormsPlot1 = new FormsPlot() { Dock = DockStyle.Fill };
            //ScottPlot.Plot plot1 = FormsPlot1.Plot;
            pnlForPlot.Controls.Add(FormsPlot1);


            //var plt = new ScottPlot.Plot();


            Random r = new Random();

            // полигоны сетки сечения
            foreach (Triangle tr in mesh.Triangles)
            {
                int randValue = r.Next(0, 10);

                ScottPlot.Coordinates[] pointss = new ScottPlot.Coordinates[3];
                for (int i = 0; i < 3; i++)
                {
                    pointss[i] = new ScottPlot.Coordinates(tr.GetVertex(i).X, tr.GetVertex(i).Y);
                }
                ScottPlot.Plottables.Polygon tmpPolygon = FormsPlot1.Plot.Add.Polygon(pointss);


                tmpPolygon.LineColor = ScottPlot.Colors.White;
                if (randValue > 7)
                {
                    tmpPolygon.FillColor = ScottPlot.Colors.Red;
                }
                else if (randValue < 4)
                {
                    tmpPolygon.FillColor = ScottPlot.Colors.Blue;
                }
                else
                {
                    tmpPolygon.FillColor = ScottPlot.Colors.Green;
                }

            }

            //pnlForPlot.Refresh();
            //myPlot.SavePng("demo.png", 400, 300);

        }


        //public void TestCreateMeshSection(Mesh mesh)
        //{
        //    ScottPlot.FormsPlot FormsPlot1 = new ScottPlot.FormsPlot() { Dock = DockStyle.Fill };
        //    //ScottPlot.Plot plot1 = FormsPlot1.Plot;
        //    pnlForPlot.Controls.Add(FormsPlot1);


        //    var plt = new ScottPlot.Plot();


        //    foreach (SubSegment segment in mesh.Segments)
        //    {
        //        double num2 = segment.GetVertex(0).X;
        //        double num3 = segment.GetVertex(0).Y;
        //        double num4 = segment.GetVertex(1).X;
        //        double num5 = segment.GetVertex(1).Y;
        //    }


        //    //foreach (Triangle tr in mesh.Triangles)
        //    //{

        //    //    Vertex vertex = tr.GetVertex(0);
        //    //    Vertex vertex2 = tr.GetVertex(1);
        //    //    Vertex vertex3 = tr.GetVertex(2);

        //    //    var a = tr.Area;
        //    //}

        //    //Edge a = mesh.Edges[1];


        //    int maxX = 0;
        //    int maxY = 0;

        //    foreach (TriangleNet.Geometry.Edge edge in mesh.Edges)
        //    {
        //        int tmpX = edge.P0;
        //        int tmpY = edge.P1;

        //        if (maxX < tmpX)
        //        {
        //            maxX = tmpX;
        //        }
        //        if (maxY < tmpY)
        //        {
        //            maxY = tmpY;
        //        }
        //    }



        //    Random r = new Random();

        //    // полигоны сетки сечения
        //    int numSeries = 3;
        //    foreach (Triangle tr in mesh.Triangles)
        //    {
        //        //chrtBSMesh.Series.Add($"Series{numSeries}");
        //        //chrtBSMesh.Series[$"Series{numSeries}"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Range;
        //        ////chrtBSMesh.Series[$"Series{numSeries}"].BorderColor = System.Drawing.Color.Black;
        //        //chrtBSMesh.Series[$"Series{numSeries}"].Color = System.Drawing.Color.Red;






        //        ScottPlot.Coordinates[] pointss = new ScottPlot.Coordinates[3];
        //        //chrtBSMesh.Series[$"Series{numSeries}"].Points.
        //        for (int i = 0; i < 3; i++)
        //        {
        //            //chrtBSMesh.Series[$"Series{numSeries}"].Points.AddXY(tr.GetVertex(i).X, tr.GetVertex(i).Y);
        //            pointss[i] = new ScottPlot.Coordinates(tr.GetVertex(i).X, tr.GetVertex(i).Y);
        //        }
        //        //var tmpPoly = FormsPlot1.Plot.Add.Polygon(pointss);

        //        //ScottPlot.Plottables.Polygon tmpPolygon = FormsPlot1.Plot.Add.Polygon(pointss);
        //        //ScottPlot.Plottables.Polygon tmpPolygon = new ScottPlot.Plottables.Polygon(pointss);

        //        ScottPlot.Plottables.Polygon tmpPolygon = FormsPlot1.Plot.Add.Polygon(pointss);


        //        //tmpPolygon.FillColor = ScottPlot.Colors.Blue;
        //        //tmpPolygon.LineColor = ScottPlot.Colors.White;


        //        if (r.Next(0, 10) > 5)
        //        {
        //            tmpPolygon.FillColor = ScottPlot.Colors.Red;
        //        }
        //        else
        //        {
        //            tmpPolygon.FillColor = ScottPlot.Colors.Blue;
        //        }


        //        //tmpPolygon.FillColor = ScottPlot.Colors.Blue;
        //        //tmpPolygon.LineColor = ScottPlot.Colors.Blue;


        //        numSeries++;
        //    }



        //    //var a = plot1.PlottableList;

        //    //foreach (var b in a)
        //    //{
        //    //    string strB = b.ToString();
        //    //    ScottPlot.Plottables.Polygon tmpPolygon1 = (ScottPlot.Plottables.Polygon)b;
        //    //    tmpPolygon1.FillColor = ScottPlot.Colors.Red;
        //    //    //Random r = new Random();
        //    //    //if (r.Next(0, 10) > 5)
        //    //    //{
        //    //    //    tmpPolygon1.FillColor = ScottPlot.Colors.Red;
        //    //    //}
        //    //    //else
        //    //    //{
        //    //    //    tmpPolygon1.FillColor = ScottPlot.Colors.Blue;
        //    //    //}

        //    //}







        //    //Triangle tr1 = mesh.Triangles.ToArray()[1];

        //    //Vertex vertex1 = tr1.GetVertex(0);
        //    //Vertex vertex2 = tr1.GetVertex(1);
        //    //Vertex vertex3 = tr1.GetVertex(2);
        //    //var area = tr1.Area;

        //    //double[] arrayX = new double[] { (double)vertex1.X, (double)vertex2.X, (double)vertex3.X };
        //    //double[] arrayY = new double[] { (double)vertex1.Y, (double)vertex2.Y, (double)vertex3.Y };
        //    ////DataPoint dPoint = new DataPoint(vertex1.X, arrayY);















        //    //ScottPlot.Coordinates[] points =
        //    //{
        //    //    new ScottPlot.Coordinates(0,   0.25),
        //    //    new ScottPlot.Coordinates(0.3, 0.75),
        //    //    new ScottPlot.Coordinates(1,   1),
        //    //    new ScottPlot.Coordinates(0.7, 0.5),
        //    //    new ScottPlot.Coordinates(1,   0)
        //    //};

        //    //FormsPlot1.Plot.Add.Polygon(points);





        //    //FormsPlot1.Plot.SavePng("demo.png", 400, 300);





        //    //chrtBSMesh.Series["Series3"].Points.Add()

        //    //foreach (Triangle tr in mesh.Triangles)
        //    //{

        //    //    Vertex vertex1 = tr.GetVertex(0);
        //    //    Vertex vertex2 = tr.GetVertex(1);
        //    //    Vertex vertex3 = tr.GetVertex(2);
        //    //    //var a = tr.Area;

        //    //    double[] arrayX = new double[] { vertex1.X, vertex3.X, vertex3.X };
        //    //    double[] arrayY = new double[] { vertex1.Y, vertex3.Y, vertex3.Y };
        //    //    chrtBSMesh.Series["Series3"].Points.AddXY(arrayX, arrayY);

        //    //}

        //    // ребра сетки сечения
        //    //chrtBSMesh.Series.Add("Series4");
        //    //chrtBSMesh.Series["Series4"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;



        //    //var ef = pctbBSMesh.CreateGraphics();




        //    }

        private void TestPolygonOnPictureBox()
        {
            //pctbBSMesh.CreateGraphics().Clear(Color.White);


            //Graphics testGraph = pctbBSMesh.CreateGraphics();
            //testGraph.SmoothingMode = SmoothingMode.AntiAlias;
            //testGraph.Clear(Color.White);

            //// draw the shading background:
            //List<System.Drawing.Point> shadePoints = new List<System.Drawing.Point>();
            //shadePoints.Add(new System.Drawing.Point(0, pctbBSMesh.ClientSize.Height));
            //shadePoints.Add(new System.Drawing.Point(pctbBSMesh.ClientSize.Width, 0));
            //shadePoints.Add(new System.Drawing.Point(pctbBSMesh.ClientSize.Width,
            //                          pctbBSMesh.ClientSize.Height));
            //testGraph.FillPolygon(Brushes.LightGray, shadePoints.ToArray());

            //// scale the drawing larger:
            //using (Matrix m = new Matrix())
            //{
            //    m.Scale(4, 4);
            //    testGraph.Transform = m;

            //    List<System.Drawing.Point> polyPoints = new List<System.Drawing.Point>();
            //    polyPoints.Add(new System.Drawing.Point(10, 10));
            //    polyPoints.Add(new System.Drawing.Point(12, 35));
            //    polyPoints.Add(new System.Drawing.Point(22, 35));
            //    polyPoints.Add(new System.Drawing.Point(24, 22));

            //    //// use a semi-transparent background brush:
            //    //using (SolidBrush br = new SolidBrush(Color.FromArgb(100, Color.Yellow)))
            //    //{
            //    //    testGraph.FillPolygon(br, polyPoints.ToArray());
            //    //}
            //    //testGraph.DrawPolygon(Pens.DarkBlue, polyPoints.ToArray());
            //    //foreach (System.Drawing.Point p in polyPoints)
            //    //{
            //    //    testGraph.FillEllipse(Brushes.Red,
            //    //                           new System.Drawing.Rectangle(p.X - 2, p.Y - 2, 4, 4));
            //    //}
            //}


            // Create pen.
            Pen blackPen = new Pen(Color.Black, 3);

            // Create points that define polygon.
            System.Drawing.Point point1 = new System.Drawing.Point(50, 50);
            System.Drawing.Point point2 = new System.Drawing.Point(100, 25);
            System.Drawing.Point point3 = new System.Drawing.Point(200, 5);
            System.Drawing.Point point4 = new System.Drawing.Point(250, 50);
            System.Drawing.Point point5 = new System.Drawing.Point(300, 100);
            System.Drawing.Point point6 = new System.Drawing.Point(350, 200);
            System.Drawing.Point point7 = new System.Drawing.Point(250, 250);
            System.Drawing.Point[] curvePoints =
                     {
                 point1,
                 point2,
                 point3,
                 point4,
                 point5,
                 point6,
                 point7
             };

            // Draw polygon to screen.
            //Graphics testG = new Graphics();
            //e.Graphics.DrawPolygon(blackPen, curvePoints);
        }

        private void BeamSectionMesh_Load(object sender, EventArgs e)
        {
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // Create pen.
            Pen blackPen = new Pen(Color.Black, 3);

            // Create points that define polygon.
            System.Drawing.Point point1 = new System.Drawing.Point(50, 50);
            System.Drawing.Point point2 = new System.Drawing.Point(100, 25);
            System.Drawing.Point point3 = new System.Drawing.Point(200, 5);
            System.Drawing.Point point4 = new System.Drawing.Point(250, 50);
            System.Drawing.Point point5 = new System.Drawing.Point(300, 100);
            System.Drawing.Point point6 = new System.Drawing.Point(350, 200);
            System.Drawing.Point point7 = new System.Drawing.Point(250, 250);
            System.Drawing.Point[] curvePoints =
                     {
                 point1,
                 point2,
                 point3,
                 point4,
                 point5,
                 point6,
                 point7
             };

            // Draw polygon to screen.
            e.Graphics.DrawPolygon(blackPen, curvePoints);
        }
    }
}

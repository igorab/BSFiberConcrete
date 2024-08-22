using BSFiberConcrete.Section.DrawBeamSection;
using ScottPlot;
using ScottPlot.WinForms;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TriangleNet;
using TriangleNet.Topology;

namespace BSFiberConcrete
{
    /// <summary>
    /// Класс для отрисовки элементов связанных с сеткой сечения балки.
    /// Для отрисовки используется NuGet ScottPlot
    /// </summary>
    public class MeshDraw
    {        
        private FormsPlot _formsPlot;

        // шаг сетки
        private int Ny; // горизонтальная ось
        private int Nz; // вертикальная ось

        /// верхняя граница
        public double MaxVal {private get; set; }
        /// нижняя граница
        public double MinVal {private get; set; }

        public List<double> Values { private get; set; }
        /// <summary>
        /// Значения для стержней арматуры
        /// </summary>
        public List<double> ValuesS { private get; set; }

        /// <summary>
        /// Сетки из треугольников
        /// </summary>
        public Mesh TriangleMesh 
        { 
            get;
            private set;
        }

        public MeshDraw(Mesh triangleMesh)
        {
            TriangleMesh = triangleMesh;
        }

        /// <summary>
        /// Отрисовка объекта FormsPlot на WinForm'е
        /// </summary>
        public void ShowMesh()
        {
            if (_formsPlot == null)
                return;
            DrawBeamSection drawBS = new DrawBeamSection();
            drawBS.PlotForForms = _formsPlot;
            drawBS.MinValue = MinVal;
            drawBS.MaxValue = MaxVal;
            drawBS.e_fbt_max = Values.Max();
            drawBS.e_fb_max = Values.Min();

            drawBS.Show();                
        }

        /// <summary>
        /// сохранение объекта FormsPlot на картинке
        /// </summary>
        public void SaveToPNG(string fullPath = null)
        {
            if (_formsPlot == null)
                return;
            if (fullPath == null)
                _formsPlot.Plot.SavePng("beamSectionMesh.png",600,600);
            else 
                _formsPlot.Plot.SavePng(fullPath, 600, 600);
        }


        /// <summary>
        /// Формирование объекта FormsPlot с сеткой из TriangleMesh
        /// </summary>
        public FormsPlot CreatePlot()
        {
            FormsPlot formsPlt = new FormsPlot() { Dock = DockStyle.Fill };
            formsPlt.Plot.Axes.SquareUnits(); // 
            Random r = new Random();
            foreach (Triangle tr in TriangleMesh.Triangles)
            {
                int randValue = r.Next(0, 10);

                ScottPlot.Coordinates[] points = new ScottPlot.Coordinates[3];
                for (int i = 0; i < points.Length; i++)
                {
                    points[i] = new ScottPlot.Coordinates(tr.GetVertex(i).X, tr.GetVertex(i).Y);
                }
                ScottPlot.Plottables.Polygon tmpPolygon = formsPlt.Plot.Add.Polygon(points);

                //tmpPolygon.LineColor = ScottPlot.Colors.White;
                //if (randValue > 7)
                //    tmpPolygon.FillColor = ScottPlot.Colors.Red;
                //else if (randValue < 4)
                //    tmpPolygon.FillColor = ScottPlot.Colors.Blue;
                //else
                //    tmpPolygon.FillColor = ScottPlot.Colors.Green;
            }
            _formsPlot = formsPlt;
            return formsPlt;
        }

        /// <summary>
        /// Треугольные полигоны закрвашиваются цветом в соответсии с maxTension и minTension
        /// </summary>
        /// <param name="_tension"> Значения напряжений для треугольников в соответсвии с TriangleMesh.Triangles</param>
        /// <param name="_maxTension">Предельное значение напряжения</param>
        /// <param name="_minTension">Предельное значение напряжения</param>
        /// <returns></returns>
        public FormsPlot PaintSectionMesh()
        {
            FormsPlot formsPlt = new FormsPlot() { Dock = DockStyle.Fill };

            for ( int i = 0; i < TriangleMesh.Triangles.Count; i++)
            {
                // отрисовка гемеотри треугольника
                Triangle tr = TriangleMesh.Triangles.ToArray()[i];

                ScottPlot.Coordinates[] points = new ScottPlot.Coordinates[3];

                for (int j = 0; j < points.Length; j++)
                {
                    points[j] = new ScottPlot.Coordinates(tr.GetVertex(j).X, tr.GetVertex(j).Y);
                }
                ScottPlot.Plottables.Polygon poly = formsPlt.Plot.Add.Polygon(points);

                if (Values != null )
                {
                    double measured_value = Values[i];

                    if (measured_value > MinVal && measured_value < MaxVal)
                        poly.FillColor = ScottPlot.Colors.Green;
                    else if (measured_value >= MaxVal)
                        poly.FillColor = ScottPlot.Colors.Red;
                    else if (measured_value <= MinVal)
                        poly.FillColor = ScottPlot.Colors.Violet;
                    else
                        poly.FillColor = ScottPlot.Colors.Green;
                }
            }

            formsPlt.Plot.Axes.SquareUnits();  

            _formsPlot = formsPlt;

            return formsPlt;
        }

        
        public MeshDraw(int _Ny, int _Nz)
        {
            Ny = _Ny;
            Nz = _Nz;
        }

        /// <summary>
        ///  Покрытие прямоугольниками
        /// </summary>
        /// <param name="sz"></param>
        /// <param name="_bs"></param>
        /// <returns></returns>
        public FormsPlot CreateRectanglePlot(double[] sz, BeamSection _bs)
        {            
            var msh = new BSCalcLib.MeshRect(Ny, Nz);

            if (_bs == BeamSection.Rect)
                msh.Rectangle(sz[0], sz[1]);
            else if (BSHelper.IsITL(_bs))
                msh.IBeamSection(sz[0], sz[1], sz[2], sz[3], sz[4], sz[5]);

            FormsPlot formsPlot = new FormsPlot() { Dock = DockStyle.Fill };
            formsPlot.Plot.Axes.SquareUnits();

            int idx = 0;
            int cnt = msh.rectangleFs.Count;
            foreach (RectangleF tr in msh.rectangleFs)
            {
                ScottPlot.Coordinates[] points = new ScottPlot.Coordinates[] 
                { 
                    new ScottPlot.Coordinates(tr.Left, tr.Bottom),
                    new ScottPlot.Coordinates(tr.Right, tr.Bottom),
                    new ScottPlot.Coordinates(tr.Right, tr.Top),
                    new ScottPlot.Coordinates(tr.Left, tr.Top)
                };
                                                        
                ScottPlot.Plottables.Polygon poly = formsPlot.Plot.Add.Polygon(points);

                if (Values != null && Values.Count == cnt)
                {                                        
                    double measured_value = Values[idx];

                    if (measured_value > MinVal && measured_value < MaxVal)
                        poly.FillColor = ScottPlot.Colors.Green;
                    else if (measured_value >= MaxVal)
                        poly.FillColor = ScottPlot.Colors.Red;
                    else if (measured_value <= MinVal)
                        poly.FillColor = ScottPlot.Colors.Violet;
                    else
                        poly.FillColor = ScottPlot.Colors.Green;
                }

                idx++;
            }

            _formsPlot = formsPlot;

            return formsPlot;
        }
    }
}

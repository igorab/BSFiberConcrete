using BSFiberConcrete.Section.DrawBeamSection;
using ScottPlot;
using ScottPlot.WinForms;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        /// <summary>
        /// 
        /// </summary>
        private FormsPlot _formsPlot;

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
            else _formsPlot.Plot.SavePng(fullPath, 600, 600);
        }


        /// <summary>
        /// Формирование объекта FormsPlot с сеткой из TriangleMesh
        /// </summary>
        public FormsPlot CreatePLot()
        {
            FormsPlot formsPlt = new FormsPlot() { Dock = DockStyle.Fill };
            formsPlt.Plot.Axes.SquareUnits(); // 
            Random r = new Random();
            foreach (Triangle tr in TriangleMesh.Triangles)
            {
                int randValue = r.Next(0, 10);

                ScottPlot.Coordinates[] pointss = new ScottPlot.Coordinates[3];
                for (int i = 0; i < 3; i++)
                {
                    pointss[i] = new ScottPlot.Coordinates(tr.GetVertex(i).X, tr.GetVertex(i).Y);
                }
                ScottPlot.Plottables.Polygon tmpPolygon = formsPlt.Plot.Add.Polygon(pointss);

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
        /// <param name="tension"> Значения напряжений для треугольников в соответсвии с TriangleMesh.Triangles</param>
        /// <param name="maxTension">Предельное значение напряжения</param>
        /// <param name="minTension">Предельное значение напряжения</param>
        /// <returns></returns>
        public FormsPlot PaintSectionMesh(List<double> tension, double maxTension, double minTension)
        {
            FormsPlot formsPlt = new FormsPlot() { Dock = DockStyle.Fill };
            for ( int i = 0; i < TriangleMesh.Triangles.Count; i++)
            {
                // отрисовка гемеотри треугольника
                Triangle tr = TriangleMesh.Triangles.ToArray()[i];
                ScottPlot.Coordinates[] pointss = new ScottPlot.Coordinates[3];
                for (int j = 0; j < 3; j++)
                {
                    pointss[j] = new ScottPlot.Coordinates(tr.GetVertex(j).X, tr.GetVertex(j).Y);
                }
                ScottPlot.Plottables.Polygon tmpPolygon = formsPlt.Plot.Add.Polygon(pointss);

                // определение цвета треугольник
                double tmpTension = tension[i]; 
                tmpPolygon.LineColor = ScottPlot.Colors.White;
                if (maxTension >= tmpTension)
                    tmpPolygon.FillColor = ScottPlot.Colors.Red;
                else if ( minTension <= tmpTension)
                    tmpPolygon.FillColor = ScottPlot.Colors.Blue;
                else
                    tmpPolygon.FillColor = ScottPlot.Colors.Green;
            }

            formsPlt.Plot.Axes.SquareUnits(); // 
            _formsPlot = formsPlt;
            return formsPlt;
        }
    }
}

using BSFiberConcrete.Section.DrawBeamSection;
using ScottPlot;
using ScottPlot.Colormaps;
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
        internal double e_st_ult;
        internal double e_s_ult;

        public int MosaicMode { private get; set; }

        /// верхняя граница
        public double UltMax {private get; set; }
        /// нижняя граница
        public double UltMin {private get; set; }
        /// <summary>
        /// предел по арматуре
        /// </summary>
        public double Rs_Ult { private get; set; }

        /// <summary>
        /// Сечение
        /// </summary>
        public List<double> Values_B { private get; set; }
        /// <summary>
        /// Значения для стержней арматуры
        /// </summary>
        public List<double> Values_S { private get; set; }

        /// <summary>
        /// Сетки из треугольников
        /// </summary>
        public Mesh TriangleMesh  {  get; private set; }

        public MeshDraw(Mesh _triangleMesh)
        {
            TriangleMesh = _triangleMesh;
        }

        public MeshDraw(int _Ny, int _Nz)
        {
            Ny = _Ny;
            Nz = _Nz;
        }

        /// <summary>
        /// Отрисовка объекта FormsPlot на WinForm'е
        /// </summary>
        public void ShowMesh()
        {
            if (_formsPlot == null)
                return;
            DrawBeamSection drawBS = new DrawBeamSection();

            drawBS.Mode = MosaicMode;
            drawBS.PlotForForms = _formsPlot;
            drawBS.MinValue = UltMin;
            drawBS.MaxValue = UltMax;
            drawBS.Rs_Value = Rs_Ult;
            drawBS.e_st_ult = e_st_ult;
            drawBS.e_s_ult = e_s_ult;
            drawBS.e_fbt_max = (Values_B!=null)? Values_B.Max() : 0;
            drawBS.e_fb_max = (Values_B != null) ? Values_B.Min() :0;
            drawBS.e_st_max = (Values_S != null)? Values_S.Max() : 0;
            drawBS.e_s_max = (Values_S != null) ? Values_S.Min() :0;

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

                if (Values_B != null )
                {
                    double measured_value = Values_B[i];

                    if (measured_value > UltMin && measured_value < UltMax)
                        poly.FillColor = ScottPlot.Colors.Green;
                    else if (measured_value >= UltMax)
                        poly.FillColor = ScottPlot.Colors.Red;
                    else if (measured_value <= UltMin)
                        poly.FillColor = ScottPlot.Colors.Violet;
                    else
                        poly.FillColor = ScottPlot.Colors.Green;
                }
            }

            formsPlt.Plot.Axes.SquareUnits();  

            _formsPlot = formsPlt;

            return formsPlt;
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

                if (Values_B != null && Values_B.Count == cnt)
                {                                        
                    double measured_value = Values_B[idx];

                    if (measured_value > UltMin && measured_value < UltMax)
                        poly.FillColor = ScottPlot.Colors.Green;
                    else if (measured_value >= UltMax)
                        poly.FillColor = ScottPlot.Colors.Red;
                    else if (measured_value <= UltMin)
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

using BSCalcLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BSFiberConcrete.CalcGroup2
{
    public partial class BSCalcNDM
    {

        public int InitReinforcement(double _y0 = 0, double _z0 = 0 )
        {
            // количество элементов арматуры
            int m = 0;

            //заполнить  массив площадей арматуры            
            foreach (double d in ds)
            {
                As.Add(Math.PI * Math.Pow(d, 2) / 4.0);
            }

            for (int l = 0; l < ds.Count; l++)
            {
                y0s[l] += _y0;
                z0s[l] += _z0;
            }

            m = As.Count;

            return m;

        }

        /// <summary>
        /// разбить прямоугольное сечение на элементы
        /// </summary>
        /// <param name="_b">ширина</param>
        /// <param name="_h">высота</param>
        /// <param name="_y0">начало координат</param>
        /// <param name="_z0">начало координат</param>
        /// <returns></returns>
        public int InitRectangleSection(double _b, double _h, double _y0 = 0, double _z0 = 0)
        {
            // количество элементов сечения
            int n = ny * nz;           
            double sy = _b / ny;
            double sz = _h / nz;
            // площадь 1 элемента
            double Ab1 = sy * sz;

            //заполнить массив площадей элементов            
            for (int i = 0; i < n; i++)
                Ab.Add(Ab1);

            //заполнить массив привязок бетонных эл-в к вспомогательной оси y0            
            for (int iz = 0; iz < nz; iz++)
                for (int iy = 0; iy < ny; iy++)
                    y0b.Add( iy * sy + sy / 2.0 + _y0);

            //заполнить массив привязок бетонных эл-в к вспомогательной оси z0            
            for (int iz = 0; iz < nz; iz++)
                for (int iy = 0; iy < ny; iy++)
                    z0b.Add( iz * sz + sz / 2.0 + _z0);
           
            return n;
        }


        // двутавровое сечение
        public int InitIBeamSection(double _bf, double _hf, double _bw, double _hw, double _b1f, double _h1f)
        {
            int n1 = 0, n2 = 0, n3 = 0;

            if (_bf>0 && _hf>0)
                n1 = InitRectangleSection(_bf, _hf, -_bf/2.0, 0);

            n2 = InitRectangleSection(_bw, _hw, -_bw / 2.0, hf);

            if (_b1f > 0 && _h1f > 0)
                n3 = InitRectangleSection(_b1f, _h1f, -_b1f / 2.0, hf + hw);

            return n1+n2+n3;
        }

        // кольцевое сечение
        private int InitRingSection(double _r1, double _R2)
        {
            int n = 0;            
            if (r1 >= R2) throw BSBeam_Ring.RadiiError();

            BSMesh.Center = new TriangleNet.Geometry.Point(0, 0);
            _ = BSMesh.GenerateRing(_R2, _r1, false);

            Tri.Mesh = BSMesh.Mesh;
            List<object> Tr = Tri.CalculationScheme();
           
            var triAreas = Tri.triAreas; // площади треугольников
            var triCGs = Tri.triCGs; // ц.т. треугольников

            n = triAreas.Count;

            //заполнить массив площадей элементов            
            foreach (var _area in triAreas)
                Ab.Add(_area);

            //заполнить массив привязок бетонных эл-в к вспомогательной оси y0            
            //заполнить массив привязок бетонных эл-в к вспомогательной оси z0            
            foreach (var triCG in triCGs)
            {
                y0b.Add(triCG.X);
                z0b.Add(triCG.Y);
            }
                                           
            return n;
        }

    }
}

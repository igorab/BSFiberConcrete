using MathNet.Numerics;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

// Рачеты по второй группе предельных состояний
namespace BSFiberConcrete.CalcGroup2
{
    public class BSCalcNDM 
    {
        void Init()
        {
            double b = 20.0;
            double h = 40.0;

            int ny = 4;
            int nz = 4;

            int n = ny * nz;

            double sy = b / ny;
            double sz = h / nz;

            // площадь 1 элемента
            double Ab1 = sy * sz;
            List<double> Ab = new List<double>();

            for (int i=0; i<n; i++)
                Ab.Add(Ab1);

            // массив привязок бетонных эл-в к вспомогательной оси y0
            List<double> y0b = new List<double>();
            for (int iz = 0; iz < nz; iz++)
                for (int iy = 0; iy < ny; ny++)
                    y0b.Add(iy * sy + sy / 2.0);

            // массив привязок бетонных эл-в к вспомогательной оси z0
            List<double> z0b = new List<double>();
            for (int iz = 0; iz < nz; iz++)
                for (int iy = 0; iy < ny; ny++)
                    z0b.Add(iz * sz + sz / 2.0);

            List<double> y0s = new List<double>();
            List<double> z0s = new List<double>();

            // диаметры арматурных стержней
            List<double> ds = new List<double>() {16.0, 16.0, 16.0, 16.0};
            List<double> As = new List<double>();

            foreach (double d in ds)
            {
                As.Add(Math.PI * Math.Pow(d/10, 2) / 4.0);
            }

            int m = As.Count;

            // Бетон B25
            double Eb0 = 30 * Math.Pow(10, 3)/ 10.0;

            // Арматура
            double Es0 = 2 * Math.Pow(10, 5) / 10.0;

            List<double> Eb = new List<double>();
            List<double> Es = new List<double>();
            List<double> Ebs = new List<double>();

            for (int i = 0; i < n; n++)
                Eb.Add(Eb0);

            for (int i = 0; i < As.Count; n++)
            {
                Es.Add(Eb0);
                Ebs.Add(Eb0);
            }

            // Массив привязок центра тяжести
            List<double> ycm = new List<double>();
            List<double> zcm = new List<double>();

            // Вычисляем положение начального (геометрического) центра тяжести
            IEnumerable<double> numcy = Ab.Zip(y0b, (A, y)=> A * y);
            IEnumerable<double> numcz = Ab.Zip(z0b, (A, z) => A * z);
            double denomc = Ab.Sum(A => A);

            double cy = 0;
            foreach (double x in numcy) 
                cy += x/denomc;
            ycm.Add(cy);

            double cz = 0;
            foreach (double x in numcz)
                cz += x / denomc;
            zcm.Add(cz);

            List<double> yb = new List<double>();
            List<double> zb = new List<double>();
            List<double> ys = new List<double>();
            List<double> zs = new List<double>();

            for (int k =0; k < n; k++) 
            {
                yb.Add(y0b[k] - ycm[0]);
                zb.Add(z0b[k] - zcm[0]);
            }

            for (int l = 0; l < m; l++)
            {
                ys.Add(y0s[l] - ycm[0]);
                zs.Add(z0s[l] - zcm[0]);
            }
        }
        


    }
}

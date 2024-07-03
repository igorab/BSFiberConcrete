using CBAnsDes.My;
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
    public class BSCalcNDM_Fiber 
    {
        private double Rbt_ser = 0;
        // Изгибающий момент, воспр нормальным сечением элемента при образовании трещин
        private double Mcrс = 0;
        // упругопластический момент сопротивления сечения для крайнего растчянутого волокна
        private double Wpl = 0;

        // изгиб момент от внешней нагрузки
        public double M = 0;

        public double N = 0;
        public double e_x = 0;

        public BSCalcNDM_Fiber()
        {

        }
            
        /// <summary>
        /// Условие
        /// </summary>
        /// <returns>Выполнено?</returns>
        public bool Condition()
        {
            return M > Mcrс;
        }

        public void Calculate()
        {
            double Wred = 0; 

            Wpl = Wred; //6.2.9


            // 6.107
            Mcrс = Rbt_ser * Wpl + N * e_x;
        }
        
        //6.2.13
        private void CalcMcr()
        {

        }

        /// <summary>
        /// Расчет кривизны 
        /// п 6.2.31
        /// </summary>
        public void CalcCurvature() 
        {
            double r, r1 = float.MaxValue, r2 = float.MaxValue;
            double k;

            k = 1 / r1 + 1 / r2;


        }

    }


    public class BSCalcNDM 
    {
        // Продольная сила, кН, - сжатие
        double N = -400.0;
        // Момент отн. оси Y, кН*см
        double My0 = 100.0 * 100;
        // Момент отн. оси Z, кН*см
        double Mz0 = 10.0 * 100;
        // Ширина сечения, см
        double b = 20.0;
        // высота сечения, см
        double h = 40.0;
        // число элементов вдоль y, шт
        int ny = 4;

        // число элементов вдоль z шт.
        int nz = 4;

        private List<double> My, Mz;

        void Init()
        {            
            int n = ny * nz;

            double sy = b / ny;
            double sz = h / nz;

            My = new List<double> { My0 };
            Mz = new List<double> { Mz0 };

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

            // привязки арматуры
            List<double> y0s = new List<double>() {5.0, 15.0, 5.0, 15.0 };
            List<double> z0s = new List<double>() {5.0, 5.0, 35.0, 35.0 }; 

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

            /// Создаем массивы упруго-геометрических характеристик

            // осевая геометрическая жесткость
            List<double> Dxx = new List<double>();

            // упругогеометрический осевой момент отн оси Y
            List<double> Dyy = new List<double>();

            // упругогеометрический осевой момент отн оси Z
            List<double> Dzz = new List<double>();

            // упругогеометрический центробежный момент
            List<double> Dyz = new List<double>();

            // Вычисляем упруго-геометрические характеристики на нулевой итерации
            double dxx = 0;
            foreach (double _A in  Ab)            
                dxx += _A * Eb[0];
            foreach (double _A in As)
                dxx += _A * Eb[0];
            dxx -= As.Sum(_A => _A * Ebs[0]);
            Dxx.Add(dxx);

            double dyy = 0;

            Dyy.Add(dyy);


            double dzz = 0;

            Dyy.Add(dzz);


            double dyz = 0;

            Dyy.Add(dyz);

            // Создаем массивы параметров деформаций
            List<double> ep0 = new List<double>();
            List<double> Ky = new List<double>();
            List<double> Kz = new List<double>();
                        
            
            //Вычисляем параметры деформаций на нулевой итерации
            ep0.Add(N / Dxx[0]);
            double denomK = Dyy[0] * Dzz[0] - Math.Pow(Dyz[0], 2) ;

            Ky.Add( (Mz[0]* Dyy[0] + My[0] * Dyz[0]) / denomK );
            Kz.Add( - (My[0] * Dzz[0] + Mz[0] * Dyz[0]) / denomK);

            //создаем массивы для деформаций бетона и арматуры   
            List<double> epB = new List<double>();
            List<double> epS = new List<double>();

            //Вычисляем деформации на нулевой итерации   

        }

    }
}

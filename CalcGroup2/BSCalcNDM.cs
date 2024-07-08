using BSFiberConcrete.BSRFib;
using CBAnsDes.My;
using MathNet.Numerics;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

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

        // Параметры материалов
        // Бетон B25
        static double Eb0 = 30 * Math.Pow(10, 3) / 10.0;
        // Арматура
        static double Es0 = 2 * Math.Pow(10, 5) / 10.0;
        //  Прочность арматуры на растяжение
        static double  Rst = 435 / 10d;
        //  Прочность арматуры на сжатие
        static double Rsc = 400 / 10d;

        static double est2 = 0.025;
        static double esc2 = 0.025;
        static double esc0 = Rsc / Es0;
        static double est0 = Rst / Es0;

        // Диаграмма деформирования арматуры (двухлинейная)
        double dia_S(double _e)
        {
            double s = 0;

            if (_e > est2 || _e < -esc2)
                s = 0;
            else if (est0 <= _e && _e <= est2)
                s = Rst;
            else if (-esc2 <= _e && _e <= -esc0)
                s = -Rst;
            else if (0 <= _e && _e <= est0 )                
                s = Math.Min(_e * Es0, Rst);
            else if (-esc0 <= _e && _e <= 0)
                s = Math.Min(_e * Es0, -Rsc);

            return s;
        }

        double Rbc = 14.5 / 10d; // Расчетное сопротивление бетона на сжатие, кН/см2
        double Rbt = 1.05 / 10d; // Расчетное сопротивление бетона на сжатие, кН/см2
        double ebc2 = 0.0035; // Предельная деформация бетона на сжатие
        double ebt2 = 0.00015; // Предельная деформация бетона на растяжение
        double ebc0 = 0.002; // Деформация бетона на сжатие
        double ebt0 = 0.0001; // Деформация бетона на растяжение

        // Диаграмма деформирования бетона (трехлинейная)
        double dia_B(double _e)
        {
            double s = 0;
            double sc1 = 0.6 * Rbc;
            double st1 = 0.6 * Rbt;
            double ebc1 = sc1 /Eb0 ;
            double ebt1 = st1 /Eb0;

            if (_e > ebt2 || _e < -ebc2)
                s = 0;            
            else if (-ebc2 <= _e && _e <= - ebc0)
                s = -Rbc;
            else if (ebt0 <= _e && _e <= ebt2)
                s = Rbt;
            else if (-ebc0 <= _e && _e <= -ebc1)
                s = -Rbc * ((1 - sc1/Rbc) * (Math.Abs(_e) - ebc1) / (ebc0 - ebc1) + sc1/Rbc);
            else if (-ebt1 <= _e && _e <= -ebt0)
                s = Rbt * ((1 - st1/Rbt) * (Math.Abs(_e)-ebt1)/(ebc0 - ebc1) + st1/Rbt);
            if (-ebc1 <= _e && _e <= ebt1)
                s = _e * Eb0;

            return s;
        }

        // секущий модуль
        private double E_sec(double _s, double _e, double _E0 )
        {
            if (_e == 0)
                return _E0;
            else
                return _s / _e;
        }


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
            for (int k=0; k < n; k ++)
                epB.Add(ep0[0] + yb[k] * Ky[0] + zb[k] * Kz[0]);

            for (int l = 0; l < m; l++)
                epS.Add(ep0[0] + ys[l] * Kz[0] + zs[l] * Kz[0] );

            // Создаем массивы для напряжений
            List<double> sigB = new List<double>();
            List<double> sigS = new List<double>();
            List<double> sigBS = new List<double>();

            // Заполняем напряжения на нулевой итерации
            for (int k = 0; k < n; k++)
                sigB.Add( dia_B( epB[k] ));

            for (int l = 0; l < m; l++)
                sigS.Add( dia_S( epS[l] ));

            for (int l = 0; l < m; l++)
                sigBS.Add( dia_B( epS[l] ));

            // максимальное число итераций
            int jmax = 1000;
            double tolmax = 10E-10;
            int err = 0;

            for (int j=1; j<jmax+1; j ++)
            {
                // пересчитываем секущие модули
                for (int k = 0; k < n; k++)
                    Eb.Add(E_sec(sigB[k], epB[k], Eb0)); // sigB[j-1, k], epB[j-1, k]

                for (int l = 0; l < m; l++)
                    Es.Add(E_sec(sigS[l], epS[l], Es0)); // sigB[j-1, k], epB[j-1, k]

                for (int l = 0; l < m; l++)
                    Ebs.Add(E_sec(sigBS[l], epS[l], Eb0)); // sigB[j-1, k], epB[j-1, k]

                var d1 = Eb.Zip(Ab, (E, A) => E * A).Sum();
                var d2 = Es.Zip(As, (E, A) => E * A).Sum();
                var d3 = Ebs.Zip(As, (E, A) => E * A).Sum();

                // пересчитываем упруго-геометрические характеристики
                Dxx.Add(d1 - d2 + d3);
                if (Dxx[j] == 0)
                {
                    err = 1;
                    break;
                }

                // пересчитываем положение упруго-геометрического положения ц.т.
                //Eb.Zip(Ab, y0b, (x, y, z)=> x*y*z);

                // пересчитываем привязки бетона и арматуры к центру тяжести    
                yb.Add(1);
                zb.Add(1);

                ys.Add(1);
                zs.Add(1);
                // пересчитываем жесткости
                Dyy.Add(1);

                Dzz.Add(1);

                Dyz.Add(1);

                denomK = Dyy[j] * Dzz[j] - Math.Pow(Dyz[j], 2) ;
                if (denomK == 0)
                {
                    err = 1;
                    break;
                }
                // Пересчитываем моменты
                My.Add(My[0] + N * zcm[j] - zcm[0]);
                Mz.Add(Mz[0] + N * ycm[j] - ycm[0]);
                // Пересчитываем параметры деформаций
                ep0.Add(N / Dxx[j]);
                Ky.Add(1/denomK);
                Kz.Add(1/denomK);
                // Пересчитываем деформации
                epB.Add(ep0[j] + yb[j] * Ky[0] + zb[j] * Kz[0] );
                epS.Add(ep0[j] + ys[j] * Ky[0] + zs[j] * Kz[0]);
                // Пересчитываем напряжения
                for (int k=0; k<n; k++)
                    sigB.Add(dia_B(epB[j]));
                for (int l = 0; l < m; l++)
                    sigS.Add(dia_S(epS[l]));
                for (int l = 0; l < m; l++)
                    sigBS.Add(dia_B(epS[l]));

                // Вычисление погрешностей
                double tol_ep0 = 0;
                double tol_Ky = 0;
                double tol_Kz = 0;
                //double tol = Math.Max(tol_ep0, tol_Ky, tol_Kz);

            }
        }

    }
}

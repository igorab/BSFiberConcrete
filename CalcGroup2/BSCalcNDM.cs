using BSFiberConcrete.BSRFib;
using CBAnsDes.My;
using MathNet.Numerics;
using MathNet.Numerics.Statistics;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

// Рачеты по второй группе предельных состояний
namespace BSFiberConcrete.CalcGroup2
{       
    public partial class  BSCalcNDM 
    {
        /// <summary>
        /// группа предельных состояний
        /// </summary>
        private readonly int GroupLSD;
        
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="_groupLSD"> группа предельных состояний</param>
        public BSCalcNDM(int _groupLSD)
        {
            GroupLSD = _groupLSD;
        }

        public void DictParams(Dictionary<string, double> _D)
        {
            // enforce
            N = BSHelper.Kgs2kN(_D["N"]);
            My0 = BSHelper.Kgs2kN(_D["My"]);
            Mz0 = BSHelper.Kgs2kN(_D["Mz"]);
            //size
            b = _D["b"];
            h = _D["h"];

            bf = _D["bf"];
            hf = _D["hf"];
            bw = _D["bw"];
            hw = _D["hw"]; 
            b1f = _D["b1f"]; 
            h1f = _D["h1f"];

            r1 = _D["r1"];
            R2 = _D["R2"];
            //
            //Mesh
            ny = (int)_D["ny"];
            nz = (int)_D["nz"];
            // beton
            Eb0 = BSHelper.Kgssm2ToKNsm2(_D["Eb0"]);
            if (GroupLSD == 2)
            {
                Rbc = BSHelper.Kgssm2ToKNsm2(_D["Rbcn"]);
                Rbt = BSHelper.Kgssm2ToKNsm2(_D["Rbtn"]);
            }
            else
            {
                Rbc = BSHelper.Kgssm2ToKNsm2(_D["Rbc"]);
                Rbt = BSHelper.Kgssm2ToKNsm2(_D["Rbt"]);
            }
            ebc0 = _D["ebc0"];
            ebc2 = _D["ebc2"];
            ebt0 = _D["ebt0"];
            ebt2 = _D["ebt2"];
            // steel / rebar
            Es0 = BSHelper.Kgssm2ToKNsm2(_D["Es0"]);
            if (GroupLSD == 2)
            {
                Rsc = BSHelper.Kgssm2ToKNsm2(_D["Rscn"]);
                Rst = BSHelper.Kgssm2ToKNsm2(_D["Rstn"]);
            }
            else
            {
                Rsc = BSHelper.Kgssm2ToKNsm2(_D["Rsc"]);
                Rst = BSHelper.Kgssm2ToKNsm2(_D["Rst"]);
            }
            esc2 = _D["esc2"];
            est2 = _D["est2"];            
        }

        /// <summary>
        /// Привязки арматуры
        /// </summary>
        /// <param name="_bD"></param>
        /// <param name="_bX"></param>
        /// <param name="_bY"></param>
        public void GetRods(List<double> _bD, List<double> _bX, List<double> _bY )
        {
            ds.Clear();
            y0s.Clear();
            z0s.Clear();
            
            int idx = 0;
            foreach (var d in _bD)
            {
                ds.Add(d);
                z0s.Add(_bX[idx]);
                y0s.Add(_bY[idx]);
                idx++;
            }
        }


        /// <summary>
        /// стержни арматуры       
        /// </summary>        
        public void GetRods(List<BSRod> _Rods, double _dx = 0, double _dy = 0)
        {
            ds.Clear();
            y0s.Clear();
            z0s.Clear();

            // вычисляем привязки арматуры ( 0 - левый нижний угол)
            foreach (var rod in _Rods)
            {
                ds.Add(rod.D);
                z0s.Add(rod.CG_X + _dx);
                y0s.Add(rod.CG_Y + _dy);                
            }
        }

        public BeamSection BeamSection  {
            set {
                m_BeamSection = value;
            }
        }
        private BeamSection m_BeamSection = BeamSection.Rect;

        #region Поля, свойства  - данные для расчета
        // Продольная сила, кН, - сжатие
        private double N = -400.0;
        // Момент отн. оси Y, кН*см
        private double My0 = 9000;
        // Момент отн. оси Z, кН*см
        private double Mz0 = 1000;
        // Ширина сечения, см
        private double b = 20.0;
        // высота сечения, см
        private double h = 40.0;
        // тавр-двутавр
        private double bf, hf, bw, hw, b1f, h1f;
        // кольцо
        private double r1, R2;

        // число элементов вдоль y, шт
        private int ny = 4;
        // число элементов вдоль z шт.
        private int nz = 4;

        // диаметры арматурных стержней
        private List<double> ds = new List<double>() { 1.6, 1.6, 1.6, 1.6 };
        // привязки арматуры
        private List<double> y0s = new List<double>() { 5.0, 15.0, 5.0, 15.0 };
        private List<double> z0s = new List<double>() { 5.0, 5.0, 35.0, 35.0 };

        // Параметры материалов
        // Бетон B25 кН/см2
        private static double Eb0 = 30.0 * Math.Pow(10, 3) / 10.0; //Начальный модуль бетона, кН/см2
        private static double Rbc = 14.5 / 10d; // Расчетное сопротивление бетона на сжатие, кН/см2
        private static double Rbt = 1.05 / 10d; // Расчетное сопротивление бетона на сжатие, кН/см2
        private static double ebc0 = 0.002; // Деформация бетона на сжатие
        private static double ebc2 = 0.0035; // Предельная деформация бетона на сжатие                
        private static double ebt0 = 0.0001; // Деформация бетона на растяжение
        private static double ebt2 = 0.00015; // Предельная деформация бетона на растяжение

        // Арматура кН/см2
        private static double Es0 = 2.0 * Math.Pow(10, 5) / 10.0; //Начальный модуль арматуры, кН/см2       
        private static double Rst = 435 / 10d; // Прочность арматуры на растяжение        
        private static double Rsc = 400 / 10d;  // Прочность арматуры на сжатие
        private static double est2 = 0.025;
        private static double esc2 = 0.025;
        private static double esc0 = Rsc / Es0;
        private static double est0 = Rst / Es0;

        private List<double> My, Mz;

        public static double Nint { get; private set; }
        public static double Myint { get; private set; }
        public static double Mzint { get; private set; }

        #endregion

        // максимальное число итераций
        private static int jmax = 1000;
        // Максимальная абсолютная погрешность
        private static double tolmax = Math.Pow(10, -10);
        private static int err = 0;
        private Dictionary<string, double> m_Results = new Dictionary<string, double>();

        public int Err => err;
        public Dictionary<string, double> Results => m_Results;


        public List<double> SigmaBResult { get; private set; }
        public List<double> SigmaSResult { get; private set; }
        public List<double> epsilonBResult { get; private set; }
        public List<double> epsilonSResult { get; private set; }


        #region разбивка сечения на элементы
        // массив привязок бетонных эл-в к вспомогательной оси y0
        private List<double> y0b = new List<double>();

        // массив привязок бетонных эл-в к вспомогательной оси z0
        private List<double> z0b = new List<double>();

        // массив площадей элементов
        private List<double> Ab = new List<double>();

        // массив площадей арматуры
        private List<double> As = new List<double>();
        #endregion

        private void InitSectionsLists()
        {
            Ab = new List<double>();
            y0b = new List<double>();
            z0b = new List<double>();
            As = new List<double>();
        }
        
        /// <summary>
        ///  рассчитать
        /// </summary>
        private void Calculate()
        {
            #region Enforces initialization
            My = new List<double> { My0 };
            Mz = new List<double> { Mz0 };
            #endregion

            #region Section initialization
            InitSectionsLists();
            int n, m;
            
            if (m_BeamSection == BeamSection.Rect)
            {                
                n = InitRectangleSection(b, h, -b / 2.0);
                m = InitReinforcement(-b / 2.0);
            }
            else if (m_BeamSection == BeamSection.IBeam)
            {
                n = InitIBeamSection(bf, hf, bw, hw, b1f, h1f);
                m = InitReinforcement(-b / 2.0);
            }
            else if (m_BeamSection == BeamSection.Ring)
            {
                n = InitRingSection(r1, R2);
                m = InitReinforcement();
            }
            else
            {
                throw new Exception($"Тип сечения {m_BeamSection} не поддерживается в данном расчете ");
            }
            
            
            #endregion


            #region Iteration 0

            //Массивы секущих модулей
            List<List<double>> Eb = new List<List<double>>() { new List<double>() } ;
            List<List<double>> Es = new List<List<double>>() { new List<double>() };
            List<List<double>> Ebs = new List<List<double>>() { new List<double>() };
            // Заполняем секущие модули для нулевой итерации
            for (int i = 0; i < n; i++)
            {
                Eb[0].Add(Eb0);
            }

            for (int i = 0; i < As.Count; i++)
            {
                Es[0].Add(Es0);
                Ebs[0].Add(Eb0);
            }

            // Массив привязок центра тяжести
            List<double> ycm = new List<double>();
            List<double> zcm = new List<double>();

            // Вычисляем положение начального (геометрического) центра тяжести
            var numcy = Ab.Zip(y0b, (A, y)=> A * y).Sum();
            var numcz = Ab.Zip(z0b, (A, z) => A * z).Sum();
            double denomc = Ab.Sum(A => A);            
            double cy = numcy / denomc;
            ycm.Add(cy);                        
            double cz = numcz / denomc;
            zcm.Add(cz);

            // массив привязок бетонных элементов к ц.т., см                
            List<List<double>> yb = new List<List<double>>() { new List<double>() };
            List<List<double>> zb = new List<List<double>>() { new List<double>() };
            // массив привязок арматурных элементов к ц.т., см                
            List <List<double>> ys = new List<List<double>>() { new List<double>() };
            List <List<double>> zs = new List<List<double>>() { new List<double>() };

            //заполняем массивы привязок относительно ц.т. на нулевой итерации    
            for (int k =0; k < n; k++) 
            {
                yb[0].Add(y0b[k] - ycm[0]);
                zb[0].Add(z0b[k] - zcm[0]);
            }

            for (int l = 0; l < m; l++)
            {
                ys[0].Add(y0s[l] - ycm[0]);
                zs[0].Add(z0s[l] - zcm[0]);
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
            double dxx0 = Eb[0].Zip(Ab, (E,A) => E*A).Sum() + 
                          Es[0].Zip(As, (E, A) => E*A).Sum() - 
                          Ebs[0].Zip(As, (E, A) => E*A).Sum();            
            Dxx.Add(dxx0);

            double dyy0 = Eb[0].ZipThree(Ab,  zb[0], (E, A, z) => E*A*z*z).Sum() +
                          Es[0].ZipThree(As,  zs[0], (E, A, z) => E*A*z*z).Sum() -
                          Ebs[0].ZipThree(As, zs[0], (E, A, z) => E*A*z*z).Sum();
            Dyy.Add(dyy0);

            double dzz0 = Eb[0].ZipThree(Ab,  yb[0], (E, A, y) => E*A*y*y).Sum() + 
                          Es[0].ZipThree(As,  ys[0], (E, A, y) => E*A*y*y).Sum() - 
                          Ebs[0].ZipThree(As, ys[0], (E, A, y) => E*A*y*y).Sum();
            Dzz.Add(dzz0);

            double dyz0 = Eb[0].ZipFour(Ab, yb[0], zb[0], (E, A, y, z) => E*A*y*z).Sum() + 
                          Es[0].ZipFour(As, ys[0], zs[0], (E, A, y, z) => E*A*y*z).Sum() - 
                          Ebs[0].ZipFour(As, ys[0], zs[0], (E, A, y, z) => E*A*y*z).Sum();
            Dyz.Add(dyz0);

            // Создаем массивы параметров деформаций
            List<double> ep0 = new List<double>();
            List<double> Ky = new List<double>();
            List<double> Kz = new List<double>();
                                    
            //Вычисляем параметры деформаций на нулевой итерации
            ep0.Add(N / Dxx[0]);
            double denomK = Dyy[0] * Dzz[0] - Math.Pow(Dyz[0], 2) ;

            Ky.Add(  (Mz[0] * Dyy[0] + My[0] * Dyz[0]) / denomK );
            Kz.Add( -(My[0] * Dzz[0] + Mz[0] * Dyz[0]) / denomK);

            //создаем массивы для деформаций бетона и арматуры   
            List<List<double>> epB = new List<List<double>>() { new List<double>() };
            List<List<double>> epS = new List<List<double>>() { new List<double>() };

            //Вычисляем деформации на нулевой итерации   
            for (int k=0; k < n; k++)
                epB[0].Add(ep0[0] + yb[0][k] * Ky[0] + zb[0][k] * Kz[0]);

            for (int l=0; l < m; l++)
                epS[0].Add(ep0[0] + ys[0][l] * Ky[0] + zs[0][l] * Kz[0]);

            // Создаем массивы для напряжений
            List <List<double>> sigB = new List<List<double>>() { new List<double>() };
            List <List<double>> sigS = new List<List<double>>() { new List<double>() };
            List <List<double>> sigBS = new List<List<double>>() { new List<double>() };

            // Заполняем напряжения на нулевой итерации
            for (int k = 0; k < n; k++)
                sigB[0].Add( Diagr_B( epB[0][k] ));

            for (int l = 0; l < m; l++)
            {
                sigS[0].Add(Diagr_S(epS[0][l]));

                sigBS[0].Add(Diagr_B(epS[0][l]));
            }
            #endregion
                      
            for (int j=1; j <= jmax; j++)
            {
                // пересчитываем секущие модули
                Eb.Add(new List<double>());
                for (int k = 0; k < n; k++)
                    Eb[j].Add(E_sec(sigB[j-1][k], epB[j-1][k], Eb0));

                Es.Add(new List<double>());
                Ebs.Add(new List<double>());
                for (int l = 0; l < m; l++)
                {
                    Es[j].Add(E_sec(sigS[j-1][l], epS[j-1][l], Es0));
                    Ebs[j].Add(E_sec(sigBS[j-1][l], epS[j-1][l], Eb0));
                }

                // пересчитываем упруго-геометрические характеристики
                double _dxx = Eb[j].Zip(Ab, (E, A) => E * A).Sum() + 
                              Es[j].Zip(As, (E, A) => E * A).Sum() - 
                              Ebs[j].Zip(As, (E, A) => E * A).Sum();                                
                Dxx.Add(_dxx);
                if (Dxx[j] == 0)
                {
                    err = 1;
                    break;
                }

                // пересчитываем положение упруго-геометрического положения ц.т.
                numcy = Eb[j].ZipThree(Ab, y0b, (E, A, y0) => E * A * y0).Sum() + 
                        Es[j].ZipThree(As, y0s, (E, A, y0) => E * A * y0).Sum() - 
                        Ebs[j].ZipThree(As, y0s, (E, A, y0) => E * A * y0).Sum() ;

                numcz = Eb[j].ZipThree(Ab, z0b, (E, A, z0) => E * A * z0).Sum() +
                        Es[j].ZipThree(As, z0s, (E, A, z0) => E * A * z0).Sum() -
                        Ebs[j].ZipThree(As, z0s, (E, A, z0) => E * A * z0).Sum();

                ycm.Add(numcy / Dxx[j]);
                zcm.Add(numcz / Dxx[j]);

                // пересчитываем привязки бетона и арматуры к центру тяжести    
                yb.Add(new List<double>());
                zb.Add(new List<double>());
                for (int k = 0; k < n; k++)
                {
                    yb[j].Add(y0b[k] - ycm[j]);
                    zb[j].Add(z0b[k] - zcm[j] );
                }

                ys.Add(new List<double>());
                zs.Add(new List<double>());
                for (int l = 0; l < m; l++)
                {
                    ys[j].Add(y0s[l] - ycm[j]);
                    zs[j].Add(z0s[l] - zcm[j]);
                }

                // пересчитываем жесткости
                double dyy = Eb[j].ZipThree( Ab, zb[j], (E, A, z) => E * A * z * z).Sum() +
                             Es[j].ZipThree( As, zs[j], (E, A, z) => E * A * z * z).Sum() -
                             Ebs[j].ZipThree(As, zs[j], (E, A, z) => E * A * z * z).Sum();
                Dyy.Add(dyy);

                double dzz = Eb[j].ZipThree( Ab, yb[j], (E, A, y) => E * A * y * y).Sum() +
                             Es[j].ZipThree( As, ys[j], (E, A, y) => E * A * y * y).Sum() -
                             Ebs[j].ZipThree(As, ys[j], (E, A, y) => E * A * y * y).Sum();
                Dzz.Add(dzz);

                double dyz = Eb[j].ZipFour( Ab, yb[j], zb[j], (E, A, y, z) => E * A * y * z).Sum() +
                             Es[j].ZipFour( As, ys[j], zs[j], (E, A, y, z) => E * A * y * z).Sum() -
                             Ebs[j].ZipFour(As, ys[j], zs[j], (E, A, y, z) => E * A * y * z).Sum();
                Dyz.Add(dyz);

                denomK = Dyy[j] * Dzz[j] - Math.Pow(Dyz[j], 2) ;
                if (denomK == 0)
                {
                    err = 1;
                    break;
                }
                // Пересчитываем моменты
                My.Add(My[0] + N * (zcm[j] - zcm[0]));
                Mz.Add(Mz[0] - N * (ycm[j] - ycm[0]));

                // Пересчитываем параметры деформаций
                ep0.Add(N / Dxx[j]);
                Ky.Add( (Mz[j]*Dyy[j] + My[j]*Dyz[j]) / denomK );
                Kz.Add(-(My[j]*Dzz[j] + Mz[j]*Dyz[j]) / denomK);

                // Пересчитываем деформации
                epB.Add(new List<double>());                
                for (int k = 0; k < n; k ++)
                    epB[j].Add(ep0[j] + yb[j][k] * Ky[j] + zb[j][k] * Kz[j]); 

                epS.Add(new List<double>());
                for (int l=0; l < m; l ++ )
                    epS[j].Add(ep0[j] + ys[j][l] * Ky[j] + zs[j][l] * Kz[j]);

                // Пересчитываем напряжения
                sigB.Add(new List<double>());
                for (int k = 0; k < n; k++)
                    sigB[j].Add(Diagr_B(epB[j][k]));

                sigS.Add(new List<double>());
                sigBS.Add(new List<double>());
                for (int l = 0; l < m; l++)
                {
                    sigS[j].Add(Diagr_S(epS[j][l]));
                    sigBS[j].Add(Diagr_B(epS[j][l]));
                }

                // Вычисление погрешностей
                double tol_ep0 = Math.Abs(ep0[j] - ep0[j-1]) ; // вычисление в серединной линии
                double tol_Ky = Math.Abs(Ky[j] - Ky[j-1]);  
                double tol_Kz = Math.Abs(Kz[j] - Kz[j-1]) ;                

                double tol = new double[] { tol_ep0, tol_Ky, tol_Kz }.Max();

                if (tol < tolmax)
                    break;

                if (j == jmax-1) 
                    err = 2;  // Достигнуто максимальное число итераций              
            }

            // Проверка - выполняются ли условия в равновестия?
            int jend = sigB.Count-1;
            Nint = sigB[jend].Zip(Ab, (s, A) => s * A).Sum() +
                   sigS[jend].Zip(As, (s, A) => s * A).Sum() -
                   sigBS[jend].Zip(As, (s, A) => s * A).Sum();

            Myint = -(sigB[jend].ZipThree(Ab, zb[0], (s, A, z) => s * A * z).Sum() +
                      sigS[jend].ZipThree(As, zs[0], (s, A, z) => s * A * z).Sum() -
                      sigBS[jend].ZipThree(As, zs[0], (s, A, z) => s * A * z).Sum());

            Mzint = sigB[jend].ZipThree(Ab, yb[0], (s, A, y) => s * A * y).Sum() +
                    sigS[jend].ZipThree(As, ys[0], (s, A, y) => s * A * y).Sum() -
                    sigBS[jend].ZipThree(As, ys[0], (s, A, y) => s * A * y).Sum();

            m_Results = new Dictionary<string, double>
            {
                ["ep0"] = ep0[jend],
                ["Ky"] = Ky[jend],
                ["ry"] = 1/Ky[jend],
                ["Kz"] = Kz[jend],
                ["rz"] = 1/Kz[jend],
                ["sigB"] = sigB[jend].MaximumAbsolute(),
                ["sigS"] = sigS[jend].MaximumAbsolute(),
                ["epsB"] = epB[jend].MaximumAbsolute(),
                ["epsS"] = epS[jend].MaximumAbsolute(),
                ["My_crc"] = Myint,
                ["Mx_crc"] = Mzint
            };

            SigmaBResult = new List<double>(sigB[jend]);
            SigmaSResult = new List<double>(sigS[jend]);
            epsilonBResult = new List<double>(epB[jend]);
            epsilonSResult = new List<double>(epS[jend]);
        }
      
        public void Run()
        {
            try
            {                
                Calculate();
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }            
            //throw new NotImplementedException();
        }

        
    }
}

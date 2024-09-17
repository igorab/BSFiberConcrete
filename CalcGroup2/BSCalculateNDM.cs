using MathNet.Numerics.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFiberConcrete.CalcGroup2
{    
    public partial class BSCalcNDM
    {
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
            List<List<double>> Eb = new List<List<double>>() { new List<double>() };
            List<List<double>> Es = new List<List<double>>() { new List<double>() };
            List<List<double>> Ebs = new List<List<double>>() { new List<double>() };
            // Заполняем секущие модули для нулевой итерации
            for (int i = 0; i < n; i++)
            {
                //Eb[0].Add(Eb0);
                Eb[0].Add(Ebt);
            }

            for (int i = 0; i < As.Count; i++)
            {
                Es[0].Add(Es0);
                //Ebs[0].Add(Eb0);
                Ebs[0].Add(Ebt);
            }

            // Массив привязок центра тяжести
            List<double> ycm = new List<double>();
            List<double> zcm = new List<double>();

            // Вычисляем положение начального (геометрического) центра тяжести
            var numcy = Ab.Zip(y0b, (A, y) => A * y).Sum();
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
            List<List<double>> ys = new List<List<double>>() { new List<double>() };
            List<List<double>> zs = new List<List<double>>() { new List<double>() };

            //заполняем массивы привязок относительно ц.т. на нулевой итерации    
            for (int k = 0; k < n; k++)
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
            double dxx0 = Eb[0].Zip(Ab, (E, A) => E * A).Sum() +
                            Es[0].Zip(As, (E, A) => E * A).Sum() -
                            Ebs[0].Zip(As, (E, A) => E * A).Sum();
            Dxx.Add(dxx0);

            double dyy0 = Eb[0].ZipThree(Ab, zb[0], (E, A, z) => E * A * z * z).Sum() +
                            Es[0].ZipThree(As, zs[0], (E, A, z) => E * A * z * z).Sum() -
                            Ebs[0].ZipThree(As, zs[0], (E, A, z) => E * A * z * z).Sum();
            Dyy.Add(dyy0);

            double dzz0 = Eb[0].ZipThree(Ab, yb[0], (E, A, y) => E * A * y * y).Sum() +
                            Es[0].ZipThree(As, ys[0], (E, A, y) => E * A * y * y).Sum() -
                            Ebs[0].ZipThree(As, ys[0], (E, A, y) => E * A * y * y).Sum();
            Dzz.Add(dzz0);

            double dyz0 = Eb[0].ZipFour(Ab, yb[0], zb[0], (E, A, y, z) => E * A * y * z).Sum() +
                            Es[0].ZipFour(As, ys[0], zs[0], (E, A, y, z) => E * A * y * z).Sum() -
                            Ebs[0].ZipFour(As, ys[0], zs[0], (E, A, y, z) => E * A * y * z).Sum();
            Dyz.Add(dyz0);

            // Создаем массивы параметров деформаций
            List<double> ep0 = new List<double>();
            List<double> Ky = new List<double>();
            List<double> Kz = new List<double>();

            //Вычисляем параметры деформаций на нулевой итерации
            ep0.Add(N0 / Dxx[0]);
            double denomK = Dyy[0] * Dzz[0] - Math.Pow(Dyz[0], 2);

            Ky.Add((Mz[0] * Dyy[0] + My[0] * Dyz[0]) / denomK);
            Kz.Add(-(My[0] * Dzz[0] + Mz[0] * Dyz[0]) / denomK);

            //создаем массивы для деформаций бетона и арматуры   
            List<List<double>> epB = new List<List<double>>() { new List<double>() };
            List<List<double>> epS = new List<List<double>>() { new List<double>() };

            //Вычисляем деформации на нулевой итерации   
            for (int k = 0; k < n; k++)
                epB[0].Add(ep0[0] + yb[0][k] * Ky[0] + zb[0][k] * Kz[0]);

            for (int l = 0; l < m; l++)
                epS[0].Add(ep0[0] + ys[0][l] * Ky[0] + zs[0][l] * Kz[0]);

            // Создаем массивы для напряжений
            List<List<double>> sigB = new List<List<double>>() { new List<double>() };
            List<List<double>> sigS = new List<List<double>>() { new List<double>() };
            List<List<double>> sigBS = new List<List<double>>() { new List<double>() };

            // Заполняем напряжения на нулевой итерации
            for (int k = 0; k < n; k++)
            {
                sigB[0].Add(Diagr_FB(epB[0][k]));
            }

            for (int l = 0; l < m; l++)
            {
                sigS[0].Add(Diagr_S(epS[0][l]));

                sigBS[0].Add(Diagr_FB(epS[0][l]));
            }
            #endregion

            // итерации 
            for (int j = 1; j <= jmax; j++)
            {
                // пересчитываем секущие модули
                Eb.Add(new List<double>());
                for (int k = 0; k < n; k++)
                {
                    Eb[j].Add(EV_Sec(sigB[j - 1][k], epB[j - 1][k], Eb0));
                }

                Es.Add(new List<double>());
                Ebs.Add(new List<double>());
                for (int l = 0; l < m; l++)
                {
                    Es[j].Add(EV_Sec(sigS[j - 1][l], epS[j - 1][l], Es0));
                    Ebs[j].Add(EV_Sec(sigBS[j - 1][l], epS[j - 1][l], Eb0));
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

                // пересчитываем положение упруго-геометрического ц.т.
                numcy = Eb[j].ZipThree(Ab, y0b, (E, A, y0) => E * A * y0).Sum() +
                        Es[j].ZipThree(As, y0s, (E, A, y0) => E * A * y0).Sum() -
                        Ebs[j].ZipThree(As, y0s, (E, A, y0) => E * A * y0).Sum();

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
                    zb[j].Add(z0b[k] - zcm[j]);
                }

                ys.Add(new List<double>());
                zs.Add(new List<double>());
                for (int l = 0; l < m; l++)
                {
                    ys[j].Add(y0s[l] - ycm[j]);
                    zs[j].Add(z0s[l] - zcm[j]);
                }

                // пересчитываем жесткости
                double dyy = Eb[j].ZipThree(Ab, zb[j], (E, A, z) => E * A * z * z).Sum() +
                                Es[j].ZipThree(As, zs[j], (E, A, z) => E * A * z * z).Sum() -
                                Ebs[j].ZipThree(As, zs[j], (E, A, z) => E * A * z * z).Sum();
                Dyy.Add(dyy);

                double dzz = Eb[j].ZipThree(Ab, yb[j], (E, A, y) => E * A * y * y).Sum() +
                                Es[j].ZipThree(As, ys[j], (E, A, y) => E * A * y * y).Sum() -
                                Ebs[j].ZipThree(As, ys[j], (E, A, y) => E * A * y * y).Sum();
                Dzz.Add(dzz);

                double dyz = Eb[j].ZipFour(Ab, yb[j], zb[j], (E, A, y, z) => E * A * y * z).Sum() +
                                Es[j].ZipFour(As, ys[j], zs[j], (E, A, y, z) => E * A * y * z).Sum() -
                                Ebs[j].ZipFour(As, ys[j], zs[j], (E, A, y, z) => E * A * y * z).Sum();
                Dyz.Add(dyz);

                denomK = Dyy[j] * Dzz[j] - Math.Pow(Dyz[j], 2);
                if (denomK == 0)
                {
                    err = 1;
                    break;
                }
                // Пересчитываем моменты
                My.Add(My[0] + N0 * (zcm[j] - zcm[0]));
                Mz.Add(Mz[0] - N0 * (ycm[j] - ycm[0]));

                // Пересчитываем параметры деформаций
                ep0.Add(N0 / Dxx[j]);
                Ky.Add((Mz[j] * Dyy[j] + My[j] * Dyz[j]) / denomK);
                Kz.Add(-(My[j] * Dzz[j] + Mz[j] * Dyz[j]) / denomK);

                // Пересчитываем деформации
                epB.Add(new List<double>());
                for (int k = 0; k < n; k++)
                    epB[j].Add(ep0[j] + yb[j][k] * Ky[j] + zb[j][k] * Kz[j]);

                epS.Add(new List<double>());
                for (int l = 0; l < m; l++)
                    epS[j].Add(ep0[j] + ys[j][l] * Ky[j] + zs[j][l] * Kz[j]);

                // Пересчитываем напряжения
                sigB.Add(new List<double>());
                for (int k = 0; k < n; k++)
                    sigB[j].Add(Diagr_FB(epB[j][k]));

                sigS.Add(new List<double>());
                sigBS.Add(new List<double>());
                for (int l = 0; l < m; l++)
                {
                    sigS[j].Add(Diagr_S(epS[j][l]));
                    sigBS[j].Add(Diagr_FB(epS[j][l]));
                }

                // Проверка - выполняются ли условия в равновестия?
                Nint = sigB[j].Zip(Ab, (s, A) => s * A).Sum() + sigS[j].Zip(As, (s, A) => s * A).Sum() -
                        sigBS[j].Zip(As, (s, A) => s * A).Sum();

                Myint = -(sigB[j].ZipThree(Ab, zb[j], (s, A, z) => s * A * z).Sum() + sigS[j].ZipThree(As, zs[j], (s, A, z) => s * A * z).Sum() -
                            sigBS[j].ZipThree(As, zs[j], (s, A, z) => s * A * z).Sum());

                Mzint = sigB[j].ZipThree(Ab, yb[j], (s, A, y) => s * A * y).Sum() + sigS[j].ZipThree(As, ys[j], (s, A, y) => s * A * y).Sum() -
                        sigBS[j].ZipThree(As, ys[j], (s, A, y) => s * A * y).Sum();

                //  Расчеты по 2 группе предельных состояний
                if (GroupLSD == BSFiberLib.CG2)
                {
                    //1. проверка на возникновение трещины
                    //2. определение ширины раскрытия трещины, если трещина возникла

                    // максимальная деформация в сечении
                    double epsBt = epB[j].Maximum();
                    // условие возникновения трещины
                    if (ebt_crc == 0 && (epsBt > 0 && epsBt >= efbt1)) { ebt_crc = epsBt; }

                    // Трещина возникла:
                    //-- определяем моменты трещинообразования
                    My_crc = My.Max(); 
                    Mz_crc = Mz.Max();
                    N_crc = 0;
                    
                    //-- рассчитываем ширину раскрытия трещины
                    if (ebt_crc != 0)
                    {
                        es_crc = epS[j].Maximum(); // деформация арматуры

                        sig_s_crc = sigS[j].Maximum(); // напряжение в арматуре

                        // цикл по стержням арматуры
                        foreach (double _d in d_nom)
                        {
                            double ls = L_s(_d);

                            a_crc = Math.Max(a_crc,  A_crc(sig_s_crc, ls)); // ширина раскрытия трещины
                        }
                    }
                }

                // Вычисление погрешностей
                double tol_ep0 = Math.Abs(ep0[j] - ep0[j - 1]); // вычисление в серединной линии
                double tol_Ky = Math.Abs(Ky[j] - Ky[j - 1]);
                double tol_Kz = Math.Abs(Kz[j] - Kz[j - 1]);

                double tol = new double[] { tol_ep0, tol_Ky, tol_Kz }.Max();

                if (tol < tolmax)
                {
                    err = -1;
                    break;
                }

                if (j == jmax - 1)
                {
                    err = 2;  // Достигнуто максимальное число итераций
                    break;
                }

                if (epB[j].Max() > 1)
                {
                    err = 3; // Деформации превысили разумный предел
                    break;
                }
            }

            int jend = sigB.Count - 1;

            // Проверка - выполняются ли условия в равновестия?
            Nint = sigB[jend].Zip(Ab, (s, A) => s * A).Sum() + sigS[jend].Zip(As, (s, A) => s * A).Sum() -
                    sigBS[jend].Zip(As, (s, A) => s * A).Sum();

            Myint = -(sigB[jend].ZipThree(Ab, zb[jend], (s, A, z) => s * A * z).Sum() + sigS[jend].ZipThree(As, zs[jend], (s, A, z) => s * A * z).Sum() -
                        sigBS[jend].ZipThree(As, zs[jend], (s, A, z) => s * A * z).Sum());

            Mzint = sigB[jend].ZipThree(Ab, yb[jend], (s, A, y) => s * A * y).Sum() + sigS[jend].ZipThree(As, ys[jend], (s, A, y) => s * A * y).Sum() -
                    sigBS[jend].ZipThree(As, ys[jend], (s, A, y) => s * A * y).Sum();

            // растяжение: 
            // напряжения:
            double sigB_t = sigB[jend].Maximum();
            double sigS_t = sigS[jend].Maximum();
            // деформации:
            double epsB_t = epB[jend].Maximum();
            double epsS_t = epS[jend].Maximum();

            // сжатие: 
            // напряжения:
            double sigB_p = sigB[jend].Minimum();
            double sigS_p = sigS[jend].Minimum();
            // деформации:
            double epsB_p = epB[jend].Minimum();
            double epsS_p = epS[jend].Minimum();

            // определяем коэффициенты использоввания:
            // -- по деформациям бетона
            e_fbt_ult = efbt1;
            UtilRate_fb = (e_fbt_ult != 0) ? epsB_t / e_fbt_ult : 1.0;
            // -- по деформациям арматуры
            UtilRate_s = (esc0 != 0) ? epsS_t / esc0 : 1.0;

            // СП 6.1.25 для эпюры с одним знаком
            // double e_fb_ult = ebc2 - (ebc2 - ebc0) * epsB_t / epsB_p;
            // double e_fbt_ult = efbt3 - (efbt3 - efbt2) * epsB_t / epsB_p;

            m_Results = new Dictionary<string, double>
            {
                ["ep0"] = ep0[jend],
                ["Ky"] = Ky[jend],
                ["ry"] = 1 / Ky[jend],
                ["Kz"] = Kz[jend],
                ["rz"] = 1 / Kz[jend],

                // растяжение
                ["sigB"] = sigB_t,
                ["sigS"] = sigS_t,
                ["epsB"] = epsB_t,
                ["epsS"] = epsS_t,

                // сжатие
                ["sigB_p"] = sigB_p,
                ["sigS_p"] = sigS_p,
                ["epsB_p"] = epsB_p,
                ["epsS_p"] = epsS_p,
                // предел
                ["esc0"] = esc0,

                // проверка усилий
                ["My"] = Myint,
                ["Mx"] = Mzint,
                ["N"] = Nint,

                ["UR_fb"] = UtilRate_fb,
                ["UR_s"] = UtilRate_s,

                // трещиностойкость
                ["My_crc"] =  My_crc,
                ["Mx_crc"] =  Mz_crc,
                ["es_crc"] = es_crc,
                ["sig_s_crc"] = sig_s_crc,
                ["a_crc"] = a_crc,
                ["ItersCnt"] = jend
            };

            SigmaBResult = new List<double>(sigB[jend]);
            SigmaSResult = new List<double>(sigS[jend]);
            EpsilonBResult = new List<double>(epB[jend]);
            EpsilonSResult = new List<double>(epS[jend]);
        }        
    }
}

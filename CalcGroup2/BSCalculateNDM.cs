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
                                private void Calculate()
        {
                        My = new List<double> { My0 };
            Mz = new List<double> { Mz0 };
                        InitDeformParams();
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
            else if (m_BeamSection == BeamSection.Any)
            {
                n = InitAnySection();
                m = InitReinforcement();
            }
            else
            {
                throw new Exception($"Тип сечения {m_BeamSection} не поддерживается в данном расчете ");
            }
                                                List<List<double>> Eb = new List<List<double>>() { new List<double>() };
            List<List<double>> Es = new List<List<double>>() { new List<double>() };
            List<List<double>> Ebs = new List<List<double>>() { new List<double>() };
                        for (int i = 0; i < n; i++)
            {                
                Eb[0].Add(Ebt);
            }
            for (int i = 0; i < As.Count; i++)
            {
                Es[0].Add(Es0);             
                Ebs[0].Add(Ebt);
            }
                        List<double> ycm = new List<double>();
            List<double> zcm = new List<double>();
                        var numcy = Ab.Zip(y0b, (A, y) => A * y).Sum();
            var numcz = Ab.Zip(z0b, (A, z) => A * z).Sum();
            double denomc = Ab.Sum(A => A);
            double cy = numcy / denomc;
            ycm.Add(cy);
            double cz = numcz / denomc;
            zcm.Add(cz);
                        List<List<double>> yb = new List<List<double>>() { new List<double>() };
            List<List<double>> zb = new List<List<double>>() { new List<double>() };
                        List<List<double>> ys = new List<List<double>>() { new List<double>() };
            List<List<double>> zs = new List<List<double>>() { new List<double>() };
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
                                    List<double> Dxx = new List<double>();
                        List<double> Dyy = new List<double>();
                        List<double> Dzz = new List<double>();
                        List<double> Dyz = new List<double>();
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
                        List<double> ep0 = new List<double>();
            List<double> Ky = new List<double>();
            List<double> Kz = new List<double>();
                        ep0.Add(N0 / Dxx[0]);
            double denomK = Dyy[0] * Dzz[0] - Math.Pow(Dyz[0], 2);
            Ky.Add((Mz[0] * Dyy[0] + My[0] * Dyz[0]) / denomK);
            Kz.Add(-(My[0] * Dzz[0] + Mz[0] * Dyz[0]) / denomK);
                        List<List<double>> epB = new List<List<double>>() { new List<double>() };
            List<List<double>> epS = new List<List<double>>() { new List<double>() };
                        for (int k = 0; k < n; k++)
                epB[0].Add(ep0[0] + yb[0][k] * Ky[0] + zb[0][k] * Kz[0]);
            for (int l = 0; l < m; l++)
                epS[0].Add(ep0[0] + ys[0][l] * Ky[0] + zs[0][l] * Kz[0]);
                        List<List<double>> sigB = new List<List<double>>() { new List<double>() };
            List<List<double>> sigS = new List<List<double>>() { new List<double>() };
            List<List<double>> sigBS = new List<List<double>>() { new List<double>() };
                        for (int k = 0; k < n; k++)
            {
                sigB[0].Add(Diagr_FB(epB[0][k]));
            }
            for (int l = 0; l < m; l++)
            {
                sigS[0].Add(Diagr_S(epS[0][l]));
                sigBS[0].Add(Diagr_FB(epS[0][l]));
            }
                                    for (int j = 1; j <= jmax; j++)
            {
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
                                double _dxx = Eb[j].Zip(Ab, (E, A) => E * A).Sum() +
                                Es[j].Zip(As, (E, A) => E * A).Sum() -
                                Ebs[j].Zip(As, (E, A) => E * A).Sum();
                Dxx.Add(_dxx);
                if (Dxx[j] == 0)
                {
                    err = 1;
                    break;
                }
                                numcy = Eb[j].ZipThree(Ab, y0b, (E, A, y0) => E * A * y0).Sum() +
                        Es[j].ZipThree(As, y0s, (E, A, y0) => E * A * y0).Sum() -
                        Ebs[j].ZipThree(As, y0s, (E, A, y0) => E * A * y0).Sum();
                numcz = Eb[j].ZipThree(Ab, z0b, (E, A, z0) => E * A * z0).Sum() +
                        Es[j].ZipThree(As, z0s, (E, A, z0) => E * A * z0).Sum() -
                        Ebs[j].ZipThree(As, z0s, (E, A, z0) => E * A * z0).Sum();
                ycm.Add(numcy / Dxx[j]);
                zcm.Add(numcz / Dxx[j]);
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
                                My.Add(My[0] + N0 * (zcm[j] - zcm[0]));
                Mz.Add(Mz[0] - N0 * (ycm[j] - ycm[0]));
                                ep0.Add(N0 / Dxx[j]);
                Ky.Add((Mz[j] * Dyy[j] + My[j] * Dyz[j]) / denomK);
                Kz.Add(-(My[j] * Dzz[j] + Mz[j] * Dyz[j]) / denomK);
                                epB.Add(new List<double>());
                for (int k = 0; k < n; k++)
                    epB[j].Add(ep0[j] + yb[j][k] * Ky[j] + zb[j][k] * Kz[j]);
                epS.Add(new List<double>());
                for (int l = 0; l < m; l++)
                    epS[j].Add(ep0[j] + ys[j][l] * Ky[j] + zs[j][l] * Kz[j]);
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
                                Nint = sigB[j].Zip(Ab, (s, A) => s * A).Sum() + sigS[j].Zip(As, (s, A) => s * A).Sum() -
                        sigBS[j].Zip(As, (s, A) => s * A).Sum();
                Myint = -(sigB[j].ZipThree(Ab, zb[j], (s, A, z) => s * A * z).Sum() + sigS[j].ZipThree(As, zs[j], (s, A, z) => s * A * z).Sum() -
                            sigBS[j].ZipThree(As, zs[j], (s, A, z) => s * A * z).Sum());
                Mzint = sigB[j].ZipThree(Ab, yb[j], (s, A, y) => s * A * y).Sum() + sigS[j].ZipThree(As, ys[j], (s, A, y) => s * A * y).Sum() -
                        sigBS[j].ZipThree(As, ys[j], (s, A, y) => s * A * y).Sum();
                                if (GroupLSD == BSFiberLib.CG2)
                {
                                        
                                        double epsBt = epB[j].Maximum();
                                        if (ebt_crc == 0 && (epsBt > 0 && epsBt >= efbt1)) { ebt_crc = epsBt; }
                                                            My_crc = My.Max(); 
                    Mz_crc = Mz.Max();
                    N_crc = 0;
                    
                                        if (CalcA_crc)
                    {
                                                es_crc = epS[j].Maximum();
                                                sig_s_crc = sigS[j].Maximum(); 
                                                foreach (double _d in d_nom)
                        {
                            double ls = L_s(_d);
                            a_crc = Math.Max(a_crc,  A_crc(sig_s_crc, ls));                         }
                    }
                }
                                double tol_ep0 = Math.Abs(ep0[j] - ep0[j - 1]);                 double tol_Ky = Math.Abs(Ky[j] - Ky[j - 1]);
                double tol_Kz = Math.Abs(Kz[j] - Kz[j - 1]);
                double tol = new double[] { tol_ep0, tol_Ky, tol_Kz }.Max();
                if (tol < tolmax)
                {
                    err = -1;
                    break;
                }
                if (j == jmax - 1)
                {
                    err = 2;                      break;
                }
                if (epB[j].Max() > 1)
                {
                    err = 3;                     break;
                }
            }
            int jend = sigB.Count - 1;
                        Nint = sigB[jend].Zip(Ab, (s, A) => s * A).Sum() + sigS[jend].Zip(As, (s, A) => s * A).Sum() -
                    sigBS[jend].Zip(As, (s, A) => s * A).Sum();
            Myint = -(sigB[jend].ZipThree(Ab, zb[jend], (s, A, z) => s * A * z).Sum() + sigS[jend].ZipThree(As, zs[jend], (s, A, z) => s * A * z).Sum() -
                        sigBS[jend].ZipThree(As, zs[jend], (s, A, z) => s * A * z).Sum());
            Mzint = sigB[jend].ZipThree(Ab, yb[jend], (s, A, y) => s * A * y).Sum() + sigS[jend].ZipThree(As, ys[jend], (s, A, y) => s * A * y).Sum() -
                    sigBS[jend].ZipThree(As, ys[jend], (s, A, y) => s * A * y).Sum();
                                    double sigB_t = NuNTo0(sigB[jend].Maximum());
            double sigS_t = NuNTo0(sigS[jend].Maximum());
                        double epsB_t = NuNTo0(epB[jend].Maximum());
            double epsS_t = NuNTo0(epS[jend].Maximum());
                                    double sigB_p = NuNTo0(sigB[jend].Minimum());
            double sigS_p = NuNTo0(sigS[jend].Minimum());
                        double epsB_p = NuNTo0(epB[jend].Minimum());
            double epsS_p = NuNTo0(epS[jend].Minimum());
            
                        if (Setup.UseRebar == false && Math.Sign(sigB_t) == Math.Sign(sigB_p))
            {
                e_fb_ult = (epsB_p != 0) ? ebc2 - (ebc2 - ebc0) * epsB_t / epsB_p : 0;
                e_fbt_ult = (epsB_p != 0) ? efbt3 - (efbt3 - efbt2) * epsB_t / epsB_p : 0;
            }
            else
            {
                e_fb_ult = 0;
                                e_fbt_ult = efbt1;             }
                                    UtilRate_fb_t = (efbt1 != 0) ? epsB_t / efbt1 : 0.0;
            UtilRate_s_t = (esc0 != 0) ? epsS_t / esc0 : 0.0;
                        UtilRate_fb_p = (ebc0 != 0) ? epsB_p / ebc0 : 0.0;
            UtilRate_s_p = (esc0 != 0) ? epsS_p / esc0 : 0.0;
            
            m_Results = new Dictionary<string, double>
            {
                                ["ep0"] = ep0[jend],
                ["Ky"] = Ky[jend],
                ["ry"] = 1 / Ky[jend],
                ["Kz"] = Kz[jend],
                ["rz"] = 1 / Kz[jend],
                                ["sigB"] = sigB_t,
                ["sigS"] = sigS_t,
                ["epsB"] = epsB_t,
                ["epsS"] = epsS_t,
                                ["sigB_p"] = sigB_p,
                ["sigS_p"] = sigS_p,
                ["epsB_p"] = epsB_p,
                ["epsS_p"] = epsS_p,
                                ["esc0"] = esc0,
                ["e_fb_ult"] = e_fb_ult,
                ["e_fbt_ult"] = e_fbt_ult,
                                ["My"] = Myint,
                ["Mx"] = Mzint,
                ["N"] = Nint,
                                                ["UR_fb_t"] = UtilRate_fb_t,
                ["UR_s_t"] = UtilRate_s_t,
                                ["UR_fb_p"] = UtilRate_fb_p,
                ["UR_s_p"] = UtilRate_s_p,
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
        private double NuNTo0(double _value)
        {
            if (double.IsNaN(_value))
                return 0;
            else
                return _value;
        }
    }
   
}

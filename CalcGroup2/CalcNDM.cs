using BSFiberConcrete.CalcGroup2;
using BSFiberConcrete.Lib;
using System.Collections.Generic;

namespace BSFiberConcrete
{
    public class CalcNDM
    {
        const int GR1 = BSFiberLib.CG1;
        const int GR2 = BSFiberLib.CG2;

        private BeamSection m_BeamSection;

        private double My0, Mx0, N0;

                public Dictionary<string, double> D;

        public NDMSetup setup { get; set; }

        public BSCalcResultNDM CalcRes => m_CalcRes;
        private BSCalcResultNDM m_CalcRes;

                private double LeftX;

        private List<double> Xs = new List<double>();
        private List<double> Ys = new List<double>();

        private List<double> lD;
        private List<double> lX;
        private List<double> lY;

                                private List<string> m_Message;

        public CalcNDM(BeamSection _BeamSection)
        {
            m_BeamSection = _BeamSection;
            setup = BSData.LoadNDMSetup();
            LeftX = 0;
        }

                                                                public double Y_interpolate(double[] _Y, double[] _X, double _x)
        {             
            Lagrange.Lagrange lagrange = new Lagrange.Lagrange();

            double value = lagrange.GetValue(_X, _Y, _x);
         
            return value;
        }

        private void Init()
        {
                        if (BSHelper.IsRectangled(m_BeamSection)) 
                LeftX = -D["b"] / 2.0;

            double _qty, _area;
            
            (lD, lX, lY, _qty, _area) = BSCalcNDM.ReinforcementBinding(m_BeamSection, LeftX, 0, setup.UseRebar);

            if (!D.ContainsKey("rods_qty"))
                D.Add("rods_qty", _qty);
            if (!D.ContainsKey("rods_area"))
                D.Add("rods_area", _area);

            My0 = D["My"];
            Mx0 = D["Mz"];
            N0 = D["N"];
        }

                                private BSCalcNDM BSCalcGr1(double _coefM = 1.0)
        {
            Init();
            BSCalcNDM bsCalcGR1 = new BSCalcNDM(GR1, m_BeamSection, setup);
            bsCalcGR1.SetMN(Mx0, My0, N0);
            bsCalcGR1.SetParamsGroup1(D);
            bsCalcGR1.MzMyNUp(_coefM);
            bsCalcGR1.SetRods(lD, lX, lY);
            bsCalcGR1.Run();

            return bsCalcGR1;
        }

                                private BSCalcNDM BSCalcGr2(double _Mx, double _My, double _N)
        {
            BSCalcNDM bscalc = new BSCalcNDM(GR2, m_BeamSection, setup);
            bscalc.SetParamsGroup2(D);
            bscalc.SetMN(_Mx, _My, _N);
            bscalc.MzMyNUp(1.0); 
            bscalc.SetRods(lD, lX, lY);
            bscalc.Run();

            return bscalc;
        }
       
                BSCalcNDM BSCalcGr2_a_Crc(double _coefM, List<double> _E_s_crc = null)
        {
            NdmCrc ndmCrc = BSData.LoadNdmCrc();
            ndmCrc.InitFi2(setup.RebarType);
            ndmCrc.InitFi3(D["N"]);
           
            BSCalcNDM bscalc = new BSCalcNDM(GR2, m_BeamSection, setup);
            bscalc.SetParamsGroup2(D);
            bscalc.SetMN(Mx0, My0, N0);
            bscalc.MzMyNUp(_coefM);
            bscalc.NdmCrc = ndmCrc;
            bscalc.SetRods(lD, lX, lY);
            bscalc.SetE_S_Crc(_E_s_crc);
            bscalc.Run();

            m_CalcRes.ErrorIdx.Add(bscalc.Err);
            m_CalcRes.SetRes2Group(bscalc.Results, false, true);
            return bscalc;
        }

                                        public Dictionary<string, double> RunMy(double _My)
        {            
            Init();
            BSCalcNDM bsCalcGR1 = new BSCalcNDM(GR1, m_BeamSection, setup);
            bsCalcGR1.SetParamsGroup1(D);
            bsCalcGR1.SetMN(0, _My, 0);
            bsCalcGR1.SetRods(lD, lX, lY);
            bsCalcGR1.Run();
            
            return bsCalcGR1.Results;            
        }

                                        public bool RunGroup1()
        {
            BSCalcNDM bsCalcGR1 = BSCalcGr1();
            
            m_CalcRes = new BSCalcResultNDM(bsCalcGR1.Results);
            m_CalcRes.InitFromCalcNDM(bsCalcGR1);
            m_CalcRes.InitCalcParams(D);
            m_CalcRes.ResultsMsg1Group(ref m_Message);

            return true;
        }

        private bool Validate()
        {
            bool res = true;

            if (D["Mz"] == 0 && D["My"] == 0 && D["N"] == 0)
            {
                res = false;
            }            
            return res;
        }

                                public void Run()
        {
            if (!Validate())
                return;

            Init();

            bool ok = RunGroup1();

            if (ok)
            {                
                if (setup.UseRebar) 
                {
                                        double mx0 = Mx0, my0= My0, n0 = N0;
                                        double ur = RunGroup2_UtilRate();
                    if (ur > 1.0)
                    {
                        mx0 = Mx0 / ur;
                        my0 = My0 / ur;
                        n0 = N0 / ur;
                    }
                    BSCalcNDM bsCalc_Mcrc = RunGroup2_Mcrc(mx0, my0, n0);

                                                            List<double> E_S_crc = bsCalc_Mcrc.EpsilonSResult;                    
                                        BSCalcNDM bsCalc_crc = BSCalcGr2_a_Crc(1.0, E_S_crc);
                }
                else
                {
                                                            BSCalcNDM bscalc = BSCalcGr2(Mx0, My0, N0);
                    m_CalcRes.ErrorIdx.Add(bscalc.Err);
                    m_CalcRes.SetRes2Group(bscalc.Results);
                }
            }            
        }

                private BSCalcNDM bsсalcgr2_Mcrc(double _coefM, double _Mx, double _My, double _N)
        {
            BSCalcNDM bscalc = BSCalcGr2(_Mx* _coefM, _My* _coefM, _N* _coefM);
            m_CalcRes.ErrorIdx.Add(bscalc.Err);
            m_CalcRes.SetRes2Group(bscalc.Results);

                        if (bscalc.UtilRate_fb_t <= 1.0)
            {
                if (!Ys.Contains(_coefM))
                {
                    Xs.Add(bscalc.UtilRate_fb_t);                     Ys.Add(_coefM);                  }
            }
            return bscalc;
        }

                                        private double RunGroup2_UtilRate()                        
        {
            BSCalcNDM bsCalc1 = BSCalcGr2(Mx0, My0, N0);
            double ur = bsCalc1.UtilRate_fb_t;
            return ur;                
        }

                                public BSCalcNDM RunGroup2_Mcrc(double mx0, double my0, double n0)
        {            
                                                double coef = 1;

            BSCalcNDM bscalc0 = bsсalcgr2_Mcrc(coef, mx0, my0, n0);
            double ur = bscalc0.UtilRate_fb_t;

                                    double dH = 1;
                        int iters = 0;
            
            while (ur < 0.8)
            {               
                BSCalcNDM bscalc = bsсalcgr2_Mcrc(coef, mx0, my0, n0);

                iters++;
                if (iters > 100) break;
                if (bscalc.UtilRate_fb_t > 0.8) break;
                coef += dH;
                ur = bscalc.UtilRate_fb_t;
            }
            if (coef >1)
                coef -= dH;

            dH = 0.2;
            for (int N = 1; N <= 100; N++)
            {
                coef += dH;
                BSCalcNDM _bsCalc = bsсalcgr2_Mcrc(coef, mx0 , my0 , n0);
                ur = _bsCalc.UtilRate_fb_t;
                if (_bsCalc.UtilRate_fb_t > 1)
                    break;
            }

            double y_coef = coef;             BSCalcNDM bsCalc_Mcrc = bsсalcgr2_Mcrc(y_coef, mx0, my0, n0);
            ur = bsCalc_Mcrc.UtilRate_fb_t;
            if (ur > 1.2)             {
                coef = y_coef - dH / 2.0;
                bsCalc_Mcrc = bsсalcgr2_Mcrc(coef, mx0, my0, n0);
                ur = bsCalc_Mcrc.UtilRate_fb_t;
                double My_crc = bsCalc_Mcrc.My_crc;              }

            return bsCalc_Mcrc;                            
        }
    }
}

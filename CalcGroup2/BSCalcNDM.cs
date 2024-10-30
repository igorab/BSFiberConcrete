using BSFiberConcrete.Lib;
using MathNet.Numerics.Statistics;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BSFiberConcrete.CalcGroup2
{       
    public partial class  BSCalcNDM 
    {
                                private readonly int GroupLSD;
                                private readonly NDMSetup Setup;
                                public NdmCrc NdmCrc { private get; set; }
                                public double Eps_s_crc { get; set; }
                private bool CalcA_crc => Eps_s_crc != 0;


                                private void InitDeformParams()
        {
                        ebc0 = 0.002;
            ebc2 = 0.0035;

                        efbt0 = 0.0;
            efbt1 = 0.0;
            efbt2 = 0.00015;
            efbt2 = 0.004;
            efbt3 = 0.02;

                                    esc2 = 0.025;
                        est2 = 0.025;

                        e_s_ult = 0.025;
        }

                                        public BSCalcNDM(int _groupLSD)
        {
            GroupLSD = _groupLSD;            
        }

                                public BSCalcNDM(int _groupLSD, BeamSection _BeamSection, NDMSetup _Setup)
        {
            GroupLSD = _groupLSD;
            BeamSection = _BeamSection;
            Setup = _Setup;
            NdmCrc = new NdmCrc();
                        ny = Setup.N;
            nz = Setup.M;            
        }

                                                        public void SetMN(double _Mx, double _My, double _N)
        {
            Mz0 = BSHelper.kgssm2kNsm(_Mx);
            My0 = BSHelper.kgssm2kNsm(_My);            
            N0 = BSHelper.Kgs2kN(_N);
        }

        public void SetSizes(Dictionary<string, double> _D)
        {
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
        }

        public void SetE(Dictionary<string, double> _D)
        {
                        Eb0 = BSHelper.Kgssm2ToKNsm2(_D["Eb0"]);
                        Ebt = BSHelper.Kgssm2ToKNsm2(_D["Ebt"]);
                        Es0 = BSHelper.Kgssm2ToKNsm2(_D["Es0"]);
        }

        public void Deform_e(Dictionary<string, double> _D)
        {
                                    ebc0 = _D["ebc0"];
            ebc2 = _D["ebc2"];

                        efbt0 = _D["ebt0"];
            efbt2 = _D["ebt2"];
            efbt3 = _D["ebt3"];

                                    esc2 = _D["esc2"];
                        est2 = _D["est2"];
        }

                                                public void SetE_S_Crc(List<double> _es)
        {
            Eps_s_crc = _es.Maximum();
        }

        public void SetRGroup1(Dictionary<string, double> _D)
        {
                        Rbc = BSHelper.Kgssm2ToKNsm2(_D["Rbc"]);
                        Rfbt = BSHelper.Kgssm2ToKNsm2(_D["Rbt"]);
            Rfbt2 = BSHelper.Kgssm2ToKNsm2(_D["Rbt2"]);
            Rfbt3 = BSHelper.Kgssm2ToKNsm2(_D["Rbt3"]);

            Rsc = BSHelper.Kgssm2ToKNsm2(_D["Rsc"]);
            Rst = BSHelper.Kgssm2ToKNsm2(_D["Rst"]);
        }

        public void SetRGroup2(Dictionary<string, double> _D)
        {
                        Rbc = BSHelper.Kgssm2ToKNsm2(_D["Rbcn"]);
                        Rfbt = BSHelper.Kgssm2ToKNsm2(_D["Rbtn"]);
            Rfbt2 = BSHelper.Kgssm2ToKNsm2(_D["Rbt2n"]);
            Rfbt3 = BSHelper.Kgssm2ToKNsm2(_D["Rbt3n"]);

            Rsc = BSHelper.Kgssm2ToKNsm2(_D["Rscn"]);
            Rst = BSHelper.Kgssm2ToKNsm2(_D["Rstn"]);
        }

                public void SetParamsGroup1(Dictionary<string, double> _D)
        {            
            SetSizes(_D);          
            SetE(_D);
            Deform_e(_D);            
            SetRGroup1(_D);                                                
        }

        public void SetParamsGroup2(Dictionary<string, double> _D)
        {            
            SetSizes(_D);
            SetE(_D);
            Deform_e(_D);            
            SetRGroup2(_D);            
        }

                                        public void MzMyNUp(double _coef)
        {
            if (_coef == 0)
                return;

            Mz0 *= _coef;
            My0 *= _coef ;
            N0  *=  _coef;            
        }

                                                        public void SetRods(List<double> _bD, List<double> _bX, List<double> _bY )
        {
            if (_bD == null) return;

            ds.Clear();
            d_nom.Clear();
            y0s.Clear();
            z0s.Clear();
            
            int idx = 0;
            foreach (var d in _bD)
            {
                ds.Add(d);
                d_nom.Add(d * 10); 
                z0s.Add(_bX[idx]);
                y0s.Add(_bY[idx]);
                idx++;
            }
        }
        
        public BeamSection BeamSection  {
            set {
                m_BeamSection = value;
            }
        }

        private BeamSection m_BeamSection = BeamSection.Rect;

        #region Поля, свойства  - данные для расчета
                private double N0 = 0;
                private double My0 = 0; 
                private double Mz0 = 0;
                private double b = 0; 
                private double h = 0; 
                private double bf, hf, bw, hw, b1f, h1f;
                private double r1, R2;

                private int ny = 0;                 private int nz = 0; 
                private readonly List<double> d_nom = new List<double>() {};
        private readonly List<double> ds = new List<double>() {};

                private readonly List<double> y0s = new List<double>() {};
        private readonly List<double> z0s = new List<double>() {};

                        private double Eb0 = 0;
                private double Ebt = 0;
                private double Rbc = 0;
                private double Rfbt = 0;
                private double Rfbt2 = 0;
                private double Rfbt3 = 0;

                private double ebc0;
        private double ebc2;

                private double efbt0;
        private double efbt1;
        private double efbt2;
        private double efbt3;

                private double e_s_ult;
        private double e_fb_ult = 0;
        private double e_fbt_ult = 0;

                        private double Es0 = 0;
                private double Rst = 0;
                private double Rsc = 0;  

                private double esc0 = 0; 
        private double esc2 = 0;
                private double est0 = 0; 
        private double est2 = 0;

        private List<double> My;
        private List<double> Mz;

                public  double Mzint { get; private set; }
        public double Myint { get; private set; }
        public double Nint { get; private set; }

                public double Mz_crc { get; private set; }
        public double My_crc { get; private set; }       
        public double N_crc { get; private set; }

                        public double es_crc { get; private set; }
                public double ebt_crc { get; private set; }

                public double sig_s_crc { get; private set; }

                public double a_crc { get; private set; }
        public List<double> A_Crc { get; private set; }
        #endregion

                        public double UtilRate_fb_p { get; private set; }
                public double UtilRate_fb_t { get; private set; }
                public double UtilRate_s_p { get; private set; }
                public double UtilRate_s_t { get; private set; }

                private int jmax = 20000;
                private double tolmax = Math.Pow(10, -6);
        private int err = 0;

        private Dictionary<string, double> m_Results = new Dictionary<string, double>();

        public int Err => err;
        public Dictionary<string, double> Results => m_Results;
        
                                public List<double> SigmaBResult { get; private set; }
                                public List<double> SigmaSResult { get; private set; }
                                public List<double> EpsilonBResult { get; private set; }
                                public List<double> EpsilonSResult { get; private set; }

        #region разбивка сечения на элементы
                private List<double> y0b = new List<double>();

                private List<double> z0b = new List<double>();

                private List<double> Ab = new List<double>();

                private List<double> As = new List<double>();
        #endregion

        private void InitSectionsLists()
        {
            Ab = new List<double>();
            y0b = new List<double>();
            z0b = new List<double>();
            As = new List<double>();
            A_Crc = new List<double>();
        }
                      
                                public bool Run()
        {
            bool ok;
            try
            {                
                Calculate();
                ok =  true;
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
                ok = false;
            }              
            return ok;
        }       
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Text;
using System.Security.Cryptography;
using System.Windows.Forms;
using BSFiberConcrete.CalcGroup2;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using TriangleNet.Geometry;

namespace BSFiberConcrete
{     
                public class BSFiberCalc_Deform : IBSFiberCalculation
    {
        public List<string> Msg { get; private set; }

                public int groupLSD { get; set; } 

                public Point CG { get; set; }
        public List<double> triAreas { get; set; }
        public List<Point> triCGs { get; set; }

                                public double Mx { get; set; }
        public double My { get; set; }
                public double N { get; set; }

        private const int cIters = 10000;
        private double[,] zero3x3 = new double[,] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };

                public BSBeam Beam
        {
            get { return m_Beam; }
            set { m_Beam = value;
                m_Rods = value.Rods; }
        }

                                public List<BSRod> Rods {
            get { return m_Rods; }
            set { m_Rods = value; } 
        }

                public BSMatFiber MatFiber { get { return m_Fiber; } set { m_Fiber = value; } }
                public BSMatRod MatRebar { get { return m_Rod; } set { m_Rod = value; } }

        private BSBeam m_Beam { get; set; }

        private BSMatFiber m_Fiber;
        private BSMatRod m_Rod;

                private int m_Y_N = 1;         private int m_X_M = 1; 
        private List<BSElement> m_BElem;
        private List<BSRod> m_Rods;

        [DisplayName("Радиус кривизны продольной оси в плоскости действия моментов, Rx, см")]
        public double rx { get; private set; }

        [DisplayName("Радиус кривизны продольной оси в плоскости действия моментов, Ry, см")]
        public double ry { get; private set; }

        [DisplayName("Относительная деформация волокна в Ц.Т. сечения, e0") ]
        public double eps_0 { get; private set; }

        [DisplayName("Кривизна 1/Rx, 1/см" )]
        public double Kx { get; private set; }

        [DisplayName("Кривизна 1/Ry, 1/см")]
        public double Ky { get; private set; }

        [DisplayName("Максимальная относительная деформация")]
        public double e_fb_max { get; private set; }

        [DisplayName("Максимальный момент возникновения трещины")]
        public double Mcrc { get; private set; }

                private const int I1 = 0, I2 = 1, I3 = 2;
        private Matrix<double> D = DenseMatrix.OfArray(new double[,] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } });
        private double[,] Db = new double[I3 + 1, I3 + 1];
        private double[,] Ds = new double[I3 + 1, I3 + 1];

                private Vector<double> Ab;
                private Vector<double> As;
                private Vector<double> Zfbx;
        private Vector<double> Zfby;
                private Vector<double> Zsx;
        private Vector<double> Zsy;

                private Vector<double> sigma_fb;
                private Vector<double> sigma_s;

                private Vector<double> epsilon_fb;
                private Vector<double> epsilon_s;
                private Vector<double> Nju_s;

                private Vector<double> Nju_fb;
        
        private readonly Dictionary<string, double> m_Res;
        private readonly Dictionary<string, double> m_Res2Group;

                public Dictionary<string, double> Results() => m_Res;
        public Dictionary<string, double> Result2Group => m_Res2Group;

        public Dictionary<string, double> Efforts
        {
            get { return new Dictionary<string, double> 
            { 
                { "Mx, кг*см", Mx }, 
                { "My, кг*см", My }, 
                { "N, кг", N } }; 
            }
        }

        public Dictionary<string, double> PhysParams
        {
            get
            {
                return new Dictionary<string, double> { 
                    { "Rfb,n, кг/см2", m_Fiber.Rfbn }, 
                    { "Eb, кг/см2", m_Fiber.Eb }, 
                    { "e_fb_ult", m_Fiber.Eps_fb_ult },
                    { "Rs, кг/см2", m_Rod.Rs }, 
                    { "Es, кг/см2", m_Rod.Es }, 
                    { "e_s_ult", m_Rod.Eps_s_ult(DeformDiagram) }
                };
            }
        }

        public Dictionary<string, double> Reinforcement { get; internal set; }
        public DeformDiagramType DeformDiagram { get; internal set; }
        public DeformMaterialType DeformMaterialType { get; set; }

        private double Mx_b_calc = 0;
        private double My_b_calc = 0;
        private double N_b_calc = 0;

                                                        public BSFiberCalc_Deform (double _Mx = 0, double _My = 0, double _N = 0)
        {
            groupLSD = 1;

            Mx = _Mx;
            My = _My;
            N  = _N;

            rx = 0;
            ry = 0;
            eps_0 = 1;

            m_Fiber = new BSMatFiber();
            m_Rod = new BSMatRod();
            Msg = new List<string>();
            m_Res = new Dictionary<string, double>();
            m_Res2Group = new Dictionary<string, double>();
        }
       
                                                public void InitVectors(int _I, int _J)
        {
                        Ab = Vector<double>.Build.Dense(_I);
            As = Vector<double>.Build.Dense(_J);

                        Zfbx = Vector<double>.Build.Dense(_I);            
            Zfby = Vector<double>.Build.Dense(_I);
                        Zsx = Vector<double>.Build.Dense(_J);
            Zsy = Vector<double>.Build.Dense(_J);

                        sigma_fb = Vector<double>.Build.Dense(_I);
            sigma_s = Vector<double>.Build.Dense(_J);

                        Nju_fb = Vector<double>.Build.Dense(_I);
            Nju_s = Vector<double>.Build.Dense(_J);

                        epsilon_fb = Vector<double>.Build.Dense(_I);
            epsilon_s = Vector<double>.Build.Dense(_J);           
        }


        public void SetSize(double[] _t = null)
        {                                   
                        foreach (var elem in m_BElem)
            {
                int i = elem.Num;

                Zfbx[i] = elem.Z_X;
                Zfby[i] = elem.Z_Y;
                Ab[i] = elem.Area;
                Nju_fb[i] = elem.Nu;
            }

                        foreach (var rod in m_Rods)
            {
                int j = rod.Id;

                Zsx[j] = rod.CG_X;
                Zsy[j] = rod.CG_Y;
                As[j] = rod.As;
                Nju_s[j] = rod.Nu;
            }
        }

                                        public void SetParams(double[] _t = null)
        {
            m_Y_N = (int)_t[0];
            m_X_M = (int)_t[1];
        }

        private void InitElementParams()
        {
            
            int i = 0;
            foreach (BSElement elem in m_BElem)
            {
                sigma_fb[i] = m_Beam.Sigma_Z(N, Mx, My, elem.Z_X, elem.Z_Y);
                                             
                epsilon_fb[i] = 1; 

                Nju_fb[i] = m_BElem[i].Nu;                 i++;
            }

            for (int j = 0; j < m_Rods.Count; j++)
            {                
                sigma_s[j] = 1;

                epsilon_s[j] = 1; 

                Nju_s[j] = m_Rods[j].Nu;             }
        }

                                        private void Calc_D(int _i = -1, int _j = -1)
        {                        
            int i = _i;
            int j = _j;

            double AZ2Eb, AZEb, AEb;
            double Zx = 0;
            double Zy = 0;

            if (i > -1) 
            {
                Zx = Zfbx[i];
                Zy = Zfby[i];
            }
            else if (j > -1)
            {
                Zx = Zsx[j];
                Zy = Zsy[j];
            }
                                                                    
                        if (i > -1)
            {
                AZ2Eb = Ab[i] * Math.Pow(Zx, 2) * m_Fiber.Efb;
                Db[I1, I1] += AZ2Eb * Nju_fb[i];
            }
            if (j > -1)
            {
                Ds[I1, I1] += As[j] * Math.Pow(Zsx[j], 2) * m_Rod.Es * Nju_s[j];
            }
                        if (i > -1)
            {
                AZ2Eb = Ab[i] * Math.Pow(Zy, 2) * m_Fiber.Efb;
                Db[I2, I2] += AZ2Eb * Nju_fb[i];
            }
            if (j > -1)
            {
                Ds[I2, I2] += As[j] * Math.Pow(Zsy[j], 2) * m_Rod.Es * Nju_s[j];
            }
                        if (i > -1)
            {
                Db[I1, I2] += Ab[i] * Zx * Zy * m_Fiber.Efb * Nju_fb[i];
            }
            if (j > -1)
            {
                Ds[I1, I2] += As[j] * Zsx[j] * Zsy[j] * m_Rod.Es * Nju_s[j];
            }
                        if (i > -1)
            {
                AZEb = Ab[i] * Zx * m_Fiber.Efb;
                Db[I1, I3] += AZEb * Nju_fb[i];
            }
            if (j > -1)
            {
                Ds[I1, I3] += As[j] * Zsx[j] * m_Rod.Es * Nju_s[j];
            }
                        if (i > -1)
            {
                AZEb = Ab[i] * Zy * m_Fiber.Efb;
                Db[I2, I3] += AZEb * Nju_fb[i];
            }
            if (j > -1)
            {
                Ds[I2, I3] += As[j] * Zsy[j] * m_Rod.Es * Nju_s[j];
            }
                        if (i > -1)
            {
                AEb = Ab[i] * m_Fiber.Efb;
                Db[I3, I3] += AEb * Nju_fb[i];
            }
            if (j > -1)
            {             
                Ds[I3, I3] += As[j] * m_Rod.Es * Nju_s[j];
            }

            if (i > -1)
            {
                Db[I2, I1] = Db[I1, I2];
                Db[I3, I1] = Db[I1, I3];
                Db[I3, I2] = Db[I2, I3];
            }

            if (j > -1)
            {
                Ds[I2, I1] = Ds[I1, I2];
                Ds[I3, I1] = Ds[I1, I3];
                Ds[I3, I2] = Ds[I2, I3];
            }
        }

                                                        private bool Calc_MxMyN()
        {
            const int I1=0, I2=1, I3=2;
            double[] v_eff = { Mx, My, N};
            
            Vector<double> V_Eff = Vector<double>.Build.Dense(v_eff);

            Vector<double> Xsol  = D.Solve(V_Eff);

            foreach (var _x in Xsol)
            {
                if (!_x.IsFinite())
                    return false;
            }

            double kx, ky;
            
                        (kx, ky, eps_0) = (Xsol[I1], Xsol[I2], Xsol[I3]);
            
            rx = (kx != 0) ? 1 / kx : float.MaxValue;
            ry = (ky != 0) ? 1 / ky : float.MaxValue; 

            Kx = kx;
            Ky = ky;

            return true;
        }

        private void ArraysClear()
        {
            epsilon_fb.Clear();
            epsilon_s.Clear();

            sigma_fb.Clear();
            sigma_s.Clear();

            Nju_fb.Clear();
            Nju_s.Clear();
        }

        private bool CalcResult(out int _res, int _group, ref double Mx_calc, ref double My_calc, ref double N_calc)
        {
            bool doNextIter = true;
            _res = 0;

            double kx = (rx != 0) ? 1 / rx : float.MaxValue;
            double ky = (ry != 0) ? 1 / ry : float.MaxValue;

            Kx = kx;
            Ky = ky;

            ArraysClear();

                        for (int i = 0; i < Zfby.Count; i++) 
            {
                double eps = eps_0 + ky * Zfby[i] + kx * Zfbx[i];

                epsilon_fb[i] = eps;

                double sgm = 0;
                if (DeformDiagram == DeformDiagramType.D2Linear)
                {                 
                    switch (DeformMaterialType)
                    {
                        case DeformMaterialType.Fiber: 
                        case DeformMaterialType.Beton:                        
                            sgm = MatFiber.Eps_StDiagram2L(eps, out _res, _group);
                            break;                        
                    }
                }
                else if (DeformDiagram == DeformDiagramType.D3Linear)
                {                    
                    switch (DeformMaterialType)
                    {
                        case DeformMaterialType.Fiber:
                        case DeformMaterialType.Beton:
                            sgm = MatFiber.Eps_StateDiagram3L(eps, out _res, _group);
                            break;
                    }
                }
                
                sigma_fb[i] = Math.Sign(eps) * sgm;

                double nju_b = sigma_fb[i] / (MatFiber.Eb * eps);

                Nju_fb[i] = nju_b;
            }
           
                        for (int j = 0; j < Zsy.Count; j++)
            {
                double _e = eps_0 + ky * Zsy[j] + kx * Zsx[j];
                epsilon_s[j] = _e;
                
                double sgm = 0;
                if (DeformDiagram == DeformDiagramType.D2Linear)
                {
                    sgm = MatRebar.Eps_StDiagram2L(Math.Abs(_e), out _res, _group);
                }
                else if (DeformDiagram == DeformDiagramType.D3Linear)
                {                    
                    sgm = MatRebar.Eps_StateDiagram3L(Math.Abs(_e), out _res, _group);
                }

                sigma_s[j] = Math.Sign(_e) * sgm;

                double nju_s = sigma_s[j] / (MatRebar.Es * _e);

                Nju_s[j] = nju_s;
            }

            Mx_b_calc = 0;
            My_b_calc = 0;
            N_b_calc = 0;

            for (int i = 0; i < Zfby.Count; i ++)
            {
                double Fi = sigma_fb[i] * Ab[i];
                Mx_b_calc += Fi * Zfbx[i];
                My_b_calc += Fi * Zfby[i];
                N_b_calc += Fi;
            }

            double Mx_s_calc = 0, 
                   My_s_calc = 0, 
                   N_s_calc = 0 ;
            for (int j = 0; j < Zsy.Count; j++)
            {
                double Fj = sigma_s[j] * As[j];
                Mx_s_calc +=  Fj * Zsx[j];
                My_s_calc += Fj * Zsy[j];
                N_s_calc += Fj;
            }

            Mx_calc = Math.Abs(Mx_b_calc + Mx_s_calc);
            My_calc =  Math.Abs(My_b_calc + My_s_calc);
            N_calc = Math.Abs(N_b_calc + N_s_calc);

            if (Math.Abs(Mx - Mx_calc) <= BSHelper.Epsilon &&
                Math.Abs(My - My_calc) <= BSHelper.Epsilon && 
                Math.Abs(N - N_calc) <= BSHelper.Epsilon)
            {
                doNextIter = false;
            }
            
            return doNextIter;
        }

        public static void TestMatrix()
        {
            
            var M = Matrix<double>.Build;
            var m = M.DenseOfArray(new[,] {{ 1.0,  2.0, 1.0},
                               {-2.0, -3.0, 1.0},
                               { 3.0,  5.0, 0.0}});

            
            var v = Vector<double>.Build.Random(500);
            var y = m.Solve(v);

        }
        
        private void AddToResult(string _attr, double _value, int _group = 1)
        {
            var res = (_group == 1) ? m_Res : m_Res2Group;

            if (Math.Abs(_value) < 10e-15 || Math.Abs(_value) > 10e15)
            {
                return;
            }

            try
            {
                res.Add(BSFiberCalculation.DsplN(typeof(BSFiberCalc_Deform), _attr), _value);
            }
            catch (Exception _E)
            {
                MessageBox.Show(_E.Message);
            }
        }


                                                private List<BSElement> CalculationScheme(bool _usemesh = true)
        {            
            List<BSElement> bs = new List<BSElement>();
            
                        (double X0, double Y0) = (CG.X, CG.Y);
                        
            foreach (var t in triCGs)
            {                               
                                double cgX = t.X ;                
                double cgY = t.Y ;

                BSElement bsElement = new BSElement(t.ID, cgX, cgY) 
                { 
                    Area = triAreas[t.ID],
                    A = 1, 
                    B = 1 
                };

                bsElement.E = m_Beam.Mat.Eb;

                bs.Add(bsElement);                                                          
            }

            return bs;
        }

                                        public bool Calculate()
        {            
            if (triAreas.Count > 0)
            {
                m_BElem = CalculationScheme(true);
            }
            else
            {
                m_BElem = CalculationScheme(m_Y_N, m_X_M);
            }

                        int qty_J = m_Beam.RodsQty;
                        int qty_I = m_BElem.Count; 
            
            InitVectors(qty_I, qty_J);
           
            SetSize();

            InitElementParams();
            
            CalculateGroupLSD(1);
            ResultsGroup1();

            CalculateGroupLSD(2);
            ResultsGroup2();

            return true;
        }

                                        private void CalculateGroupLSD(int _group)
        {
            groupLSD = _group;                 
                        int qty_J = m_Beam.RodsQty;
                        int qty_I = m_BElem.Count;
                        double _A_s = m_Beam.AreaS();

            DenseMatrix Zeros3x3 = DenseMatrix.OfArray(zero3x3);

            bool doNextIter = true;
           

            for (int iter = 0; iter < cIters; iter++)
            {
                if (iter == 3)
                    Debug.Assert(true);

                if (!doNextIter)
                    break;
                try
                {
                    D = DenseMatrix.OfArray(zero3x3);
                    Db = new double[I3 + 1, I3 + 1];
                    Ds = new double[I3 + 1, I3 + 1];

                    for (int i = 0; i < qty_I; i++)
                    {
                        Calc_D(i);
                    }

                    if (_A_s > 0)
                    {
                        for (int j = 0; j < qty_J; j++)
                        {
                            Calc_D(-1, j);
                        }
                    }

                    MatrixBuilder<double> M = Matrix<double>.Build;
                    D = M.DenseOfArray(Db);

                    var _Ds = M.DenseOfArray(Ds);
                    D = D.Add(_Ds);

                    if (D.Equals(Zeros3x3))
                    {
                        break;
                    }

                    bool ret = Calc_MxMyN();
                    if (!ret)
                        break;

                    double Mx_calc = 0, My_calc = 0, N_calc = 0;
                    doNextIter = CalcResult(out int res, groupLSD, ref Mx_calc, ref My_calc, ref N_calc);


                                        Mcrc = My_calc;
                    double ky_crc = Ky; 
                    double kx_crc = Kx; 
                    
                }
                catch (Exception ex)
                {
                    var ex_type = ex.GetType().Name;
                    MessageBox.Show($"Исключение: {ex.Message} Тип: {ex_type}  Итерация: {iter}   Метод: {ex.TargetSite} Трассировка стека: {ex.StackTrace}");
                    doNextIter = false;
                }
                finally
                {
                }
            }
        }


                                private void ResultsGroup1()
        {
            e_fb_max = epsilon_fb.AbsoluteMaximum();

            AddToResult("eps_0", eps_0);
            AddToResult("rx", rx);
            AddToResult("Kx", Kx);
            AddToResult("ry", ry);
            AddToResult("Ky", Ky);
            AddToResult("e_fb_max", e_fb_max);

            bool res_fb = e_fb_max <= m_Fiber.Eps_fb_ult;
            if (res_fb)
                Msg.Add(string.Format("Проверка сечения по фибробетону пройдена. e_fb_max={0} <= e_fb_ult={1} ", Math.Round(e_fb_max, 6), m_Fiber.Eps_fb_ult));
            else
                Msg.Add(string.Format("Не пройдена проверка сечения по фибробетону. e_fb_max={0} <= e_fb_ult={1} ", Math.Round(e_fb_max, 6), m_Fiber.Eps_fb_ult));

            if (epsilon_s != null && epsilon_s.Count > 0)
            {
                double e_s_max = epsilon_s.AbsoluteMaximum();
                double e_s_ult = m_Rod.Eps_s_ult(DeformDiagram);
                bool res_s = e_s_max <= e_s_ult;

                if (res_s)
                    Msg.Add(string.Format("Проверка сечения по арматуре пройдена. e_s_max={0} <= e_s_ult={1} ", Math.Round(e_s_max, 6), e_s_ult));
                else
                    Msg.Add(string.Format("Не пройдена проверка сечения по арматуре. e_s_max={0} <= e_s_ult={1}", Math.Round(e_s_max, 6), e_s_ult));
            }
        }

                                private void ResultsGroup2()
        {            
            AddToResult("Mcrc", Mcrc, groupLSD);
            AddToResult("ry", ry, groupLSD);
        }

                                        private List<BSElement> CalculationScheme(int _N = 10, int _M = 1)
        {            
            BSBeam_Rect beam = (BSBeam_Rect) m_Beam;

            (double X0, double Y0) = beam.CG();                        
            double dH = beam.h / _N;
            double dB =  beam.b / _M;            

            double elX = 0, 
                   elY = 0;

            List <BSElement> bs = new List<BSElement>();
            int num = 0;
            for (int idx = 0; idx < _M; idx++) 
            {                
                for (int idy = 0; idy < _N; idy++)
                {
                                        double cgX = elX + dB / 2.0 - X0;
                    double cgY = elY + dH / 2.0 - Y0;

                    BSElement bsElement = new BSElement(num, cgX, cgY) {A = dB, B = dH };
                    bsElement.E = beam.Mat.Eb; 
                    bs.Add(bsElement);

                    elY += dH;
                    num ++;
                }

                elX += dB;
            }

            return  bs;
        }

        public Dictionary<string, double> GeomParams()
        {             
            return null;
        }

              
         
    }
}

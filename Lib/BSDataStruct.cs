using System;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace BSFiberConcrete
{
                public class FormParams
    {
        public int ID { get; set; }
        public double Length { get; set; }
        public double LengthCoef { get; set; }
        public string BetonType { get; set; }
        public string Fib_i { get; set; }
        public string Bft3n { get; set; }
        public string Bfn { get; set; }
        public string Bftn { get; set; }
        public string Eb { get; set; }
        public string Efbt { get; set; }
        public string Rs { get; set; }
        public string Rsw { get; set; }
        public double Area_s { get; set; }
        public double Area1_s { get; set; }
        public double a_s { get; set; }
        public double a1_s { get; set; }
    }

                public class NdmCrc
    {
        public int Id { get; set; }
        public double fi1 { get; set; }
        public double fi2 { get; set; }
        public double fi3 { get; set; }
        public double mu_fv { get; set; }
        public double psi_s { get; set; }
        public double kf { get;  set; }

                public void InitFi2(string _RebarType)
        {
            if (_RebarType == "A240")            
                fi2 = 0.8;
            else
                fi2 = 0.5;
        }

                public void InitFi3(double _N)
        {
            if (_N < 0)
                fi3 = 1.0;             else
                fi3 = 0.5;
        }
    }


                public class NDMSetup
    {
        public int Id { get; set; }
        public int Iters { get; set; }
        public int N { get; set; }
        public int M { get; set; }
        public double MinAngle { get; set; }
        public double MaxArea { get; set; }
        public int BetonTypeId { get; set; }
        public bool UseRebar { get; set; }
        public string RebarType { get; set; }
    }


                public class Units
    {
        public static string R { get; set; }
        public static string E { get; set; }
        public static string L { get; set; }
        public static string D { get; set; }
        public static string A { get; set; }

    }

                        public class Beton
    {
        public  int Id { get; set; }
                                public  string BT { get; set; }              
                                public  double Rbn { get; set; }
                                public double Rb { get; set; }

                                public double Rbt { get; set; }
                                public double Rbtn { get; set; }

                                public  double Eb { get; set; }
                                public double B { get; set; }
        public double BetonType { get; set; }
    }


                public class FiberBft
    {  
                public string ID { get; set; }        
                public double Rfbt { get; set; }
                public double Rfbtn { get; set; }
    }

                public class BetonType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Omega { get; set; }        
        public double Eps_fb2 { get; set; }
    }

                public class Fiber : ICloneable
    {
                public double e_tot { get; set; }
                                public double Ef { get; set; }
                                public double Eb { get; set; }
        public double mu_fv { get; set; }
        public double omega { get; set; }

                                public double Efb => Eb * (1 - mu_fv) + Ef * mu_fv;

                                public double Efib { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

                public class Rebar : ICloneable
    {
        public string ID { get; set; }

                public double Rsn { get; set; }
                public double Rs { get; set; }

                public double Rsc { get; set; }
                public double Es { get; set; }
       
                public double As { get; set; }
                public double As1 { get; set; }

                                public double Esw_X { get; internal set; }
                public double Rsw_X { get; set; }
        public double Asw_X { get; set; }
                                public double Sw_X { get; set; }

                                public double Sw_Y { get; set; }
        public double Esw_Y { get; set; }
        public double Rsw_Y { get; set; }
        public double Asw_Y { get; set; }

        public TypeYieldStress typeYieldStress { get; set; }
        public double k_s { get; set; }
        public double ls { get; set; }
        
                public double a { get; set; }
                public double a1 { get; set; }
        public double Epsilon_s => (Es > 0 ) ? Rs / Es : 0;

        public string DiagramType
        { 
            get
            {
                string res = BSHelper.TwoLineDiagram;
                if (typeYieldStress == TypeYieldStress.Physical)
                    res = BSHelper.TwoLineDiagram;
                if (typeYieldStress == TypeYieldStress.Offset)
                    res = BSHelper.ThreeLineDiagram;
                return res;
            }
        }

                                public double Epsilon_s_ult
        {
            get
            {
                double res = 0;

                                if (typeYieldStress == TypeYieldStress.Physical)
                {
                    res = 0.025;                 }
                else if (typeYieldStress == TypeYieldStress.Offset)                 {
                    res = 0.015;
                }

                return res;
            }
        }
        
        public double Dzeta_R(double omega, double eps_fb2) => omega / (1 + Epsilon_s / eps_fb2);

        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }

                public class Beton2
    {
        public string Cls_b { get; set; }
        public double Rb_ser { get; set; }
        public double Rb { get; set; }
        public double Eb { get; set; }
        public double eps_b1 { get; set; }
        public double eps_b1_red { get; set; }
        public double eps_b2 { get; set; }
    }

                public class Rod2
    {
        public string Cls_s { get; set; }
        public double Rs_ser { get; set; }
        public double Rs { get; set; }
        public double Rsc { get; set; }
        public double Es { get; set; }
        public double eps_s0 { get; set; }
        public double eps_s2 { get; set; }
    }


                public class BSFiberParams
    {
        public Units Units { get; set; }
        public Fiber Fiber { get; set; }
        public Rebar Rebar { get; set; }
        public Beton2 Beton2 { get; set; }
        public Rod2 Rod2 { get; set; }
    }


                public class BSFiberBeton
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }

        public double Rfbt3n { get; set; }

        public double Rfbt3_ser => Rfbt3n;

        public double Rfbt2n { get; set; }

        public double Rfbt2_ser => Rfbt2n;

                                public double Rfbt3 { get; set; }

        public double Rfbt2 { get; set; }

                                public double Rfbn { get; set; }
    }

    public class StrengthFactors
    {
        public int Id { get; set; }
        public double Yb { get; set; }
        public double Yft { get; set; }
        public double Yb1 { get; set; }
        public double Yb2 { get; set; }
        public double Yb3 { get; set; }
        public double Yb5 { get; set; }
    }


    public class Elements
    {
        public int Id { get; set; }
        public string BT { get; set; }
        public double Rfbt3n { get; set; }
        public double Rfbt2n { get; set; }

        public double Rfbn { get; set; }
        public double Yb { get; set; }
        public double Yft { get; set; }
        public double Yb1 { get; set; }
        public double Yb2 { get; set; }
        public double Yb3 { get; set; }
        public double Yb5 { get; set; }
        public string i_B { get; set; }
        public double Rfbt3 { get; set; }
        public double Rfbt2 { get; set; }
    }



                public class FiberConcreteClass
    {
        public int ID { get; set; }
        public string Bft { get; set; }
        public double Rfbt_n { get; set; }
    }

                public class Efforts
    {
        public int Id { get; set; }
        public double Mx { get; set; }
        public double My { get; set; }
        public double Mz { get; set; }
        public double N { get; set; }
        public double Qx { get; set; }
        public double Qy { get; set; }       
    }

                public class Coefficients
    {
        public int ID { get; set; }
        public string Y { get; set; }
        public double Val { get; set; }
        public string Name { get; set; }
        public string Descr { get; set; }
    }


    public class RFiber
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Rfser { get; set; }
        public double Rf { get; set; }
        public double G1 { get; set; }
        public double G2 { get; set; }
        public double Ef { get; set; }
                                public double Hita_f { get; set; }
                                public double Gamma_fb1 { get; set; }
                                public int IndexForGeometry { get; set; }
    }



    public class FiberType
    {
        public int ID { get; set; }
                                public double Hita_f { get; set; }
        public string Name { get; set; }

                                public double Gamma_fb1 { get; set; }

    }

    public class FiberKind
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Rfser { get; set; }
        public double Rf { get; set; }
        public double G1 { get; set; }
        public double G2 { get; set; }
        public double Ef { get; set; }

                                public int TypeID { get; set; }
                                public int IndexForGeometry { get; set; }
    }



    public class FiberGeometry
    {
        public int ID { get; set; }
                                public int GeometryIndex { get; set; }
                                public double Square { get; set; }
                                public double Diameter{ get; set; }
                                public int IndexForLength { get; set; }
    }



    public class FiberLength
    {
        public int ID { get; set; }
                                public int LenghtIndex { get; set; }
        public double Length { get; set; }
    }


    public class RFibKor
    {
        public int ID { get; set; }
        public double A { get; set; }
        public double B { get; set; }
        public double C { get; set; }
        public double D { get; set; }
        public double E { get; set; }
        public double F { get; set; }
        public double G { get; set; }
        public double H { get; set; }
        public double I { get; set; }
    }


                public class Fiber_K
    {
        public int ID { get; set; }
        public double HL { get; set; }
        public double BL_05 { get; set; }
        public double BL_1 { get; set; }
        public double BL_2 { get; set; }
        public double BL_3 { get; set; }
        public double BL_5 { get; set; }
        public double BL_10 { get; set; }
        public double BL_20 { get; set; }
        public double BL_21 { get; set; }
    }


                public class FaF
    {
                                public  int Num { get; set; }
                                public  double aF { get; set; }
                                public  double F { get; set; }

        public FaF()
        {
        }
    }
 
                public class FibLab
    {
                                public string Id { get; set; }
                                public double Fel { get; set; }
                                public double F05 { get; set; }
                                public double F25 { get; set; }

                                public double L { get; set; }
                                public double B { get; set; }
                                public double H_sp { get; set; }

        public FibLab()
        {
        }
    }


                public class Deflection_f_aF
    {
                                public string Id  { get; set; }

                                public int Num { get; set; }

                                public double aF { get; set; }

                                public double f { get; set; }
    }

                public class LocalStress
    {
                                public int Id { get; set; }

                                public string VarName { get; set; }

                                public string VarDescr { get; set; }

                                public double Value { get; set; }

                                public int Type { get; set; }
    }

                public class EpsilonFromAirHumidity
    {
        public int Id { get; set; }
                                public string AirHumidityStr { get; set; }
                                public double FirstBorder { get; set; }
                                public double SecondBorder { get; set; }
        public double Eps_b0 { get; set; }
        public double Eps_b2 { get; set; }
        public double Eps_bt0 { get; set; }
        public double Eps_bt2 { get; set; }
    }



                public class InitBeamSectionGeometry
    {
                                public BeamSection SectionTypeNum { get; set; }
                                public string SectionTypeStr { get; set; }

        #region Габаритные размеры сечения. 
        

        public double? b { get; set; }
        public double? h { get; set; }
        public double? bf { get; set; }
        public double? hf { get; set; }
        public double? bw { get; set; }
        public double? hw { get; set; }
        public double? b1f { get; set; }
        public double? h1f { get; set; }
        public double? r1 { get; set; }
        public double? r2 { get; set; }
        #endregion
    }



                public class RebarDiameters
    { 
        public int ID { get; set; }
    
        public string TypeRebar { get; set; }

        public double Diameter { get; set; }    

        public double Square { get; set; }
    }


                public class FiberClass
    {
        public int ID { get; set; }

        public string Name { get; set; }
    }


}


using System;
using System.Security.Policy;
namespace BSFiberConcrete
{
                public enum RebarLTType
    {
        Longitudinal = 0,
        Transverse = 1
    }
                public class BSRod
    {
                                public int Id { get; set; }
                                public double CG_X { get; set; }
                                public double CG_Y { get; set; }
                                public string Dnom { get; set; }
                                public double D { get; set; }
                                public BeamSection SectionType { get; set; }
                                public double As { get => Math.PI * Math.Pow(D, 2) / 4; }
                                public double a { get; set; }
                                public double a1 { get; set; }
                                public double Nu { get; set; }
                                public RebarLTType LTType { get; set; }
       
                                public BSMatRod MatRod { get; set; }
    }
    public class NdmSection
    {
                                public string Num { get; set; }
                                public int N { get; set; }
                                public double X { get; set; }
                                public double Y { get; set; }
    }
}

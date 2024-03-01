using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFiberConcrete
{
    /// <summary>
    /// Единицы измерения
    /// </summary>
    public class Units
    {
        public static string R { get; set; }
        public static string E { get; set; }
        public static string L { get; set; }
        public static string D { get; set; }
        public static string A { get; set; }

    }


    /// <summary>
    /// Фибробетон, параметры
    /// </summary>
    public class Fiber
    {
        public double e0 { get; set; }
        public double Ef { get; set; }
        public double Eb { get; set; }
        public double mu_fv { get; set; }
        public double omega { get; set; }
    }

    /// <summary>
    /// Арматура, параметры
    /// </summary>
    public class Rebar
    {
        public string ID { get; set; }
        public double Rs { get; set; }
        public double Rsc { get; set;}
        public double Es { get; set; }
        public double As { get; set; }    
        public double Rsw { get; set; }    
        public double Asw { get; set; }
        public double s_w { get; set; }
    }

    /// <summary>
    /// Таблица характеристики бетонов (СП 63.13)
    /// </summary>
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

    /// <summary>
    /// Характеристики арматуры СП 63.13
    /// </summary>
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


    /// <summary>
    /// Параметры бетонов и арматуры, считываем из json 
    /// </summary>
    public class BSFiberParams
    {
        public Units Units { get; set; }
        public Fiber Fiber { get; set; }
        public Rebar Rebar { get; set; }
        public Beton2 Beton2 { get; set; }
        public Rod2 Rod2 { get; set; }        
    }


    class BSFiberBeton
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        /// <summary>
        /// значения сопротивления сталефибробетона растяжению при классе сталефибробетона по остаточной прочности на растяжение, МПа
        /// </summary>
        public double Rfbt3 { get; set; }
        /// <summary>
        /// Сжатие осевое
        /// </summary>
        public double Rfbn { get; set; }
    }


    public class Elements
    {
        public double Rfbt3n { get; set; }
        public double Rfbn { get; set; }
        public double Yb { get; set; }
        public double Yft { get; set; }
        public double Yb1 { get; set; }
        public double Yb2 { get; set; }
        public double Yb3 { get; set; }
        public double Yb5 { get; set; }
        public int B { get; set; }
    }


    /// <summary>
    /// Усилия
    /// </summary>
    public class Efforts
    {
        public int Id { get; set; }
        public double Mx { get; set; }
        public double My { get; set; }
        public double N { get; set; }
        public double Q { get; set; }
        public double Ml { get; set; }
        public double eN { get; set; }                
    }


    internal class BSFiberLib
    {
        public static List<BSFiberBeton> betonList = new List<BSFiberBeton>
        {
            new BSFiberBeton{Id = 2, Name = "B2.5", Type = 4, Rfbt3 = 0, Rfbn = 0 },
            new BSFiberBeton{Id = 3, Name = "B3.5", Type = 4, Rfbt3 = 0, Rfbn = 2.7 },
            new BSFiberBeton{Id = 5, Name = "B5", Type = 4, Rfbt3 = 0, Rfbn = 3.5 },            
            new BSFiberBeton{Id = 7, Name = "B7.5", Type = 4, Rfbt3 = 0, Rfbn = 5.5 },
            new BSFiberBeton{Id = 10, Name = "B1i", Type = 2, Rfbt3 = 1.0, Rfbn = 7.5 },
            new BSFiberBeton{Id = 12, Name = "B1.25i",Type = 2, Rfbt3 = 1.25, Rfbn = 9.5},
            new BSFiberBeton{Id = 15, Name = "B1.5i", Type = 2, Rfbt3 = 1.5, Rfbn = 11 },
            new BSFiberBeton{Id = 20, Name = "B2i", Type = 2, Rfbt3 = 2.0, Rfbn = 15 },
            new BSFiberBeton{Id = 25, Name = "B2.5i", Type = 2, Rfbt3 = 2.5, Rfbn = 18.5 },
            new BSFiberBeton{Id = 30, Name = "B3i", Type = 2, Rfbt3 = 3.0, Rfbn = 22.0 },
            new BSFiberBeton{Id = 35, Name = "B3.5i", Type = 2, Rfbt3 = 3.5, Rfbn = 25.5 },
            new BSFiberBeton{Id = 40, Name = "B4i", Type = 2, Rfbt3 = 4.0, Rfbn = 29.0 },
            new BSFiberBeton{Id = 45, Name = "B4.5i", Type = 2, Rfbt3 = 4.5, Rfbn = 32.0 },
            new BSFiberBeton{Id = 50, Name = "B5i", Type = 2, Rfbt3 = 5.0, Rfbn = 36.0 },
            new BSFiberBeton{Id = 55, Name = "B5.5i", Type = 2 , Rfbt3 = 5.5, Rfbn = 39.5},
            new BSFiberBeton{Id = 60, Name = "B6i", Type = 2, Rfbt3 = 6.0, Rfbn = 43 }
        };

        public static Elements PhysElements = new Elements { 
            Rfbt3n = 30.58, Yb = 1.3, Rfbn = 224.0, Yft = 1.3, Yb1 = 0.9, Yb2 = 0.9, Yb3 = 0.9, Yb5 = 1,  B = 30 
        };

    }
}

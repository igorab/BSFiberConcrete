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
        public double Rsc { get; set; }
        public double Es { get; set; }
        public double As { get; set; }
        public double Rsw { get; set; }
        public double Asw { get; set; }
        public double s_w { get; set; }
        public double ls { get; set; }
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


    /// <summary>
    /// Таблица 2 СП360
    /// </summary>
    class BSFiberBeton
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }

        public double Rfbt3n { get; set; }

        public double Rfbt3_ser => Rfbt3n;

        public double Rfbt2n { get; set; }

        public double Rfbt2_ser => Rfbt2n;

        /// <summary>
        /// значения сопротивления сталефибробетона растяжению при классе сталефибробетона по остаточной прочности на растяжение, МПа
        /// </summary>
        public double Rfbt3 { get; set; }

        public double Rfbt2 { get; set; }

        /// <summary>
        /// Сжатие осевое
        /// </summary>
        public double Rfbn { get; set; }
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
        public int iB { get; set; }
        public double Rfbt3 { get; set; }
        public double Rfbt2 { get; set; }
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

    /// <summary>
    /// Коэффициенты
    /// </summary>
    public class Coefficients
    {
        public int ID { get; set; }
        public string Y { get; set; }
        public double Val { get; set; }
        public string Name { get; set; }
        public string Descr { get; set; }
    }

}

using System;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

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
    /// Данные из таблицы Beton
    /// </summary>
    public class Beton
    {
        public  int Id { get; set; }
        public  string BT { get; set; }
        public  double Rb { get; set; }
        public  double Rbt { get; set; }
        public  double Eb { get; set; }
        public double B { get; set; }
    }


    /// <summary>
    /// Данные из таблицы FiberFbt - класс фибробетона по прочности на растяжение
    /// </summary>
    public class FiberBft
    {  
        // Класс
        public string ID { get; set; }        
        // Расчетное
        public double Rfbt { get; set; }
        // Нормативное
        public double Rfbtn { get; set; }
    }

    /// <summary>
    /// Данные из таблицы BetonType
    /// </summary>
    public class BetonType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Omega { get; set; }        
        public double Eps_fb2 { get; set; }
    }

    /// <summary>
    /// Фибробетон, параметры
    /// </summary>
    public class Fiber : ICloneable
    {
        //TODO refactoring полный эксцентриситет приложения силы
        public double e_tot { get; set; }
        /// <summary>
        /// начальный модуль упругости стальной фибры
        /// </summary>
        public double Ef { get; set; }
        /// <summary>
        /// начальный модуль упругости бетона-матрицы
        /// </summary>
        public double Eb { get; set; }
        public double mu_fv { get; set; }
        public double omega { get; set; }
        public double Efb => Eb * (1 - mu_fv) + Ef * mu_fv;

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    /// <summary>
    /// Арматура, параметры
    /// </summary>
    public class Rebar : ICloneable
    {
        public string ID { get; set; }

        //Расчетное сопротивление арматуры
        public double Rs { get; set; }
        //Расчетное сопротивление арматуры сжатию
        public double Rsc { get; set; }
        //Значения модуля упругости арматуры
        public double Es { get; set; }
        // Площадь растянутой арматуры
        public double As { get; set; }
        // Площадь сжатой арматуры
        public double A1s { get; set; }
        public double Rsw { get; set; }
        public double Asw { get; set; }
        public double s_w { get; set; }
        public double k_s { get; set; }
        public double ls { get; set; }
        
        //Растояние до цента тяжести растянутой арматуры см
        public double a { get; set; }
        //Растояние до цента тяжести сжатой арматуры см
        public double a1 { get; set; }
        public double Epsilon_s => (Es > 0 ) ? Rs / Es : 0;

        public double Dzeta_R(double omega, double eps_fb2) => omega / (1 + Epsilon_s / eps_fb2);

        public object Clone()
        {
            return this.MemberwiseClone();
        }

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
        public string i_B { get; set; }
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

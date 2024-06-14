using System;

namespace BSFiberConcrete
{
    /// <summary>
    /// Тип арматуры : продольная / поперечная
    /// </summary>
    public enum RebarLTType
    {
        Longitudinal = 0,
        Transverse = 1
    }


    /// <summary>
    /// Арматурный стержень
    /// </summary>
    public class BSRod
    {
        /// <summary>
        /// Номер стержня
        /// </summary>
        public int Id { get; set; } = 0;

        /// <summary>
        /// Координата X Ц.Т.
        /// </summary>
        public double CG_X { get; set; }

        /// <summary>
        /// Координата Y Ц.Т.
        /// </summary>
        public double CG_Y { get; set; }
       
        /// <summary>
        /// Диаметр стержня, мм
        /// </summary>
        public double D { get; set; }

        /// <summary>
        /// Площадь стержня
        /// </summary>
        public double As { get => Math.PI * Math.Pow(D, 2) / 4; }

        /// <summary>
        /// Расстояние до ц.т. растянутой арматуры
        /// </summary>
        public double a { get; set; }

        /// <summary>
        /// Расстояние до ц.т. сжатой арматуры
        /// </summary>
        public double a1 { get; set; }

        /// <summary>
        /// Коэффициент упругости
        /// </summary>
        public double Nu { get; set; }

        /// <summary>
        /// Тип продольная/поперечная
        /// </summary>
        public RebarLTType LTType { get; set; }

        /// <summary>
        /// Материал
        /// </summary>
        public BSMatRod MatRod { get; set; }
    }
}

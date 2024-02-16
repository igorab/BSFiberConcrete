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
    /// арматурный стержень
    /// </summary>
    public class BSRod
    {
        // Номер стержня
        public int Num { get; set; } = 0;

        // Координаты Ц.Т.
        public double Z_X { get; set; }
        public double Z_Y { get; set; }

        // напряжение в стержне
        public double Sigma_s { get; set; }

        // Диаметр стержня, мм
        public double D { get; set; }

        // Площадь стержня
        public double As { get => Math.PI * Math.Pow(D, 2) / 4; }

        // Расстояние до ц.т. растянутой арматуры
        public double a { get; set; }

        // Расстояние до ц.т. сжатой арматуры
        public double a1 { get; set; }

        // Коэффициент упругости
        public double Nu { get; set; }

        public RebarLTType LTType { get; set; }

        public BSMatRod MatRod { get; set; }
    }
}

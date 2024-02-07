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

    // арматурный стержень
    public class BSRod
    {
        // номер стержня
        public double Num { get; set; } = 0;

        // Координаты Ц.Т.
        public double X { get; set; }
        public double Y { get; set; }

        // Диаметр стержня
        public double D { get; set; }

        public RebarLTType LTType { get; set; }

        public BSMatRod MatRod { get; set; }
    }
}

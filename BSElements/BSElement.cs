namespace BSFiberConcrete
{
    /// <summary>
    /// Элемент сечения балки
    /// </summary>
    public class BSElement
    {
        // Номер
        public int Num;

        /// <summary>
        /// Координата X центра тяжести
        /// </summary>
        public double Z_X { get; }

        /// <summary>
        /// Координата Y ц.т.
        /// </summary>
        public double Z_Y { get; }

        /// <summary>
        /// Площадь элемента
        /// </summary>
        public double Area { get; set; }

        /// <summary>
        /// Границы
        /// </summary>
        public double A { get; set; }
        public double B { get; set; }

        /// <summary>
        /// Border Area
        /// </summary>
        public double Ab { get => AreaAB(); }

        // напряжение на уровне Ц.Т.
        public double Sigma { get; set; }

        // модуль упругости
        public double E { get; set; }

        // относительная деформация
        public double Epsilon { get; set; }

        public double Nu { get => calcNu(); }

        private double AreaAB() => A * B;

        public double calcNu() => Epsilon != 0 ? Sigma / (E * Epsilon) : 1;


        public BSElement(int _N, double _X, double _Y)
        {
            Num = _N;
            Z_X = _X;
            Z_Y = _Y;
        }
    }
}

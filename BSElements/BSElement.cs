namespace BSFiberConcrete
{
                public class BSElement
    {
                public int Num;
                                public double Z_X { get; }
                                public double Z_Y { get; }
                                public double Area { get; set; }
                                public double A { get; set; }
        public double B { get; set; }
                                public double Ab { get => AreaAB(); }
                public double Sigma { get; set; }
                public double E { get; set; }
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

namespace BSFiberConcrete
{
                        public interface INonlinear
    {
        double Eps_StDiagram2L(double _e, out int _res, int _group);
        double Eps_StateDiagram3L(double e_s, out int _res, int _group);
    }
}

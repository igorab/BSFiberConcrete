namespace BSFiberConcrete
{
    /// <summary>
    /// Расчет по нелинейной деформационной модели
    /// Связь деформации с напряжением реализуется через
    /// диаграмму состаояния
    /// </summary>
    public interface INonlinear
    {
        double Eps_StDiagram2L(double _e, out int _res);
        double Eps_StateDiagram3L(double e_s, out int _res);
    }
}

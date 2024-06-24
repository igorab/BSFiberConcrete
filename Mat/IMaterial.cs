namespace BSFiberConcrete
{
    public interface IMaterial
    {
        /// <summary>
        /// Наименование материала
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Модуль упругости
        /// </summary>
        double E_young { get; }
    }
}

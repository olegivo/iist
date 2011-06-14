namespace EmulationClient.Emulation
{
    /// <summary>
    /// Контролируемый параметр
    /// </summary>
    public interface IControlledParameter
    {
        /// <summary>
        /// Значение контролируемого параметра
        /// </summary>
        double OutputValue { get; }

        /// <summary>
        /// Обновить значение (используется функция пересчёта входных параметров в выходной)
        /// </summary>
        void Refresh();
    }
}
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
        double GetOutputValue();

        /// <summary>
        /// Обновить значение (используется функция пересчёта входных параметров в выходной)
        /// </summary>
        void Refresh();
    }
}
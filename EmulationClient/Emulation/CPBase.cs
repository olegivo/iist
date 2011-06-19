namespace EmulationClient.Emulation
{
    /// <summary>
    /// Базовая реализация контролируемого параметра
    /// </summary>
    public abstract class CPBase : IControlledParameter
    {
        /// <summary>
        /// Выходное значение контролируемого параметра. Используется для установки и чтения
        /// </summary>
        protected double _outputValue;

        /// <summary>
        /// Получить актуальное значение контролируемого параметра
        /// </summary>
        public double GetOutputValue()
        {
            Refresh();
            return _outputValue;
        }

        /// <summary>
        /// Обновить значение (используется функция пересчёта входных параметров в выходной)
        /// </summary>
        public abstract void Refresh();
    }
}
namespace Oleg_ivo.Plc
{
    /// <summary>
    /// Опции загрузки уровня конфигурации
    /// </summary>
    public class MeasurementSystemLevelLoadOptions
    {
        /// <summary>
        /// Вычислить текущую конфигурацию
        /// </summary>
        public bool ComputeCurrentConfiguration { get; set; }

        /// <summary>
        /// Загрузить сохранённую конфигурацию
        /// </summary>
        public bool LoadSavedConfiguration { get; set; }

        /// <summary>
        /// Необходимость каким-либо образом инициализировать конфигурацию
        /// </summary>
        public bool IsNeedComputeOrLoad
        {
            get { return ComputeCurrentConfiguration || LoadSavedConfiguration; }
        }
    }
}
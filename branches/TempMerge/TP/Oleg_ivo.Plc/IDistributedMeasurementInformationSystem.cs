namespace Oleg_ivo.Plc
{
    public interface IDistributedMeasurementInformationSystem
    {
        ///<summary>
        /// Диспетчер ПЛК системы
        ///</summary>
        IPlcManager PlcManager { get; }

        ///<summary>
        /// Настройки распределённой системы
        ///</summary>
        IDistributedSystemSettings Settings { get; }

        ///<summary>
        /// Построить конфигурацию системы
        ///</summary>
        void BuildSystemConfiguration();

        ///<summary>
        ///
        ///</summary>
        void ShowConfiguration();
    }
}
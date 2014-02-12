using Oleg_ivo.Plc.Devices.Contollers;
using Oleg_ivo.Plc.FieldBus;

namespace Oleg_ivo.Plc
{
    /// <summary>
    /// Опции загрузки полевой шины
    /// </summary>
    public class FieldBusLoadOptions
    {
        /// <summary>
        /// Опции загрузки полевой шины
        /// </summary>
        /// <param name="fieldBusType"></param>
        public FieldBusLoadOptions(FieldBusType fieldBusType)
        {
            FieldBusType = fieldBusType;
        }

        /// <summary>
        /// Тип полевой шины
        /// </summary>
        public FieldBusType FieldBusType { get; private set; }

        /// <summary>
        /// Опции загрузки узлов полевых шин 
        /// </summary>
        public MeasurementSystemLevelLoadOptions FieldNodesLevel { get; set; }

        /// <summary>
        /// Опции загрузки физических каналов
        /// </summary>
        public MeasurementSystemLevelLoadOptions PhysicalChannelsLevel { get; set; }

        /// <summary>
        /// Опции загрузки узлов логических каналов
        /// </summary>
        public MeasurementSystemLevelLoadOptions LogicalChannelsLevel { get; set; }

        /// <summary>
        /// Коллекция адресов ПЛК на шине
        /// </summary>
        public FieldBusNodeAddressCollection FieldBusNodeAddresses { get; set; }
 
    }
}
using Oleg_ivo.Plc.Devices.Contollers;
using Oleg_ivo.Plc.FieldBus;

namespace Oleg_ivo.Plc.Ports
{
    ///<summary>
    /// Параметры подключения к полевой шине
    ///</summary>
    public abstract class FieldBusPortParameters
    {
        private readonly FieldBusType _fieldBusType;

        ///<summary>
        ///Параметры подключения к полевой шине
        ///</summary>
        ///<param name="fieldBusType"></param>
        protected FieldBusPortParameters(FieldBusType fieldBusType)
        {
            _fieldBusType = fieldBusType;
        }

        ///<summary>
        /// Диапазон физических адресов шины
        ///</summary>
        public FieldBusNodeAddressCollection PhysicalAddressRange//todo:rename OR оно нам надо?
        {
            get { return DistributedMeasurementInformationSystemBase.Instance.Settings.FieldBusLoadOptions[FieldBusType].FieldBusNodeAddresses; }
        }

        ///<summary>
        /// Тип полевой шины
        ///</summary>
        public FieldBusType FieldBusType
        {
            get { return _fieldBusType; }
        }
    }
}
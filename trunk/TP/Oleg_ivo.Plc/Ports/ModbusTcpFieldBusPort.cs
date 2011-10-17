using Oleg_ivo.Plc.FieldBus;

namespace Oleg_ivo.Plc.Ports
{
    /// <summary>
    /// Порт подключения к полевой шине, поддерживающий протокол Modbus/TCP
    /// </summary>
    public class ModbusTcpFieldBusPort: ModbusIpFieldBusPort
    {

        #region properties

        #endregion

        #region constructors
        ///<summary>
        ///
        ///</summary>
        public ModbusTcpFieldBusPort(ModbusIpAccessor modbusIpAccessor)
            : base(modbusIpAccessor)
        {
            
        }

        #endregion

        #region overrides

        #endregion

        #region methods

        #endregion

        #region static

        #endregion

        #region events

        #endregion


    }
}
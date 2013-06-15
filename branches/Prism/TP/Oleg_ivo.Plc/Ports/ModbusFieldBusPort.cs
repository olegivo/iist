using System;
using System.Collections.Generic;
using System.ComponentModel;
using Oleg_ivo.Plc.Devices.Contollers;
using Oleg_ivo.Plc.FieldBus;

namespace Oleg_ivo.Plc.Ports
{
    ///<summary>
    /// Порт подключения к полевой шине, поддерживающий одну из разновидностей протокола Modbus
    ///</summary>
    public abstract class ModbusFieldBusPort : Component
    {

        #region fields

        ///<summary>
        /// Минимальное количество миллисекунд для ответа
        ///</summary>
        protected int _minimumWaitToRetryMilliseconds;

        ///<summary>
        /// Минимальное количество попыток для ответа
        ///</summary>
        protected int _minimumRetries;

        ///<summary>
        /// Диапазон адресов на шине
        ///</summary>
        protected readonly List<object> _addressRange;

        #endregion

        #region properties

        ///<summary>
        /// 
        ///</summary>
        protected ModbusAccessor ModbusAccessor { get; private set; }

        ///<summary>
        /// Тип полевой шины
        ///</summary>
        public abstract FieldBusType FieldBusType { get; }

        #endregion

        #region constructors

        ///<summary>
        ///
        ///</summary>
        protected ModbusFieldBusPort() : this(null, null)
        {
        }

        ///<summary>
        ///
        ///</summary>
        ///<param name="modbusAccessor"></param>
        protected ModbusFieldBusPort(ModbusAccessor modbusAccessor) : this(modbusAccessor, null)
        {
        }

        ///<summary>
        ///
        ///</summary>
        ///<param name="addressRange"></param>
        protected ModbusFieldBusPort(List<object> addressRange) : this(null, addressRange)
        {
        }

        ///<summary>
        ///
        ///</summary>
        ///<param name="modbusMaster"></param>
        ///<param name="addressRange"></param>
        protected ModbusFieldBusPort(ModbusAccessor modbusMaster, List<object> addressRange)
        {
            ModbusAccessor = modbusMaster;

            _addressRange = addressRange;

            _minimumRetries = 3;
            _minimumWaitToRetryMilliseconds = 20;
        }

        #endregion

        ///<summary>
        /// Имя порта
        ///</summary>
        public string PortName
        {
            get { return ModbusAccessor.PortName; }
        }

        ///<summary>
        /// Получить диапазон адресов для данного порта
        ///</summary>
        ///<returns></returns>
        ///<exception cref="NotImplementedException"></exception>
        protected abstract FieldBusNodeAddress[] GetPLCAddressRange();

        #region overrides

        ///<summary>
        ///Returns a <see cref="T:System.String"></see> containing the name of the <see cref="T:System.ComponentModel.Component"></see>, if any. This method should not be overridden.
        ///</summary>
        ///
        ///<returns>
        ///A <see cref="T:System.String"></see> containing the name of the <see cref="T:System.ComponentModel.Component"></see>, if any, or <see langword="null"/> if the <see cref="T:System.ComponentModel.Component"></see> is unnamed.
        ///</returns>
        ///
        public override string ToString()
        {
            return PortName;
        }

        #endregion
    }
}
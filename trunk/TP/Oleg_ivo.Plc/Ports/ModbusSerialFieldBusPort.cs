using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Oleg_ivo.Plc.Devices.Contollers;
using Oleg_ivo.Plc.FieldBus;

namespace Oleg_ivo.Plc.Ports
{
    ///<summary>
    /// Порт подключения к полевой шине, поддерживающий протокола Modbus (/RS-232, /RS-485...)
    ///</summary>
    public class ModbusSerialFieldBusPort : ModbusFieldBusPort
    {
        #region properties

        ///<summary>
        ///
        ///</summary>
        public ModbusSerialAccessor ModbusSerialAccessor
        {
            get { return ModbusAccessor as ModbusSerialAccessor; }
        }

        ///<summary>
        /// 
        ///</summary>
        public AsciiRtuMode Mode
        {
            get { return ModbusSerialAccessor.Mode; }
            set { ModbusSerialAccessor.Mode = value; }
        }

        #endregion

        #region constructors
        ///<summary>
        /// Порт подключения к полевой шине, поддерживающий протокола Modbus (/RS-232, /RS-485...)
        ///</summary>
        public ModbusSerialFieldBusPort(ModbusSerialAccessor modbusSerialAccessor, List<object> addressRange)
            : base(modbusSerialAccessor, addressRange)
        {
        }

        ///<summary>
        ///Только для дизайнера
        ///</summary>
        protected ModbusSerialFieldBusPort() : this(null)
        {
        }

        ///<summary>
        ///
        ///</summary>
        ///<param name="modbusSerialAccessor"></param>
        protected ModbusSerialFieldBusPort(ModbusSerialAccessor modbusSerialAccessor) : this(modbusSerialAccessor, null)
        {
        }
        

        #endregion

        #region methods

        ///<summary>
        /// Тип полевой шины
        ///</summary>
        public override FieldBusType FieldBusType
        {
            get { return FieldBusType.RS485; }
        }

        ///<summary>
        /// Получить диапазон адресов для данного порта
        ///</summary>
        ///<returns></returns>
        protected override FieldBusNodeAddress[] GetPLCAddressRange()
        {
            FieldBusNodeAddressCollection plcAddresses = new FieldBusNodeAddressCollection();
            plcAddresses.AddRange((from Pair slaveAddress in _addressRange
                                   where String.Equals(slaveAddress.First as string, PortName, StringComparison.InvariantCultureIgnoreCase)
                                   select new FieldBusNodeSerialAddress(PortName, Convert.ToByte(slaveAddress.Second), 0)).Cast<FieldBusNodeAddress>());

            return plcAddresses.ToArray();

        }


        #endregion

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Component"></see> and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing"><see langword="true"/> to release both managed and unmanaged resources; <see langword="false"/> to release only unmanaged resources. </param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing && ModbusSerialAccessor != null)
            {
                ModbusSerialAccessor.Dispose();
            }
        }
    }
}
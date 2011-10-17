using System;
using System.Net;
using Oleg_ivo.Plc.Devices.Contollers;

namespace Oleg_ivo.Plc.FieldBus
{
    ///<summary>
    /// Компонент доступа к ресурсам полевой шины, поддерживающий одну из разновидностей протокола Modbus по протоколу IP
    ///</summary>
    public abstract class ModbusIpAccessor : ModbusAccessor
    {

        #region fields

        private readonly FieldBusType _fieldBusType;

        #endregion

        ///<summary>
        ///
        ///</summary>
        ///<param name="port"></param>
        ///<param name="ipAddress"></param>
        ///<param name="fieldBusType"></param>
        protected ModbusIpAccessor(int port, IPAddress ipAddress, FieldBusType fieldBusType)
        {
            Port = port;
            IPAddress = ipAddress;
            _fieldBusType = fieldBusType;
        }

        ///<summary>
        /// Тип полевой шины
        ///</summary>
        public override FieldBusType FieldBusType
        {
            get { return _fieldBusType; }
        }

        /// <summary>
        /// Порт подключения
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Адрес подключения
        /// </summary>
        public IPAddress IPAddress { get; set; }

        ///<summary>
        /// Получить диапазон адресов для данного порта
        ///</summary>
        ///<returns></returns>
        ///<exception cref="NotImplementedException"></exception>
        protected override FieldBusNodeAddress[] GetPLCAddressRange()
        {
            throw new NotImplementedException();
        }
    }
}
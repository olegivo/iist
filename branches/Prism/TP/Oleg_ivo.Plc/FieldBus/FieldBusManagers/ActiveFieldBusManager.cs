using System;
using System.Collections.Generic;
using System.Linq;
using Oleg_ivo.Plc.Devices.Contollers;
using Oleg_ivo.Plc.FieldBus.FieldBusNodes;

namespace Oleg_ivo.Plc.FieldBus.FieldBusManagers
{
    ///<summary>
    /// ƒиспетчер полевой шины, управл€ющий своими ресурсами сам
    ///</summary>
    public class ActiveFieldBusManager : FieldBusManager
    {
        #region constructors

        ///<summary>
        ///
        ///</summary>
        ///<param name="fieldBusAccessor"></param>
        public ActiveFieldBusManager(IFieldBusAccessor fieldBusAccessor)
        {
            if (fieldBusAccessor == null) throw new ArgumentNullException("fieldBusAccessor");
            //if (fieldBusAccessor is ActiveFieldBusManager) throw new InvalidOperationException("fieldBusAccessor");

            _fieldBusAccessor = fieldBusAccessor;
        }

        #endregion

        #region fields
        private readonly IFieldBusAccessor _fieldBusAccessor;

        #endregion

        #region properties
        ///<summary>
        /// 
        ///</summary>
        public IFieldBusAccessor FieldBusAccessor
        {
            get { return _fieldBusAccessor; }
        }

        #endregion

        #region methods

        #endregion

        #region overrides

        ///<summary>
        /// ѕолучить диапазон адресов дл€ данного порта
        ///</summary>
        ///<returns></returns>
        public override IEnumerable<FieldBusNodeAddress> GetPLCAddressRange()
        {
            FieldBusNodeAddressCollection plcAddresses = new FieldBusNodeAddressCollection();
            switch (FieldBusType)
            {
                case FieldBusType.RS232:
                case FieldBusType.RS485:
                    //throw new NotImplementedException("определение последовательных портов");//todo: определение последовательных портов, которые сохранены
                    plcAddresses.AddRange(
                        FieldBusAddresses.Cast<FieldBusNodeSerialAddress>().Where(
                            slaveAddress =>
                            String.Equals(slaveAddress.SerialPortName, PortName,
                                          StringComparison.InvariantCultureIgnoreCase)).Cast<FieldBusNodeAddress>());
                    break;
            }
            return plcAddresses.ToArray();
        }

        ///<summary>
        ///
        ///</summary>
        public override FieldBusType FieldBusType
        {
            get { return FieldBusAccessor.FieldBusType; }
        }

        ///<summary>
        /// ѕроверить доступность полевой шины
        ///</summary>
        ///<returns></returns>
        public override bool CheckOnline()
        {
            return (IsOnline = FieldBusAccessor.CheckOnline());
        }

        ///<summary>
        /// ѕорт подключени€ к полевой шине
        ///</summary>
        public string PortName
        {
            get { return ((ModbusAccessor) _fieldBusAccessor).PortName; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldBusNodeAccessor"></param>
        /// <param name="address"></param>
        /// <param name="numberOfPoints"></param>
        /// <returns></returns>
        public override ushort[] ReadHoldingRegisters(IFieldBusNodeAccessor fieldBusNodeAccessor, ushort address, ushort numberOfPoints)
        {
            return FieldBusAccessor.ReadHoldingRegisters(fieldBusNodeAccessor, address, numberOfPoints);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldBusNodeAccessor"></param>
        /// <param name="address"></param>
        /// <param name="numberOfPoints"></param>
        /// <returns></returns>
        public override ushort[] ReadInputRegisters(IFieldBusNodeAccessor fieldBusNodeAccessor, ushort address, ushort numberOfPoints)
        {
            return FieldBusAccessor.ReadInputRegisters(fieldBusNodeAccessor, address, numberOfPoints);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldBusNodeAccessor"></param>
        /// <param name="address"></param>
        /// <param name="numberOfPoints"></param>
        /// <returns></returns>
        public override bool[] ReadCoils(IFieldBusNodeAccessor fieldBusNodeAccessor, ushort address, ushort numberOfPoints)
        {
            return FieldBusAccessor.ReadCoils(fieldBusNodeAccessor, address, numberOfPoints);
        }

        ///<summary>
        ///
        ///</summary>
        ///<param name="fieldBusNodeAccessor"></param>
        ///<param name="address"></param>
        ///<param name="value"></param>
        ///<returns></returns>
        public override bool WriteSingleRegister(IFieldBusNodeAccessor fieldBusNodeAccessor, ushort address, ushort value)
        {
            return FieldBusAccessor.WriteSingleRegister(fieldBusNodeAccessor, address, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldBusNodeAccessor"></param>
        /// <param name="address"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool WriteSingleCoil(IFieldBusNodeAccessor fieldBusNodeAccessor, ushort address, bool value)
        {
            return FieldBusAccessor.WriteSingleCoil(fieldBusNodeAccessor, address, value);
        }

        /// <summary>
        /// »нициализировать управление по Modbus 
        /// </summary>
        public override void InitializeModbusMaster()
        {
            FieldBusAccessor.InitializeModbusMaster();
        }

        ///<summary>
        ///
        ///</summary>
        ///<param name="fieldBusNodeAccessor"></param>
        ///<param name="address"></param>
        ///<param name="values"></param>
        ///<returns></returns>
        public override bool WriteMultipleRegisters(IFieldBusNodeAccessor fieldBusNodeAccessor, ushort address, ushort[] values)
        {
            return FieldBusAccessor.WriteMultipleRegisters(fieldBusNodeAccessor, address, values);
        }

        ///<summary>
        ///
        ///</summary>
        ///<param name="fieldBusNodeAccessor"></param>
        ///<param name="address"></param>
        ///<param name="values"></param>
        ///<returns></returns>
        public override bool WriteMultipleCoils(IFieldBusNodeAccessor fieldBusNodeAccessor, ushort address, bool[] values)
        {
            return FieldBusAccessor.WriteMultipleCoils(fieldBusNodeAccessor, address, values);
        }

        #endregion
    }
}
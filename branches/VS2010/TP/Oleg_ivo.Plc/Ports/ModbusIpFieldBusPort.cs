using System;
using System.Linq;
using System.Net;
using System.Web.UI;
using Oleg_ivo.Plc.Devices.Contollers;
using Oleg_ivo.Plc.FieldBus;

namespace Oleg_ivo.Plc.Ports
{
    ///<summary>
    /// ���� ����������� � ������� ����, �������������� ��������� Modbus (/TCP, /UPD...)
    ///</summary>
    public abstract class ModbusIpFieldBusPort : ModbusFieldBusPort
    {

        #region fields


        #endregion

        #region properties
        ///<summary>
        ///
        ///</summary>
        public ModbusIpAccessor ModbusIpAccessor
        {
            get { return ModbusAccessor as ModbusIpAccessor; }
        }

        #endregion

        #region constructors
        ///<summary>
        ///
        ///</summary>
        ///<param name="modbusAccessor"></param>
        protected ModbusIpFieldBusPort(ModbusIpAccessor modbusAccessor) : base(modbusAccessor)
        {
        }

        ///<summary>
        ///
        ///</summary>
        protected ModbusIpFieldBusPort() : this(null)
        {
        }

        #endregion

        /// <summary>
        /// ���� �����������
        /// </summary>
        public int Port
        {
            get { return ModbusIpAccessor.Port; }
            set { ModbusIpAccessor.Port = value; }
        }

        /// <summary>
        /// ����� �����������
        /// </summary>
        public IPAddress IPAddress
        {
            get { return ModbusIpAccessor.IPAddress; }
            set { ModbusIpAccessor.IPAddress = value; }
        }

        ///<summary>
        /// ��� ������� ����
        ///</summary>
        public override FieldBusType FieldBusType
        {
            get { return ModbusIpAccessor.FieldBusType; }
        }

        ///<summary>
        /// �������� �������� ������� ��� ������� �����
        ///</summary>
        ///<returns></returns>
        ///<exception cref="NotImplementedException"></exception>
        protected override FieldBusNodeAddress[] GetPLCAddressRange()
        {
            FieldBusNodeAddressCollection plcAddresses = new FieldBusNodeAddressCollection();
            plcAddresses.AddRange((from Pair slaveAddress in _addressRange
                                   select
                                       new FieldBusNodeIpAddress((byte[]) slaveAddress.First, (int) slaveAddress.Second,
                                                                 0)).Cast<FieldBusNodeAddress>());

            return plcAddresses.ToArray();
        }
    }
}
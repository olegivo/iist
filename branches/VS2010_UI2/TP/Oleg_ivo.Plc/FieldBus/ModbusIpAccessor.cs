using System;
using System.Net;
using Oleg_ivo.Plc.Devices.Contollers;

namespace Oleg_ivo.Plc.FieldBus
{
    ///<summary>
    /// ��������� ������� � �������� ������� ����, �������������� ���� �� �������������� ��������� Modbus �� ��������� IP
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
        /// ��� ������� ����
        ///</summary>
        public override FieldBusType FieldBusType
        {
            get { return _fieldBusType; }
        }

        /// <summary>
        /// ���� �����������
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// ����� �����������
        /// </summary>
        public IPAddress IPAddress { get; set; }

        ///<summary>
        /// �������� �������� ������� ��� ������� �����
        ///</summary>
        ///<returns></returns>
        ///<exception cref="NotImplementedException"></exception>
        protected override FieldBusNodeAddress[] GetPLCAddressRange()
        {
            throw new NotImplementedException();
        }
    }
}
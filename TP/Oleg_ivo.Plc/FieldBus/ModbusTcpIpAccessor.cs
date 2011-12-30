using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace Oleg_ivo.Plc.FieldBus
{
    ///<summary>
    ///
    ///</summary>
    public class ModbusTcpIpAccessor : ModbusIpAccessor
    {
        #region fields
        private TcpClient _client;

        #endregion

        #region properties

        ///<summary>
        ///
        ///</summary>
        ///<param name="port"></param>
        ///<param name="ipAddress"></param>
        ///<param name="fieldBusType"></param>
        public ModbusTcpIpAccessor(int port, IPAddress ipAddress, FieldBusType fieldBusType)
            : base(port, ipAddress, fieldBusType)
        {
        }

        #endregion


        #region Overrides of ModbusAccessor

        ///<summary>
        /// ��� �����
        ///</summary>
        public override string PortName
        {
            get
            {
                return string.Format("{0} : {1}.",
                                     IPAddress,
                                     Port);
            }
        }

        #endregion

        ///<summary>
        /// ���������������� ���������� �� Modbus 
        ///</summary>
        public override void InitializeModbusMaster()
        {
            if (_modbusAdapter == null)
            {
                Debug.WriteLine("������������� ���������� �� Modbus...");
                _modbusAdapter = new WagoTcpModbusAdapter(IPAddress.ToString(), (ushort)Port, true, 1000);
            }
        }

        ///<summary>
        /// ��������� ����������� ������� ����
        ///</summary>
        ///<returns></returns>
        public override bool CheckOnline()
        {
            Console.WriteLine("������������ ����������� � {0}...", _client);
            bool[] coils = _modbusAdapter.ReadCoils(0, 0, 1);
            return coils!=null && coils.Length > 0;
        }
    }
}
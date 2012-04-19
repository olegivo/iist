using WAGO.IO.Modbus;

//#define MBT

namespace Oleg_ivo.Plc.FieldBus
{
    public class WagoTcpModbusAdapter : ModbusAdapter
    {
        private readonly string _ipAddress;
        private readonly ushort _port;
        private readonly bool _useTcp;
        private MBTDLL _mbtdll;//TODO: организовать обработку ошибок

        public WagoTcpModbusAdapter(string ipAddress, ushort port, bool useTcp, ushort timeout)
        {

            _ipAddress = ipAddress;
            _port = port;
            _useTcp = useTcp;
            Timeout = timeout;
#if MBT
            InitMBT();
#endif
        }

        private void InitMBT()
        {
            _mbtdll = new MBTDLL();
            Connect();
        }

        private void Connect()
        {
            _mbtdll.Connect(IpAddress, Port, UseTcp, Timeout);
        }

        public ushort Timeout { get; set; }

        public bool UseTcp
        {
            get { return _useTcp; }
        }

        public ushort Port
        {
            get { return _port; }
        }

        public string IpAddress
        {
            get { return _ipAddress; }
        }

        public override ushort[] ReadHoldingRegisters(byte slaveAddress, ushort address, ushort numberOfPoints)
        {
            ushort[] data = null;
            _mbtdll.Read(address, ref data, true);
            return data;
        }

        public override ushort[] ReadInputRegisters(byte slaveAddress, ushort address, ushort numberOfPoints)
        {
            ushort[] data = null;
            _mbtdll.Read(address, ref data, true);
            return data;
        }

        public override bool[] ReadCoils(byte slaveAddress, ushort address, ushort numberOfPoints)
        {
            bool[] data = null;
            _mbtdll.Read(address, ref data, true);
            return data;
        }

        public override void WriteSingleRegister(byte slaveAddress, ushort address, ushort value)
        {
            _mbtdll.Write(address, new[]{value});
        }

        public override void WriteSingleCoil(byte slaveAddress, ushort address, bool value)
        {
            _mbtdll.Write(address, new[] { value });
        }

        public override void WriteMultipleRegisters(byte slaveAddress, ushort address, ushort[] values)
        {
            _mbtdll.Write(address, values);
        }

        public override void WriteMultipleCoils(byte slaveAddress, ushort address, bool[] values)
        {
            _mbtdll.Write(address, values);
        }

        /// <summary>
        /// Выполняет определяемые приложением задачи, связанные с удалением, высвобождением или сбросом неуправляемых ресурсов.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public override void Dispose()
        {
#if MBT
            _mbtdll.Disconnect();
            _mbtdll.Destroy();
#endif
        }
    }
}
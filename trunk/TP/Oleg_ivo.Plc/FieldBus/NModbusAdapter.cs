using Modbus.Device;

namespace Oleg_ivo.Plc.FieldBus
{
    public class NModbusAdapter : ModbusAdapter
    {
        private readonly IModbusMaster _modbusMaster;

        public NModbusAdapter(IModbusMaster modbusMaster)
        {
            _modbusMaster = modbusMaster;
        }

        public ModbusAccessorTimeouts ModbusAccessorTimeouts
        {
            get
            {
                return new ModbusAccessorTimeouts
                           {
                               WaitToRetryMilliseconds = _modbusMaster.Transport.WaitToRetryMilliseconds,
                               Retries = _modbusMaster.Transport.Retries,
                               ReadTimeout = _modbusMaster.Transport.ReadTimeout,
                               WriteTimeout = _modbusMaster.Transport.WriteTimeout

                           };
            }
            set
            {
                _modbusMaster.Transport.WaitToRetryMilliseconds = value.WaitToRetryMilliseconds;
                _modbusMaster.Transport.Retries = value.Retries;
                _modbusMaster.Transport.ReadTimeout = value.ReadTimeout;
                _modbusMaster.Transport.WriteTimeout = value.WriteTimeout;

            }
        }

        public override ushort[] ReadHoldingRegisters(byte slaveAddress, ushort address, ushort numberOfPoints)
        {
            return _modbusMaster.ReadHoldingRegisters(slaveAddress, address, numberOfPoints);
        }

        public override ushort[] ReadInputRegisters(byte slaveAddress, ushort address, ushort numberOfPoints)
        {
            return _modbusMaster.ReadInputRegisters(slaveAddress, address, numberOfPoints);
        }

        public override bool[] ReadCoils(byte slaveAddress, ushort address, ushort numberOfPoints)
        {
            return _modbusMaster.ReadCoils(slaveAddress, address, numberOfPoints);
        }

        public override void WriteSingleRegister(byte slaveAddress, ushort address, ushort value)
        {
            _modbusMaster.WriteSingleRegister(slaveAddress,address,value);
        }

        public override void WriteSingleCoil(byte slaveAddress, ushort address, bool value)
        {
            _modbusMaster.WriteSingleCoil(slaveAddress, address, value);
        }

        public override void WriteMultipleRegisters(byte slaveAddress, ushort address, ushort[] values)
        {
            _modbusMaster.WriteMultipleRegisters(slaveAddress, address, values);
        }

        public override void WriteMultipleCoils(byte slaveAddress, ushort address, bool[] values)
        {
            _modbusMaster.WriteMultipleCoils(slaveAddress, address, values);
        }

        /// <summary>
        /// ¬ыполн€ет определ€емые приложением задачи, св€занные с удалением, высвобождением или сбросом неуправл€емых ресурсов.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public override void Dispose()
        {
            _modbusMaster.Dispose();
        }
    }
}
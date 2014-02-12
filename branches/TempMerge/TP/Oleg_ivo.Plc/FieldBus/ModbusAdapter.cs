using System;

namespace Oleg_ivo.Plc.FieldBus
{
    public abstract class ModbusAdapter: IDisposable
    {
        public abstract ushort[] ReadHoldingRegisters(byte slaveAddress, ushort address, ushort numberOfPoints);
        //TODO: убрать numberOfPoints (скопипастить из MBT.dll) и сделать методы перегруженными
        public abstract ushort[] ReadInputRegisters(byte slaveAddress, ushort address, ushort numberOfPoints);
        public abstract bool[] ReadCoils(byte slaveAddress, ushort address, ushort numberOfPoints);
        public abstract void WriteSingleRegister(byte slaveAddress, ushort address, ushort value);
        public abstract void WriteSingleCoil(byte slaveAddress, ushort address, bool value);
        public abstract void WriteMultipleRegisters(byte slaveAddress, ushort address, ushort[] values);
        public abstract void WriteMultipleCoils(byte slaveAddress, ushort address, bool[] values);

        /// <summary>
        /// Выполняет определяемые приложением задачи, связанные с удалением, высвобождением или сбросом неуправляемых ресурсов.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public abstract void Dispose();
    }
}
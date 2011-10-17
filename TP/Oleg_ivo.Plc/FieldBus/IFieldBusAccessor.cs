using System;
using Oleg_ivo.Plc.FieldBus.FieldBusNodes;

namespace Oleg_ivo.Plc.FieldBus
{
    /// <summary>
    /// ��������� ������� � �������� ������� ����
    /// </summary>
    public interface IFieldBusAccessor :  IDisposable
    {
        ///<summary>
        /// ��������� ����������� ������� ����
        ///</summary>
        ///<returns></returns>
        bool CheckOnline();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldBusNodeAccessor"></param>
        /// <param name="address"></param>
        /// <param name="numberOfPoints"></param>
        /// <returns></returns>
        ushort[] ReadHoldingRegisters(IFieldBusNodeAccessor fieldBusNodeAccessor, ushort address, ushort numberOfPoints);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldBusNodeAccessor"></param>
        /// <param name="address"></param>
        /// <param name="numberOfPoints"></param>
        /// <returns></returns>
        ushort[] ReadInputRegisters(IFieldBusNodeAccessor fieldBusNodeAccessor, ushort address, ushort numberOfPoints);

        ///<summary>
        ///
        ///</summary>
        ///<param name="fieldBusNodeAccessor"></param>
        ///<param name="address"></param>
        ///<param name="value"></param>
        ///<returns></returns>
        bool WriteSingleRegister(IFieldBusNodeAccessor fieldBusNodeAccessor, ushort address, ushort value);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldBusNodeAccessor"></param>
        /// <param name="address"></param>
        /// <param name="numberOfPoints"></param>
        /// <returns></returns>
        bool[] ReadCoils(IFieldBusNodeAccessor fieldBusNodeAccessor, ushort address, ushort numberOfPoints);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldBusNodeAccessor"></param>
        /// <param name="address"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool WriteSingleCoil(IFieldBusNodeAccessor fieldBusNodeAccessor, ushort address, bool value);

        ///<summary>
        ///
        ///</summary>
        ///<param name="fieldBusNodeAccessor"></param>
        ///<param name="address"></param>
        ///<param name="values"></param>
        ///<returns></returns>
        bool WriteMultipleRegisters(IFieldBusNodeAccessor fieldBusNodeAccessor, ushort address, ushort[] values);

        ///<summary>
        ///
        ///</summary>
        ///<param name="fieldBusNodeAccessor"></param>
        ///<param name="address"></param>
        ///<param name="values"></param>
        ///<returns></returns>
        bool WriteMultipleCoils(IFieldBusNodeAccessor fieldBusNodeAccessor, ushort address, bool[] values);

        /// <summary>
        /// ���������������� ���������� �� Modbus 
        /// </summary>
        void InitializeModbusMaster();

        ///<summary>
        /// ��� ������� ����
        ///</summary>
        FieldBusType FieldBusType { get; }
    }
}
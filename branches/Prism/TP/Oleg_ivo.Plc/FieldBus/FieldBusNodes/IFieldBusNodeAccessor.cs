using System;
using Oleg_ivo.Plc.Devices.Contollers;

namespace Oleg_ivo.Plc.FieldBus.FieldBusNodes
{
    /// <summary>
    /// ��������� ������� � �������� ���� ������� ����
    /// </summary>
    public interface IFieldBusNodeAccessor : IDisposable
    {
        /// <summary>
        /// ��������� ������� � �������� ������� ����
        /// </summary>
        IFieldBusAccessor FieldBusAccessor { get; }

        /// <summary>
        /// ��������� ��������
        /// </summary>
        /// <param name="address"></param>
        /// <param name="numberOfPoints"></param>
        /// <returns></returns>
        ushort[] ReadHoldingRegisters(ushort address, ushort numberOfPoints);

        /// <summary>
        /// ��������� ������� ��������
        /// </summary>
        /// <param name="address"></param>
        /// <param name="numberOfPoints"></param>
        /// <returns></returns>
        ushort[] ReadInputRegisters(ushort address, ushort numberOfPoints);

        ///<summary>
        /// �������� � �������
        ///</summary>
        ///<param name="address"></param>
        ///<param name="value"></param>
        ///<returns></returns>
        bool WriteSingleRegister(ushort address, ushort value);

        /// <summary>
        /// ��������� ����
        /// </summary>
        /// <param name="address"></param>
        /// <param name="numberOfPoints"></param>
        /// <returns></returns>
        bool[] ReadCoils(ushort address, ushort numberOfPoints);

        /// <summary>
        /// �������� ���� ���
        /// </summary>
        /// <param name="address"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool WriteSingleCoil(ushort address, bool value);

        ///<summary>
        /// �������� ��������� ���������
        ///</summary>
        ///<param name="address"></param>
        ///<param name="values"></param>
        ///<returns></returns>
        bool WriteMultipleRegisters(ushort address, ushort[] values);

        ///<summary>
        /// �������� ��������� �����
        ///</summary>
        ///<param name="address"></param>
        ///<param name="values"></param>
        ///<returns></returns>
        bool WriteMultipleCoils(ushort address, bool[] values);

        /// <summary>
        /// ���������������� ���������� �� Modbus 
        /// </summary>
        void InitializeModbusMaster();

        ///<summary>
        /// ���� ������� ���� ������
        ///</summary>
        bool IsOnline { get; }

        string AddressPart1 //TODO:���� ���� FieldBusAddress, ������������ ������ ��� �������������� ���� � CMS
        { get; set; }

        int AddressPart2 //TODO:���� ���� FieldBusAddress, ������������ ������ ��� �������������� ���� � CMS
        { get; set; }

        ///<summary>
        /// ��������� ����������� ���� ������� ����
        ///</summary>
        ///<returns></returns>
        bool CheckOnline();
    }
}
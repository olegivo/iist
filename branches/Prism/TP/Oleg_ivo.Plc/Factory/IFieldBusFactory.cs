using System;
using Oleg_ivo.Plc.Devices.Contollers;
using Oleg_ivo.Plc.FieldBus;
using Oleg_ivo.Plc.FieldBus.FieldBusManagers;
using Oleg_ivo.Plc.Ports;

namespace Oleg_ivo.Plc.Factory
{
    ///<summary>
    /// ������� ������� ���
    ///</summary>
    public interface IFieldBusFactory
    {
        ///<summary>
        /// ����� ����� �������
        ///</summary>
        ///<returns></returns>
        object[] FindPorts();

        /// <summary>
        /// ��� ����� �� ���������
        /// </summary>
        FieldBusType DefaultFieldBusType { get; }

        ///<summary>
        /// ����� ����� ����������� � ������� ����� ��������� ����
        ///</summary>
        ///<param name="fieldBusType"></param>
        ///<returns></returns>
        ///<exception cref="ArgumentOutOfRangeException"></exception>
        object[] FindPorts(FieldBusType fieldBusType);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldBusAccessor"></param>
        /// <param name="fieldBus"></param>
        /// <returns></returns>
        FieldBusManager CreateFieldBusManager(IFieldBusAccessor fieldBusAccessor, Entities.FieldBus fieldBus);


        ///<summary>
        /// 
        ///</summary>
        ///<param name="fieldBusType"></param>
        ///<param name="port"></param>
        ///<returns></returns>
        IFieldBusAccessor CreateFieldbusAccessor(FieldBusType fieldBusType, object port);

        ///<summary>
        ///
        ///</summary>
        ///<param name="fieldBusType"></param>
        ///<param name="port"></param>
        ///<returns></returns>
        ///<exception cref="ArgumentOutOfRangeException"></exception>
        FieldBusPortParameters CreatePortParameters(FieldBusType fieldBusType, object port);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldBusType"></param>
        FieldBusNodeAddressCollection GetFieldBusNodesAddresses(FieldBusType fieldBusType);
    }
}
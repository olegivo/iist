using System;
using System.Collections.Generic;
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

        /// <summary>
        ///  ����� ����� ����������� � ������� ����� ��������� ����
        /// </summary>
        /// <param name="fieldBusType"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        List<Entities.FieldBus> FindPorts(FieldBusType fieldBusType);

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldBusType"></param>
        [Obsolete("use property FieldBusAddresses of instance FieldBusManager instead")]
        FieldBusNodeAddressCollection GetFieldBusNodesAddresses(FieldBusType fieldBusType);
    }
}
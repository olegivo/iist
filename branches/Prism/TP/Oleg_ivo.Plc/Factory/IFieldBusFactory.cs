using System;
using Oleg_ivo.Plc.Devices.Contollers;
using Oleg_ivo.Plc.FieldBus;
using Oleg_ivo.Plc.FieldBus.FieldBusManagers;
using Oleg_ivo.Plc.Ports;

namespace Oleg_ivo.Plc.Factory
{
    ///<summary>
    /// Фабрика полевых шин
    ///</summary>
    public interface IFieldBusFactory
    {
        ///<summary>
        /// Найти порты системы
        ///</summary>
        ///<returns></returns>
        object[] FindPorts();

        /// <summary>
        /// Тип порта по умолчанию
        /// </summary>
        FieldBusType DefaultFieldBusType { get; }

        ///<summary>
        /// Найти порты подключения к полевым шинам заданного типа
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
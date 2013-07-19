using Oleg_ivo.Plc.Devices.Contollers;
using Oleg_ivo.Plc.FieldBus;
using Oleg_ivo.Plc.FieldBus.FieldBusManagers;
using Oleg_ivo.Plc.FieldBus.FieldBusNodes;

namespace Oleg_ivo.Plc.Factory
{
    ///<summary>
    /// Фабрика узлов полевой шины
    ///</summary>
    public interface IFieldBusNodeFactory
    {
        ///<summary>
        /// Построить узлы полевой шины
        ///</summary>
        ///<param name="fieldBusManager"></param>
        ///<returns></returns>
        FieldBusNodeCollection BuildFieldBusNodes(FieldBusManager fieldBusManager);

        ///<summary>
        ///
        ///</summary>
        ///<param name="fieldBusManager"></param>
        ///<param name="modbusAccessor"></param>
        ///<returns></returns>
        FieldBusNodeCollection FindNodes(FieldBusManager fieldBusManager, ModbusAccessor modbusAccessor);

        ///<summary>
        ///
        ///</summary>
        ///<param name="fieldBusManager"></param>
        ///<param name="fieldBusNodeAddress"></param>
        ///<returns></returns>
        FieldBusNode CreateFieldBusNode(FieldBusManager fieldBusManager, FieldBusNodeAddress fieldBusNodeAddress);

        /// <summary>
        /// Фабрика ПЛК
        /// </summary>
        IPlcFactory PlcFactory { get; }

        /// <summary>
        /// Загрузить настроенные узлы для полевой шины
        /// </summary>
        /// <param name="fieldBusManager"></param>
        /// <returns></returns>
        FieldBusNodeCollection LoadFieldBusNodes(FieldBusManager fieldBusManager);
    }
}
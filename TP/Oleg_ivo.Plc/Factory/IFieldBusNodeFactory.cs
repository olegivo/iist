using Oleg_ivo.Plc.Devices.Contollers;
using Oleg_ivo.Plc.FieldBus;
using Oleg_ivo.Plc.FieldBus.FieldBusManagers;
using Oleg_ivo.Plc.FieldBus.FieldBusNodes;

namespace Oleg_ivo.Plc.Factory
{
    ///<summary>
    /// ������� ����� ������� ����
    ///</summary>
    public interface IFieldBusNodeFactory
    {
        ///<summary>
        /// ��������� ���� ������� ����
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldBusManager"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        FieldBusNode CreateFieldBusNode(FieldBusManager fieldBusManager, Entities.FieldBusNode entity);

        /// <summary>
        /// ������� ���
        /// </summary>
        IPlcFactory PlcFactory { get; }

        /// <summary>
        /// ��������� ����������� ���� ��� ������� ����
        /// </summary>
        /// <param name="fieldBusManager"></param>
        /// <returns></returns>
        FieldBusNodeCollection LoadFieldBusNodes(FieldBusManager fieldBusManager);

        /// <summary>
        /// TODO: ������ ����������� ������ (�������� � ����������)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        FieldBusNodeAddress GetFieldBusNodeAddress(Plc.Entities.FieldBusNode entity);
    }
}
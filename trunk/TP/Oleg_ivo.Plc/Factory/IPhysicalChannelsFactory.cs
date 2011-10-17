using Oleg_ivo.Plc.Channels;
using Oleg_ivo.Plc.Devices.Contollers;
using Oleg_ivo.Plc.FieldBus.FieldBusNodes;

namespace Oleg_ivo.Plc.Factory
{
    ///<summary>
    /// ‘абрика физических каналов
    ///</summary>
    public interface IPhysicalChannelsFactory
    {
        ///<summary>
        /// ѕостроить физические каналы
        ///</summary>
        ///<param name="plc"></param>
        ///<returns></returns>
        PhysicalChannelCollection BuildPhysicalChannels(PLC plc);

        ///<summary>
        /// «агрузить настроенные физические каналы дл€ узла полевой шины
        ///</summary>
        ///<param name="fieldBusNode"></param>
        ///<returns></returns>
        PhysicalChannelCollection LoadPhysicalChannels(FieldBusNode fieldBusNode);

        ///<summary>
        /// —охранить настроенные физические каналы дл€ физического канала
        ///</summary>
        ///<param name="fieldBusNode"></param>
        ///<returns></returns>
        void SavePhysicalChannels(FieldBusNode fieldBusNode);
    }
}
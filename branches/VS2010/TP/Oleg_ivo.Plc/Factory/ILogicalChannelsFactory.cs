using Oleg_ivo.Plc.Channels;

namespace Oleg_ivo.Plc.Factory
{
    ///<summary>
    /// Фабрика логических каналов
    ///</summary>
    public interface ILogicalChannelsFactory
    {
        ///<summary>
        ///
        ///</summary>
        ///<param name="physicalChannel"></param>
        ///<returns></returns>
        LogicalChannelCollection BuildLogicalChannel(PhysicalChannel physicalChannel);

        ///<summary>
        /// Загрузить настроенные логические каналы для физического канала
        ///</summary>
        ///<returns></returns>
        LogicalChannelCollection LoadLogicalChannels(PhysicalChannel physicalChannel);

        ///<summary>
        /// Загрузить настроенные логические каналы для физического канала
        ///</summary>
        ///<returns></returns>
        void SaveLogicalChannels(PhysicalChannel physicalChannel);
    }
}
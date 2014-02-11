using System;
using Oleg_ivo.Plc.Channels;

namespace Oleg_ivo.Plc.Factory
{
    ///<summary>
    /// Фабрика логических каналов
    ///</summary>
    public interface ILogicalChannelsFactory
    {
        /// <summary>
        ///  Построить коллекцию логических каналов по умолчанию для данного физического канала (т.е. соответствующих физическому каналу по размерности)
        /// </summary>
        /// <param name="physicalChannel"></param>
        /// <returns></returns>
        LogicalChannelCollection BuildDefaultLogicalChannels(PhysicalChannel physicalChannel);

        ///<summary>
        /// Загрузить настроенные логические каналы для физического канала
        ///</summary>
        ///<returns></returns>
        LogicalChannelCollection LoadLogicalChannels(PhysicalChannel physicalChannel);

        ///<summary>
        /// Сохранить настроенные логические каналы для физического канала
        ///</summary>
        ///<returns></returns>
        [Obsolete("Вместо данного метода следует пользоваться DataContext.SubmitChanges")]
        void SaveLogicalChannels(PhysicalChannel physicalChannel);

        /// <summary>
        /// Создать шаблон логического канала для физического канала (с нулевым сдвигом адреса внутри физического канала)
        /// </summary>
        /// <param name="physicalChannel"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        LogicalChannel CreateLogicalChannelTemplate(PhysicalChannel physicalChannel, Entities.LogicalChannel entity = null);
    }
}
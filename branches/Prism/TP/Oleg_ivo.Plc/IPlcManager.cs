using System.Collections.Generic;
using System.ComponentModel;
using Oleg_ivo.Plc.Channels;
using Oleg_ivo.Plc.Factory;
using Oleg_ivo.Plc.FieldBus;
using Oleg_ivo.Plc.FieldBus.FieldBusManagers;

namespace Oleg_ivo.Plc
{
    public interface IPlcManager
    {
        ///<summary>
        /// Диспетчеры полевых шин
        ///</summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Browsable(true)]
        List<FieldBusManager> FieldBusManagers { get; }

        ///<summary>
        ///
        ///</summary>
        LogicalChannelCollection LogicalChannels { get; }

        ///<summary>
        /// Фабрика узлов полевой шины
        ///</summary>
        ///<returns></returns>
        IFieldBusNodeFactory FieldBusNodesFactory { get; }

        ///<summary>
        /// Фабрика полевых шин
        ///</summary>
        ///<returns></returns>
        IFieldBusFactory FieldBusFactory { get; }

        /// <summary>
        /// Фабрика физических каналов
        /// </summary>
        IPhysicalChannelsFactory PhysicalChannelsFactory { get; }

        /// <summary>
        /// Фабрика логических каналов
        /// </summary>
        ILogicalChannelsFactory LogicalChannelsFactory { get; }

        ///<summary>
        /// Построить конфигурацию полевых шин системы
        ///</summary>
        ///<param name="cleanBeforeBuild">Очищать перед построением</param>
        ///<param name="fieldBusType">Тип полевой шины</param>
        void BuildFieldBuses(bool cleanBeforeBuild, FieldBusType fieldBusType);

        void Dispose();
        string ToString();
        
        void Save();
    }
}
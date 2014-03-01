using System;
using System.Linq;
using Oleg_ivo.Base.Autofac;
using Oleg_ivo.Plc;
using Oleg_ivo.Plc.Channels;
using Oleg_ivo.Plc.Devices.Modules;
using Oleg_ivo.Plc.Factory;
using Oleg_ivo.Plc.FieldBus.FieldBusNodes;

namespace Oleg_ivo.WAGO.Factory
{
    ///<summary>
    /// ‘абрика логических каналов
    ///</summary>
    public class LogicalChannelsFactory : FactoryBase, ILogicalChannelsFactory
    {
        private readonly IDistributedMeasurementInformationSystem dmis;

        public LogicalChannelsFactory(IDistributedMeasurementInformationSystem dmis)
        {
            this.dmis = Enforce.ArgumentNotNull(dmis, "dmis");
        }

        ///<summary>
        /// ѕостроить коллекцию логических каналов по умолчанию дл€ данного физического канала (т.е. соответствующих физическому каналу по размерности)
        ///</summary>
        ///<param name="physicalChannel"></param>
        ///<exception cref="ArgumentOutOfRangeException"></exception>
        ///<returns></returns>
        public LogicalChannelCollection BuildDefaultLogicalChannels(PhysicalChannel physicalChannel)
        {
            var channelCollection = new LogicalChannelCollection();
            var ioModule = physicalChannel.IOModule;
            if (ioModule == null)
                throw new NullReferenceException("ƒл€ физического канала должен быть задан модуль ввода-вывода");

            if (!ioModule.IsInput && !ioModule.IsOutput)
                throw new ArgumentOutOfRangeException("physicalChannel",
                    "ћодуль ввода-вывода физического канала не €вл€етс€ ни входным, ни выходным");
            
            ushort count;
            ushort logicalChannelSize;
            GetIOModuleDimention(ioModule, out logicalChannelSize, out count);

            for (ushort shift = 0; shift < count; shift++)
                channelCollection.Add(BuildLogicalChannel(physicalChannel, shift, logicalChannelSize, null));

            return channelCollection;
        }

        /// <summary>
        /// ѕолучить размерность модул€ ввода-вывода (размер одного логического канала и количество логических каналов)
        /// </summary>
        /// <param name="ioModule"></param>
        /// <param name="logicalChannelSize"></param>
        /// <param name="count"></param>
        private static void GetIOModuleDimention(IOModule ioModule, out ushort logicalChannelSize, out ushort count)
        {
            //todo:«десь надо строить логический канал
            bool isAnalog = ioModule.IsAnalog;
            bool isDiscrete = ioModule.IsDiscrete;
            ushort size = ioModule.Size;
            //TODO:IsAnalog + IsDescrete => enum (справочник)
            count = (ushort) (isAnalog && !isDiscrete ? size/16 : size);
            logicalChannelSize = (ushort) (size/count);
        }

        private LogicalChannel BuildLogicalChannel(PhysicalChannel physicalChannel, ushort shift, ushort logicalChannelSize, Plc.Entities.LogicalChannel entity)
        {
            var logicalChannel = new LogicalChannel(physicalChannel, entity, shift, logicalChannelSize)
            {
                IsEmulationMode = dmis.Settings.IsEmulationMode
            };
            return logicalChannel;
        }

        ///<summary>
        /// «агрузить настроенные логические каналы дл€ физического канала
        ///</summary>
        ///<returns></returns>
        public LogicalChannelCollection LoadLogicalChannels(PhysicalChannel physicalChannel)
        {
            LogicalChannelCollection channels = new LogicalChannelCollection();

            channels.AddRange(
                DataContext.LogicalChannels.Where(lc => lc.PhysicalChannelId == physicalChannel.Id)
                    .Select(entity => CreateLogicalChannelTemplate(physicalChannel, entity)));

            return channels;

            //LogicalChannelsDAC logicalChannelsDAC = new LogicalChannelsDAC {LogicalChannelsFactory = this};
            //return logicalChannelsDAC.GetChannels(physicalChannel);
        }

        [Obsolete("¬место данного метода следует использовать метод CreateLogicalChannelTemplate")]
        private LogicalChannel CreateChannelFromData(Plc.Entities.LogicalChannel row, PhysicalChannel physicalChannel)
        {
            var channel = CreateLogicalChannelTemplate(physicalChannel, row);
            //channel.Id = row.Id;
            //if (row.DirectPolynom!=null) channel.DirectTransform = Polynom.DeSerializePolynom(row.DirectPolynom);
            //if (row.ReversePolynom != null) channel.ReverseTransform = Polynom.DeSerializePolynom(row.ReversePolynom);
            //if (row.PollPeriod != null) channel.PollPeriod = TimeSpan.FromMilliseconds((double)row.PollPeriod);
            //if (row.Size!=null) channel.ChannelSize = (ushort)row.Size;
            //if (row.Description!=null) channel.Description = row.Description;
            //if (row.SensivityDelta!=null) channel.DeltaChangeLimit = (double)row.SensivityDelta;
            //if (row.MinValue!=null) channel.MinValue = (double)row.MinValue;
            //if (row.MaxValue!=null) channel.MaxValue = (double)row.MaxValue;
            //if (row.MinNormalValue!=null) channel.MinNormalValue = (double)row.MinNormalValue;
            //if (row.MaxNormalValue!=null) channel.MaxNormalValue = (double)row.MaxNormalValue;
            //if (row.AddressShift!=null) channel.AddressShift = Convert.ToUInt16(row.AddressShift);

            return channel;
        }

        /// <summary>
        /// —оздать шаблон логического канала дл€ физического канала (с нулевым сдвигом адреса внутри физического канала)
        /// </summary>
        /// <param name="physicalChannel"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public LogicalChannel CreateLogicalChannelTemplate(PhysicalChannel physicalChannel, Plc.Entities.LogicalChannel entity = null)
        {
            var ioModule = physicalChannel.IOModule;
            if (ioModule == null)
                throw new NullReferenceException("ƒл€ физического канала должен быть задан модуль ввода-вывода");

            if (!ioModule.IsInput && !ioModule.IsOutput)
                throw new ArgumentOutOfRangeException("physicalChannel",
                    "ћодуль ввода-вывода физического канала не €вл€етс€ ни входным, ни выходным");

            ushort count;
            ushort logicalChannelSize;
            GetIOModuleDimention(ioModule, out logicalChannelSize, out count);

            return BuildLogicalChannel(physicalChannel, (ushort)(entity!=null ? entity.AddressShift.Value : 0), logicalChannelSize, entity);
        }


        ///<summary>
        /// «агрузить настроенные логические каналы дл€ физического канала
        ///</summary>
        ///<returns></returns>
        [Obsolete("«десь вызываетс€ код из LogicalChannelsDAC, там осуществл€етс€ перенос данных из Ћ  в датасет с последующим сохранением. Ќужно пользоватьс€ DataContext.SubmitChanges")]
        public void SaveLogicalChannels(PhysicalChannel physicalChannel)
        {
            var logicalChannelsDAC = new LogicalChannelsDAC {LogicalChannelsFactory = this};
            logicalChannelsDAC.SaveChannels(physicalChannel);
        }
    }
}
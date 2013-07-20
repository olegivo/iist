using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Oleg_ivo.Plc.Channels;
using Oleg_ivo.Plc.Factory;
using Enumerable = System.Linq.Enumerable;

namespace Oleg_ivo.WAGO.Factory
{
    ///<summary>
    ///
    ///</summary>
    public partial class LogicalChannelsDAC : Component
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ILogicalChannelsFactory LogicalChannelsFactory { get; set; }

        ///<summary>
        ///
        ///</summary>
        public LogicalChannelsDAC()
        {
            InitializeComponent();
        }

        ///<summary>
        ///
        ///</summary>
        ///<returns></returns>
        public LogicalChannelCollection GetChannels(PhysicalChannel physicalChannel)
        {
            return GetChannels(physicalChannel, true);
        }

        ///<summary>
        ///
        ///</summary>
        ///<returns></returns>
        private LogicalChannelCollection GetChannels(PhysicalChannel physicalChannel, bool needLoad)
        {
            LogicalChannelCollection channels = new LogicalChannelCollection();

            if (needLoad)
                FillChannelsFromDb(physicalChannel.Id, 0);

            channels.AddRange(Enumerable.Select(dtsChannelConfiguration1.LogicalChannel,
                                                row => CreateChannelFromData(row, physicalChannel)));

            return channels;
        }

        ///<summary>
        ///
        ///</summary>
        ///<param name="physicalChannelId"></param>
        ///<param name="id"></param>
        public void FillChannelsFromDb(int physicalChannelId, int id)
        {
            if (physicalChannelId > 0) SDA.SelectCommand.Parameters["@PhysicalChannelId"].Value = physicalChannelId;
            if (id > 0) SDA.SelectCommand.Parameters["@Id"].Value = id;
// ReSharper disable RedundantCheckBeforeAssignment
            if (dataManager1.DataSet != DataSet) dataManager1.DataSet = DataSet;
// ReSharper restore RedundantCheckBeforeAssignment

            dataManager1.Fill();
        }

        ///<summary>
        /// —охран€ет логические каналы и возвращает их
        ///</summary>
        ///<param name="physicalChannel"></param>
        public void SaveChannels(PhysicalChannel physicalChannel)
        {
            LogicalChannelCollection newChannels = new LogicalChannelCollection();
            newChannels.AddRange(physicalChannel.LogicalChannels.Where(logicalChannel => logicalChannel.Id == 0));

            ((SqlParameter)dataManager1.DataAdapter.SelectCommand.Parameters["@PhysicalChannelId"]).Value
                = physicalChannel.Id;

            FillLogicalChannelsData(physicalChannel);
            Console.WriteLine("ѕодлежит сохранению логических каналов: {0}", dtsChannelConfiguration1.LogicalChannel.Count);
            dataManager1.Save();

            //при успешном сохранении и перезаполнении перезаполн€ем и каналы
            if (dtsChannelConfiguration1.LogicalChannel.Count > 0)
            {
                //удал€ем все несохранЄнные каналы:
                foreach (var logicalChannel in physicalChannel.LogicalChannels.Where(channel => channel.Id > 0).ToArray())
                {
                    physicalChannel.LogicalChannels.Remove(logicalChannel);
                }

                physicalChannel.LogicalChannels.AddRange(GetChannels(physicalChannel, false));
            }

            //foreach (LogicalChannel logicalChannel in newChannels)
            //{
            //    string condition = string.Format("IsInput={0} and IsOutput={1}", logicalChannel.IOModule.IsInput, logicalChannel.IOModule.IsOutput
            //                                    );
            //    DataRow[] dataRows = dtsChannelConfiguration1.PhysicalChannel.Select(condition,null, DataViewRowState.Unchanged);
            //    if (dataRows.Length==1)
            //    {
            //        logicalChannel.Id = ((DtsChannelConfiguration.PhysicalChannelRow) dataRows[0]).Id;
            //    }
            //    else
            //    {
            //        throw new Exception();
            //    }
            //}
        }

        ///<summary>
        ///
        ///</summary>
        ///<param name="physicalChannel"></param>
        ///<returns></returns>
        private void FillLogicalChannelsData(PhysicalChannel physicalChannel)
        {
            if (physicalChannel != null)
            {
                DtsChannelConfiguration.LogicalChannelDataTable channelDataTable =
                    dtsChannelConfiguration1.LogicalChannel;

                dataManager1.Fill();

                FillLogicalChannelsData(physicalChannel.LogicalChannels, channelDataTable);
            }
        }

        /// <summary>
        ///  опирует данные из <paramref name="channelDataTable"/> в <paramref name="logicalChannels"/>.
        /// “олько обновление каналов, не добавление!
        /// </summary>
        /// <param name="channelDataTable"></param>
        /// <param name="logicalChannels"></param>
        public void FillLogicalChannelsData(DtsChannelConfiguration.LogicalChannelDataTable channelDataTable, IEnumerable<LogicalChannel> logicalChannels)
        {
            foreach (DtsChannelConfiguration.LogicalChannelRow row in channelDataTable)
            {
                LogicalChannel logicalChannel = null;
                if (row.Id > 0)
                    logicalChannel = logicalChannels.SingleOrDefault(LogicalChannel.GetFindChannelPredicate(row.Id));

                if (logicalChannel != null)
                    FillLogicalChannelData(row, logicalChannel, logicalChannel.PhysicalChannel);
            }
        }

        /// <summary>
        ///  опирует данные из <paramref name="logicalChannels"/> в <paramref name="channelDataTable"/>.
        /// » обновление, и добавление каналов
        /// </summary>
        /// <param name="channelDataTable"></param>
        /// <param name="logicalChannels"></param>
        public void FillLogicalChannelsData(IEnumerable<LogicalChannel> logicalChannels, DtsChannelConfiguration.LogicalChannelDataTable channelDataTable)
        {
            foreach (LogicalChannel logicalChannel in logicalChannels)
            {
                DtsChannelConfiguration.LogicalChannelRow row;
                if (logicalChannel.Id > 0)
                {
                    row = channelDataTable.FindById(logicalChannel.Id);
                }
                else
                {
                    row = channelDataTable.NewLogicalChannelRow();
                    row.Id = 0;
                    channelDataTable.AddLogicalChannelRow(row);
                }

                FillLogicalChannelData(logicalChannel, row, logicalChannel.PhysicalChannel);
            }
        }

        /// <summary>
        ///  опирует данные из <paramref name="row"/> в <paramref name="logicalChannel"/>
        /// </summary>
        /// <param name="row"></param>
        /// <param name="logicalChannel"></param>
        /// <param name="physicalChannel"></param>
        private void FillLogicalChannelData(DtsChannelConfiguration.LogicalChannelRow row, LogicalChannel logicalChannel, PhysicalChannel physicalChannel)
        {
            if (row.Id > 0) logicalChannel.Id = row.Id;
            logicalChannel.AddressShift = Convert.ToUInt16(row.AddressShift);
            logicalChannel.ChannelSize = (ushort) row.Size;
            logicalChannel.Description = row.Description;
            logicalChannel.MinValue = (double?) row.MinValue;
            logicalChannel.MaxValue = (double?) row.MaxValue;
            logicalChannel.MinNormalValue = (double?) row.MinNormalValue;
            logicalChannel.MaxNormalValue = (double?) row.MaxNormalValue;
            logicalChannel.PollPeriod = TimeSpan.FromMilliseconds(row.PollPeriod);
            logicalChannel.DeltaChangeLimit = (double) row.SensivityDelta;
            //row.PhysicalChannelId = physicalChannel.Id;
            //TODO: не все пол€ проинициализированы при копировании из row в logicalChannel
        }

        /// <summary>
        ///  опирует данные из <paramref name="logicalChannel"/> в <paramref name="row"/>
        /// </summary>
        /// <param name="row"></param>
        /// <param name="logicalChannel"></param>
        /// <param name="physicalChannel"></param>
        private void FillLogicalChannelData(LogicalChannel logicalChannel, DtsChannelConfiguration.LogicalChannelRow row, PhysicalChannel physicalChannel)
        {
            if (logicalChannel.Id > 0) row.Id = logicalChannel.Id;
            row.PhysicalChannelId = physicalChannel.Id;
            if (logicalChannel.MinValue != null) row.MinValue = (decimal) logicalChannel.MinValue;
            if (logicalChannel.MaxValue != null) row.MaxValue = (decimal) logicalChannel.MaxValue;

            row.AddressShift = logicalChannel.AddressShift;
            row.Size = logicalChannel.ChannelSize;
            row.Description = logicalChannel.Description;
            if (logicalChannel.PollPeriod.TotalMilliseconds > 0) row.PollPeriod = logicalChannel.PollPeriod.TotalMilliseconds;
            if (logicalChannel.DeltaChangeLimit > 0) row.SensivityDelta = (decimal)logicalChannel.DeltaChangeLimit;
            //TODO: не все пол€ проинициализированы при копировании из logicalChannel в row
        }

        private LogicalChannel CreateChannelFromData(DtsChannelConfiguration.LogicalChannelRow row, PhysicalChannel physicalChannel)
        {
            var logicalChannels = LogicalChannelsFactory.BuildLogicalChannel(physicalChannel);
            LogicalChannel channel = logicalChannels[0];
            channel.Id = row.Id;
            if(!row.IsDirectPolynomNull()) channel.DirectTransform = Polynom.DeSerializePolynom(row.DirectPolynom);
            if(!row.IsReversePolynomNull()) channel.ReverseTransform = Polynom.DeSerializePolynom(row.ReversePolynom);
            if (!row.IsPollPeriodNull()) channel.PollPeriod = TimeSpan.FromMilliseconds(row.PollPeriod);
            if (!row.IsSizeNull()) channel.ChannelSize = (ushort) row.Size;
            if (!row.IsDescriptionNull()) channel.Description = row.Description;
            if (!row.IsSensivityDeltaNull()) channel.DeltaChangeLimit = (double) row.SensivityDelta;
            if (!row.IsMinValueNull()) channel.MinValue = (double) row.MinValue;
            if (!row.IsMaxValueNull()) channel.MaxValue = (double) row.MaxValue;
            if (!row.IsMinNormalValueNull()) channel.MinNormalValue = (double) row.MinNormalValue;
            if (!row.IsMaxNormalValueNull()) channel.MaxNormalValue = (double) row.MaxNormalValue;
            if (!row.IsAddressShiftNull()) channel.AddressShift = Convert.ToUInt16(row.AddressShift);

            return channel;
        }

        ///<summary>
        ///
        ///</summary>
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)/*, TypeConverterAttribute(typeof(DataSetTypeConverter))*/]
        public DtsChannelConfiguration DataSet
        {
            get { return dtsChannelConfiguration1 ?? new DtsChannelConfiguration(); }
            set { dtsChannelConfiguration1 = value; }
        }

        ///<summary>
        ///
        ///</summary>
        public IDataAdapter DataAdapter
        {
            get { return SDA; }
        }

        ///<summary>
        ///
        ///</summary>
        public void Save()
        {
            dataManager1.Save();
        }
    }
}
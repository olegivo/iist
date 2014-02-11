using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using NLog;
using Oleg_ivo.Plc.Channels;
using Oleg_ivo.Plc.Factory;

namespace Oleg_ivo.WAGO.Meta
{
    ///<summary>
    ///
    ///</summary>
    public partial class WagoMetaDAC : Component
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        private readonly ILogicalChannelsFactory logicalChannelsFactory;

        ///<summary>
        ///
        ///</summary>
        public WagoMetaDAC(ILogicalChannelsFactory logicalChannelsFactory)
        {
            this.logicalChannelsFactory = logicalChannelsFactory;
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
                FillChannelsFromDb(physicalChannel.Id);

            foreach (DtsWago.WagoMetaRow row in dtsWago1.WagoMeta)
            {
                LogicalChannel channel = CreateChannelFromData(row, physicalChannel);
                channels.Add(channel);
            }

            return channels;
        }

        ///<summary>
        ///
        ///</summary>
        ///<param name="physicalChannelId"></param>
        public void FillChannelsFromDb(int physicalChannelId)
        {
            if (physicalChannelId > 0) SDA.SelectCommand.Parameters["@PhysicalChannelId"].Value = physicalChannelId;
            if (dataManager1.DataSet != DataSet) dataManager1.DataSet = DataSet;

            dataManager1.Fill();
        }

        ///<summary>
        /// Сохраняет логические каналы и возвращает их
        ///</summary>
        ///<param name="physicalChannel"></param>
        public void SaveChannels(PhysicalChannel physicalChannel)
        {
            LogicalChannelCollection newChannels = new LogicalChannelCollection();
            foreach (LogicalChannel logicalChannel in physicalChannel.LogicalChannels)
                if (logicalChannel.Id == 0)
                    newChannels.Add(logicalChannel);

            ((SqlParameter)dataManager1.DataAdapter.SelectCommand.Parameters["@PhysicalChannelId"]).Value
                = physicalChannel.Id;

            FillLogicalChannelsData(physicalChannel);
            Log.Debug("Подлежит сохранению логических каналов: {0}", dtsWago1.WagoMeta.Count);
            dataManager1.Save();

            //при успешном сохранении и перезаполнении перезаполняем и каналы
            if (dtsWago1.WagoMeta.Count > 0)
            {
                physicalChannel.LogicalChannels.Clear();
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
        public void FillLogicalChannelsData(PhysicalChannel physicalChannel)
        {
            if (physicalChannel != null)
            {
                DtsWago.WagoMetaDataTable channelDataTable =
                    dtsWago1.WagoMeta;

                dataManager1.Fill();

                foreach (LogicalChannel logicalChannel in physicalChannel.LogicalChannels)
                {
                    DtsWago.WagoMetaRow row;
                    if (logicalChannel.Id > 0)
                    {
                        row = channelDataTable.FindById(logicalChannel.Id);
                    }
                    else
                    {
                        row = channelDataTable.NewWagoMetaRow();
                        channelDataTable.AddWagoMetaRow(row);
                    }
                    
                    FillLogicalChannelData(row, logicalChannel);
                }
            }
        }

        private void FillLogicalChannelData(DtsWago.WagoMetaRow row, LogicalChannel logicalChannel)
        {
            if (logicalChannel.Id > 0) row.Id = logicalChannel.Id;
        }

        private LogicalChannel CreateChannelFromData(DtsWago.WagoMetaRow row, PhysicalChannel physicalChannel)
        {
            LogicalChannel channel = logicalChannelsFactory.CreateLogicalChannelTemplate(physicalChannel);
            channel.Id = row.Id;
            return channel;
        }

        ///<summary>
        ///
        ///</summary>
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)/*, TypeConverterAttribute(typeof(DataSetTypeConverter))*/]
        public DtsWago DataSet
        {
            get { return dtsWago1 ?? new DtsWago(); }
            set { dtsWago1 = value; }
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
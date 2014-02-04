using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using Oleg_ivo.Plc.Channels;
using Oleg_ivo.Plc.Factory;
using Oleg_ivo.Plc.FieldBus.FieldBusNodes;
using Oleg_ivo.WAGO.Devices;
using Oleg_ivo.WAGO.Meta;

namespace Oleg_ivo.WAGO.Factory
{
    ///<summary>
    ///
    ///</summary>
    [Obsolete("Подлежит замене на сущности Linq2Sql")]
    public partial class PhysicalChannelsDAC : Component
    {
        ///<summary>
        ///
        ///</summary>
        public PhysicalChannelsDAC()
        {
            InitializeComponent();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ILogicalChannelsFactory LogicalChannelsFactory { get; set; }

        ///<summary>
        ///
        ///</summary>
        ///<param name="container"></param>
        public PhysicalChannelsDAC(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        ///<summary>
        ///
        ///</summary>
        ///<returns></returns>
        [Obsolete("")]
        public PhysicalChannelCollection GetChannels(FieldBusNode fieldBusNode)
        {
            var channels = new PhysicalChannelCollection();

            FillChannelsFromDb(fieldBusNode.Id, 0);

            channels.AddRange(from DtsChannelConfiguration.PhysicalChannelRow row in dtsChannelConfiguration1.PhysicalChannel
                              select CreateChannelFromData(row, fieldBusNode));

            return channels;
        }

        ///<summary>
        ///
        ///</summary>
        ///<param name="fieldNodeId"></param>
        ///<param name="id"></param>
        public void FillChannelsFromDb(int fieldNodeId, int id)
        {
            if (fieldNodeId > 0) SDA.SelectCommand.Parameters["@FieldNodeId"].Value = fieldNodeId;
            if (id > 0) SDA.SelectCommand.Parameters["@Id"].Value = id;
// ReSharper disable RedundantCheckBeforeAssignment
            if (dataManager1.DataSet != DataSet) dataManager1.DataSet = DataSet;
// ReSharper restore RedundantCheckBeforeAssignment

            dataManager1.Fill();
        }

        ///<summary>
        ///
        ///</summary>
        ///<param name="fieldBusNode"></param>
        public void SaveChannels(FieldBusNode fieldBusNode)
        {
            var newChannels = fieldBusNode.PhysicalChannels.Where(x => x.Id==0);//только несохранённыее каналы

            FillChannelsData(fieldBusNode);
            dataManager1.Save();
            // TODO: может после сохранения ФК проще переинициализировать их? Правда, тогда нужно переинициализировать ещё и ЛК, а также узлы ПШ
            var physicalChannelDataTable = dtsChannelConfiguration1.PhysicalChannel;

            foreach (var physicalChannel in newChannels)
            {
                var ioModule = physicalChannel.IOModule;
                var channel = physicalChannel;

                try
                {
                    if(physicalChannelDataTable.Count>0)
                    {
                        //ищем один ряд, которого с искомым каналов совпадает IsInput и IsOutput, а также адреса чтения и записи
                        var channelRow = physicalChannelDataTable.AsEnumerable().
                            Cast<DtsChannelConfiguration.PhysicalChannelRow>().
                            Single(row =>
                                   !row.IsIsInputNull() && row.IsInput == ioModule.IsInput
                                   && !row.IsIsOutputNull() && row.IsOutput == ioModule.IsOutput
                                   && !row.IsReadAddressNull() && row.ReadAddress == channel.ReadAddress
                                   && !row.IsWriteAddressNull() && row.WriteAddress == channel.WriteAddress
                            );
                        physicalChannel.Id = channelRow.Id;
                    }
                }
                catch(NullReferenceException ex)
                {
                    throw new Exception("Не нашли ни одного соответствия между сохранёнными и имеющимися физическими каналами", ex);
                }
                catch (Exception ex)
                {
                    throw new Exception("Не нашли однозначного соответствия между сохранёнными и имеющимися физическими каналами", ex);
                }
            }
        }

        ///<summary>
        /// Копирует данные из каналов в таблицу
        ///</summary>
        ///<param name="fieldBusNode"></param>
        ///<returns></returns>
        private void FillChannelsData(FieldBusNode fieldBusNode)
        {
            if (fieldBusNode != null)
            {
                DtsChannelConfiguration.PhysicalChannelDataTable channelDataTable =
                    dtsChannelConfiguration1.PhysicalChannel;

                dataManager1.Fill();

                foreach (PhysicalChannel physicalChannel in fieldBusNode.PhysicalChannels)
                {
                    DtsChannelConfiguration.PhysicalChannelRow row;
                    if (physicalChannel.Id > 0) // канал был сохранён
                    {
                        row = channelDataTable.FindById(physicalChannel.Id);
                    }
                    else // новый канал
                    {
                        row = channelDataTable.NewPhysicalChannelRow();
                        channelDataTable.AddPhysicalChannelRow(row);
                    }
                    
                    FillChannelData(row, physicalChannel, fieldBusNode);
                }
            }
        }

        /// <summary>
        /// Копирует данные из <paramref name="channel"/> в <paramref name="row"/>
        /// </summary>
        /// <param name="row"></param>
        /// <param name="channel"></param>
        /// <param name="fieldBusNode"></param>
        private void FillChannelData(DtsChannelConfiguration.PhysicalChannelRow row, PhysicalChannel channel, FieldBusNode fieldBusNode)
        {
            if (channel.Id > 0) row.Id = channel.Id;
            row.AddressShift = (short)channel.AddressShift;
            row.PhysicalChannelSize = channel.ChannelSize;
            row.IsInput = channel.IOModule.IsInput;
            row.IsOutput = channel.IOModule.IsOutput;
            row.IsAnalog = channel.IOModule.IsAnalog;
            row.IsDiscrete = channel.IOModule.IsDiscrete;
            row.ReadAddress = channel.ReadAddress;
            row.WriteAddress = channel.WriteAddress;
            row.FieldNodeId = fieldBusNode.Id;
        }

        [Obsolete("")]
        private PhysicalChannel CreateChannelFromData(DtsChannelConfiguration.PhysicalChannelRow row, FieldBusNode fieldBusNode)
        {
            var ioModule = new WagoIOModule(LogicalChannelsFactory)
                               {
                                   Meta =
                                       new WagoIOModuleMeta(row.IsAnalog, row.IsDiscrete, row.IsInput, row.IsOutput,
                                                            (ushort) row.PhysicalChannelSize, 0, row.AddressShift)
                               };

            var channel = new PhysicalChannel(null, LogicalChannelsFactory, fieldBusNode, ioModule, (ushort)row.AddressShift, (ushort)row.PhysicalChannelSize)
                              {Id = row.Id, WriteAddress = row.WriteAddress, ReadAddress = row.ReadAddress};
            return channel;
        }

        ///<summary>
        ///
        ///</summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), TypeConverterAttribute(typeof(DataSetTypeConverter))]
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
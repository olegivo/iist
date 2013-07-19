using System;
using System.ComponentModel;
using System.Data;
using Oleg_ivo.Plc.Channels;
using Oleg_ivo.Plc.Devices.Contollers;
using Oleg_ivo.Plc.Factory;
using Oleg_ivo.Plc.FieldBus;
using Oleg_ivo.Plc.FieldBus.FieldBusManagers;
using Oleg_ivo.Plc.FieldBus.FieldBusNodes;
using Enumerable = System.Linq.Enumerable;

namespace Oleg_ivo.WAGO.Factory
{
    ///<summary>
    ///
    ///</summary>
    public partial class FieldBusNodeDAC : Component
    {
        /// <summary>
        /// 
        /// </summary>
        public FieldBusNodeDAC()
        {
            InitializeComponent();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IFieldBusNodeFactory FieldBusNodesFactory { get; set; }

        ///<summary>
        ///
        ///</summary>
        ///<returns></returns>
        public FieldBusNodeCollection GetFieldBusNodes(FieldBusManager fieldBusManager)
        {
            return GetFieldBusNodes(fieldBusManager, true);
        }

        ///<summary>
        ///
        ///</summary>
        ///<returns></returns>
        private FieldBusNodeCollection GetFieldBusNodes(FieldBusManager fieldBusManager, bool needLoad)
        {
            FieldBusNodeCollection fieldBusNodes = new FieldBusNodeCollection();

            if (needLoad)
                FillFieldBusNodesFromDb(fieldBusManager.FieldBusType, 0);

            fieldBusNodes.AddRange(Enumerable.Select(dtsChannelConfiguration1.FieldBusNode,
                                                     row => CreateFieldBusNodeFromData(row, fieldBusManager)));

            return fieldBusNodes;
        }

        ///<summary>
        ///
        ///</summary>
        ///<param name="fieldBusType"></param>
        ///<param name="fieldBusNodeId">0, если не фильтровать</param>
        public void FillFieldBusNodesFromDb(FieldBusType fieldBusType, int fieldBusNodeId)
        {
            if (fieldBusType > 0) SDA.SelectCommand.Parameters["@FieldBusTypeId"].Value = fieldBusType;
            if (fieldBusNodeId > 0) SDA.SelectCommand.Parameters["@Id"].Value = fieldBusNodeId;
// ReSharper disable RedundantCheckBeforeAssignment
            if (dataManager1.DataSet != DataSet) dataManager1.DataSet = DataSet;
// ReSharper restore RedundantCheckBeforeAssignment

            dataManager1.Fill();
        }

/*TODO
        ///<summary>
        /// Сохраняет логические каналы и возвращает их
        ///</summary>
        ///<param name="physicalChannel"></param>
        public void SaveFieldBusNodes(PhysicalChannel physicalChannel)
        {
            LogicalChannelCollection newChannels = new LogicalChannelCollection();
            foreach (LogicalChannel logicalChannel in physicalChannel.LogicalChannels)
                if (logicalChannel.Id == 0)
                    newChannels.Add(logicalChannel);

            ((OleDbParameter)dataManager1.DataAdapter.SelectCommand.Parameters["PhysicalChannelId"]).Value
                = physicalChannel.Id;

            FillFieldBusNodesData(physicalChannel);
            System.Console.WriteLine("Подлежит сохранению логических каналов: {0}", dtsChannelConfiguration1.LogicalChannel.Count);
            dataManager1.Save();

            //при успешном сохранении и перезаполнении перезаполняем и каналы
            if (dtsChannelConfiguration1.LogicalChannel.Count > 0)
            {
                physicalChannel.LogicalChannels.Clear();
                physicalChannel.LogicalChannels.AddRange(GetFieldBusNodes(physicalChannel, false));
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
*/

        ///<summary>
        /// Копирует данные из каналов в таблицу
        ///</summary>
        ///<param name="physicalChannel"></param>
        ///<returns></returns>
        public void FillFieldBusNodesData(PhysicalChannel physicalChannel)
        {
            if (physicalChannel != null)
            {
                DtsChannelConfiguration.LogicalChannelDataTable channelDataTable =
                    dtsChannelConfiguration1.LogicalChannel;

                dataManager1.Fill();

                foreach (LogicalChannel logicalChannel in physicalChannel.LogicalChannels)
                {
                    DtsChannelConfiguration.LogicalChannelRow row;
                    if (logicalChannel.Id > 0) // канал был сохранён
                    {
                        row = channelDataTable.FindById(logicalChannel.Id);
                    }
                    else // новый канал
                    {
                        row = channelDataTable.NewLogicalChannelRow();
                        channelDataTable.AddLogicalChannelRow(row);
                    }
                    
                    FillFieldBusNodeData(row, logicalChannel, physicalChannel);
                }
            }
        }

        /// <summary>
        /// Копирует данные из <paramref name="logicalChannel"/> в <paramref name="row"/>
        /// </summary>
        /// <param name="row"></param>
        /// <param name="logicalChannel"></param>
        /// <param name="physicalChannel"></param>
        private void FillFieldBusNodeData(DtsChannelConfiguration.LogicalChannelRow row, LogicalChannel logicalChannel, PhysicalChannel physicalChannel)
        {
            if (logicalChannel.Id > 0) row.Id = logicalChannel.Id;
            row.PhysicalChannelId = physicalChannel.Id;
        }

        private FieldBusNode CreateFieldBusNodeFromData(DtsChannelConfiguration.FieldBusNodeRow row, FieldBusManager fieldBusManager)
        {
            FieldBusNode fieldBusNode;
            if (fieldBusManager is ActiveFieldBusManager)
            {
                fieldBusNode = new FieldBusNode(fieldBusManager, FieldBusDAC.GetFieldBusNodeAddress(row))
                                   {Id = row.Id};
            }
            else//нужно построить активный узел полевой шины
            {
                //по адресу из row получаем FieldBusNodeAddress
                FieldBusNodeAddress fieldBusNodeAddress = fieldBusManager.FieldBusAddresses.Find(row.AddressPart1, row.AddressPart2);

                if (fieldBusNodeAddress==null)
                {
                    throw new Exception("Не найден адрес для активного узла полевой шины");
                }

                fieldBusNode = FieldBusNodesFactory.CreateFieldBusNode(fieldBusManager, fieldBusNodeAddress);
            }

            return fieldBusNode;
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
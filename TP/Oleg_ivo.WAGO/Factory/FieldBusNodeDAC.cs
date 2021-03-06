using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using Oleg_ivo.Plc.Channels;
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
        [Obsolete]
        public FieldBusNodeCollection GetFieldBusNodes(FieldBusManager fieldBusManager)
        {
            return GetFieldBusNodes(fieldBusManager, true);
        }

        ///<summary>
        ///
        ///</summary>
        ///<returns></returns>
        [Obsolete]
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
        ///<param name="fieldBusNodeId">0, ���� �� �����������</param>
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
        /// ��������� ���������� ������ � ���������� ��
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
            System.Log.Debug("�������� ���������� ���������� �������: {0}", dtsChannelConfiguration1.LogicalChannel.Count);
            dataManager1.Save();

            //��� �������� ���������� � �������������� ������������� � ������
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
        /// �������� ������ �� ������� � �������
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
                    if (logicalChannel.Id > 0) // ����� ��� ��������
                    {
                        row = channelDataTable.FindById(logicalChannel.Id);
                    }
                    else // ����� �����
                    {
                        row = channelDataTable.NewLogicalChannelRow();
                        channelDataTable.AddLogicalChannelRow(row);
                    }
                    
                    FillFieldBusNodeData(row, logicalChannel, physicalChannel);
                }
            }
        }

        /// <summary>
        /// �������� ������ �� <paramref name="logicalChannel"/> � <paramref name="row"/>
        /// </summary>
        /// <param name="row"></param>
        /// <param name="logicalChannel"></param>
        /// <param name="physicalChannel"></param>
        private void FillFieldBusNodeData(DtsChannelConfiguration.LogicalChannelRow row, LogicalChannel logicalChannel, PhysicalChannel physicalChannel)
        {
            if (logicalChannel.Id > 0) row.Id = logicalChannel.Id;
            row.PhysicalChannelId = physicalChannel.Id;
        }

        [Obsolete]
        private FieldBusNode CreateFieldBusNodeFromData(DtsChannelConfiguration.FieldBusNodeRow row, FieldBusManager fieldBusManager)
        {
            FieldBusNode fieldBusNode;
            if (fieldBusManager is ActiveFieldBusManager)
            {
                var fieldBusNodeAddress = FieldBusDAC.GetFieldBusNodeAddress(row);
                var entity = new Plc.Entities.FieldBusNode
                {
                    AddressPart1 = fieldBusNodeAddress.AddressPart1,
                    AddressPart2 = fieldBusNodeAddress.AddressPart2,
                    Id = fieldBusNodeAddress.Id,
                    FieldBusId = fieldBusManager.Entity.Id,
                    FieldBusTypeId = fieldBusManager.Entity.FieldBusTypeId
                };
                fieldBusNode = new FieldBusNode(fieldBusManager, entity);
            }
            else//����� ��������� �������� ���� ������� ����
            {
                //�� ������ �� row �������� FieldBusNodeAddress
                string addressPart1 = row.AddressPart1;
                int addressPart2 = row.AddressPart2;
                var fieldBusNodeAddress =
                    fieldBusManager.FieldBusAddresses.SingleOrDefault(
                        address => address.AddressPart1 == addressPart1 && address.AddressPart2 == addressPart2);

                if (fieldBusNodeAddress==null)
                {
                    throw new Exception("�� ������ ����� ��� ��������� ���� ������� ����");
                }

                var entity = new Plc.Entities.FieldBusNode
                {
                    AddressPart1 = fieldBusNodeAddress.AddressPart1,
                    AddressPart2 = fieldBusNodeAddress.AddressPart2,
                    Id = fieldBusNodeAddress.Id,
                    FieldBusId = fieldBusManager.Entity.Id,
                    FieldBusTypeId = fieldBusManager.Entity.FieldBusTypeId
                };
                fieldBusNode = FieldBusNodesFactory.CreateFieldBusNode(fieldBusManager, entity);
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
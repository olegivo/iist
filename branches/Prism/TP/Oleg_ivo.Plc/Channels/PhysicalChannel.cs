using System;
using System.Collections.Generic;
using Oleg_ivo.Base.Autofac;
using Oleg_ivo.Plc.Common;
using Oleg_ivo.Plc.Devices.Modules;
using Oleg_ivo.Plc.Factory;
using Oleg_ivo.Plc.FieldBus.FieldBusNodes;

namespace Oleg_ivo.Plc.Channels
{
    ///<summary>
    /// ���������� �����.
    /// ��������� ������ � �������� ������ ��� (�������������� ������, ����������������� ������, ������ ���������� � �.�.)
    ///</summary>
    public class PhysicalChannel : IIdentified
    {

        #region fields
        private readonly ILogicalChannelsFactory logicalChannelsFactory;
        private IOModule ioModule;
        private ushort channelSize;
        private ushort addressShift;
        private int id;

        #endregion

        #region properties

        ///<summary>
        /// ���� ������� ����
        ///</summary>
        public FieldBusNode FieldBusNode { get; private set; }

        ///<summary>
        /// ������ �����-������
        ///</summary>
        public IOModule IOModule
        {
            get { return ioModule; }
            private set
            {
                if(ioModule == value) return;
                ioModule = value;
                if (IOModule != null && IOModule.PhysicalChannel != this)
                {
                    if (IOModule.PhysicalChannel != null)
                        throw new InvalidOperationException(
                            "������ ��������� IOModule � ������ �������, �.�. �� ��� ��� ������ � ������.");
                    IOModule.PhysicalChannel = this;
                }
            }
        }

        public Entities.PhysicalChannel Entity { get; private set; }

        ///<summary>
        /// ����� � ������ �����-������
        ///</summary>
        public ushort AddressShift
        {
            get
            {
                return
                    Entity != null && Entity.AddressShift.HasValue ? (ushort)Entity.AddressShift.Value : addressShift;
            }
            set
            {
                if (AddressShift == value) return;
                addressShift = value;
                if (Entity != null) Entity.AddressShift = (short?)value;
            }
        }

        ///<summary>
        /// ����������� ����������� ������
        ///</summary>
        public ushort ChannelSize
        {
            get
            {
                return
                    (ushort)
                        (Entity != null && Entity.PhysicalChannelSize.HasValue
                            ? Entity.PhysicalChannelSize.Value
                            : (channelSize > 0 ? channelSize : IOModule.Size));
            }
            set
            {
                if (ChannelSize == value) return;
                channelSize = value;
                if (Entity != null) Entity.PhysicalChannelSize = value;
            }
        }

        ///<summary>
        /// ����� �� ���� ��� ������
        ///</summary>
        public int WriteAddress
        {
            get { return IOModule.WriteAddress + AddressShift; }
            set { IOModule.WriteAddress = value - AddressShift; }
        }

        ///<summary>
        /// ����� �� ���� ��� ������
        ///</summary>
        public int ReadAddress
        {
            get { return IOModule.ReadAddress + AddressShift; }
            set { IOModule.ReadAddress = value - AddressShift; }
        }

        ///<summary>
        ///
        ///</summary>
        public LogicalChannelCollection LogicalChannels { get; protected internal set; }

        ///<summary>
        /// �������������
        ///</summary>
        public int Id
        {
            get { return Entity != null ? Entity.Id : id; }
            set
            {
                if (Id == value) return;
                id = value;
                if (Entity != null) Entity.Id = value;
            }
        }

        #endregion

        #region constructors

        /*
        ///<summary>
        ///
        ///</summary>
        ///<param name="fieldBusNode"></param>
        ///<param name="ioModule"></param>
        ///<param name="addressShift"></param>
        public PhysicalChannel(FieldBusNode fieldBusNode, IOModule ioModule, ushort addressShift)
            : this(fieldBusNode, ioModule, addressShift, ioModule.Size)
        {
        }
*/

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="logicalChannelsFactory"></param>
        /// <param name="fieldBusNode"></param>
        /// <param name="ioModule"></param>
        /// <param name="addressShift"></param>
        /// <param name="channelSize"></param>
        public PhysicalChannel(Entities.PhysicalChannel entity, ILogicalChannelsFactory logicalChannelsFactory, FieldBusNode fieldBusNode, IOModule ioModule, ushort addressShift, ushort channelSize)
        {
            this.logicalChannelsFactory = Enforce.ArgumentNotNull(logicalChannelsFactory, "logicalChannelsFactory");
            IOModule = Enforce.ArgumentNotNull(ioModule, "ioModule");
            FieldBusNode = Enforce.ArgumentNotNull(fieldBusNode, "fieldBusNode");
            Entity = entity;
            AddressShift = addressShift;
            ChannelSize = channelSize;
        }

        #endregion

        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return string.Format("<{3}> ���������� ����� [{0}-{1}]{2}",
                WriteAddress,
                WriteAddress + ChannelSize - 1,
                IOModule != null ? string.Format(" ({0})", IOModule) : "",
                Id);
        }


        #region methods
        ///<summary>
        /// ��������� ���������� ������ �� ��������� ��� ������� ����������� ������
        ///</summary>
        public void BuildDefaultLogicalChannels()
        {
            //���� ��� ����������, ����� ��������� �� ���������
            if (LogicalChannels == null || LogicalChannels.Count == 0)
            {
                LogicalChannels = logicalChannelsFactory.BuildDefaultLogicalChannels(this);
            }
            //LogicalChannels.AddRange(IOModule.BuildDefaultLogicalChannels());
        }

        ///<summary>
        /// ��������� ����������� ���������� ������ ��� ����������� ������
        ///</summary>
        ///<returns></returns>
        public void LoadLogicalChannels()
        {
            LogicalChannels = logicalChannelsFactory.LoadLogicalChannels(this);
        }

        ///<summary>
        /// �������� �������� � ���������� �����
        ///</summary>
        ///<param name="addressShift"></param>
        ///<param name="size"></param>
        ///<param name="value"></param>
        public void SetValue(ushort addressShift, ushort size, object value)
        {
            if (!IOModule.IsOutput)
            {
                throw new Exception(string.Format(
                                        "���������� �������� ������ � �����, �.�. �� �� ��������� ������. {0}{1}", Environment.NewLine, this));
            }

            ushort address = (ushort)(WriteAddress + addressShift);
            bool success = false;

            if (size % 16 == 0)//������ 16 ��� - ����� ����������
            {
                if (size > 16)//��������� ���������
                {
                    ushort[] registers = value as ushort[];
                    if (registers != null && registers.Length == size / 16)
                        success = FieldBusNode.WriteMultipleRegisters(address, registers);
                }
                else//���� �������
                {
                    //if (value is ushort)
                    success = FieldBusNode.WriteSingleRegister(address, Convert.ToUInt16(value));
                }
            }
            else//����� ��������
            {
                if (size > 1)//��������� �����
                {
                    bool[] coils = value is bool[] ? (bool[])value : GetBools(value);

                    if (coils != null && coils.Length == size)
                        success = FieldBusNode.WriteMultipleCoils(address, coils);
                }
                else//���� ������
                {
                    if (value is bool)
                        success = FieldBusNode.WriteSingleCoil(address, (bool)value);
                    else if (value is decimal || value is float || value is double || value is int)
                    {
                        var d = Convert.ToDouble(value);
                        if (1.0.Equals(d))
                            success = FieldBusNode.WriteSingleCoil(address, true);
                        else if (0.0.Equals(d))
                            success = FieldBusNode.WriteSingleCoil(address, false);
                    }
                }

            }

            if (!success)
                throw new Exception("�� ������� �������� �������� � �����");
        }

        private bool[] GetBools(object value)
        {
            List<bool> bools = new List<bool>();

            if (value is Int16 || value is Int32 || value is Int64
                || value is UInt16 || value is UInt32 || value is UInt64)
            {
                Int64 u = Int64.Parse(value.ToString());

                while (u > 0)
                {
                    ushort reminder = (ushort)(u % 2);
                    bools.Add(reminder != 0);
                    u /= 2;
                }

                u = Int64.Parse(value.ToString());
                ushort u2 = (ushort)(bools.Count / 8);
                if (u2 > u / 8 || u2 == 0)
                    u2++;

                for (int i = bools.Count; i < u2 * 8; i++) bools.Add(false);
                //bools.Reverse();
            }

            return bools.ToArray();
        }

        ///<summary>
        /// �������� �������� �� ����������� ������
        ///</summary>
        ///<returns></returns>
        ///<param name="addressShift"></param>
        ///<param name="size"></param>
        public object GetValue(ushort addressShift, ushort size)
        {
            object result;
            ushort address = (ushort)(ReadAddress + addressShift);

            if (size % 16 == 0)//������ 16 ��� - ������ ����������
            {
                ushort[] registers = FieldBusNode.ReadHoldingRegisters(address, (ushort)(size / 16));
                result = registers;
            }
            else
            {
                bool[] coils = FieldBusNode.ReadCoils(address, size);
                result = coils;
            }

            return result;
        }
        #endregion

        /// <summary>
        /// �������� ��� ������ �� 
        /// </summary>
        /// <param name="physicalChannel"></param>
        /// <param name="withAddresses">���������� �� ������� (<see cref="AddressShift"/>, <see cref="ReadAddress"/>, <see cref="WriteAddress"/>)</param>
        /// <param name="withSize">���������� �� ������� (<see cref="ChannelSize"/>)</param>
        /// <param name="withIOModule">���������� �� ���������� ������ �����-������</param>
        /// <returns></returns>
        public bool EqualsPredicate(PhysicalChannel physicalChannel, bool withAddresses, bool withSize, bool withIOModule)
        {
            return physicalChannel != null
                   && (Equals(physicalChannel)
                       ||
                       (
                           (!withAddresses ||
                            (physicalChannel.AddressShift == AddressShift &&
                             physicalChannel.ReadAddress == ReadAddress &&
                             physicalChannel.WriteAddress == WriteAddress))
                           && (!withSize || physicalChannel.ChannelSize == ChannelSize)
                           && (!withIOModule || physicalChannel.IOModule.EqualsPredicate(IOModule, false, true, withSize))
                       )
                      );
        }
    }
}
using System;
using System.Runtime.Serialization;
using DMS.Common.Messages;

namespace DMS.Common.MessageExchangeSystem.HighLevel
{
    [DataContract]
    public class RegisteredLogicalChannel : IRegisteredChannel
    {
        public RegisteredLogicalChannel()
        {
        }

        public RegisteredLogicalChannel(int id)
        {
            Id = id;
        }

        public static Func<IRegisteredChannel, bool> GetFindChannelPredicate(int id)
        {
            return (channel => channel.LogicalChannelId == id);
        }


        /// <summary>
        /// ������������� ������
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// ����� ������
        /// </summary>
        [DataMember]
        public int LogicalChannelId
        {
            get { return Id; }
            set { Id = value; }
        }

        /// <summary>
        /// ����������� ���������� �������� ��� ������
        /// </summary>
        [DataMember]
        public double? MinValue { get; set; }

        /// <summary>
        /// ������������ ���������� �������� ��� ������
        /// </summary>
        [DataMember]
        public double? MaxValue { get; set; }

        /// <summary>
        /// ����������� ���������� �������� ��� ������
        /// </summary>
        [DataMember]
        public double? MinNormalValue { get; set; }

        /// <summary>
        /// ������������ ���������� �������� ��� ������
        /// </summary>
        [DataMember]
        public double? MaxNormalValue { get; set; }

        ///<summary>
        /// �������� ������
        ///</summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// ����� ������ ������
        /// </summary>
        [DataMember]
        public DataMode DataMode { get; set; }
    }
}
using System.Runtime.Serialization;

namespace DMS.Common.Messages
{
    /// <summary>
    /// ���������, ���������� ����������� ����������, ��������� � ���������� �������
    /// </summary>
    public abstract class InternalServiceChannelMessage : InternalServiceMessage
    {
        protected InternalServiceChannelMessage(string regNameFrom, string regNameTo, int logicalChannelId) : base(regNameFrom, regNameTo)
        {
            LogicalChannelId = logicalChannelId;
        }

        protected InternalServiceChannelMessage()
        {
        }

        /// <summary>
        /// ����� ����������� ������
        /// </summary>
        [DataMember]
        public int LogicalChannelId { get; set; }
    }
}
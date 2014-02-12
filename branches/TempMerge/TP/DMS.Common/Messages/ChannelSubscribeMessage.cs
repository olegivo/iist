using System.Runtime.Serialization;

namespace DMS.Common.Messages
{
    /// <summary>
    /// ��������� ��� �������� / ������� �� ������
    /// </summary>
    public class ChannelSubscribeMessage : InternalServiceMessage
    {
        protected ChannelSubscribeMessage()
        {
        }

        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="regNameFrom">�������������� ���, �� �������� ���������� ���������</param>
        /// <param name="regNameTo">�������������� ���, �������� ���������� ���������</param>
        /// <param name="mode">����� ����������� (�����������/������)</param>
        /// <param name="logicalChannelId">����� ����������� ������</param>
        public ChannelSubscribeMessage(string regNameFrom, string regNameTo, SubscribeMode mode, int logicalChannelId) : base(regNameFrom, regNameTo)
        {
            Mode = mode;
            LogicalChannelId = logicalChannelId;
        }

        /// <summary>
        /// �������� / ������� �� ������
        /// </summary>
        [DataMember]
        public SubscribeMode Mode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int LogicalChannelId { get; set; }
    }
}
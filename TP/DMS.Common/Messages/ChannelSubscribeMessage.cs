using System.Runtime.Serialization;

namespace DMS.Common.Messages
{
    /// <summary>
    /// ��������� ��� �������� / ������� �� ������
    /// </summary>
    public class ChannelSubscribeMessage : InternalServiceChannelMessage
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
        public ChannelSubscribeMessage(string regNameFrom, string regNameTo, SubscribeMode mode, int logicalChannelId)
            : base(regNameFrom, regNameTo, logicalChannelId)
        {
            Mode = mode;
        }

        /// <summary>
        /// �������� / ������� �� ������
        /// </summary>
        [DataMember]
        public SubscribeMode Mode { get; set; }

        public override EventType EventType
        {
            get { return EventType.ChannelSubscription; }
        }

        protected override string GetMessageType()
        {
            return "�������� ������";
        }

        protected override string GetMessageDescription()
        {
            return string.Format("{0}, ����� �{1}, �������� - {2}", base.GetMessageDescription(), LogicalChannelId, Mode);
        }
    }
}
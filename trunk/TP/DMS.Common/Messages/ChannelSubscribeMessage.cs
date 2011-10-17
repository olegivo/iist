using System.Runtime.Serialization;

namespace DMS.Common.Messages
{
    /// <summary>
    /// ��������� ��� �������� / ������� �� ������
    /// </summary>
    public class ChannelSubscribeMessage : InternalServiceMessage
    {
        /// <summary>
        /// �������� (<see langword="true"/>) / ������� (<see langword="false"/>) �� ������
        /// </summary>
        [DataMember]
        public bool Mode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int LogicalChannelId { get; set; }
    }
}
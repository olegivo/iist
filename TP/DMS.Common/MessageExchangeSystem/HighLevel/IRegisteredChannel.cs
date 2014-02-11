namespace DMS.Common.MessageExchangeSystem.HighLevel
{
    public interface IRegisteredChannel
    {
        /// <summary>
        /// ����� ������
        /// </summary>
        int LogicalChannelId { get; set; }

        /// <summary>
        /// ����������� ���������� �������� ��� ������
        /// </summary>
        double? MinValue { get; }

        /// <summary>
        /// ������������ ���������� �������� ��� ������
        /// </summary>
        double? MaxValue { get; }

        /// <summary>
        /// ����������� ���������� �������� ��� ������
        /// </summary>
        double? MinNormalValue { get; }

        /// <summary>
        /// ������������ ���������� �������� ��� ������
        /// </summary>
        double? MaxNormalValue { get; }

        ///<summary>
        /// �������� ������
        ///</summary>
        string Description { get; }

    }
}
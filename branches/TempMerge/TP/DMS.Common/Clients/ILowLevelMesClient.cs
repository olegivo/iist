using DMS.Common.Messages;

namespace DMS.Common.Clients
{
    /// <summary>
    /// ������ (������� ������) ������� ������ �����������
    /// </summary>
    public interface ILowLevelMesClient : IMesClient
    {
        /// <summary>
        /// �������� ������ ��������������� ������
        /// </summary>
        /// <param name="message"></param>
        void ChannelSubscribe(IInternalMessage message);

        /// <summary>
        /// ������� �� ������ ��������������� ������
        /// </summary>
        /// <param name="message"></param>
        void ChannelUnSubscribe(IInternalMessage message);

        /// <summary>
        /// ����������� ������ � ����������� �����
        /// </summary>
        /// <param name="message"></param>
        void IsAllowWriteChannel(IInternalMessage message);

        /// <summary>
        /// ������ � ����������� �����
        /// </summary>
        /// <param name="message"></param>
        void WriteChannel(IInternalMessage message);

    }
}
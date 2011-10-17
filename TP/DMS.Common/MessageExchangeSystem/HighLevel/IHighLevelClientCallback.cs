using System.ServiceModel;
using DMS.Common.Messages;

namespace DMS.Common.MessageExchangeSystem.HighLevel
{
    /// <summary>
    /// �������� ��������� ������ �� ������ � ������� �������� ������.
    /// ������������ ��������, ��������� ��� ������ � ��������� �������.
    /// </summary>
    public interface IHighLevelClientCallback : IHighLevelMessageExchangeSystem, IClientCallback
    {
        /// <summary>
        /// ����������� ������ � ������� ������ �����������
        /// </summary>
        /// <param name="message"></param>
        [OperationContract(IsOneWay = true)]
        void ChannelRegister(ChannelSubscribeMessage message);

        /// <summary>
        /// ������ ����������� ������ � ������� ������ �����������
        /// </summary>
        /// <param name="message"></param>
        [OperationContract(IsOneWay = true)]
        void ChannelUnRegister(ChannelSubscribeMessage message);

        /// <summary>
        /// �������� ��� ������ ������ �� ������� ������� (��������� - ������)
        /// </summary>
        /// <param name="message"></param>
        [OperationContract(IsOneWay = true)]
        void SendReadToClient(InternalLogicalChannelDataMessage message);

    }
}
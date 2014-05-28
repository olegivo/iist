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
        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
        void ChannelRegister(ChannelRegistrationMessage message);

        /// <summary>
        /// ������ ����������� ������ � ������� ������ �����������
        /// </summary>
        /// <param name="message"></param>
        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
        void ChannelUnRegister(ChannelRegistrationMessage message);

        /// <summary>
        /// �������� ��� ������ ������ �� ������� ������� (��������� - ������)
        /// </summary>
        /// <param name="message"></param>
        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
        void SendReadToClient(InternalLogicalChannelDataMessage message);

        /// <summary>
        /// �������� ��� ������ ��������� ������ �� ������� ������� (��������� - ������)
        /// </summary>
        /// <param name="message"></param>
        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
        void SendChannelStateToClient(InternalLogicalChannelStateMessage message);

    }
}
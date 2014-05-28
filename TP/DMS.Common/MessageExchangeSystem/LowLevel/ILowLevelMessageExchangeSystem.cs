using System;
using System.ServiceModel;
using DMS.Common.Messages;

namespace DMS.Common.MessageExchangeSystem.LowLevel
{
    /// <summary>
    /// �������� ������� ������ ����������� � ��������� ������� ������
    /// </summary>
    [ServiceContract(CallbackContract = typeof(ILowLevelClientCallback), SessionMode = SessionMode.Required)]
    public interface ILowLevelMessageExchangeSystem : IMessageExchangeSystem
    {
        /// <summary>
        /// ������ ����������� ������ � ������� ������ �����������
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        [OperationContract(AsyncPattern = true, IsInitiating = false, IsTerminating = false)]
        IAsyncResult BeginChannelRegister(ChannelRegistrationMessage message, AsyncCallback callback, object state);

        /// <summary>
        /// ���������� ����������� ������ � ������� ������ �����������
        /// </summary>
        /// <param name="message"></param>
        /// <param name="result"></param>
        void EndChannelRegister(ChannelRegistrationMessage message, IAsyncResult result);

        /// <summary>
        /// ������ ������ ����������� ������ � ������� ������ �����������
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        [OperationContract(AsyncPattern = true, IsInitiating = false, IsTerminating = false)]
        IAsyncResult BeginChannelUnRegister(ChannelRegistrationMessage message, AsyncCallback callback, object state);

        /// <summary>
        /// ���������� ������ ����������� ������ � ������� ������ �����������
        /// </summary>
        /// <param name="message"></param>
        /// <param name="result"></param>
        void EndChannelUnRegister(ChannelRegistrationMessage message, IAsyncResult result);

        /// <summary>
        /// ������ ������ ������ �� ��������������� ������
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [OperationContract(AsyncPattern = true, IsInitiating = false, IsTerminating = false)]
        IAsyncResult BeginReadChannel(InternalLogicalChannelDataMessage message, AsyncCallback callback, object state);


        /// <summary>
        /// ���������� ������ ������ �� ��������������� ������
        /// </summary>
        /// <param name="message"></param>
        /// <param name="result"></param>
        void EndReadChannel(InternalLogicalChannelDataMessage message, IAsyncResult result);

        /// <summary>
        /// ������ ��������� ��������� ��������������� ������
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [OperationContract(AsyncPattern = true, IsInitiating = false, IsTerminating = false)]
        IAsyncResult BeginChangeChannelState(InternalLogicalChannelStateMessage message, AsyncCallback callback, object state);


        /// <summary>
        /// ���������� ��������� ��������� �� ��������������� ������
        /// </summary>
        /// <param name="message"></param>
        /// <param name="result"></param>
        void EndChangeChannelState(InternalLogicalChannelStateMessage message, IAsyncResult result);

    }
}
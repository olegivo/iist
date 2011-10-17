using System;
using System.ServiceModel;
using DMS.Common.Messages;

namespace DMS.Common.MessageExchangeSystem.LowLevel
{
    /// <summary>
    /// �������� ������� ������ ����������� � ��������� ������� ������
    /// </summary>
    [ServiceContract(CallbackContract = typeof(ILowLevelClientCallback))]
    public interface ILowLevelMessageExchangeSystem : IMessageExchangeSystem
    {
        /// <summary>
        /// ������ ����������� ������ � ������� ������ �����������
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        [OperationContract(AsyncPattern = true)]
        IAsyncResult BeginChannelRegister(ChannelSubscribeMessage message, AsyncCallback callback, object state);

        /// <summary>
        /// ���������� ����������� ������ � ������� ������ �����������
        /// </summary>
        /// <param name="message"></param>
        /// <param name="result"></param>
        void EndChannelRegister(ChannelSubscribeMessage message, IAsyncResult result);

        /// <summary>
        /// ������ ������ ����������� ������ � ������� ������ �����������
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        [OperationContract(AsyncPattern = true)]
        IAsyncResult BeginChannelUnRegister(ChannelSubscribeMessage message, AsyncCallback callback, object state);

        /// <summary>
        /// ���������� ������ ����������� ������ � ������� ������ �����������
        /// </summary>
        /// <param name="message"></param>
        /// <param name="result"></param>
        void EndChannelUnRegister(ChannelSubscribeMessage message, IAsyncResult result);

        /// <summary>
        /// ������ ������ ������ �� ��������������� ������
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [OperationContract(AsyncPattern = true)]
        IAsyncResult BeginReadChannel(InternalLogicalChannelDataMessage message, AsyncCallback callback, object state);


        /// <summary>
        /// ���������� ������ ������ �� ��������������� ������
        /// </summary>
        /// <param name="message"></param>
        /// <param name="result"></param>
        void EndReadChannel(InternalLogicalChannelDataMessage message, IAsyncResult result);

    }
}
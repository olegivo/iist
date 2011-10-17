using System;
using System.ServiceModel;
using DMS.Common.Messages;

namespace DMS.Common.MessageExchangeSystem.HighLevel
{
    /// <summary>
    /// �������� ������� ������ ����������� �������� ������.
    /// ������������ ��������, ��������� ��� �������� �������� ������.
    /// �������� ��������� ������ �� ������ � ������� �������� ������ - <c>��.</c> <see cref="IHighLevelClientCallback"/>.
    /// </summary>
    [ServiceContract(CallbackContract = typeof(IHighLevelClientCallback))]
    public interface IHighLevelMessageExchangeSystem : IMessageExchangeSystem
    {
        /// <summary>
        /// �������� ������������������ � ������� ������
        /// </summary>
        /// <param name="message">��������� � �������������� ��������������</param>
        /// <returns></returns>
        [OperationContract]
        int[] GetRegisteredChannels(InternalMessage message);

        /// <summary>
        /// ������ �������� �� ������ ��������������� ������
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        [OperationContract(AsyncPattern = true)]
        IAsyncResult BeginChannelSubscribe(ChannelSubscribeMessage message, AsyncCallback callback, object state);

        /// <summary>
        /// ���������� �������� �� ������ ��������������� ������
        /// </summary>
        /// <param name="message"></param>
        /// <param name="result"></param>
        void EndChannelSubscribe(ChannelSubscribeMessage message, IAsyncResult result);

        /// <summary>
        /// ������ ������� �� ������ ��������������� ������
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        [OperationContract(AsyncPattern = true)]
        IAsyncResult BeginChannelUnSubscribe(ChannelSubscribeMessage message, AsyncCallback callback, object state);

        /// <summary>
        /// ���������� ������� �� ������ ��������������� ������
        /// </summary>
        /// <param name="message"></param>
        /// <param name="result"></param>
        void EndChannelUnSubscribe(ChannelSubscribeMessage message, IAsyncResult result);

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
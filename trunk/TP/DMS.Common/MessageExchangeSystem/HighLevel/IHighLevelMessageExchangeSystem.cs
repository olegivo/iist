using System;
using System.ServiceModel;
using DMS.Common.Messages;

namespace DMS.Common.MessageExchangeSystem.HighLevel
{
    /// <summary>
    /// Контракт системы обмена сообщениями верхнего уровня.
    /// Представляет операции, доступные для клиентов верхнего уровня.
    /// Контракт обратного вызова от службы к клиенту верхнего уровня - <c>см.</c> <see cref="IHighLevelClientCallback"/>.
    /// </summary>
    [ServiceContract(CallbackContract = typeof(IHighLevelClientCallback))]
    public interface IHighLevelMessageExchangeSystem : IMessageExchangeSystem
    {
        /// <summary>
        /// Получить зарегистрированные в системе каналы
        /// </summary>
        /// <param name="message">Сообщение с идентификацией запрашивающего</param>
        /// <returns></returns>
        [OperationContract]
        int[] GetRegisteredChannels(InternalMessage message);

        /// <summary>
        /// Начало подписки на чтение контролируемого канала
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        [OperationContract(AsyncPattern = true)]
        IAsyncResult BeginChannelSubscribe(ChannelSubscribeMessage message, AsyncCallback callback, object state);

        /// <summary>
        /// Завершение подписки на чтение контролируемого канала
        /// </summary>
        /// <param name="message"></param>
        /// <param name="result"></param>
        void EndChannelSubscribe(ChannelSubscribeMessage message, IAsyncResult result);

        /// <summary>
        /// Начало отписки от чтения контролируемого канала
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        [OperationContract(AsyncPattern = true)]
        IAsyncResult BeginChannelUnSubscribe(ChannelSubscribeMessage message, AsyncCallback callback, object state);

        /// <summary>
        /// Завершение отписки от чтения контролируемого канала
        /// </summary>
        /// <param name="message"></param>
        /// <param name="result"></param>
        void EndChannelUnSubscribe(ChannelSubscribeMessage message, IAsyncResult result);

        /// <summary>
        /// Возможность записи в управляемый канал
        /// </summary>
        /// <param name="message"></param>
        void IsAllowWriteChannel(IInternalMessage message);

        /// <summary>
        /// Запись в управляемый канал
        /// </summary>
        /// <param name="message"></param>
        void WriteChannel(IInternalMessage message);
    }
}
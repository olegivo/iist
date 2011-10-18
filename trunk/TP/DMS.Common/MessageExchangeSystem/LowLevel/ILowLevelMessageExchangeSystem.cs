using System;
using System.ServiceModel;
using DMS.Common.Messages;

namespace DMS.Common.MessageExchangeSystem.LowLevel
{
    /// <summary>
    /// Контракт системы обмена сообщениями с клиентами нижнего уровня
    /// </summary>
    [ServiceContract(CallbackContract = typeof(ILowLevelClientCallback))]
    public interface ILowLevelMessageExchangeSystem : IMessageExchangeSystem
    {
        /// <summary>
        /// Начало регистрации канала в системе обмена сообщениями
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        [OperationContract(AsyncPattern = true)]
        IAsyncResult BeginChannelRegister(ChannelRegistrationMessage message, AsyncCallback callback, object state);

        /// <summary>
        /// Завершение регистрации канала в системе обмена сообщениями
        /// </summary>
        /// <param name="message"></param>
        /// <param name="result"></param>
        void EndChannelRegister(ChannelRegistrationMessage message, IAsyncResult result);

        /// <summary>
        /// Начало отмены регистрации канала в системе обмена сообщениями
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        [OperationContract(AsyncPattern = true)]
        IAsyncResult BeginChannelUnRegister(ChannelRegistrationMessage message, AsyncCallback callback, object state);

        /// <summary>
        /// Завершение отмены регистрации канала в системе обмена сообщениями
        /// </summary>
        /// <param name="message"></param>
        /// <param name="result"></param>
        void EndChannelUnRegister(ChannelRegistrationMessage message, IAsyncResult result);

        /// <summary>
        /// Начало чтения данных из контролируемого канала
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [OperationContract(AsyncPattern = true)]
        IAsyncResult BeginReadChannel(InternalLogicalChannelDataMessage message, AsyncCallback callback, object state);


        /// <summary>
        /// Завершение чтения данных из контролируемого канала
        /// </summary>
        /// <param name="message"></param>
        /// <param name="result"></param>
        void EndReadChannel(InternalLogicalChannelDataMessage message, IAsyncResult result);

    }
}
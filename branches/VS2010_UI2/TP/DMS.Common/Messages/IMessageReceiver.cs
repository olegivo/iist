using System;
using System.ServiceModel;
using DMS.Common.Exceptions;

namespace DMS.Common.Messages
{
    /// <summary>
    /// Приёмник сообщений
    /// </summary>
    [ServiceContract(CallbackContract = typeof (IMessageReceiverCallback))]
    public interface IMessageReceiver
    {
        /// <summary>
        /// Послать данному приёмнику сообщений сообщение
        /// </summary>
        /// <param name="message"></param>
        [OperationContract]
        void SendMessage(InternalMessage message);

        /// <summary>
        /// Начало передачи сообщения об ошибке
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        [OperationContract(AsyncPattern = true)]
        [FaultContract(typeof(TestException))]
        [FaultContract(typeof (RegistrationException))]
        IAsyncResult BeginSendError(InternalErrorMessage message, AsyncCallback callback, object state);

        /// <summary>
        /// Завершение передачи сообщения об ошибке
        /// </summary>
        /// <param name="message"></param>
        /// <param name="result"></param>
        void EndSendError(InternalErrorMessage message, IAsyncResult result);
    }
}
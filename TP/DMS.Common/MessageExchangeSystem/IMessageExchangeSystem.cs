using System;
using System.ServiceModel;
using DMS.Common.Exceptions;
using DMS.Common.Messages;

namespace DMS.Common.MessageExchangeSystem
{
    /// <summary>
    /// Контракт системы обмена сообщениями
    /// </summary>
    [ServiceContract(CallbackContract = typeof(IClientCallback), SessionMode = SessionMode.Required)]
    public interface IMessageExchangeSystem : IMessageReceiver
    {
        /// <summary>
        /// Начало регистрации клиента
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        [OperationContract(AsyncPattern = true, IsInitiating = true, IsTerminating = false)]
        [FaultContract(typeof (RegistrationException))]
        IAsyncResult BeginRegister(RegistrationMessage message, AsyncCallback callback, object state);

        /// <summary>
        /// Завершение регистрации клиента
        /// </summary>
        /// <param name="message"></param>
        /// <param name="result"></param>
        void EndRegister(RegistrationMessage message, IAsyncResult result);

        /// <summary>
        /// Начало отмены регистрации клиента
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        [OperationContract(AsyncPattern = true, IsInitiating = false, IsTerminating = false)]
        [FaultContract(typeof (RegistrationException))]
        IAsyncResult BeginUnregister(RegistrationMessage message, AsyncCallback callback, object state);

        /// <summary>
        /// Завершение отмены регистрации клиента
        /// </summary>
        /// <param name="message"></param>
        /// <param name="result"></param>
        void EndUnregister(RegistrationMessage message, IAsyncResult result);

        /// <summary>
        /// Отключение клиента от системы
        /// </summary>
        /// <param name="clientName"></param>
        [OperationContract(AsyncPattern = false, IsInitiating = false, IsTerminating = true)]
        void Disconnect(string clientName);
    }
}

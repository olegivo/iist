using System.ServiceModel;
using DMS.Common.Exceptions;

namespace DMS.Common.Messages
{
    /// <summary>
    /// Обратный вызов для приёмника сообщений
    /// </summary>
    public interface IMessageReceiverCallback : IMessageReceiver
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        [OperationContract]
        [FaultContract(typeof(InternalException))]
        void SendMessageToClient(InternalMessage message);
    }
}
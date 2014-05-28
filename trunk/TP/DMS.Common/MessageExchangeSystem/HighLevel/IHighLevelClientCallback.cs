using System.ServiceModel;
using DMS.Common.Messages;

namespace DMS.Common.MessageExchangeSystem.HighLevel
{
    /// <summary>
    /// Контракт обратного вызова от службы к клиенту верхнего уровня.
    /// Представляет операции, доступные для вызова с серверной стороны.
    /// </summary>
    public interface IHighLevelClientCallback : IHighLevelMessageExchangeSystem, IClientCallback
    {
        /// <summary>
        /// Регистрация канала в системе обмена сообщениями
        /// </summary>
        /// <param name="message"></param>
        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
        void ChannelRegister(ChannelRegistrationMessage message);

        /// <summary>
        /// Отмена регистрации канала в системе обмена сообщениями
        /// </summary>
        /// <param name="message"></param>
        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
        void ChannelUnRegister(ChannelRegistrationMessage message);

        /// <summary>
        /// Операция для чтения канала от сервера клиенту (инициатор - сервер)
        /// </summary>
        /// <param name="message"></param>
        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
        void SendReadToClient(InternalLogicalChannelDataMessage message);

        /// <summary>
        /// Операция для чтения состояния канала от сервера клиенту (инициатор - сервер)
        /// </summary>
        /// <param name="message"></param>
        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
        void SendChannelStateToClient(InternalLogicalChannelStateMessage message);

    }
}
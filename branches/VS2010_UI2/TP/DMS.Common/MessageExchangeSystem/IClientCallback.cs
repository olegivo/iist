
using DMS.Common.Messages;

namespace DMS.Common.MessageExchangeSystem
{
    /// <summary>
    /// Контракт обратного вызова от службы к клиенту (верхнего и нижнего уровня).
    /// Представляет операции, доступные для вызова с серверной стороны.
    /// </summary>
    public interface IClientCallback : IMessageExchangeSystem, IMessageReceiverCallback
    {
        
    }
}
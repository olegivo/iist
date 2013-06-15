using DMS.Common.Messages;

namespace DMS.Common.Clients
{
    /// <summary>
    /// Клиент (нижнего уровня) системы обмена сообщениями
    /// </summary>
    public interface ILowLevelMesClient : IMesClient
    {
        /// <summary>
        /// Подписка чтения контролируемого канала
        /// </summary>
        /// <param name="message"></param>
        void ChannelSubscribe(IInternalMessage message);

        /// <summary>
        /// Отписка от чтения контролируемого канала
        /// </summary>
        /// <param name="message"></param>
        void ChannelUnSubscribe(IInternalMessage message);

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
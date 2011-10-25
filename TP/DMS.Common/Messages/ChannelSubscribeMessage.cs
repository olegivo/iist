using System.Runtime.Serialization;

namespace DMS.Common.Messages
{
    /// <summary>
    /// Сообщение для подписки / отписки от канала
    /// </summary>
    public class ChannelSubscribeMessage : InternalServiceMessage
    {
        protected ChannelSubscribeMessage()
        {
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="regNameFrom">Регистрационое имя, от которого посылается сообщение</param>
        /// <param name="regNameTo">Регистрационое имя, которому посылается сообщение</param>
        /// <param name="mode">Режим регистрации (регистрация/отмена)</param>
        /// <param name="logicalChannelId">Номер логического канала</param>
        public ChannelSubscribeMessage(string regNameFrom, string regNameTo, SubscribeMode mode, int logicalChannelId) : base(regNameFrom, regNameTo)
        {
            Mode = mode;
            LogicalChannelId = logicalChannelId;
        }

        /// <summary>
        /// Подписка / отписка от канала
        /// </summary>
        [DataMember]
        public SubscribeMode Mode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int LogicalChannelId { get; set; }
    }
}
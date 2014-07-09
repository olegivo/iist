using System.Runtime.Serialization;

namespace DMS.Common.Messages
{
    /// <summary>
    /// Сообщение для подписки / отписки от канала
    /// </summary>
    public class ChannelSubscribeMessage : InternalServiceChannelMessage
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
        public ChannelSubscribeMessage(string regNameFrom, string regNameTo, SubscribeMode mode, int logicalChannelId)
            : base(regNameFrom, regNameTo, logicalChannelId)
        {
            Mode = mode;
        }

        /// <summary>
        /// Подписка / отписка от канала
        /// </summary>
        [DataMember]
        public SubscribeMode Mode { get; set; }

        public override EventType EventType
        {
            get { return EventType.ChannelSubscription; }
        }

        protected override string GetMessageType()
        {
            return "Подписка канала";
        }

        protected override string GetMessageDescription()
        {
            return string.Format("{0}, канал №{1}, подписка - {2}", base.GetMessageDescription(), LogicalChannelId, Mode);
        }
    }
}
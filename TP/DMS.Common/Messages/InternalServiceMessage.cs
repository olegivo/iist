using System.Runtime.Serialization;

namespace DMS.Common.Messages
{
    /// <summary>
    /// Сообщение, содержащее управляющую информацию
    /// </summary>
    public abstract class InternalServiceMessage : InternalMessage
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="regNameFrom">Регистрационое имя, от которого посылается сообщение</param>
        /// <param name="regNameTo">Регистрационое имя, которому посылается сообщение</param>
        protected InternalServiceMessage(string regNameFrom, string regNameTo)
            : base(regNameFrom, regNameTo)
        {
        }

        protected InternalServiceMessage()
        {
            
        }

        [DataMember]
        public abstract EventType EventType { get; }
    }
}
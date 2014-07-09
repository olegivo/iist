using System.Runtime.Serialization;
using System.Text;

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

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            if (!string.IsNullOrEmpty(RegNameFrom)) sb.AppendFormat(" from [{0}]", RegNameFrom);
            if (!string.IsNullOrEmpty(RegNameTo)) sb.AppendFormat(" to [{0}]", RegNameTo);
            if (sb.Length == 0) sb.AppendFormat(" ({0})", GetMessageType());
            sb.Append(GetMessageDescription());
            sb.Insert(0, "Message");
            return sb.ToString();
        }

        protected virtual string GetMessageType()
        {
            return GetType().Name;
        }

        protected virtual string GetMessageDescription()
        {
            return "";
        }
    }
}
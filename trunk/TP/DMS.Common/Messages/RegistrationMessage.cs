using System.Runtime.Serialization;

namespace DMS.Common.Messages
{
    /// <summary>
    /// Сообщение для регистрации/отмены регистрации
    /// </summary>
    public class RegistrationMessage : InternalServiceMessage
    {
        /// <summary>
        /// Регистрация (<see langword="true"/>) / отмена регистрации (<see langword="false"/>)
        /// </summary>
        [DataMember]
        public RegistrationMode Mode { get; set; }
    }
}
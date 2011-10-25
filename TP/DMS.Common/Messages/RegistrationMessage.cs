using System.Runtime.Serialization;

namespace DMS.Common.Messages
{
    /// <summary>
    /// Сообщение для регистрации/отмены регистрации
    /// </summary>
    public class RegistrationMessage : InternalServiceMessage
    {
        protected RegistrationMessage()
        {
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="regNameFrom">Регистрационое имя, от которого посылается сообщение</param>
        /// <param name="regNameTo">Регистрационое имя, которому посылается сообщение</param>
        /// <param name="registrationMode">Режим регистрации (регистрация/отмена)</param>
        /// <param name="dataMode">Режим данных (чтение/запись)</param>
        public RegistrationMessage(string regNameFrom, string regNameTo, RegistrationMode registrationMode, DataMode dataMode) : base(regNameFrom, regNameTo)
        {
            RegistrationMode = registrationMode;
            DataMode = dataMode;
        }

        /// <summary>
        /// Регистрация / отмена регистрации
        /// </summary>
        [DataMember]
        public RegistrationMode RegistrationMode { get; set; }

        /// <summary>
        /// Режим данных (чтение/запись)
        /// </summary>
        [DataMember]
        public DataMode DataMode { get; set; }
    }
}
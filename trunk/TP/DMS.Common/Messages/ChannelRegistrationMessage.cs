using System.Runtime.Serialization;

namespace DMS.Common.Messages
{
    /// <summary>
    /// Сообщение для регистрации/отмены регистрации канала
    /// </summary>
    public class ChannelRegistrationMessage : RegistrationMessage
    {
        protected ChannelRegistrationMessage()
        {
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="regNameFrom">Регистрационое имя, от которого посылается сообщение</param>
        /// <param name="regNameTo">Регистрационое имя, которому посылается сообщение</param>
        /// <param name="registrationMode">Режим регистрации (регистрация/отмена)</param>
        /// <param name="dataMode">Режим данных (чтение/запись)</param>
        /// <param name="logicalChannelId">Номер логического канала</param>
        public ChannelRegistrationMessage
            (
                string regNameFrom,
                string regNameTo,
                RegistrationMode registrationMode,
                DataMode dataMode,
                int logicalChannelId
            )
            : base(regNameFrom, regNameTo, registrationMode, dataMode)
        {
            LogicalChannelId = logicalChannelId;
            //TODO:добавить параметры для инициализации других свойств
        }

        /// <summary>
        /// Номер канала
        /// </summary>
        [DataMember]
        public int LogicalChannelId { get; set; }

        /// <summary>
        /// Минимальное допустимое значение для канала
        /// </summary>
        [DataMember]
        public double? MinValue { get; set; }

        /// <summary>
        /// Максимальное допустимое значение для канала
        /// </summary>
        [DataMember]
        public double? MaxValue { get; set; }

        /// <summary>
        /// Минимальное нормальное значение для канала
        /// </summary>
        [DataMember]
        public double? MinNormalValue { get; set; }

        /// <summary>
        /// Максимальное нормальное значение для канала
        /// </summary>
        [DataMember]
        public double? MaxNormalValue { get; set; }

        ///<summary>
        /// Описание канала
        ///</summary>
        [DataMember]
        public string Description { get; set; }

    }
}
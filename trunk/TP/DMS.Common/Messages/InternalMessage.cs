using System;
using System.Runtime.Serialization;

namespace DMS.Common.Messages
{
    /// <summary>
    /// Внутреннее сообщение
    /// </summary>
    [DataContract]
    [KnownType(typeof(RegistrationMessage))]
    [KnownType(typeof(ChannelSubscribeMessage))]
    [KnownType(typeof(InternalLogicalChannelDataMessage))]
    [KnownType(typeof(InternalErrorMessage))]
    public class InternalMessage : IInternalMessage
    {
        #region constructors
        /// <summary>
        /// Конструктор
        /// </summary>
        public InternalMessage(/*[Optional,DefaultParameterValue(null)] DateTime timeStamp*/)
        {
            //если временная метка не указана, она создаётся внутри конструктора как текущее время
            TimeStamp = /*DateTime.MinValue > timeStamp ? timeStamp : */DateTime.Now;
        }

        #endregion

        #region properties
        /// <summary>
        /// Временная метка создания сообщения
        /// </summary>
        [DataMember]
        public DateTime TimeStamp { get; private set; }

        /// <summary>
        /// Режим данных
        /// </summary>
        [DataMember]
        public DataMode DataMode { get; set; }

        /// <summary>
        /// Наименование пославшего сообщение
        /// </summary>
        [DataMember]
        public string RegName { get; set; }

        #endregion

    }
}
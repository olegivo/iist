using System;
using System.Runtime.Serialization;

namespace DMS.Common.Messages
{
    /// <summary>
    /// Внутреннее сообщение
    /// </summary>
    [DataContract]
    [KnownType(typeof(RegistrationMessage))]
    [KnownType(typeof(ChannelRegistrationMessage))]
    [KnownType(typeof(ChannelSubscribeMessage))]
    [KnownType(typeof(InternalLogicalChannelDataMessage))]
    [KnownType(typeof(InternalLogicalChannelStateMessage))]
    [KnownType(typeof(InternalErrorMessage))]
    public class InternalMessage : IInternalMessage, ICloneable
    {
        #region constructors

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="regNameFrom">Регистрационое имя, от которого посылается сообщение</param>
        /// <param name="regNameTo">Регистрационое имя, которому посылается сообщение</param>
        public InternalMessage(string regNameFrom, string regNameTo)
        {
            RegNameFrom = regNameFrom;
            RegNameTo = regNameTo;
            //если временная метка не указана, она создаётся внутри конструктора как текущее время
            TimeStamp = /*DateTime.MinValue > timeStamp ? timeStamp : */DateTime.Now;
        }

        protected InternalMessage()
        {
            
        }

        #endregion

        #region properties
        /// <summary>
        /// Временная метка создания сообщения
        /// </summary>
        [DataMember]
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// Регистрационое имя, от которого посылается сообщение
        /// </summary>
        [DataMember]
        public string RegNameFrom { get; set; }

        /// <summary>
        /// Регистрационое имя, которому посылается сообщение
        /// </summary>
        [DataMember]
        public string RegNameTo { get; set; }

        #endregion

        public virtual object Clone()
        {
            return new InternalMessage(RegNameFrom, RegNameTo) {TimeStamp = TimeStamp};
        }
    }
}
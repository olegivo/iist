using System;
using System.Runtime.Serialization;

namespace DMS.Common.Messages
{
    /// <summary>
    /// Внутреннее сообщение, передающее ошибку
    /// </summary>
    public class InternalErrorMessage : InternalServiceMessage
    {
        protected InternalErrorMessage() : base()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="regNameFrom">Регистрационое имя, от которого посылается сообщение</param>
        /// <param name="regNameTo">Регистрационое имя, которому посылается сообщение</param>
        private InternalErrorMessage(string regNameFrom, string regNameTo) : base(regNameFrom, regNameTo)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="regNameFrom">Регистрационое имя, от которого посылается сообщение</param>
        /// <param name="regNameTo">Регистрационое имя, которому посылается сообщение</param>
        /// <param name="error"></param>
        public InternalErrorMessage(string regNameFrom, string regNameTo, Exception error)
            : this(regNameFrom, regNameTo)
        {
            Error = error.ToString();
        }

        /// <summary>
        /// Ошибка
        /// </summary>
        [DataMember]
        public string Error { get; set; }

    }
}
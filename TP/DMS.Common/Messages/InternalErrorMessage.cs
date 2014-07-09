using System;
using System.Runtime.Serialization;

namespace DMS.Common.Messages
{
    /// <summary>
    /// Внутреннее сообщение, передающее ошибку
    /// </summary>
    public class InternalErrorMessage : InternalServiceMessage
    {
        protected InternalErrorMessage()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="regNameFrom">Регистрационое имя, от которого посылается сообщение</param>
        /// <param name="regNameTo">Регистрационое имя, которому посылается сообщение</param>
        /// <param name="error"></param>
        public InternalErrorMessage(string regNameFrom, string regNameTo, Exception error)
            : base(regNameFrom, regNameTo)
        {
            Error = error.Message;
            StackTrace = error.StackTrace;
        }

        /// <summary>
        /// Текст ошибки
        /// </summary>
        [DataMember]
        public string Error { get; set; }

        /// <summary>
        /// Стек исключения
        /// </summary>
        [DataMember]
        public string StackTrace { get; set; }

        public override EventType EventType
        {
            get { return EventType.Error; }
        }

        protected override string GetMessageType()
        {
            return "Ошибка";
        }

        protected override string GetMessageDescription()
        {
            return Error;
        }
    }
}
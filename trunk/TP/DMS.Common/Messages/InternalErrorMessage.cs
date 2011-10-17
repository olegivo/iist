using System;
using System.Runtime.Serialization;
using DMS.Common.Exceptions;

namespace DMS.Common.Messages
{
    /// <summary>
    /// Внутреннее сообщение, передающее ошибку
    /// </summary>
    [KnownType(typeof(ArgumentOutOfRangeException))]
    [KnownType(typeof(TestException))]
    public class InternalErrorMessage : InternalServiceMessage
    {
        /// <summary>
        /// 
        /// </summary>
        private InternalErrorMessage()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="error"></param>
        public InternalErrorMessage(Exception error):this()
        {
            Error = error;
        }

        /// <summary>
        /// Ошибка
        /// </summary>
        [DataMember]
        public Exception Error { get; set; }

    }
}
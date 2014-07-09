using System;
using System.Runtime.Serialization;

namespace DMS.Common.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class InternalServiceException : InternalException
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.ApplicationException"/>.
        /// </summary>
        public InternalServiceException()
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.ApplicationException"/>, используя указанное сообщение об ошибке.
        /// </summary>
        /// <param name="message">Сообщение, описывающее ошибку. </param>
        public InternalServiceException(string message) : base(message)
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.ApplicationException"/> указанным сообщением об ошибке и ссылкой на внутреннее исключение, которое стало причиной данного исключения.
        /// </summary>
        /// <param name="message">Сообщение об ошибке с объяснением причины исключения. </param><param name="innerException">Исключение, являющееся причиной текущего исключения. Если параметр <paramref name="innerException"/> не является указателем NULL, текущее исключение вызывается в блоке catch, обрабатывающем внутренние исключения. </param>
        public InternalServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.ApplicationException"/> сериализованными данными.
        /// </summary>
        /// <param name="info">Объект, содержащий сериализованные данные объекта. </param><param name="context">Контекстные сведения об источнике или назначении. </param>
        protected InternalServiceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
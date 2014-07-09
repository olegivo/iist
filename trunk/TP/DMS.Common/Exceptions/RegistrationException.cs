using System;
using System.Runtime.Serialization;

namespace DMS.Common.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class RegistrationException : InternalServiceException
    {
        /// <summary>
        /// »нициализирует новый экземпл€р класса <see cref="T:System.ApplicationException"/>.
        /// </summary>
        public RegistrationException()
        {
        }

        /// <summary>
        /// »нициализирует новый экземпл€р класса <see cref="T:System.ApplicationException"/>, использу€ указанное сообщение об ошибке.
        /// </summary>
        /// <param name="message">—ообщение, описывающее ошибку. </param>
        public RegistrationException(string message) : base(message)
        {
        }

        /// <summary>
        /// »нициализирует новый экземпл€р класса <see cref="T:System.ApplicationException"/> указанным сообщением об ошибке и ссылкой на внутреннее исключение, которое стало причиной данного исключени€.
        /// </summary>
        /// <param name="message">—ообщение об ошибке с объ€снением причины исключени€. </param><param name="innerException">»сключение, €вл€ющеес€ причиной текущего исключени€. ≈сли параметр <paramref name="innerException"/> не €вл€етс€ указателем NULL, текущее исключение вызываетс€ в блоке catch, обрабатывающем внутренние исключени€. </param>
        public RegistrationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// »нициализирует новый экземпл€р класса <see cref="T:System.ApplicationException"/> сериализованными данными.
        /// </summary>
        /// <param name="info">ќбъект, содержащий сериализованные данные объекта. </param><param name="context"> онтекстные сведени€ об источнике или назначении. </param>
        protected RegistrationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
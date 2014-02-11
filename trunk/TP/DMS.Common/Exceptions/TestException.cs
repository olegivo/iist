using System;

namespace DMS.Common.Exceptions
{
    /// <summary>
    /// Тестовое исключение
    /// </summary>
    [Serializable]
    public class TestException : InternalException
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.ApplicationException"/>, используя указанное сообщение об ошибке.
        /// </summary>
        private TestException()
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.ApplicationException"/>, используя указанное сообщение об ошибке.
        /// </summary>
        /// <param name="message">Сообщение, описывающее ошибку. </param>
        public TestException(string message) : base(message)
        {
        }
    }
}
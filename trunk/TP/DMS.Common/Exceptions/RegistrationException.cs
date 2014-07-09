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
        /// �������������� ����� ��������� ������ <see cref="T:System.ApplicationException"/>.
        /// </summary>
        public RegistrationException()
        {
        }

        /// <summary>
        /// �������������� ����� ��������� ������ <see cref="T:System.ApplicationException"/>, ��������� ��������� ��������� �� ������.
        /// </summary>
        /// <param name="message">���������, ����������� ������. </param>
        public RegistrationException(string message) : base(message)
        {
        }

        /// <summary>
        /// �������������� ����� ��������� ������ <see cref="T:System.ApplicationException"/> ��������� ���������� �� ������ � ������� �� ���������� ����������, ������� ����� �������� ������� ����������.
        /// </summary>
        /// <param name="message">��������� �� ������ � ����������� ������� ����������. </param><param name="innerException">����������, ���������� �������� �������� ����������. ���� �������� <paramref name="innerException"/> �� �������� ���������� NULL, ������� ���������� ���������� � ����� catch, �������������� ���������� ����������. </param>
        public RegistrationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// �������������� ����� ��������� ������ <see cref="T:System.ApplicationException"/> ���������������� �������.
        /// </summary>
        /// <param name="info">������, ���������� ��������������� ������ �������. </param><param name="context">����������� �������� �� ��������� ��� ����������. </param>
        protected RegistrationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
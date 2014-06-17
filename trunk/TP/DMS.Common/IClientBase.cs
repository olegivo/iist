using System;
using System.ServiceModel;

namespace DMS.Common
{
    public interface IClientBase : IDisposable, IErrorSender
    {
        /// <summary></summary>
        Func<string> GetRegName { get; set; }

        ICommunicationObject Proxy { get; }

        /// <summary>
        /// ������� � ������� ������ ����������� ��������� � �����������
        /// </summary>
        void Register();

        void AbortProxy();
    }
}
using System;
using System.ComponentModel;
using Oleg_ivo.Tools.ExceptionCatcher;

namespace DMS.Common
{
    /// <summary>
    /// ���������, �������������� ����������� ��������� �� �������
    /// </summary>
    public interface IErrorSender
    {
        event EventHandler<AsyncCompletedEventArgs> SendErrorCompleted;
        bool IsCommunicationFailed { get; }
        bool IsRegistered { get; }
        void SendErrorAsync(ExtendedThreadExceptionEventArgs e);
    }
}
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
        void SendErrorAsync(ExtendedThreadExceptionEventArgs e);
    }
}
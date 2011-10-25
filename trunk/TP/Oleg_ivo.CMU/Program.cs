using System;
using System.Windows.Forms;
using DMS.Common.Messages;
using Oleg_ivo.LowLevelClient;
using Oleg_ivo.Tools.ConnectionProvider;
using Oleg_ivo.Tools.ExceptionCatcher;

namespace Oleg_ivo.CMU
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
#pragma warning disable 168
            ExceptionHandler exceptionHandler = new ExceptionHandler(LogError);
#pragma warning restore 168
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DbConnectionProvider.Instance.SetupConnectionStringFromConfigurationFile();

            Application.Run(new LowLevelClientForm());
        }

        private static void LogError(object sender, ExtendedThreadExceptionEventArgs e)
        {
            ControlManagementUnit.Proxy.SendErrorCompleted += Proxy_SendErrorCompleted;
            try
            {
                //TODO: заполнить RegNameFrom
                ControlManagementUnit.Proxy.SendErrorAsync(new InternalErrorMessage(null, null, e.Exception), e);
                if (e.Exception is ArgumentOutOfRangeException)
                    e.ShowError = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static void Proxy_SendErrorCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
/*
 * TODO: если не удалось передать ошибку службе обмена сообщениями, выбрасывать ошибку здесь?
            Proxy.SendErrorCompleted -= Proxy_SendErrorCompleted;
            if(e.Error!=null)
            {
                ExtendedThreadExceptionEventArgs args = e.UserState as ExtendedThreadExceptionEventArgs;
            }
*/
        }
    }
}
using System;
using System.Windows.Forms;
using DMS.Common.Exceptions;
using DMS.Common.Messages;
using Oleg_ivo.Tools.ExceptionCatcher;

namespace Oleg_ivo.HighLevelClient.UI
{
    static class Program
    {
        private static GetRegNameDelegate GetRegName;
        private delegate string GetRegNameDelegate();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
#pragma warning disable 168
            ExceptionHandler exceptionHandler = new ExceptionHandler(LogError);
#pragma warning restore 168
            //TODO: ClientProvider.Instance.InitWithConfigFile("");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var form = new HighLevelClientForm();
            GetRegName = form.GetRegName;
            Application.Run(form);
        }


        private static void LogError(object sender, ExtendedThreadExceptionEventArgs e)
        {
            if (!(e.Exception is System.ServiceModel.FaultException))
                return;
            ClientProvider.Instance.Proxy.SendErrorCompleted += Proxy_SendErrorCompleted;
            try
            {
                ClientProvider.Instance.Proxy.SendErrorAsync(new InternalErrorMessage(GetRegName(), null, e.Exception), e);
                if (e.Exception is TestException)
                    e.ShowError = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static void Proxy_SendErrorCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            ClientProvider.Instance.Proxy.SendErrorCompleted -= Proxy_SendErrorCompleted;
            if(e.Error!=null)
            {
                //TODO: если не удалось передать ошибку службе обмена сообщениями, выбрасывать ошибку здесь?
                //ExtendedThreadExceptionEventArgs args = e.UserState as ExtendedThreadExceptionEventArgs;
            }
        }

    }
}
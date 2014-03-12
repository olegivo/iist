using System;
using Oleg_ivo.Tools.ExceptionCatcher;

namespace DMS.Common
{
    public class ErrorSenderWrapper<T> where T : IErrorSender
    {
        private readonly Func<T> errorSenderProvider;
        private T errorSender;

        public ErrorSenderWrapper(T errorSender)
        {
            this.errorSender = errorSender;
        }

        public ErrorSenderWrapper(Func<T> errorSenderProvider)
        {
            this.errorSenderProvider = errorSenderProvider;
        }

        protected T ErrorSender
        {
            get
            {
                return errorSender != null ? errorSender : (errorSender = errorSenderProvider());
            }
        }

        public void LogError(object sender, ExtendedThreadExceptionEventArgs e)
        {
            //if (!(e.Exception is FaultException))
            //    return;
            ErrorSender.SendErrorCompleted += ErrorSender_SendErrorCompleted;
            try
            {
                //TODO: заполнить RegNameFrom
                ErrorSender.SendErrorAsync(e);
                if (e.Exception is ArgumentOutOfRangeException)
                    e.ShowError = false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        void ErrorSender_SendErrorCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            /*
             * TODO: если не удалось передать ошибку службе обмена сообщениями, выбрасывать ошибку здесь?
                        ErrorSender.SendErrorCompleted -= ErrorSender_SendErrorCompleted;
                        if(e.Error!=null)
                        {
                            ExtendedThreadExceptionEventArgs args = e.UserState as ExtendedThreadExceptionEventArgs;
                        }
            */
        }
    }
}
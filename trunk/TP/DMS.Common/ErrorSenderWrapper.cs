using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Security;
using NLog;
using Oleg_ivo.Tools.ExceptionCatcher;

namespace DMS.Common
{
    [SecuritySafeCritical]
    public class ErrorSenderWrapper<T> : IDisposable where T : class, IErrorSender
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        private readonly Func<T> errorSenderProvider;
        private T errorSender;

        public ErrorSenderWrapper(T errorSender) : this()
        {
            this.errorSender = errorSender;
        }

        public ErrorSenderWrapper(Func<T> errorSenderProvider) : this()
        {
            this.errorSenderProvider = errorSenderProvider;
        }

        private ErrorSenderWrapper()
        {
            replaySubject = new ReplaySubject<QueueItem>(50);
            replaySubject.Delay(TimeSpan.FromSeconds(3)).Subscribe(
                SendError,
                exception => Log.Error("Очередь обработки ошибок остановлена из-за ошибки", exception),
                () => Log.Debug("Очередь обработки ошибок остановлена"));
        }

        protected T ErrorSender
        {
            get
            {
                if (errorSender == null)
                {
                    errorSender = errorSenderProvider();
                    errorSender.SendErrorCompleted += ErrorSender_SendErrorCompleted;
                }
                return errorSender;
            }
        }

        //private readonly ConcurrentQueue<ExtendedThreadExceptionEventArgs> queue = new ConcurrentQueue<ExtendedThreadExceptionEventArgs>();
        private IDisposable disposable;
        private readonly ReplaySubject<QueueItem> replaySubject;

        private void SendError(QueueItem queueItem)
        {
            var reEnqueue = false;
            try
            {
                Log.Trace("Отправка ошибки на сервер (попытка №{0}). Ошибка впервые создана {1}.", ++queueItem.Times, queueItem.ThrowTime);
                if (ErrorSender.IsCommunicationFailed)
                {
                    Log.Warn("Канал связи нарушен. Ошибка повторно добавлена в очередь.");
                    reEnqueue = true;
                }
                else if(!ErrorSender.IsRegistered)
                {
                    Log.Warn("Канал связи не инициализирован. Ошибка повторно добавлена в очередь.");
                    reEnqueue = true;
                }
                else
                {
                    //TODO: заполнить RegNameFrom
                    ErrorSender.SendErrorAsync(queueItem.E);
                    Log.Trace("Ошибка отправлена на сервер, скоро он должен сообщить, принял ли он её");
                }
            }
            catch (Exception ex)
            {
                Log.Warn(
                    "При попытке отправки сообщений на сервер произошла ошибка. Ошибка повторно добавлена в очередь.",
                    ex);
                reEnqueue = true;
            }
            finally
            {
                if(reEnqueue) 
                    Enqueue(queueItem);
            }
        }

        private class QueueItem
        {
            public readonly ExtendedThreadExceptionEventArgs E;
            public readonly DateTime ThrowTime;
            public int Times;

            public QueueItem(ExtendedThreadExceptionEventArgs e)
            {
                ThrowTime = DateTime.Now;
                E = e;
            }
        }

        //private enum QueueItemState
        //{
            
        //}

        public void LogError(object sender, ExtendedThreadExceptionEventArgs e)//TODO: переименовать метод
        {
            Log.Trace("Добавление ошибки в очередь отправки на сервер");
            e.ShowError = false;
            //if(!ErrorSender.IsCommunicationFailed)
                Enqueue(new QueueItem(e));
            //else
            //{
            //    Observable.
            //    //Task.Factory.StartNew(() => )
            //}

            //queue.Enqueue(e);
            /*lock (this)
            {
                if (disposable == null)
                    disposable =
                        Observable.Interval(TimeSpan.FromSeconds(20))
                            .Where(l => !queue.IsEmpty)
                            .Subscribe(l => ProcessQueue());
            }*/
        }

        private void Enqueue(QueueItem item)
        {
            replaySubject.OnNext(item);
        }
/*
        private void ProcessQueue()
        {
            SendError(e);
        }
*/

        void ErrorSender_SendErrorCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            //если не удалось передать ошибку службе обмена сообщениями, ещё раз передаём
            if (e.Error != null)
            {
                Log.Warn("Не удалось передать ошибку на сервер. Повторное добавление в очередь.");
                var args = e.UserState as ExtendedThreadExceptionEventArgs;
                replaySubject.OnNext(new QueueItem(args));
            }
        }

        public void Dispose()
        {
            if (disposable == null) return;
            ErrorSender.SendErrorCompleted -= ErrorSender_SendErrorCompleted;
            disposable.Dispose();
            disposable = null;
        }
    }
}
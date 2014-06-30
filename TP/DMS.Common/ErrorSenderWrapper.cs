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
                exception => Log.Error("������� ��������� ������ ����������� ��-�� ������", exception),
                () => Log.Debug("������� ��������� ������ �����������"));
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
                Log.Trace("�������� ������ �� ������ (������� �{0}). ������ ������� ������� {1}.", ++queueItem.Times, queueItem.ThrowTime);
                if (ErrorSender.IsCommunicationFailed)
                {
                    Log.Warn("����� ����� �������. ������ �������� ��������� � �������.");
                    reEnqueue = true;
                }
                else if(!ErrorSender.IsRegistered)
                {
                    Log.Warn("����� ����� �� ���������������. ������ �������� ��������� � �������.");
                    reEnqueue = true;
                }
                else
                {
                    //TODO: ��������� RegNameFrom
                    ErrorSender.SendErrorAsync(queueItem.E);
                    Log.Trace("������ ���������� �� ������, ����� �� ������ ��������, ������ �� �� �");
                }
            }
            catch (Exception ex)
            {
                Log.Warn(
                    "��� ������� �������� ��������� �� ������ ��������� ������. ������ �������� ��������� � �������.",
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

        public void LogError(object sender, ExtendedThreadExceptionEventArgs e)//TODO: ������������� �����
        {
            Log.Trace("���������� ������ � ������� �������� �� ������");
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
            //���� �� ������� �������� ������ ������ ������ �����������, ��� ��� �������
            if (e.Error != null)
            {
                Log.Warn("�� ������� �������� ������ �� ������. ��������� ���������� � �������.");
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
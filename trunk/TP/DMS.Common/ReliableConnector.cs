using System;
using System.Reactive.Linq;
using System.Security;
using System.ServiceModel;
using System.Timers;
using NLog;

namespace DMS.Common
{
    [SecuritySafeCritical]
    public class ReliableConnector
    {
        private readonly IClientBase clientBase;
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        private Timer reconnectTimer;
        private ICommunicationObject proxy;
        private IDisposable disposable;
        private DateTime start;
        private int reconnectsCount;

        public ReliableConnector(IClientBase clientBase)
        {
            this.clientBase = clientBase;
        }

        private void Run()
        {
            Log.Debug("Запуск таймеров переподключения ({0})", clientBase.GetRegName());
            //TODO:Timers to RX
            reconnectTimer = new Timer(5000) {Enabled = false};
            reconnectTimer.Elapsed += reconnectTimer_Elapsed;

            start = DateTime.Now;
            disposable = Observable.Interval(TimeSpan.FromSeconds(10)).Subscribe(l => CheckAlive());
        }

        private bool repairing;

        private void CheckAlive()
        {
            lock (this)
            {
                if (repairing) return;
                //Log.Trace("CheckAlive");
                Log.Debug("Ping from {0}. Продолжительность полёта - {1}. Переподключений: {2}", clientBase.GetRegName(), DateTime.Now - start, reconnectsCount);
                if (IsNeedRepair)
                {
                    try
                    {
                        RepairConnection();
                    }
                    finally
                    {
                        reconnectsCount++;
                        repairing = false;
                    }
                }
            }
        }

        private bool IsNeedRepair
        {
            get
            {
                Log.Trace("Is need repair connection? ({0})", clientBase.GetRegName());
                if (clientBase.Proxy == null)
                {
                    Log.Trace("clientBase.Proxy == null ({0})", clientBase.GetRegName());
                    return true;
                }
                if (clientBase.Proxy.State == CommunicationState.Faulted)
                {
                    Log.Trace("clientBase.Proxy.State == CommunicationState.Faulted ({0})", clientBase.GetRegName());
                    return true;
                }
                Log.Trace("No, it's OK ({0})", clientBase.GetRegName());
                return false;
            }
        }

        private void reconnectTimer_Elapsed(object sender, EventArgs e)
        {
            Log.Trace("reconnectTimer_Elapsed ({0})", clientBase.GetRegName());
            if (clientBase.Proxy == null)
            {
                Log.Debug("Прокси не создан. Запуск регистрации на сервере, которая создаст прокси. ({0})", clientBase.GetRegName());
                clientBase.Register();
            }

            if (clientBase.Proxy != null)
            {
                Log.Debug("Регистрация на сервере завершена. Остановка цикла восстановления связи. ({0})", clientBase.GetRegName());
                reconnectTimer.Stop();
                //_keepAliveTimer.Start();
            }
        }

        public void SetProxy<T>(ClientBase<T> value) where T : class
        {
            if (proxy == value) return;
            Log.Trace("Установка нового прокси ({0})", clientBase.GetRegName());
            if (proxy != null)
            {
                Log.Trace("Отписка старого прокси от InnerChannel_Faulted ({0})", clientBase.GetRegName());
                ((ClientBase<T>)proxy).InnerChannel.Faulted -= InnerChannel_Faulted;
            }
            proxy = value;
            if (proxy != null)
            {
                Log.Trace("Подписка нового прокси на InnerChannel_Faulted ({0})", clientBase.GetRegName());
                Log.Trace("HashCode={0}", ((ClientBase<T>)proxy).InnerChannel.GetHashCode());
                ((ClientBase<T>)proxy).InnerChannel.Faulted += InnerChannel_Faulted;
                if (reconnectTimer == null) Run();//перед первой попыткой коннекта - запускаем таймеры
            }
        }

        private void InnerChannel_Faulted(object sender, EventArgs e)
        {
            Log.Trace("InnerChannel_Faulted ({0})", clientBase.GetRegName());
            Log.Trace("HashCode={0} ({1})", sender.GetHashCode(), clientBase.GetRegName());
            RepairConnection();
        }

        private void RepairConnection()
        {
            Log.Error("Связь с сервером нарушена ({0})", clientBase.GetRegName());
            //The proxy channel should no longer be used
            clientBase.AbortProxy();//TODO:проверить, что клиент принимает все меры, чтобы соответствовать состоянию нарушенной связи, т.е. приостанавливает работу

            //Enable the try again timer and attempt to reconnect
            Log.Debug("Запуск цикла восстановления связи с сервером ({0})", clientBase.GetRegName());
            reconnectTimer.Start();
        }
    }
}
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
        private readonly Timer reconnectTimer;
        private ICommunicationObject proxy;
        private IDisposable disposable;
        private readonly DateTime start;
        private int reconnectsCount;

        public ReliableConnector(IClientBase clientBase)
        {
            this.clientBase = clientBase;
            //TODO:Timers to RX
            reconnectTimer = new Timer(5000);
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
                Log.Debug("Ping from {0}. ����������������� ����� - {1}. ���������������: {2}", clientBase.GetRegName(), DateTime.Now - start, reconnectsCount);
                if (clientBase.Proxy == null || clientBase.Proxy.State == CommunicationState.Faulted)
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

        private void reconnectTimer_Elapsed(object sender, EventArgs e)
        {
            if (clientBase.Proxy == null)
            {
                Log.Debug("�������� �� �������������� �����. ������ ��������� ����������� �� �������");
                clientBase.Register();
            }

            if (clientBase.Proxy != null)
            {
                Log.Debug("����������� �� ������� ���������. ��������� ����� �������������� �����.");
                reconnectTimer.Stop();
                //_keepAliveTimer.Start();
            }
        }

        public void SetProxy<T>(ClientBase<T> value) where T : class
        {
            if (proxy != value) return;
            if (proxy != null)
                ((ClientBase<T>)proxy).InnerChannel.Faulted -= InnerChannel_Faulted;
            proxy = value;
            if (proxy != null)
                ((ClientBase<T>)proxy).InnerChannel.Faulted += InnerChannel_Faulted;
        }

        private void InnerChannel_Faulted(object sender, EventArgs e)
        {
            RepairConnection();
        }

        private void RepairConnection()
        {
            Log.Error("����� � �������� ��������");
            //The proxy channel should no longer be used
            clientBase.AbortProxy();//TODO:���������, ��� ������ ��������� ��� ����, ����� ��������������� ��������� ���������� �����, �.�. ���������������� ������

            //Enable the try again timer and attempt to reconnect
            Log.Debug("������ ����� �������������� ����� � ��������");
            reconnectTimer.Start();
        }
    }
}
using System;
using System.Reactive.Linq;
using NLog;
using Oleg_ivo.Plc.Channels;

namespace Oleg_ivo.LowLevelClient
{
    /// <summary>
    /// Измерительный таймер
    /// </summary>
    public class MeasurementPoll
    {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();
        private IDisposable disposable;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logicalChannel"></param>
        public MeasurementPoll(LogicalChannel logicalChannel)
        {
            LogicalChannel = logicalChannel;
            var period = logicalChannel.PollPeriod ?? TimeSpan.FromSeconds(5);
            disposable = Observable.Interval(period).Where(l => IsStarted).Subscribe(l => OnTick(), HandleException);//TODO:dispose
        }

        private void HandleException(Exception exception)
        {
            log.Error(exception);
        }

        private void OnTick()
        {
            if (IsStarted)
            {
                object newValue;

                try
                {
                    newValue = LogicalChannel.GetNewValue();
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    throw;
                }

                if (newValue != null)
                    InvokeNewDataReceived(LogicalChannel, newValue);
            }
        }

        public bool IsStarted { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public LogicalChannel LogicalChannel { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<NewDataReceivedEventArgs> Elapsed;

        private void InvokeNewDataReceived(LogicalChannel logicalChannel, object newValue)
        {
            EventHandler<NewDataReceivedEventArgs> handler = Elapsed;
            if (handler != null) handler(this, new NewDataReceivedEventArgs(logicalChannel, newValue));
        }

        /// <summary>
        /// запустить таймер опроса канала
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void StartPoll()
        {
            log.Trace("Запуск опроса канала №{0}", LogicalChannel.Id);
            if (IsStarted)
                log.Warn("Запуск опроса канала №{0} уже был произведён", LogicalChannel.Id);
            else
                IsStarted = true;
        }

        /// <summary>
        /// остановить таймер опроса канала
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void StopPoll()
        {
            log.Trace("Остановка опроса канала №{0}", LogicalChannel.Id);
            if (!IsStarted)
                log.Warn("Остановка опроса канала №{0} уже была произведёна", LogicalChannel.Id);
            else
                IsStarted = false;
        }
    }
}
using System;
using System.Reactive.Linq;
using Oleg_ivo.Plc.Channels;

namespace Oleg_ivo.LowLevelClient
{
    /// <summary>
    /// Измерительный таймер
    /// </summary>
    public class MeasurementPoll
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logicalChannel"></param>
        public MeasurementPoll(LogicalChannel logicalChannel)
        {
            LogicalChannel = logicalChannel;
            var period = logicalChannel.PollPeriod ?? TimeSpan.FromSeconds(5);
            Observable.Interval(period).Subscribe(l => OnTick());
            //TODO: более продуманный отключаемый таймер (чтобы он не работал, когда выключен, чтобы каждый раз не проверять в OnTick свойство IsStarted)
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
            IsStarted = true;
        }

        /// <summary>
        /// остановить таймер опроса канала
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void StopPoll()
        {
            IsStarted = false;
        }
    }
}
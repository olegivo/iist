using System;
using System.ComponentModel;
using System.Timers;
using Oleg_ivo.Plc.Channels;

namespace Oleg_ivo.LowLevelClient
{
    /// <summary>
    /// Измерительный таймер
    /// </summary>
    public class MeasurementPoll
    {
        private readonly Timer _timer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logicalChannel"></param>
        /// <param name="interval">Интервал опроса канала (в миллисекундах)</param>
        /// <param name="synchronizingObject">Объект для синхронизации</param>
        public MeasurementPoll(LogicalChannel logicalChannel, double interval, ISynchronizeInvoke synchronizingObject)
        {
            LogicalChannel = logicalChannel;
            _timer = new Timer(interval);
            _timer.Elapsed += _timer_Elapsed;
            SynchronizingObject = synchronizingObject;
        }

        /// <summary>
        /// 
        /// </summary>
        private LogicalChannel LogicalChannel { get; set; }

        /// <summary>
        /// Объект для синхронизации
        /// </summary>
        private ISynchronizeInvoke SynchronizingObject
        {
            get { return _timer.SynchronizingObject; }
            set { _timer.SynchronizingObject = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<NewDataReceivedEventArgs> Elapsed;

        private void InvokeNewDataReceived(LogicalChannel logicalChannel, object newValue)
        {
            EventHandler<NewDataReceivedEventArgs> handler = Elapsed;
            if (handler != null) handler(this, new NewDataReceivedEventArgs(logicalChannel, newValue));
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
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

            if (newValue!=null)
                InvokeNewDataReceived(LogicalChannel, newValue);
        }

        /// <summary>
        /// запустить таймер опроса канала
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void StartPoll()
        {
            _timer.Start();
        }

        /// <summary>
        /// остановить таймер опроса канала
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void StopPoll()
        {
            _timer.Stop();
        }
    }
}
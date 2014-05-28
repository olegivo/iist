using System;
using System.ComponentModel;
using System.Timers;
using Oleg_ivo.Plc.Channels;

namespace Oleg_ivo.LowLevelClient
{
    /// <summary>
    /// ������������� ������
    /// </summary>
    public class MeasurementPoll
    {
        private readonly Timer _timer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logicalChannel"></param>
        /// <param name="interval">�������� ������ ������ (� �������������)</param>
        /// <param name="synchronizingObject">������ ��� �������������</param>
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
        /// ������ ��� �������������
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
        /// ��������� ������ ������ ������
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void StartPoll()
        {
            _timer.Start();
        }

        /// <summary>
        /// ���������� ������ ������ ������
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void StopPoll()
        {
            _timer.Stop();
        }
    }
}
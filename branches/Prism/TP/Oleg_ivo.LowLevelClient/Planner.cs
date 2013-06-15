using System;
using System.Collections.Generic;
using System.ComponentModel;
using Oleg_ivo.Plc.Channels;

namespace Oleg_ivo.LowLevelClient
{
    ///<summary>
    /// ����������� ������ �������
    ///</summary>
    public class Planner
    {
        #region Singleton

        private static Planner _instance;

        ///<summary>
        /// ������������ ���������
        ///</summary>
        public static Planner Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Planner();
                }
                return _instance;
            }
        }

        /// <summary>
        /// �������������� ����� ��������� ������ <see cref="Planner" />.
        /// </summary>
        private Planner()
        {
        }

        #endregion

        /// <summary>
        /// ������ �������
        /// </summary>
        private readonly Dictionary<LogicalChannel, MeasurementPoll> MeasurementPolls = new Dictionary<LogicalChannel, MeasurementPoll>();

        /// <summary>
        /// �������� ����� ������
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="interval"></param>
        /// <param name="synchronizingObject"></param>
        public void AddPoll(LogicalChannel channel, double interval, ISynchronizeInvoke synchronizingObject)
        {
            MeasurementPoll measurementPoll = GetMeasurementPoll(channel);
            if (measurementPoll != null)
                throw new Exception("��� ���� ����� ��� ������� ������");

            MeasurementPoll poll = new MeasurementPoll(channel, interval, synchronizingObject);
            MeasurementPolls.Add(channel, poll);
            //throw new NotImplementedException("������ ��������� ������");
        }

        private MeasurementPoll GetMeasurementPoll(LogicalChannel channel)
        {
            return MeasurementPolls.ContainsKey(channel) ? MeasurementPolls[channel] : null;
        }

        /// <summary>
        /// ������� ����� ������
        /// </summary>
        /// <param name="channel"></param>
        public void RemovePoll(LogicalChannel channel)
        {
            MeasurementPoll measurementPoll = GetMeasurementPoll(channel);
            if (measurementPoll == null)
                throw new Exception("�� ������ ����� ��� ������� ������");

            measurementPoll.StopPoll();//����������� ����������, ����� ����� ������
            MeasurementPolls.Remove(channel);
            //throw new NotImplementedException("������� �����");
        }

        /// <summary>
        /// ��������� ������ ������ ������
        /// </summary>
        /// <param name="channel"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void StartPoll(LogicalChannel channel)
        {
            MeasurementPoll measurementPoll = GetMeasurementPoll(channel);
            if (measurementPoll == null)
                throw new Exception("�� ������ ����� ��� ������� ������");

            measurementPoll.Elapsed += measurementPoll_Elapsed;
            measurementPoll.StartPoll();
        }

        void measurementPoll_Elapsed(object sender, NewDataReceivedEventArgs e)
        {
            EventHandler<NewDataReceivedEventArgs> handler = NewDadaReceived;
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<NewDataReceivedEventArgs> NewDadaReceived;

        /// <summary>
        /// ���������� ������ ������ ������
        /// </summary>
        /// <param name="channel"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void StopPoll(LogicalChannel channel)
        {
            MeasurementPoll measurementPoll = GetMeasurementPoll(channel);
            if (measurementPoll == null)
                throw new Exception("�� ������ ����� ��� ������� ������");

            measurementPoll.Elapsed -= measurementPoll_Elapsed;
            measurementPoll.StopPoll();
        }
    }
}
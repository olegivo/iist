using System;
using System.Collections.Generic;
using Oleg_ivo.Plc.Channels;

namespace Oleg_ivo.LowLevelClient
{
    ///<summary>
    /// ����������� ������ �������
    ///</summary>
    public class Planner
    {
        /// <summary>
        /// ������ �������
        /// </summary>
        private readonly Dictionary<LogicalChannel, MeasurementPoll> measurementPolls = new Dictionary<LogicalChannel, MeasurementPoll>();

        /// <summary>
        /// �������� ����� ������
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="interval"></param>
        public void AddPoll(LogicalChannel channel, double interval)
        {
            var measurementPoll = GetMeasurementPoll(channel);
            if (measurementPoll != null)
                throw new Exception("��� ���� ����� ��� ������� ������");

            var poll = new MeasurementPoll(channel);
            measurementPolls.Add(channel, poll);
            //throw new NotImplementedException("������ ��������� ������");
        }

        private MeasurementPoll GetMeasurementPoll(LogicalChannel channel)
        {
            return measurementPolls.ContainsKey(channel) ? measurementPolls[channel] : null;
        }

        /// <summary>
        /// ������� ����� ������
        /// </summary>
        /// <param name="channel"></param>
        public void RemovePoll(LogicalChannel channel)
        {
            var measurementPoll = GetMeasurementPoll(channel);
            if (measurementPoll == null)
                throw new Exception("�� ������ ����� ��� ������� ������");

            measurementPoll.StopPoll();//����������� ����������, ����� ����� ������
            measurementPolls.Remove(channel);
            //throw new NotImplementedException("������� �����");
        }

        /// <summary>
        /// ��������� ������ ������ ������
        /// </summary>
        /// <param name="channel"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void StartPoll(LogicalChannel channel)
        {
            var measurementPoll = GetMeasurementPoll(channel);
            if (measurementPoll == null)
                throw new Exception("�� ������ ����� ��� ������� ������");

            measurementPoll.Elapsed += measurementPoll_Elapsed;
            measurementPoll.StartPoll();
        }

        void measurementPoll_Elapsed(object sender, NewDataReceivedEventArgs e)
        {
            var handler = NewDadaReceived;
            if (handler != null)
                handler(sender, e);
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
            var measurementPoll = GetMeasurementPoll(channel);
            if (measurementPoll == null)
                throw new Exception("�� ������ ����� ��� ������� ������");

            measurementPoll.Elapsed -= measurementPoll_Elapsed;
            measurementPoll.StopPoll();
        }

        /// <summary>
        /// ���������� ��� ������� ������ �������
        /// </summary>
        public void StopAllPolls()
        {
            foreach (var measurementPoll in measurementPolls)
                measurementPoll.Value.StopPoll();
        }
    }
}
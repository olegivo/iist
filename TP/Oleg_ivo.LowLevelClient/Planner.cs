using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using NLog;
using Oleg_ivo.Base.Extensions;
using Oleg_ivo.Plc.Channels;

namespace Oleg_ivo.LowLevelClient
{
    ///<summary>
    /// ����������� ������ �������
    ///</summary>
    public class Planner
    {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// ������ �������
        /// </summary>
        private readonly ConcurrentDictionary<LogicalChannel, MeasurementPoll> measurementPolls = new ConcurrentDictionary<LogicalChannel, MeasurementPoll>();

        /// <summary>
        /// �������� ����� ������
        /// </summary>
        /// <param name="channel"></param>
        public void AddPoll(LogicalChannel channel)
        {
            var poll = new MeasurementPoll(channel);
            measurementPolls.AddOrUpdate(channel, logicalChannel => poll, (logicalChannel, existsPoll) =>
            {
                log.Trace("����� ��� ������ �{0} ��� ���������� � ����������. ����� ����������� ���������� ��� ��������� � ��������.", logicalChannel.Id);
                existsPoll.StopPoll();
                MeasurementPoll oldPoll;
                var removed = measurementPolls.TryRemove(channel, out oldPoll);
                log.Trace("�������� ����������� ������ ��� ������ �{0}: {1}", logicalChannel.Id, removed ? "�������, ���������� ����� ������" : "��������, ����� ������ �� ���������");
                return removed ? poll : existsPoll;
            });
        }

        private MeasurementPoll GetMeasurementPoll(LogicalChannel channel)
        {
            return measurementPolls.GetValueOrDefault(channel);
        }

        /// <summary>
        /// ������� ����� ������
        /// </summary>
        /// <param name="channel"></param>
        public void RemovePoll(LogicalChannel channel)
        {
            log.Trace("�������� ������ ������ �{0}", channel.Id);
            MeasurementPoll poll;
            if (measurementPolls.TryRemove(channel, out poll))
            {
                poll.StopPoll(); //����������� ����������, ����� ����� ������
                log.Trace("����� ������ �{0} ����� �� ���������� � ����������", channel.Id);
            }
            else
                log.Warn("��� ������ �{0} �� ������ �����", channel.Id);
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
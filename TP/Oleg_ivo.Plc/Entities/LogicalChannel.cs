using Oleg_ivo.Plc.Common;

namespace Oleg_ivo.Plc.Entities
{
    public partial class LogicalChannel : IIdentified
    {
        //TODO:����� 1. ��������� � ������ ����� ����� ������ �� Oleg_ivo.Plc.Channels.LogicalChannel
        //TODO:����� 2. ������������ ������������ ������ ����� ������ Oleg_ivo.Plc.Channels.LogicalChannel
/*
        public delegate double GetLogicalChannelValueDelegate();

        #region fields

        private ushort _channelSize;
        private double? previousValue;

        #endregion

        #region properties

        /// <summary>
        /// ����� ��������� ��������
        /// </summary>
        public double DeltaChangeLimit { get; set; }

        /// <summary>
        /// ������� ������� ��������������
        /// </summary>
        public Polynom DirectTransform { get; set; }

        /// <summary>
        /// ������� ��������� ��������������
        /// </summary>
        public Polynom ReverseTransform { get; set; }

        /// <summary>
        /// ������� ��������������� ��������� ������ �� ������.
        /// ���� �����, ������������ � ������ <see cref="GetValue"/>
        /// </summary>
        public Channels.LogicalChannel.GetLogicalChannelValueDelegate GetValueAltDelegate { protected get; set; }

        /// <summary>
        /// ������� �������� ��������������� ��������� �������� �� ������.
        /// ���� �����, ������������ � ������ <see cref="GetValueEmulation"/>
        /// </summary>
        public Channels.LogicalChannel.GetLogicalChannelValueDelegate GetValueEmulationAltDelegate { get; set; }

        /// <summary>
        /// ����� �������� (��������� � �������� ����������� �� �����)
        /// </summary>
        public bool IsEmulationMode { get; set; }

        #endregion


        #region methods

        /// <summary>
        /// �������� ��������� �������� � ��������
        /// </summary>
        /// <param name="value"></param>
        private void CheckValueRange(double value)
        {
            double limitValue;
            string message = "";

            limitValue = (double?) MinValue ?? value;
            if (value < limitValue)
                message += string.Format("����������� ����������� �������� ({0})", limitValue);

            limitValue = (double?) MaxValue ?? value;
            if (value > limitValue)
                message +=
                    message.Length > 0 ? Environment.NewLine : ""
                    + string.Format("������������ ����������� �������� ({0})", limitValue);

            if (message.Length > 0)
            {
                throw new ArgumentOutOfRangeException("value", value, message);
            }

        }

        /// <summary>
        /// �������� ����������� �������� �� ������.
        /// ���� ����� ������� �������� ��������������� ��������� ������ �� ������, ������������ ��,
        /// ����� ������������ ��������� ������� ������ �� ��������� �������, 
        /// �������� ���� ������ � ���������� ��������, ������ ��������� ��������� ��������,
        /// ���������� ������ �������������� (���� ����� ������� ������� �������������),
        /// ����� ������������ �������� ����������� �������� �� ��������� ��������� ��������.
        /// </summary>
        /// <returns></returns>
        private double GetValueEmulation()
        {
            if (GetValueEmulationAltDelegate != null)
                return GetValueEmulationAltDelegate();

            int second = DateTime.Now.Second;
            double value = Math.Sin(second * 6 * (Math.PI / 180));//����� [-1;+1]
            value = ((value + 1) / 2);//����� - ����� [0;+1]
            if (MinValue != null && MaxValue != null)
                value = (double)MinValue + (double)(MaxValue - MinValue).Value * value;//��������������� �� ���������

            //NOTE: ��� �������� ������� ������� �������������� ����� � �� ������������, ����� ����� ������������ ��������� ������
            if (DirectTransform != null)
                value = DirectTransform.GetValue(value);

            CheckValueRange(value);

            return value;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Func<Channels.LogicalChannel, bool> GetFindChannelPredicate(int id)
        {
            return (channel => channel.Id == id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">���� > 0</param>
        /// <param name="physicalChannelId">���� > 0</param>
        /// <param name="addressShift"></param>
        /// <param name="channelSize">���� > 0</param>
        /// <returns></returns>
        public static Func<Channels.LogicalChannel, bool> GetEqualsPredicate(int id, int physicalChannelId, ushort addressShift, ushort channelSize)
        {
            return
                (channel =>
                 (id == 0 || channel.Id == id)
                 && (physicalChannelId == 0
                        || channel.PhysicalChannel == null
                        || channel.PhysicalChannel.Id == 0
                        || channel.PhysicalChannel.Id == physicalChannelId)
                 && (channel.AddressShift == addressShift)
                 && (channelSize == 0 || channel.ChannelSize == channelSize)
                );
        }

        /// <summary>
        /// �������� �� ��������� �������� (������������ <see cref="DeltaChangeLimit"/>)
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IsNewData(double? oldValue, double? value)
        {
            bool result = true;
            if (DeltaChangeLimit > 0 && oldValue != null && value != null)
            {
                //���� ���� �� ���� �� ������ ��������� �� ������, ���������, ��� DeltaChangeLimit - ���������� ���������, ����� - ������������� (� % �� ������� ���������)
                double delta;
                if (MinValue != null && MaxValue != null)
                {
                    //������������� ��������
                    delta = DeltaChangeLimit * 100 / Math.Abs((double)(MaxValue - MinValue));
                }
                else
                {
                    //���������� ��������
                    delta = DeltaChangeLimit;
                }

                result = (oldValue == null || Math.Abs((double)(oldValue - value)) >= delta);
            }

            return result;
        }
*/

    }
}
using System;
using Oleg_ivo.Plc.Common;

namespace Oleg_ivo.Plc.Channels
{
    ///<summary>
    /// ���������� �����.
    /// ��������� ��������� ��������� � ������ ���, ��������� � ������� ��������
    ///</summary>
    public abstract class LogicalChannel : IIdentified
    {

        #region fields

        private ushort _channelSize;
        private double? previousValue;

        #endregion

        #region properties

        ///<summary>
        /// ���������� �����
        ///</summary>
        public PhysicalChannel PhysicalChannel { get; private set; }

        ///<summary>
        ///
        ///</summary>
        public string Description { get; set; }

        ///<summary>
        /// ����� � ���������� ������
        ///</summary>
        public ushort AddressShift { get; set; }

        ///<summary>
        /// ����������� ����������� ������
        ///</summary>
        public ushort ChannelSize
        {
            get { return _channelSize > 0 ? _channelSize : PhysicalChannel.ChannelSize; }
            set { _channelSize = value; }
        }

        ///<summary>
        /// ����� �� ���� ��� ������
        ///</summary>
        public int WriteAddress
        {
            get { return PhysicalChannel.WriteAddress + AddressShift; }
        }

        ///<summary>
        /// ����� �� ���� ��� ������
        ///</summary>
        public int ReadAddress
        {
            get { return PhysicalChannel.ReadAddress + AddressShift; }
        }

        ///<summary>
        /// �������������
        ///</summary>
        public int Id { get; set; }

        /// <summary>
        /// ����� ��������� ��������
        /// </summary>
        public double DeltaChangeLimit { get; set; }

        /// <summary>
        /// ����������� ���������� �������� ��� ������
        /// </summary>
        public double? MinValue { get; set; }

        /// <summary>
        /// ������������ ���������� �������� ��� ������
        /// </summary>
        public double? MaxValue { get; set; }

        /// <summary>
        /// ������� ������� ��������������
        /// </summary>
        public Polynom DirectTransform { get; set; }

        /// <summary>
        /// ������� ��������� ��������������
        /// </summary>
        public Polynom ReverseTransform { get; set; }

        /// <summary>
        /// ������ ������ ������
        /// </summary>
        public TimeSpan PollPeriod { get; set; }

        #endregion

        #region constructors
        ///<summary>
        ///
        ///</summary>
        ///<param name="physicalChannel"></param>
        ///<param name="addressShift"></param>
        ///<param name="channelSize"></param>
        protected LogicalChannel(PhysicalChannel physicalChannel, ushort addressShift, ushort channelSize)
        {
            PhysicalChannel = physicalChannel;
            AddressShift = addressShift;
            _channelSize = channelSize;
        }

        #endregion

        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return string.Format("���������� ����� {0}[{1}-{2}] Id = {3}", Description, WriteAddress, WriteAddress + ChannelSize - 1, Id);
        }


        #region methods

        ///<summary>
        /// �������� �������� � ���������� �����
        ///</summary>
        ///<param name="value"></param>
        public void SetValue(double value)
        {
            if (ReverseTransform != null) value = ReverseTransform.GetValue(value);
            PhysicalChannel.SetValue(AddressShift, ChannelSize, value);
        }

        ///<summary>
        /// �������� �������� �� ����������� ������
        ///</summary>
        ///<exception cref="ArgumentOutOfRangeException"></exception>
        ///<returns></returns>
        public double GetValue()
        {
            object obj = PhysicalChannel.GetValue(AddressShift, ChannelSize);
            if (obj is Array)
            {
                Array ar = (Array) obj;
                if(ar.Length!=1)
                    throw new ArgumentOutOfRangeException("ChannelSize", ChannelSize, "��� ����������� ������ ����������� ����� ������ (��� ������� ��������� ���� �������� ��� �������� ������ ��������)");
                obj = ar.GetValue(0);
            }
            double value = Convert.ToDouble(obj);
            if (DirectTransform != null)
                value = DirectTransform.GetValue(value);

            CheckValueRange(value);
            
            return value;
        }

        /// <summary>
        /// �������� ����� �������� �� ����������� ������.
        /// </summary>
        /// <returns>���� ����� �������� ���� ���������� �� �����������, ������������ <see langword="null"/>. 
        /// ����� ����� �������� �������������� ����������</returns>
        public double? GetNewValue()
        {
            double? value = GetValue();

            if (previousValue == null || IsNewData(previousValue, value))
                previousValue = value;
            else
                value = null;

            return value;
        }

        /// <summary>
        /// �������� ����� �������� �� ����������� ������. ����� ��������.
        /// </summary>
        /// <returns>���� ����� �������� ���� ���������� �� �����������, ������������ <see langword="null"/>. 
        /// ����� ����� �������� �������������� ����������</returns>
        public double? GetNewValueEmulation()
        {
            double? value = GetValueEmulation();

            if (previousValue == null || IsNewData(previousValue, value))
                previousValue = value;
            else
                value = null;

            return value;
        }

        /// <summary>
        /// �������� ��������� �������� � ��������
        /// </summary>
        /// <param name="value"></param>
        private void CheckValueRange(double value)
        {
            double limitValue;
            string message = "";

            limitValue = MinValue ?? value;
            if (value < limitValue)
                message += string.Format("����������� ����������� �������� ({0})", limitValue);

            limitValue = MaxValue ?? value;
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
        /// �������� ����������� �������� �� ������
        /// </summary>
        /// <returns></returns>
        public double GetValueEmulation()
        {
            int second = DateTime.Now.Second;
            double value = Math.Sin(second * 6 * (Math.PI / 180));//����� [-1;+1]
            value = ((value + 1) / 2);//����� - ����� [0;+1]
            if (MinValue!=null && MaxValue!=null)
                value = (double) (MinValue + (MaxValue - MinValue)*value);//��������������� �� ���������
            
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
        public static Func<LogicalChannel,bool> GetFindChannelPredicate(int id)
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
        public static Func<LogicalChannel, bool> GetEqualsPredicate(int id, int physicalChannelId, ushort addressShift, ushort channelSize)
        {
            return
                (channel =>
                 (id == 0 || channel.Id == id)
                 && (physicalChannelId==0 
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
            if(DeltaChangeLimit>0 && oldValue!=null && value!=null)
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
    }
}
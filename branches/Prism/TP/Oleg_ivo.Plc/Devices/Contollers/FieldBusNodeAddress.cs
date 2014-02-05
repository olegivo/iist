using System;
using Oleg_ivo.Plc.Common;

namespace Oleg_ivo.Plc.Devices.Contollers
{
    ///<summary>
    /// ����� ��� �� ����
    ///</summary>
    //TODO: 2 entity?
    public abstract class FieldBusNodeAddress: IIdentified, IEquatable<FieldBusNodeAddress>
    {
        #region fields
        private readonly byte _slaveAddress;

        #endregion

        #region properties
        ///<summary>
        /// ����� ���������� (��� ��������)
        ///</summary>
        public byte SlaveAddress
        {
            get { return _slaveAddress; }
        }

        #endregion

        #region constructors
        ///<summary>
        /// ����� ��� �� ����
        ///</summary>
        ///<param name="slaveAddress"></param>
        ///<param name="id"></param>
        protected FieldBusNodeAddress(byte slaveAddress, int id)
            : this(id)
        {
            _slaveAddress = slaveAddress;
        }

        ///<summary>
        ///
        ///</summary>
        protected FieldBusNodeAddress(int id)
        {
            Id = id;
        }

        #endregion

        ///<summary>
        ///Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        ///</summary>
        ///
        ///<returns>
        ///A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        ///</returns>
        ///<filterpriority>2</filterpriority>
        public override string ToString()
        {
            return string.Format("����� ��� - {0}", SlaveAddress);
        }

        /// <summary>
        /// ���������, ����� �� ������� ������ ������� ������� ���� �� ����.
        /// </summary>
        /// <returns>
        /// <see langword="true"/>, ���� ������� ������ ����� ��������� <paramref name="other"/>, � ��������� �����堗 <see langword="false"/>.
        /// </returns>
        /// <param name="other">������, ������� ��������� �������� � ������ ��������.
        ///                 </param>
        public abstract bool Equals(FieldBusNodeAddress other);//return !ReferenceEquals(null, other);

        /// <summary>
        /// ����������, ����� �� �������� ������ <see cref="T:System.Object"/> �������� ������� <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// <see langword="true"/>, ���� ��������� ������ <see cref="T:System.Object"/> ����� �������� ������� <see cref="T:System.Object"/>; � ��������� ������ � <see langword="false"/>.
        /// </returns>
        /// <param name="obj">������ <see cref="T:System.Object"/>, ������� ��������� �������� � ������� �������� <see cref="T:System.Object"/>. 
        ///                 </param><exception cref="T:System.NullReferenceException">�������� <paramref name="obj"/> ����� �������� <see langword="null"/>.
        ///                 </exception><filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((FieldBusNodeAddress) obj);
        }

        /// <summary>
        /// ������ ���� ���-������� ��� ������������� ����. 
        /// </summary>
        /// <returns>
        /// ���-��� ��� �������� ������� <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(FieldBusNodeAddress left, FieldBusNodeAddress right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(FieldBusNodeAddress left, FieldBusNodeAddress right)
        {
            return !Equals(left, right);
        }

        ///<summary>
        /// �������������
        ///</summary>
        public int Id { get; set; }

        /// <summary>
        /// ��������� ������ � ������������, �� ������� �� �������
        /// </summary>
        /// <param name="addressPart1"></param>
        /// <param name="addressPart2"></param>
        /// <returns></returns>
        public abstract bool IsEquals(string addressPart1, int addressPart2);
    }
}
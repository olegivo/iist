using System;
using Oleg_ivo.Plc.Common;

namespace Oleg_ivo.Plc.Devices.Contollers
{
    ///<summary>
    /// Адрес ПЛК на шине
    ///</summary>
    //TODO: 2 entity?
    public abstract class FieldBusNodeAddress: IIdentified, IEquatable<FieldBusNodeAddress>
    {
        #region fields
        private readonly byte _slaveAddress;

        #endregion

        #region properties
        ///<summary>
        /// Адрес устройства (как ведомого)
        ///</summary>
        public byte SlaveAddress
        {
            get { return _slaveAddress; }
        }

        #endregion

        #region constructors
        ///<summary>
        /// Адрес ПЛК на шине
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
            return string.Format("Адрес ПЛК - {0}", SlaveAddress);
        }

        /// <summary>
        /// Указывает, равен ли текущий объект другому объекту того же типа.
        /// </summary>
        /// <returns>
        /// <see langword="true"/>, если текущий объект равен параметру <paramref name="other"/>, в противном случае — <see langword="false"/>.
        /// </returns>
        /// <param name="other">Объект, который требуется сравнить с данным объектом.
        ///                 </param>
        public abstract bool Equals(FieldBusNodeAddress other);//return !ReferenceEquals(null, other);

        /// <summary>
        /// Определяет, равен ли заданный объект <see cref="T:System.Object"/> текущему объекту <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// <see langword="true"/>, если указанный объект <see cref="T:System.Object"/> равен текущему объекту <see cref="T:System.Object"/>; в противном случае — <see langword="false"/>.
        /// </returns>
        /// <param name="obj">Объект <see cref="T:System.Object"/>, который требуется сравнить с текущим объектом <see cref="T:System.Object"/>. 
        ///                 </param><exception cref="T:System.NullReferenceException">Параметр <paramref name="obj"/> имеет значение <see langword="null"/>.
        ///                 </exception><filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((FieldBusNodeAddress) obj);
        }

        /// <summary>
        /// Играет роль хэш-функции для определенного типа. 
        /// </summary>
        /// <returns>
        /// Хэш-код для текущего объекта <see cref="T:System.Object"/>.
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
        /// Идентификатор
        ///</summary>
        public int Id { get; set; }

        /// <summary>
        /// Сравнение адреса с компонентами, из которых он состоит
        /// </summary>
        /// <param name="addressPart1"></param>
        /// <param name="addressPart2"></param>
        /// <returns></returns>
        public abstract bool IsEquals(string addressPart1, int addressPart2);
    }
}
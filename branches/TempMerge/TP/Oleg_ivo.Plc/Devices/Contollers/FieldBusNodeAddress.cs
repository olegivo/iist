using System;
using Oleg_ivo.Plc.Common;
using Oleg_ivo.Plc.FieldBus;

namespace Oleg_ivo.Plc.Devices.Contollers
{
    ///<summary>
    /// Адрес ПЛК на шине
    ///</summary>
    //TODO: 2 entity?
    public class FieldBusNodeAddress: IIdentified, IEquatable<FieldBusNodeAddress>
    {
        #region fields

        private readonly FieldBusType fieldBusType;
        private readonly string addressPart1;
        private readonly int addressPart2;

        #endregion

        #region properties
        
        
        ///<summary>
        /// IP-адрес (для протоколов Ethernet) / Наименование порта (для протоколов RS-232, RS-485)
        ///</summary>
        public string AddressPart1
        {
            get { return addressPart1; }
        }
        
        ///<summary>
        /// Номер порта (для протоколов Ethernet) / Адрес устройства как ведомого (для протоколов RS-232, RS-485)
        ///</summary>
        public int AddressPart2
        {
            get { return addressPart2; }
        }

        #endregion

        #region constructors

        public FieldBusNodeAddress(Entities.FieldBusNode entity)
            : this(entity.FieldBus.FieldBusType.FieldBusTypeEnum,
                entity.Id,
                entity.AddressPart1,
                entity.AddressPart2.Value)
        {
        }

        /// <summary>
        ///  Адрес ПЛК на шине
        /// </summary>
        /// <param name="fieldBusType"></param>
        /// <param name="id"></param>
        /// <param name="addressPart1"></param>
        /// <param name="addressPart2"></param>
        public FieldBusNodeAddress(FieldBusType fieldBusType, int id, string addressPart1, int addressPart2)
        {
            switch (fieldBusType)
            {
                case FieldBusType.RS232:
                case FieldBusType.RS485:
                    //todo: название порта должно удовлетворять требованиям COMx, адрес на шине - [1..99]
                    break;
                case FieldBusType.Ethernet:
                    //todo: проверять требования IP-Address
                    break;
            }

            Id = id;
            this.addressPart1 = addressPart1;
            this.addressPart2 = addressPart2;
            this.fieldBusType = fieldBusType;
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
            return string.Format("Адрес ПЛК - {0}", AddressPart2);
        }

        /// <summary>
        /// Указывает, равен ли текущий объект другому объекту того же типа.
        /// </summary>
        /// <returns>
        /// <see langword="true"/>, если текущий объект равен параметру <paramref name="other"/>, в противном случае — <see langword="false"/>.
        /// </returns>
        /// <param name="other">Объект, который требуется сравнить с данным объектом.
        ///                 </param>
        public bool Equals(FieldBusNodeAddress other)
        {
            return ReferenceEquals(null, other)
                   || (other.AddressPart1 == AddressPart1 && other.AddressPart2 == AddressPart2);
;
        }

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
            unchecked
            {
                int hashCode = (int)fieldBusType;
                hashCode = (hashCode * 397) ^ (addressPart1 != null ? addressPart1.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ addressPart2;
                hashCode = (hashCode * 397) ^ Id;
                return hashCode;
            }
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

        public FieldBusType FieldBusType
        {
            get { return fieldBusType; }
        }
    }
}
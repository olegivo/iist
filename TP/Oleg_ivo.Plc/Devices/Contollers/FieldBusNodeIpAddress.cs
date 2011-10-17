using System.Linq;

namespace Oleg_ivo.Plc.Devices.Contollers
{
    ///<summary>
    /// Адрес ПЛК на шине
    ///</summary>
    public class FieldBusNodeIpAddress : FieldBusNodeAddress
    {
        private readonly byte[] _ipSlaveAddress;
        private readonly int _port;

        ///<summary>
        /// Адрес ПЛК на шине
        ///</summary>
        ///<param name="ipSlaveAddress"></param>
        ///<param name="port"></param>
        ///<param name="id"></param>
        public FieldBusNodeIpAddress(byte[] ipSlaveAddress, int port, int id) : base(id)
        {
            _ipSlaveAddress = ipSlaveAddress;
            _port = port;
        }

        ///<summary>
        /// IP-адрес
        ///</summary>
        public byte[] IpSlaveAddress
        {
            get { return _ipSlaveAddress; }
        }

        ///<summary>
        /// Порт
        ///</summary>
        public int Port
        {
            get { return _port; }
        }

        #region Overrides of FieldBusNodeAddress

        /// <summary>
        /// Указывает, равен ли текущий объект другому объекту того же типа.
        /// </summary>
        /// <returns>
        /// <see langword="true"/>, если текущий объект равен параметру <paramref name="other"/>, в противном случае — <see langword="false"/>.
        /// </returns>
        /// <param name="other">Объект, который требуется сравнить с данным объектом.
        ///                 </param>
        public override bool Equals(FieldBusNodeAddress other)
        {
            FieldBusNodeIpAddress address = other as FieldBusNodeIpAddress;
            return address != null 
                && address.IpSlaveAddress.Length == IpSlaveAddress.Length 
                && Tools.Utils.Utils.StringUtils.Array2String(address.IpSlaveAddress) == Tools.Utils.Utils.StringUtils.Array2String(IpSlaveAddress)
                && address.Port == Port;
        }

        /// <summary>
        /// Сравнение адреса с компонентами, из которых он состоит
        /// </summary>
        /// <param name="addressPart1"></param>
        /// <param name="addressPart2"></param>
        /// <returns></returns>
        public override bool IsEquals(string addressPart1, int addressPart2)
        {
            string ip =
                IpSlaveAddress.Select(b => b.ToString()).Aggregate((left, result) => left + "." + result);

            return ip.Equals(addressPart1) && (Port==addressPart2);
        }

        #endregion
    }
}
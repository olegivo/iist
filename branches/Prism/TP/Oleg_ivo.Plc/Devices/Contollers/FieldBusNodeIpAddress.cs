using System.Linq;

namespace Oleg_ivo.Plc.Devices.Contollers
{
    ///<summary>
    /// ����� ��� �� ����
    ///</summary>
    public class FieldBusNodeIpAddress : FieldBusNodeAddress
    {
        private readonly byte[] _ipSlaveAddress;
        private readonly int _port;

        ///<summary>
        /// ����� ��� �� ����
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
        /// IP-�����
        ///</summary>
        public byte[] IpSlaveAddress
        {
            get { return _ipSlaveAddress; }
        }

        ///<summary>
        /// ����
        ///</summary>
        public int Port
        {
            get { return _port; }
        }

        #region Overrides of FieldBusNodeAddress

        /// <summary>
        /// ���������, ����� �� ������� ������ ������� ������� ���� �� ����.
        /// </summary>
        /// <returns>
        /// <see langword="true"/>, ���� ������� ������ ����� ��������� <paramref name="other"/>, � ��������� �����堗 <see langword="false"/>.
        /// </returns>
        /// <param name="other">������, ������� ��������� �������� � ������ ��������.
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
        /// ��������� ������ � ������������, �� ������� �� �������
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
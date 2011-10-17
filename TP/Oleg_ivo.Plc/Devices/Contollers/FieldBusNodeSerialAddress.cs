namespace Oleg_ivo.Plc.Devices.Contollers
{
    ///<summary>
    /// ����� ��� �� ���������������� ����
    ///</summary>
    public class FieldBusNodeSerialAddress : FieldBusNodeAddress
    {
        private readonly string serialPortName;

        ///<summary>
        /// ����� ��� �� ����
        ///</summary>
        ///<param name="slaveAddress"></param>
        ///<param name="serialPortName"></param>
        ///<param name="id"></param>
        public FieldBusNodeSerialAddress(string serialPortName, byte slaveAddress, int id) : base(slaveAddress, id)
        {
            this.serialPortName = serialPortName;
        }

        ///<summary>
        /// ������������ �����
        ///</summary>
        public string SerialPortName
        {
            get { return serialPortName; }
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
            FieldBusNodeSerialAddress address = other as FieldBusNodeSerialAddress;
            return address != null
                && address.SerialPortName == SerialPortName
                && address.SlaveAddress == SlaveAddress;
        }

        /// <summary>
        /// ��������� ������ � ������������, �� ������� �� �������
        /// </summary>
        /// <param name="addressPart1"></param>
        /// <param name="addressPart2"></param>
        /// <returns></returns>
        public override bool IsEquals(string addressPart1, int addressPart2)
        {
            return SerialPortName.Equals(addressPart1) && (SlaveAddress==addressPart2);
        }

        #endregion
    }
}
namespace Oleg_ivo.Plc.Devices.Contollers
{
    ///<summary>
    /// Адрес ПЛК на последовательной шине
    ///</summary>
    public class FieldBusNodeSerialAddress : FieldBusNodeAddress
    {
        private readonly string serialPortName;

        ///<summary>
        /// Адрес ПЛК на шине
        ///</summary>
        ///<param name="slaveAddress"></param>
        ///<param name="serialPortName"></param>
        ///<param name="id"></param>
        public FieldBusNodeSerialAddress(string serialPortName, byte slaveAddress, int id) : base(slaveAddress, id)
        {
            this.serialPortName = serialPortName;
        }

        ///<summary>
        /// Наименование порта
        ///</summary>
        public string SerialPortName
        {
            get { return serialPortName; }
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
            FieldBusNodeSerialAddress address = other as FieldBusNodeSerialAddress;
            return address != null
                && address.SerialPortName == SerialPortName
                && address.SlaveAddress == SlaveAddress;
        }

        /// <summary>
        /// Сравнение адреса с компонентами, из которых он состоит
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
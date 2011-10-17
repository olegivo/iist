using System.Net.NetworkInformation;

namespace Oleg_ivo.WAGO.Meta
{
    /// <summary>
    /// Описание ПЛК WAGO (Ethernet)
    /// </summary>
    public class WagoPlcMetaEthernet : WagoPlcMeta
    {
        private PhysicalAddress _macAddress;

        ///<summary>
        ///
        ///</summary>
        ///<param name="model"></param>
        ///<param name="address"></param>
        public WagoPlcMetaEthernet(ushort model, byte[] address) : base(model)
        {
            if(address!=null) InitMACAddress(address);
        }

        ///<summary>
        ///
        ///</summary>
        ///<param name="model"></param>
        public WagoPlcMetaEthernet(ushort model) : this(model, null)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        public void InitMACAddress(byte[] address)
        {
            MACAddress = new PhysicalAddress(address);
        }

        ///<summary>
        ///
        ///</summary>
        public PhysicalAddress MACAddress
        {
            get { return _macAddress; }
            set { _macAddress = value; }
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            string address = "";
            if(MACAddress!=null)
            {
                foreach (byte addressByte in MACAddress.GetAddressBytes())
                {
                    if(address.Length>0) address += "-";
                    address += addressByte.ToString("X2");//todo: правильный формат MAC-адреса???
                }
            }

            if(address.Length>0) address = string.Format("; MAC-ID = {0}", address);
            return string.Format("{0}{1}", base.ToString(), address);
        }
    }
}
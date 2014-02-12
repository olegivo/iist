using System;
using System.ComponentModel;
using Oleg_ivo.Plc.Devices.Contollers;
using Oleg_ivo.Plc.FieldBus;

namespace Oleg_ivo.WAGO.Factory
{
    ///<summary>
    ///
    ///</summary>
    public partial class FieldBusDAC : Component
    {
        ///<summary>
        ///
        ///</summary>
        public FieldBusDAC()
        {
            InitializeComponent();
        }

        ///<summary>
        ///
        ///</summary>
        ///<param name="container"></param>
        public FieldBusDAC(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        ///<summary>
        ///
        ///</summary>
        ///<returns></returns>
        public FieldBusNodeAddressCollection GetAddresses(FieldBusType fieldBusType)
        {
            FieldBusNodeAddressCollection addresses = null;

            SDA.SelectCommand.Parameters["@FieldBusTypeId"].Value = (int)fieldBusType;

            switch (fieldBusType)
            {
                case FieldBusType.Unknown:
                    break;
                case FieldBusType.RS232:
                    addresses = GetAddresses(GetSerialAddresses);
                    break;
                case FieldBusType.RS485:
                    addresses = GetAddresses(GetSerialAddresses);
                    break;
                case FieldBusType.Ethernet:
                    addresses = GetAddresses(GetEthernetAddresses);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("fieldBusType");
            }

            return addresses;
        }

        //private List<object> GetRS485Addresses()
        //{
        //    List<object> range;
        //    //RS-485
        //    range = new List<object>();
        //    range.Add(new Pair("COM1", 1));
        //    range.Add(new Pair("COM3", 11));
        //    range.Add(new Pair("COM6", 1));
        //    return range;
        //}

        //private List<object> GetRS232Addresses()
        //{
        //    List<object> range;
        //    //RS-485
        //    range = new List<object>();
        //    range.Add(new Pair("COM1", 1));
        //    range.Add(new Pair("COM3", 11));
        //    range.Add(new Pair("COM6", 1));
        //    return range;
        //}

        internal static FieldBusNodeAddress GetFieldBusNodeAddress(DtsChannelConfiguration.FieldBusNodeRow row)
        {
            switch ((FieldBusType)row.FieldBusTypeId)
            {
                case FieldBusType.RS232:
                case FieldBusType.RS485:
                    return GetSerialAddresses(row);
                case FieldBusType.Ethernet:
                    return GetEthernetAddresses(row);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        internal static FieldBusNodeAddress GetEthernetAddresses(DtsChannelConfiguration.FieldBusNodeRow row)
        {
            FieldBusNodeAddress address = null;
            //todo: порт должен удовлетворять требованиям IP-Address
            if (row != null) address = new FieldBusNodeAddress((FieldBusType) row.FieldBusTypeId, row.Id, row.AddressPart1, row.AddressPart2);
            return address;
        }

        internal static FieldBusNodeAddress GetSerialAddresses(DtsChannelConfiguration.FieldBusNodeRow row)
        {
            FieldBusNodeAddress address = null;
            //todo: порт должен удовлетворять требованиям COMx, адрес на шине - [1..99]
            if (row != null) address = new FieldBusNodeAddress((FieldBusType)row.FieldBusTypeId, row.Id, row.AddressPart1, (byte)row.AddressPart2);
            return address;
        }

        private FieldBusNodeAddressCollection GetAddresses(GetNodeAddressDelegate getNodeAddressDelegate)
        {
            FieldBusNodeAddressCollection addresses = new FieldBusNodeAddressCollection();

            if (getNodeAddressDelegate!=null)
            {
                dataManager1.Fill();
                foreach (DtsChannelConfiguration.FieldBusNodeRow row in dtsChannelConfiguration1.FieldBusNode)
                {
                    FieldBusNodeAddress address = getNodeAddressDelegate(row);
                    if (address != null) addresses.Add(address);
                }
            }

            return addresses;
        }

        private delegate FieldBusNodeAddress GetNodeAddressDelegate(DtsChannelConfiguration.FieldBusNodeRow row);

/*
        private static byte[] GetIPAddress(string address)
        {
            string ip = address;
            List<byte> bytes = new List<byte>();
            while (ip.Length>0)
            {
                //if (ip.StartsWith(".")) ip = ip.Substring(1, ip.Length - 1);
                int dot = ip.IndexOf(".");
                string b = dot>=0 ? ip.Substring(0, dot ) : ip;
                byte parseResult;
                byte.TryParse(b, out parseResult);
                bytes.Add(parseResult);
                ip = dot>=0 ? ip.Substring(dot + 1, ip.Length - b.Length - 1) : "";
            }

            return bytes.ToArray();
        }
*/
    }
}
using System.Collections.Generic;
using Oleg_ivo.Plc.Ports;

namespace Oleg_ivo.Plc.FieldBus
{
    public class ucFieldBusTypeCombobox:ucPairCombobox
    {
        protected override List<ValueDescriptionPair> CreateItems()
        {
            List<ValueDescriptionPair> list = new List<ValueDescriptionPair>();
            list.Add(new ValueDescriptionPair(FieldBusType.Ethernet, "Ethernet"));
            list.Add(new ValueDescriptionPair(FieldBusType.RS232, "RS232"));
            list.Add(new ValueDescriptionPair(FieldBusType.RS485, "RS485"));
            
            return list;
        }

        public FieldBusType FieldBusType
        {
            get
            {
                return SelectedValue != null
                           ?
                               (FieldBusType) SelectedValue
                           :
                               FieldBusType.Unknown;
            }
        }
    }
}
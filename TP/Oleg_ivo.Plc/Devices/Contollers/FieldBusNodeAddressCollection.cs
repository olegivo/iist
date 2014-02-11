using System.Collections.Generic;

namespace Oleg_ivo.Plc.Devices.Contollers
{
    ///<summary>
    /// Коллекция адресов ПЛК на шине
    ///</summary>
    public class FieldBusNodeAddressCollection : List<FieldBusNodeAddress>//TODO:inline?
    {
        public FieldBusNodeAddressCollection(IEnumerable<FieldBusNodeAddress> collection) : base(collection)
        {
        }

        public FieldBusNodeAddressCollection()
        {
        }
    }
}
using System.Collections.Generic;

namespace Oleg_ivo.Plc.Devices.Contollers
{
    ///<summary>
    /// ��������� ������� ��� �� ����
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
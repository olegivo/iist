using System.Collections.Generic;
using System.ComponentModel;

namespace Oleg_ivo.Plc.FieldBus.FieldBusManagers
{
    ///<summary>
    /// Коллекция диспетчеров полевых шин
    ///</summary>
    [TypeConverter(typeof(ArrayConverter))]
    public class FieldBusManagerCollection : List<FieldBusManager>
    {
        
    }
}
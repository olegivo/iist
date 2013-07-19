using System;
using System.Collections.Generic;

namespace Oleg_ivo.Plc.Devices.Contollers
{
    ///<summary>
    /// Коллекция адресов ПЛК на шине
    ///</summary>
    public class FieldBusNodeAddressCollection : List<FieldBusNodeAddress>//TODO:inline?
    {
        /// <summary>
        /// Поиск адреса по его частям
        /// </summary>
        /// <param name="addressPart1"></param>
        /// <param name="addressPart2"></param>
        /// <returns></returns>
        public FieldBusNodeAddress Find(string addressPart1,int addressPart2)
        {
            FieldBusNodeAddress fieldBusNodeAddress = null;

            foreach (FieldBusNodeAddress address in this)
            {
                if (address.IsEquals(addressPart1, addressPart2))
                {
                    if (fieldBusNodeAddress != null)
                        throw new Exception("Найдено больше одного одинаковых адреса");
                    fieldBusNodeAddress = address;
                }
            }

            return fieldBusNodeAddress;
        }
    }
}
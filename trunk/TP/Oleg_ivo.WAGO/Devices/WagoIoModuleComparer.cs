using System.Collections.Generic;
using Oleg_ivo.WAGO.Meta;

namespace Oleg_ivo.WAGO.Devices
{
    ///<summary>
    ///
    ///</summary>
    public class WagoIoModuleComparer : IComparer<WagoMeta>
    {
        /// <summary>
        /// Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.
        /// </summary>
        /// <returns>
        /// Value Condition Less than zerox is less than y.Zerox equals y.Greater than zerox is greater than y.
        /// </returns>
        /// <param name="y">The second object to compare.</param>
        /// <param name="x">The first object to compare.</param>
        public int Compare(WagoMeta x, WagoMeta y)
        {
            WagoIOModuleMeta mX = x as WagoIOModuleMeta;
            WagoIOModuleMeta mY = y as WagoIOModuleMeta;

            if (mX == null && mY == null) return 0;
            // ReSharper disable ConditionIsAlwaysTrueOrFalse
            if (mX == null && mY != null) return 1;
            if (mX != null && mY == null) return -1;
            // ReSharper restore ConditionIsAlwaysTrueOrFalse
            if (mX == mY) return 0;

            if (mX.IsAnalog != mY.IsAnalog)//1. Аналоговые и специальные модули получают адреса первыми.
            {
                return mX.IsAnalog && !mY.IsAnalog ? -1 : 1;
            }

            //2. Цифровые модули адресуются после Аналоговых и специальных.
            //3. Адресация идет последовательно.
            //4. Адресация начинается с нулевого слова.

            return 0;
        }
    }
}

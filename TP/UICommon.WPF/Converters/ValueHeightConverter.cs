using System;
using System.Windows.Data;

namespace UICommon.WPF.Converters
{

    public class ValueHeightConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            double? total = 0;
            double? currentValue = values[0] as double?;
            double? minValue = values[1] as double?;
            double? maxValue = values[2] as double?;

            if (values[0] != null && currentValue!=0)
            {
                total = 185/(maxValue - minValue)*currentValue;
            }

            return total;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}

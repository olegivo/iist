using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
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
            double? minValue = values[1] as double?;        //Минимально допустимое значение
            double? maxValue = values[2] as double?;        //Максимально допустимое значение

            //Если не поступают данные -> устанавливаем индикатор в ноль
            if (currentValue == null)
                return total;
            
            // Можно добавить ограничение
            //if (currentValue > maxValue)
            //    return 140;
            //if (currentValue < minValue)
            //    return total;
            
            var d = maxValue - minValue;                    //рабочий диапазон измеряемой величины
            var m = (currentValue - minValue) / d;          //нормированая входная величина
            total = (m * 140) + 20;                         //прибавим 20 пикселей снизу т.к. нижняя отметка шкалы приподнята
            return total;

            return total;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}

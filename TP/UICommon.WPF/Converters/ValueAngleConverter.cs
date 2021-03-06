﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace UICommon.WPF.Converters
{

        public class ValueAngleConverter : IMultiValueConverter
        {
            public object Convert(object[] values, Type targetType, object parameter,
                System.Globalization.CultureInfo culture)
            {
                double? total = 0;
				double? currentValue = values[0] as double?;
				double? minValue = values[1] as double?;
				double? maxValue = values[2] as double?;
				
                if (values[0]!= null)
                {
					total = (currentValue-minValue)*180/(maxValue-minValue);
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

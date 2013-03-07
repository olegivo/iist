using System;
using System.Globalization;
using System.Windows.Data;

namespace UICommon.WPF.Converters
{
    [ValueConversion(typeof(double?), typeof(String))]
    public class StringToNullableDoubleConverter : IValueConverter
    {
        /// <summary>
        /// ѕреобразует значение. 
        /// </summary>
        /// <returns>
        /// ѕреобразованное значение. ≈сли метод возвращает null, используетс€ действительное значение null.
        /// </returns>
        /// <param name="value">«начение, произведенное исходной прив€зкой.</param><param name="targetType">“ип свойства цели св€зывани€.</param><param name="parameter">ѕараметр используемого преобразовател€.</param><param name="culture">язык и региональные параметры, используемые в преобразователе.</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double f;
            double.TryParse((value ?? "").ToString(), out f);
            return f;
        }

        /// <summary>
        /// ѕреобразует значение. 
        /// </summary>
        /// <returns>
        /// ѕреобразованное значение. ≈сли метод возвращает null, используетс€ действительное значение null.
        /// </returns>
        /// <param name="value">«начение, произведенное целью прив€зки.</param><param name="targetType">“ип, к которому выполн€етс€ преобразование.</param><param name="parameter">»спользуемый параметр преобразовател€.</param><param name="culture">язык и региональные параметры, используемые в преобразователе.</param>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }
    }
}
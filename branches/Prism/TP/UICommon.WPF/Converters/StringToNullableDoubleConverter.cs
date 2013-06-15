using System;
using System.Globalization;
using System.Windows.Data;

namespace UICommon.WPF.Converters
{
    [ValueConversion(typeof(double?), typeof(String))]
    public class StringToNullableDoubleConverter : IValueConverter
    {
        /// <summary>
        /// ����������� ��������. 
        /// </summary>
        /// <returns>
        /// ��������������� ��������. ���� ����� ���������� null, ������������ �������������� �������� null.
        /// </returns>
        /// <param name="value">��������, ������������� �������� ���������.</param><param name="targetType">��� �������� ���� ����������.</param><param name="parameter">�������� ������������� ���������������.</param><param name="culture">���� � ������������ ���������, ������������ � ���������������.</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double f;
            double.TryParse((value ?? "").ToString(), out f);
            return f;
        }

        /// <summary>
        /// ����������� ��������. 
        /// </summary>
        /// <returns>
        /// ��������������� ��������. ���� ����� ���������� null, ������������ �������������� �������� null.
        /// </returns>
        /// <param name="value">��������, ������������� ����� ��������.</param><param name="targetType">���, � �������� ����������� ��������������.</param><param name="parameter">������������ �������� ���������������.</param><param name="culture">���� � ������������ ���������, ������������ � ���������������.</param>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }
    }
}
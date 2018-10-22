using System;
using System.Globalization;
using System.Windows.Data;

namespace SchadLucas.Wpf.Converters.Boolean
{
    public class NegateBoolean : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConvertBoolean(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConvertBoolean(value);
        }

        private static object ConvertBoolean(object value)
        {
            if (value is bool b)
            {
                return !b;
            }

            throw new ArgumentException(nameof(value));
        }
    }
}
using System;
using System.Globalization;
using System.Windows.Data;

namespace SchadLucas.Wpf.Converters.Math
{
    public class Sqrt : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (double.TryParse(value?.ToString(), out var n))
            {
                return System.Math.Sqrt(n);
            }

            throw new ArgumentException(nameof(value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (double.TryParse(value?.ToString(), out var n))
            {
                return System.Math.Pow(n, 2);
            }

            throw new ArgumentException(nameof(value));
        }
    }
}
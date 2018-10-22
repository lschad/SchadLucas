using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace SchadLucas.Wpf.Converters.Color
{
    public class ColorToSolidColorBrush : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is System.Windows.Media.Color c)
            {
                return new SolidColorBrush(c);
            }

            throw new ArgumentException(nameof(value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SolidColorBrush b)
            {
                return b.Color;
            }

            throw new ArgumentException(nameof(value));
        }
    }
}
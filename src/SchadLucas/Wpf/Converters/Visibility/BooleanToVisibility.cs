using System;
using System.Globalization;
using System.Windows.Data;

namespace SchadLucas.Wpf.Converters.Visibility
{
    public class BooleanToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool valueIsTrue)
            {
                return valueIsTrue ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
            }

            throw new ArgumentException(nameof(value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is System.Windows.Visibility visibility)
            {
                return visibility == System.Windows.Visibility.Visible;
            }

            throw new ArgumentException(nameof(value));
        }
    }
}
using System;
using System.Globalization;
using System.Windows.Data;

namespace SchadLucas.Wpf.Converters.Visibility
{
    public class NegativeBooleanToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b)
            {
                return b ? System.Windows.Visibility.Collapsed : System.Windows.Visibility.Visible;
            }

            throw new ArgumentException(nameof(value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is System.Windows.Visibility visibility)
            {
                return visibility == System.Windows.Visibility.Collapsed || visibility == System.Windows.Visibility.Hidden;
            }

            throw new ArgumentException(nameof(value));
        }
    }
}
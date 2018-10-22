using System;
using System.Globalization;
using System.Windows.Data;

namespace SchadLucas.Wpf.Converters.Visibility
{
    public class NullToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new ConvertBackNotSupportedException();
    }
}
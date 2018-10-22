using System;
using System.Globalization;
using System.Windows.Data;

namespace SchadLucas.Wpf.Converters.String
{
    public class Truncate : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string s && parameter is int i)
            {
                if (i < 0)
                {
                    throw new ArgumentException(nameof(parameter));
                }
                
                if (s.Length <= i)
                {
                    return s;
                }

                return s.Substring(0, i);
            }
         
            throw new ArgumentException(nameof(value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new ConvertBackNotSupportedException();
    }
}

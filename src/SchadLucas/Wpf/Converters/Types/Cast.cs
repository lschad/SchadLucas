using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;

namespace SchadLucas.Wpf.Converters.Types
{
    public class Cast : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is Type t)
            {
                var typeConverter = TypeDescriptor.GetConverter(t);

                // ReSharper disable once AssignNullToNotNullAttribute
                return typeConverter.ConvertTo(value, t);
            }

            throw new ArgumentException("Parameter needs to be a Type.", nameof(parameter));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new ConvertBackNotSupportedException();
    }
}
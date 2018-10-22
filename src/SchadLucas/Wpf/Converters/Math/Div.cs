using System;
using System.Globalization;
using System.Windows.Data;

namespace SchadLucas.Wpf.Converters.Math
{
    public class Div : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return MathConverter.Convert(values, (x, y) => x / y);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => throw new ConvertBackNotSupportedException();
    }
}
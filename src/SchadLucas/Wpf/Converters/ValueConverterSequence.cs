using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace SchadLucas.Wpf.Converters
{
    /// <summary>
    ///     Allows to chain a sequence of IValueConverter.
    /// </summary>
    /// <remarks>https://stackoverflow.com/a/8326207/3450580</remarks>
    public class ValueConverterSequence : List<IValueConverter>, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.Aggregate(value, (current, converter) => converter.Convert(current, targetType, parameter, culture));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new ConvertBackNotSupportedException();
    }
}
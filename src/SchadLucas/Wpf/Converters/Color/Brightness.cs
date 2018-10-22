using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace SchadLucas.Wpf.Converters.Color
{
    public class Brightness : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            System.Windows.Media.Color? color = null;
            float? factor = null;

            switch (value)
            {
                case System.Windows.Media.Color c:
                    color = c;
                    break;

                case string s:
                    var converted = ColorConverter.ConvertFromString(s);
                    if (converted != null)
                    {
                        color = (System.Windows.Media.Color) converted;
                    }

                    break;
            }

            switch (parameter)
            {
                case float i:
                    factor = i;
                    break;

                case string ps:
                    factor = float.Parse(ps);
                    break;
            }

            if (color == null)
            {
                throw new ArgumentException(nameof(value));
            }

            if (factor == null)
            {
                throw new ArgumentException(nameof(parameter));
            }

            return ColorHelper.ChangeBrightness((System.Windows.Media.Color) color, (decimal) factor);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new ConvertBackNotSupportedException();
    }
}
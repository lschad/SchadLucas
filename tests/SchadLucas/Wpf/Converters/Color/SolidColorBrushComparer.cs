using System.Collections.Generic;
using System.Windows.Media;

namespace SchadLucas.Wpf.Converters.Tests.Color
{
    internal class SolidColorBrushComparer : IEqualityComparer<SolidColorBrush>
    {
        public bool Equals(SolidColorBrush x, SolidColorBrush y)
        {
            if (x is null || y is null)
            {
                return false;
            }

            return x.Color == y.Color && x.Opacity == y.Opacity;
        }

        public int GetHashCode(SolidColorBrush obj)
        {
            return new {C = obj.Color, O = obj.Opacity}.GetHashCode();
        }
    }
}
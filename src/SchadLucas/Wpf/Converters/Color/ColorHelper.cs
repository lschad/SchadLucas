using System;
using System.Windows.Media;

namespace SchadLucas.Wpf.Converters.Color
{
    internal static class ColorHelper
    {
        /// <summary>
        ///     Creates color with corrected brightness.
        /// </summary>
        /// <param name="color">Color to correct.</param>
        /// <param name="correctionFactor">
        ///     The brightness correction factor. Must be between -1 and 1. Negative values produce
        ///     darker colors.
        /// </param>
        /// <returns>Corrected <see cref="Color" /> structure.</returns>
        public static System.Windows.Media.Color ChangeBrightness(System.Windows.Media.Color color, decimal correctionFactor)
        {
            if (correctionFactor > 1 || correctionFactor < -1)
            {
                throw new ArgumentOutOfRangeException(nameof(correctionFactor), "Value has to be between -1 and 1.");
            }

            var factor = (float) correctionFactor;

            var red = (float) color.R;
            var green = (float) color.G;
            var blue = (float) color.B;

            if (correctionFactor < 0)
            {
                factor = (float) (1m + correctionFactor);
                red *= factor;
                green *= factor;
                blue *= factor;
            }
            else
            {
                red = (255 - red) * factor + red;
                green = (255 - green) * factor + green;
                blue = (255 - blue) * factor + blue;
            }

            return System.Windows.Media.Color.FromArgb(color.A, (byte) red, (byte) green, (byte) blue);
        }
    }
}
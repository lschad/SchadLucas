using System.Windows.Controls;
using System.Windows.Media;

namespace SchadLucas.Wpf.FontAwesome
{
    internal static class TextBlockExtensions
    {
        public static void SetTextAndFamily(this TextBlock textBlock, FontFamily family, char text)
        {
            textBlock.FontFamily = family;
            textBlock.Text = text.ToString();
        }
    }
}
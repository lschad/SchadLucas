using System;
using System.Windows.Media;

namespace SchadLucas.Wpf.FontAwesome
{
    internal static class Fonts
    {
        private const string PATH = "./FontAwesome/use-on-desktop";

        private static FontFamily _brands;
        private static FontFamily _regular;
        private static FontFamily _solid;

        private static Uri Uri { get; } = new Uri("pack://application:,,,/SchadLucas.Wpf.FontAwesome;component/");

        internal static FontFamily Regular => _regular ?? (_regular = GetFont("Free Regular"));
        internal static FontFamily Brands => _brands ?? (_brands = GetFont("Brands Regular"));
        internal static FontFamily Solid => _solid ?? (_solid = GetFont("Free Solid"));

        private static FontFamily GetFont(string name) => new FontFamily(Uri, GetPath(name));

        private static string GetPath(string name) => $"{PATH}/#Font Awesome 5 {name}";
    }
}
using System;
using System.Windows.Media;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchadLucas.Tests.Basics;
using SchadLucas.Wpf.Converters.Color;

namespace SchadLucas.Wpf.Converters.Tests.Color
{
    [TestClass]
    [TestCategory("Converters")]
    public class ColorToSolidColorBrushTests : EzTest
    {
        private static readonly ValueConverSutHelper Converter = new ValueConverSutHelper(() => new ColorToSolidColorBrush());
        private static SolidColorBrushComparer SolidBrushComparer => new SolidColorBrushComparer();

        [TestMethod]
        public void Convert_ThrowsException_WhenWrongValue()
        {
            Assert.ThrowsException<ArgumentException>(() => Converter.Convert(null));
            Assert.ThrowsException<ArgumentException>(() => Converter.Convert(1234));
            Assert.ThrowsException<ArgumentException>(() => Converter.Convert("####"));
        }

        [TestMethod]
        public void ConvertBack_ThrowsException_WhenWrongValue()
        {
            Assert.ThrowsException<ArgumentException>(() => Converter.ConvertBack(null));
            Assert.ThrowsException<ArgumentException>(() => Converter.ConvertBack(1234));
            Assert.ThrowsException<ArgumentException>(() => Converter.ConvertBack("####"));
        }

        [TestMethod]
        public void ConvertBackTest()
        {
            var actual = Converter.ConvertBack<System.Windows.Media.Color>(Brushes.Red);
            var expected = Colors.Red;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ConvertTest()
        {
            var expected = new SolidColorBrush(Colors.Red);
            var actual = Converter.Convert<SolidColorBrush>(Colors.Red);

            Assert.IsTrue(SolidBrushComparer.Equals(expected, actual));
        }
    }
}
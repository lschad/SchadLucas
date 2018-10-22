using System;
using System.Windows.Media;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchadLucas.Tests.Basics;
using SchadLucas.Wpf.Converters.Color;

namespace SchadLucas.Wpf.Converters.Tests.Color
{
    [TestClass]
    [TestCategory("Converters")]
    public class BrightnessTests : EzTest
    {
        private static readonly ValueConverSutHelper Converter = new ValueConverSutHelper(() => new Brightness());

        [TestMethod]
        public void Convert_FromString()
        {
            Assert.AreEqual(System.Windows.Media.Color.FromArgb(255, 178, 178, 255), Converter.Convert("Blue", "0.7"));
            Assert.AreEqual(System.Windows.Media.Color.FromArgb(255, 178, 178, 255), Converter.Convert("blue", "0.7"));
            Assert.AreEqual(System.Windows.Media.Color.FromArgb(255, 178, 178, 255), Converter.Convert("BLue", "0.7"));
            Assert.AreEqual(System.Windows.Media.Color.FromArgb(255, 178, 178, 255), Converter.Convert("BLUE", "0.7"));
        }

        [TestMethod]
        public void Convert_NegativeValue()
        {
            Assert.AreEqual(System.Windows.Media.Color.FromArgb(255, 0, 0, 229), Converter.Convert(Colors.Blue, -0.1f));
            Assert.AreEqual(System.Windows.Media.Color.FromArgb(255, 0, 0, 204), Converter.Convert(Colors.Blue, -0.2f));
            Assert.AreEqual(System.Windows.Media.Color.FromArgb(255, 0, 0, 178), Converter.Convert(Colors.Blue, -0.3f));
            Assert.AreEqual(System.Windows.Media.Color.FromArgb(255, 0, 0, 153), Converter.Convert(Colors.Blue, -0.4f));
            Assert.AreEqual(System.Windows.Media.Color.FromArgb(255, 0, 0, 127), Converter.Convert(Colors.Blue, -0.5f));
            Assert.AreEqual(System.Windows.Media.Color.FromArgb(255, 0, 0, 102), Converter.Convert(Colors.Blue, -0.6f));
            Assert.AreEqual(System.Windows.Media.Color.FromArgb(255, 0, 0, 76), Converter.Convert(Colors.Blue, -0.7f));
            Assert.AreEqual(System.Windows.Media.Color.FromArgb(255, 0, 0, 51), Converter.Convert(Colors.Blue, -0.8f));
            Assert.AreEqual(System.Windows.Media.Color.FromArgb(255, 0, 0, 25), Converter.Convert(Colors.Blue, -0.9f));
            Assert.AreEqual(System.Windows.Media.Color.FromArgb(255, 0, 0, 0), Converter.Convert(Colors.Blue, -1f));
        }

        [TestMethod]
        public void Convert_PositiveValue()
        {
            Assert.AreEqual(System.Windows.Media.Color.FromArgb(255, 255, 255, 255), Converter.Convert(Colors.Blue, 1f));
            Assert.AreEqual(System.Windows.Media.Color.FromArgb(255, 229, 229, 255), Converter.Convert(Colors.Blue, 0.9f));
            Assert.AreEqual(System.Windows.Media.Color.FromArgb(255, 204, 204, 255), Converter.Convert(Colors.Blue, 0.8f));
            Assert.AreEqual(System.Windows.Media.Color.FromArgb(255, 178, 178, 255), Converter.Convert(Colors.Blue, 0.7f));
            Assert.AreEqual(System.Windows.Media.Color.FromArgb(255, 153, 153, 255), Converter.Convert(Colors.Blue, 0.6f));
            Assert.AreEqual(System.Windows.Media.Color.FromArgb(255, 127, 127, 255), Converter.Convert(Colors.Blue, 0.5f));
            Assert.AreEqual(System.Windows.Media.Color.FromArgb(255, 102, 102, 255), Converter.Convert(Colors.Blue, 0.4f));
            Assert.AreEqual(System.Windows.Media.Color.FromArgb(255, 76, 76, 255), Converter.Convert(Colors.Blue, 0.3f));
            Assert.AreEqual(System.Windows.Media.Color.FromArgb(255, 51, 51, 255), Converter.Convert(Colors.Blue, 0.2f));
            Assert.AreEqual(System.Windows.Media.Color.FromArgb(255, 25, 25, 255), Converter.Convert(Colors.Blue, 0.1f));
            Assert.AreEqual(System.Windows.Media.Color.FromArgb(255, 0, 0, 255), Converter.Convert(Colors.Blue, 0f));
        }

        [TestMethod]
        public void Convert_ThrowsException_WhenColorNull()
        {
            Assert.ThrowsException<ArgumentException>(() => Converter.Convert(null, 0.1f));
        }

        [TestMethod]
        public void Convert_ThrowsException_WhenFactorNull()
        {
            Assert.ThrowsException<ArgumentException>(() => Converter.Convert(Colors.Blue));
        }

        [TestMethod]
        public void Convert_ThrowsException_WhenOutOfRange()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Converter.Convert(Colors.Blue, -1.1f));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Converter.Convert(Colors.Blue, -23f));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Converter.Convert(Colors.Blue, -2349f));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Converter.Convert(Colors.Blue, 1.1f));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Converter.Convert(Colors.Blue, 23f));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Converter.Convert(Colors.Blue, 2349f));
        }

        [TestMethod]
        public void ConvertBack_ThrowsException_WhenWrongValue()
        {
            Assert.ThrowsException<ConvertBackNotSupportedException>(() => Converter.ConvertBack(1234));
            Assert.ThrowsException<ConvertBackNotSupportedException>(() => Converter.ConvertBack(null));
            Assert.ThrowsException<ConvertBackNotSupportedException>(() => Converter.ConvertBack(default));
            Assert.ThrowsException<ConvertBackNotSupportedException>(() => Converter.ConvertBack("1234"));
            Assert.ThrowsException<ConvertBackNotSupportedException>(() => Converter.ConvertBack(new object[1234]));
        }
    }
}
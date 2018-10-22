using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchadLucas.Tests.Basics;
using SchadLucas.Wpf.Converters.Visibility;

namespace SchadLucas.Wpf.Converters.Tests.Visibility
{
    [TestClass]
    [TestCategory("Converters")]
    public class NegativeBooleanToVisibilityTests : EzTest
    {
        private static readonly ValueConverSutHelper Converter = new ValueConverSutHelper(() => new NegativeBooleanToVisibility());

        [TestMethod]
        public void ConvertBackTest()
        {
            Assert.AreEqual(true, Converter.ConvertBack<bool>(System.Windows.Visibility.Collapsed));
            Assert.AreEqual(true, Converter.ConvertBack<bool>(System.Windows.Visibility.Hidden));
            Assert.AreEqual(false, Converter.ConvertBack<bool>(System.Windows.Visibility.Visible));
        }

        [TestMethod]
        public void ConvertBackThrowsArgumentExceptionTest()
        {
            Assert.ThrowsException<ArgumentException>(() => Converter.ConvertBack(null));
            Assert.ThrowsException<ArgumentException>(() => Converter.ConvertBack("absadj"));
            Assert.ThrowsException<ArgumentException>(() => Converter.ConvertBack(12345));
            Assert.ThrowsException<ArgumentException>(() => Converter.ConvertBack("Visible"));
            Assert.ThrowsException<ArgumentException>(() => Converter.ConvertBack("Hidden"));
            Assert.ThrowsException<ArgumentException>(() => Converter.ConvertBack("Collapsed"));
            Assert.ThrowsException<ArgumentException>(() => Converter.ConvertBack("Visibility.Visible"));
            Assert.ThrowsException<ArgumentException>(() => Converter.ConvertBack("Visibility.Hidden"));
            Assert.ThrowsException<ArgumentException>(() => Converter.ConvertBack("Visibility.Collapsed"));
        }

        [TestMethod]
        public void ConvertTest()
        {
            Assert.AreEqual(System.Windows.Visibility.Collapsed, Converter.Convert<System.Windows.Visibility>(true));
            Assert.AreEqual(System.Windows.Visibility.Visible, Converter.Convert<System.Windows.Visibility>(false));
            Assert.AreNotEqual(System.Windows.Visibility.Hidden, Converter.Convert<System.Windows.Visibility>(true));
        }

        [TestMethod]
        public void ConvertThrowsArgumentExceptionTest()
        {
            Assert.ThrowsException<ArgumentException>(() => Converter.Convert(null));
            Assert.ThrowsException<ArgumentException>(() => Converter.Convert("absadj"));
            Assert.ThrowsException<ArgumentException>(() => Converter.Convert(12345));
            Assert.ThrowsException<ArgumentException>(() => Converter.Convert("Visible"));
            Assert.ThrowsException<ArgumentException>(() => Converter.Convert("Hidden"));
            Assert.ThrowsException<ArgumentException>(() => Converter.Convert("Collapsed"));
            Assert.ThrowsException<ArgumentException>(() => Converter.Convert("Visibility.Visible"));
            Assert.ThrowsException<ArgumentException>(() => Converter.Convert("Visibility.Hidden"));
            Assert.ThrowsException<ArgumentException>(() => Converter.Convert("Visibility.Collapsed"));
        }
    }
}
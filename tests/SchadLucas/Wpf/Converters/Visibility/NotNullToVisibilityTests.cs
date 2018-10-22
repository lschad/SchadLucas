using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchadLucas.Tests.Basics;
using SchadLucas.Wpf.Converters.Visibility;

namespace SchadLucas.Wpf.Converters.Tests.Visibility
{
    [TestClass]
    [TestCategory("Converters")]
    public class NotNullToVisibilityTests : EzTest
    {
        private static readonly ValueConverSutHelper Converter = new ValueConverSutHelper(() => new NotNullToVisibility());

        [TestMethod]
        public void ConvertBackThrowsNotSupportedExceptionTest()
        {
            Assert.ThrowsException<ConvertBackNotSupportedException>(() => Converter.ConvertBack(default));
            Assert.ThrowsException<ConvertBackNotSupportedException>(() => Converter.ConvertBack(null));
            Assert.ThrowsException<ConvertBackNotSupportedException>(() => Converter.ConvertBack(1234));
            Assert.ThrowsException<ConvertBackNotSupportedException>(() => Converter.ConvertBack("foobar"));
            Assert.ThrowsException<ConvertBackNotSupportedException>(() => Converter.ConvertBack(new object()));
        }

        [TestMethod]
        public void ConvertTest()
        {
            Assert.AreEqual(System.Windows.Visibility.Collapsed, Converter.Convert(null));
            Assert.AreEqual(System.Windows.Visibility.Visible, Converter.Convert(1234));
            Assert.AreEqual(System.Windows.Visibility.Visible, Converter.Convert("foobar"));
            Assert.AreEqual(System.Windows.Visibility.Visible, Converter.Convert(new object()));
        }
    }
}
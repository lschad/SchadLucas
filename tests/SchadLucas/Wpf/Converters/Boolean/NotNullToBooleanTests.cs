using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchadLucas.Tests.Basics;
using SchadLucas.Wpf.Converters.Boolean;

namespace SchadLucas.Wpf.Converters.Tests.Boolean
{
    [TestClass]
    [TestCategory("Converters")]
    public class NotNullToBooleanTests : EzTest
    {
        private static readonly ValueConverSutHelper Converter = new ValueConverSutHelper(() => new NotNullToBoolean());

        [TestMethod]
        public void ConvertBackThrowsNotSupportedExceptionTest()
        {
            Assert.ThrowsException<ConvertBackNotSupportedException>(() => Converter.ConvertBack(default));
            Assert.ThrowsException<ConvertBackNotSupportedException>(() => Converter.ConvertBack(null));
            Assert.ThrowsException<ConvertBackNotSupportedException>(() => Converter.ConvertBack(1234));
            Assert.ThrowsException<ConvertBackNotSupportedException>(() => Converter.ConvertBack("foobar"));
        }

        [TestMethod]
        public void ConvertTest()
        {
            Assert.IsFalse(Converter.Convert<bool>(null));
            Assert.IsTrue(Converter.Convert<bool>(new object()));
            Assert.IsTrue(Converter.Convert<bool>(1234));
            Assert.IsTrue(Converter.Convert<bool>("foobar"));
        }
    }
}
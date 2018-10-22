using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchadLucas.Tests.Basics;
using SchadLucas.Wpf.Converters.Boolean;

namespace SchadLucas.Wpf.Converters.Tests.Boolean
{
    [TestClass]
    [TestCategory("Converters")]
    public class NullToBooleanTests : EzTest
    {
        private static readonly ValueConverSutHelper Converter = new ValueConverSutHelper(() => new NullToBoolean());

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
            Assert.IsTrue(Converter.Convert<bool>(null));
            Assert.IsFalse(Converter.Convert<bool>(new object()));
            Assert.IsFalse(Converter.Convert<bool>(1234));
            Assert.IsFalse(Converter.Convert<bool>("foobar"));
        }
    }
}
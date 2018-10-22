using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchadLucas.Tests.Basics;
using SchadLucas.Wpf.Converters.Boolean;

namespace SchadLucas.Wpf.Converters.Tests
{
    [TestClass]
    [TestCategory("Converters")]
    public class ValueConverterSequenceTests : EzTest
    {
        private static ValueConverSutHelper Converter => new ValueConverSutHelper(() => new ValueConverterSequence
        {
            new NullToBoolean(),
            new NegateBoolean()
        });

        [TestMethod]
        public void ConvertBackThrowsNotSupportedExceptionTest()
        {
            Assert.ThrowsException<ConvertBackNotSupportedException>(() => Converter.ConvertBack(null));
            Assert.ThrowsException<ConvertBackNotSupportedException>(() => Converter.ConvertBack(1234));
            Assert.ThrowsException<ConvertBackNotSupportedException>(() => Converter.ConvertBack("abcd"));
            Assert.ThrowsException<ConvertBackNotSupportedException>(() => Converter.ConvertBack(new object[23]));
        }

        [TestMethod]
        public void ConverTest()
        {
            var result = Converter.Convert(null);
            Assert.IsInstanceOfType(result, typeof(bool));
            Assert.IsFalse((bool) result);

            result = Converter.Convert(1234);
            Assert.IsInstanceOfType(result, typeof(bool));
            Assert.IsTrue((bool) result);
        }
    }
}
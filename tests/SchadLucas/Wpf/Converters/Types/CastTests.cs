using System;
using System.Windows.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchadLucas.Tests.Basics;

namespace SchadLucas.Wpf.Converters.Tests.Types
{
    [TestClass]
    [TestCategory("Converters")]
    [TestCategory("Converters.Types")]
    public class Cast : EzTest
    {
        private static readonly ValueConverSutHelper Converter = new ValueConverSutHelper(() => new Converters.Types.Cast());

        [TestMethod]
        public void Convert_CastsType()
        {
            Assert.AreEqual(123, Converter.Convert("123", typeof(int)));
            Assert.AreEqual("123", Converter.Convert(123, typeof(string)));
        }

        [TestMethod]
        public void Convert_Throws_OnInvalidCast()
        {
            Assert.ThrowsException<FormatException>(() => Converter.Convert("abcd", typeof(int)));
            Assert.ThrowsException<InvalidCastException>(() => Converter.Convert(null, typeof(int)));
            Assert.ThrowsException<NotSupportedException>(() => Converter.Convert(123, typeof(IValueConverter)));
        }

        [TestMethod]
        public void Convert_ThrowsException_WhenProvidedParameterIsNotAType()
        {
            Assert.ThrowsException<ArgumentException>(() => Converter.Convert("abc", "def"));
            Assert.ThrowsException<ArgumentException>(() => Converter.Convert(123, "def"));
            Assert.ThrowsException<ArgumentException>(() => Converter.Convert("abc", 123));
            Assert.ThrowsException<ArgumentException>(() => Converter.Convert(123, 456));
            Assert.ThrowsException<ArgumentException>(() => Converter.Convert(null, 456));
            Assert.ThrowsException<ArgumentException>(() => Converter.Convert(null));
            Assert.ThrowsException<ArgumentException>(() => Converter.Convert(123));
            Assert.ThrowsException<ArgumentException>(() => Converter.Convert(new object[23]));
            Assert.ThrowsException<ArgumentException>(() => Converter.Convert(new object()));
        }

        [TestMethod]
        public void ConvertBack_ThrowsException()
        {
            Assert.ThrowsException<ConvertBackNotSupportedException>(() => Converter.ConvertBack(default));
            Assert.ThrowsException<ConvertBackNotSupportedException>(() => Converter.ConvertBack(1, 2));
            Assert.ThrowsException<ConvertBackNotSupportedException>(() => Converter.ConvertBack(1, typeof(int)));
            Assert.ThrowsException<ConvertBackNotSupportedException>(() => Converter.ConvertBack(1, typeof(string)));
            Assert.ThrowsException<ConvertBackNotSupportedException>(() => Converter.ConvertBack("abc", typeof(string)));
        }
    }
}
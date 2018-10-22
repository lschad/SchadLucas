using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchadLucas.Tests.Basics;
using SchadLucas.Wpf.Converters.Boolean;

namespace SchadLucas.Wpf.Converters.Tests.Boolean
{
    [TestClass]
    [TestCategory("Converters")]
    public class NegateBooleanTest : EzTest
    {
        private static readonly ValueConverSutHelper Converter = new ValueConverSutHelper(() => new NegateBoolean());

        [TestMethod]
        public void ConvertBackTest()
        {
            Assert.IsTrue(Converter.ConvertBack<bool>(false));
            Assert.IsFalse(Converter.ConvertBack<bool>(true));
        }

        [TestMethod]
        public void ConvertBackThrowsWhenPassedWrongValue()
        {
            Assert.ThrowsException<ArgumentException>(() => Converter.ConvertBack(null));
            Assert.ThrowsException<ArgumentException>(() => Converter.ConvertBack(new object()));
            Assert.ThrowsException<ArgumentException>(() => Converter.ConvertBack(1234));
            Assert.ThrowsException<ArgumentException>(() => Converter.ConvertBack("abcd"));
            Assert.ThrowsException<ArgumentException>(() => Converter.ConvertBack(new object[23]));
        }

        [TestMethod]
        public void ConvertTest()
        {
            Assert.IsTrue(Converter.Convert<bool>(false));
            Assert.IsFalse(Converter.Convert<bool>(true));
        }

        [TestMethod]
        public void ConvertThrowsWhenPassedWrongValue()
        {
            Assert.ThrowsException<ArgumentException>(() => Converter.Convert(null));
            Assert.ThrowsException<ArgumentException>(() => Converter.Convert(new object()));
            Assert.ThrowsException<ArgumentException>(() => Converter.Convert(1234));
            Assert.ThrowsException<ArgumentException>(() => Converter.Convert("abcd"));
            Assert.ThrowsException<ArgumentException>(() => Converter.Convert(new object[23]));
        }
    }
}
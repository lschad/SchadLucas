using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchadLucas.Tests.Basics;
using SchadLucas.Wpf.Converters.Math;

namespace SchadLucas.Wpf.Converters.Tests.Math
{
    [TestClass]
    [TestCategory("Converters")]
    public class SqrtTests : EzTest
    {
        private static readonly ValueConverSutHelper Converter = new ValueConverSutHelper(() => new Sqrt());

        [TestMethod]
        public void Convert_ThrowsException_OnWrongValue()
        {
            Assert.ThrowsException<ArgumentException>(() => Converter.Convert("abcd"));
            Assert.ThrowsException<ArgumentException>(() => Converter.Convert(null));
            Assert.ThrowsException<ArgumentException>(() => Converter.Convert(new object()));
        }

        [TestMethod]
        public void ConvertBack_ThrowsException_OnWrongValue()
        {
            Assert.ThrowsException<ArgumentException>(() => Converter.ConvertBack("abcd"));
            Assert.ThrowsException<ArgumentException>(() => Converter.ConvertBack(null));
            Assert.ThrowsException<ArgumentException>(() => Converter.ConvertBack(new object()));
        }

        [TestMethod]
        public void ConvertBackTest()
        {
            Assert.AreEqual(4d, Converter.ConvertBack<double>(2));
            Assert.AreEqual(16d, Converter.ConvertBack<double>(4));
        }

        [TestMethod]
        public void ConvertTest()
        {
            Assert.AreEqual(2d, Converter.Convert<double>(4));
            Assert.AreEqual(4d, Converter.Convert<double>(16));
        }
    }
}
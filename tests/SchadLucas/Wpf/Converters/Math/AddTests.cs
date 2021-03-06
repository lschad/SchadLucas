using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchadLucas.Tests.Basics;
using SchadLucas.Wpf.Converters.Math;

namespace SchadLucas.Wpf.Converters.Tests.Math
{
    [TestClass]
    [TestCategory("Converters")]
    public class AddTests : EzTest
    {
        private static readonly MultiValueConverSutHelper Converter = new MultiValueConverSutHelper(() => new Add());

        [TestMethod]
        public void ConvertBackTest()
        {
            Assert.ThrowsException<ConvertBackNotSupportedException>(() => Converter.ConvertBack(default));
        }

        [TestMethod]
        public void ConvertManyTest()
        {
            var rnd = new Random();

            for (var x = 2; x < 99; x++)
            {
                var numbers = new object[x];
                for (var i = 0; i < x; i++)
                {
                    numbers[i] = new decimal(rnd.Next(0, 999));
                }

                Assert.AreEqual(numbers.Select(n => (decimal) n).Sum(), Converter.Convert<decimal>(numbers));
            }
        }

        [TestMethod]
        public void ConvertTest()
        {
            Assert.AreEqual(6m, Converter.Convert(new object[] {1, 2, 3}));
            Assert.AreEqual(-4m, Converter.Convert(new object[] {-3, -1}));
            Assert.AreEqual(5m, Converter.Convert(new object[] {1, 1, 1, 1, 1}));
            Assert.AreEqual(0m, Converter.Convert(new object[] {-2, 2}));
            Assert.AreEqual(48m, Converter.Convert(new object[] {"24", "24"}));
            Assert.AreEqual(48m, Converter.Convert(new object[] {"24", 24}));
            Assert.AreEqual(5m, Converter.Convert(new object[] {1.25, 3.75}));
            Assert.AreEqual(6.5m, Converter.Convert(new object[] {1.25, 3.75, 1.5}));
            Assert.AreEqual(15m, Converter.Convert(new object[] {1.25, 3.75, 10}));
            Assert.AreEqual(4.9m, Converter.Convert(new object[] {1.2, 3.7}));
            Assert.AreEqual(3.69m, Converter.Convert(new object[] {1.23, 1.23, 1.23}));
        }

        [TestMethod]
        public void ConvertThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() => Converter.Convert(new object[0]));
            Assert.ThrowsException<ArgumentException>(() => Converter.Convert(new object[497]));
            Assert.ThrowsException<ArgumentException>(() => Converter.Convert(null));
            Assert.ThrowsException<ArgumentException>(() => Converter.Convert(new object[] {1, null, "not-valid"}));
        }
    }
}
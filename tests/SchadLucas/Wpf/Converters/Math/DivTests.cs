using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchadLucas.Tests.Basics;
using SchadLucas.Wpf.Converters.Math;

namespace SchadLucas.Wpf.Converters.Tests.Math
{
    [TestClass]
    [TestCategory("Converters")]
    public class DivTests : EzTest
    {
        private static readonly MultiValueConverSutHelper Converter = new MultiValueConverSutHelper(() => new Div());

        [TestMethod]
        public void ConvertBackTest()
        {
            Assert.ThrowsException<ConvertBackNotSupportedException>(() => Converter.ConvertBack(default));
        }

        [TestMethod]
        public void ConvertManyTest()
        {
            var rnd = new Random();

            for (var x = 2; x < 50; x++)
            {
                var numbers = new object[x];
                for (var i = 0; i < x; i++)
                {
                    numbers[i] = new decimal(rnd.Next(1, 999));
                }

                var result = numbers.Select(n => (decimal) n).Aggregate((i, j) => i / j);

                Assert.AreEqual(result, Converter.Convert<decimal>(numbers));
            }
        }

        [TestMethod]
        public void ConvertTest()
        {
            Assert.AreEqual(1m, Converter.Convert(new object[] {1, 1}));
            Assert.AreEqual(2m, Converter.Convert(new object[] {2, 1}));
            Assert.AreEqual(-3m, Converter.Convert(new object[] {-3, 1}));
            Assert.AreEqual(3m, Converter.Convert(new object[] {3, 1}));
            Assert.AreEqual(1m / 3m, Converter.Convert(new object[] {1, 3}));
            Assert.AreEqual(1m, Converter.Convert(new object[] {-2, -2}));
            Assert.AreEqual(-1m, Converter.Convert(new object[] {-2, 2}));
            Assert.AreEqual(-1m, Converter.Convert(new object[] {2, -2}));
            Assert.AreEqual(1m, Converter.Convert(new object[] {16, 2, 2, 2, 2}));
            Assert.AreEqual(2m, Converter.Convert(new object[] {"24", "12"}));
            Assert.AreEqual(2m, Converter.Convert(new object[] {"24", 12}));
            Assert.AreEqual(0.22726017562268735848014579996686m, Converter.Convert(new object[] {1.2345m, 5.4321m}));
            Assert.AreEqual(80.555555555555555555555555555556m, Converter.Convert(new object[] {2.9, 0.3, 0.4, 0.5, 0.6}));
        }

        [TestMethod]
        public void ConvertThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() => Converter.Convert(new object[0]));
            Assert.ThrowsException<ArgumentException>(() => Converter.Convert(new object[497]));
            Assert.ThrowsException<ArgumentException>(() => Converter.Convert(null));
            Assert.ThrowsException<ArgumentException>(() => Converter.Convert(new object[] {1, null, "not-valid"}));
        }

        [TestMethod]
        public void ConvertThrowsDivideByZeroException()
        {
            Assert.ThrowsException<DivideByZeroException>(() => Converter.Convert(new object[] {24, 0}));
            Assert.ThrowsException<DivideByZeroException>(() => Converter.Convert(new object[] {123, 456, 789, 0}));
            Assert.ThrowsException<DivideByZeroException>(() => Converter.Convert(new object[] {1.23, 4.56, 78.9, 0.0}));
        }
    }
}
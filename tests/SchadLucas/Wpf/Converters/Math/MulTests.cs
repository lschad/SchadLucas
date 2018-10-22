using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchadLucas.Tests.Basics;
using SchadLucas.Wpf.Converters.Math;

namespace SchadLucas.Wpf.Converters.Tests.Math
{
    [TestClass]
    [TestCategory("Converters")]
    public class MulTests : EzTest
    {
        private static readonly MultiValueConverSutHelper Converter = new MultiValueConverSutHelper(() => new Mul());

        [TestMethod]
        public void ConvertBackTest()
        {
            Assert.ThrowsException<ConvertBackNotSupportedException>(() => Converter.ConvertBack(default));
        }

        [TestMethod]
        public void ConvertTest()
        {
            var rnd = new Random();

            for (var i = 0; i < 99; i++)
            {
                var maxJ = rnd.Next(2, 15);
                var numbers = new object[maxJ];

                for (var j = 0; j < maxJ; j++)
                {
                    numbers[j] = new decimal(rnd.Next(-9, 9));
                }

                var result = numbers.Select(n => (decimal) n).Aggregate((x, y) => x * y);

                Assert.AreEqual(result, Converter.Convert(numbers));
            }
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
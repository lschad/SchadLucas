﻿using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchadLucas.Tests.Basics;
using SchadLucas.Wpf.Converters.Math;

namespace SchadLucas.Wpf.Converters.Tests.Math
{
    [TestClass]
    [TestCategory("Converters")]
    public class SubTests : EzTest
    {
        private static readonly MultiValueConverSutHelper Converter = new MultiValueConverSutHelper(() => new Sub());

        [TestMethod]
        public void ConvertBackThrowsConvertBackNotSupportedExceptionTest()
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

                var result = numbers.Select(n => (decimal) n).Aggregate((i, j) => i - j);

                Assert.AreEqual(result, Converter.Convert<decimal>(numbers));
            }
        }

        [TestMethod]
        public void ConvertTest()
        {
            Assert.AreEqual(1m, Converter.Convert<decimal>(new object[] {4, 2, 1}));
            Assert.AreEqual(-3m, Converter.Convert<decimal>(new object[] {1, 1, 1, 1, 1}));
            Assert.AreEqual(-4m, Converter.Convert<decimal>(new object[] {-2, 2}));
            Assert.AreEqual(4m, Converter.Convert<decimal>(new object[] {2, -2}));
            Assert.AreEqual(0m, Converter.Convert<decimal>(new object[] {2, 2}));
            Assert.AreEqual(12m, Converter.Convert<decimal>(new object[] {"24", "12"}));
            Assert.AreEqual(12m, Converter.Convert<decimal>(new object[] {"24", 12}));
            Assert.AreEqual(1m, Converter.Convert<decimal>(new object[] {2.9, 1.9}));
            Assert.AreEqual(2m, Converter.Convert<decimal>(new object[] {2.9, 0.9}));
            Assert.AreEqual(3m, Converter.Convert<decimal>(new object[] {2.1, -0.9}));
            Assert.AreEqual(-3m, Converter.Convert<decimal>(new object[] {-2.1, 0.9}));
            Assert.AreEqual(-9.1m, Converter.Convert<decimal>(new object[] {-2.1, 0.9, 3, 3.1}));
        }

        [TestMethod]
        public void ConvertThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() => Converter.Convert(new object[0]));
            Assert.ThrowsException<ArgumentException>(() => Converter.Convert(new object[497]));
            Assert.ThrowsException<ArgumentException>(() => Converter.Convert(null));
            Assert.ThrowsException<ArgumentException>(() => Converter.Convert(new object[] {1, null, "not-valid"}));
            Assert.ThrowsException<ArgumentException>(() => Converter.Convert(new object[] {"four", "three", "two", "one"}));
        }
    }
}
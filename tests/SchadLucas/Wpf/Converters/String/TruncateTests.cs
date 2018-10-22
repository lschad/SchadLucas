using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchadLucas.Tests.Basics;
using SchadLucas.Wpf.Converters.String;

namespace SchadLucas.Wpf.Converters.Tests.String
{
    [TestClass]
    public class TruncateTests : EzTest
    {
        private static readonly ValueConverSutHelper Converter = new ValueConverSutHelper(() => new Truncate());

        [TestMethod]
        public void ConvertBackNotSupported()
        {
            EzAssert.That(() => Converter.ConvertBack(null)).Throws<ConvertBackNotSupportedException>();
            EzAssert.That(() => Converter.ConvertBack("foo")).Throws<ConvertBackNotSupportedException>();
            EzAssert.That(() => Converter.ConvertBack("foo", 123)).Throws<ConvertBackNotSupportedException>();
            EzAssert.That(() => Converter.ConvertBack("foo", "bar")).Throws<ConvertBackNotSupportedException>();
        }

        [TestMethod]
        public void IfIntIsLessThanString_ReturnString()
        {
            for (var i = 0; i < 99; i++)
            {
                var word = RandomString();
                var len = word.Length + 1;

                EzAssert
                    .That(Converter.Convert(word, len))
                    .IsEqualTo(word);
            }
        }


        [TestMethod]
        public void LessThan0_ThrowsException()
        {
            for (var i = 0; i < 99; i++)
            {
                EzAssert
                    .That(() => Converter.Convert(RandomString(), RandomNegativeNumber()))
                    .Throws<ArgumentException>();
            }
        }

        [TestMethod]
        public void Test()
        {
            const int len = 3;
            const string actual = "foo bar";
            const string expected = "foo";

            EzAssert
                .That(Converter.Convert(actual, len))
                .IsEqualTo(expected);
        }


        [TestMethod]
        [SuppressMessage("ReSharper", "RedundantArgumentDefaultValue")]
        public void WrongParameter_ThrowsException()
        {
            EzAssert.That(() => Converter.Convert(RandomString(), 2.5f)).Throws<ArgumentException>();
            EzAssert.That(() => Converter.Convert(RandomString(), 2.5d)).Throws<ArgumentException>();
            EzAssert.That(() => Converter.Convert(RandomString(), 2.5m)).Throws<ArgumentException>();
            EzAssert.That(() => Converter.Convert(RandomString(), (byte) 2)).Throws<ArgumentException>();
            EzAssert.That(() => Converter.Convert(RandomString(), "abc")).Throws<ArgumentException>();
            EzAssert.That(() => Converter.Convert(RandomString(), null)).Throws<ArgumentException>();
        }
    }
}
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchadLucas.Tests.Basics;
using SchadLucas.Utilities.Extensions.System;

namespace SchadLucas.Utilities.Tests.Extensions.System
{
    [TestClass]
    public class TypeExtensionsTests
    {
        [TestMethod]
        public void GetDefault_ReturnsNull_ForReferenceTypes()
        {
            EzAssert.That(typeof(object).GetDefault()).IsEqualTo(null);
            EzAssert.That(typeof(TypeExtensionsTests).GetDefault()).IsEqualTo(null);
        }

        [TestMethod]
        public void GetDefault_ReturnsDefault_ForValueTypes()
        {
            void Test<TType>()
            {
                var defaultValue = typeof(TType).GetDefault();
                EzAssert.That(defaultValue)
                        .IsTypeOf<TType>()
                        .And
                        .IsDefault<TType>();
            }

            Test<sbyte>();
            Test<byte>();
            Test<short>();
            Test<ushort>();
            Test<int>();
            Test<uint>();
            Test<long>();
            Test<ulong>();
            Test<float>();
            Test<double>();
            Test<decimal>();

            Test<char>();
            Test<bool>();
        }

        [TestMethod]
        public void Null_ThrowsException()
        {
            EzAssert.That(() => ((Type) null).GetDefault()).Throws<ArgumentNullException>();
        }
    }


}

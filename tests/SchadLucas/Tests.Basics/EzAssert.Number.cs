using System;

namespace SchadLucas.Tests.Basics
{
    public static partial class EzAssert
    {
        public static EzAssertNumber That(decimal actual) => new EzAssertNumber(actual);
        public static EzAssertNumber That(double actual) => new EzAssertNumber((decimal) actual);
        public static EzAssertNumber That(long actual) => new EzAssertNumber((decimal) actual);
        public static EzAssertNumber That(float actual) => new EzAssertNumber((decimal) actual);
        public static EzAssertNumber That(ulong actual) => new EzAssertNumber((decimal) actual);
        public static EzAssertNumber That(int actual) => new EzAssertNumber((decimal) actual);
        public static EzAssertNumber That(uint actual) => new EzAssertNumber((decimal) actual);
        public static EzAssertNumber That(byte actual) => new EzAssertNumber((decimal) actual);
        public static EzAssertNumber That(sbyte actual) => new EzAssertNumber((decimal) actual);
        public static EzAssertNumber That(short actual) => new EzAssertNumber((decimal) actual);
        public static EzAssertNumber That(ushort actual) => new EzAssertNumber((decimal) actual);

        public class EzAssertNumber
        {
            private readonly decimal _actual;

            public EzAssertNumber(double actual) : this((decimal) actual) { }
            public EzAssertNumber(decimal actual)
            {
                _actual = actual;
            }


            public void IsEqualTo(double expected) => IsEqualTo((decimal) expected);
            public void IsEqualTo(decimal expected, decimal tolerance = 0)
            {
                if (Math.Abs(_actual - expected) > tolerance)
                {
                    Failed(expected, _actual);
                }
            }

            public void IsGreaterOrEqualTo(double expected) => IsGreaterOrEqualTo((decimal) expected);
            public void IsGreaterOrEqualTo(decimal expected, decimal tolerance = 0)
            {
                if (_actual + tolerance < expected)
                {
                    Failed(expected, _actual);
                }
            }

            public void IsGreaterThan(double expected) => IsGreaterThan((decimal) expected);
            public void IsGreaterThan(decimal expected, decimal tolerance = 0)
            {
                if (_actual + tolerance <= expected)
                {
                    Failed(expected, _actual);
                }
            }

            public void IsLessOrEqualTo(double expected) => IsLessOrEqualTo((decimal) expected);
            public void IsLessOrEqualTo(decimal expected, decimal tolerance = 0)
            {
                if (_actual - tolerance > expected)
                {
                    Failed(expected, _actual);
                }
            }

            public void IsLessThan(double expected) => IsLessThan((decimal) expected);
            public void IsLessThan(decimal expected, decimal tolerance = 0)
            {
                if (_actual - tolerance >= expected)
                {
                    Failed(expected, _actual);
                }
            }
        }
    }
}
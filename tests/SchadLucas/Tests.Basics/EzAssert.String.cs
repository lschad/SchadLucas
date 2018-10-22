using System;

namespace SchadLucas.Tests.Basics
{
    public partial class EzAssert
    {
        public static EzAssertString That(string actual)
        {
            return new EzAssertString(actual);
        }

        public class EzAssertString
        {
            private readonly string _actual;

            public EzAssertString(string actual)
            {
                _actual = actual;
            }

            public void IsEmpty()
            {
                if (_actual != string.Empty)
                {
                    Failed(new object[] {null, string.Empty}, _actual);
                }
            }

            public void IsEqualTo(string expected)
            {
                if (!Equals(_actual, expected))
                {
                    Failed(expected, _actual);
                }
            }

            public void IsNull()
            {
                if (_actual != null)
                {
                    Failed(new object[] {null, string.Empty}, _actual);
                }
            }

            public void IsNullOrEmpty()
            {
                if (!string.IsNullOrEmpty(_actual))
                {
                    Failed(new object[] {null, string.Empty}, _actual);
                }
            }

            public void IsNullOrWhiteSpace()
            {
                if (!string.IsNullOrWhiteSpace(_actual))
                {
                    Failed(new object[] {null, string.Empty}, _actual);
                }
            }
        }
    }
}
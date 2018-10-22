using System.Collections.Generic;
using System.Linq;

namespace SchadLucas.Tests.Basics
{
    public static partial class EzAssert
    {
        public static EzAssertEnumerable<T> That<T>(IEnumerable<T> actual)
        {
            return new EzAssertEnumerable<T>(actual);
        }

        public class EzAssertEnumerable<TEnumerable>
        {
            private readonly IEnumerable<TEnumerable> _actual;

            public EzAssertEnumerable(IEnumerable<TEnumerable> actual)
            {
                _actual = actual;
            }

            #region Equal

            private static bool AreEqual(IEnumerable<TEnumerable> e1, IEnumerable<TEnumerable> e2)
            {
                var x = e1.ToList();
                var y = e2.ToList();

                var firstNotSecond = x.Except(y).ToList();
                var secondNotFirst = y.Except(x).ToList();

                return !firstNotSecond.Any() && !secondNotFirst.Any();
            }

            public void IsEqualTo(IEnumerable<TEnumerable> expected)
            {
                if (!AreEqual(expected, _actual))
                {
                    Failed(expected, _actual);
                }
            }

            public void IsNotEqualTo(IEnumerable<TEnumerable> expected)
            {
                if (AreEqual(expected, _actual))
                {
                    Failed(expected, _actual);
                }
            }

            #endregion

            #region Count

            public void IsEmpty()
            {
                if (_actual.Any())
                {
                    Failed("<empty enumeration>", _actual.ToList());
                }
            }

            public void IsNotEmpty()
            {
                if (!_actual.Any())
                {
                    Failed("<enumeration>", "<empty enumeration>");
                }
            }

            public void CountIs(long expected)
            {
                var actual = _actual.Count();

                if (actual != expected)
                {
                    Failed(expected, actual);
                }
            }

            public void IsGreaterThan(long expected)
            {
                var actual = _actual.Count();

                if (actual <= expected)
                {
                    Failed(expected, actual);
                }
            }

            public void IsLessThan(long expected)
            {
                var actual = _actual.Count();

                if (actual >= expected)
                {
                    Failed(expected, actual);
                }
            }

            public void IsLessOrEqualTo(long expected)
            {
                var actual = _actual.Count();

                if (actual > expected)
                {
                    Failed(expected, actual);
                }
            }

            public void IsGreaterOrEqualTo(long expected)
            {
                var actual = _actual.Count();

                if (actual < expected)
                {
                    Failed(expected, actual);
                }
            }

            #endregion
        }
    }
}
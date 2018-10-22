using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using SchadLucas.Utilities.Extensions.System;

namespace SchadLucas.Tests.Basics
{
    [SuppressMessage("ReSharper", "UnusedMethodReturnValue.Global")]
    public static partial class EzAssert
    {
        public static EzAssertObject That(object actual)
        {
            return new EzAssertObject(actual);
        }

        public class EzAssertAndObject
        {
            public EzAssertAndObject(EzAssertObject obj)
            {
                And = obj;
            }

            public EzAssertObject And { get; }
        }

        public class EzAssertObject
        {
            #region _Internal

            private readonly object _actual;
            private readonly EzAssertAndObject _and;

            public EzAssertObject(object obj)
            {
                _and = new EzAssertAndObject(this);
                _actual = obj;
            }

            private void Failed(object expected)
            {
                EzAssert.Failed(expected, _actual);
            }

            #endregion

            #region Attributes

            public EzAssertAndObject HasAttribute<T>() where T : Attribute
            {
                var attributes = _actual.GetType().GetCustomAttributes(false).Select(a => a.GetType()).ToList();

                if (!attributes.Contains(typeof(T)))
                {
                    Failed(typeof(T));
                }

                return _and;
            }

            public EzAssertAndObject HasAttributes<T1, T2>() where T1 : Attribute where T2 : Attribute
            {
                var attributes = _actual.GetType().GetCustomAttributes(false).Select(a => a.GetType()).ToList();

                if (false == (attributes.Contains(typeof(T1)) && attributes.Contains(typeof(T2))))
                {
                    Failed(new[] {typeof(T1), typeof(T2)});
                }

                return _and;
            }

            #endregion

            #region Equality

            public EzAssertAndObject IsEqualTo(object obj)
            {
                if (!Equals(_actual, obj))
                {
                    Failed(obj);
                }

                return _and;
            }

            /// <summary>
            ///     Shorthand for <see cref="IsEqualTo" />
            /// </summary>
            public EzAssertAndObject Returns(object obj) => IsEqualTo(obj);

            public EzAssertAndObject IsNotEqualTo(object obj)
            {
                if (Equals(_actual, obj))
                {
                    EzAssert.Failed(obj, _actual);
                }

                return _and;
            }

            #endregion

            #region Default

            public EzAssertAndObject IsDefault(Type type)
            {
                if (!Equals(_actual, type.GetDefault()))
                {
                    Failed(type.GetDefault());
                }

                return _and;
            }

            public EzAssertAndObject IsDefault<TType>()
            {
                var @default = typeof(TType).GetDefault();

                if (!Equals(_actual, @default))
                {
                    Failed(@default);
                }

                return _and;
            }

            public EzAssertAndObject IsNotDefault()
            {
                throw new NotImplementedException();
            }

            #endregion

            #region TypeOf

            public EzAssertAndObject IsTypeOf<T>()
            {
                if (_actual == null || _actual.GetType() != typeof(T))
                {
                    Failed(typeof(T));
                }

                return _and;
            }

            public EzAssertAndObject IsNotTypeOf<T>()
            {
                if (_actual.GetType() == typeof(T))
                {
                    Failed(typeof(T));
                }

                return _and;
            }

            #endregion

            #region Null

            public EzAssertAndObject IsNotNull()
            {
                if (_actual == null)
                {
                    Failed("not null");
                }

                return _and;
            }

            public EzAssertAndObject IsNull()
            {
                if (_actual != null)
                {
                    Failed(null);
                }

                return _and;
            }

            #endregion

            #region Boolean

            public EzAssertAndObject IsTrue()
            {
                IsBool(true);
                return _and;
            }

            public EzAssertAndObject IsFalse()
            {
                IsBool(false);
                return _and;
            }

            private void IsBool(bool b)
            {
                var result = false;

                if (_actual is bool bx)
                {
                    result = bx == b;
                }

                if (!result)
                {
                    Failed(false);
                }
            }

            #endregion

            #region Is...

            private static bool IsNumber(object value)
            {
                return value is sbyte
                    || value is byte
                    || value is short
                    || value is ushort
                    || value is int
                    || value is uint
                    || value is long
                    || value is ulong
                    || value is float
                    || value is double
                    || value is decimal;
            }

            public EzAssertAndObject IsNumber()
            {
                if (!IsNumber(_actual))
                {
                    EzAssert.Failed("number", _actual.GetType());
                }

                return _and;
            }

            public EzAssertAndObject IsString()
            {
                if (!(_actual is string))
                {
                    EzAssert.Failed(typeof(string), _actual.GetType());
                }

                return _and;
            }

            #endregion
        }
    }
}
using System;
using System.Linq;

namespace SchadLucas.Wpf.Converters.Math
{
    internal static class MathConverter
    {
        internal static decimal Convert(object[] values, Func<decimal, decimal, decimal> action)
        {
            if (values == null || values.Length < 2)
            {
                throw new ArgumentException(nameof(values));
            }

            return values.Select(CovnertToDecimal).Aggregate(action);
        }

        private static decimal CovnertToDecimal(object value)
        {
            if (decimal.TryParse(value?.ToString(), out var o))
            {
                return o;
            }

            throw new ArgumentException(nameof(value));
        }
    }
}
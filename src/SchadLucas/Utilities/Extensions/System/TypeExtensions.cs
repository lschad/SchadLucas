using System;
using System.Linq.Expressions;

namespace SchadLucas.Utilities.Extensions.System
{
    public static class TypeExtensions
    {

        /// <summary>
        ///     Gets the default value of this type.
        /// </summary>
        /// <remarks>https://stackoverflow.com/a/12733445/3450580</remarks>
        public static object GetDefault(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            // We want an Func<object> which returns the default.
            // Create that expression here.
            var e = Expression.Lambda<Func<object>>(
                // Have to convert to object.
                Expression.Convert(
                    // The default value, always get what the *code* tells us.
                    Expression.Default(type), typeof(object)
                )
            );

            // Compile and return the value.
            return e.Compile()();
        }
    }
}
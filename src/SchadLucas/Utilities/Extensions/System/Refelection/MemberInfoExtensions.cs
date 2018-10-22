using System;
using System.Linq;
using System.Reflection;

namespace SchadLucas.Utilities.Extensions.System.Refelection
{
    public static class MemberInfoExtensions
    {
        public static bool HasAttribute<TAttribute>(this MemberInfo info) where TAttribute : Attribute
        {
            var attributes = info.GetCustomAttributes(typeof(TAttribute));

            return attributes.Any();
        }
    }
}
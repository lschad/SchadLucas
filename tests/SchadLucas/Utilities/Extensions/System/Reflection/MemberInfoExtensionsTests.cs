using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchadLucas.Tests.Basics;
using SchadLucas.Utilities.Extensions.System.Refelection;

namespace SchadLucas.Utilities.Tests.Extensions.System.Reflection
{
    [AttributeUsage(AttributeTargets.Property)]
    internal sealed class MyAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Property)]
    internal sealed class MyAttribute2 : Attribute { }

    [TestClass]
    public class MemberInfoExtensionsTests : EzTest
    {
        [SuppressMessage("ReSharper", "All")]
        [MyAttribute]
        public object TestProperty { get; set; }


        [TestMethod]
        public void Test()
        {
            var info = GetType().GetProperty(nameof(TestProperty));

            EzAssert.That(info.HasAttribute<MyAttribute>()).IsTrue();
            EzAssert.That(info.HasAttribute<MyAttribute2>()).IsFalse();
        }
    }
}
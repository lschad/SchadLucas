using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SchadLucas.Tests.Basics
{
    [TestClass]
    [SuppressMessage("ReSharper", "VirtualMemberNeverOverridden.Global")]
    public abstract class EzTest
    {
        private static readonly Random Random = new Random();
        private static readonly string[] Alphabet = {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"};

        [UsedImplicitly(ImplicitUseKindFlags.Assign)]
        public TestContext TestContext { private get; set; }

        protected static char RandomChar()
        {
            return Convert.ToChar(RandomLetter());
        }

        protected static string RandomLetter()
        {
            return Alphabet[Random.Next(0, Alphabet.Length)];
        }

        protected static int RandomNumber(int min = 0, int max = int.MaxValue)
        {
            return Random.Next(min, max);
        }

        protected static int RandomNegativeNumber()
        {
            return Math.Abs(RandomNumber()) * -1;
        }

        protected static string RandomString()
        {
            var unixTimestamp = (long) DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
            var guid = Guid.NewGuid().ToString("N");
            var guid2 = Guid.NewGuid().ToString("N");

            return $"{guid}{unixTimestamp}{guid2}"; // this is not overkill at all.
        }

        protected static void Repeat(Action action, int times = 2)
        {
            for (var i = 0; i < times; i++)
            {
                action();
            }
        }

        protected static async Task RepeatAsync(Func<Task> func, int times = 2)
        {
            for (var i = 0; i < times; i++)
            {
                await func();
            }
        }

        [ClassCleanup]
        public void _ClassCleanup()
        {
            TestContext.WriteLine($"Cleanup Class \"{TestContext.FullyQualifiedTestClassName}\"");

            if (!HasAttribute<SkipClassInitializeAttribute>(GetMethod()))
            {
                ClassCleanup();
            }
        }

        [ClassInitialize]
        public void _ClassInitialize()
        {
            TestContext.WriteLine($"Initialize Class \"{TestContext.FullyQualifiedTestClassName}\"");

            if (!HasAttribute<SkipClassInitializeAttribute>(GetMethod()))
            {
                ClassInitialize();
            }
        }

        [TestCleanup]
        public void _TestCleanup()
        {
            TestContext.WriteLine($"Cleanup Test \"{TestContext.TestName}\"");

            if (!HasAttribute<SkipTestCleanupAttribute>(GetMethod()))
            {
                TestCleanup();
            }
        }

        [TestInitialize]
        public void _TestInitialize()
        {
            TestContext.WriteLine($"Initialize Test \"{TestContext.TestName}\"");

            if (!HasAttribute<SkipTestInitializeAttribute>(GetMethod()))
            {
                TestInitialize();
            }
        }

        protected virtual void ClassCleanup() { }

        protected virtual void ClassInitialize() { }

        protected virtual void TestCleanup() { }

        protected virtual void TestInitialize() { }

        private MethodInfo GetMethod()
        {
            var type = GetType();
            var method = type.GetMethod(TestContext.TestName);

            return method;
        }

        private bool HasAttribute<T>(MethodInfo method)
            where T : Attribute
        {
            var hasAttribute = method
                               .GetCustomAttributes(typeof(T), false)
                               .Any();

            return hasAttribute;
        }
    }
}
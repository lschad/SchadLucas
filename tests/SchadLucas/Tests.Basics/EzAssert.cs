using System.Runtime.CompilerServices;

namespace SchadLucas.Tests.Basics
{
    public static partial class EzAssert
    {
        public static void Fail(string message)
        {
            throw new EzAssertFailedException(message);
        }

        private static void Failed(object expected, object actual, [CallerMemberName] string caller = "")
        {
            var message = $"[{caller}] Failed. Expected: {expected}, Actual: {actual}";

            throw new EzAssertFailedException(message);
        }
    }
}
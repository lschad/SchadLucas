using System;

namespace SchadLucas.Tests.Basics
{
    [Serializable]
    public class EzAssertFailedException : Exception
    {
        public EzAssertFailedException(string message) : base(message) { }
    }
}
using System;

namespace SchadLucas.Tests.Basics
{
    public sealed class SkipClassInitializeAttribute : Attribute { }

    public sealed class SkipClassCleanupAttribute : Attribute { }

    public sealed class SkipTestInitializeAttribute : Attribute { }

    public sealed class SkipTestCleanupAttribute : Attribute { }
}
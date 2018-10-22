using System;

namespace SchadLucas.Tests.Basics
{
    public static partial class EzAssert
    {
        public static EzAssertAction That(Action actual)
        {
            return new EzAssertAction(actual);
        }

        public class EzAssertAction
        {
            private readonly Action _action;

            public EzAssertAction(Action action) => _action = action;

            #region Exception

            private static Exception Catched(Action action)
            {
                try
                {
                    action();
                }
                catch (Exception e)
                {
                    return e;
                }

                return null;
            }

            public void Throws<TException>() where TException : Exception
            {
                var ex = Catched(_action);
                if (ex != null && ex.GetType() != typeof(TException))
                {
                    Failed(typeof(TException), ex.GetType());
                }
            }

            public void DoesNotThrow<TException>() where TException : Exception
            {
                var ex = Catched(_action);
                if (ex != null && ex.GetType() == typeof(TException))
                {
                    Failed(typeof(TException), "<no exception>");
                }
            }

            #endregion
        }
    }
}
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace SchadLucas.Wpf.EzMvvm.Services
{
    [ExcludeFromCodeCoverage]
    internal sealed class Subscription
    {
        private const long TicksMultiplier = 1000 * TimeSpan.TicksPerMillisecond;
        private readonly long _throttleByTicks;
        private double? _lastHandleTimestamp;

        internal Subscription(Type type, Guid token, TimeSpan throttleBy, object handler)
        {
            Type = type;
            Token = token;
            Handler = handler;
            _throttleByTicks = throttleBy.Ticks;
        }

        internal Guid Token { get; }

        internal Type Type { get; }

        private object Handler { get; }

        internal void Handle<T>(T message)
        {
            if (!CanHandle())
            {
                return;
            }

            var handler = Handler as Action<T>;

            // ReSharper disable once PossibleNullReferenceException
            handler(message);
        }

        private bool CanHandle()
        {
            if (_throttleByTicks == 0)
            {
                return true;
            }

            if (_lastHandleTimestamp == null)
            {
                _lastHandleTimestamp = Stopwatch.GetTimestamp();
                return true;
            }

            var now = Stopwatch.GetTimestamp();
            var durationInTicks = (now - _lastHandleTimestamp) / Stopwatch.Frequency * TicksMultiplier;

            if (durationInTicks >= _throttleByTicks)
            {
                _lastHandleTimestamp = now;
                return true;
            }

            return false;
        }
    }
}
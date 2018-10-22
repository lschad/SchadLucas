using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;

namespace SchadLucas.Wpf.EzMvvm.Services
{
    /// <summary>
    ///     An implementation of the <i>Event Aggregator</i> pattern.
    /// </summary>
    /// <remarks>
    ///     notes: This was a static class once. I'm not sure if it's cool to just make it non-static tbh.<p />
    ///     source: https://github.com/NimaAra/Easy.MessageHub
    /// </remarks>
    [ExcludeFromCodeCoverage]
    internal class Subscriptions
    {
        private readonly List<Subscription> _allSubscriptions = new List<Subscription>();

        private int _localSubscriptionRevision;
        private Subscription[] _localSubscriptions;

        private int _subscriptionsChangeCounter;

        internal void Clear()
        {
            lock (_allSubscriptions)
            {
                _allSubscriptions.Clear();
                if (_localSubscriptions != null)
                {
                    Array.Clear(_localSubscriptions, 0, _localSubscriptions.Length);
                }

                _subscriptionsChangeCounter++;
            }
        }

        internal Subscription[] GetTheLatestSubscriptions()
        {
            if (_localSubscriptions == null)
            {
                _localSubscriptions = new Subscription[0];
            }

            var changeCounterLatestCopy = Interlocked.CompareExchange(ref _subscriptionsChangeCounter, 0, 0);
            if (_localSubscriptionRevision == changeCounterLatestCopy)
            {
                return _localSubscriptions;
            }

            Subscription[] latestSubscriptions;
            lock (_allSubscriptions)
            {
                latestSubscriptions = _allSubscriptions.ToArray();
            }

            _localSubscriptionRevision = changeCounterLatestCopy;
            _localSubscriptions = latestSubscriptions;
            return _localSubscriptions;
        }

        internal bool IsRegistered(Guid token)
        {
            lock (_allSubscriptions)
            {
                return _allSubscriptions.Any(s => s.Token == token);
            }
        }

        internal Guid Register<T>(TimeSpan throttleBy, Action<T> action)
        {
            var type = typeof(T);
            var key = Guid.NewGuid();
            var subscription = new Subscription(type, key, throttleBy, action);

            lock (_allSubscriptions)
            {
                _allSubscriptions.Add(subscription);
                _subscriptionsChangeCounter++;
            }

            return key;
        }

        internal void UnRegister(Guid token)
        {
            lock (_allSubscriptions)
            {
                var subscription = _allSubscriptions.Find(s => s.Token == token);
                if (subscription == null)
                {
                    return;
                }

                var removed = _allSubscriptions.Remove(subscription);
                if (!removed)
                {
                    return;
                }

                if (_localSubscriptions != null)
                {
                    var localIdx = Array.IndexOf(_localSubscriptions, subscription);
                    if (localIdx >= 0)
                    {
                        _localSubscriptions = RemoveAt(_localSubscriptions, localIdx);
                    }
                }

                _subscriptionsChangeCounter++;
            }
        }

        private T[] RemoveAt<T>(T[] source, int index)
        {
            var dest = new T[source.Length - 1];
            if (index > 0)
            {
                Array.Copy(source, 0, dest, 0, index);
            }

            if (index < source.Length - 1)
            {
                Array.Copy(source, index + 1, dest, index, source.Length - index - 1);
            }

            return dest;
        }
    }
}
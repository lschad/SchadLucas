using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace SchadLucas.Tests.Basics
{
    public static partial class EzAssert
    {
        public static Tracker TrackEvent(object trackedObject, string eventName)
        {
            var tracker = new Tracker(trackedObject, eventName);

            var eventInfo = trackedObject.GetType().GetEvent(eventName);
            eventInfo.AddEventHandler(trackedObject, GetDelegate(eventInfo, tracker));

            return tracker;
        }

        private static Delegate GetDelegate(EventInfo eventInfo, Tracker tracker)
        {
            var methodInfo = typeof(Tracker).GetMethod(nameof(Tracker.Trigger), BindingFlags.NonPublic | BindingFlags.Instance);
            var parameterTypes = eventInfo.EventHandlerType
                                          .GetMethod(nameof(EventHandler.Invoke))
                                          ?.GetParameters()
                                          .Select(o => o.ParameterType).ToArray();
            var genericAction = typeof(Tracker).GetMethod(nameof(Tracker.CreateAction2), BindingFlags.NonPublic | BindingFlags.Instance);

            if (genericAction != null && parameterTypes != null)
            {
                var createAction = genericAction.MakeGenericMethod(parameterTypes);
                var action = (Delegate) createAction.Invoke(tracker, new object[] {methodInfo, eventInfo.Name});
                return Delegate.CreateDelegate(eventInfo.EventHandlerType, action.Target, action.Method);
            }

            return null;
        }

        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        public sealed class Tracker
        {
            private readonly string _eventName;
            private readonly WeakReference _trackedObject;
            private Action _action;

            internal Tracker(object trackedObject, string eventName)
            {
                _eventName = eventName;
                _trackedObject = new WeakReference(trackedObject);
            }

            public object EventArgs { get; private set; }
            public string EventName { get; private set; }
            public object Sender { get; private set; }
            public int TimesTriggered { get; private set; }

            public void Verify() => Verify(1);

            [SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Global")]
            public void Verify(int times)
            {
                _action?.Invoke();

                if (TimesTriggered < times)
                {
                    throw new EzAssertFailedException($"{_trackedObject.Target.GetType()} did not raise {_eventName}.");
                }
            }

            public Tracker WithAction(Action action)
            {
                _action = action;
                return this;
            }

            internal Action<T1, T2> CreateAction2<T1, T2>(MethodInfo methodInfo, string eventName)
            {
                return (p1, p2) => { methodInfo.Invoke(this, new object[] {p1, p2, eventName}); };
            }

            internal void Trigger(object sender, object eventArgs, string eventName)
            {
                Sender = sender;
                EventArgs = eventArgs;
                EventName = eventName;

                TimesTriggered++;
            }
        }
    }
}
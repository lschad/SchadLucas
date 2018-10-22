using System;
using System.Diagnostics;

namespace SchadLucas.Wpf.EzMvvm.Services
{
    /// <summary>
    ///     An implementation of the <i>Event Aggregator</i> pattern.
    /// </summary>
    /// <remarks>https://github.com/NimaAra/Easy.MessageHub</remarks>
    public sealed class MessageHub : Service, IMessageHub
    {
        private Action<Type, object> _globalHandler;
        private Subscriptions Subscriptions {get;} = new Subscriptions();

        public event EventHandler<MessageHubErrorEventArgs> Error;

        public void ClearSubscriptions() => Subscriptions.Clear();

        public void Dispose()
        {
            _globalHandler = null;
            ClearSubscriptions();
        }

        public bool IsSubscribed(Guid token) => Subscriptions.IsRegistered(token);

        public void Publish<T>(T message)
        {
            var localSubscriptions = Subscriptions.GetTheLatestSubscriptions();

            var msgType = typeof(T);

            _globalHandler?.Invoke(msgType, message);

            // ReSharper disable once ForCanBeConvertedToForeach | Performance Critical
            for (var idx = 0; idx < localSubscriptions.Length; idx++)
            {
                var subscription = localSubscriptions[idx];

                if (!subscription.Type.IsAssignableFrom(msgType))
                {
                    continue;
                }

                try
                {
                    subscription.Handle(message);
                }
                catch (Exception e)
                {
                    var copy = Error;
                    copy?.Invoke(this, new MessageHubErrorEventArgs(e, subscription.Token));
                }
            }
        }

        public void Publish<TMessage>(object[] args = default)
        {
            var message = (TMessage) Activator.CreateInstance(typeof(TMessage), args);
            Publish(message);
        }

        public void RegisterGlobalHandler(Action<Type, object> onMessage)
        {
            EnsureNotNull(onMessage);
            _globalHandler = onMessage;
        }

        public Guid Subscribe<T>(Action<T> action) => Subscribe(action, TimeSpan.Zero);

        public Guid Subscribe<T>(Action<T> action, TimeSpan throttleBy)
        {
            EnsureNotNull(action);
            return Subscriptions.Register(throttleBy, action);
        }

        public void UnSubscribe(Guid token) => Subscriptions.UnRegister(token);

        [DebuggerStepThrough]
        private void EnsureNotNull(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
        }
    }
}
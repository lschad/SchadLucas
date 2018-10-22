using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nito.AsyncEx;
using SchadLucas.Tests.Basics;
using SchadLucas.Wpf.EzMvvm.Services;

namespace SchadLucas.Wpf.EzMvvm.Tests
{
    [TestClass]
    [TestCategory("Services")]
    [TestCategory("EzMvvm")]
    public class MessageHubTests : EzTest
    {
        [TestMethod]
        public void AllSubscriptions_Called_OnPublish()
        {
            var called = 0;

            // these should be called.
            _messageHub.Subscribe<object>(o => called++);
            _messageHub.Subscribe<object>(o => called++);
            _messageHub.Subscribe<object>(o => called++);

            // these should not be called.
            _messageHub.Subscribe<int>(o => called++);
            _messageHub.Subscribe<int>(o => called++);
            _messageHub.Subscribe<int>(o => called++);

            _messageHub.Publish<object>();

            EzAssert.That(called).IsEqualTo(3);
        }

        [TestMethod]
        public void ClearSubscriptions_ClearsAllSubs()
        {
            var timesCalled = 0;

            _messageHub.Subscribe<int>(i => timesCalled += 1);

            _messageHub.Publish(RandomNumber());
            EzAssert.That(timesCalled).IsEqualTo(1);

            _messageHub.Publish(RandomNumber());
            EzAssert.That(timesCalled).IsEqualTo(2);

            _messageHub.ClearSubscriptions();
            _messageHub.Publish(RandomNumber());
            EzAssert.That(timesCalled).IsEqualTo(2);
        }

        [TestMethod]
        public void Dispose_ClearsGlobalHandler()
        {
            var called = 0;

            _messageHub.RegisterGlobalHandler((s, e) => called++);

            _messageHub.Dispose();
            _messageHub.Publish<object>();

            EzAssert.That(called).IsEqualTo(0);
        }

        [TestMethod]
        public void Dispose_ClearsSubscriptions()
        {
            var called = 0;

            _messageHub.Subscribe<object>(o => called++);
            _messageHub.Subscribe<object>(o => called++);
            _messageHub.Subscribe<object>(o => called++);

            _messageHub.Dispose();
            _messageHub.Publish<object>();

            EzAssert.That(called).IsEqualTo(0);
        }

        [TestMethod]
        public void Error_GetsRaised_OnEror()
        {
            var called = false;

            _messageHub.Error += (s, e) => called = true;
            _messageHub.Subscribe<object>(_ => throw new Exception());
            _messageHub.Publish<object>();

            EzAssert.That(called).IsTrue();
        }

        [TestMethod]
        public void Error_PassedProperException_OnEror()
        {
            const string msg = "++++";
            const string innerMsg = "----";

            var sub = _messageHub.Subscribe<object>(_ => throw new Exception(msg, new Exception(innerMsg)));
            _messageHub.Error += (s, e) =>
            {
                EzAssert.That(e.Token).IsEqualTo(sub);
                EzAssert.That(e.Exception).IsNotNull();
                EzAssert.That(e.Exception.InnerException).IsNotNull();
                EzAssert.That(e.Exception).IsTypeOf<Exception>();
                EzAssert.That(e.Exception.Message).IsEqualTo(msg);
                EzAssert.That(e.Exception?.InnerException?.Message).IsEqualTo(innerMsg);
            };
            _messageHub.Publish<object>();
        }

        [TestMethod]
        public void GlobalHandler_GetsCalled_OnPublish()
        {
            var called = false;

            _messageHub.RegisterGlobalHandler((t, o) => called = true);
            _messageHub.Publish<object>();

            Assert.IsTrue(called);
        }

        [TestMethod]
        public void IsSubscribed_GetsFalse_OnClear()
        {
            var sub = _messageHub.Subscribe<object>(x => { });
            Assert.IsTrue(_messageHub.IsSubscribed(sub));

            _messageHub.ClearSubscriptions();
            Assert.IsFalse(_messageHub.IsSubscribed(sub));
        }

        [TestMethod]
        public void IsSubscribed_GetsFalse_OnUnsubscribe()
        {
            var sub = _messageHub.Subscribe<object>(x => { });
            Assert.IsTrue(_messageHub.IsSubscribed(sub));

            _messageHub.UnSubscribe(sub);
            Assert.IsFalse(_messageHub.IsSubscribed(sub));
        }

        [TestMethod]
        public void RegisterGlobalHandler_ThrowsException_OnNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _messageHub.RegisterGlobalHandler(null));
        }

        [TestMethod]
        public void Subscribe_ThrowsException_OnNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _messageHub.Subscribe<int>(null));
        }

        [TestMethod]
        public void Subscription_GetsExecuted_OnPublish()
        {
            var called = false;

            _messageHub.Subscribe<object>(m => called = true);
            _messageHub.Publish<object>();

            Assert.IsTrue(called);
        }

        [TestMethod]
        public void Subscription_GetsPassedCorrectMessage_OnPublish()
        {
            var message = new TestHubMessage("_-*#1234abcd");
            TestHubMessage received = null;

            _messageHub.Subscribe<TestHubMessage>(m => received = m);
            _messageHub.Publish(message);

            EzAssert.That(received).IsNotNull();
            EzAssert.That(message.TestValue).IsEqualTo(received.TestValue);
        }


        [TestMethod]
        public void ThreadSafety()
        {
            const int times = 237;
            var timesCalled = 0;

            _messageHub.Subscribe<object>(o => timesCalled++);

            AsyncContext.Run(async () =>
                await RepeatAsync(async () =>
                    await Task.Run(() =>
                        new Thread(() =>
                            _messageHub.Publish<object>()).Start()), times));

            EzAssert.That(timesCalled).IsEqualTo(times);
        }

        #region Setup

        private MessageHub _messageHub;

        private class TestHubMessage
        {
            public readonly string TestValue;

            public TestHubMessage(string testValue)
            {
                TestValue = testValue;
            }
        }

        protected override void TestInitialize()
        {
            base.TestInitialize();

            _messageHub = new MessageHub();
        }

        protected override void TestCleanup()
        {
            base.TestCleanup();

            _messageHub.Dispose();
            _messageHub = null;
        }

        #endregion
    }
}
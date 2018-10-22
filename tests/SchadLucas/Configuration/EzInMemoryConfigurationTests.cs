using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchadLucas.Tests.Basics;

namespace SchadLucas.Configuration.Tests
{
    [TestClass]
    [TestCategory("Configuration")]
    public class EzInMemoryConfigurationTests : EzTest
    {
        [TestMethod]
        public void Get_Throws_IfKeyDoesNotExist()
        {
            Assert.ThrowsException<ArgumentException>(() => _provider.Get("_KEY_THAT_DOES_NOT_EXIST_"));
        }

        [TestMethod]
        public void Has()
        {
            Assert.IsFalse(MemoryProvider.HasKey(_key));
            _provider.Set(_key, 123456);
            Assert.IsTrue(MemoryProvider.HasKey(_key));
        }

        [TestMethod]
        public void HasKey_IsFalse_AfterRemove()
        {
            _provider.Set(_key, 1234);
            Assert.IsTrue(MemoryProvider.HasKey(_key));
            MemoryProvider.Remove(_key);
            Assert.IsFalse(MemoryProvider.HasKey(_key));
        }

        [TestMethod]
        public void Remove_Throws_OnWrongKey()
        {
            Assert.ThrowsException<ArgumentException>(() => MemoryProvider.Remove("KEY_DOES_NOT_EXIST"));
        }

        [TestMethod]
        public void Set_ThenGet()
        {
            void TestSetThenGet(string k, object v)
            {
                _provider.Set(k, v);
                Assert.AreEqual(v, _provider.Get(k));
                Assert.AreEqual(v, _provider.Get<object>(k));
            }

            TestSetThenGet(_key, "123");
            TestSetThenGet(_key, 123);
            TestSetThenGet(_key, new object[0]);
            TestSetThenGet(_key, new object[0]);
            TestSetThenGet(_key, 123);
            TestSetThenGet(_key, "123");
            TestSetThenGet(_key, new Action(() => { }));
            TestSetThenGet(_key, 5m);
            TestSetThenGet(_key, 5d);
            TestSetThenGet(_key, 5f);
        }

        #region Setup

        private EzConfiguration _provider;
        private EzInMemoryConfiguration MemoryProvider => (EzInMemoryConfiguration) _provider;
        private string _key;

        protected override void TestInitialize()
        {
            base.TestInitialize();

            _provider = new EzInMemoryConfiguration();
            _key = Guid.NewGuid().ToString("N");
        }

        protected override void TestCleanup()
        {
            base.TestCleanup();

            _provider = null;
        }

        #endregion
    }
}
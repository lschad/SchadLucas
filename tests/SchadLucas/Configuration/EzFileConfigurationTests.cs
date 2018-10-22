using System;
using System.Collections;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchadLucas.Tests.Basics;

namespace SchadLucas.Configuration.Tests
{
    [TestClass]
    public class EzFileConfigurationTests : EzTest
    {
        private string _filePath;
        private string _key;
        private EzFileConfiguration _sut;

        [TestMethod]
        [SkipTestInitialize]
        public void CorruptedConfiguration_GetsOverwritten_WithDefault()
        {
            const string content = "###>>>>>THIS_IS_NOT_A_VALID_JSON_STRING<<<<<<###";

            _filePath = Path.Combine(Path.GetTempPath(), $"{RandomKey()}.tmp");
            File.WriteAllText(_filePath, content);

            Assert.IsTrue(File.Exists(_filePath));
            _sut = new EzFileConfiguration(_filePath);
            _sut.Get<int>("fake_value"); // config is lazy. 
            Assert.IsTrue(File.Exists(_filePath));
            var newContent = File.ReadAllText(_filePath);
            Assert.AreNotEqual(content, newContent);
        }

        [TestMethod]
        public void Get_ReturnsDefault_IfKeyDoesNotExist()
        {
            Assert.AreEqual(default, _sut.Get<int>(RandomKey()));
            Assert.AreEqual(default, _sut.Get<string>(RandomKey()));
            Assert.AreEqual(default, _sut.Get<object>(RandomKey()));
            Assert.AreEqual(default, _sut.Get<object[]>(RandomKey()));
            Assert.AreEqual(default, _sut.Get<Guid>(RandomKey()));
            Assert.AreEqual(default, _sut.Get<TestClassAttribute>(RandomKey()));
            Assert.AreEqual(default, _sut.Get<IEnumerable>(RandomKey()));
            Assert.AreEqual(default, _sut.Get<IList>(RandomKey()));
            Assert.AreEqual(default, _sut.Get<Type>(RandomKey()));
            Assert.AreEqual(default, _sut.Get(RandomKey()));
        }

        [TestMethod]
        public void Set_DoesNotThrow_IfKeyDoesNotExist()
        {
            var exceptionRaised = false;

            try
            {
                for (var i = 0; i < 99; i++)
                {
                    _sut.Set(RandomKey(), 0);
                }
            }
            catch (Exception)
            {
                exceptionRaised = true;
            }

            Assert.IsFalse(exceptionRaised);
        }

        [TestMethod]
        public void Set_SetsValueProperly()
        {
            for (var i = 0; i < 99; i++)
            {
                _sut.Set(_key, i);
                Assert.AreEqual(i, _sut.Get<int>(_key));
            }
        }

        #region TestSetup

        protected override void TestCleanup()
        {
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
                Assert.IsFalse(File.Exists(_filePath));
            }

            _filePath = string.Empty;
        }

        protected override void TestInitialize()
        {
            _filePath = Path.Combine(Path.GetTempPath(), $"{RandomKey()}.tmp");
            _key = RandomKey();
            _sut = new EzFileConfiguration(_filePath);
        }

        private static string RandomKey() => Guid.NewGuid().ToString();

        #endregion
    }
}
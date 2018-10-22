using System.Windows;
using System.Windows.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchadLucas.Tests.Basics;

namespace SchadLucas.Wpf.Utilities.Tests
{
    [TestClass]
    public class DependencyPropertyHelperTests : EzTest
    {
        [TestMethod]
        public void Callback_GetsCalled_OnPropertySet()
        {
            const int times = 20;

            EzAssert.That(_obj.TestObject2Changed).IsEqualTo(0);

            for (var i = 0; i < times; i++)
            {
                _obj.TestObject2 = RandomString();
            }

            EzAssert.That(_obj.TestObject2Changed).IsEqualTo(times);
        }

        [TestMethod]
        public void GetAndSet_AreWorking()
        {
            var random = new string[5];

            for (var i = 0; i < 5; i++)
            {
                random[i] = RandomString();
            }

            EzAssert.That(_obj.TestObject).IsDefault<DependencyProperty>();
            EzAssert.That(_obj.TestObject2).IsDefault<DependencyProperty>();
            EzAssert.That(_obj.TestObject3).IsDefault<DependencyProperty>();
            EzAssert.That(_obj.TestObject4).IsDefault<DependencyProperty>();
            EzAssert.That(_obj.TestObject5).IsDefault<DependencyProperty>();

            _obj.TestObject = random[0];
            _obj.TestObject2 = random[1];
            _obj.TestObject3 = random[2];
            _obj.TestObject4 = random[3];
            _obj.TestObject5 = random[4];

            Assert.AreEqual(random[0], _obj.TestObject);
            Assert.AreEqual(random[1], _obj.TestObject2);
            Assert.AreEqual(random[2], _obj.TestObject3);
            Assert.AreEqual(random[3], _obj.TestObject4);
            Assert.AreEqual(random[4], _obj.TestObject5);
        }

        [TestMethod]
        public void MetaDataOptions_GetPassed()
        {
            var metadata = (FrameworkPropertyMetadata) DpHelperObject.TestObject3Property.GetMetadata(_obj);

            EzAssert.That(metadata.Journal).IsTrue();
            EzAssert.That(metadata.IsNotDataBindable).IsTrue();
        }

        [TestMethod]
        public void ShortHandMethod_PassesDefaults()
        {
            var metadata = DpHelperObject.TestObject4Property.GetMetadata(_obj);

            EzAssert.That(metadata.CoerceValueCallback).IsEqualTo(DpHelperObject.TestObject4Property.DefaultMetadata.CoerceValueCallback);
            EzAssert.That(metadata.DefaultValue).IsEqualTo(DpHelperObject.TestObject4Property.DefaultMetadata.DefaultValue);

            _obj.TestObject4 = 4;
            EzAssert.That((int) _obj.TestObject4).IsEqualTo(4);
        }


        #region setup

        private class DpHelperObject : UserControl
        {
            private static readonly DependencyProperty TestObjectProperty = DependencyPropertyHelper.Register<DpHelperObject>(nameof(TestObject));
            private static readonly DependencyProperty TestObject2Property = DependencyPropertyHelper.Register<DpHelperObject>(nameof(TestObject2), OnTestObject2Called);
            public static readonly DependencyProperty TestObject3Property = DependencyPropertyHelper.Register<DpHelperObject>(nameof(TestObject3), FrameworkPropertyMetadataOptions.Journal | FrameworkPropertyMetadataOptions.NotDataBindable);
            public static readonly DependencyProperty TestObject4Property = DependencyPropertyHelper.Register<DpHelperObject>(nameof(TestObject4));


            private static readonly DependencyProperty TestObject5Property = DependencyPropertyHelper.Register<DpHelperObject>(nameof(TestObject5), Callback, FrameworkPropertyMetadataOptions.Journal | FrameworkPropertyMetadataOptions.NotDataBindable);

            public object TestObject
            {
                get => GetValue(TestObjectProperty);
                set => SetValue(TestObjectProperty, value);
            }

            public object TestObject2
            {
                get => GetValue(TestObject2Property);
                set => SetValue(TestObject2Property, value);
            }

            public int TestObject2Changed { get; set; }

            public object TestObject3
            {
                get => GetValue(TestObject3Property);
                set => SetValue(TestObject3Property, value);
            }

            public object TestObject4
            {
                get => GetValue(TestObject4Property);
                set => SetValue(TestObject4Property, value);
            }

            public object TestObject5
            {
                get => GetValue(TestObject5Property);
                set => SetValue(TestObject5Property, value);
            }

            private static void Callback(DependencyObject d, DependencyPropertyChangedEventArgs e) { }

            private static void OnTestObject2Called(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                if (d is DpHelperObject obj)
                {
                    obj.TestObject2Changed++;
                }
            }

            //public object TestObject {  get => GetValue(TestObjectProperty); set => SetValue(TestObjectProperty, value); }
        }


        private DpHelperObject _obj;

        protected override void TestInitialize()
        {
            base.TestInitialize();

            _obj = new DpHelperObject();
        }

        protected override void TestCleanup()
        {
            base.TestCleanup();

            _obj = null;
        }

        #endregion
    }
}
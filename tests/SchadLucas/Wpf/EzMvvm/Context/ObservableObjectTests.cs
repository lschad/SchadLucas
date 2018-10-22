using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchadLucas.Tests.Basics;
using SchadLucas.Wpf.EzMvvm.Context;

namespace SchadLucas.Wpf.EzMvvm.Tests.Context
{
    [TestClass]
    public class ObservableObjectTests : EzTest
    {
        [TestMethod]
        public void OnPropertyChanged_ByExpression_RaisesEvent()
        {
            EzAssert.TrackEvent(_testObject, nameof(_testObject.PropertyChanged))
                    .WithAction(() => _testObject.ForwardOnPropertyChanged(() => _testObject.TestProperty))
                    .Verify();
        }

        [TestMethod]
        public void OnPropertyChanged_TriggersMultipleTimes_WithIEnumerable()
        {
            MultipleTimes_WithIEnumerable(nameof(_testObject.PropertyChanged), list => _testObject.ForwardOnPropertyChanged(list));
        }

        [TestMethod]
        public void OnPropertyChanging_ByExpression_RaisesEvent()
        {
            EzAssert.TrackEvent(_testObject, nameof(_testObject.PropertyChanging))
                    .WithAction(() => _testObject.ForwardOnPropertyChanging(() => _testObject.TestProperty))
                    .Verify();
        }

        [TestMethod]
        public void OnPropertyChanging_TriggersMultipleTimes_WithIEnumerable()
        {
            MultipleTimes_WithIEnumerable(nameof(_testObject.PropertyChanging), list => _testObject.ForwardOnPropertyChanging(list));
        }

        [TestMethod]
        public void SetField()
        {
            EzAssert.That(_testObject.TestProperty).IsEqualTo(default);
            EzAssert.That(_testObject._testProperty).IsEqualTo(default);
            _testObject.TestProperty = _value;
            EzAssert.That(_testObject.TestProperty).IsEqualTo(_value);
            EzAssert.That(_testObject._testProperty).IsEqualTo(_value);
        }

        [TestMethod]
        public void SetField_RaisesPropertyChanged()
        {
            EzAssert.TrackEvent(_testObject, nameof(_testObject.PropertyChanged))
                    .WithAction(() => _testObject.TestProperty = _value)
                    .Verify();
        }

        [TestMethod]
        public void SetField_RaisesPropertyChanging()
        {
            EzAssert.TrackEvent(_testObject, nameof(_testObject.PropertyChanging))
                    .WithAction(() => _testObject.TestProperty = _value)
                    .Verify();
        }

        [TestMethod]
        public void SetSameValue_DoesNotRaise_OnPropertyChanged()
        {
            SetSameValue_DoesNotRaise_Event(nameof(_testObject.PropertyChanged));
        }

        [TestMethod]
        public void SetSameValue_DoesNotRaise_OnPropertyChanging()
        {
            SetSameValue_DoesNotRaise_Event(nameof(_testObject.PropertyChanging));
        }

        #region setup

        private void MultipleTimes_WithIEnumerable(string e, Action<IEnumerable<string>> action)
        {
            const int times = 99;

            EzAssert.TrackEvent(_testObject, e)
                    .WithAction(() =>
                    {
                        var list = Enumerable.Range(0, times)
                                             .Select(n => nameof(_testObject.TestProperty))
                                             .ToList();
                        action(list);
                    })
                    .Verify(times);
        }

        private void SetSameValue_DoesNotRaise_Event(string eventName)
        {
            _testObject.TestProperty = _value; // set once before tracking

            var tracker = EzAssert.TrackEvent(_testObject, eventName);
            for (var i = 0; i < 99; i++)
            {
                _testObject.TestProperty = _value;
            }

            EzAssert.That(tracker.TimesTriggered).IsEqualTo(0);
        }

        private class ObservableObjectTestObject : ObservableObject
        {
            // ReSharper disable once InconsistentNaming | for sake of testing
            public string _testProperty;

            public string TestProperty
            {
                get => _testProperty;
                set => SetField(ref _testProperty, value);
            }

            public void ForwardOnPropertyChanged(IEnumerable<string> names) => OnPropertyChanged(names);
            public void ForwardOnPropertyChanged<TProperty>(Expression<Func<TProperty>> e) => OnPropertyChanged(e);

            public void ForwardOnPropertyChanging(IEnumerable<string> names) => OnPropertyChanging(names);
            public void ForwardOnPropertyChanging<TProperty>(Expression<Func<TProperty>> e) => OnPropertyChanging(e);

        }

        private ObservableObjectTestObject _testObject;
        private string _value;

        protected override void TestInitialize()
        {
            base.TestInitialize();

            _testObject = new ObservableObjectTestObject();
            _value = Guid.NewGuid().ToString("N");
        }

        #endregion
    }
}
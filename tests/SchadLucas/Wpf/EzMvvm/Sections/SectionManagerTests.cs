using System;
using System.Collections.Generic;
using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SchadLucas.Tests.Basics;
using SchadLucas.Wpf.EzMvvm.Logging;
using SchadLucas.Wpf.EzMvvm.Sections;

namespace SchadLucas.Wpf.EzMvvm.Tests.Sections
{
    [TestClass]
    public class SectionManagerTests : EzTest
    {
        [TestMethod]
        public void Activate_UnAttachedView_Throws()
        {
            var view = new FakeView();

            _sectionManager.Register(_section);
            EzAssert.That(() => _sectionManager.Activate(_sectionName, view)).Throws<ArgumentException>();
        }

        [TestMethod]
        public void ActivatingView_ChangesSectionContent()
        {
            var view = new FakeView();

            EzAssert.That(_section.SectionContent).IsDefault<object>();

            _sectionManager.Register(_section);
            _sectionManager.Attach(_sectionName, view);
            _sectionManager.Activate(_sectionName, view);

            EzAssert.That(_section.SectionContent).IsEqualTo(view);
        }

        [TestMethod]
        public void ChangingView_RefreshsDataContext()
        {
            _sectionManager.Register(_section);

            var ctx = RandomString();
            var view1 = new FakeView(4);
            var view2 = new FakeView(8);
            _sectionManager.Attach(_sectionName, view1);
            _sectionManager.Attach(_sectionName, view2);

            EzAssert.That(view1.DataContext).IsDefault<object>();
            EzAssert.That(view2.DataContext).IsDefault<object>();
            _sectionManager.SetDataContext(_sectionName, ctx);
            EzAssert.That(_section.SectionContext).IsEqualTo(ctx);
            EzAssert.That(view1.DataContext).IsNotEqualTo(ctx);
            EzAssert.That(view2.DataContext).IsNotEqualTo(ctx);

            _sectionManager.Activate(_sectionName, view1);
            EzAssert.That(_section.SectionContext).IsEqualTo(ctx);
            EzAssert.That(view1.DataContext).IsEqualTo(ctx);
            EzAssert.That(view2.DataContext).IsNotEqualTo(ctx);

            _sectionManager.Activate(_sectionName, view2);
            EzAssert.That(_section.SectionContext).IsEqualTo(ctx);
            EzAssert.That(view1.DataContext).IsEqualTo(ctx);
            EzAssert.That(view2.DataContext).IsEqualTo(ctx);
        }

        [TestMethod]
        public void Detach_DetachesView()
        {
            _sectionManager.Register(_section);

            Detach();
        }

        [TestMethod]
        public void DetachAll_DetachesAllViews()
        {
            _sectionManager.Register(_section);
            Detach(25);
        }

        [TestMethod]
        public void GetSectionByName_Works()
        {
            _sectionManager.Register(_section);
            EzAssert.That(_sectionManager.GetSectionByName(_sectionName)).IsEqualTo(_section);
        }

        [TestMethod]
        public void Methods_RaiseException_WhenSectionIsNotRegistered()
        {
            EzAssert.That(() => _sectionManager.Activate(RandomString(), null)).Throws<ArgumentException>();
            EzAssert.That(() => _sectionManager.Attach(RandomString(), null)).Throws<ArgumentException>();
            EzAssert.That(() => _sectionManager.Detach(RandomString(), null)).Throws<ArgumentException>();
            EzAssert.That(() => _sectionManager.SetDataContext(RandomString(), null)).Throws<ArgumentException>();
            EzAssert.That(() => _sectionManager.DetachAll(RandomString())).Throws<ArgumentException>();
            EzAssert.That(() => _sectionManager.Hide(RandomString())).Throws<ArgumentException>();
            EzAssert.That(() => _sectionManager.Show(RandomString())).Throws<ArgumentException>();
        }

        [TestMethod]
        public void Register_DoesNotThrow_IfSectionIsAlreadyRegistered()
        {
            _sectionManager.Register(_section);

            try
            {
                _sectionManager.Register(_section);
            }
            catch (Exception)
            {
                EzAssert.Fail("Register threw an Exception.");
            }
        }

        [TestMethod]
        public void Register_RaisesException_WhenParameterIsNull()
        {
            EzAssert.That(() => _sectionManager.Register(null)).Throws<ArgumentNullException>();
        }

        [TestMethod]
        public void SetDataContext_SetsDataContext_Duh()
        {
            var ctx = RandomString();
            _sectionManager.Register(_section);

            EzAssert.That(_section.DataContext).IsDefault<object>();
            _sectionManager.SetDataContext(_sectionName, ctx);

            EzAssert.That(_section.SectionContext).IsEqualTo(ctx);
        }

        [TestMethod]
        public void SetVisible_ChangesVisibility()
        {
            _section.Visible = true;
            EzAssert.That(_section.Visibility).IsEqualTo(Visibility.Visible);
            _section.Visible = false;
            EzAssert.That(_section.Visibility).IsEqualTo(Visibility.Collapsed);
        }

        [TestMethod]
        public void ShowAndHide_Change_Visible()
        {
            _sectionManager.Register(_section);

            EzAssert.That(_section.Visible).IsTrue();
            _sectionManager.Hide(_sectionName);
            EzAssert.That(_section.Visible).IsFalse();
            _sectionManager.Show(_sectionName);
            EzAssert.That(_section.Visible).IsTrue();
        }

        #region setup
        
        private void Detach(int n = 1)
        {
            var views = new List<FakeView>();

            for (var i = 0; i < n; i++)
            {
                views.Add(new FakeView(n + 1));
            }

            views.ForEach(v => _sectionManager.Attach(_sectionName, v));
            views.ForEach(v => EzAssert.That(() => _sectionManager.Activate(_sectionName, v)).DoesNotThrow<ArgumentException>());

            if (n == 1)
            {
                _sectionManager.Detach(_sectionName, views[0]);
            }
            else
            {
                _sectionManager.DetachAll(_sectionName);
            }

            views.ForEach(v => EzAssert.That(() => _sectionManager.Activate(_sectionName, v)).Throws<ArgumentException>());
        }


        protected override void TestInitialize()
        {
            base.TestInitialize();

            var loggerMock = new Mock<IEzLogger>();

            _sectionName = $"Region_{RandomString()}";
            _section = new Section {SectionName = _sectionName};
            _sectionManager = new SectionManager(loggerMock.Object);
        }

        private SectionManager _sectionManager;
        private Section _section;
        private string _sectionName;

        #endregion
    }
}
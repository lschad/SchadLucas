using System;
using System.Collections.Generic;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SchadLucas.Tests.Basics;
using SchadLucas.Wpf.EzMvvm.Logging;
using SchadLucas.Wpf.EzMvvm.Sections;

namespace SchadLucas.Wpf.EzMvvm.Tests.Sections
{
    [TestClass]
    public class SectionBuilderTests : EzTest
    {
        [TestMethod]
        public void Build_BuildsProperSection()
        {
            SectionBase fakeSection = new FakeSection();
            _sectionManager.Register(new Section {SectionName = fakeSection.GetSectionName()});
            var section = _sectionBuilder.Build<FakeSection>();

            EzAssert.That(fakeSection.GetDataContext()).IsEqualTo(section.GetDataContext());
            EzAssert.That(fakeSection.GetActiveView()).IsEqualTo(section.GetActiveView());
            EzAssert.That(fakeSection.GetSectionName()).IsEqualTo(section.GetSectionName());
            EzAssert.That(fakeSection.GetViews()).IsEqualTo(section.GetViews());
        }

        [TestMethod]
        public void Test()
        {
            _sectionManager.Register(new Section {SectionName = FakeSection.SectionName});
            SectionBase section = _sectionBuilder.Build<FakeSection>();

            EzAssert.That(FakeSection.Views).IsEqualTo(section.GetViews());
            EzAssert.That(FakeSection.ActiveView).IsEqualTo(section.GetActiveView());
            EzAssert.That(FakeSection.DataContext).IsEqualTo(section.GetDataContext());

            var fs = new EmptyFakeSection();
            EzAssert.That(fs.GetViews()).CountIs(0);
            EzAssert.That(fs.GetActiveView()).IsNull();
            EzAssert.That(fs.GetDataContext()).IsNull();
        }

        #region setup

        protected override void TestInitialize()
        {
            base.TestInitialize();

            var loggerMock = new Mock<IEzLogger>();
            _sectionManager = new SectionManager(loggerMock.Object);

            var builder = new ContainerBuilder();
            builder.RegisterType<FakeSection>().AsSelf().SingleInstance();
            builder.RegisterType<FakeView>().As<IFakeView>().SingleInstance();
            builder.RegisterType<FakeViewModel>().As<IFakeViewModel>().SingleInstance();
            _container = builder.Build();

            _sectionBuilder = new SectionBuilder(_container, _sectionManager);
        }

        private SectionBuilder _sectionBuilder;
        private SectionManager _sectionManager;
        private IContainer _container;

        private class FakeSection : SectionBase
        {
            internal static readonly Type ActiveView = typeof(IFakeView);
            internal static readonly Type DataContext = typeof(IFakeViewModel);
            internal static readonly IEnumerable<Type> Views = new[] {typeof(IFakeView)};
            internal const string SectionName = "THIS_IS_MY_SECTION";

            public override Type GetActiveView() => ActiveView;
            public override Type GetDataContext() => DataContext;
            public override string GetSectionName() => SectionName;
            public override IEnumerable<Type> GetViews() => Views;
        }

        private class EmptyFakeSection : SectionBase
        {
            public override string GetSectionName() => string.Empty;
        }

        #endregion
    }
}
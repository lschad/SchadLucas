using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SchadLucas.Tests.Basics;
using SchadLucas.Wpf.EzMvvm.Context;

namespace SchadLucas.Wpf.EzMvvm.Tests
{
    [TestClass]
    [TestCategory("EzMvvm")]
    public class ViewModelBinderTests : EzTest
    {
        [TestMethod]
        public void BindView_ReturnsTrue_IfDataContextIsValid()
        {
            var viewMock = new Mock<IView>();
            const int context = 1234;

            Assert.IsTrue(ViewModelBinder.BindView(context, viewMock.Object));

            viewMock = new Mock<IView>();
            var viewModelMock = new Mock<IViewModel>();
            Assert.IsTrue(ViewModelBinder.BindView(viewModelMock.Object, viewMock.Object));
        }

        [TestMethod]
        public void BindView_SetsDataContext()
        {
            const int context = 1234;
            var viewMock = new Mock<IView>();
            ViewModelBinder.BindView(1234, viewMock.Object);

            viewMock.VerifySet(x => x.DataContext = context);

            viewMock = new Mock<IView>();
            var viewModelMock = new Mock<IViewModel>();
            ViewModelBinder.BindView(viewModelMock.Object, viewMock.Object);

            viewMock.VerifySet(x => x.DataContext = viewModelMock.Object);
        }

        [TestMethod]
        public void BindView_ThrowsException_WhenViewIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() => ViewModelBinder.BindView(1234, null));
            Assert.ThrowsException<ArgumentNullException>(() => ViewModelBinder.BindView(new Mock<IViewModel>().Object, null));
        }
    }
}
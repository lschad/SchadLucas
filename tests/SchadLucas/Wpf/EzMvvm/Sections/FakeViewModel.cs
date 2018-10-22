using SchadLucas.Wpf.EzMvvm.Context;

namespace SchadLucas.Wpf.EzMvvm.Tests.Sections
{
    internal interface IFakeViewModel : IViewModel { }

    internal class FakeViewModel : ViewModel, IFakeViewModel { }
}
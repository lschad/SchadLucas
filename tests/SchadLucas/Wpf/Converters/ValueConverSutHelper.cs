using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Data;

namespace SchadLucas.Wpf.Converters.Tests
{
    [ExcludeFromCodeCoverage]
    internal class MultiValueConverSutHelper
    {
        internal MultiValueConverSutHelper(Func<IMultiValueConverter> getSut)
        {
            GetSut = getSut;
        }

        private Func<IMultiValueConverter> GetSut { get; }
        private IMultiValueConverter Sut => GetSut();

        internal object Convert(object[] o, object p = default) => Sut.Convert(o, default, p, default);

        internal T Convert<T>(object[] o, object p = default) => (T) Sut.Convert(o, default, p, default);

        internal object[] ConvertBack(object o, object p = default) => Sut.ConvertBack(o, default, p, default);

        internal T[] ConvertBack<T>(object o, object p = default) => Array.ConvertAll(Sut.ConvertBack(o, default, p, default), i => (T) i);
    }

    [ExcludeFromCodeCoverage]
    internal class ValueConverSutHelper
    {
        internal ValueConverSutHelper(Func<IValueConverter> getSut)
        {
            GetSut = getSut;
        }

        private Func<IValueConverter> GetSut { get; }
        private IValueConverter Sut => GetSut();

        internal object Convert(object o, object p = default) => Sut.Convert(o, default, p, default);

        internal T Convert<T>(object o, object p = default) => (T) Sut.Convert(o, default, p, default);

        internal object ConvertBack(object o, object p = default) => Sut.ConvertBack(o, default, p, default);

        internal T ConvertBack<T>(object o, object p = default) => (T) Sut.ConvertBack(o, default, p, default);
    }
}
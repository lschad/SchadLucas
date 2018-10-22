using SchadLucas.Wpf.EzMvvm.Services;

namespace SchadLucas.Wpf.EzMvvm.Sections
{
    public interface ISectionBuilder : IService
    {
        T Build<T>() where T : SectionBase;
    }
}
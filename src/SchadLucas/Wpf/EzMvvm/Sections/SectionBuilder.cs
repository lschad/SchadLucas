using Autofac;
using SchadLucas.Wpf.EzMvvm.Services;

namespace SchadLucas.Wpf.EzMvvm.Sections
{
    internal class SectionBuilder : Service, ISectionBuilder
    {
        private readonly ILifetimeScope _container;
        private readonly ISectionManager _sectionManager;

        public SectionBuilder(ILifetimeScope container, ISectionManager sectionManager)
        {
            _container = container;
            _sectionManager = sectionManager;
        }

        public T Build<T>() where T : SectionBase
        {
            var section = _container.Resolve<T>();
            var views = section.GetViews();
            var activeView = section.GetActiveView();
            var dataContext = section.GetDataContext();
            var name = section.GetSectionName();

            foreach (var view in views)
            {
                _sectionManager.Attach(name, _container.Resolve(view));
            }

            if (dataContext != null)
            {
                _sectionManager.SetDataContext(name, _container.Resolve(dataContext));
            }

            if (activeView != null)
            {
                _sectionManager.Activate(name, _container.Resolve(activeView));
            }

            return section;
        }
    }
}
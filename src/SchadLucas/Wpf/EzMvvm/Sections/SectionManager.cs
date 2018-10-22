using System;
using System.Collections.Generic;
using System.Linq;
using SchadLucas.Wpf.EzMvvm.Logging;
using SchadLucas.Wpf.EzMvvm.Services;

namespace SchadLucas.Wpf.EzMvvm.Sections
{
    public class SectionManager : Service, ISectionManager
    {
        private readonly IEzLogger _logger;

        public SectionManager(IEzLogger logger)
        {
            _logger = logger;
        }

        private IList<ISection> Sections { get; } = new List<ISection>();
        private Dictionary<string, List<object>> Views { get; } = new Dictionary<string, List<object>>();

        public void Activate(string sectionName, object view)
        {
            ThrowIfSectionDoesNotExist(sectionName);

            if (!Views.ContainsKey(sectionName) || !Views[sectionName].Contains(view))
            {
                throw new ArgumentException(nameof(view));
            }

            _logger.Debug($"Activate view in Section '{sectionName}'. (View: {view})");
            Sections.First(r => r.SectionName == sectionName).SectionContent = view;
        }

        public void Attach(string sectionName, object view)
        {
            ThrowIfSectionDoesNotExist(sectionName);

            if (!Views.ContainsKey(sectionName))
            {
                Views.Add(sectionName, new List<object>());
            }

            _logger.Debug($"Attach view to Section '{sectionName}'. (View: {view})");
            Views[sectionName].Add(view);
        }

        public void Detach(string sectionName, object view)
        {
            ThrowIfSectionDoesNotExist(sectionName);

            _logger.Debug($"Detach view from Section '{sectionName}'. (View: {view})");
            Views[sectionName].Remove(view);
        }

        public void DetachAll(string sectionName)
        {
            ThrowIfSectionDoesNotExist(sectionName);

            _logger.Debug($"Detach all {Views[sectionName].Count} views from Section '{sectionName}'.");
            Views[sectionName].Clear();
        }

        public void Hide(string sectionName)
        {
            ThrowIfSectionDoesNotExist(sectionName);

            var section = GetSectionByName(sectionName);
            section.Visible = false;
        }

        public void SetDataContext(string sectionName, object dataContext)
        {
            ThrowIfSectionDoesNotExist(sectionName);

            _logger.Debug($"Set DataContext for Section '{sectionName}'. (DataContext: {dataContext})");
            var section = Sections.First(r => r.SectionName == sectionName);
            section.SectionContext = dataContext;
        }

        public void Show(string sectionName)
        {
            ThrowIfSectionDoesNotExist(sectionName);

            var section = GetSectionByName(sectionName);
            section.Visible = true;
        }

        internal ISection GetSectionByName(string sectionName)
        {
            return Sections.First(r => r.SectionName == sectionName);
        }

        internal void Register(ISection section)
        {
            if (section == null)
            {
                throw new ArgumentNullException(nameof(section));
            }

            _logger.Debug($"Register Section {section}");

            if (!SectionExists(section.SectionName))
            {
                Sections.Add(section);
            }
        }

        private bool SectionExists(string sectionName)
        {
            return Sections.Any(s => s.SectionName == sectionName);
        }

        private void ThrowIfSectionDoesNotExist(string sectionName)
        {
            if (!SectionExists(sectionName))
            {
                throw new ArgumentException($"Section \"{sectionName}\" does not exist", nameof(sectionName));
            }
        }
    }
}
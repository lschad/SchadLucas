using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace SchadLucas.Wpf.EzMvvm.Sections
{
    public abstract class SectionBase
    {
        [CanBeNull]
        public virtual Type GetActiveView()
        {
            return null;
        }

        [CanBeNull]
        public virtual Type GetDataContext()
        {
            return null;
        }

        public virtual IEnumerable<Type> GetViews()
        {
            return Enumerable.Empty<Type>();
        }

        public abstract string GetSectionName();
    }
}
using System;
using SchadLucas.Wpf.EzMvvm.Services;

namespace SchadLucas.Wpf.EzMvvm.Sections
{
    public interface ISectionManager : IService
    {
        /// <summary>
        ///     Activates the passed <paramref name="view" />.
        /// </summary>
        /// <remarks>
        ///     The <paramref name="view" /> has to be <see cref="Attach">attached</see> to be <see cref="Activate">activated</see>
        ///     .
        /// </remarks>
        /// <param name="sectionName">The section which the view should be associated with.</param>
        /// <param name="view">The view which should be activated.</param>
        /// <exception cref="System.ArgumentException">
        ///     If the <paramref name="view" /> is not <see cref="Attach">attached</see> to the section.
        /// </exception>
        void Activate(string sectionName, object view);

        /// <summary>
        ///     Attaches a view to a section.
        /// </summary>
        /// <remarks>
        ///     A view can be associated with multiple sections. Although it's intended to use the
        ///     same instance with multiple sections.
        ///     <para />
        ///     A view does not automatically get <see cref="Activate">activated</see> if you attach
        ///     it to a section.
        /// </remarks>
        /// <param name="sectionName">The section which the view should be associated with.</param>
        /// <param name="view">The view which should be attached.</param>
        void Attach(string sectionName, object view);

        /// <summary>
        ///     Detaches a view from a section.
        /// </summary>
        /// <param name="sectionName">The section which the view is associated with.</param>
        /// <param name="view">The view which should be detached.</param>
        void Detach(string sectionName, object view);

        /// <summary>
        ///     Detaches all views from a section.
        /// </summary>
        /// <param name="sectionName"></param>
        void DetachAll(string sectionName);

        /// <summary>
        ///     Hides the section.
        /// </summary>
        /// <remarks>
        ///     The visible space used by the section will be cleared. It won't just be <i>transparent</i>.
        /// </remarks>
        /// <param name="sectionName">The name of the section which will be hidden.</param>
        void Hide(string sectionName);

        /// <summary>
        ///     Sets the DataContext of a section.
        /// </summary>
        /// <param name="sectionName">The section whose context should be set.</param>
        /// <param name="dataContext">The context which should be set.</param>
        void SetDataContext(string sectionName, object dataContext);

        /// <summary>
        ///     Shows the section.
        /// </summary>
        /// <param name="sectionName">The name of the section which will be shown.</param>
        void Show(string sectionName);
    }
}
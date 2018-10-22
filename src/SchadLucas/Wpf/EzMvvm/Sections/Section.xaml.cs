using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using System.Windows;
using JetBrains.Annotations;
using SchadLucas.Wpf.EzMvvm.Context;
using SchadLucas.Wpf.EzMvvm.Logging;
using DependencyPropertyHelper = SchadLucas.Wpf.Utilities.DependencyPropertyHelper;

namespace SchadLucas.Wpf.EzMvvm.Sections
{
    public partial class Section : ISection
    {
        public static readonly DependencyProperty SectionContentProperty = DependencyPropertyHelper.Register<Section, object>(nameof(SectionContent), SectionContentOrContextChanged);
        public static readonly DependencyProperty SectionContextProperty = DependencyPropertyHelper.Register<Section, object>(nameof(SectionContext), SectionContentOrContextChanged);
        public static readonly DependencyProperty SectionNameProperty = DependencyPropertyHelper.Register<Section, string>(nameof(SectionName), SectionNameChangedCallback);
        public static readonly DependencyProperty VisibleProperty = DependencyPropertyHelper.Register<Section, bool>(nameof(Visible), VisibleChangedCallback);

        public Section()
        {
            InitializeComponent();

            Visible = true;
        }

        [UsedImplicitly(ImplicitUseKindFlags.Access | ImplicitUseKindFlags.Assign)]
        [ExcludeFromCodeCoverage]
        public IEzLogger Logger { private get; set; }

        public object SectionContent
        {
            get => GetValue(SectionContentProperty);
            set => SetValueAndLog(SectionContentProperty, value);
        }

        public object SectionContext
        {
            get => GetValue(SectionContextProperty);
            set => SetValueAndLog(SectionContextProperty, value);
        }

        public string SectionName
        {
            get => (string) GetValue(SectionNameProperty);
            set => SetValueAndLog(SectionNameProperty, value);
        }

        public bool Visible
        {
            get => (bool) GetValue(VisibleProperty);
            set => SetValueAndLog(VisibleProperty, value);
        }

        public override string ToString()
        {
            return string.IsNullOrEmpty(SectionName) ? base.ToString() : SectionName;
        }

        private static void SectionContentOrContextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d.GetValue(SectionContentProperty) is IView view)
            {
                var dataContext = d.GetValue(SectionContextProperty);
                ViewModelBinder.BindView(dataContext, view);
            }
        }

        private static void SectionNameChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Section section)
            {
                var normalized = Regex.Replace(section.SectionName, "[^A-Za-z0-9_]", "_");
                if (!string.IsNullOrWhiteSpace(normalized))
                {
                    section.Name = normalized;
                }
            }
        }

        private static void VisibleChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Section section)
            {
                section.Visibility = section.Visible ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private void SetValueAndLog(DependencyProperty dp, object value)
        {
            Logger?.Debug($"Set {dp} = {value}");
            SetValue(dp, value);
        }
    }
}
using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using Autofac;
using Autofac.Core;
using Autofac.Core.Registration;
using NLog;
using SchadLucas.Wpf.EzMvvm.Context;
using SchadLucas.Wpf.EzMvvm.Logging;
using SchadLucas.Wpf.EzMvvm.Sections;
using SchadLucas.Wpf.EzMvvm.Services;
using SchadLucas.Wpf.Utilities;

namespace SchadLucas.Wpf.EzMvvm.Core
{
    [SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "But 3rd parties might derive from it.")]
    [SuppressMessage("ReSharper", "UnusedParameter.Global", Justification = "But deriving classes from 3rd parties might use it.")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "But deriving classes from 3rd parties might use it.")]
    public abstract class EzBootstrapper
    {
        protected Application App { get; private set; }
        protected IContainer Container { get; private set; }
        protected SectionManager SectionManager { get; private set; }
        protected Window Window { get; private set; }
        private static ILogger Logger => LogManager.GetLogger(nameof(EzBootstrapper));

        protected virtual IContainer BuildIocContainer()
        {
            var builder = GetContainerBuilder();

            OnRegistering();
            RegisterModules(builder);
            OnRegistered();

            return builder.Build();
        }

        protected virtual void BuildSections(ISectionBuilder sectionBuilder) { }

        protected virtual void ConfigureServices()
        {
            SectionManager = (SectionManager)Container.Resolve<ISectionManager>();
        }

        protected virtual ContainerBuilder GetContainerBuilder()
        {
            var builder = new ContainerBuilder();
            return builder;
        }

        protected abstract IViewModel GetRootDataContext();

        protected abstract Window GetRootWindow();

        protected virtual void Initialize()
        {
            LogManager.Configuration = new AppLoggingConfiguration();

            OnInitializing();
            InitializeApp();
            OnInitialized();
        }

        protected virtual void LoadWindow()
        {
            Logger.Debug("Try to load Window..");

            try
            {
                Logger.Debug("Resolve RootWindow");
                Window = GetRootWindow();
                Window.Loaded += OnWindowLoaded;

                Logger.Debug("Resolve RootDataContext");
                var viewModel = GetRootDataContext();

                ViewModelBinder.BindView(viewModel, (IView) Window);
                App.MainWindow = Window;
                App.MainWindow.Show();
                App.MainWindow.Activate();
            }
            catch (Exception exception) when (exception is ComponentNotRegisteredException || exception is DependencyResolutionException)
            {
                Logger.Fatal(exception, "Application Startup crashed!");
                OnApplicationCrashed(exception);
                App.Shutdown(-1);
            }
        }

        protected virtual void OnApplicationCrashed(Exception exception) { }

        protected virtual void OnBuilding() { }

        protected virtual void OnBuilt()
        {
            ConfigureServices();
        }

        protected virtual void OnExit(object sender, ExitEventArgs e)
        {
            Container.Dispose();
        }

        protected virtual void OnInitialized() { }

        protected virtual void OnInitializing() { }

        protected virtual void OnRegistered() { }

        protected virtual void OnRegistering() { }

        protected virtual void OnStartup(object sender, StartupEventArgs e)
        {
            Logger.Debug("Startup");

            OnBuilding();
            Container = BuildIocContainer();
            OnBuilt();

            LoadWindow();
        }

        protected virtual void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            Window.Loaded -= OnWindowLoaded;

            var regionBuilder = Container.Resolve<ISectionBuilder>();

            RegisterRegions(Window);
            BuildSections(regionBuilder);
        }

        protected virtual void RegisterModules(ContainerBuilder builder)
        {
            builder.RegisterModule<EzMvvmModule>();
        }

        private void InitializeApp()
        {
            Logger.Debug("Initialize App");

            App = Application.Current;

            App.Startup += OnStartup;
            App.Exit += OnExit;

            App.ShutdownMode = ShutdownMode.OnMainWindowClose;
            App.MainWindow = new Window(); // this succs. it's because of the LoadingScreen.I don't get it
        }

        private void RegisterRegions(DependencyObject root)
        {
            var sections = DependencyObjectHelper.GetChildren<Section>(root);

            sections.ForEach(SectionManager.Register);
        }
    }
}
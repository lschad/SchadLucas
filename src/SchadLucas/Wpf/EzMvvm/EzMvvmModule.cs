using Autofac;
using SchadLucas.Wpf.EzMvvm.Context;
using SchadLucas.Wpf.EzMvvm.Logging;
using SchadLucas.Wpf.EzMvvm.Sections;
using SchadLucas.Wpf.EzMvvm.Services;

namespace SchadLucas.Wpf.EzMvvm
{
    internal class EzMvvmModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterModule<EzNLogModule>();
            builder.RegisterType<Section>();
        
            RegisterTypeFromAssemblies<SectionBase>(builder);
            RegisterTypeFromAssemblies<IView>(builder);
            RegisterTypeFromAssemblies<IViewModel>(builder);
            //RegisterFromAssemblies<IModel>(builder);
            RegisterTypeFromAssemblies<IService>(builder);
        }

        private static void RegisterTypeFromAssemblies<T>(ContainerBuilder builder) => builder
                                                                                       .RegisterAssemblyTypes(AssemblyCache.Assemblies)
                                                                                       .AssignableTo<T>()
                                                                                       .AsImplementedInterfaces()
                                                                                       .AsSelf()
                                                                                       .SingleInstance();
    }
}
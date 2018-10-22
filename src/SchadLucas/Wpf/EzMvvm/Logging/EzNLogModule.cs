using System.Linq;
using System.Reflection;
using Autofac.Core;
using NLog;
using Module = Autofac.Module;

namespace SchadLucas.Wpf.EzMvvm.Logging
{
    public class EzNLogModule : Module
    {
        protected override void AttachToComponentRegistration(IComponentRegistry componentRegistry, IComponentRegistration registration)
        {
            registration.Preparing += OnComponentPreparing;
            registration.Activated += (sender, e) => InjectLoggerProperties(e.Instance);
        }

        private static void InjectLoggerProperties(object instance)
        {
            var type = instance.GetType();
            var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var loggerProperties = properties.Where(p => IsIEzLogger(p) && p.CanWrite && IsNoIndex(p)).ToList();

            loggerProperties.ForEach(p => p.SetValue(instance, new EzLogger(LogManager.GetLogger(type.Name)), null));
        }

        private static bool IsIEzLogger(PropertyInfo p) => p.PropertyType == typeof(IEzLogger);
        private static bool IsNoIndex(PropertyInfo p) => p.GetIndexParameters().Length == 0;

        private static void OnComponentPreparing(object sender, PreparingEventArgs e)
        {
            var t = e.Component.Activator.LimitType;
            e.Parameters = e.Parameters.Union(new[]
            {
                new ResolvedParameter((p, i) => p.ParameterType == typeof(IEzLogger), (p, i) => new EzLogger(LogManager.GetLogger(t.Name)))
            });
        }
    }
}
using System.Diagnostics.CodeAnalysis;
using System.Windows;

namespace SchadLucas.Wpf.Utilities
{
    public static class DependencyPropertyHelper
    {
        public static DependencyProperty Register<TParent>(string name)
        {
            return Register<TParent, object>(name);
        }

        public static DependencyProperty Register<TParent>(string name, PropertyChangedCallback callback)
        {
            return Register<TParent, object>(name, callback);
        }

        public static DependencyProperty Register<TParent>(string name, FrameworkPropertyMetadataOptions flags)
        {
            return Register<TParent, object>(name, flags);
        }

        public static DependencyProperty Register<TParent>(string name, PropertyChangedCallback callback, FrameworkPropertyMetadataOptions flags)
        {
            return Register<TParent, object>(name, callback, flags);
        }

        public static DependencyProperty Register<TParent, TValue>(string name)
        {
            return Register<TParent, TValue>(name, delegate { });
        }

        public static DependencyProperty Register<TParent, TValue>(string name, PropertyChangedCallback callback)
        {
            return Register<TParent, TValue>(name, callback, FrameworkPropertyMetadataOptions.None);
        }

        public static DependencyProperty Register<TParent, TValue>(string name, FrameworkPropertyMetadataOptions flags)
        {
            return Register<TParent, TValue>(name, delegate { }, flags);
        }

        public static DependencyProperty Register<TParent, TValue>(string name, PropertyChangedCallback callback, FrameworkPropertyMetadataOptions flags)
        {
            return DependencyProperty.Register(
                name,
                typeof(TValue),
                typeof(TParent),
                new FrameworkPropertyMetadata(
                    default(TValue),
                    flags,
                    callback));
        }
    }
}
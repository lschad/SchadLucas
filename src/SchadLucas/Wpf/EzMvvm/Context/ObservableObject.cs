using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace SchadLucas.Wpf.EzMvvm.Context
{
    [SuppressMessage("ReSharper", "VirtualMemberNeverOverridden.Global", Justification = "But 3rd parties might override it.")]
    [SuppressMessage("ReSharper", "UnusedMethodReturnValue.Global", Justification = "But 3rd parties might use it.")]
    public abstract class ObservableObject : IObservableObject
    {
        public virtual event PropertyChangedEventHandler PropertyChanged;
        public virtual event PropertyChangingEventHandler PropertyChanging;

        [DebuggerStepThrough]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [DebuggerStepThrough]
        protected virtual void OnPropertyChanged<TProperty>(Expression<Func<TProperty>> p)
        {
            if (p.Body is MemberExpression expression)
            {
                OnPropertyChanged(expression.Member.Name);
            }
        }

        [DebuggerStepThrough]
        protected virtual void OnPropertyChanged(IEnumerable<string> propertyNames)
        {
            foreach (var propertyName in propertyNames)
            {
                OnPropertyChanged(propertyName);
            }
        }

        [DebuggerStepThrough]
        protected virtual void OnPropertyChanging([CallerMemberName] string propertyName = null)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        [DebuggerStepThrough]
        protected virtual void OnPropertyChanging<TProperty>(Expression<Func<TProperty>> p)
        {
            if (p.Body is MemberExpression expression)
            {
                OnPropertyChanging(expression.Member.Name);
            }
        }

        [DebuggerStepThrough]
        protected virtual void OnPropertyChanging(IEnumerable<string> propertyNames)
        {
            foreach (var propertyName in propertyNames)
            {
                OnPropertyChanging(propertyName);
            }
        }

        [DebuggerStepThrough]
        protected virtual bool SetField<T>(ref T field, T value, [CallerMemberName] string property = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return false;
            }

            OnPropertyChanging(property);
            field = value;
            OnPropertyChanged(property);
            return true;
        }
    }
}
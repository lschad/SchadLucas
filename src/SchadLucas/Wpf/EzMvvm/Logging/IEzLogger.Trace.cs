using System;
using System.ComponentModel;

namespace SchadLucas.Wpf.EzMvvm.Logging
{
    public partial interface IEzLogger
    {
        bool IsTraceEnabled { get; }
      
        void Trace<T>(T value);

        void Trace<T>(IFormatProvider formatProvider, T value);

        void Trace(Exception exception, [Localizable(false)] string message);

        void Trace(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args);

        void Trace([Localizable(false)] string message);

        void Trace([Localizable(false)] string message, params object[] args);

        void Trace<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument);

        void Trace<TArgument>([Localizable(false)] string message, TArgument argument);

        void Trace<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2);

        void Trace<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2);

        void Trace<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3);

        void Trace<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3);
    }
}
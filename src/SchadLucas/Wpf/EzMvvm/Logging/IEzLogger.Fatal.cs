using System;
using System.ComponentModel;

namespace SchadLucas.Wpf.EzMvvm.Logging
{
    public partial interface IEzLogger
    {
        bool IsFatalEnabled { get; }

        void Fatal<T>(T value);

        void Fatal<T>(IFormatProvider formatProvider, T value);

        void Fatal(Exception exception, [Localizable(false)] string message);

        void Fatal(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args);

        void Fatal([Localizable(false)] string message);

        void Fatal([Localizable(false)] string message, params object[] args);

        void Fatal<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument);

        void Fatal<TArgument>([Localizable(false)] string message, TArgument argument);

        void Fatal<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2);

        void Fatal<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2);

        void Fatal<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3);

        void Fatal<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3);
    }
}
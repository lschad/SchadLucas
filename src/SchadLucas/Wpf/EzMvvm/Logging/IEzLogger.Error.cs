using System;
using System.ComponentModel;

namespace SchadLucas.Wpf.EzMvvm.Logging
{
    public partial interface IEzLogger
    {
        bool IsErrorEnabled { get; }

        void Error<T>(T value);

        void Error<T>(IFormatProvider formatProvider, T value);

        void Error(Exception exception, [Localizable(false)] string message);

        void Error(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args);

        void Error([Localizable(false)] string message);

        void Error([Localizable(false)] string message, params object[] args);

        void Error<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument);

        void Error<TArgument>([Localizable(false)] string message, TArgument argument);

        void Error<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2);

        void Error<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2);

        void Error<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3);

        void Error<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3);
    }
}
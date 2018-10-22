using System;
using System.ComponentModel;

namespace SchadLucas.Wpf.EzMvvm.Logging
{
    public partial interface IEzLogger
    {
        bool IsDebugEnabled { get; }

        void Debug<T>(T value);

        void Debug<T>(IFormatProvider formatProvider, T value);

        void Debug(Exception exception, [Localizable(false)] string message);

        void Debug(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args);

        void Debug([Localizable(false)] string message);

        void Debug([Localizable(false)] string message, params object[] args);

        void Debug<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument);

        void Debug<TArgument>([Localizable(false)] string message, TArgument argument);

        void Debug<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2);

        void Debug<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2);

        void Debug<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3);

        void Debug<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3);
    }
}
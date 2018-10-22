using System;
using System.ComponentModel;

namespace SchadLucas.Wpf.EzMvvm.Logging
{
    public partial interface IEzLogger
    {
        bool IsWarnEnabled { get; }
       
        void Warn<T>(T value);

        void Warn<T>(IFormatProvider formatProvider, T value);

        void Warn(Exception exception, [Localizable(false)] string message);

        void Warn(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args);

        void Warn([Localizable(false)] string message);

        void Warn([Localizable(false)] string message, params object[] args);

        void Warn<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument);

        void Warn<TArgument>([Localizable(false)] string message, TArgument argument);

        void Warn<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2);

        void Warn<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2);

        void Warn<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3);

        void Warn<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3);
    }
}
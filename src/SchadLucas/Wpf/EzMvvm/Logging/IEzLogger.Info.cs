using System;
using System.ComponentModel;

namespace SchadLucas.Wpf.EzMvvm.Logging
{
    public partial interface IEzLogger
    {
        bool IsInfoEnabled { get; }
       
        void Info<T>(T value);

        void Info<T>(IFormatProvider formatProvider, T value);

        void Info(Exception exception, [Localizable(false)] string message);

        void Info(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args);

        void Info([Localizable(false)] string message);

        void Info([Localizable(false)] string message, params object[] args);

        void Info<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument);

        void Info<TArgument>([Localizable(false)] string message, TArgument argument);

        void Info<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2);

        void Info<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2);

        void Info<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3);

        void Info<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3);
    }
}
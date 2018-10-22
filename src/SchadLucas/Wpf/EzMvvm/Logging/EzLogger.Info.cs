using System;

namespace SchadLucas.Wpf.EzMvvm.Logging
{
    public partial class EzLogger
    {
        public bool IsInfoEnabled
        {
            get { return _logger.IsInfoEnabled; }
        }

        public void Info<T>(T value)
        {
            _logger.Info(value);
        }

        public void Info<T>(IFormatProvider formatProvider, T value)
        {
            _logger.Info(formatProvider, value);
        }

        public void Info(Exception exception, string message)
        {
            _logger.Info(exception, message);
        }

        public void Info(IFormatProvider formatProvider, string message, params object[] args)
        {
            _logger.Info(formatProvider, message, args);
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Info(string message, params object[] args)
        {
            _logger.Info(message, args);
        }

        public void Info<TArgument>(IFormatProvider formatProvider, string message, TArgument argument)
        {
            _logger.Info(formatProvider, message, argument);
        }

        public void Info<TArgument>(string message, TArgument argument)
        {
            _logger.Info(message, argument);
        }

        public void Info<TArgument1, TArgument2>(IFormatProvider formatProvider, string message, TArgument1 argument1, TArgument2 argument2)
        {
            _logger.Info(formatProvider, message, argument1, argument2);
        }

        public void Info<TArgument1, TArgument2>(string message, TArgument1 argument1, TArgument2 argument2)
        {
            _logger.Info(message, argument1, argument2);
        }

        public void Info<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            _logger.Info(formatProvider, message, argument1, argument2, argument3);
        }

        public void Info<TArgument1, TArgument2, TArgument3>(string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            _logger.Info(message, argument1, argument2, argument3);
        }
    }
}
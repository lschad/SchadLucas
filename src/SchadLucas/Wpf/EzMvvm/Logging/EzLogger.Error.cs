using System;

namespace SchadLucas.Wpf.EzMvvm.Logging
{
    public partial class EzLogger
    {
        public bool IsErrorEnabled
        {
            get { return _logger.IsErrorEnabled; }
        }

        public void Error<T>(T value)
        {
            _logger.Error(value);
        }

        public void Error<T>(IFormatProvider formatProvider, T value)
        {
            _logger.Error(formatProvider, value);
        }

        public void Error(Exception exception, string message)
        {
            _logger.Error(exception, message);
        }

        public void Error(IFormatProvider formatProvider, string message, params object[] args)
        {
            _logger.Error(formatProvider, message, args);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Error(string message, params object[] args)
        {
            _logger.Error(message, args);
        }

        public void Error<TArgument>(IFormatProvider formatProvider, string message, TArgument argument)
        {
            _logger.Error(formatProvider, message, argument);
        }

        public void Error<TArgument>(string message, TArgument argument)
        {
            _logger.Error(message, argument);
        }

        public void Error<TArgument1, TArgument2>(IFormatProvider formatProvider, string message, TArgument1 argument1, TArgument2 argument2)
        {
            _logger.Error(formatProvider, message, argument1, argument2);
        }

        public void Error<TArgument1, TArgument2>(string message, TArgument1 argument1, TArgument2 argument2)
        {
            _logger.Error(message, argument1, argument2);
        }

        public void Error<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            _logger.Error(formatProvider, message, argument1, argument2, argument3);
        }

        public void Error<TArgument1, TArgument2, TArgument3>(string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            _logger.Error(message, argument1, argument2, argument3);
        }
    }
}
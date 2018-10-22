using System;

namespace SchadLucas.Wpf.EzMvvm.Logging
{
    public partial class EzLogger
    {
        public bool IsDebugEnabled
        {
            get { return _logger.IsDebugEnabled; }
        }

        public void Debug<T>(T value)
        {
            _logger.Debug(value);
        }

        public void Debug<T>(IFormatProvider formatProvider, T value)
        {
            _logger.Debug(formatProvider, value);
        }

        public void Debug(Exception exception, string message)
        {
            _logger.Debug(exception, message);
        }

        public void Debug(IFormatProvider formatProvider, string message, params object[] args)
        {
            _logger.Debug(formatProvider, message, args);
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Debug(string message, params object[] args)
        {
            _logger.Debug(message, args);
        }

        public void Debug<TArgument>(IFormatProvider formatProvider, string message, TArgument argument)
        {
            _logger.Debug(formatProvider, message, argument);
        }

        public void Debug<TArgument>(string message, TArgument argument)
        {
            _logger.Debug(message, argument);
        }

        public void Debug<TArgument1, TArgument2>(IFormatProvider formatProvider, string message, TArgument1 argument1, TArgument2 argument2)
        {
            _logger.Debug(formatProvider, message, argument1, argument2);
        }

        public void Debug<TArgument1, TArgument2>(string message, TArgument1 argument1, TArgument2 argument2)
        {
            _logger.Debug(message, argument1, argument2);
        }

        public void Debug<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            _logger.Debug(formatProvider, message, argument1, argument2, argument3);
        }

        public void Debug<TArgument1, TArgument2, TArgument3>(string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            _logger.Debug(message, argument1, argument2, argument3);
        }
    }
}
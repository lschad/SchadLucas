using System;

namespace SchadLucas.Wpf.EzMvvm.Logging
{
    public partial class EzLogger
    {
        public bool IsWarnEnabled
        {
            get { return _logger.IsWarnEnabled; }
        }

        public void Warn<T>(T value)
        {
            _logger.Warn(value);
        }

        public void Warn<T>(IFormatProvider formatProvider, T value)
        {
            _logger.Warn(formatProvider, value);
        }

        public void Warn(Exception exception, string message)
        {
            _logger.Warn(exception, message);
        }

        public void Warn(IFormatProvider formatProvider, string message, params object[] args)
        {
            _logger.Warn(formatProvider, message, args);
        }

        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        public void Warn(string message, params object[] args)
        {
            _logger.Warn(message, args);
        }

        public void Warn<TArgument>(IFormatProvider formatProvider, string message, TArgument argument)
        {
            _logger.Warn(formatProvider, message, argument);
        }

        public void Warn<TArgument>(string message, TArgument argument)
        {
            _logger.Warn(message, argument);
        }

        public void Warn<TArgument1, TArgument2>(IFormatProvider formatProvider, string message, TArgument1 argument1, TArgument2 argument2)
        {
            _logger.Warn(formatProvider, message, argument1, argument2);
        }

        public void Warn<TArgument1, TArgument2>(string message, TArgument1 argument1, TArgument2 argument2)
        {
            _logger.Warn(message, argument1, argument2);
        }

        public void Warn<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            _logger.Warn(formatProvider, message, argument1, argument2, argument3);
        }

        public void Warn<TArgument1, TArgument2, TArgument3>(string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            _logger.Warn(message, argument1, argument2, argument3);
        }
    }
}
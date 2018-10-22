using System;

namespace SchadLucas.Wpf.EzMvvm.Logging
{
    public partial class EzLogger
    {
        public bool IsTraceEnabled
        {
            get { return _logger.IsTraceEnabled; }
        }

        public void Trace<T>(T value)
        {
            _logger.Trace(value);
        }

        public void Trace<T>(IFormatProvider formatProvider, T value)
        {
            _logger.Trace(formatProvider, value);
        }

        public void Trace(Exception exception, string message)
        {
            _logger.Trace(exception, message);
        }

        public void Trace(IFormatProvider formatProvider, string message, params object[] args)
        {
            _logger.Trace(formatProvider, message, args);
        }

        public void Trace(string message)
        {
            _logger.Trace(message);
        }

        public void Trace(string message, params object[] args)
        {
            _logger.Trace(message, args);
        }

        public void Trace<TArgument>(IFormatProvider formatProvider, string message, TArgument argument)
        {
            _logger.Trace(formatProvider, message, argument);
        }

        public void Trace<TArgument>(string message, TArgument argument)
        {
            _logger.Trace(message, argument);
        }

        public void Trace<TArgument1, TArgument2>(IFormatProvider formatProvider, string message, TArgument1 argument1, TArgument2 argument2)
        {
            _logger.Trace(formatProvider, message, argument1, argument2);
        }

        public void Trace<TArgument1, TArgument2>(string message, TArgument1 argument1, TArgument2 argument2)
        {
            _logger.Trace(message, argument1, argument2);
        }

        public void Trace<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            _logger.Trace(formatProvider, message, argument1, argument2, argument3);
        }

        public void Trace<TArgument1, TArgument2, TArgument3>(string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            _logger.Trace(message, argument1, argument2, argument3);
        }
    }
}
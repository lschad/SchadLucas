using System;

namespace SchadLucas.Wpf.EzMvvm.Logging
{
    public partial class EzLogger
    {
        public bool IsFatalEnabled
        {
            get { return _logger.IsErrorEnabled; }
        }
      
        public void Fatal<T>(T value)
        {
            _logger.Fatal(value);
        }

        public void Fatal<T>(IFormatProvider formatProvider, T value)
        {
            _logger.Fatal(formatProvider, value);
        }

        public void Fatal(Exception exception, string message)
        {
            _logger.Fatal(exception, message);
        }

        public void Fatal(IFormatProvider formatProvider, string message, params object[] args)
        {
            _logger.Fatal(formatProvider, message, args);
        }

        public void Fatal(string message)
        {
            _logger.Fatal(message);
        }

        public void Fatal(string message, params object[] args)
        {
            _logger.Fatal(message, args);
        }

        public void Fatal<TArgument>(IFormatProvider formatProvider, string message, TArgument argument)
        {
            _logger.Fatal(formatProvider, message, argument);
        }

        public void Fatal<TArgument>(string message, TArgument argument)
        {
            _logger.Fatal(message, argument);
        }

        public void Fatal<TArgument1, TArgument2>(IFormatProvider formatProvider, string message, TArgument1 argument1, TArgument2 argument2)
        {
            _logger.Fatal(formatProvider, message, argument1, argument2);
        }

        public void Fatal<TArgument1, TArgument2>(string message, TArgument1 argument1, TArgument2 argument2)
        {
            _logger.Fatal(message, argument1, argument2);
        }

        public void Fatal<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            _logger.Fatal(formatProvider, message, argument1, argument2, argument3);
        }

        public void Fatal<TArgument1, TArgument2, TArgument3>(string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            _logger.Fatal(message, argument1, argument2, argument3);
        }
    }
}
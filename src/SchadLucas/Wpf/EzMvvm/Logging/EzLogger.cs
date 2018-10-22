using System;
using NLog;

namespace SchadLucas.Wpf.EzMvvm.Logging
{
    public partial class EzLogger : IEzLogger
    {
        private readonly Logger _logger;

        public EzLogger(Logger logger)
        {
            _logger = logger;
        }

        public event EventHandler<EventArgs> LoggerReconfigured
        {
            add { _logger.LoggerReconfigured += value; }
            remove { _logger.LoggerReconfigured -= value; }
        }

        public LogFactory Factory
        {
            get { return _logger.Factory; }
        }

        public string Name
        {
            get { return _logger.Name; }
        }
    }
}
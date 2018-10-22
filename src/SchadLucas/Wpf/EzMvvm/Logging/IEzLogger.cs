using System;
using NLog;

namespace SchadLucas.Wpf.EzMvvm.Logging
{
    public partial interface IEzLogger
    {
        event EventHandler<EventArgs> LoggerReconfigured;

        LogFactory Factory { get; }

        string Name { get; }
    }
}
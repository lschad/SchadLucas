using System;
using SchadLucas.Wpf.EzMvvm.Commands.Generic;

namespace SchadLucas.Wpf.EzMvvm.Commands
{
    public class EzCommand : EzCommand<object, object>
    {
        public EzCommand(Action<object> execute) : base(execute) { }
        public EzCommand(Action<object> execute, Predicate<object> canExecute) : base(execute, canExecute) { }
    }
}
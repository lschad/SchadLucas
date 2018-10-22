using System;
using System.Windows.Input;

namespace SchadLucas.Wpf.EzMvvm.Commands.Generic
{
    public class EzCommand<TExecute, TCanExecute> : ICommand
    {
        private readonly Predicate<TCanExecute> _canExecute;
        private readonly Action<TExecute> _execute;

        public EzCommand(Action<TExecute> execute) : this(execute, _ => true) { }

        public EzCommand(Action<TExecute> execute, Predicate<TCanExecute> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute((TCanExecute) parameter);
        }

        public void Execute(object parameter)
        {
            _execute((TExecute) parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
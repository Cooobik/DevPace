using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DevPace.Desktop.Helpers.Commands
{
    public class RelayCommandAsync : ICommand
    {
        private readonly Func<Task> _execute;
        private readonly Func<bool> _canExecute;

        public event EventHandler CanExecuteChanged;

        public RelayCommandAsync(Func<Task> execute)
            : this(execute, null)
        {
        }

        public RelayCommandAsync(Func<Task> execute, Func<bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke() ?? true;
        }

        public async void Execute(object parameter)
        {
            await ExecuteAsync(parameter);
        }

        protected virtual async Task ExecuteAsync(object parameter)
        {
            await _execute();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    //public class RelayCommandAsync<T> : RelayCommandAsync
    //{
    //    private readonly Func<T, Task> _executeWithParameter;

    //    public RelayCommandAsync(Func<Task> execute)
    //        : base(execute)
    //    {
    //    }

    //    public RelayCommandAsync(Func<Task> execute, Func<bool> canExecute)
    //        : base(execute, canExecute)
    //    {
    //    }

    //    public RelayCommandAsync(Func<T, Task> executeWithParameter)
    //        : base(null)
    //    {
    //        _executeWithParameter = executeWithParameter ?? throw new ArgumentNullException(nameof(executeWithParameter));
    //    }

    //    public RelayCommandAsync(Func<T, Task> executeWithParameter, Func<bool> canExecute)
    //        : base(null, canExecute)
    //    {
    //        _executeWithParameter = executeWithParameter ?? throw new ArgumentNullException(nameof(executeWithParameter));
    //    }

    //    protected override async Task ExecuteAsync(object parameter)
    //    {
    //        if (parameter is T parameterValue)
    //        {
    //            await _executeWithParameter(parameterValue);
    //        }
    //        else
    //        {
    //            await base.ExecuteAsync(parameter);
    //        }
    //    }
    //}
}

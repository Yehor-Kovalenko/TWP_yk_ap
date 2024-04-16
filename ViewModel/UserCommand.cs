
using System.Windows.Input;

namespace ViewModel
{
    internal class UserCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;
        public event EventHandler? CanExecuteChanged;

        public UserCommand(Action execute, Func<bool>? canExecute = null)
        {
            if (execute is null)
            {
                throw new ArgumentNullException("Action is null");
            } 

            this._execute = execute;
            this._canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            if (this._canExecute is null)
            {
                return false;
            } 
            else
            {
                return this._canExecute();

            }
        }

        public void Execute(object? parameter)
        {
            this._execute();
        }

        internal void NotifyCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}

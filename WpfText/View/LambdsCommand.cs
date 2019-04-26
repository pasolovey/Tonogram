using System;
using System.Windows.Input;

namespace WpfText.View
{
    public class LambdsCommand : ICommand
    {
        private readonly Action action;

        public event EventHandler CanExecuteChanged;

        public LambdsCommand(Action action)
        {
            this.action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            action?.Invoke();
        }
    }
}

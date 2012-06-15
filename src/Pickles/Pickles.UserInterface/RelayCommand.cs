using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Pickles.UserInterface
{
    public class RelayCommand : ICommand
    {
        private readonly Action execute;

        public RelayCommand(Action execute)
        {
            this.execute = execute;
        }

        #region Implementation of ICommand

        public void Execute(object parameter)
        {
            execute();
        }

        public bool CanExecute(object parameter)
        {
            return execute != null;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (CanExecute(null)) CommandManager.RequerySuggested += value;
            }
            remove
            {
                if (CanExecute(null)) CommandManager.RequerySuggested -= value;
            }
        }

        #endregion
    }
}

using System;
using System.Windows.Input;

namespace Pickles.UserInterface
{
  public class RelayCommand : ICommand
  {
    private readonly Action<object> execute;

    private readonly Func<object, bool> canExecute;

    public RelayCommand(Action execute)
      : this(o => execute())
    {
    }

    public RelayCommand(Action execute, Func<bool> canExecute)
      : this(o => execute(), o => canExecute())
    {
    }

    public RelayCommand(Action<object> execute)
      : this(execute, o => true)
    {
    }

    public RelayCommand(Action<object> execute, Func<object, bool> canExecute)
    {
      this.execute = execute;
      this.canExecute = canExecute;
    }

    #region Implementation of ICommand

    public void Execute(object parameter)
    {
      execute(parameter);
    }

    public bool CanExecute(object parameter)
    {
      return canExecute(parameter);
    }

    public event EventHandler CanExecuteChanged;

    #endregion

    public void RaiseCanExecuteChanged()
    {
      this.CanExecuteChanged.Raise(this, EventArgs.Empty);
    }
  }
}

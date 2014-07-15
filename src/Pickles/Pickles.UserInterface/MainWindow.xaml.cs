using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Shell;

using PicklesDoc.Pickles.UserInterface.ViewModel;

namespace PicklesDoc.Pickles.UserInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private MainViewModel ViewModel
        {
          get { return this.DataContext as MainViewModel; }
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.ViewModel.LoadFromSettings();
        }

        private void MetroWindow_Closed(object sender, EventArgs e)
        {
            this.ViewModel.SaveToSettings();
        }

        private void MainWindow_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
          var oldVm = e.NewValue as MainViewModel;

          if (oldVm != null)
          {
            oldVm.PropertyChanged -= this.ViewModelOnPropertyChanged;
          }

          var newVm = e.NewValue as MainViewModel;

          if (newVm != null)
          {
            newVm.PropertyChanged += this.ViewModelOnPropertyChanged;
          }
        }

        private void ViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
          switch (propertyChangedEventArgs.PropertyName)
          {
            case "IsRunning":
              {
                this.progressIndicator.Visibility = this.ViewModel.IsRunning ? Visibility.Visible : Visibility.Hidden;
                this.taskBarItemInfo.ProgressState = this.ViewModel.IsRunning ? TaskbarItemProgressState.Indeterminate : TaskbarItemProgressState.None;
                break;
              }
          }
        }
    }
}

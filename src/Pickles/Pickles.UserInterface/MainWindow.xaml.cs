using System;

namespace Pickles.UserInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private MainWindowViewModel ViewModel
        {
            get { return this.DataContext as MainWindowViewModel; }
        }

        private void MetroWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            ViewModel.LoadFromSettings();
        }

        private void MetroWindow_Closed(object sender, EventArgs e)
        {
            ViewModel.SaveToSettings();
        }
    }
}

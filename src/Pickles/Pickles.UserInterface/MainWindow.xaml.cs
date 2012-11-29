using System;

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

        private MainWindowViewModel ViewModel
        {
            get { return this.DataContext as MainWindowViewModel; }
        }

        private void MetroWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.ViewModel.LoadFromSettings();
        }

        private void MetroWindow_Closed(object sender, EventArgs e)
        {
            this.ViewModel.SaveToSettings();
        }
    }
}

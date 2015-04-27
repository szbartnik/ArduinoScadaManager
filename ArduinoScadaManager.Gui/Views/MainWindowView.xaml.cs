using System.Windows;
using System.Windows.Controls;
using ArduinoScadaManager.Gui.ViewModels.MainWindowViewModels;

namespace ArduinoScadaManager.Gui.Views
{
    public partial class MainWindowView : Window
    {
        public MainWindowView(MainWindowViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void OnLogTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            LogTextBox.CaretIndex = LogTextBox.Text.Length;
            LogTextBox.ScrollToEnd();
        }
    }
}

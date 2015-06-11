using ArduinoScadaManager.Common.Infrastructure;

namespace ArduinoScadaManager.Gui.ViewModels.MainWindowViewModels
{
    public sealed partial class MainWindowViewModel
    {
        public RelayCommand AddMasterCommand { get; set; }
        public RelayCommand AddSlaveCommand { get; set; }
        public RelayCommand ConnectCommand { get; set; }
        public RelayCommand ClearConsoleCommand { get; set; }

        private void InitializeCommands()
        {
            AddMasterCommand = new RelayCommand(async () => await AddNewScadaModule());
            AddSlaveCommand  = new RelayCommand(() => AddNewSlaveModule(SelectedSlaveModuleToAdd));
            ConnectCommand   = new RelayCommand(async () => await InitializeSlavesConnection());
            ClearConsoleCommand = new RelayCommand(() => OutputTextBoxContent = string.Empty);
        }
    }
}

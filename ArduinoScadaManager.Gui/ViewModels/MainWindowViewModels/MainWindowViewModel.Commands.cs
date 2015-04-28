using ArduinoScadaManager.Common.Infrastructure;

namespace ArduinoScadaManager.Gui.ViewModels.MainWindowViewModels
{
    public sealed partial class MainWindowViewModel
    {
        public RelayCommand AddMasterCommand { get; set; }
        public RelayCommand AddSlaveCommand { get; set; }

        private void InitializeCommands()
        {
            AddMasterCommand = new RelayCommand(AddNewScadaModule);
            AddSlaveCommand = new RelayCommand(() => AddNewSlaveModule(SelectedSlaveModuleToAdd));
        }
    }
}

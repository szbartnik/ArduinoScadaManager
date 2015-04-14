using ArduinoScadaManager.Common.Infrastructure;

namespace ArduinoScadaManager.Gui.ViewModels.MainWindowViewModels
{
    public partial class MainWindowViewModel
    {
        public RelayCommand AddMasterCommand { get; set; }
        public RelayCommand AddSlaveCommand { get; set; }

        private void InitializeCommands()
        {
            AddMasterCommand = new RelayCommand(AddNewMaster);
            AddSlaveCommand = new RelayCommand(() => AddNewSlave(SelectedSlaveModuleToAdd));
        }
    }
}

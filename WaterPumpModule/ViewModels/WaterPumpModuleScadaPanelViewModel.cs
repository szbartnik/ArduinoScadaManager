using ArduinoScadaManager.Common.Core;
using ArduinoScadaManager.Common.Infrastructure;
using ArduinoScadaManager.Common.ViewModels;

namespace WaterPumpModule.ViewModels
{
    public class WaterPumpModuleScadaPanelViewModel : SlaveModuleScadaPanelViewModelBase
    {
        public bool IsPumpPoweredOn
        {
            get { return _isPumpPoweredOn; }
            set
            {
                _isPumpPoweredOn = value;
                OnPropertyChanged();
            }
        }
        private bool _isPumpPoweredOn;

        public RelayCommand TurnPumpOnCommand { get; set; }
        public RelayCommand TurnPumpOffCommand { get; set; }

        public WaterPumpModuleScadaPanelViewModel()
        {
            
        }
    }
}

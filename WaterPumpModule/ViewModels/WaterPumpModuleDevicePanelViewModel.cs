using ArduinoScadaManager.Common.Core;
using ArduinoScadaManager.Common.ViewModels;
using WaterPumpModule.Models;

namespace WaterPumpModule.ViewModels
{
    public class WaterPumpModuleDevicePanelViewModel : SlaveModuleDevicePanelViewModelBase
    {
        public PumpState PumpState
        {
            get { return _pumpState; }
            set
            {
                _pumpState = value; 
                OnPropertyChanged();
            }
        }
        private PumpState _pumpState;

        public WaterPumpModuleDevicePanelViewModel(ICoreManager manager)
            :base(manager)
        {
            
        }
    }
}

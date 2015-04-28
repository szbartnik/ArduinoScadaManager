using ArduinoScadaManager.Common.Core;
using ArduinoScadaManager.Common.Infrastructure;
using ArduinoScadaManager.Common.Interfaces;
using ArduinoScadaManager.Common.ViewModels;

namespace WaterPumpModule.ViewModels
{
    public class WaterPumpModuleScadaPanelViewModel : SlaveModuleScadaPanelViewModelBase
    {
        public string PumpState
        {
            get { return _pumpState; }
            private set
            {
                _pumpState = value;
                OnPropertyChanged();
            }
        }
        private string _pumpState;

        public RelayCommand TurnPumpOnCommand { get; private set; }
        public RelayCommand TurnPumpOffCommand { get; private set; }
        public RelayCommand RefreshPumpStateCommand { get; private set; }

        public WaterPumpModuleScadaPanelViewModel(
            IModbusTransferManager modbusTransferManager, 
            IMasterModuleProcess masterModuleProcess, 
            ISlaveModuleProcess slaveModuleProcess)
            : base(modbusTransferManager, masterModuleProcess, slaveModuleProcess)
        {
            PumpState = "Unknown";
            TurnPumpOnCommand = new RelayCommand(() => SendRequest(1, "ON"));
            TurnPumpOffCommand = new RelayCommand(() => SendRequest(1, "OFF"));
            RefreshPumpStateCommand = new RelayCommand(() => SendRequest(2));
        }

        protected override void OnDataReceived(ModbusTransferData modbusTransferData)
        {
            switch (modbusTransferData.CommandId)
            {
                case 1:
                    Logger.WriteDebug(string.Format("Command successfully sent (master received the confirmation)"));
                    break;
                case 2:
                    PumpState = modbusTransferData.Data.ByteArrayToString();
                    break;
                case ErrorCommand:
                    Logger.WriteDebug(string.Format("Error received by master. Error message: {0}", 
                        modbusTransferData.Data.ByteArrayToString()));
                    break;
            }
        }
    }
}

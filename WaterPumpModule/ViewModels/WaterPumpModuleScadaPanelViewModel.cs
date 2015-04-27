using ArduinoScadaManager.Common.Core;
using ArduinoScadaManager.Common.Infrastructure;
using ArduinoScadaManager.Common.Interfaces;
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

        public WaterPumpModuleScadaPanelViewModel(
            IModbusTransferManager modbusTransferManager, 
            IMasterModuleProcess masterModuleProcess, 
            ISlaveModuleProcess slaveModuleProcess)
            : base(modbusTransferManager, masterModuleProcess, slaveModuleProcess)
        {
            TurnPumpOnCommand = new RelayCommand(() => SendRequest(1, "ON"));
            TurnPumpOffCommand = new RelayCommand(() => SendRequest(1, "OFF"));
        }

        protected override void OnDataReceived(ModbusTransferData modbusTransferData)
        {
            switch (modbusTransferData.CommandId)
            {
                case 1:
                    break;
                case 255:
                    Logger.WriteDebug(string.Format("Error received by master. {0}", 
                        modbusTransferData.Data.ByteArrayToString()));
                    break;
            }
        }
    }
}

using System.Threading;
using System.Threading.Tasks;
using System.Windows.Navigation;
using ArduinoScadaManager.Common.Core;
using ArduinoScadaManager.Common.Infrastructure;
using ArduinoScadaManager.Common.Interfaces;
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

        public RelayCommand SimulateFailureCommand { get; private set; }

        public WaterPumpModuleDevicePanelViewModel(
            IModbusTransferManager modbusTransferManager, 
            SlaveModuleProcessBase slaveModuleProcess)
            :base(modbusTransferManager, slaveModuleProcess)
        {
            PumpState = PumpState.Stopped;
            SimulateFailureCommand = new RelayCommand(() => PumpState = PumpState.Failure);
        }

        protected override void OnDataReceived(ModbusTransferData modbusTransferData)
        {
            switch (modbusTransferData.CommandId)
            {
                case 1:
                    PumpStateControl(modbusTransferData.Data);
                    break;
                case 2:
                    GetPumpState();
                    break;
                default:
                    SendErrorResponse(string.Format("Command {0} not recognized", modbusTransferData.CommandId));
                    break;
            }
        }

        private void GetPumpState()
        {
            SendResponse(2, PumpState.ToString());
        }

        private void PumpStateControl(byte[] pumpControlCommand)
        {
            var pumpControlCommandStr = pumpControlCommand.ByteArrayToString();
            switch (pumpControlCommandStr)
            {
                case "ON":
                    RunPump();
                    break;
                case "OFF":
                    StopPump();
                    break;
                default:
                    SendErrorResponse(string.Format("Control word '{0}' of PumpStateControl not recognized", pumpControlCommandStr));
                    break;
            }
        }

        private void RunPump()
        {
            Task.Run(() =>
            {
                if ((PumpState != PumpState.Stopped && PumpState != PumpState.Failure))
                {
                    SendErrorResponse(PumpState.ToString());
                    return;
                }

                PumpState = PumpState.Starting;

                SendResponse(1);

                Thread.Sleep(5000);
                PumpState = PumpState.Running;
            });
        }

        private void StopPump()
        {
            Task.Run(() =>
            {
                if ((PumpState != PumpState.Running))
                {
                    SendErrorResponse(PumpState.ToString());
                    return;
                }

                PumpState = PumpState.Stopping;

                SendResponse(1);

                Thread.Sleep(5000);
                PumpState = PumpState.Stopped;
            });
        }
    }
}

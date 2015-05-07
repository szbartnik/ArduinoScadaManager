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

        private void GetPumpState()
        {
            // TODO
            //SendResponse(2, PumpState.ToString());
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
                    break;
            }
        }

        private void RunPump()
        {
            Task.Run(() =>
            {
                if ((PumpState != PumpState.Stopped && PumpState != PumpState.Failure))
                {
                    return;
                }

                PumpState = PumpState.Starting;

                // TODO
                //SendResponse(1);

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
                    return;
                }

                PumpState = PumpState.Stopping;

                // TODO
                //SendResponse(1);

                Thread.Sleep(5000);
                PumpState = PumpState.Stopped;
            });
        }
    }
}

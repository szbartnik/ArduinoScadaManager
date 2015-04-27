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

        public WaterPumpModuleDevicePanelViewModel(
            IModbusTransferManager modbusTransferManager, 
            SlaveModuleProcessBase slaveModuleProcess)
            :base(modbusTransferManager, slaveModuleProcess)
        {
            PumpState = PumpState.Stopped;
        }

        protected override void OnDataReceived(ModbusTransferData modbusTransferData)
        {
            if (modbusTransferData.CommandId != 1) 
                return;

            switch (modbusTransferData.Data.ByteArrayToString())
            {
                case "ON":
                    RunPump();
                    break;
                case "OFF":
                    StopPump();
                    break;
            }
        }

        private void RunPump()
        {
            Task.Run(() =>
            {
                if ((PumpState == PumpState.Starting || PumpState == PumpState.Running))
                {
                    SendResponse(255, PumpState.ToString());
                    return;
                }

                PumpState = PumpState.Starting;

                SendResponse(1, (byte[]) null);

                Thread.Sleep(5000);
                PumpState = PumpState.Running;
            });
        }

        private void StopPump()
        {
            Task.Run(() =>
            {
                if ((PumpState == PumpState.Failure || PumpState == PumpState.Stopped))
                {
                    SendResponse(255, PumpState.ToString());
                    return;
                }

                PumpState = PumpState.Stopping;

                SendResponse(1, (byte[])null);

                Thread.Sleep(5000);
                PumpState = PumpState.Stopped;
            });
        }
    }
}

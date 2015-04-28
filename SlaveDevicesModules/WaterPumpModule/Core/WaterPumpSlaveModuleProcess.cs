using System.Windows.Controls;
using ArduinoScadaManager.Common.Core;
using ArduinoScadaManager.Common.Interfaces;
using ArduinoScadaManager.Common.ViewModels;
using WaterPumpModule.ViewModels;
using WaterPumpModule.Views;

namespace WaterPumpModule.Core
{
    public class WaterPumpSlaveModuleProcess : SlaveModuleProcessBase
    {
        public WaterPumpSlaveModuleProcess(ICoreManager manager, string name)
            : base(manager, name)
        {
        }

        protected override UserControl GetDevicePanelView()
        {
            return new WaterPumpModuleDevicePanelView(
                new WaterPumpModuleDevicePanelViewModel(CoreManager.ModbusTransferManager, this));
        }

        protected override SlaveModuleScadaPanelViewModelBase GetScadaPanelOfSlaveModule(
            IMasterModuleProcess masterModuleProcess,
            ISlaveModuleProcess slaveModuleProcess)
        {
            var viewModel = new WaterPumpModuleScadaPanelViewModel(
                CoreManager.ModbusTransferManager,
                masterModuleProcess,
                slaveModuleProcess);

            viewModel.View = new WaterPumpModuleScadaPanelView(viewModel);

            return viewModel;
        }
    }
}
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
        private readonly ICoreManager _manager;

        public override sealed UserControl View { get; set; }

        public WaterPumpSlaveModuleProcess(ICoreManager manager)
            : base(manager)
        {
            _manager = manager;

            View = new WaterPumpModuleDevicePanelView(
                new WaterPumpModuleDevicePanelViewModel(_manager));
        }

        protected override SlaveModuleScadaPanelViewModelBase GetScadaPanelOfSlaveModule(
            IMasterModuleProcess scadaModuleProcess,
            SlaveModuleProcessBase slaveModuleProcessBase)
        {
            var viewModel = new WaterPumpModuleScadaPanelViewModel(
                scadaModuleProcess,
                slaveModuleProcessBase);

            viewModel.View = new WaterPumpModuleScadaPanelView(viewModel);

            return viewModel;
        }
    }
}
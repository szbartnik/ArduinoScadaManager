using System.ComponentModel.Composition;
using System.Windows.Controls;
using ArduinoScadaManager.Common.Core;
using WaterPumpModule.ViewModels;
using WaterPumpModule.Views;

namespace WaterPumpModule.Core
{
    [Export(typeof(ISlaveModule))]
    public class SlaveModule : ISlaveModule
    {
        public string Name
        {
            get { return "Water pump module"; }
        }

        public ISlaveModuleProcess GetSlaveModuleProcess(ICoreManager manager)
        {
            return new WaterPumpSlaveModuleProcess(manager);
        }
    }

    public class WaterPumpSlaveModuleProcess : ISlaveModuleProcess
    {
        private readonly ICoreManager _manager;

        public WaterPumpSlaveModuleProcess(ICoreManager manager)
        {
            _manager = manager;
        }

        public UserControl GetSlaveModuleDevicePanelView()
        {
            return new WaterPumpModuleDevicePanelView(new WaterPumpModuleDevicePanelViewModel(_manager));
        }
    }
}

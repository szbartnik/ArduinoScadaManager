using System.Windows.Controls;
using ArduinoScadaManager.Common.Infrastructure;

namespace ArduinoScadaManager.Common.Core
{
    public abstract class SlaveModuleProcessBase
    {
        private readonly ICoreManager _manager;
        public abstract UserControl SlaveModuleDevicePanelView { get; set; }
        public RelayCommand RemoveSlaveModuleCommand { get; set; }

        public SlaveModuleProcessBase(ICoreManager manager)
        {
            _manager = manager;
            RemoveSlaveModuleCommand = new RelayCommand(() => _manager.RemoveSlaveModule(this));
        }
    }
}
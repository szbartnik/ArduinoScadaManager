using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using ArduinoScadaManager.Common.Core;
using ArduinoScadaManager.Common.Infrastructure;
using ArduinoScadaManager.Common.Interfaces;

namespace ArduinoScadaManager.Gui.ViewModels.MasterModuleViewModels
{
    public partial class ScadaModuleProcessViewModel : ViewModelBase
    {
        private readonly ICoreManager _coreManager;
        private readonly IMasterModuleProcess _masterModuleProcess;
        public ObservableCollection<UserControl> ActiveSlaveModules { get; set; }

        public UserControl SelectedSlaveModule
        {
            get { return _selectedSlaveModule; }
            set
            {
                _selectedSlaveModule = value;
                OnPropertyChanged();
            }
        }
        private UserControl _selectedSlaveModule;

        public ScadaModuleProcessViewModel(ICoreManager coreManager, IMasterModuleProcess masterModuleProcess)
        {
            _coreManager = coreManager;
            _masterModuleProcess = masterModuleProcess;

            ActiveSlaveModules = new ObservableCollection<UserControl>(
                coreManager.ActiveSlaveDevices.Select(x => x.GetScadaPanelOfSlaveModule(_masterModuleProcess)));

            coreManager.SlaveModuleAdded += OnSlaveModuleAdded;
        }

        private void OnSlaveModuleAdded(SlaveModuleProcessBase slaveModuleProcessBase)
        {
            ActiveSlaveModules.Add(slaveModuleProcessBase.GetScadaPanelOfSlaveModule(_masterModuleProcess));
        }

        public override void Dispose()
        {
            base.Dispose();
            _coreManager.SlaveModuleAdded -= OnSlaveModuleAdded;
        }
    }
}

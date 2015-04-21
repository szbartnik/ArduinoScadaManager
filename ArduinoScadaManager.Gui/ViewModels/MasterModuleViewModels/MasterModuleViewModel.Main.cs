using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows.Controls;
using ArduinoScadaManager.Common.Core;
using ArduinoScadaManager.Common.Infrastructure;
using ArduinoScadaManager.Common.Interfaces;
using ArduinoScadaManager.Common.ViewModels;

namespace ArduinoScadaManager.Gui.ViewModels.MasterModuleViewModels
{
    public partial class ScadaModuleProcessViewModel : ViewModelBase
    {
        private readonly ICoreManager _coreManager;
        private readonly IMasterModuleProcess _masterModuleProcess;
        public ObservableCollection<SlaveModuleScadaPanelViewModelBase> ActiveSlaveModules { get; set; }

        public SlaveModuleScadaPanelViewModelBase SelectedSlaveModule
        {
            get { return _selectedSlaveModule; }
            set
            {
                _selectedSlaveModule = value;
                OnPropertyChanged();
            }
        }
        private SlaveModuleScadaPanelViewModelBase _selectedSlaveModule;

        public ScadaModuleProcessViewModel(ICoreManager coreManager, IMasterModuleProcess masterModuleProcess)
        {
            _coreManager = coreManager;
            _masterModuleProcess = masterModuleProcess;

            ActiveSlaveModules = new ObservableCollection<SlaveModuleScadaPanelViewModelBase>(
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

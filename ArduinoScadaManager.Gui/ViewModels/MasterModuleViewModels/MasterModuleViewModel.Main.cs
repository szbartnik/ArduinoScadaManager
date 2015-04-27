using System.Collections.ObjectModel;
using System.Linq;
using ArduinoScadaManager.Common.Core;
using ArduinoScadaManager.Common.Infrastructure;
using ArduinoScadaManager.Common.Interfaces;
using ArduinoScadaManager.Common.ViewModels;

namespace ArduinoScadaManager.Gui.ViewModels.MasterModuleViewModels
{
    public partial class MasterModuleViewModel : ViewModelBase
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

        public MasterModuleViewModel(ICoreManager coreManager, IMasterModuleProcess masterModuleProcess)
        {
            _coreManager = coreManager;
            _masterModuleProcess = masterModuleProcess;

            ActiveSlaveModules = new ObservableCollection<SlaveModuleScadaPanelViewModelBase>(
                coreManager.ActiveSlaveDevices.Select(x => x.GetScadaPanelOfSlaveModule(_masterModuleProcess)));

            coreManager.SlaveModuleAdded   += OnSlaveModuleAdded;
            coreManager.SlaveModuleRemoved += OnSlaveModuleRemoved;
        }

        private void OnSlaveModuleAdded(SlaveModuleProcessBase slaveModuleProcess)
        {
            ActiveSlaveModules.Add(slaveModuleProcess.GetScadaPanelOfSlaveModule(_masterModuleProcess));
        }

        private void OnSlaveModuleRemoved(SlaveModuleProcessBase slaveModuleProcess)
        {
            var toRemove = ActiveSlaveModules.Single(x => x.SlaveModuleProcess.Identifier == slaveModuleProcess.Identifier);
            ActiveSlaveModules.Remove(toRemove);
        }

        public override void Dispose()
        {
            base.Dispose();
            _coreManager.SlaveModuleAdded   -= OnSlaveModuleAdded;
            _coreManager.SlaveModuleRemoved -= OnSlaveModuleRemoved;
        }
    }
}

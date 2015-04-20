using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using ArduinoScadaManager.Common.Core;
using ArduinoScadaManager.Common.Infrastructure;

namespace ArduinoScadaManager.Common.ViewModels.ScadaModuleProcessViewModel
{
    public partial class ScadaModuleProcess : ViewModelBase
    {
        private readonly ICoreManager _coreManager;
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

        public RelayCommand RemoveScadaPanelCommand { get; set; }

        public ScadaModuleProcess(ICoreManager coreManager)
        {
            _coreManager = coreManager;
            RemoveScadaPanelCommand = new RelayCommand(RemoveScadaPanel);
            
            ActiveSlaveModules = new ObservableCollection<UserControl>(
                coreManager.ActiveSlaveDevices.Select(x => x.GetScadaPanelViewOfSlaveModule(this)));

            coreManager.SlaveModuleAdded += OnSlaveModuleAdded;
        }

        private void OnSlaveModuleAdded(SlaveModuleProcessBase slaveModuleProcessBase)
        {
            ActiveSlaveModules.Add(slaveModuleProcessBase.GetScadaPanelViewOfSlaveModule(this));
        }

        private void RemoveScadaPanel()
        {
            _coreManager.SlaveModuleAdded -= OnSlaveModuleAdded;
            _coreManager.RemoveScadaModule(this);
        }
    }
}

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using ArduinoScadaManager.Common.Core;
using ArduinoScadaManager.Common.Infrastructure;

namespace ArduinoScadaManager.Gui.ViewModels.ScadaPanelViewModels
{
    public partial class ScadaPanelViewModel : ViewModelBase
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

        public ScadaPanelViewModel(ICoreManager coreManager)
        {
            _coreManager = coreManager;
            RemoveScadaPanelCommand = new RelayCommand(() => _coreManager.RemoveScadaPanel(this));
        }
    }
}

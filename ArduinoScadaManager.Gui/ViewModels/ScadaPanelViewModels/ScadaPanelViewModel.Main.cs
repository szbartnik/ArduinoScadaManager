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

        public ScadaPanelViewModel(IEnumerable<ISlaveModule> slaveModules)
        {
            //ActiveSlaveModules = new ObservableCollection<UserControl>(
            //    slaveModules.Select(x => x.));
        }
    }
}

using System.Windows.Controls;
using ArduinoScadaManager.Common.Infrastructure;
using ArduinoScadaManager.Common.Interfaces;
using ArduinoScadaManager.Gui.ViewModels.ScadaModuleProcessViewModels;
using ArduinoScadaManager.Gui.Views;

namespace ArduinoScadaManager.Gui.ViewModels
{
    public class MasterModuleProcess : ViewModelBase, IMasterModuleProcess
    {
        private ICoreManager _manager;
        private ScadaModuleProcessViewModel _scadaProcessViewModel;

        public UserControl View { get; set; }

        public MasterModuleProcess(ICoreManager manager)
        {
            _manager = manager;

            _scadaProcessViewModel = new ScadaModuleProcessViewModel(_manager, this);
            View = new ScadaPanelView(_scadaProcessViewModel);
        }
    }
}
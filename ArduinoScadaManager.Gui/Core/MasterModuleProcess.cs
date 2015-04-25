using System.Windows.Controls;
using ArduinoScadaManager.Common.Infrastructure;
using ArduinoScadaManager.Common.Interfaces;
using ArduinoScadaManager.Gui.ViewModels.MasterModuleViewModels;
using ArduinoScadaManager.Gui.Views;

namespace ArduinoScadaManager.Gui.Core
{
    public class MasterModuleProcess : ViewModelBase, IMasterModuleProcess
    {
        private readonly ICoreManager _manager;
        private readonly MasterModuleViewModel _masterViewModel;

        public int Identifier { get; set; }
        public UserControl View { get; set; }
        public RelayCommand RemoveScadaPanelCommand { get; set; }

        public MasterModuleProcess(ICoreManager manager)
        {
            _manager = manager;

            _masterViewModel = new MasterModuleViewModel(_manager, this);
            View = new ScadaPanelView(_masterViewModel);

            RemoveScadaPanelCommand = new RelayCommand(() =>
            {
                _manager.RemoveScadaModule(this);
                Dispose();
            });

            Identifier = _manager.GenerateMasterModuleIdentifier();
        }

        public override void Dispose()
        {
            base.Dispose();
            _masterViewModel.Dispose();
        }
    }
}
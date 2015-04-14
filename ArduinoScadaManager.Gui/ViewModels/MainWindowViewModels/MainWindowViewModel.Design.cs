using System.Collections.Generic;
using System.Windows.Controls;
using ArduinoScadaManager.Common.Core;

namespace ArduinoScadaManager.Gui.ViewModels.MainWindowViewModels
{
    public partial class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            SlaveModules = new List<ISlaveModule>
            {
                new DesignSlaveModule(),
                new DesignSlaveModule(),
                new DesignSlaveModule(),
            };
        }

        class DesignSlaveModule : ISlaveModule
        {
            public UserControl GetModuleDevicePanelView()
            {
                throw new System.NotImplementedException();
            }

            public UserControl GetModuleScadaPanelView()
            {
                throw new System.NotImplementedException();
            }
        }
    }
}

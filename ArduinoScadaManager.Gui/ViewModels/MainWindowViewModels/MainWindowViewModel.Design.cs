using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ArduinoScadaManager.Common.Core;
using ArduinoScadaManager.Common.Interfaces;
using ArduinoScadaManager.Common.ViewModels;
using ArduinoScadaManager.Gui.Core;

namespace ArduinoScadaManager.Gui.ViewModels.MainWindowViewModels
{
    public sealed partial class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            ModbusTransferManager = new ModbusTransferManager(this);
            SlaveModules = new List<ISlaveModule>
            {
                new DesignSlaveModule(),
                new DesignSlaveModule(),
                new DesignSlaveModule(),
            };
        }

        class DesignSlaveModule : ISlaveModule
        {
            public string Name
            {
                get { return "Sample slave module"; }
            }

            public SlaveModuleProcessBase GetSlaveModuleProcess(ICoreManager manager)
            {
                return new DesignSlaveModuleProcess();
            }
        }

        class DesignSlaveModuleProcess : SlaveModuleProcessBase
        {
            private readonly UserControl _sampleUserControl;

            public DesignSlaveModuleProcess()
                :base(new MainWindowViewModel(), "Sample")
            {
                _sampleUserControl = new UserControl
                {
                    Width  = 300,
                    Height = 200,
                    BorderBrush = new SolidColorBrush(Colors.Black),
                    BorderThickness = new Thickness(1),
                };
            }

            protected override SlaveModuleScadaPanelViewModelBase GetScadaPanelOfSlaveModule(
                IMasterModuleProcess masterModuleProcess, 
                ISlaveModuleProcess slaveModuleProcessBase)
            {
                throw new NotImplementedException();
            }

            protected override UserControl GetDevicePanelView()
            {
                throw new NotImplementedException();
            }
        }
    }
}

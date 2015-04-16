using System.Windows.Controls;

namespace ArduinoScadaManager.Common.Core
{
    public interface ISlaveModule
    {
        IInterface1 GetInterfaceImplementation(ICoreManager manager);
    }

    public interface IInterface1
    {
        SlaveModuleDevicePanelViewModelBase DevicePanelViewModel { get; set; }
        SlaveModuleScadaPanelViewModelBase ScadaPanelViewModel { get; set; }
    }
}
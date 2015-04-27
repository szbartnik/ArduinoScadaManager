using System.Windows.Controls;
using ArduinoScadaManager.Common.Infrastructure;
using ArduinoScadaManager.Common.ViewModels;

namespace ArduinoScadaManager.Common.Interfaces
{
    public interface ISlaveModuleProcess
    {
        UserControl View { get; }
        RelayCommand RemoveSlaveModuleCommand { get; set; }
        byte Identifier { get; }
    }
}
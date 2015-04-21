using System.Security.Cryptography.X509Certificates;
using System.Windows.Controls;
using ArduinoScadaManager.Common.Infrastructure;

namespace ArduinoScadaManager.Common.Interfaces
{
    public interface IMasterModuleProcess
    {
        UserControl View { get; set; }
        RelayCommand RemoveScadaPanelCommand { get; set; }
    }
}
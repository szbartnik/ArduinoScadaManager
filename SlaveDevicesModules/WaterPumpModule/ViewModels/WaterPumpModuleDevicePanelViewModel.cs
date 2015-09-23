using System.Collections.Generic;
using ArduinoScadaManager.Common.Core;
using ArduinoScadaManager.Common.Interfaces;
using ArduinoScadaManager.Common.ViewModels;

namespace WaterPumpModule.ViewModels
{
    public class WaterPumpModuleDevicePanelViewModel : SlaveModuleDevicePanelViewModelBase
    {
        /// <summary>
        /// Design-time constructor
        /// </summary>
        public WaterPumpModuleDevicePanelViewModel()
        {
        }

        public WaterPumpModuleDevicePanelViewModel(IModbusTransferManager modbusTransferManager,
            SlaveModuleProcessBase slaveModuleProcess)
            : base(modbusTransferManager, slaveModuleProcess)
        {
            InitializeHoldingRegistersCheckboxes();
        }

        private void InitializeHoldingRegistersCheckboxes()
        {
            Checkboxes = new List<List<bool>>();
            for (int i = 0; i < 5; i++)
            {
                var list = new List<bool>();
                for (int j = 0; j < 16; j++)
                {
                    list.Add(false);
                }
                Checkboxes.Add(list);
            }
        }

        public List<List<bool>> Checkboxes { get; set; }
    }
}
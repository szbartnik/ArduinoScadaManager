using System.ComponentModel.Composition;
using ArduinoScadaManager.Common.Core;
using ArduinoScadaManager.Common.Interfaces;

namespace WaterPumpModule.Core
{
    [Export(typeof(ISlaveModule))]
    public class WaterPumpSlaveModule : ISlaveModule
    {
        public string Name
        {
            get { return "Water pump module"; }
        }

        public SlaveModuleProcessBase GetSlaveModuleProcess(ICoreManager manager)
        {
            return new WaterPumpSlaveModuleProcess(manager, Name);
        }
    }
}

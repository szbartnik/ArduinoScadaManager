using System.Collections;
using ArduinoScadaManager.Common.Core;
using ArduinoScadaManager.Common.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WaterPumpModule.ViewModels;

namespace ArduinoScadaManager.Tests
{
    [TestClass]
    public class SlaveModuleRequestActions
    {
        [TestMethod]
        public void ShouldWriteMultipleCoils()
        {
            // Assign
            var modbusTransferManager = new Mock<IModbusTransferManager>();
            modbusTransferManager.Setup(x => x.SendAsMaster(It.IsAny<ModbusTransferData>()));

            var slaveModuleProcess = new Mock<ISlaveModuleProcess>();
            slaveModuleProcess.Setup(x => x.Identifier).Returns(2);


            var scadaPanelViewModel = new WaterPumpModuleScadaPanelViewModel(
                modbusTransferManager.Object,
                new Mock<IMasterModuleProcess>().Object,
                slaveModuleProcess.Object);

            // Act
            scadaPanelViewModel.WriteMultipleCoilsRequest(234, new BitArray(new []
            {
                true, false, true, true, false, false, true, false,
                false, false, true, false, false, false, true, true,
            }));


            // Assert
            Assert.IsTrue(true);
        }
    }
}

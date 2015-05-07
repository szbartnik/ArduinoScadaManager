using System.Collections;
using System.Linq;
using ArduinoScadaManager.Common.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArduinoScadaManager.Tests
{
    [TestClass]
    public class ByteHelpersTests
    {
        [TestMethod]
        public void ShouldParseBitArrayToByteArray()
        {
            // Assign
            var bitArray = new BitArray(new[]
            {
                true, false, true, true, false, true, true, false,
                false, true, true, true, false, false, true, true,
                true, true, false, false, true, true, true, false,
            });

            // Act
            var result = bitArray.ToByteArray();

            // Assert
            Assert.IsTrue(result.SequenceEqual(new byte[] { 1 }));
        }
    }
}

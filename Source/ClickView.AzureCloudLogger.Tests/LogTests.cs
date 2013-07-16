using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClickView.AzureCloudLogger.Tests
{
    [TestClass]
    public class LogTests
    {
        [TestMethod]
        public void AzureCloudTableLogger_Fatal_Sunny_1()
        {
            //-- Arrange
            var storageName = "applicationlogs";
            var accessKey = "71/eSWyB9fk0k7Wt9GTKADsPh86WyXv6sdBjx9Kv1+TGu3y8A46X7xv4WFuXxeF8ploCN5sgHzHjF+FOej0iLw==";
            var tableName = "sandbox";

            //-- Act
            AzureCloudTableLogger logger = new AzureCloudTableLogger(storageName, accessKey, tableName);
            logger.Fatal("test message");

            //-- Assert
            Assert.IsNotNull(logger);
            Assert.IsTrue(logger.IsConnected);
        }

    }
}

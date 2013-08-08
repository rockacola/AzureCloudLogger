using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClickView.AzureCloudLogger.Tests
{
    [TestClass]
    public class LogTests
    {
        private static AzureCloudTableLogger Logger;

        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            //-- Arrange
            var storageName     = ConfigurationManager.AppSettings["Azure.Logger.Storage.Name"];
            var accessKey       = ConfigurationManager.AppSettings["Azure.Logger.Storage.AccessKey"];
            var tableName       = ConfigurationManager.AppSettings["Azure.Logger.TableRepository.Name"];
            var logLevel        = "WARN";
            var ignoreException = false;
            Logger              = new AzureCloudTableLogger(storageName, accessKey, tableName, logLevel, ignoreException);
        }

        [TestMethod]
        public void AzureCloudTableLogger_Fatal_Sunny_1()
        {
            //-- Act
            Logger.Fatal("test message");
        }

        [TestMethod]
        public void AzureCloudTableLogger_Fatal_Sunny_2()
        {
            //-- Arrange
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict["lorem"] = "hello lorem";

            //-- Act
            Logger.Fatal("test message", parameters: dict);
        }

        [TestMethod]
        public void AzureCloudTableLogger_Fatal_KeyRestrictionFail_1()
        {
            //-- Arrange
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict["lorem"] = "hello lorem";
            dict["Lorem"] = 133;
            dict["Timestamp"] = "value overwrite";

            //-- Act
            bool exceptionTriggered = false;
            try
            {
                Logger.Fatal("test message", parameters: dict);
            }
            catch (Exception ex)
            {
                exceptionTriggered = true;
            }

            //-- Assert
            Assert.IsTrue(exceptionTriggered, "Exception is expected");
        }

    }
}

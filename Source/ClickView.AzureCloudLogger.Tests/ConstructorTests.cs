using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClickView.AzureCloudLogger.Tests
{
    [TestClass]
    public class ConstructorTests
    {
        [TestMethod]
        public void AzureCloudTableLogger_Constructor_Sunny_1()
        {
            //-- Arrange
            var storageName = "applicationlogs";
            var accessKey = "71/eSWyB9fk0k7Wt9GTKADsPh86WyXv6sdBjx9Kv1+TGu3y8A46X7xv4WFuXxeF8ploCN5sgHzHjF+FOej0iLw==";
            var tableName = "sandbox";

            //-- Act
            AzureCloudTableLogger logger = new AzureCloudTableLogger(storageName, accessKey, tableName);

            //-- Assert
            Assert.IsNotNull(logger);
            Assert.IsTrue(logger.IsConnected);
            Assert.AreEqual(false, logger.IsDebugEnabled);
            Assert.AreEqual(false, logger.IsInfoEnabled);
            Assert.AreEqual(true, logger.IsWarnEnabled);
            Assert.AreEqual(true, logger.IsErrorEnabled);
            Assert.AreEqual(true, logger.IsFatalEnabled);
        }

        [TestMethod]
        public void AzureCloudTableLogger_Constructor_Sunny_2()
        {
            //-- Arrange
            var storageName = "applicationlogs";
            var accessKey = "71/eSWyB9fk0k7Wt9GTKADsPh86WyXv6sdBjx9Kv1+TGu3y8A46X7xv4WFuXxeF8ploCN5sgHzHjF+FOej0iLw==";
            var tableName = "sandbox";
            var logLevel = "DEBUG";

            //-- Act
            AzureCloudTableLogger logger = new AzureCloudTableLogger(storageName, accessKey, tableName, logLevel);

            //-- Assert
            Assert.IsNotNull(logger);
            Assert.IsTrue(logger.IsConnected);
            Assert.AreEqual(true, logger.IsDebugEnabled);
            Assert.AreEqual(true, logger.IsInfoEnabled);
            Assert.AreEqual(true, logger.IsWarnEnabled);
            Assert.AreEqual(true, logger.IsErrorEnabled);
            Assert.AreEqual(true, logger.IsFatalEnabled);
        }

        /***travo20130614: Constructor deprecated
        [TestMethod]
        public void AzureCloudTableLogger_Constructor_Sunny_3()
        {
            //-- Arrange
            var storageName = "applicationlogs";
            var accessKey = "71/eSWyB9fk0k7Wt9GTKADsPh86WyXv6sdBjx9Kv1+TGu3y8A46X7xv4WFuXxeF8ploCN5sgHzHjF+FOej0iLw==";
            var tableName = "sandbox";
            var logLevel = LogLevel.ERROR;

            //-- Act
            AzureCloudTableLogger logger = new AzureCloudTableLogger(storageName, accessKey, tableName, logLevel);

            //-- Assert
            Assert.IsNotNull(logger);
            Assert.IsTrue(logger.IsConnected);
            Assert.AreEqual(false, logger.IsDebugEnabled);
            Assert.AreEqual(false, logger.IsInfoEnabled);
            Assert.AreEqual(false, logger.IsWarnEnabled);
            Assert.AreEqual(true, logger.IsErrorEnabled);
            Assert.AreEqual(true, logger.IsFatalEnabled);
        }
        */

        /***travo20130614: Constructor deprecated
        [TestMethod]
        public void AzureCloudTableLogger_Constructor_BadStorageName_1()
        {
            //-- Arrange
            var storageName = "BAD NAME";
            var accessKey = "71/eSWyB9fk0k7Wt9GTKADsPh86WyXv6sdBjx9Kv1+TGu3y8A46X7xv4WFuXxeF8ploCN5sgHzHjF+FOej0iLw==";
            var tableName = "sandbox";
            var logLevel = LogLevel.NONE;

            //-- Act
            AzureCloudTableLogger logger = new AzureCloudTableLogger(storageName, accessKey, tableName, logLevel);

            //-- Assert
            Assert.IsNotNull(logger);
            Assert.IsFalse(logger.IsConnected);
        }
        */

        /***travo20130614: Constructor deprecated
        [TestMethod]
        public void AzureCloudTableLogger_Constructor_BadStorageName_2()
        {
            //-- Arrange
            var storageName = "applicationlogs";
            var accessKey = "71/eSWyB9fk0k7Wt9GTKADsPh86WyXv6sdBjx9Kv1+TGu3y8A46X7xv4WFuXxeF8ploCN5sgHzHjF+FOej0iLw==";
            var tableName = "bad.table.name";
            var logLevel = LogLevel.NONE;

            //-- Act
            AzureCloudTableLogger logger = new AzureCloudTableLogger(storageName, accessKey, tableName, logLevel);

            //-- Assert
            Assert.IsNotNull(logger);
            Assert.IsFalse(logger.IsConnected);
        }
        */

    }
}

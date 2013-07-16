using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ClickView.AzureCloudLogger.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;

namespace ClickView.AzureCloudLogger
{
    /// <summary>
    /// Logger by writing into Azure table storage.
    /// </summary>
    public class AzureCloudTableLogger : IAzureCloudTableLogger
    {
        //-- Private Properties
        private string StorageName          { get; set; }
        private string StorageAccessKey     { get; set; }
        private string TableRepositoryName  { get; set; }
        private LogLevel Level              { get; set; }
        private CloudTable LogTable         { get; set; }

        //-- Public Properties
        private bool _isConnected { get; set; }
        public bool IsConnected
        {
            get
            {
                return _isConnected;
            }
        }

        private bool _ignoreException { get; set; }
        public bool IgnoreException
        {
            get
            {
                return _ignoreException;
            }
        }

        //-- Calculated Properties
        public bool IsDebugEnabled
        {
            get
            {
                //-- Arrange
                bool output = false;

                //-- Act
                if (Level == LogLevel.ALL)
                    output = true;
                else if (Level == LogLevel.DEBUG)
                    output = true;

                //-- Act
                return output;
            }
        }
        public bool IsInfoEnabled
        {
            get
            {
                //-- Arrange
                bool output = false;

                //-- Act
                if (Level == LogLevel.ALL)
                    output = true;
                else if (Level == LogLevel.INFO)
                    output = true;
                else if (Level == LogLevel.DEBUG)
                    output = true;

                //-- Act
                return output;
            }
        }
        public bool IsWarnEnabled
        {
            get
            {
                //-- Arrange
                bool output = false;

                //-- Act
                if (Level == LogLevel.ALL)
                    output = true;
                else if (Level == LogLevel.WARN)
                    output = true;
                else if (Level == LogLevel.INFO)
                    output = true;
                else if (Level == LogLevel.DEBUG)
                    output = true;

                //-- Act
                return output;
            }
        }
        public bool IsErrorEnabled
        {
            get
            {
                //-- Arrange
                bool output = false;

                //-- Act
                if (Level == LogLevel.ALL)
                    output = true;
                else if (Level == LogLevel.ERROR)
                    output = true;
                else if (Level == LogLevel.WARN)
                    output = true;
                else if (Level == LogLevel.INFO)
                    output = true;
                else if (Level == LogLevel.DEBUG)
                    output = true;

                //-- Act
                return output;
            }
        }
        public bool IsFatalEnabled
        {
            get
            {
                //-- Arrange
                bool output = false;

                //-- Act
                if (Level == LogLevel.ALL)
                    output = true;
                else if (Level == LogLevel.FETAL)
                    output = true;
                else if (Level == LogLevel.ERROR)
                    output = true;
                else if (Level == LogLevel.WARN)
                    output = true;
                else if (Level == LogLevel.INFO)
                    output = true;
                else if (Level == LogLevel.DEBUG)
                    output = true;

                //-- Act
                return output;
            }
        }

        //-- Constructors

        public AzureCloudTableLogger(string storageName, string storageAccessKey, string tableRepositoryName, string levelLabel = null, bool ignoreException = true)
        {
            //-- Assign IgnoreException
            _ignoreException = ignoreException;

            //-- Parse Level Enum
            LogLevel level = LogLevel.WARN;
            try
            {
                level = (LogLevel)Enum.Parse(typeof(LogLevel), levelLabel.ToUpper());
            }
            catch (Exception ex)
            {
                if (!_ignoreException)
                {
                    throw new ArgumentException("Unknown LogLevel option.", "levelLabel", ex);
                }
            }

            //-- Act
            Initialize(storageName, storageAccessKey, tableRepositoryName, level);
            Connect();
        }

        private void Initialize(string storageName, string storageAccessKey, string tableRepositoryName, LogLevel level)
        {
            //-- Assign
            StorageName = storageName;
            StorageAccessKey = storageAccessKey;
            TableRepositoryName = tableRepositoryName;
            Level = level;

            //-- Validate
            if (!_ignoreException)
            {
                ValidateStorageName();
                ValidateStorageAccessKey();
                ValidateTableRepositoryname();
            }
        }

        private void Connect()
        {
            try
            {
                //-- Arrange
                var storageCredential = new StorageCredentials(StorageName, StorageAccessKey);
                var storageAccount = new CloudStorageAccount(storageCredential, true);
                var tableClient = storageAccount.CreateCloudTableClient();
                //-- Act
                LogTable = tableClient.GetTableReference(TableRepositoryName);
                LogTable.CreateIfNotExists();
                _isConnected = true;
            }
            catch (Exception ex)
            {
                _isConnected = false;
                if (!_ignoreException)
                    throw new Exception("Failed to connect to Azure table storage.", ex);
            }
        }

        //-- Public Methods

        public void Debug(string message, Exception t = null, string sender = null)
        {
            //-- Explore Machine Identity
            var machineName = FetchMachineName();

            //-- Explore Sender Identity
            if (string.IsNullOrWhiteSpace(sender))
            {
                try
                {
                    StackTrace stackTrace = new StackTrace();
                    MethodBase methodBase = stackTrace.GetFrame(1).GetMethod();
                    sender = string.Format("{0}.{1}", methodBase.ReflectedType.FullName, methodBase.Name);
                }
                catch (Exception ex)
                {
                    sender = "Unknown";
                    if (!_ignoreException)
                        throw new Exception("Failed to identify Sender through StackTrace.", ex);
                }
            }

            //-- Reroute Method
            Log(LogLevel.DEBUG, machineName, sender, message, t);
        }

        public void Info(string message, Exception t = null, string sender = null)
        {
            //-- Explore Machine Identity
            var machineName = FetchMachineName();

            //-- Explore Sender Identity
            if (string.IsNullOrWhiteSpace(sender))
            {
                try
                {
                    StackTrace stackTrace = new StackTrace();
                    MethodBase methodBase = stackTrace.GetFrame(1).GetMethod();
                    sender = string.Format("{0}.{1}", methodBase.ReflectedType.FullName, methodBase.Name);
                }
                catch (Exception ex)
                {
                    sender = "Unknown";
                    if (!_ignoreException)
                        throw new Exception("Failed to identify Sender through StackTrace.", ex);
                }
            }

            //-- Reroute Method
            Log(LogLevel.INFO, machineName, sender, message, t);
        }

        public void Warn(string message, Exception t = null, string sender = null)
        {
            //-- Explore Machine Identity
            var machineName = FetchMachineName();

            //-- Explore Sender Identity
            if (string.IsNullOrWhiteSpace(sender))
            {
                try
                {
                    StackTrace stackTrace = new StackTrace();
                    MethodBase methodBase = stackTrace.GetFrame(1).GetMethod();
                    sender = string.Format("{0}.{1}", methodBase.ReflectedType.FullName, methodBase.Name);
                }
                catch (Exception ex)
                {
                    sender = "Unknown";
                    if (!_ignoreException)
                        throw new Exception("Failed to identify Sender through StackTrace.", ex);
                }
            }

            //-- Reroute Method
            Log(LogLevel.WARN, machineName, sender, message, t);
        }

        public void Error(string message, Exception t = null, string sender = null)
        {
            //-- Explore Machine Identity
            var machineName = FetchMachineName();

            //-- Explore Sender Identity
            if (string.IsNullOrWhiteSpace(sender))
            {
                try
                {
                    StackTrace stackTrace = new StackTrace();
                    MethodBase methodBase = stackTrace.GetFrame(1).GetMethod();
                    sender = string.Format("{0}.{1}", methodBase.ReflectedType.FullName, methodBase.Name);
                }
                catch (Exception ex)
                {
                    sender = "Unknown";
                    if (!_ignoreException)
                        throw new Exception("Failed to identify Sender through StackTrace.", ex);
                }
            }

            //-- Reroute Method
            Log(LogLevel.ERROR, machineName, sender, message, t);
        }

        public void Fatal(string message, Exception t = null, string sender = null)
        {
            //-- Explore Machine Identity
            var machineName = FetchMachineName();

            //-- Explore Sender Identity
            if (string.IsNullOrWhiteSpace(sender))
            {
                try
                {
                    StackTrace stackTrace = new StackTrace();
                    MethodBase methodBase = stackTrace.GetFrame(1).GetMethod();
                    sender = string.Format("{0}.{1}", methodBase.ReflectedType.FullName, methodBase.Name);
                }
                catch (Exception ex)
                {
                    sender = "Unknown";
                    if (!_ignoreException)
                        throw new Exception("Failed to identify Sender through StackTrace.", ex);
                }
            }

            //-- Reroute Method
            Log(LogLevel.FETAL, machineName, sender, message, t);
        }

        //-- Private Methods

        private void ValidateStorageName()
        {
            if (StorageName.Length < 3 ||
                StorageName.Length > 63 ||
                !Regex.IsMatch(StorageName, @"^[a-z0-9]+(-[a-z0-9]+)*$"))
            {
                throw new ArgumentException("Invalid storage name", "StorageName");
            }
        }

        private void ValidateStorageAccessKey()
        {
            if (!Regex.IsMatch(StorageName, @"^(?:[A-Za-z0-9+/]{4})*(?:[A-Za-z0-9+/]{2}==|[A-Za-z0-9+/]{3}=)?$"))
                throw new ArgumentException("Invalid table name", "TableRepositoryName");
        }

        private void ValidateTableRepositoryname()
        {
            if (!Regex.IsMatch(TableRepositoryName, @"^[A-Za-z][A-Za-z0-9]{2,62}$"))
                throw new ArgumentException("Invalid table name", "TableRepositoryName");
        }

        private string FetchMachineName()
        {
            //-- Arrange
            var machineName = "Unknown";

            //-- Act
            try
            {
                machineName = System.Environment.MachineName;
            }
            catch (Exception ex)
            {
                if (!_ignoreException)
                    throw new Exception("Failed to fetch Environment.MachineName.", ex);
            }

            //-- Output
            return machineName;
        }

        private void Log(LogLevel level, string machineName, string sender, object message, Exception t = null)
        {
            //-- Determine to Log or to Ignore
            if (ToIgnore(level))
                return;

            //-- Validate
            if (!_isConnected)
            {
                if (!_ignoreException)
                    throw new Exception("Connection to Azure table storage was not established.");
                else
                    return;
            }

            //-- Act
            try
            {
                LogMessage logMessage = new LogMessage(level, machineName, sender, message, t);
                TableOperation insertOperation = TableOperation.Insert(logMessage);
                LogTable.Execute(insertOperation);
            }
            catch (Exception ex)
            {
                if (!_ignoreException)
                    throw new Exception("Failed to log message into Azure table storage.", ex);
            }
        }

        private bool ToIgnore(LogLevel logLevel)
        {
            //-- Arrange
            bool output = false;
            //-- Act
            if (Level == LogLevel.ALL)
                output = false;
            else if (Level == LogLevel.NONE)
                output = true;
            else if (Level == LogLevel.DEBUG)
                output = false;
            else if (Level == LogLevel.INFO)
            {
                if (logLevel == LogLevel.DEBUG)
                    output = true;
            }
            else if (Level == LogLevel.WARN)
            {
                if (logLevel == LogLevel.DEBUG)
                    output = true;
                else if (logLevel == LogLevel.INFO)
                    output = true;
            }
            else if (Level == LogLevel.ERROR)
            {
                if (logLevel == LogLevel.DEBUG)
                    output = true;
                else if (logLevel == LogLevel.INFO)
                    output = true;
                else if (logLevel == LogLevel.WARN)
                    output = true;
            }
            else if (Level == LogLevel.FETAL)
            {
                if (logLevel == LogLevel.DEBUG)
                    output = true;
                else if (logLevel == LogLevel.INFO)
                    output = true;
                else if (logLevel == LogLevel.WARN)
                    output = true;
                else if (logLevel == LogLevel.ERROR)
                    output = true;
            }
            //-- Output
            return output;
        }

    }
}

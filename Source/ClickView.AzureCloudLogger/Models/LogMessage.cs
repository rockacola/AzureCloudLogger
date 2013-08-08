using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ClickView.AzureCloudLogger.Enumerators;
using Microsoft.WindowsAzure.Storage.Table;

namespace ClickView.AzureCloudLogger.Models
{
    public class LogMessage : ElasticTableEntity
    {
        private const string DateFormat     = "yyyy-MM-dd HH:mm:ss.fff";
        private const string RowKeyFormat   = "{0} - {1}";

        public LogMessage()
        {
        }

        public LogMessage(LogLevel level, string machineName, string sender, object message, Exception exception = null)
        {
            //-- Base Properties
            PartitionKey    = level.ToString("G");
            var dateLabel   = DateTime.UtcNow.ToString(DateFormat);
            RowKey          = string.Format("{0} - {1}", dateLabel, Guid.NewGuid().ToString());
            
            //-- Act
            Version version = Assembly.GetAssembly(this.GetType()).GetName().Version;
            this[ReservedLogProperty.LoggerVersion.ToString()]  = version.ToString();
            this[ReservedLogProperty.MachineName.ToString()]    = machineName;
            this[ReservedLogProperty.Sender.ToString()]         = sender;
            this[ReservedLogProperty.Message.ToString()]        = message.ToString();
            if (exception != null)
            {
                this[ReservedLogProperty.ExceptionMessage.ToString()]   = exception.Message;
                this[ReservedLogProperty.ExceptionTraceLog.ToString()]  = BuildTraceLog(exception);
            }
        }

        public void AddProperties(Dictionary<string, object> parameters)
        {
            //-- Validate
            if (parameters == null)
                return;

            //-- Act
            foreach (KeyValuePair<string, object> entry in parameters)
                this[entry.Key] = entry.Value;
        }

        private string BuildTraceLog(Exception exception)
        {
            //-- Arrange
            string output = string.Empty;
            //-- Validate
            if (exception == null)
                return output;
            //-- Act
            output = string.Format("[{0}: {1}]", exception.GetType().Name, exception.Message);
            output += System.Environment.NewLine;
            output += System.Environment.NewLine;
            output += exception.StackTrace;
            output += System.Environment.NewLine;
            output += System.Environment.NewLine;
            //-- Recursive
            return BuildTraceLog(exception.InnerException) + output;
        }

    }
}

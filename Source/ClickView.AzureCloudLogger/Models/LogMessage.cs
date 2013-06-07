using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace ClickView.AzureCloudLogger.Models
{
    public class LogMessage : TableEntity
    {
        private const string MessagePartionKey = "LogEntry";
        private const string DateFormat = "yyyy-MM-dd HH:mm:ss";
        private const string RowKeyFormat = "{0} - {1}";

        public string MachineName           { get; set; }
        public string Sender                { get; set; }
        public string Message               { get; set; }
        public string ExceptionMessage      { get; set; }
        public string ExceptionTraceLog     { get; set; }

        public LogMessage()
        {
        }

        public LogMessage(LogLevel level, string machineName, string sender, object message, Exception exception = null)
        {
            //-- Base Properties
            PartitionKey = level.ToString("G");
            var dateLabel = DateTime.Now.ToUniversalTime().ToString(DateFormat);
            RowKey = string.Format("{0} - {1}", dateLabel, Guid.NewGuid().ToString());

            //-- Act
            MachineName = machineName;
            Sender = sender;
            Message = message.ToString();
            if (exception != null)
            {
                ExceptionMessage = exception.Message;
                ExceptionTraceLog = BuildTraceLog(exception);
            }
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

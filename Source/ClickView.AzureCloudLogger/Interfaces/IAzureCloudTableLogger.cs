using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClickView.AzureCloudLogger.Models;

namespace ClickView.AzureCloudLogger
{
    public interface IAzureCloudTableLogger
    {
        //REF: Interface as referenced from Log4net: http://logging.apache.org/log4net/release/manual/introduction.html
        /* Test if a level is enabled for logging */
        bool IsDebugEnabled { get; }
        bool IsInfoEnabled { get; }
        bool IsWarnEnabled { get; }
        bool IsErrorEnabled { get; }
        bool IsFatalEnabled { get; }

        /* Log a message object with optional exception and sender */
        void Debug(string message, Exception t = null, string sender = null);
        void Info(string message, Exception t = null, string sender = null);
        void Warn(string message, Exception t = null, string sender = null);
        void Error(string message, Exception t = null, string sender = null);
        void Fatal(string message, Exception t = null, string sender = null);

    }
}

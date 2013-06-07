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

        /* Log a message object */
        /*
        void Debug(object message);
        void Info(object message);
        void Warn(object message);
        void Error(object message);
        void Fatal(object message);
        */

        /* Log a message object and exception */
        void Debug(object message, Exception t = null);
        void Info(object message, Exception t = null);
        void Warn(object message, Exception t = null);
        void Error(object message, Exception t = null);
        void Fatal(object message, Exception t = null);

        /* Log a message string using the System.String.Format syntax */
        /*
        void DebugFormat(string format, params object[] args);
        void InfoFormat(string format, params object[] args);
        void WarnFormat(string format, params object[] args);
        void ErrorFormat(string format, params object[] args);
        void FatalFormat(string format, params object[] args);
        */

        /* Log a message string using the System.String.Format syntax */
        /*
        void DebugFormat(IFormatProvider provider, string format, params object[] args);
        void InfoFormat(IFormatProvider provider, string format, params object[] args);
        void WarnFormat(IFormatProvider provider, string format, params object[] args);
        void ErrorFormat(IFormatProvider provider, string format, params object[] args);
        void FatalFormat(IFormatProvider provider, string format, params object[] args);
        */

        //TEST METHODS
        //List<LogMessage> ListErrors();

    }
}

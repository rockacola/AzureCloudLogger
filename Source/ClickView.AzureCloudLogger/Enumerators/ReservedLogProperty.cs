using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClickView.AzureCloudLogger.Enumerators
{
    public enum ReservedLogProperty
    {
        PartitionKey,
        RowKey,
        Timestamp,
        LoggerVersion,
        MachineName,
        Sender,
        Message,
        ExceptionMessage,
        ExceptionTraceLog
    }
}

using Serilog;
using System;
using System.Collections.Generic;

namespace Advertising.Logging
{
    public static class LogHelper
    {
        public static void AddInfo(string message)
        {
            Log.Information(message);
        }

        public static void AddWarning(string correlationId, string message, string internalMessage, List<string> errors = null)
        {
            var logMessage = new LogMessage
            {
                CorrelationId = correlationId,
                Message = message,
                InternalMessage = internalMessage,
                Errors = errors
            };

            Log.Warning("{@logMessage}", logMessage);
        }

        public static void AddError(string correlationId, Exception exception)
        {
            var logMessage = new LogMessage
            {
                CorrelationId = correlationId,
                Message = exception.Message
            };

            Log.Error(exception, "{@logMessage}", logMessage);
        }
    }
}
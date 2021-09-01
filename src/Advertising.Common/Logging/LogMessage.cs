using System;
using System.Collections.Generic;

namespace Advertising.Logging
{
    [Serializable]
    public class LogMessage
    {
        public string CorrelationId { get; set; }
        public string Message { get; set; }
        public string InternalMessage { get; set; }
        public List<string> Errors { get; set; }
    }
}
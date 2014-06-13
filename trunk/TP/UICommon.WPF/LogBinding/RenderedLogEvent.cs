using System;
using NLog;

namespace UICommon.WPF.LogBinding
{
    public class RenderedLogEvent
    {
        public RenderedLogEvent(LogEventInfo logEvent, string message)
        {
            if (logEvent == null) throw new ArgumentNullException("logEvent");
            LogEvent = logEvent;
            Message = message;
        }
        public LogEventInfo LogEvent { get; private set; }
        public string Message { get; set; }
        public LogLevel Level { get { return LogEvent.Level; } }

        public override string ToString()
        {
            return Message ?? string.Empty;
        }
    }
}

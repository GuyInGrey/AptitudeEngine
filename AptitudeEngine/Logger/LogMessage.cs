using System;
using System.Diagnostics;

namespace AptitudeEngine.Logger
{
    public class LogMessage
    {
        public readonly object Content;
        public readonly LogMessageType MessageType;
        public readonly LogMessageSource MessageSource;
        public readonly DateTime SentDateTime;
        public readonly StackTrace StackTrace;
        public readonly StackFrame StackFrame;

        public LogMessage(object content, LogMessageType type, LogMessageSource source, DateTime timeSent, StackTrace st, StackFrame sf)
        {
            Content = content;
            MessageType = type;
            MessageSource = source;
            SentDateTime = timeSent;
            StackTrace = st;
            StackFrame = sf;
        }
    }
}
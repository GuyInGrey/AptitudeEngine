using System;

namespace AptitudeEngine.Logging.Modules
{
    public abstract class LoggerModule
    {
        public abstract string Name { get; }

        public virtual void BeforeLog()
        {
        }

        public virtual void AfterLog(LogMessage logMessage)
        {
        }

        public virtual void ExceptionLog(Exception exception)
        {
        }

        public virtual void Initialize()
        {
        }
    }
}
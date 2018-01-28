namespace AptitudeEngine.Logging
{
    public interface ILoggerHandler
    {
        void Publish(LogMessage logMessage);
    }
}
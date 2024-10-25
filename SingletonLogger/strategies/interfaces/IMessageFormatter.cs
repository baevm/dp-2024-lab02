namespace SingletonLogger;

public interface IMessageFormatter
{
    string FormatMessage(string date, LogLevel level, string message);
}
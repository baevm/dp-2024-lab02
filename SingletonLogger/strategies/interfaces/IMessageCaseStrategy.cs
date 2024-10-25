namespace SingletonLogger;

public interface IFormatMessageStrategy
{
    string FormatMessage(string date, LogLevel level, string message);
}
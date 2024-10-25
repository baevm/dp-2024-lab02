namespace SingletonLogger;

public class LowercaseMessageStrategy : IFormatMessageStrategy
{
    public string FormatMessage(string date, LogLevel level, string message)
    {
        return $"{date} [{level}] {message}";
    }
}
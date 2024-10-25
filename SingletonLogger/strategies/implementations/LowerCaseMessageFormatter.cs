namespace SingletonLogger;

public class LowerCaseMessageFormatter : IMessageFormatter
{
    public string FormatMessage(string date, LogLevel level, string message)
    {
        return $"{date} [{level}] {message}";
    }
}
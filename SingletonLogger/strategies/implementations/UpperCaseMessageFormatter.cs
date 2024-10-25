namespace SingletonLogger;


public class UppercaseMessageFormatter : IMessageFormatter
{
    public string FormatMessage(string date, LogLevel level, string message)
    {
        return $"{date} [{level}] {message.ToUpper()}";
    }
}
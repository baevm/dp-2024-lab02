namespace SingletonLogger;


public class UppercaseMessageStrategy : IFormatMessageStrategy
{
    public string FormatMessage(string date, LogLevel level, string message)
    {
        return $"{date} [{level}] {message.ToUpper()}";
    }
}
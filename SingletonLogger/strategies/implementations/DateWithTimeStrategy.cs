
namespace SingletonLogger;

public class DateWithTimeFormat : IDateFormatter
{
    public string FormatDate()
    {
        return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }
}
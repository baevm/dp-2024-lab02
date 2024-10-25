namespace SingletonLogger;
public sealed class Logger
{
    private static readonly Mutex _RMutex = new Mutex();
    private static readonly Mutex _WMutex = new Mutex();

    private static StreamWriter _streamWriter = new StreamWriter(Console.OpenStandardOutput());
    public static StreamWriter StreamWriter
    {
        get
        {
            _RMutex.WaitOne();
            try
            {
                return _streamWriter;

            }
            finally
            {
                _RMutex.ReleaseMutex();
            }
        }
        set
        {
            _WMutex.WaitOne();
            _streamWriter = value;
            _WMutex.ReleaseMutex();
        }
    }

    private static IFormatMessageStrategy _messageFormatter = new LowercaseMessageStrategy();
    public static IFormatMessageStrategy MessageFormatter
    {
        get
        {
            _RMutex.WaitOne(); ;
            try
            {
                return _messageFormatter;

            }
            finally
            {
                _RMutex.ReleaseMutex();
            }
        }
        set
        {
            _WMutex.WaitOne();
            _messageFormatter = value;
            _WMutex.ReleaseMutex();
        }
    }

    private static IFormatDateStrategy _dateFormatter = new DateWithTimeStrategy();
    public static IFormatDateStrategy DateFormatter
    {
        get
        {
            _RMutex.WaitOne(); ;

            try
            {
                return _dateFormatter;

            }
            finally
            {
                _RMutex.ReleaseMutex();

            }
        }
        set
        {
            _WMutex.WaitOne();
            _dateFormatter = value;
            _WMutex.ReleaseMutex();
        }
    }


    // вызывается при загрузке класса в память
    private static readonly Logger _instance = new Logger();
    public static Logger Instance
    {
        get
        {
            return _instance;
        }
    }

    private Logger() { }


    private void LogWithLevel(LogLevel level, string message)
    {
        _WMutex.WaitOne();

        var date = DateFormatter.GetDate();
        var logMessage = MessageFormatter.FormatMessage(date, level, message);

        StreamWriter.WriteLine(logMessage);
        StreamWriter.Flush();

        _WMutex.ReleaseMutex();
    }


    public void LogTrace(string message)
    {
        LogWithLevel(LogLevel.TRACE, message);
    }

    public void LogInfo(string message)
    {
        LogWithLevel(LogLevel.INFO, message);
    }

    public void LogWarn(string message)
    {
        LogWithLevel(LogLevel.WARN, message);
    }

    public void LogError(string message)
    {
        LogWithLevel(LogLevel.ERROR, message);
    }

    public void LogFatal(string message)
    {
        LogWithLevel(LogLevel.FATAL, message);
    }
}

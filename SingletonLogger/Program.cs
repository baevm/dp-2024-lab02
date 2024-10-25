
using SingletonLogger;

internal class Program
{
    static void Main(string[] args)
    {
        LogWithThreadingToConsole();
    }

    static void LogToConsole()
    {
        var logger = Logger.Instance;
        logger.LogInfo("hello world");
    }

    static void LogToFile()
    {
        var fileName = $"DP.P1.{DateTime.Now.ToString("yyyy-MM-dd.HH-mm-ss")}.log";
        var logFile = new StreamWriter(fileName);

        Logger.StreamWriter = logFile;

        var logger = Logger.Instance;
        logger.LogInfo("hello world");
    }

    static void LogToFileWithUppercase()
    {
        var fileName = $"DP.P1.{DateTime.Now.ToString("yyyy-MM-dd.HH-mm-ss")}.log";
        var logFileStrategy = new StreamWriter(fileName);
        var uppercaseLogStrategy = new UppercaseMessageFormatter();

        Logger.StreamWriter = logFileStrategy;
        Logger.MessageFormatter = uppercaseLogStrategy;

        var logger = Logger.Instance;
        logger.LogInfo("hello world");
    }

    static void LogWithThreadingToConsole()
    {
        var numOfThreads = 5;
        var countdown = new CountdownEvent(numOfThreads);
        for (int i = 0; i < numOfThreads; i++)
        {
            new Thread(delegate ()
            {
                var logger = Logger.Instance;
                logger.LogTrace($"trace log from thread {Thread.CurrentThread.ManagedThreadId}");
                countdown.Signal();
            }).Start();
        }
        countdown.Wait();
        Logger.Instance.LogInfo("Finished");
    }
}
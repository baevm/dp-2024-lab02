
using System.Text;

namespace SingletonLogger.Tests;

public class LoggerTest
{
    [Fact]
    public void Test_singleton_same_instance()
    {
        var instance1 = Logger.Instance;
        var instance2 = Logger.Instance;

        Assert.True(instance1.Equals(instance2));
    }

    [Fact]
    public void Test_Logger_LogInfo()
    {
        /* mocks */
        var mockStringWriter = new MemoryStream();
        var streamWriter = new StreamWriter(mockStringWriter);

        Logger.StreamWriter = streamWriter;

        var expected = "Hello world!";
        var instance1 = Logger.Instance;

        instance1.LogInfo(expected);

        var actual = Encoding.UTF8.GetString(mockStringWriter.ToArray());

        Assert.Contains(expected, actual);
    }

    [Fact]
    public void Test_Logger_Switch_MessageCaseStrategy_In_Runtime()
    {
        /* mocks */
        var mockStringWriter = new MemoryStream();
        var streamWriter = new StreamWriter(mockStringWriter);

        #region Запись lowercase
        Logger.StreamWriter = streamWriter;

        var message = "hello world";
        var logger = Logger.Instance;

        logger.LogInfo(message);

        var actual = Encoding.UTF8.GetString(mockStringWriter.ToArray());

        Assert.Contains(message, actual);
        mockStringWriter.Position = 0;
        mockStringWriter.SetLength(0);
        #endregion

        #region Запись uppercase
        Logger.MessageFormatter = new UppercaseMessageFormatter();

        logger.LogInfo(message);

        var actualUpperCase = Encoding.UTF8.GetString(mockStringWriter.ToArray());
        Assert.Contains(message.ToUpper(), actualUpperCase);
        #endregion
    }

    [Fact]
    public void Test_Logger_Thread_Safe()
    {
        /* mocks */
        var mockStringWriter = new MemoryStream();
        var streamWriter = new StreamWriter(mockStringWriter);

        Logger.StreamWriter = streamWriter;

        var numOfThreads = 5;
        var countdown = new CountdownEvent(numOfThreads);

        for (int i = 0; i < numOfThreads; i++)
        {
            new Thread(delegate ()
            {
                var instance1 = Logger.Instance;
                instance1.LogInfo($"thread {Thread.CurrentThread.ManagedThreadId}");
                countdown.Signal();
            }).Start();
        }

        countdown.Wait();

        var actual = Encoding.UTF8.GetString(mockStringWriter.ToArray()).Split('\n');

        // 5 строк + 1 пустая
        Assert.Equal(numOfThreads + 1, actual.Length);
    }
}
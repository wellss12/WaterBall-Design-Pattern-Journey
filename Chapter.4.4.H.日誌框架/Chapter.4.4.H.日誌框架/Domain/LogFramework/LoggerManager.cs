namespace Chapter._4._4.H.日誌框架.Domain.LogFramework;

public static class LoggerManager
{
    private static readonly Dictionary<string, Logger> LoggerMap = new();

    public static void DeclareLoggers(params Logger[] loggers)
    {
        foreach (var logger in loggers)
        {
            LoggerMap.Add(logger.Name, logger);
        }
    }

    public static Logger GetLogger(string name) => LoggerMap[name];
}
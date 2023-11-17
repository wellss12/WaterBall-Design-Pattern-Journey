namespace Chapter._4._4.H.日誌框架.Domain.LogFramework.Layouts;

public class StandardLayout : ILayout
{
    public string Format(LevelThreshold level, string loggerName, string message)
    {
        var createdTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        return $"{createdTime} |-{level.ToString()} {loggerName} - {message}";
    }
}
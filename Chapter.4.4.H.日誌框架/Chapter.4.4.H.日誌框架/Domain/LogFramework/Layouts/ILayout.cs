namespace Chapter._4._4.H.日誌框架.Domain.LogFramework.Layouts;

public interface ILayout
{
    public string Format(LevelThreshold level, string loggerName, string message);
}
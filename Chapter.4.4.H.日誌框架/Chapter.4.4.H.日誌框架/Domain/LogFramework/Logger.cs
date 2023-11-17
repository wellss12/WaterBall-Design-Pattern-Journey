using Chapter._4._4.H.日誌框架.Domain.LogFramework.Exporters;
using Chapter._4._4.H.日誌框架.Domain.LogFramework.Layouts;

namespace Chapter._4._4.H.日誌框架.Domain.LogFramework;

public class Logger
{
    public Logger(string name, Logger parent) : this(parent.LevelThreshold, parent.Exporter, parent.Layout)
    {
        Name = name;
        Parent = parent;
    }

    private Logger(LevelThreshold levelThreshold, Exporter exporter, ILayout layout)
    {
        LevelThreshold = levelThreshold;
        Exporter = exporter;
        Layout = layout;
    }

    public static Logger Root(LevelThreshold levelThreshold, Exporter exporter, ILayout layout)
    {
        return new Logger(levelThreshold, exporter, layout)
        {
            Name = "Root"
        };
    }

    public Logger? Parent { get; }
    public string Name { get; private init; } = null!;
    public LevelThreshold LevelThreshold { get; init; }
    public Exporter Exporter { get; init; }
    public ILayout Layout { get; init; }

    public void Trace(string message) => Log(message, LevelThreshold.TRACE);

    public void Info(string message) => Log(message, LevelThreshold.INFO);

    public void Debug(string message) => Log(message, LevelThreshold.DEBUG);

    public void Warn(string message) => Log(message, LevelThreshold.WARN);

    public void Error(string message) => Log(message, LevelThreshold.ERROR);

    private void Log(string message, LevelThreshold targetLevel)
    {
        if (LevelThreshold > targetLevel)
        {
            return;
        }

        var formatMessage = Layout.Format(targetLevel, Name, message);
        Exporter.Export(formatMessage);
    }
}
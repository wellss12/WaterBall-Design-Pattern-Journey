using System.Text.Json;
using System.Text.Json.Serialization;
using Chapter._4._4.H.日誌框架.Domain.LogFramework.Exporters;
using Chapter._4._4.H.日誌框架.Domain.LogFramework.Layouts;
using Chapter._4._4.H.日誌框架.Json;

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

    public static void AddLoggerFrom(string jsonFile)
    {
        using var reader = new StreamReader(jsonFile);
        var json = reader.ReadToEnd();
        var jsonSerializerOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            Converters =
            {
                new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
            }
        };

        var loggerRoot = JsonSerializer.Deserialize<LoggerRoot>(json, jsonSerializerOptions);
        var loggerConfig = loggerRoot!.Loggers;
        var root = CreateRoot(loggerConfig);
        var children = CreateChildren(loggerConfig.Children, root).ToArray();
        var allLoggers = new List<Logger> {root};
        allLoggers.AddRange(children);
        DeclareLoggers(allLoggers.ToArray());
    }

    private static Logger CreateRoot(LoggerConfig loggerConfig)
    {
        if (loggerConfig.LevelThreshold is null || loggerConfig.Exporter is null || loggerConfig.Layout is null)
        {
            throw new ArgumentException("Root logger must have LevelThreshold, Exporter and Layout");
        }

        var exporter = CreateExporterType(loggerConfig.Exporter);
        var layout = CreateLayout(loggerConfig.Layout.Value);
        return Logger.Root(loggerConfig.LevelThreshold.Value, exporter, layout);
    }

    private static List<Logger> CreateChildren(Dictionary<string, LoggerConfig> children, Logger parent)
    {
        var loggers = new List<Logger>();
        foreach (var (name, loggerConfig) in children)
        {
            var logger = new Logger(name, parent);
            if (loggerConfig.LevelThreshold.HasValue)
            {
                logger.LevelThreshold = loggerConfig.LevelThreshold.Value;
            }

            if (loggerConfig.Exporter is not null)
            {
                logger.Exporter = CreateExporterType(loggerConfig.Exporter);
            }

            if (loggerConfig.Layout.HasValue)
            {
                logger.Layout = CreateLayout(loggerConfig.Layout.Value);
            }

            loggers.Add(logger);
            if (loggerConfig.Children.Any())
            {
                var childrenLoggers = CreateChildren(loggerConfig.Children, logger);
                loggers.AddRange(childrenLoggers);
            }
        }

        return loggers;
    }

    private static ILayout CreateLayout(LayoutType layoutType) =>
        layoutType switch
        {
            LayoutType.Standard => new StandardLayout(),
            _ => throw new ArgumentOutOfRangeException(nameof(layoutType), layoutType, null)
        };

    private static Exporter CreateExporterType(ExporterConfig exporterConfig) =>
        exporterConfig.Type switch
        {
            ExporterType.Console => new ConsoleExporter(),
            ExporterType.File => new FileExporter(exporterConfig.FileName),
            ExporterType.Composite => new CompositeExporter(
                exporterConfig.Children.Select(CreateExporterType).ToArray()),
            _ => throw new ArgumentOutOfRangeException(nameof(exporterConfig.Type), exporterConfig.Type, null)
        };
}
using Chapter._4._4.H.日誌框架.Domain;
using Chapter._4._4.H.日誌框架.Domain.LogFramework;
using Chapter._4._4.H.日誌框架.Domain.LogFramework.Exporters;
using Chapter._4._4.H.日誌框架.Domain.LogFramework.Layouts;

var root = Logger.Root(LevelThreshold.DEBUG, new ConsoleExporter(), new StandardLayout());
var gameLogger = new Logger("app.game", root)
{
    LevelThreshold = LevelThreshold.INFO,
    Exporter = new CompositeExporter(
        new ConsoleExporter(),
        new CompositeExporter(
            new FileExporter("game.log"),
            new FileExporter("game.backup.log")
        )
    )
};

var aiLogger = new Logger("app.game.ai", gameLogger)
{
    LevelThreshold = LevelThreshold.TRACE,
    Layout = new StandardLayout()
};

// Configured in code 
LoggerManager.DeclareLoggers(root, gameLogger, aiLogger);

// Configure logging without code
// LoggerManager.AddLoggerFrom("loggers.json");

var game = new Game();
game.Start();
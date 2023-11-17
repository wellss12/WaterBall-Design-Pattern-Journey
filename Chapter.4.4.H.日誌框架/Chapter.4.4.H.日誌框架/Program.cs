using Chapter._4._4.H.日誌框架;
using Chapter._4._4.H.日誌框架.Domain;
using Chapter._4._4.H.日誌框架.Domain.LogFramework;
using Chapter._4._4.H.日誌框架.Domain.LogFramework.Exporters;
using Chapter._4._4.H.日誌框架.Domain.LogFramework.Layouts;

Console.WriteLine("Hello, World!");

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

LoggerManager.DeclareLoggers(root, gameLogger, aiLogger);
var game = new Game();
game.Start();
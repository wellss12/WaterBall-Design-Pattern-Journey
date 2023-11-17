using Chapter._4._4.H.日誌框架.Domain.LogFramework;

namespace Chapter._4._4.H.日誌框架.Domain;

public class AI
{
    private readonly Logger _logger = LoggerManager.GetLogger("app.game.ai");

    public AI(string name)
    {
        Name = name;
    }

    public void MakeDecision()
    {
        _logger.Trace($"{Name} starts making decisions...");

        _logger.Warn($"{Name} decides to give up.");
        _logger.Error("Something goes wrong when AI gives up.");

        _logger.Trace($"{Name} completes its decision.");
    }

    public string Name { get; }
}
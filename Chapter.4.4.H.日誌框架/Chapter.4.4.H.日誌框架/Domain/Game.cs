using Chapter._4._4.H.日誌框架.Domain.LogFramework;

namespace Chapter._4._4.H.日誌框架.Domain;

public class Game
{
    private readonly Logger _logger = LoggerManager.GetLogger("app.game");
    private readonly List<AI> _players = new()
    {
        new AI("AI 1"), new AI("AI 2"), new AI("AI 3"), new AI("AI 4")
    };

    public void Start()
    {
        _logger.Info("The game begins.");

        foreach (var ai in _players)
        {
            _logger.Trace($"The player {ai.Name} begins his turn.");
            ai.MakeDecision();
            _logger.Trace($"The player {ai.Name} finishes his turn.");
        }

        _logger.Debug("Game ends.");
    }
}
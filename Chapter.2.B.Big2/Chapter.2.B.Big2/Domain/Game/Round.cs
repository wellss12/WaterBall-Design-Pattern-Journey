using Chapter._2.B.Big2.Domain.CardPatterns;
using Chapter._2.B.Big2.Domain.Exceptions;

namespace Chapter._2.B.Big2.Domain.Game;

public class Round
{
    private readonly Big2 _big2;
    private CardPattern _topPlay;
    private Player.Player _topPlayer;
    private int _passCount;
    private int _currentPlayerIndex;
    private bool _isFirstPlayer;

    public Round(Big2 big2)
    {
        _big2 = big2;
    }

    public void Start()
    {
        Console.WriteLine("新的回合開始了。");

        _isFirstPlayer = true;
        while (IsThreePass() is false && _big2.AllPlayersHaveHandCard())
        {
            var currentPlayer = GetCurrentPlayer();
            _currentPlayerIndex = _big2.Players.IndexOf(currentPlayer);
            TakeTurn(currentPlayer);
            _isFirstPlayer = false;
        }
    }

    private void TakeTurn(Player.Player currentPlayer)
    {
        Console.WriteLine($"輪到{currentPlayer.Name}了");

        while (true)
        {
            currentPlayer.HandCard.ShowAll();
            var turnMove = currentPlayer.TakeTurn();
            if (turnMove.IsPass)
            {
                if (IsFirstPlayer())
                {
                    Console.WriteLine("你不能在新的回合中喊 PASS");
                }
                else
                {
                    HandlePass(currentPlayer);
                    break;
                }
            }
            else
            {
                try
                {
                    var cardPattern = _big2.CardPatternMatcher.DecideCardPattern(turnMove.Cards!);
                    ThrowIfInvalidPlay(cardPattern);
                    HandleValidPlay(currentPlayer, cardPattern);
                    break;
                }
                catch (CardPatternNotSupportException ex)
                {
                    Console.WriteLine(ex.Message);
                    turnMove.Undo();
                }
            }
        }
    }

    private void HandlePass(Player.Player player)
    {
        _passCount++;
        Console.WriteLine($"玩家 {player.Name} PASS.");
    }

    private void HandleValidPlay(Player.Player player, CardPattern cardPattern)
    {
        Console.WriteLine($"玩家 {player.Name} 打出了 {cardPattern.Name} {cardPattern}");
        _topPlay = cardPattern;
        _topPlayer = player;
        _passCount = 0;
    }

    private void ThrowIfInvalidPlay(CardPattern cardPattern)
    {
        if (IsInvalidPlay(cardPattern))
        {
            throw new CardPatternNotSupportException();
        }
    }

    private Player.Player GetCurrentPlayer()
    {
        return IsFirstPlayer()
            ? GetFirstPlayer()
            : GetNextPlayer();
    }

    private Player.Player GetFirstPlayer()
    {
        var hasClub3Player = _big2.GetPlayerWhoHasClub3();
        if (hasClub3Player is not null)
        {
            return hasClub3Player;
        }

        var previousRoundTopPlayer = _big2.Rounds[^2]._topPlayer;
        return previousRoundTopPlayer;
    }


    private Player.Player GetNextPlayer()
    {
        return _big2.Players[(_currentPlayerIndex + 1) % 4];
    }

    private bool IsInvalidPlay(CardPattern cardPattern)
    {
        if (IsFirstPlayer() && IsClub3PlayerNotPlayed())
        {
            return true;
        }

        if (IsFirstPlayer())
        {
            return false;
        }

        if (cardPattern.GetType() != _topPlay.GetType())
        {
            return true;
        }

        var comparer = _big2.ComparersLookup[cardPattern.GetType()];
        return comparer.Compare(cardPattern, _topPlay) <= 0;
    }

    private bool IsClub3PlayerNotPlayed()
    {
        return _big2.GetPlayerWhoHasClub3() is not null;
    }

    private bool IsFirstPlayer()
    {
        return _isFirstPlayer;
    }

    private bool IsThreePass()
    {
        return _passCount == 3;
    }
}
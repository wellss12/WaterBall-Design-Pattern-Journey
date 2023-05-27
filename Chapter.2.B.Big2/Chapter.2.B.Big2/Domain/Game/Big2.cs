using Chapter._2.B.Big2.Domain.Comparer;
using Chapter._2.B.Big2.Domain.Matcher;

namespace Chapter._2.B.Big2.Domain.Game;

public class Big2
{
    private Domain.Deck.Deck Deck { get; }
    public readonly List<Player.Player> Players;
    public readonly CardPatternMatcher CardPatternMatcher;
    public readonly Dictionary<Type, SameCardPatternComparer> ComparersLookup;
    public List<Round> Rounds { get; }

    public Big2(
        List<Player.Player> players,
        Domain.Deck.Deck deck,
        CardPatternMatcher cardPatternMatcher,
        Dictionary<Type, SameCardPatternComparer> comparersLookup)
    {
        Deck = deck;
        Players = players;
        CardPatternMatcher = cardPatternMatcher;
        ComparersLookup = comparersLookup;
        Rounds = new List<Round>();
    }

    public void Start()
    {
        PlayerNameHimSelf();
        DeckDeal();
        RoundStart();
        DisplayWinner();
    }

    private void DisplayWinner()
    {
        var winner = GetWinner();
        Console.WriteLine($"遊戲結束，遊戲的勝利者為 {winner.Name}");
    }

    private Player.Player GetWinner()
    {
        return Players.Single(player => player.HasHandCard() is false);
    }

    private void RoundStart()
    {
        while (AllPlayersHaveHandCard())
        {
            var round = new Round(this);
            Rounds.Add(round);
            round.Start();
        }
    }

    public Player.Player? GetPlayerWhoHasClub3()
    {
        return Players.SingleOrDefault(player => player.HandCard.HasClub3());
    }

    public bool AllPlayersHaveHandCard()
    {
        return Players.All(player => player.HasHandCard());
    }

    private void DeckDeal()
    {
        while (Deck.HasCards())
        {
            foreach (var player in Players.TakeWhile(player => player.HandCard.Count() < 13))
            {
                player.AddHandCard(Deck.Deal());
            }
        }
    }

    private void PlayerNameHimSelf()
    {
        Players.ForEach(player => player.NameHimSelf());
    }
}
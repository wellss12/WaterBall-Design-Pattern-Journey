using Chapter;

public class Round
{
    public readonly Game Game;
    public Dictionary<Player, Card> ScoreLookup { get; }

    public Round(Game game)
    {
        Game = game;
        ScoreLookup = new Dictionary<Player, Card>();
    }

    public void Start()
    {
        Console.WriteLine($"第{Game.Rounds.Count}回合開始!");

        PlayersTakeTurn();
        DisplayShowCardResult();

        var winner = Showdown();
        winner.IncreasePoint();

        DisplayWinner(winner.Name);
    }

    private void DisplayWinner(string winnerName)
    {
        Console.WriteLine($"第{Game.Rounds.Count}回合結束，贏家是: {winnerName} ");
    }

    private void PlayersTakeTurn()
    {
        Game.Players.ForEach(player => player.TakeTurn());
    }

    private Player Showdown()
    {
        var winner = ScoreLookup.First().Key;
        var winnerCard = ScoreLookup.First().Value;

        foreach (var (player, card) in ScoreLookup)
        {
            var result = card.Compare(winnerCard);
            winner = result > 0 ? player : winner;
        }

        return winner;
    }

    private void DisplayShowCardResult()
    {
        Console.WriteLine("各玩家出牌結果為:");
        foreach (var (player, card) in ScoreLookup)
        {
            Console.WriteLine($"{player.Name}: {card}");
        }
    }
}
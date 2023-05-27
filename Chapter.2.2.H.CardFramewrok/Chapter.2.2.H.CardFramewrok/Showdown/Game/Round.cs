public class Round
{
    private readonly Showdown _game;
    public Dictionary<ShowdownPlayer, ShowdownCard> ScoreLookup { get; }

    public Round(Showdown game)
    {
        _game = game;
        ScoreLookup = new Dictionary<ShowdownPlayer, ShowdownCard>();
    }

    public void Start()
    {
        Console.WriteLine($"第{_game.Rounds.Count}回合開始!");

        PlayersTakeTurn();
        DisplayShowCardResult();

        var winner = Showdown();
        winner.IncreasePoint();

        Console.WriteLine($"第{_game.Rounds.Count}回合結束，贏家是: {winner.Name} ");
    }

    private void PlayersTakeTurn()
    {
        _game.Players.ForEach(player => player.TakeTurn());
    }

    private ShowdownPlayer Showdown()
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
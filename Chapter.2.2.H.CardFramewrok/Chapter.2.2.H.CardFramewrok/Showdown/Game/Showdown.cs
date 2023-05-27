public class Showdown : CardGame<ShowdownPlayer, ShowdownCard>
{
    public Showdown(List<ShowdownPlayer> players) : base(players, new ShowdownDeck())
    {
        Rounds = new List<Round>(RoundLimitCount);
    }

    public List<Round> Rounds { get; }
    private const int RoundLimitCount = 13;
    protected override int StartingHandCardCount => 13;

    protected override void ExecuteRoundAction()
    {
        var round = new Round(this);
        Rounds.Add(round);
        round.Start();
    }

    protected override bool RoundEndCondition()
    {
        return Rounds.Count >= RoundLimitCount;
    }

    protected override void DisplayWinner()
    {
        var finalWinner = Players.MaxBy(player => player.Point);
        Console.WriteLine($"""
最後贏家是: {finalWinner.Name} 
獲得 {finalWinner.Point} 點
""");
        Console.ReadLine();
    }
}
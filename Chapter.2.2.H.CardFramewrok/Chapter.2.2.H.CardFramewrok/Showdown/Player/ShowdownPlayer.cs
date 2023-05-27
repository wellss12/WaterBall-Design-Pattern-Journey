public abstract class ShowdownPlayer : Player<ShowdownCard>
{
    protected ShowdownPlayer() : base(new HandCard<ShowdownCard>(13))
    {
    }

    public Showdown Game { get; set; }
    public int Point { get; private set; }

    public void TakeTurn()
    {
        Console.WriteLine($"{Name} 的手牌有 {HandCard.AllCards()}");
        Console.WriteLine($"{Name} 要出第幾張牌?");

        var round = Game.Rounds.Last();
        round.ScoreLookup.Add(this, ShowCard());
    }

    public void IncreasePoint()
    {
        Point++;
    }
}
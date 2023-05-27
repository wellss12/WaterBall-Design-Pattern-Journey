public class UNO : CardGame<UNOPlayer, UNOCard>
{
    public UNO(List<UNOPlayer> players) : base(players, new UNODeck())
    {
        OnTableCards = new List<UNOCard>();
    }

    public UNOCard OnTableLatestCard { get; set; }
    public List<UNOCard> OnTableCards { get; } 
    protected override int StartingHandCardCount => 5;

    protected override void ExecutePreRoundAction() => ShowFirstCard();

    protected override void ExecuteRoundAction() => Players.ForEach(player => player.TakeTurn());

    protected override bool RoundEndCondition() => AnyPlayersHandCardEmpty();

    protected override void DisplayWinner()
    {
        var winner = Players.First(player => player.HandCard.Any() is false);
        Console.WriteLine($"{winner.Name} 最快打完手牌，獲勝了!");
        Console.ReadLine();
    }

    public void PutCardsToDeck()
    {
        var onTableCardsWithoutLatest = OnTableCards.Where(card => card != OnTableLatestCard);
        Deck.AddRange(onTableCardsWithoutLatest);
        OnTableCards.RemoveAll(card => !card.Equals(OnTableLatestCard));
        Console.WriteLine("沒牌了，將場上的牌放回牌堆中");
    }

    private bool AnyPlayersHandCardEmpty()
    {
        return Players.Any(player => player.HandCard.Any() is false);
    }

    private void ShowFirstCard()
    {
        OnTableLatestCard = Deck.DrawCard();
        OnTableCards.Add(OnTableLatestCard);
        Console.WriteLine($"場面上的牌為:{OnTableLatestCard}");
    }
}
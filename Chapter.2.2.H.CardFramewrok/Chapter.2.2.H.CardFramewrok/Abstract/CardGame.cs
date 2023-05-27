public abstract class CardGame<TPlayer, TCard> 
    where TPlayer : Player<TCard> 
    where TCard : Card
{
    protected CardGame(List<TPlayer> players, Deck<TCard> deck)
    {
        Players = players;
        Deck = deck;
    }

    public List<TPlayer> Players { get; }
    public Deck<TCard> Deck { get; }
    protected abstract int StartingHandCardCount { get; }

    public void Start()
    {
        PlayersNameHimSelf();
        DeckShuffle();
        PlayersDrawCard();
        RoundStart();
        DisplayWinner();
    }

    private void PlayersDrawCard()
    {
        Console.WriteLine($"開始輪流抽牌，直到所有人都{StartingHandCardCount}張");

        while (Players.All(player => player.HandCard.Count() != StartingHandCardCount))
        {
            foreach (var player in Players.TakeWhile(player => player.HandCard.Count() < StartingHandCardCount))
            {
                player.AddHandCard(Deck.DrawCard());
            }
        }

        Console.WriteLine("抽完牌了");
    }

    protected abstract void DisplayWinner();

    private void RoundStart()
    {
        ExecutePreRoundAction();

        while (true)
        {
            ExecuteRoundAction();

            if (RoundEndCondition())
            {
                break;
            }
        }
    }

    protected virtual void ExecutePreRoundAction()
    {
        //預設不做事
    }

    protected abstract void ExecuteRoundAction();

    protected abstract bool RoundEndCondition();


    private void PlayersNameHimSelf()
    {
        foreach (var player in Players)
        {
            Console.WriteLine("請輸入自己的名字:");
            player.NameHimSelf();
        }
    }

    private void DeckShuffle() => Deck.Shuffle();
}
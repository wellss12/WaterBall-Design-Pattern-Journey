namespace Chapter;

public abstract class Player
{
    public Game Game { get; set; }
    public HandsExchange? HandsExchanger { get; set; }
    public List<HandsExchange> HandsExchangees { get; } = new(3);
    public HandCard HandCard { get; set; }
    public string Name { get; protected set; }
    public int Point { get; private set; }

    /// <summary>
    /// 交換手牌到交換回來 整個流程已完成
    /// </summary>
    public bool IsExchangeComplete { get; set; }

    protected Player(HandCard handCard)
    {
        HandCard = handCard;
    }

    public abstract void NameHimSelf();
    protected abstract Card ShowCard();
    protected abstract Player DecideExchangePlayer();
    protected abstract bool IsExchangeHands();

    public void AddHandCard(Card card)
    {
        if (HandCard.Count >= 13)
        {
            throw new Exception("Hands can only have 13 cards");
        }

        HandCard.Cards.Add(card);
    }

    public void TakeTurn()
    {
        if (HasCardExchanged() is false && IsExchangeComplete is false)
        {
            if (HasSameHandsCount() is false)
            {
                Console.WriteLine("其他玩家跟你的手牌數量都不一致，你只能出牌了喔");
            }
            else
            {
                MakeExchangeHandsDecision();
            }
        }

        if (HasCardExchanged() && IsExchangeComplete is false)
        {
            var roundCountdown = HandsExchanger!.RoundCountdown;
            if (roundCountdown > 0)
            {
                HandsExchanger.RoundCountdown--;
            }
            else if (roundCountdown == 0)
            {
                HandsExchanger.ExchangeBack();
            }
        }

        Console.WriteLine($"{Name} 的手牌有 {HandCard.AllCards()}");
        Console.WriteLine($"{Name} 要出第幾張牌?");

        var round = Game.Rounds.Last();
        round.ScoreLookup.Add(this, ShowCard());
    }

    private bool HasSameHandsCount()
    {
        return Game.Players.Any(player => player.HandCard.Count == HandCard.Count && player.Name != Name);
    }

    private bool HasCardExchanged()
    {
        return HandsExchanger is not null;
    }

    public void IncreasePoint()
    {
        Point++;
    }


    private void MakeExchangeHandsDecision()
    {
        if (IsExchangeHands())
        {
            ExchangeHands(DecideExchangePlayer());
        }
    }

    private void ExchangeHands(Player exchangee)
    {
        if (HasCardExchanged() || IsExchangeComplete)
        {
            throw new Exception("Exchange hands can only be used once");
        }

        HandsExchanger = new HandsExchange(this, exchangee);
        exchangee.HandsExchangees.Add(HandsExchanger);

        (HandCard, exchangee.HandCard) = (exchangee.HandCard, HandCard);

        Console.WriteLine($"{Name} 跟 {exchangee.Name} 手牌已交換，三回合會換回來");
    }
}
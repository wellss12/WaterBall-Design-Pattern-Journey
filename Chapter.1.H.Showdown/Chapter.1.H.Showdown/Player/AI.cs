namespace Chapter;

public class AI : Player
{
    private readonly Random _random;

    public AI(HandCard handCard) : base(handCard)
    {
        _random = new Random();
    }

    public override void NameHimSelf()
    {
        var number = GetRandomNumber();
        var name = $"{nameof(AI)}-{number}";
        Name = name;
        Console.WriteLine(name);
    }

    protected override Card ShowCard()
    {
        var index = GetRandomNumber(0, HandCard.Count);
        var card = HandCard.Cards[index];
        HandCard.Cards.Remove(card);
        Console.WriteLine(index + 1);

        return card;
    }

    protected override Player DecideExchangePlayer()
    {
        Console.WriteLine($"{Name} 要跟哪一位玩家交換? 請輸入玩家名稱");

        var players = Game.Players
            .Where(player => player.Name != Name && player.HandCard.Count == HandCard.Count)
            .ToList();
        
        var randomIndex = GetRandomNumber(0, players.Count);
        var exchangee = players[randomIndex];
        Console.WriteLine(exchangee.Name);
        
        return exchangee;
    }


    protected override bool IsExchangeHands()
    {
        Console.WriteLine($"{Name} 要不要跟其他人交換手排?  回答 y/n");

        var number = GetRandomNumber(0, 2);
        Console.WriteLine(number == 0 ? "n" : "y");

        return Convert.ToBoolean(number);
    }

    private int GetRandomNumber(int minValue = 0, int maxValue = 1000)
    {
        return _random.Next(minValue, maxValue);
    }
}
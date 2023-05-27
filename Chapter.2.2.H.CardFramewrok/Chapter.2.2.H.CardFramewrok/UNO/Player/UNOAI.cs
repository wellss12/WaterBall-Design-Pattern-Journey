public class UNOAI : UNOPlayer
{
    private readonly Random _random;

    public UNOAI()
    {
        _random = new Random();
    }

    public override void NameHimSelf()
    {
        var number = GetRandomNumber();
        var name = $"{nameof(UNOAI)}-{number}";
        Name = name;
        Console.WriteLine(name);
    }

    protected override UNOCard ShowCard()
    {
        var showableCards = GetShowableCards();
        var index = GetRandomNumber(0, showableCards.Count);
        var targetCard = showableCards[index];
        var targetIndex = HandCard.IndexOf(targetCard);
        
        HandCard.Remove(targetCard);
        Console.WriteLine(targetIndex + 1);

        return targetCard;
    }

    private int GetRandomNumber(int minValue = 0, int maxValue = 1000)
    {
        return _random.Next(minValue, maxValue);
    }
}
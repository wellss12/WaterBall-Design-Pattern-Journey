public class ShowdownAI : ShowdownPlayer
{
    private readonly Random _random;

    public ShowdownAI()
    {
        _random = new Random();
    }

    public override void NameHimSelf()
    {
        var number = GetRandomNumber();
        var name = $"{nameof(ShowdownAI)}-{number}";
        Name = name;
        Console.WriteLine(name);
    }

    protected override ShowdownCard ShowCard()
    {
        var index = GetRandomNumber(0, HandCard.Count());
        var card = HandCard[index];
        HandCard.Remove(card);
        Console.WriteLine(index + 1);

        return card;
    }

    private int GetRandomNumber(int minValue = 0, int maxValue = 1000)
    {
        return _random.Next(minValue, maxValue);
    }
}
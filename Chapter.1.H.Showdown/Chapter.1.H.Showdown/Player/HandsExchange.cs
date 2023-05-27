namespace Chapter;

public class HandsExchange
{
    private int _roundCountdown = 3;

    public HandsExchange(Player exchanger, Player exchangee)
    {
        Exchanger = exchanger;
        Exchangee = exchangee;
    }

    public Player Exchanger { get; }
    public Player Exchangee { get; }

    public int RoundCountdown
    {
        get => _roundCountdown;
        set => _roundCountdown = value is <= 3 and >= 0
            ? value
            : throw new ArgumentOutOfRangeException();
    }

    public void ExchangeBack()
    {
        (Exchanger.HandCard, Exchangee.HandCard) = (Exchangee.HandCard, Exchanger.HandCard);
        Exchanger.IsExchangeComplete = true;

        var handsExchanger = Exchanger.HandsExchanger;
        Exchanger.HandsExchanger = null;
        Exchangee.HandsExchangees.Remove(handsExchanger);

        Console.WriteLine($"{handsExchanger.Exchanger.Name} 跟 {handsExchanger.Exchangee.Name}手牌已交換回來!");
    }
}
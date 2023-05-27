public class UNODeck : Deck<UNOCard>
{
    public UNODeck()
    {
        Cards = Enum.GetValues<Color>()
            .SelectMany(color => Enumerable.Range(0, 10).Select(number => new UNOCard(color, number)))
            .ToList();
    }
}
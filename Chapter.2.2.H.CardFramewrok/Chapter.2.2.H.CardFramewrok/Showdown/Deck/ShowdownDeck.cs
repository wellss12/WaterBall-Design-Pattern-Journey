public class ShowdownDeck : Deck<ShowdownCard>
{
    public ShowdownDeck()
    {
        Cards = Enum.GetValues<Suit>()
            .SelectMany(_ => Enum.GetValues<Rank>(), (suit, rank) => new ShowdownCard(suit, rank))
            .ToList();
    }
}
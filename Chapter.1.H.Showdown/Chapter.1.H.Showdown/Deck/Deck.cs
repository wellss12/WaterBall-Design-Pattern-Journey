public class Deck
{
    private readonly List<Card> Cards;

    public Deck()
    {
        Cards = Enum.GetValues<Suit>()
            .SelectMany(_ => Enum.GetValues<Rank>(), (suit, rank) => new Card(suit, rank))
            .ToList();
    }

    public bool HasCard()
    {
        return Cards.Any();
    }

    public void Shuffle()
    {
        // Knuth shuffle
        var random = new Random();
        var cardCount = Cards.Count;

        while (cardCount > 1)
        {
            cardCount--;
            var randomIndex = random.Next(cardCount + 1);
            (Cards[randomIndex], Cards[cardCount]) = (Cards[cardCount], Cards[randomIndex]);
        }
    }

    public Card DrawCard()
    {
        if (!HasCard())
        {
            throw new Exception("52 cards have been drawn");
        }

        var topCard = Cards[0];
        Cards.RemoveAt(0);
        return topCard;
    }
}